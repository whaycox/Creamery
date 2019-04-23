using System;
using System.Collections.Generic;
using System.Text;
using Curds.Domain.Security;
using System.Threading.Tasks;

namespace Curds.Application.Persistence.Persistor
{
    public interface IReAuthPersistor<T> : IPersistor<T> where T : ReAuth
    {
        Task<T> Lookup(string series);
        Task<List<T>> Lookup(int userID);
        Task Update(string series, string newToken);
        Task Delete(string series);
        Task Delete(int userID);
    }
}
