using Curds.Application.DateTimes;
using Curds.Application.Persistence;
using Curds.Application.Security;
using Curds.Application.Security.Command;
using Curds.Domain.Security;
using System;
using System.Security.Authentication;
using System.Threading.Tasks;

namespace Curds.Infrastructure.Security
{
    public class SecurityProvider : ISecurity
    {
        public IDateTime Time { get; }
        public ISecurityPersistence Persistence { get; }

        public SecurityProvider(IDateTime time, ISecurityPersistence persistence)
        {
            Time = time;
            Persistence = persistence;
        }

        private async Task<Session> GenerateSession(int userID, string series = null)
        {
            Session generated = new Session
            {
                Identifier = Session.NewSessionID,
                Series = series,
                UserID = userID,
                Expiration = Time.Fetch,
            };
            generated.IncrementExpiration();
            await Persistence.Sessions.Insert(generated);
            return generated;
        }

        public async Task<Authentication> Login(Login command)
        {
            try
            {
                User user = await Persistence.Users.FindByEmail(command.Email);
                string encryptedPassword = User.EncryptPassword(user.Salt, command.Password);
                if (!user.Password.Equals(encryptedPassword))
                    throw new InvalidOperationException("Passwords do not match");

                ReAuth reAuth = null;
                if (command.RememberMe)
                    reAuth = await GenerateReAuth(user, command.DeviceIdentifier);
                Session session = await GenerateSession(user.ID, reAuth?.Series);

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

        public async Task Logout(LogoutUser command)
        {
            await Persistence.ReAuthentications.Delete(command.UserID);
            await Persistence.Sessions.Delete(command.UserID);
        }

        public async Task Logout(LogoutSeries command)
        {
            await Persistence.ReAuthentications.Delete(command.Series);
            await Persistence.Sessions.Delete(command.Series);
        }

        public async Task<Authentication> ReAuthenticate(ReAuthenticate command)
        {
            try
            {
                ReAuth currentReAuth = await Persistence.ReAuthentications.Lookup(command.Series);
                if (currentReAuth.Token != command.Token)
                {
                    await Logout(new LogoutUser(currentReAuth.UserID));
                    throw new InvalidOperationException("Supplied token did not match, user is being logged out");
                }
                string newToken = ReAuth.NewToken;
                await Persistence.ReAuthentications.Update(currentReAuth.Series, newToken);
                currentReAuth.Token = newToken;
                await Persistence.Sessions.Delete(currentReAuth.Series);
                Session currentSession = await GenerateSession(currentReAuth.UserID, currentReAuth.Series);
                return new Authentication() { Session = currentSession, ReAuthentication = currentReAuth };
            }
            catch (Exception ex)
            {
                throw new AuthenticationException("ReAuthentication failed", ex);
            }
        }

        public async Task<bool> Validate(ValidateSession command)
        {
            try
            {
                Session toValidate = await Persistence.Sessions.Lookup(command.Identifier);
                if (toValidate.Expiration <= Time.Fetch)
                {
                    Persistence.Sessions.Delete(Time.Fetch).AwaitResult();
                    return false;
                }
                toValidate.IncrementExpiration();
                await Persistence.Sessions.Update(toValidate.Identifier, toValidate.Expiration);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<Authentication> CreateInitialUser(CreateInitialUser command)
        {
            if (await Persistence.Users.Count != 0)
                throw new InvalidOperationException("Cannot create an initial user when users exist");

            User initial = new User
            {
                Name = command.UserName,
                Email = command.Email,
                Salt = User.NewSalt,
            };
            initial.Password = User.EncryptPassword(initial.Salt, command.Password);
            initial = await Persistence.Users.Insert(initial);
            return await Login(new Login(command));
        }
    }
}
