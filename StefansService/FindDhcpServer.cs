using System;
using System.Net;
using System.Net.NetworkInformation;

namespace StefansService
{
    internal class FindDhcpServer
    {
        public static string DhcpServerAddresses()
        {
            string myDhcpServer = "";
            NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface adapter in adapters)
            {
                IPInterfaceProperties adapterProperties = adapter.GetIPProperties();
                IPAddressCollection addresses = adapterProperties.DhcpServerAddresses;
                if (addresses.Count > 0)
                {
                    foreach (IPAddress address in addresses)
                    {
                        myDhcpServer = address.ToString();
                        
                    }
                }
            }
            return myDhcpServer;
        }
    }
}
