using System;
using System.Collections.Generic;
using System.Text;
using Curds.Application.Message.Query;
using Curds.Application.Message;
using Queso.Application.Message.ViewModels;
using System.Threading.Tasks;

namespace Queso.Application.Message.Query.Character
{
    using Queso.Application.Message.ViewModels;

    public class ScanQuery : BaseQuery
    {
        public string CharacterPath { get; }

        public ScanQuery(string characterPath)
        {
            CharacterPath = characterPath;
        }
    }

    public class ScanDefinition : BaseQueryDefinition<QuesoApplication, ScanQuery, Character>
    {
        public ScanDefinition(QuesoApplication application)
            : base(application)
        { }

        public override Task<Character> Execute(ScanQuery message) => Task.Factory.StartNew(() => ExecuteAndReturn(message));
        private Character ExecuteAndReturn(ScanQuery query)
        {
            Domain.Character loaded = Application.Character.Load(query.CharacterPath);
            return new Character()
            {
                Name = loaded.Name,
                Class = loaded.Class,
                Alive = loaded.Alive,
            };
        }
    }
}
