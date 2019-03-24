using System;

namespace Curds.CLI
{
    public class ArgumentCrawler
    {
        private int CurrentIndex = 0;

        private string[] Arguments { get; }

        public bool AtStart => CurrentIndex == 0;
        public bool AtEnd => CurrentIndex == Arguments.Length - 1;

        public ArgumentCrawler(string[] args)
        {
            Arguments = args;
        }

        public void Previous()
        {
            if (AtStart)
                throw new InvalidOperationException("Already at the first argument");
            CurrentIndex--;
        }

        public void Next()
        {
            if (AtEnd)
                throw new InvalidOperationException("Already at the last argument");
            CurrentIndex++;
        }

        public string Parse() => Arguments[CurrentIndex];
    }
}
