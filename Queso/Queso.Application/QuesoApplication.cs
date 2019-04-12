using System;
using System.Collections.Generic;
using System.Text;
using Curds.Application;

namespace Queso.Application
{
    public class QuesoApplication : CurdsApplication
    {
        internal Character.ICharacter Character { get; }

        public override string Description => "An application for Diablo 2 character management.";

        public Message.Command.Dispatch Commands { get; }
        public Message.Query.Dispatch Queries { get; }

        public QuesoApplication(QuesoOptions options)
            : base(options)
        {
            Character = options.Character;
            Commands = new Message.Command.Dispatch(this);
            Queries = new Message.Query.Dispatch(this);
        }
    }
}
