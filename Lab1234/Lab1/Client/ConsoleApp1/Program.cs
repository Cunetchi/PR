
using System.Net;
using System.Net.Sockets;
using System.Text;

IPAddress serverIp = IPAddress.Parse("127.0.0.1");
int serverPort = 9001;

Thread.Sleep(3000);
Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
client.Connect(new IPEndPoint(serverIp, serverPort));
Console.WriteLine("Connected to server");
while (true)
{
    
    
    Console.WriteLine("Enter some text to send to server");
    string text = Console.ReadLine() ?? "";

    byte[] bytesData = Encoding.UTF8.GetBytes(text);

    client.Send(bytesData);

    string receivedText = "";

    do
    {
        byte[] receivedBuffer = new byte[1024];
        int bytesReceived = client.Receive(receivedBuffer);

        receivedText += Encoding.UTF8.GetString(receivedBuffer, 0, bytesReceived);
    } while (client.Available > 0);

    Console.WriteLine($"Server: {receivedText}\n");
    
}
