﻿using System.Net;
using System.Net.Sockets;
using System.Text;

const int port = 8005;
const string address = "127.0.0.1";
const string stopWord = "STOP";

try
{
	var ipEndPoint = new IPEndPoint(IPAddress.Parse(address), port);
	var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
	socket.Connect(ipEndPoint);

	while (true)
	{
		var builder = new StringBuilder();

		Console.Write($"Введите сообщение (для закрытия подключения введите {stopWord}):\n> ");
		var message = Console.ReadLine();
		ArgumentException.ThrowIfNullOrEmpty(message);

		if (message == stopWord)
		{
			socket.Shutdown(SocketShutdown.Both);
			socket.Close();
			break;
		}

		var data = Encoding.Unicode.GetBytes(message);
		socket.Send(data);

		var answer = new byte[256];
		do
		{
			var bytes = socket.Receive(answer, answer.Length, 0);
			builder.Append(Encoding.Unicode.GetString(answer, 0, bytes));
		} while (socket.Available > 0);

		Console.WriteLine($"Ответ сервера: {builder}\n");
	}
}
catch (Exception e)
{
	Console.WriteLine(e);
}
