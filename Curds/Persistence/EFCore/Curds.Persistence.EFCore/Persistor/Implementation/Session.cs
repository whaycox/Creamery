using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Curds.Persistence.EFCore.Persistor.Implementation
{
    using EFCore.Domain;
    using Security.Domain;

    public abstract class Session<T> : BaseEntity<T, Session>
        where T : SecureContext
    {
        protected override DbSet<Session> Set(T context) => context.Sessions;

        public async Task Delete(string series)
        {
            using (T context = Context)
            {
                await Delete(context, series);
                await context.SaveChangesAsync();
            }
        }
        public async Task Delete(T context, string series)
        {
            context.Sessions.RemoveRange(await context.Sessions.Where(s => s.Series == series).ToListAsync());
        }

        public async Task Delete(int userID)
        {
            using (T context = Context)
            {
                await Delete(context, userID);
                await context.SaveChangesAsync();
            }
        }
        public async Task Delete(T context, int userID)
        {
            context.Sessions.RemoveRange(await context.Sessions.Where(s => s.UserID == userID).ToListAsync());
        }

        public async Task Delete(DateTimeOffset expiration)
        {
            using (T context = Context)
            {
                await Delete(context, expiration);
                await context.SaveChangesAsync();
            }
        }
        public async Task Delete(T context, DateTimeOffset expiration)
        {
            DbSet<Session> set = Set(context);
            set.RemoveRange(await set.Where(s => s.Expiration < expiration).ToListAsync());
        }

        public async Task<Session> Lookup(string id)
        {
            using (T context = Context)
                return await Lookup(context, id);
        }
        public Task<Session> Lookup(T context, string id) => Set(context).FindAsync(id);

        public async Task Update(string id, DateTimeOffset newExpiration)
        {
            using (T context = Context)
            {
                await Update(context, id, newExpiration);
                await context.SaveChangesAsync();
            }
        }
        public async Task Update(T context, string id, DateTimeOffset newExpiration)
        {
            Session toUpdate = await Lookup(context, id);
            toUpdate.Expiration = newExpiration;
        }
    }
}
