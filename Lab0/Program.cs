using System.Net;

var ip = IPAddress.Loopback;
Console.WriteLine($"IP Loopback = {ip}");

ip = IPAddress.None;
Console.WriteLine($"IP None = {ip}");

ip = IPAddress.Any;
Console.WriteLine($"IP Any = {ip}");

ip = IPAddress.Broadcast;
Console.WriteLine($"IP Broadcast = {ip}");

ip = IPAddress.Parse("123.45.67.89");
Console.WriteLine($"IP = {ip}");

Console.WriteLine();


var host = Dns.GetHostName();
Console.WriteLine($"My comp name = {host}");

Console.WriteLine("My addresses:");
foreach (var address in Dns.GetHostAddresses(host))
	Console.WriteLine($"*) {address} (address family: {address.AddressFamily})");

Console.WriteLine();


var host1 = Dns.GetHostEntry("unn.ru");
Console.WriteLine(host1.HostName);

foreach(var ip0 in host1.AddressList)
	Console.WriteLine(ip0);
