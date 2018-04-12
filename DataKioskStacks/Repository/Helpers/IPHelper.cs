﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace DataKioskStacks.Repository.Helpers
{
   public class IPHelper
    {

        public static string GetIpAddress(NetworkInterface ni)
        {
            try
            {
                if (ni.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 || ni.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                {
                    Console.WriteLine(ni.Name);
                    foreach (UnicastIPAddressInformation ip in ni.GetIPProperties().UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        {
                            return ip.Address.ToString();
                        }
                    }
                }
                return "";
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        public static NetworkInterface GetMainNetworkInterface()
        {
            List<NetworkInterface> candidates = new List<NetworkInterface>();

            if (NetworkInterface.GetIsNetworkAvailable())
            {
                NetworkInterface[] networkInterfaces =
                    NetworkInterface.GetAllNetworkInterfaces();

                candidates.AddRange(networkInterfaces.Where(ni => ni.OperationalStatus == OperationalStatus.Up));
            }

            if (candidates.Count == 1)
            {
                return candidates[0];
            }

            // Accoring to our tech, the main NetworkInterface should have a Gateway 
            // and it should be the ony one with a gateway.
            if (candidates.Count > 1)
            {
                for (int n = candidates.Count - 1; n >= 0; n--)
                {
                    if (candidates[n].GetIPProperties().GatewayAddresses.Count == 0)
                    {
                        candidates.RemoveAt(n);
                    }
                }

                if (candidates.Count == 1)
                {
                    return candidates[0];
                }
            }

            // Fallback to try by getting my ipAdress from the dns
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress myMainIpAdress = host.AddressList.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);

            if (myMainIpAdress != null)
            {
                NetworkInterface[] networkInterfaces =
                    NetworkInterface.GetAllNetworkInterfaces();

                return (from ni in networkInterfaces where ni.OperationalStatus == OperationalStatus.Up let props = ni.GetIPProperties() from ai in props.UnicastAddresses where ai.Address.Equals(myMainIpAdress) select ni).FirstOrDefault();
            }

            return null;
        }
    }
}