using System;
using System.Diagnostics;

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
            if (args == null || args.Length == 0)
                throw new ArgumentNullException(nameof(args));

            Arguments = args;
        }

        public void Previous()
        {
            if (AtStart)
                throw new InvalidOperationException("Already at the first argument");
            CurrentIndex--;
            Debug.WriteLine($"NewIndex: {CurrentIndex}");
        }

        public void Next()
        {
            if (AtEnd)
                throw new InvalidOperationException("Already at the last argument");
            CurrentIndex++;
            Debug.WriteLine($"NewIndex: {CurrentIndex}");
        }

        public string Parse() => Arguments[CurrentIndex];
    }
}
