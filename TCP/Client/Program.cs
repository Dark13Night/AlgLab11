using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace MyTcpClient
{
    class Program
    {
        static void Main(string[] args)
        {
            IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
            int port = 5555;

            System.Net.Sockets.TcpClient client = new System.Net.Sockets.TcpClient();

            try
            {
                client.Connect(ipAddress, port);
                Console.WriteLine("Connected to server.");

                Console.WriteLine("Enter ticker: ");
                string ticker = Console.ReadLine();

                using (StreamReader reader = new StreamReader(client.GetStream()))
                using (StreamWriter writer = new StreamWriter(client.GetStream()))
                {
                    // Send the ticker to the server
                    writer.WriteLine(ticker);
                    writer.Flush();

                    // Receive the last price from the server
                    decimal lastPrice = decimal.Parse(reader.ReadLine());
                    Console.WriteLine("Last price: " + lastPrice);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                client.Close();
            }
        }
    }
}
