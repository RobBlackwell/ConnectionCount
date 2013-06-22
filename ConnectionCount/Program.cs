#region Copyright (c) 2013 Two10degrees Ltd
//
// (C) Copyright 2013 Two10degrees Ltd
//      All rights reserved.
//
// This software is provided "as is" without warranty of any kind,
// express or implied, including but not limited to warranties as to
// quality and fitness for a particular purpose. Two10degrees Ltd
// does not support the Software, nor does it warrant that the Software
// will meet your requirements or that the operation of the Software will
// be uninterrupted or error free or that any defects will be
// corrected. Nothing in this statement is intended to limit or exclude
// any liability for personal injury or death caused by the negligence of
// Two10degrees Ltd, its employees, contractors or agents.
//
#endregion

using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading;

namespace MyNetstat
{
    class Program
    {
        static int Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("ConnectionCount <hostname>");
                return 1;
            }

            IPGlobalProperties ipProperties = IPGlobalProperties.GetIPGlobalProperties();

            IPEndPoint[] endPoints = ipProperties.GetActiveTcpListeners();

            // Resolve the hostname's DNS address
            IPAddress[] addresslist = Dns.GetHostAddresses(args[0]);
            string arg = addresslist[0].ToString();

            while (true)
            {
                int i = 0; // Count the number of established connections

                TcpConnectionInformation[] tcpConnections = ipProperties.GetActiveTcpConnections();

                foreach (TcpConnectionInformation info in tcpConnections)
                {
                    if (string.Equals( info.RemoteEndPoint.Address.ToString(), arg))
                    {
                        if (info.State == TcpState.Established)
                            i++;
                    }
                }

                Console.WriteLine("Connection count to {0}, {1}", arg, i);
                Thread.Sleep(1000);
            }
        }
    }
}