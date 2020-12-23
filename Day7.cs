using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;

int Part1(Dictionary<string, Dictionary<string, int>> bags)
{
    var can = new Dictionary<string, bool>();
    can.Add("shiny gold", true);
    var queue = new Queue<string>();
    foreach (string bag in bags.Keys)
    {
        queue.Enqueue(bag);
    }
    while (queue.Count > 0)
    {
        string bag = queue.Dequeue();
        if (can.ContainsKey(bag))
        {
            continue;
        }
        var keys = bags[bag].Keys;
        if (keys.Count == 0)
        {
            can.Add(bag, false);
            continue;
        }
        if (keys.Where(k => can.ContainsKey(k) && can[k]).Any())
        {
            can.Add(bag, true);
            continue;
        }
        if (keys.All(k => can.ContainsKey(k) && !can[k]))
        {
            can.Add(bag, false);
            continue;
        }
        foreach (string k in keys.Where(k => !can.ContainsKey(k)))
        {
            queue.Enqueue(k);
        }
        queue.Enqueue(bag);
    }
    return can.Keys.Where(k => can[k]).Count() - 1;
}

int Part2(Dictionary<string, Dictionary<string, int>> bags)
{
    var counts = new Dictionary<string, int>();
    var queue = new Queue<string>();
    queue.Enqueue("shiny gold");
    while (queue.Count > 0)
    {
        string bag = queue.Dequeue();
        var keys = bags[bag].Keys;
        if (counts.ContainsKey(bag))
        {
            continue;
        }
        if (keys.Count == 0)
        {
            counts.Add(bag, 0);
            continue;
        }
        if (keys.All(k => counts.ContainsKey(k)))
        {
            counts.Add(bag, keys.Aggregate(0, (acc, k) => acc + bags[bag][k] * (1 + counts[k])));
            continue;
        }
        foreach (string k in keys.Where(k => !counts.ContainsKey(k)))
        {
            queue.Enqueue(k);
        }
        queue.Enqueue(bag);
    }
    return counts["shiny gold"];
}

string[] lines = File.ReadAllLines("input.txt");
var bags = new Dictionary<string, Dictionary<string, int>>();
foreach (string line in lines)
{
    string bagColor = Regex.Match(line, @"^(.+) bags contain ").Groups[1].Value;
    var bagContent = new Dictionary<string, int>();
    foreach (Match match in Regex.Matches(line, @"(\d+) (.+?) bags*[,.]"))
    {
        string color = match.Groups[2].Value;
        int count = Convert.ToInt32(match.Groups[1].Value);
        bagContent.Add(color, count);
    }
    bags.Add(bagColor, bagContent);
}
Console.WriteLine("Part 1: {0}", Part1(bags));
Console.WriteLine("Part 2: {0}", Part2(bags));