using System;
using System.Collections.Generic;
using CommandLine;
using TextCopy;

namespace ReverseHell
{
    class Program
    {

        class Options
        {
            [Option('l', "language", Required = true, HelpText = "Language to generate reverse shell string.")]
            public string Language { get; set; }

            [Option('c', "copy",
              Default = true,
              HelpText = "Copy generated string to clipboard.")]
            public bool ToClipboard { get; set; }

            [Option('i', "interface",
              Default = 1,
              HelpText = "Interface to gather information about.")]
            public int Interface { get; set; }

            [Option('p', "port",
              Default = "1234",
              HelpText = "Listening port (default: 1234)")]
            public string Port { get; set; }
        }

        static void Main(string[] args)
        {
            CommandLine.Parser.Default.ParseArguments<Options>(args)
                .WithParsed(RunOptions)
                .WithNotParsed(HandleParseError);
            

        }
        static void RunOptions(Options opts)
        {
            var networkDevices = InterfaceExtractor.ExtractLinuxInterfaces(InterfaceInfo.GetInterfaceInfo());
            var interfaceNumber = opts.Interface - 1;
            var designatedPort = opts.Port;
            string terminalCommand = string.Empty;
            switch (opts.Language.ToLower())
            {
                case "bash":
                    terminalCommand = ReverseShells.GetBashShell(networkDevices[interfaceNumber].IPv4, designatedPort);
                    break;
                    case "perl":
                    terminalCommand = ReverseShells.GetPerlShell(networkDevices[interfaceNumber].IPv4, designatedPort);
                    break;
                    case "python":
                    terminalCommand = ReverseShells.GetPythonShell(networkDevices[interfaceNumber].IPv4, designatedPort);
                    break;
                    case "php":
                    terminalCommand = ReverseShells.GetPHPShell(networkDevices[interfaceNumber].IPv4, designatedPort);
                    break;
                    case "ruby":
                    terminalCommand = ReverseShells.GetRubyShell(networkDevices[interfaceNumber].IPv4, designatedPort);
                    break;
                    case "nc":
                    terminalCommand = ReverseShells.GetNetcatShell(networkDevices[interfaceNumber].IPv4, designatedPort);
                    break;
                    case "java":
                    terminalCommand = ReverseShells.GetJavaShell(networkDevices[interfaceNumber].IPv4, designatedPort);
                    break;
                default:
                    throw new Exception("unsupported language");
            }
            System.Console.WriteLine(terminalCommand);
            if(opts.ToClipboard)
                ClipboardService.SetText(terminalCommand);
        }
        static void HandleParseError(IEnumerable<Error> errs)
        {
            foreach (var item in errs)
            {
                System.Console.WriteLine(item);
            }
        }

    }
}
