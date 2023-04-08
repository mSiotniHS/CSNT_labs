using System.Net;
using System.Net.Sockets;
using System.Text;

const int port = 8005;
const string address = "127.0.0.1";

var ipEndPoint = new IPEndPoint(IPAddress.Parse(address), port);
var listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

try
{
	listenSocket.Bind(ipEndPoint);
	listenSocket.Listen(10);

	Console.WriteLine("Сервер запущен");

	while (true)
	{
		var handler = listenSocket.Accept();
		Console.WriteLine("Клиент подключился");

		while (true)
		{
			var builder = new StringBuilder();

			var totalBytes = 0;
			var data = new byte[255];
			var clientHasDisconnected = false;

			do
			{
				var bytes = handler.Receive(data);

				if (bytes == 0)
				{
					clientHasDisconnected = true;
					break;
				}

				builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
				totalBytes += bytes;
			} while (handler.Available > 0);

			if (clientHasDisconnected)
			{
				Console.WriteLine("Клиент отключился");
				break;
			}

			Console.WriteLine($"[{DateTime.Now.ToShortTimeString()}] {builder} ({totalBytes} bytes)");

			handler.Send(Encoding.Unicode.GetBytes(builder.ToString()));
		}
	}
}
catch (Exception e)
{
	Console.WriteLine(e);
}
