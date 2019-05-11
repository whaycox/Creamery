using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Curds.Persistence.Persistor.Mock
{
    using Security.Domain;

    public class ISessionPersistor : ProtoMock<Session>, Abstraction.ISessionPersistor<Session>
    {
        protected override List<Session> Samples => new List<Session>
        {
            new Security.Mock.Session(1),
        };

        public Task<int> Count => Task.Run(() => Samples.Count);

        public List<string> DeletedSeries = new List<string>();
        public Task Delete(string series) => Task.Run(() => DeletedSeries.Add(series));

        public List<int> DeletedUsers = new List<int>();
        public Task Delete(int userID) => Task.Run(() => DeletedUsers.Add(userID));

        public List<DateTimeOffset> DeletedDateTimes = new List<DateTimeOffset>();
        public Task Delete(DateTimeOffset expiration) => Task.Run(() => DeletedDateTimes.Add(expiration));

        public Task<List<Session>> FetchAll() => Task.Run(() => Samples);

        public List<Session> InsertedSessions = new List<Session>();
        public Task<Session> Insert(Session newEntity) => Task.Run(() => InsertInternal(newEntity));
        private Session InsertInternal(Session newEntity)
        {
            InsertedSessions.Add(newEntity);
            return newEntity;
        }

        public Task<Session> Lookup(string id) => Task.Run(() => LookupInternal(id));
        private Session LookupInternal(string id) => Samples.Where(s => s.Identifier == id).FirstOrDefault();

        public List<(string id, DateTimeOffset expiration)> UpdatedSessions = new List<(string id, DateTimeOffset expiration)>();
        public Task Update(string id, DateTimeOffset newExpiration) => Task.Run(() => UpdatedSessions.Add((id, newExpiration)));
    }
}
