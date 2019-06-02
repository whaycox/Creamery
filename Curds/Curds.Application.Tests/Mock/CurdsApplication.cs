namespace Curds.Application.Mock
{
    using Abstraction;

    public class CurdsApplication : Domain.CurdsApplication
    {
        public override string Description => nameof(Description);

        public Time.Mock.ITime MockTime { get; }

        public CurdsApplication(ICurdsOptions mockOptions)
            : base(mockOptions)
        {
            MockTime = Time as Time.Mock.ITime;
        }
    }
}
