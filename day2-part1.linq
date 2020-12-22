<Query Kind="Program" />

void Main()
{
	string[] lines = File.ReadAllLines(@"D:\Dev\advent-of-code-2020\day2-input.txt");
	int validCount = 0;
	foreach (string line in lines)
	{
		string[] parts = line.Split(' ');
		int[] bounds = parts[0].Split('-').Select(x => int.Parse(x)).ToArray();
		char target = parts[1][0];
		string password = parts[2].Trim();
		int count = password.Aggregate(0, (acc, curr) => acc += curr == target ? 1 : 0);
		if (count >= bounds[0] && count <= bounds[1])
		{
			validCount += 1;
		}
	}
	Console.WriteLine(validCount);
}

// You can define other methods, fields, classes and namespaces here
