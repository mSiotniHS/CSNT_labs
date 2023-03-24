using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;

const int port = 8005;
const string address = "127.0.0.1";

try
{
	var ipEndPoint = new IPEndPoint(IPAddress.Parse(address), port);
	var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
	socket.Connect(ipEndPoint);

	var builder = new StringBuilder();

	Console.Write("Введите сообщение: ");
	var message = Console.ReadLine();
	Debug.Assert(message != null, nameof(message) + " != null");

	var data = Encoding.Unicode.GetBytes(message);
	socket.Send(data);

	var answer = new byte[256];
	do
	{
		var bytes = socket.Receive(answer, answer.Length, 0);
		builder.Append(Encoding.Unicode.GetString(answer, 0, bytes));
	} while (socket.Available > 0);

	Console.WriteLine($"Ответ сервера: {builder}");

	socket.Shutdown(SocketShutdown.Both);
	socket.Close();
}
catch (Exception e)
{
	Console.WriteLine(e.Message);
}
