namespace Curds.CLI.Mock
{
    public class IArgumentCrawler : Abstraction.IArgumentCrawler
    {
        private Implementation.ArgumentCrawler Real = null;

        public bool FullyConsumed
        {
            get
            {
                if (Real != null)
                    return Real.FullyConsumed;
                else
                    return false;
            }
        }

        public int Consumptions = 0;
        public string Consume()
        {
            Consumptions++;
            if (Real != null)
                return Real.Consume();
            else
                return nameof(Consume);
        }

        public int Steps = 0;
        public void StepBackwards()
        {
            Steps++;
            if (Real != null)
                Real.StepBackwards();
        }

        public void LoadScript(params string[] pieces)
        {
            Real = new Implementation.ArgumentCrawler(pieces);
        }
    }
}
