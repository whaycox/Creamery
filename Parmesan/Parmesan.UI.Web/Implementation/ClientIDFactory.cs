namespace Parmesan.UI.Web.Implementation
{
    using Abstraction;

    internal class ClientIDFactory : IClientIDFactory
    {
        public string ClientID { get; }

        public ClientIDFactory(string clientID)
        {
            ClientID = clientID;
        }
    }
}
