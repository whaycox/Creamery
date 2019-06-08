namespace Curds.Parsing.CSV.Domain
{
    public class Cell
    {
        public string Value { get; }

        public Cell(string value = null)
        {
            Value = value ?? string.Empty;
        }

        public override string ToString() => Value;
    }
}
