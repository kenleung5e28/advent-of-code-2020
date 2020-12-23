using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

int Part1(string[] lines)
{
    int count = 0;
    bool[] yes = new bool[26];
    foreach (string line in lines)
    {
        if (line == "")
        {
            int total = yes.Where(x => x).Count();
            count += total;
            yes = new bool[26];
        }
        foreach (char c in line)
        {
            yes[c - 'a'] = true;
        }
    }
    return count;
}

int Part2(string[] lines)
{
    int count = 0;
    int[] yesCounts = new int[26];
    int groupSize = 0;
    foreach (string line in lines)
    {
        if (line == "")
        {
            count += yesCounts.Where(x => x == groupSize).Count();
            yesCounts = new int[26];
            groupSize = 0;
            continue;
        }
        groupSize += 1;
        foreach (char c in line)
        {
            yesCounts[c - 'a'] += 1;
        }
    }
    return count;
}

List<string> lines = new List<string>(File.ReadAllLines("input.txt"));
lines.Add("");
string[] processed = lines.ToArray();
Console.WriteLine("Part 1: {0}", Part1(processed));
Console.WriteLine("Part 2: {0}", Part2(processed));