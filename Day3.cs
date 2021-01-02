using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

void Part1()
{
	string[] lines = File.ReadAllLines("input.txt");
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

void Part2()
{
	string[] lines = File.ReadAllLines("input.txt");
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

Console.WriteLine("Part 1:");
Part1();
Console.WriteLine("Part 2:");
Part2();