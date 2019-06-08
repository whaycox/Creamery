using System;
using System.Security.Authentication;
using System.Threading.Tasks;

namespace Curds.Security.Implementation
{
    using Abstraction;
    using Credentials.Domain;
    using Domain;
    using Persistence.Abstraction;
    using Time.Abstraction;

    public class SecurityProvider : ISecurity
    {
        public ITime Time { get; }
        public ISecurityPersistence Persistence { get; }

        public SecurityProvider(ITime time, ISecurityPersistence persistence)
        {
            Time = time;
            Persistence = persistence;
        }

        private async Task<Session> GenerateSession(string deviceIdentifier, int userID, string series = null)
        {
            Session generated = new Session
            {
                Identifier = Session.NewSessionID,
                DeviceIdentifier = deviceIdentifier,
                Series = series,
                UserID = userID,
                Expiration = Time.Fetch,
            };
            generated.ExtendExpiration(Time.Fetch);
            await Persistence.Sessions.Insert(generated);
            return generated;
        }

        public async Task<Authentication> Login(Password passwordCredentials)
        {
            try
            {
                User user = await Persistence.Users.FindByEmail(passwordCredentials.Email);
                string encryptedPassword = User.EncryptPassword(user.Salt, passwordCredentials.RawPassword);
                if (!user.Password.Equals(encryptedPassword))
                    throw new InvalidOperationException("Passwords do not match");

                ReAuth reAuth = null;
                if (passwordCredentials.RememberMe)
                    reAuth = await GenerateReAuth(user, passwordCredentials.DeviceIdentifier);
                Session session = await GenerateSession(passwordCredentials.DeviceIdentifier, user.ID, reAuth?.Series);

                return new Authentication() { Session = session, ReAuthentication = reAuth };
            }
            catch (Exception ex)
            {
                throw new AuthenticationException("Authentication failed", ex);
            }
        }
        private async Task<ReAuth> GenerateReAuth(User user, string deviceIdentifier)
        {
            ReAuth generated = new ReAuth()
            {
                DeviceIdentifier = deviceIdentifier,
                UserID = user.ID,
                Series = ReAuth.NewSeries,
                Token = ReAuth.NewToken,
            };
            return await Persistence.ReAuthentications.Insert(generated);
        }

        public async Task Logout(int userID)
        {
            await Persistence.ReAuthentications.Delete(userID);
            await Persistence.Sessions.Delete(userID);
        }

        public async Task Logout(string series)
        {
            await Persistence.ReAuthentications.Delete(series);
            await Persistence.Sessions.Delete(series);
        }

        public async Task<Authentication> ReAuthenticate(string seriesID, string token)
        {
            try
            {
                ReAuth currentReAuth = await Persistence.ReAuthentications.Lookup(seriesID);
                if (currentReAuth.Token != token)
                {
                    await Logout(currentReAuth.UserID);
                    throw new InvalidOperationException("Supplied token did not match, user is being logged out");
                }
                string newToken = ReAuth.NewToken;
                await Persistence.ReAuthentications.Update(currentReAuth.Series, newToken);
                currentReAuth.Token = newToken;
                await Persistence.Sessions.Delete(currentReAuth.Series);
                Session currentSession = await GenerateSession(currentReAuth.DeviceIdentifier, currentReAuth.UserID, currentReAuth.Series);
                return new Authentication() { Session = currentSession, ReAuthentication = currentReAuth };
            }
            catch (Exception ex)
            {
                throw new AuthenticationException("ReAuthentication failed", ex);
            }
        }

        public async Task<bool> Validate(string sessionID)
        {
            try
            {
                Session toValidate = await Persistence.Sessions.Lookup(sessionID);
                if (toValidate.Expiration <= Time.Fetch)
                {
                    Persistence.Sessions.Delete(Time.Fetch).AwaitResult();
                    return false;
                }
                toValidate.ExtendExpiration(Time.Fetch);
                await Persistence.Sessions.Update(toValidate.Identifier, toValidate.Expiration);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
