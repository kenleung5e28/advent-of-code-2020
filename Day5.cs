using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

int Part1()
{
    string[] lines = File.ReadAllLines("input.txt");
    int max = -1;
    foreach (string line in lines)
    {
        string bin = new string(line.ToCharArray().Select(c => c == 'F' || c == 'L' ? '0' : '1').ToArray());
        int value = Convert.ToInt32(bin, 2);
        if (value > max)
        {
            max = value;
        }
    }
    return max;
}

int Part2()
{
    string[] lines = File.ReadAllLines("input.txt");
    var ids = new List<int>();
    foreach (string line in lines)
    {
        string bin = new string(line.ToCharArray().Select(c => c == 'F' || c == 'L' ? '0' : '1').ToArray());
        ids.Add(Convert.ToInt32(bin, 2));      
    }
    ids.Sort();
    for (int i = 1; i < ids.Count; i++)
    {
        if (ids[i] - ids[i - 1] == 2)
        {
            return ids[i] - 1;
        }
    }
    return -1;
}

Console.WriteLine("Part 1: {0}", Part1());
Console.WriteLine("Part 2: {0}", Part2());