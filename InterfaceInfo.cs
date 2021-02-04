using System.Diagnostics;

namespace ReverseHell{
    class InterfaceInfo {

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