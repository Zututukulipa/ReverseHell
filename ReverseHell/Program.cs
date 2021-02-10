using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using CommandLine;
using ShellScrape;
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
            var interfaceNumber = opts.Interface - 1;
            var designatedPort = opts.Port;
            var language = opts.Language.ToLower();
            string terminalCommand = string.Empty;

            NetworkInterface[] networkDevices = NetworkInterface.GetAllNetworkInterfaces();

            if (interfaceNumber <= 0 || interfaceNumber > networkDevices.Length)
            {
                System.Console.WriteLine("Invalid interface index.");
                return;
            }

            var addresses = networkDevices[interfaceNumber].GetIPProperties().UnicastAddresses;
            IPAddress ipv4Address = InterfaceExtractor.ExtractIpV4Address(addresses);
            try
            {
                terminalCommand = ReverseShellProvider.GetReverseShell(language, ipv4Address, designatedPort);
            }
            catch (ArgumentException ex)
            {
                System.Console.WriteLine(ex.Message);
                return;
            }

            System.Console.WriteLine(terminalCommand);
            if (opts.ToClipboard)
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
