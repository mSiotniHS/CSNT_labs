using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using Lab4_Common;

namespace Lab4_Server;

public sealed class Server
{
	private readonly int _maxClientCount;
	private readonly List<Socket> _clientHandlers;
	private readonly Chat _chat;
	private int _totalClientCount;

	public Server(int maxClientCount)
	{
		_maxClientCount = maxClientCount;
		_clientHandlers = new List<Socket>();
		_chat = new Chat(new List<Message>());
		_totalClientCount = 0;
	}

	public void Run(IPEndPoint endPoint)
	{
		var listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

		try
		{
			listenSocket.Bind(endPoint);
			listenSocket.Listen(_maxClientCount);

			Console.WriteLine("Сервер запущен");

			while (true)
			{
				var handler = listenSocket.Accept();
				Console.WriteLine("Клиент подключился");
				_totalClientCount++;
				_clientHandlers.Add(handler);

				var thread = new Thread(() => ServeClient(handler, _totalClientCount));
				thread.Start();
			}
		}
		catch (Exception exception)
		{
			Console.WriteLine(exception);
		}
	}

	private void ServeClient(Socket handler, int clientId)
	{
		Console.WriteLine($"Начал обслуживание клиента №{clientId}");
		handler.Send(
			Encoding.Unicode.GetBytes(
				PackageWrapper.Wrap(clientId).Serialize()));
		Console.WriteLine($"Отправил клиенту №{clientId} его id");

		handler.Send(
			Encoding.Unicode.GetBytes(
				PackageWrapper.Wrap(_chat).Serialize()));
		Console.WriteLine($"Отправил текущее состояние чата клиенту №{clientId}");

		while (true)
		{
			var clientHasDisconnected = false;
			var data = new byte[256];
			var builder = new StringBuilder();

			do
			{
				int bytes;
				try
				{
					bytes = handler.Receive(data);
				}
				catch (SocketException)
				{
					Console.WriteLine($"Ошибка сокета. Считаю, что соединение с клиентом №{clientId} разорвано");
					clientHasDisconnected = true;
					break;
				}

				if (bytes == 0)
				{
					clientHasDisconnected = true;
					break;
				}

				builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
			} while (handler.Available > 0);

			if (clientHasDisconnected)
			{
				Console.WriteLine($"Клиент №{clientId} отключился");
				_clientHandlers.Remove(handler);
				break;
			}

			HandleIncomingData(builder.ToString(), clientId);
		}
	}

	private void HandleIncomingData(string data, int clientId)
	{
		var rawPackages = Utilities.SplitRawPackages(data);

		foreach (var rawPackage in rawPackages)
		{
			var package = PackageWrapper.Parse(rawPackage);
			var rawMessage = package.Match(
				_ => throw new UnreachableException("Серверу пришёл номер клиента"),
				_ => throw new UnreachableException("Серверу пришёл чат"),
				_ => throw new UnreachableException("Серверу пришло сообщение"),
				rawMessage => rawMessage
			);

			Console.WriteLine($"Получил новое сообщение от клиента №{clientId}: {rawMessage}");
			var message = AddMessage(clientId, rawMessage);

			Console.WriteLine($"Отправляю всем клиентам новое сообщение (от №{clientId})");
			foreach (var clientHandler in _clientHandlers)
			{
				clientHandler.Send(
					Encoding.Unicode.GetBytes(
						JsonSerializer.Serialize(
							PackageWrapper.Wrap(message))));
				Console.WriteLine("Отправил новое сообщение очередному клиенту");
			}
		}
	}

	private Message AddMessage(int clientId, string content)
	{
		var newMessage = new Message(clientId, content, DateTime.Now);
		_chat.Messages.Add(newMessage);

		return newMessage;
	}
}
