namespace Curds.Persistence.Model.Domain
{
    public class TestEnumClass
    { 
        public TestEnum TestEnum { get; set; }
        
        public TestShortEnum TestShortEnum { get; set; }
    }

    public enum TestEnum
    { }

    public enum TestShortEnum : short
    { }
}
