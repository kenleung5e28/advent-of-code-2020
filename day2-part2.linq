<Query Kind="Program" />

void Main()
{
	string[] lines = File.ReadAllLines(@"D:\Dev\advent-of-code-2020\day2-input.txt");
	int validCount = 0;
	foreach (string line in lines)
	{
		string[] parts = line.Split(' ');
		int[] positions = parts[0].Split('-').Select(x => int.Parse(x)).ToArray();
		char target = parts[1][0];
		string password = parts[2].Trim();
		if (password.Length < positions[1])
		{
			continue;
		}
		bool matchedFirst = password[positions[0] - 1] == target;
		bool matchedSecond = password[positions[1] - 1] == target;
		if (matchedFirst ^ matchedSecond)
		{
			validCount += 1;
		}
	}
	Console.WriteLine(validCount);
}

// You can define other methods, fields, classes and namespaces here
