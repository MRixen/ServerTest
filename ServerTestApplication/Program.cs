using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

// -------------------
// Server application
// -------------------

namespace ConsoleApplication2
{
    class Program
    {
        static TcpClient clientSocket;

        static int timestamp = 0;
        private static int timer = 10;
        static void Main(string[] args)
        {
            TcpListener serverSocket = new TcpListener(4555);
            int requestCount = 0;
            clientSocket = default(TcpClient);
            serverSocket.Start();
            Console.WriteLine(" >> Server Started");
            clientSocket = serverSocket.AcceptTcpClient();
            Console.WriteLine(" >> Accept connection from client");
            requestCount = 0;

            while ((true))
            {
                try
                {
                    //for (int i = 0; i <= 3; i++)
                    //{
                    //    requestCount = requestCount + 1;
                        
                        //byte[] bytesFrom = new byte[10025];
                        //networkStream.Read(bytesFrom, 0, (int)clientSocket.ReceiveBufferSize);
                        //string dataFromClient = System.Text.Encoding.ASCII.GetString(bytesFrom);
                        //dataFromClient = dataFromClient.Substring(0, dataFromClient.IndexOf("$"));
                        //Console.WriteLine(" >> Data from client - " + dataFromClient);
                        //string serverResponse = "Last Message from client" + dataFromClient;
                        string sensorID_0 = ":" + 0 + ":x10::y10::z10::" + timestamp + ";";
                        sender(sensorID_0);
                        string sensorID_1 = ":" + 1 + ":x20::y20::z20::" + timestamp + ";";
                        sender(sensorID_1);
                        string sensorID_2 = ":" + 2 + ":x30::y30::z30::" + timestamp + ";";
                        sender(sensorID_2);
                        string sensorID_3 = ":" + 3 + ":x40::y40::z40::" + timestamp + ";";
                        sender(sensorID_3);
                        Thread.Sleep(timer);
                        timestamp += timer;
                 //   }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }

            clientSocket.Close();
            serverSocket.Stop();
            Console.WriteLine(" >> exit");
            Console.ReadLine();
        }

        static void sender(String message)
        {
            NetworkStream networkStream = clientSocket.GetStream();
            Byte[] sendBytes = Encoding.ASCII.GetBytes(message);
            networkStream.Write(sendBytes, 0, sendBytes.Length);
            networkStream.Flush();
            Console.WriteLine(" >> " + message);

        }
    }
}
