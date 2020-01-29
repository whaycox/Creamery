﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Persistence.Implementation
{
    using Abstraction;
    using Domain;

    internal class SqlConnectionStringFactory : ISqlConnectionStringFactory
    {
        public string Build(SqlConnectionInformation connectionInformation)
        {
            if (UserNameOrPasswordSupplied(connectionInformation))
            {
                if (!UserNameAndPasswordSupplied(connectionInformation))
                    throw new FormatException($"Both {nameof(SqlConnectionInformation.UserName)} and {nameof(SqlConnectionInformation.Password)} must be supplied if either is supplied");

                throw new NotImplementedException();
            }
            else
                return $"Server={connectionInformation.Server};Database={connectionInformation.Database};Trusted_Connection=True;";
        }
        private bool UserNameOrPasswordSupplied(SqlConnectionInformation connectionInformation) =>
            !string.IsNullOrEmpty(connectionInformation.UserName) ||
            !string.IsNullOrEmpty(connectionInformation.Password);
        private bool UserNameAndPasswordSupplied(SqlConnectionInformation connectionInformation) =>
            !string.IsNullOrEmpty(connectionInformation.UserName) &&
            !string.IsNullOrEmpty(connectionInformation.Password);
    }
}
