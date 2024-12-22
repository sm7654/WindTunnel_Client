using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using System.Linq;

namespace WindTunnel_Client
{
    internal class Program
    {
        public static int getPort(IPAddress ip) 
        {
            Socket TempSock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            EndPoint En = new IPEndPoint(ip, 10000);
            byte[] tempBuffer = new byte[1024]; 
            TempSock.SendTo(tempBuffer, En);
            En = new IPEndPoint(IPAddress.Any, 0);
            TempSock.ReceiveFrom(tempBuffer, ref En); // ref - changes in ReceiveFrom() on En will effect it
            TempSock.Close();
            return int.Parse(Encoding.UTF8.GetString(tempBuffer));
        }



        static void Main(string[] args)
        {

            


            Encryption.GenerateKeys();
            
            
            IPAddress iP = IPAddress.Parse("127.0.0.1");

            int ServerPort = 65000;


            // creating udp "connection" to send the video
            /*
            Socket Udp = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            EndPoint EN = new IPEndPoint(IPAddress.Loopback, 3000);
            byte[] gg =new byte[1024];
            Udp.SendTo(Encoding.UTF8.GetBytes(""), EN);
            Udp.ReceiveFrom(gg, ref EN);

            Console.WriteLine(Encoding.UTF8.GetString(gg) + $" {EN.ToString()}");
            */


            try
            {
                Socket ClientSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint ServerPath = new IPEndPoint(iP, ServerPort);
                Console.WriteLine("Trying to connect to server...");
                ClientSock.Connect(ServerPath);

               


                Console.WriteLine("CONNECTION ESTABLISHED\n");

                Console.WriteLine("Focusing now on MicroControllerSide and the server.....");

                Console.Write("for now the format is this:    ");

                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("roomCode + ; + nameOfClient\n");

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("for example: ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("RTf23;shai");

                Console.ResetColor();

                ClientSock.Send(RsaEncryption.GenerateKeys());

                byte[] ServerpublicKey = new byte[1024];
                int bytesRec = ClientSock.Receive(ServerpublicKey);

                RsaEncryption.SetServerPublicKey(Encoding.UTF8.GetString(ServerpublicKey, 0, bytesRec));


                byte[] Message = RsaEncryption.EncryptToServer(Encoding.UTF8.GetBytes(Console.ReadLine()));
                byte[] bytes = Encoding.UTF8.GetBytes(Message.Length.ToString());
                ClientSock.Send(bytes);
                Thread.Sleep(200);
                ClientSock.Send(Message);






            } catch (Exception ex) 
            { }
        }
    }
}
