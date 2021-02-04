using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace ReverseHell{
    class InterfaceExtractor{

        public static List<LinuxInterface> ExtractLinuxInterfaces(string terminalOutput){
            List<LinuxInterface> interfaces = new List<LinuxInterface>();
            var networkDevices = Regex.Split(GetInterfaceInfo(), "\n[0-9]:\x20");

            if(networkDevices.Length > 0)
                networkDevices[0] = networkDevices[0].Remove(0,3);

            foreach (var item in networkDevices)
            {
                interfaces.Add(new LinuxInterface(item));
            }

            return interfaces;
        }


     public static string GetInterfaceInfo(){
            ProcessStartInfo startInfo = new ProcessStartInfo() { FileName = "/usr/bin/ip", Arguments = "a", }; 
            Process proc = new Process() { StartInfo = startInfo, };
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.Start();
            string output = proc.StandardOutput.ReadToEnd();
            proc.WaitForExit();

            return output;
     }   
    }
}