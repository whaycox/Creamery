using Queso.Application;
using Queso.Infrastructure;
using Curds.CLI;
using Curds.CLI.Writer;

namespace Queso.CLI
{
    class Program
    {
        private static QuesoCommandLine CommandLine = new QuesoCommandLine(BuildApplication, BuildWriter);
        private static QuesoApplication BuildApplication => new QuesoApplication(BuildOptions);
        private static QuesoOptions BuildOptions => new DefaultOptions();
        private static IConsoleWriter BuildWriter => new ConsoleWriter();

        static void Main(string[] args) => CommandLine.Execute(args);
    }
}
