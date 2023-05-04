namespace Lab4_Common;

public static class Utilities
{
	public static IEnumerable<string> SplitRawPackages(string json)
	{
		var parts = json.Split("}{");

		for (var i = 0; i < parts.Length; i++)
		{
			var part = parts[i];

			var isFirst = i == 0;
			var isLast = i == parts.Length - 1;

			yield return (isFirst, isLast) switch
			{
				(true, true) => part,
				(true, false) => part + "}",
				(false, true) => "{" + part,
				(false, false) => "{" + part + "}"
			};
		}
	}
}
