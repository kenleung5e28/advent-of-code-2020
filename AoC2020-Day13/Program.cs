using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

int Part1(int arrival, int[] buses)
{
    int min = int.MaxValue;
    int argmin = -1;
    for (int i = 0; i < buses.Length; i++)
    {
        int wait = buses[i] - arrival % buses[i];
        if (wait < min)
        {
            min = wait;
            argmin = buses[i];
        }
    }
    return min * argmin;
}

long Part2(string line)
{
    string[] parts = line.Split(',');
    var buses = new List<(int, int)>();
    long m = 1;
    for (int i = 0; i < parts.Length; i++)
    {
        if (parts[i] != "x")
        {
            int bus = int.Parse(parts[i]);
            int rem = -i;
            while (rem < 0)
            {
                rem += bus;
            
            }
            buses.Add((bus, rem));
            m *= bus;
        }
    }
    long answer = 0;
    foreach (var (bus, rem) in buses)
    {
        long b = m / bus;
        long c = 1;
        while ((b * c) % bus != 1)
        {
            c++;
        }
        answer += rem * b * c;
    }
    return answer % m;
}

string[] lines = File.ReadAllLines("input.txt");
int arrival = int.Parse(lines[0]);
int[] buses1 = lines[1].Split(',').Where(s => s != "x").Select(s => int.Parse(s)).ToArray();
Console.WriteLine("Part 1: {0}", Part1(arrival, buses1));
Console.WriteLine("Part 2: {0}", Part2(lines[1]));