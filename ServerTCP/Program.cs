using System.Net;
using System.Net.Sockets;
using System.Text;

IPEndPoint ipPoint = new IPEndPoint(IPAddress.Any, 8888);
using Socket tcpListener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

try
{
    tcpListener.Bind(ipPoint);
    tcpListener.Listen();    // запускаем сервер
    Console.WriteLine("Сервер запущен. Ожидание подключений... ");

    while (true)
    {
        // получаем входящее подключение
        using var tcpClient = await tcpListener.AcceptAsync();

        
        byte[] dataClient = new byte[1024];
        int bytes = await tcpClient.ReceiveAsync(dataClient);
        string resultClinet = Encoding.UTF8.GetString(dataClient, 0, bytes);

        // определяем данные для отправки - текущее время
        byte[] data = Encoding.UTF8.GetBytes($"Привет дорого пушистик! {resultClinet}");
        // отправляем данные
        await tcpClient.SendAsync(data);
        Console.WriteLine($"Клиенту {tcpClient.RemoteEndPoint}, {resultClinet} отправлены данные");
    }
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}