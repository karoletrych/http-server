using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace HttpServer
{
 
public class GetSocket
{
    public static void Main(string[] args)
    {
        string host;
        int port = 80;

        if (args.Length == 0)
            // If no server name is passed as argument to this program,
            // use the current host name as the default.
            host = Dns.GetHostName();
        else
            host = args[0];

        Socket s = SetupHost(host, port);
        byte[] buffer = new byte[1024];

        List<Socket> connections = new List<Socket>();

        while(true)
        {
            Thread.Sleep(1000);
            Socket child = s.Accept();
            connections.Add(child);

            foreach(var conn in connections)
            {
                conn.Receive(buffer);

                var msg = Encoding.ASCII.GetString(buffer);
                System.Console.WriteLine(msg);

                System.Console.WriteLine($"{buffer.Length} bytes received.");
            }
        }
    }

        private static Socket SetupHost(string host, int port)
        {
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            var anyIp = IPAddress.Any;



            var endpoint = new IPEndPoint(anyIp, port);

            Console.WriteLine($"Listening on {endpoint}");

            const int maxConnections = 1024;
            Console.WriteLine($"Max connections {maxConnections}");

            socket.Bind(endpoint);
            socket.Listen(maxConnections);
            return socket;
        }
    }

}
