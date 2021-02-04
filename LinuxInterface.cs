using System.Text.RegularExpressions;

namespace ReverseHell
{
    class LinuxInterface
    {
        public string Name { get; }
        public string StateDescriptors { get; }
        public string MaximumTransferUnit { get; }
        public string QdiscType { get; }
        public string State { get; }
        public string Group { get; }
        public string QueueLenght { get; }
        public string Link { get; }
        public string MacAddress { get; }
        public string IPv4Full { get; }
        public string IPv4 { get; }
        public string SubnetV4 { get; }
        public string IPv6Full { get; }
        public string IPv6 { get; }
        public string SubnetV6 { get; }

        public LinuxInterface(string linuxInterfaceOutput)
        {
            Name = Regex.Match(linuxInterfaceOutput, ".+?(?=:)").Value;
            StateDescriptors = Regex.Match(linuxInterfaceOutput, "<.+>").Value;
            MaximumTransferUnit = Regex.Match(linuxInterfaceOutput, "(?<=u\x20)[0-9]+").Value;
            QdiscType = Regex.Match(linuxInterfaceOutput, "(?<=qdisc\x20)[a-z]+").Value;
            State = Regex.Match(linuxInterfaceOutput, "(?<=state\x20)[A-Z]+").Value;
            Group = Regex.Match(linuxInterfaceOutput, "(?<=group\x20)[a-zA-Z]+").Value;
            QueueLenght = Regex.Match(linuxInterfaceOutput, "(?<=qlen\x20)[0-9]+").Value;
            Link = Regex.Match(linuxInterfaceOutput, "(?<=link/)[a-z]+").Value;
            MacAddress = Regex.Match(linuxInterfaceOutput, "(?<=link/.*\x20).+(?= b)").Value;
            IPv4Full = Regex.Match(linuxInterfaceOutput, "(?<=inet\x20).+/[0-9]+").Value;
            if (!string.IsNullOrEmpty(IPv4Full))
            {
                IPv4 = IPv4Full.Split("/")[0];
                SubnetV4 = IPv4Full.Split("/")[1];
            }
            IPv6Full = Regex.Match(linuxInterfaceOutput, "(?<=inet6\x20).+/[0-9]+").Value;
            if (!string.IsNullOrEmpty(IPv6Full))
            {
                IPv6 = IPv6Full.Split("/")[0];
                SubnetV6 = IPv6Full.Split("/")[1];
            }
        }

        public override string ToString()
        {
            return $"{Name}: {StateDescriptors}; MTU:{MaximumTransferUnit}; QDISC:{QdiscType}, GROUP:{Group}, QLEN:{QueueLenght}\nlink/{Link} {MacAddress}\nIPv4: {IPv4}/{SubnetV4}\nIPv6: {IPv6}/{SubnetV6}";
        }
    }
}