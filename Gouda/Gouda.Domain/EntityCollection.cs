using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Gouda.Domain
{
    using Communication;
    using Check;

    public class EntityCollection
    {
        private List<Satellite> _satellites = new List<Satellite>();
        public List<Satellite> Satellites => _satellites.ToList();
        public Dictionary<int, Satellite> SatelliteMap => _satellites.ToDictionary(s => s.ID);
        public void Add(Satellite satellite) => _satellites.Add(satellite);
        public void Add(IEnumerable<Satellite> satellites) => _satellites.AddRange(satellites);

        private List<Definition> _definitions = new List<Definition>();
        public List<Definition> Definitions => _definitions.ToList();
        public void Add(Definition definition) => _definitions.Add(definition);
        public void Add(IEnumerable<Definition> definitions) => _definitions.AddRange(definitions);

        private List<User> _users = new List<User>();
        public List<User> Users => _users.ToList();
        public void Add(User user) => _users.Add(user);
        public void Add(IEnumerable<User> users) => _users.AddRange(users);
    }
}
