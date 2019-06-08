using System;
using System.Diagnostics;

namespace Curds.CLI.Implementation
{
    public class ArgumentCrawler : Abstraction.IArgumentCrawler
    {
        private int ArgumentsConsumed = 0;
        private string[] Arguments { get; }

        public bool FullyConsumed => ArgumentsConsumed == Arguments.Length;

        public ArgumentCrawler(string[] args)
        {
            if (args == null || args.Length == 0)
                throw new ArgumentNullException(nameof(args));

            Arguments = args;
        }

        public string Consume()
        {
            if (FullyConsumed)
                throw new InvalidOperationException("Cannot consumed when already fully consumed");
            return Arguments[ArgumentsConsumed++];
        }
        public void StepBackwards()
        {
            if (ArgumentsConsumed == 0)
                throw new InvalidOperationException("Cannot step before the start");
            ArgumentsConsumed--;
        }
    }
}
