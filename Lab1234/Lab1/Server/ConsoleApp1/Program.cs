using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
        serverSocket.Bind(new IPEndPoint(ipAddress, 9001));

        serverSocket.Listen(5);

        int connectionCount = 0;

        while (true)
        {
            Console.WriteLine("Waiting for a connection...");

            Socket connection = await serverSocket.AcceptAsync();
            connectionCount++;

            Console.WriteLine($"Connection {connectionCount} accepted");
            Console.WriteLine(connection.RemoteEndPoint);

            // Start a new thread to handle the connection asynchronously
            _ = Task.Run(() => HandleConnection(connection, connectionCount));
        }
    }

    static void HandleConnection(Socket connection, int connectionCount)
    {
        try
        {
            string text = "";

            do
            {
                byte[] buffer = new byte[1024];
                int bytesReceived = connection.Receive(buffer);

                text += Encoding.UTF8.GetString(buffer, 0, bytesReceived);
            } while (connection.Available > 0);

            Console.WriteLine($"Received from Connection {connectionCount}: {text}");

            byte[] bytesData = Encoding.UTF8.GetBytes("Server received your message");
            connection.Send(bytesData);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error handling Connection {connectionCount}: {ex.Message}");
        }
        finally
        {
            connection.Close();
        }
    }
}