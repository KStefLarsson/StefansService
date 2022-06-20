using Dhcp;
using System.Linq;

namespace StefansService
{
    public class FindAllMacAddressesFromDhcp
    {
        public static string GetAllMacAddresses()
        {
            // Find DHCP-server
            var getDhcp = FindDhcpServer.DhcpServerAddresses();
            
            // Connect to DHCP Server
            var dhcpServer = DhcpServer.Connect(getDhcp);

            // Get a scope
            var scope = dhcpServer.Scopes.FirstOrDefault();

            // Get active client leases
            var activeClients = scope.Clients
            .Where(c => c.AddressState == DhcpServerClientAddressStates.Active);

            string str = string.Empty;
            // Show all clients i scope
            foreach (var client in scope.Clients)
            {
                Service1.WriteToFile($"{client.HardwareAddress} -- {client.IpAddress} -- {client.Name} -- {client.LeaseExpires}");
            }
                               
            return str;
        }
    }
}
