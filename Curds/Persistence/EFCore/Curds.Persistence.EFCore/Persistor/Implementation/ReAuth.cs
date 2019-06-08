using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Curds.Persistence.EFCore.Persistor.Implementation
{
    using EFCore.Domain;
    using Security.Domain;

    public abstract class ReAuth<T> : BaseEntity<T, ReAuth>
        where T : SecureContext
    {
        protected override DbSet<ReAuth> Set(T context) => context.ReAuthentications;

        public async Task<ReAuth> Lookup(string series)
        {
            using (T context = Context)
                return await Lookup(context, series);
        }
        public Task<ReAuth> Lookup(T context, string series) => Set(context).FindAsync(series);

        public async Task<List<ReAuth>> Lookup(int userID)
        {
            using (T context = Context)
                return await Lookup(context, userID);
        }
        public Task<List<ReAuth>> Lookup(T context, int userID) => Set(context).Where(r => r.UserID == userID).ToListAsync();

        public async Task Update(string series, string newToken)
        {
            using (T context = Context)
            {
                await Update(context, series, newToken);
                await context.SaveChangesAsync();
            }
        }
        public async Task Update(T context, string series, string newToken)
        {
            ReAuth toUpdate = await Lookup(context, series);
            toUpdate.Token = newToken;
        }

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
            ReAuth toDelete = await Lookup(context, series);
            context.Remove(toDelete);
        }

        public async Task Delete(int userID)
        {
            using (T context = Context)
            {
                await Delete(context, userID);
                await context.SaveChangesAsync();
            }
        }
        public async Task Delete(T context, int userID) => context.RemoveRange(await Lookup(context, userID));
    }
}
