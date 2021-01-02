using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

void Part1()
{
	string[] lines = File.ReadAllLines("input.txt");
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

void Part2()
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

Console.WriteLine("Part 1:");
Part1();
Console.WriteLine("Part 2:");
Part2();