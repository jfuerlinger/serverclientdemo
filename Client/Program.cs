using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        const string SERVER_IP = "172.17.24.80";
        const int SERVER_PORT = 9999;


        static void Main(string[] args)
        {
            //Console.WriteLine("Bitte IP eingeben: ");
            //string ip = Console.ReadLine();


            //Console.WriteLine("Bitte Port eingeben: ");
            //int port = Convert.ToInt32(Console.ReadLine());

            //---create a TCPClient object at the IP and port no.---
            Console.WriteLine("Connecting ...");
            using (TcpClient client = new TcpClient(SERVER_IP, SERVER_PORT))
            {
                NetworkStream nwStream = client.GetStream();
                Console.WriteLine("Connection established.");

                do
                {
                    Console.Write(">> ");
                    string textToSend = Console.ReadLine();
                    if (textToSend[0] == 'x')
                    {
                        break;
                    }
                    byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes(textToSend);

                    nwStream.Write(bytesToSend, 0, bytesToSend.Length);

                    byte[] bytesToRead = new byte[client.ReceiveBufferSize];
                    int bytesRead = nwStream.Read(bytesToRead, 0, client.ReceiveBufferSize);
                    string receivedText = Encoding.ASCII.GetString(bytesToRead, 0, bytesRead);
                    Console.WriteLine("Received : " + receivedText);
                } while (true);

            }

        }

        private static void PrintServerResponse(string response)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"--> {response}");
            Console.ResetColor();
        }
    }
}
