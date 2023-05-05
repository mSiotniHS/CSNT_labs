using System.Diagnostics;
using System.Net.Sockets;
using System.Text;
using Lab4_Common;
using Message = Lab4_Common.Message;

namespace Lab4_Client;

public partial class MainForm : Form
{
	private int? _userId;

	private Chat _chat;
	private readonly Socket _socket;

	private readonly Font _regular = new("Iosevka", 12, FontStyle.Regular);
	private readonly Font _bold = new("Iosevka", 12, FontStyle.Bold);

	public MainForm()
	{
		InitializeComponent();

		_chat = new Chat(new List<Message>());
		_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
		_socket.Connect(Definitions.EndPoint);

		_userId = null;

		var receivingThread = new Thread(ReceiveUpdates);
		receivingThread.Start();
	}

	private void SendMessage()
	{
        var text = MessageTextBox.Text.Trim();
        if (text == string.Empty) return;

        _socket.Send(
            Encoding.Unicode.GetBytes(
                PackageWrapper.Wrap(text).Serialize()));
        MessageTextBox.Clear();
    }

	private void SendButton_Click(object sender, EventArgs e)
	{
		SendMessage();
	}

	private void ReceiveUpdates()
	{
		while (true)
		{
			var rawData = GetData();
			if (rawData is null)
			{
				MessageBox.Show(
					@"Возможно, сервер отключился. Аварийно завершаю работу.",
					@"Ошибка сокета",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error);
				Close();

				return;
			}

			var rawPackages = Utilities.SplitRawPackages(rawData);

			foreach (var rawPackage in rawPackages)
			{
				var package = PackageWrapper.Parse(rawPackage);
				package.Switch(
					userId => _userId = userId,
					chat =>
					{
						_chat = chat;
						UpdateChatBox(_chat.Messages);
					},
					message =>
					{
						_chat.Messages.Add(message);
						UpdateChatBox(new []{ message });
					},
					_ => throw new UnreachableException("Клиенту пришло сообщение от другого клиента")
				);
			}
		}
	}

	private string? GetData()
	{
		var answer = new byte[256];
		var builder = new StringBuilder();

		do
		{
			var bytes = 0;
			try
			{
				bytes = _socket.Receive(answer);
			}
			catch (SocketException)
			{
				return null;
			}

			if (bytes == 0)
			{
				return null;
			}

			builder.Append(Encoding.Unicode.GetString(answer, 0, bytes));
		} while (_socket.Available > 0);

		return builder.ToString();
	}

	private void UpdateChatBox(IEnumerable<Message> messages)
	{
		var sorted = messages
			.OrderBy(message => message.DateTime);

		foreach (var message in sorted)
		{
			ChatBox.SelectionAlignment =
				message.UserId == _userId
					? HorizontalAlignment.Right
					: HorizontalAlignment.Left;

			ChatBox.SelectionFont = _bold;
			ChatBox.SelectedText = $"User {message.UserId} [{message.DateTime.ToShortTimeString()}]\n";

			ChatBox.SelectionFont = _regular;
			ChatBox.SelectedText = message.Content;

			ChatBox.SelectedText = "\n\n";
		}

		ChatBox.ScrollToCaret();
	}

	private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
    {
	    _socket.Shutdown(SocketShutdown.Both);
	    _socket.Close();
    }
}
