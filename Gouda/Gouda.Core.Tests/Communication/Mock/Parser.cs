namespace Gouda.Communication.Mock
{
    using Enumerations;

    public class Parser : Domain.Parser
    {
        public Parser(byte[] buffer)
            : base(buffer)
        { }

        public override Abstraction.ICommunicableObject ParseObject(CommunicableType type)
        {
            if (type == CommunicableType.Mock)
                return new ICommunicableObject(this);
            return base.ParseObject(type);
        }
    }
}
