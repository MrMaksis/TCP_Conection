using System.Net.Sockets;
using System.Text;

using var tcpClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
try
{
    await tcpClient.ConnectAsync("127.0.0.1", 8888);

    Console.WriteLine("Your name: ");
    byte[] bufferUeser = Encoding.UTF8.GetBytes(Console.ReadLine());
    await tcpClient.SendAsync(bufferUeser);

    // буфер для считывания данных
    byte[] data = new byte[512];

    // получаем данные из потока
    int bytes = await tcpClient.ReceiveAsync(data);
    // получаем отправленное время
    string resultServer = Encoding.UTF8.GetString(data, 0, bytes);
    Console.WriteLine($"[{DateTime.Now}] Ответ от сервера: {resultServer}");
    Console.ReadLine();
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}