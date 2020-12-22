<Query Kind="Program" />

void Main()
{
	string[] lines = File.ReadAllLines(@"D:\Dev\advent-of-code-2020\day3-input.txt");
	int len = lines.Length;
	int width = lines[0].Length;
	int[] xs = new int[]{ 1, 3, 5, 7, 1 };
	int[] ys = new int[] { 1, 1, 1, 1, 2 };
	long product = 1;
	for (int k = 0; k < xs.Length; k++)
	{
		int count = 0;
		for (int i = 0; i < len; i++)
		{
			if ((xs[k] * i) % ys[k] != 0)
				continue;
			int j = ((xs[k] * i) / ys[k]) % width;
			if (lines[i][j] == '#')
			{
				count += 1;
			}
		}
		Console.WriteLine(count);
		product *= count;
	}
	Console.WriteLine(product);
}

// You can define other methods, fields, classes and namespaces here
