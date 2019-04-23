using System;
using System.Collections.Generic;
using System.Text;
using Curds.Domain.Security;
using Curds.Application.Persistence.Persistor;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Curds.Persistence.EFCore.Persistor
{
    public abstract class BaseSessionPersistor<T> : BasePersistor<T, Session>, ISessionPersistor<Session>
        where T : SecureCurdsContext
    {
        public BaseSessionPersistor(EFProvider<T> provider)
            : base(provider)
        { }

        public async Task Delete(string series)
        {
            using (T context = Provider.Context)
            {
                await Delete(series, context);
                await context.SaveChangesAsync();
            }
        }
        private async Task Delete(string series, T context)
        {
            context.Sessions.RemoveRange(await context.Sessions.Where(s => s.Series == series).ToListAsync());
        }

        public async Task Delete(int userID)
        {
            using (T context = Provider.Context)
            {
                await Delete(userID, context);
                await context.SaveChangesAsync();
            }
        }
        private async Task Delete(int userID, T context)
        {
            context.Sessions.RemoveRange(await context.Sessions.Where(s => s.UserID == userID).ToListAsync());
        }

        public async Task Delete(DateTimeOffset expiration)
        {
            using (T context = Provider.Context)
            {
                await Delete(expiration, context);
                await context.SaveChangesAsync();
            }
        }
        private async Task Delete(DateTimeOffset expiration, T context)
        {
            context.Sessions.RemoveRange(await context.Sessions.Where(s => s.Expiration < expiration).ToListAsync());
        }

        public async Task<Session> Lookup(string id)
        {
            using (T context = Provider.Context)
                return await Lookup(id, context);
        }
        private Task<Session> Lookup(string id, T context) => context.Sessions.FindAsync(id);

        public async Task Update(string id, DateTimeOffset newExpiration)
        {
            using (T context = Provider.Context)
            {
                await Update(id, newExpiration, context);
                await context.SaveChangesAsync();
            }
        }
        private async Task Update(string id, DateTimeOffset newExpiration, T context)
        {
            Session toUpdate = await Lookup(id, context);
            toUpdate.Expiration = newExpiration;
        }
    }
}
