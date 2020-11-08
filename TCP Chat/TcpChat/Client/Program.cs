using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Diagnostics;

namespace Client
{
    class Program
    {
        
        static void Main(string[] args)
        {
            
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write("  IP -> ");
            string IP = Console.ReadLine();
            Console.Write("  Port -> ");
            string port = Console.ReadLine();

            int prt = Convert.ToInt32(port);

            Console.Write("  Nickname -> ");
            string nick = Console.ReadLine();

            
            Console.Clear();
           
            TcpClient client = new TcpClient();
            client.Connect(IP, prt);
            logo();
            while (true)
            {
                getmessage(client, nick);
                Thread.Sleep(69);
            }
        }
        public static void logo()
        {
            string logo = @"
    ███╗   ██╗██╗ ██████╗  ██████╗ ███████╗██████╗ ███████╗██╗      █████╗ ██╗   ██╗███████╗██████╗ 
    ████╗  ██║██║██╔════╝ ██╔════╝ ██╔════╝██╔══██╗██╔════╝██║     ██╔══██╗╚██╗ ██╔╝██╔════╝██╔══██╗
    ██╔██╗ ██║██║██║  ███╗██║  ███╗█████╗  ██████╔╝███████╗██║     ███████║ ╚████╔╝ █████╗  ██████╔╝
    ██║╚██╗██║██║██║   ██║██║   ██║██╔══╝  ██╔══██╗╚════██║██║     ██╔══██║  ╚██╔╝  ██╔══╝  ██╔══██╗
    ██║ ╚████║██║╚██████╔╝╚██████╔╝███████╗██║  ██║███████║███████╗██║  ██║   ██║   ███████╗██║  ██║
    ╚═╝  ╚═══╝╚═╝ ╚═════╝  ╚═════╝ ╚══════╝╚═╝  ╚═╝╚══════╝╚══════╝╚═╝  ╚═╝   ╚═╝   ╚══════╝╚═╝  ╚═╝
                                                                                                    

";
            Console.WriteLine(logo);
        }
        public static void getmessage(TcpClient client, string nick)
        {

          
            Console.Write($"  -> ");
            string message = Console.ReadLine();



            Write(client.GetStream(), Encoding.UTF8.GetBytes($" {nick} -> " + message));

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
        private static void Write(NetworkStream stream, byte[] data)
        {
            stream.Write(data, 0, data.Length);
        }   




    }
}
