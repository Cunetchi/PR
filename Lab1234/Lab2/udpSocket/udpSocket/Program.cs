using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

// Declare a variable to store the port number
int port;

// Prompt the user to enter the port number and read it from the console
Console.WriteLine("Enter Port:");
port = Int32.Parse(Console.ReadLine());

// Create an instance of UdpChat with the specified port number
UdpChat chat = new UdpChat(port);

// Start a thread to receive messages
chat.StartReceiveLoop();

// Prompt the user to input messages in the format <IP>:<PORT>:<TEXT>
Console.WriteLine("Input format: <IP>:<PORT>:<TEXT>");

// Loop indefinitely to read user input and send messages
while (true)
{
    // Read a line of input from the console or use an empty string if no input is provided
    var input = Console.ReadLine() ?? "";

    // Split the input into an array using ':' as the separator
    var splitted = input.Split(':');

    // Extract the destination IP address, port number, and message text from the input
    var toIP = splitted[0];
    var toPort = Int32.Parse(splitted[1]);
    var text = splitted[2];

    // Send the message to the specified IP address and port number
    chat.SendTo(toIP, toPort, text);
}

// Definition of the UdpChat class
public class UdpChat
{
    private int _port;
    private Socket _socket;

    // Constructor that initializes the port number and creates a UDP socket
    public UdpChat(int port)
    {
        _port = port;
        var hostIP = new IPEndPoint(IPAddress.Any, port);
        _socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        _socket.Bind(hostIP);
    }

    // Method to start a thread for receiving messages
    public void StartReceiveLoop()
    {
        Thread thread = new Thread(new ThreadStart(receive));
        thread.Start();
    }

    // Method to send a message to a specific IP address and port number
    public void SendTo(string ip, int port, string text)
    {
        IPEndPoint remoteEndpoint = new IPEndPoint(IPAddress.Parse(ip), port);
        byte[] bytes = Encoding.UTF8.GetBytes(text);
        _socket.SendTo(bytes, remoteEndpoint);
    }

    // Method that runs in a loop to receive messages
    private void receive()
    {
        while (true)
        {
            byte[] buffer = new byte[1024];
            EndPoint remoteClient = new IPEndPoint(0, 0);
            _socket.ReceiveFrom(buffer, ref remoteClient);
            string text = Encoding.UTF8.GetString(buffer);
            Console.WriteLine($"Received from {remoteClient}: {text}");
        }
    }
}
