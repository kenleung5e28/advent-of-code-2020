<Query Kind="Program" />

void Main()
{
	string[] lines = File.ReadAllLines(@"D:\Dev\advent-of-code-2020\day3-input.txt");
	int len = lines.Length;
	int width = lines[0].Length;
	int count = 0;
	for (int i = 0; i < len; i++)
	{
		int j = (3 * i) % width;
		if (lines[i][j] == '#') {
			count += 1;
		}
	}
	Console.WriteLine(count);
}

// You can define other methods, fields, classes and namespaces here
