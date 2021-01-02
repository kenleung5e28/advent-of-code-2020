using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

var symbols = new Dictionary<int, char>()
{
    [-1] = 'a',
    [-2] = 'b'
};
var rules = new Dictionary<int, List<int>[]>();

// rules.Add(0, new List<int>[] { new List<int> { 4, 1, 5 } });
// rules.Add(1, new List<int>[] { new List<int> { 2, 3 }, new List<int> { 3, 2 } });
// rules.Add(2, new List<int>[] { new List<int> { 4, 4 }, new List<int> { 5, 5 } });
// rules.Add(3, new List<int>[] { new List<int> { 4, 5 }, new List<int> { 5, 4 } });
// rules.Add(4, new List<int>[] { new List<int> { -1 } });
// rules.Add(5, new List<int>[] { new List<int> { -2 } });

(bool, int) CheckRule(string input, int start, int ruleNumber)
{
    if (start >= input.Length)
    {
        return (false, start);
    }
    if (ruleNumber < 0)
    {
        return (input[start] == symbols[ruleNumber], start + 1);
    }
    List<int>[] ruleLists = rules[ruleNumber];
    int minNext = input.Length + 1;
    foreach (List<int> ls in ruleLists)
    {
        int i = 0;
        int next = start;
        bool accept = true;
        while (accept && i < ls.Count)
        {
            (accept, next) = CheckRule(input, next, ls[i]);
            i++;
        }
        if (accept)
        {
            return (true, next);
        }
        if (next < minNext)
        {
            minNext = next;
        }
    }
    return (false, minNext);
}

bool IsValid(string input)
{
    (bool pass, int pos) = CheckRule(input, 0, 0);
    return pass && pos == input.Length;
}

(bool, int) IsValid2(string input)
{
    (bool pass, int pos) = CheckRule(input, 0, 0);
    return (pass && pos == input.Length, pos);
}

string[] lines = File.ReadAllLines("input.txt");
int i = 0;
while (lines[i] != "")
{
    string[] ruleNumberEtc = lines[i].Split(':');
    int ruleNumber = Convert.ToInt32(ruleNumberEtc[0]);
    int index = ruleNumberEtc[1].IndexOf('"');
    if (index > -1)
    {
        int symbolId = ruleNumberEtc[1][index + 1] == 'a' ? -1 : -2;
        ruleNumberEtc[1] = symbolId.ToString();
    }
    string[] ruleListsParts = ruleNumberEtc[1].Split('|');
    var ruleLists = ruleListsParts.Select(s => s.Trim().Split(' ').Select(n => Convert.ToInt32(n)).ToList()).ToArray();
    rules.Add(ruleNumber, ruleLists);
    i++;
}
int validCount = 0;
for (int j = i; j < lines.Length; j++)
{
    if (IsValid(lines[j]))
    {
        validCount++;
    }
}

Console.WriteLine("Part 1: {0}", validCount);
// rule 0 = 8 11 = 42^m 31^n (m > n >= 1)
validCount = 0;
for (int j = i; j < lines.Length; j++)
{
    int m = 2;
    bool nextLine = false;
    while (!nextLine && m < lines[j].Length)
    {
        rules[0] = new List<int>[] { new List<int>(Enumerable.Repeat(42, m)) };
        for (int n = 1; n < m; n++)
        {
            rules[0][0].Add(31);
            (bool valid, int end) = IsValid2(lines[j]);
            if (!valid && end == lines[j].Length)
            {
                nextLine = true;
                break;
            }
            if (valid)
            {
                validCount++;
                nextLine = true;
                break;
            }
        }
        m++;
    }
}
Console.WriteLine("Part 2: {0}", validCount);
