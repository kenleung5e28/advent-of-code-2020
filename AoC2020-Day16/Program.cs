using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;

long Part1(Dictionary<string, (int, int)[]> fieldRanges, List<int[]> nearbyTickets)
{
    long sum = 0;
    foreach (int[] ticket in nearbyTickets)
    {
        foreach (int n in ticket)
        {
            if (fieldRanges.Values.All(ranges => ranges.All(range => n < range.Item1 || n > range.Item2)))
            {
                sum += n;
            }
        }
    }
    return sum;
}

bool validTicket(Dictionary<string, (int, int)[]> fieldRanges, int[] ticket)
{
    foreach (int n in ticket)
    {
        if (fieldRanges.Values.All(ranges => ranges.All(range => n < range.Item1 || n > range.Item2)))
        {
            return false;
        }
    }
    return true;
}

long Part2(Dictionary<string, (int, int)[]> fieldRanges, List<int[]> tickets)
{
    string[] keys = fieldRanges.Keys.ToArray();
    int n = keys.Length;
    List<string>[] validKeys = new List<string>[n];
    for (int i = 0; i < n; i++)
    {
        var ls = new List<string>();
        foreach (string k in keys)
        {
            if (tickets.All(values => !fieldRanges[k].All(range => range.Item1 > values[i] || values[i] > range.Item2)))
            {
                ls.Add(k);
            }
        }
        validKeys[i] = ls;
    }
    string[] correctKeys = new string[n];
    int foundCount = 0;
    while (foundCount < n)
    {
        int singletonIndex = 0;
        while (validKeys[singletonIndex].Count != 1)
        {
            singletonIndex++;
        }
        string key = validKeys[singletonIndex][0];
        correctKeys[singletonIndex] = key;
        foundCount++;
        foreach (List<string> ls in validKeys)
        {
            ls.Remove(key);
        }
    }
    // foreach (string k in correctKeys)
    // {
    //     Console.Write("{0},", k);
    // }
    // Console.WriteLine();
    long answer = 1;
    for (int i = 0; i < n; i++)
    {
        if (correctKeys[i].StartsWith("departure"))
        {
            // Console.WriteLine(tickets[tickets.Count - 1][i]);
            answer *= tickets[tickets.Count - 1][i];
        }
    }
    return answer;
}

string[] lines = File.ReadAllLines("input.txt");
var fieldRanges = new Dictionary<string, (int, int)[]>();
int i = 0;
while (lines[i] != "")
{
    string[] nameAndRest = lines[i].Split(':');
    string[] rangesText = nameAndRest[1].Split(" or ");
    (int, int)[] ranges = rangesText.Select(s =>
    {
        string[] parts = s.Split('-');
        return (int.Parse(parts[0].Trim()), int.Parse(parts[1].Trim()));
    }
    ).ToArray();
    fieldRanges.Add(nameAndRest[0], ranges);
    i++;
}
i += 2;
int[] myTicket = lines[i].Split(',').Select(s => int.Parse(s)).ToArray();
i += 3;
var nearbyTickets = new List<int[]>();
for (; i < lines.Length; i++)
{
    nearbyTickets.Add(lines[i].Split(',').Select(s => int.Parse(s)).ToArray());
}
Console.WriteLine("Part 1: {0}", Part1(fieldRanges, nearbyTickets));
List<int[]> tickets = nearbyTickets.Where(x => validTicket(fieldRanges, x)).ToList();
tickets.Add(myTicket);
Console.WriteLine("Part 2: {0}", Part2(fieldRanges, tickets));