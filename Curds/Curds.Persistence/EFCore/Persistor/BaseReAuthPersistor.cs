using Curds.Application.Persistence.Persistor;
using Curds.Domain.Security;
using System;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Curds.Persistence.EFCore.Persistor
{
    public abstract class BaseReAuthPersistor<T> : BasePersistor<T, ReAuth>, IReAuthPersistor<ReAuth>
        where T : SecureCurdsContext
    {
        public BaseReAuthPersistor(EFProvider<T> provider)
            : base(provider)
        { }

        public Task<ReAuth> Lookup(string series)
        {
            using (T context = Provider.Context)
                return Lookup(series, context);
        }
        private Task<ReAuth> Lookup(string series, T context) =>
            context.ReAuthentications
                .FindAsync(series);

        public async Task<List<ReAuth>> Lookup(int userID)
        {
            using (T context = Provider.Context)
                return await Lookup(userID, context);
        }
        private Task<List<ReAuth>> Lookup(int userID, T context) =>
            context.ReAuthentications
                .Where(r => r.UserID == userID)
                .ToListAsync();

        public async Task Update(string series, string newToken)
        {
            using (T context = Provider.Context)
            {
                await Update(series, newToken, context);
                await context.SaveChangesAsync();
            }
        }
        private async Task Update(string series, string newToken, T context)
        {
            ReAuth toUpdate = await Lookup(series, context);
            toUpdate.Token = newToken;
        }

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
            ReAuth toDelete = await Lookup(series, context);
            context.Remove(toDelete);
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
            List<ReAuth> userReAuths = await Lookup(userID, context);
            foreach (ReAuth toDelete in userReAuths)
                context.Remove(toDelete);
        }
    }
}
