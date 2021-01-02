using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

int FindTwo(List<int> ns, int total, int start)
{
	for (int i = start; i < ns.Count - 1; i++)
	{
		for (int j = i + 1; j < ns.Count; j++)
		{
			int sum = ns[i] + ns[j];
			if (sum == total)
			{
				return ns[i] * ns[j];
			}
		}
	}
	return -1;
}

void Part1()
{
	var ns = new List<int>();
	foreach (string line in File.ReadLines("input.txt"))
	{
		ns.Add(int.Parse(line));
	}
	bool done = false;
	for (int i = 0; i < ns.Count - 1; i++)
	{
		for (int j = i + 1; j < ns.Count; j++) {
			int sum = ns[i] + ns[j];
			if (sum == 2020) {
				Console.WriteLine(ns[i] * ns[j]);
				done = true;
				break;
			}
		}
		if (done)
		{
			break;
		}
	}
}

void Part2()
{
	var ns = new List<int>();
	foreach (string line in File.ReadLines(@"D:\Dev\advent-of-code-2020\day1-input.txt"))
	{
		ns.Add(int.Parse(line));
	}
	for (int i = 0; i < ns.Count - 2; i++) {
		int result = FindTwo(ns, 2020 - ns[i], i + 1);
		if (result > -1) {
			Console.WriteLine(ns[i] * result);
			break;
		}
	}
}

Console.WriteLine("Part 1:");
Part1();
Console.WriteLine("Part 2:");
Part2();


