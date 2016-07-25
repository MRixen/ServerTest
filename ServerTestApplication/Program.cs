using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        // TEST QUADRANT 1
        //static float[] yData = { 0f, -0.087155743f, -0.173648178f, -0.258819045f, -0.342020143f, -0.422618262f, -0.5f, -0.573576436f, -0.64278761f, -0.707106781f, -0.766044443f, -0.819152044f, -0.866025404f, -0.906307787f, -0.939692621f, -0.965925826f, -0.984807753f, -0.996194698f, -1f };
        //static float[] zData = { -1f, -0.996194698f, -0.984807753f, -0.965925826f, -0.939692621f, -0.906307787f, -0.866025404f, -0.819152044f, -0.766044443f, -0.707106781f, -0.64278761f, -0.573576436f, -0.5f, -0.422618262f, -0.342020143f, -0.258819045f, -0.173648178f, -0.087155743f, -0 };


        // TEST QUADRANT 2
        static float[] yData = { 0f, -0.087155743f, -0.173648178f, -0.258819045f, -0.342020143f, -0.422618262f, -0.5f, -0.573576436f, -0.64278761f, -0.707106781f, -0.766044443f, -0.819152044f, -0.866025404f, -0.906307787f, -0.939692621f, -0.965925826f, -0.984807753f, -0.996194698f, -1f };
        static float[] zData = { 1f, 0.996194698f, 0.984807753f, 0.965925826f, 0.939692621f, 0.906307787f, 0.866025404f, 0.819152044f, 0.766044443f, 0.707106781f, 0.64278761f, 0.573576436f, 0.5f, 0.422618262f, 0.342020143f, 0.258819045f, 0.173648178f, 0.087155743f, 0 };


        static int timestamp = 0;
        private static int timer = 50;
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
            Random rng = new Random();

            int i = 0;
            int j = 0;
            bool countBackward = false;

            while ((true))
            {
                try
                {
                    //for (int i = 0; i <= 3; i++)
                    //{
                    //    requestCount = requestCount + 1;

                    Debug.WriteLine(i);
                    Debug.WriteLine(j);
                    //byte[] bytesFrom = new byte[10025];
                    //networkStream.Read(bytesFrom, 0, (int)clientSocket.ReceiveBufferSize);
                    //string dataFromClient = System.Text.Encoding.ASCII.GetString(bytesFrom);
                    //dataFromClient = dataFromClient.Substring(0, dataFromClient.IndexOf("$"));
                    //Console.WriteLine(" >> Data from client - " + dataFromClient);
                    //string serverResponse = "Last Message from client" + dataFromClient;
                    string sensorID_0 = ":" + 0 + ":x" + 0 + "::y" + yData[i] + "::z" + zData[i] + "::" + timestamp + ";";
                    //string sensorID_0 = ":" + 0 + ":x" + rng.Next(10) + "::y" + rng.Next(60) + "::z" + rng.Next(60) + "::" + timestamp + ";";
                    sender(sensorID_0);
                    Thread.Sleep(timer);
                    //string sensorID_1 = ":" + 1 + ":x" + rng.Next(10) + "::y" + rng.Next(60) + "::z" + rng.Next(60) + "::" + timestamp + ";";
                    string sensorID_1 = ":" + 1 + ":x" + 0 + "::y" + yData[i] + "::z" + zData[i] + "::" + timestamp + ";";
                    sender(sensorID_1);
                    //string sensorID_2 = ":" + 2 + ":x" + rng.Next(10) + "::y" + rng.Next(60) + "::z" + rng.Next(60) + "::" + timestamp + ";";
                    //sender(sensorID_2);
                    //string sensorID_3 = ":" + 3 + ":x" + rng.Next(10) + "::y" + rng.Next(60) + "::z" + rng.Next(60) + "::" + timestamp + ";";
                    //sender(sensorID_3);
                    Thread.Sleep(timer);
                    timestamp += timer;

                    if (i == yData.Length - 1) countBackward = true;
                    else if (i == 0) countBackward = false;

                    if (!countBackward & (j >= ( 30)))
                    {
                        i++;
                        j = 0;
                    }
                    else j++;

                    if (countBackward & (j >= ( 30)))
                    {
                        i--;
                        j = 0;
                    }
                    else j++;

                    //Debug.Write(rng.Next(60) + "\n");
                    //Debug.WriteLine(timestamp);
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
