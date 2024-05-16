using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        int port;

        Console.WriteLine("Enter Port:");
        port = Int32.Parse(Console.ReadLine());

        UdpChat chat = new UdpChat(port);

        chat.StartReceiveLoop();

        while (true)
        {
            Console.WriteLine("Menu:");
            Console.WriteLine("1. Broadcast");
            Console.WriteLine("2. Private message");
            Console.WriteLine("3. Exit");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.WriteLine("Enter message to broadcast:");
                    var broadcastText = Console.ReadLine();
                    chat.Broadcast(broadcastText);
                    break;
                case "2":
                    Console.WriteLine("Enter IP:");
                    var toIP = Console.ReadLine();
                    Console.WriteLine("Enter Port:");
                    var toPort = Int32.Parse(Console.ReadLine());
                    Console.WriteLine("Enter message:");
                    var privateText = Console.ReadLine();
                    chat.SendTo(toIP, toPort, privateText);
                    break;
                case "3":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please select 1, 2, or 3.");
                    break;
            }
        }
    }
}

public class UdpChat
{
    private int _port;
    private Socket _socket;

    public UdpChat(int port)
    {
        _port = port;
        var hostIP = new IPEndPoint(IPAddress.Any, port);
        _socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        _socket.Bind(hostIP);
    }

    public void StartReceiveLoop()
    {
        Thread thread = new Thread(new ThreadStart(receive));
        thread.Start();
    }

    public void SendTo(string ip, int port, string text)
    {
        IPEndPoint remoteEndpoint = new IPEndPoint(IPAddress.Parse(ip), port);
        byte[] bytes = Encoding.UTF8.GetBytes(text);
        _socket.SendTo(bytes, remoteEndpoint);
    }

    public void Broadcast(string text)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(text);
        IPEndPoint broadcastEndpoint = new IPEndPoint(IPAddress.Broadcast, _port);
        _socket.SendTo(bytes, broadcastEndpoint);
    }

    private void receive()
    {
        while (true)
        {
            byte[] buffer = new byte[1024];
            EndPoint remoteClient = new IPEndPoint(IPAddress.Any, 0);
            _socket.ReceiveFrom(buffer, ref remoteClient);
            string text = Encoding.UTF8.GetString(buffer).Trim('\0');

            // Check if the message is intended for this instance
            IPEndPoint remoteEndpoint = (IPEndPoint)remoteClient;
            if (remoteEndpoint.Address.Equals(IPAddress.Parse("127.0.0.1")) && remoteEndpoint.Port == _port)
            {
                Console.WriteLine($"Received from {remoteClient}: {text}");
            }
        }
    }

}
