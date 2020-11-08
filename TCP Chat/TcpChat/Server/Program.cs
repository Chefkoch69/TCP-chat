using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

namespace Server
{
    class Program
    {
        private static TcpListener _Listener;
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write("  Port -> ");

            string port = Console.ReadLine();

            int prt = Convert.ToInt32(port);


            

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            string logo = @"
    ███╗   ██╗██╗ ██████╗  ██████╗ ███████╗██████╗ ███████╗██╗      █████╗ ██╗   ██╗███████╗██████╗ 
    ████╗  ██║██║██╔════╝ ██╔════╝ ██╔════╝██╔══██╗██╔════╝██║     ██╔══██╗╚██╗ ██╔╝██╔════╝██╔══██╗
    ██╔██╗ ██║██║██║  ███╗██║  ███╗█████╗  ██████╔╝███████╗██║     ███████║ ╚████╔╝ █████╗  ██████╔╝
    ██║╚██╗██║██║██║   ██║██║   ██║██╔══╝  ██╔══██╗╚════██║██║     ██╔══██║  ╚██╔╝  ██╔══╝  ██╔══██╗
    ██║ ╚████║██║╚██████╔╝╚██████╔╝███████╗██║  ██║███████║███████╗██║  ██║   ██║   ███████╗██║  ██║
    ╚═╝  ╚═══╝╚═╝ ╚═════╝  ╚═════╝ ╚══════╝╚═╝  ╚═╝╚══════╝╚══════╝╚═╝  ╚═╝   ╚═╝   ╚══════╝╚═╝  ╚═╝
 ";
            Console.WriteLine(logo);
            _Listener = new TcpListener(IPAddress.Any, prt);
            _Listener.Start();
            while (true)
            {
                TcpClient client = _Listener.AcceptTcpClient();

                NetworkStream stream = client.GetStream();
               
                Task.Run(() =>
                {
                    while (true)
                    {
                        if (stream.DataAvailable)
                        {

                            byte[] recievedbytes = ReadToEnd(stream);
                            Console.WriteLine("    " + Encoding.UTF8.GetString(recievedbytes));

                        }
                        else Thread.Sleep(1);
                    }
                });

               
            }
            
        }
        private static void Write(NetworkStream stream, byte[] data)
        {
            stream.Write(data, 0, data.Length);
        }
        private static byte[] ReadToEnd(NetworkStream stream)
        {
            List<byte> recievedbytes = new List<byte>();
            while (stream.DataAvailable)
            {

                byte[] buffer = new byte[1024];
                stream.Read(buffer, 0, buffer.Length);

                recievedbytes.AddRange(buffer);
                
            }

            recievedbytes.RemoveAll(b => b == 0);

            return recievedbytes.ToArray();
        }


    }
}
