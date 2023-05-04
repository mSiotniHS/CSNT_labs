using System.Diagnostics;
using System.Text.Json;
using OneOf;

namespace Lab4_Common;

[Serializable]
public enum PackageKind
{
	UserId,
	Chat,
	Message,
	RawMessage
}

[Serializable]
public record PackageWrapper(PackageKind Kind, string Contents)
{
	public static OneOf<int, Chat, Message, string> Parse(string raw)
	{
		var argumentException = new ArgumentException("Error parsing wrapper contents", nameof(raw));

		var wrapper = JsonSerializer.Deserialize<PackageWrapper>(raw);
		return wrapper switch
		{
			{Kind: PackageKind.UserId} =>
				int.Parse(wrapper.Contents),
			{Kind: PackageKind.Chat} =>
				JsonSerializer.Deserialize<Chat>(wrapper.Contents) ?? throw argumentException,
			{Kind: PackageKind.Message} =>
				JsonSerializer.Deserialize<Message>(wrapper.Contents) ?? throw argumentException,
			{Kind: PackageKind.RawMessage} =>
				wrapper.Contents,
			_ => throw new UnreachableException("Package is of unknown kind")
		};
	}

	public static PackageWrapper Wrap(OneOf<int, Chat, Message, string> item) =>
		item.Match(
			userId => new PackageWrapper(PackageKind.UserId, userId.ToString()),
			chat => new PackageWrapper(PackageKind.Chat, JsonSerializer.Serialize(chat)),
			message => new PackageWrapper(PackageKind.Message, JsonSerializer.Serialize(message)),
			rawMessage => new PackageWrapper(PackageKind.RawMessage, rawMessage)
		);

	public string Serialize() => JsonSerializer.Serialize(this);
}
