using System;
using System.IO;
using System.Linq;

long Part1(long[] ns)
{ 
    for (int k = 25; k < ns.Length; k++)
    {
        long n = ns[k];
        bool found = false;
        for (int i = k - 25; i < k - 1; i++)
        {
            for (int j = i + 1; j < k; j++)
            {
                if (ns[i] + ns[j] == n)
                {
                    found = true;
                    break;
                }
            }
            if (found)
            {
                break;
            }
        }
        if (!found)
        {
            return n;
        }
    }
    return -1;
}

long Part2(long[] ns)
{
    long target = Part1(ns);
    for (int i = 0; i < ns.Length - 1; i++)
    {
        long acc = ns[i];
        long min = ns[i];
        long max = ns[i];
        for (int j = i + 1; j < ns.Length; j++)
        {
            long n = ns[j];
            acc += n;
            if (n < min)
            {
                min = n;
            } else if (n > max)
            {
                max = n;
            }
            if (acc == target)
            {
                return min + max;
            } else if (acc > target)
            {
                break;
            }
        }
    }
    return -1;
}

string[] lines = File.ReadAllLines("input.txt");
long[] numbers = lines.Select(s => long.Parse(s)).ToArray();
Console.WriteLine("Part 1: {0}", Part1(numbers));
Console.WriteLine("Part 2: {0}", Part2(numbers));
