partial class Program
{
	public static void MySetColor(ConsoleColor foreground, ConsoleColor background)
	{
		Console.ForegroundColor = foreground;
		Console.BackgroundColor = background;
	}

	public static bool Keyboard(ref int select, int count)
	{
		ConsoleKey keyboard = Console.ReadKey().Key;

		switch (keyboard)
		{
			case ConsoleKey.UpArrow:
			case ConsoleKey.LeftArrow:
			case ConsoleKey.W:
			case ConsoleKey.A:
				if (select == 0)
					select = count;
				select--;
				return true;
			case ConsoleKey.DownArrow:
			case ConsoleKey.RightArrow:
			case ConsoleKey.S:
			case ConsoleKey.D:
				select++;
				select %= count;
				return true;
			case ConsoleKey.Enter:
				return false;
			default:
				return true;
		}
	}

	public static int GetSelect(string selection, string[] selections)
	{
		int select = default;
		int selectionCount = Convert.ToInt32(selections.Length);

		bool isStart = true;
		while (isStart)
		{
			Console.Clear();
			Console.WriteLine(selection);
			for (ushort i = 0; i < selectionCount; i++)
			{
				char prefix = ' ';
				if (i == select)
				{
					MySetColor(ConsoleColor.Blue, ConsoleColor.Red);
					prefix = ' ';
				}
				Console.WriteLine($" {prefix} {selections[i]}");
				Console.ResetColor();
			}
			isStart = Keyboard(ref select, selectionCount);
		}

		return select;
	}
}