using System.Net;
using System.Net.Sockets;
using System.Text;

const int port = 8005;
const string address = "127.0.0.1";

var hostName = Dns.GetHostName();
Console.WriteLine($"Comp name = {hostName}");

var ips = Dns.GetHostAddresses(hostName);
foreach(var ip in ips) Console.WriteLine(ip);

var ipEndPoint = new IPEndPoint(IPAddress.Parse(address), port);

var listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

try
{
	listenSocket.Bind(ipEndPoint);
	listenSocket.Listen(10);

	Console.WriteLine("Сервер запущен. Ожидание подключений...");

	while (true)
	{
		var handler = listenSocket.Accept();  // блокирует, пока не появится клиент

		var builder = new StringBuilder();

		var totalBytes = 0;
		var data = new byte[255];

		do
		{
			var bytes = handler.Receive(data);
			builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
			totalBytes += bytes;
		} while (handler.Available > 0);

		Console.WriteLine($"[{DateTime.Now.ToShortTimeString()}] {builder} ({totalBytes} bytes)");

		handler.Send(Encoding.Unicode.GetBytes(builder.ToString()));  // !

		// рекомендованный способ
		// ref: https://learn.microsoft.com/en-us/dotnet/api/system.net.sockets.socket.close?view=net-8.0#system-net-sockets-socket-close:~:text=call%20Shutdown%20before%20calling%20the%20Close
		handler.Shutdown(SocketShutdown.Both);  // отключает возможность получать/отправлять
		handler.Close();  // закрывает подключение, освобождает память
	}
}
catch (Exception e)
{
	Console.WriteLine(e.Message);
}
