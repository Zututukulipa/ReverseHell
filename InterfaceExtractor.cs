using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;

namespace ReverseHell
{
    class InterfaceExtractor
    {

        public static IPAddress ExtractIpV4Address(UnicastIPAddressInformationCollection addresses)
        {
            foreach (var address in addresses)
            {
                if (address.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    return address.Address;

            }

            throw new KeyNotFoundException("Selected interface does not have assigned IPv4 Address");
        }
    }
}