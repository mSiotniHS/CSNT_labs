using System.Net;

namespace Lab4_Common;

public static class Definitions
{
	public const int Port = 8005;
	public const string Address = "127.0.0.1";

	public static readonly IPEndPoint EndPoint = new(IPAddress.Parse(Address), Port);
}
