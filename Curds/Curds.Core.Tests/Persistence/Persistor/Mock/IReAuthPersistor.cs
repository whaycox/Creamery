using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Curds.Persistence.Persistor.Mock
{
    using Security.Domain;

    public class IReAuthPersistor : ProtoMock<ReAuth>, Abstraction.IReAuthPersistor<ReAuth>
    {
        protected override List<ReAuth> Samples => new List<ReAuth>
        {
            new Security.Mock.ReAuth(1),
        };

        public Task<int> Count => Task.Run(() => Samples.Count);

        public List<string> DeletedSeries = new List<string>();
        public Task Delete(string series) => Task.Run(() => DeletedSeries.Add(series));

        public List<int> DeletedUsers = new List<int>();
        public Task Delete(int userID) => Task.Run(() => DeletedUsers.Add(userID));

        public Task<List<ReAuth>> FetchAll() => Task.Run(() => Samples);

        public List<ReAuth> InsertedReAuths = new List<ReAuth>();
        public Task<ReAuth> Insert(ReAuth newEntity) => Task.Run(() => InsertInternal(newEntity));
        private ReAuth InsertInternal(ReAuth newEntity)
        {
            InsertedReAuths.Add(newEntity);
            return newEntity;
        }

        public Task<ReAuth> Lookup(string series) => Task.Run(() => LookupInternal(series));
        private ReAuth LookupInternal(string series) => Samples.Where(s => s.Series == series).FirstOrDefault();

        public Task<List<ReAuth>> Lookup(int userID) => Task.Run(() => LookupInternal(userID));
        private List<ReAuth> LookupInternal(int userID) => Samples.Where(s => s.UserID == userID).ToList();

        public List<(string series, string token)> UpdatedReAuths = new List<(string series, string token)>();
        public Task Update(string series, string newToken) => Task.Run(() => UpdatedReAuths.Add((series, newToken)));
    }
}
