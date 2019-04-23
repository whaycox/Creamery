using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Application.Security.Command
{
    using Message.Command;

    public class LogoutUser : BaseCommand
    {
        public int UserID { get; }

        public LogoutUser(int userID)
        {
            UserID = userID;
        }
    }
}
