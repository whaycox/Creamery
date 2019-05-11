using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Curds.Persistence.Persistor.Mock
{
    using System.Threading.Tasks;
    using Security.Domain;

    public class IUserPersistor : ProtoMock<User>, Abstraction.IUserPersistor<User>
    {
        protected override List<User> Samples => new List<User>
        {
            new Security.Mock.User(1),
            new Security.Mock.User(2),
            new Security.Mock.User(3),
        };

        public Task<int> Count => Task.Run(() => Samples.Count);

        public List<int> DeletedEntities = new List<int>();
        public Task Delete(int id) => Task.Run(() => DeletedEntities.Add(id));

        public Task<List<User>> FetchAll() => Task.Run(() => Samples);

        public Task<User> FindByEmail(string email) => Task.Run(() => FindByEmailInternal(email));
        private User FindByEmailInternal(string email) => Samples.Where(s => s.Email == email).FirstOrDefault();

        public List<User> InsertedUsers = new List<User>();
        public Task<User> Insert(User newEntity) => Task.Run(() => InsertInternal(newEntity));
        private User InsertInternal(User newEntity)
        {
            InsertedUsers.Add(newEntity);
            return newEntity;
        }

        public Task<User> Lookup(int id) => Task.Run(() => LookupInternal(id));
        private User LookupInternal(int id) => Samples.Where(s => s.ID == id).FirstOrDefault();

        public Task<List<User>> LookupMany(IEnumerable<int> ids) => Task.Run(() => LookupManyInternal(ids));
        private List<User> LookupManyInternal(IEnumerable<int> ids) => Samples.Where(s => ids.Contains(s.ID)).ToList();

        public List<int> UpdatedUsers = new List<int>();
        public Task Update(int id, Func<User, User> updateDelegate) => Task.Run(() => UpdatedUsers.Add(id));
    }
}
