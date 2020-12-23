using System;
using System.IO;

(int, bool) Part1(string[] lines)
{
    int acc = 0;
    bool[] visited = new bool[lines.Length];
    int pointer = 0;
    while (pointer < lines.Length)
    {
        if (visited[pointer])
        {
            return (acc, false);
        }
        visited[pointer] = true;
        string[] parts = lines[pointer].Split(' ');
        string type = parts[0];
        int arg = int.Parse(parts[1]);
        if (type == "acc")
        {
            acc += arg;
            pointer++;
        }
        else if (type == "jmp")
        {
            pointer += arg;
        }
        else
        {
            pointer++;
        }
    }
    return (acc, true);
}

int Part2(string[] lines)
{
    for (int i = 0; i < lines.Length; i++)
    {
        string[] entry = lines[i].Split(' ');
        if (entry[0] == "acc")
        {
            continue;
        }
        string oldLine = lines[i];
        lines[i] = (entry[0] == "nop" ? "jmp" : "nop") + " " + entry[1];
        var (acc, exitNormally) = Part1(lines);
        if (exitNormally)
        {
            return acc;
        }
        lines[i] = oldLine;
    }
    return 0;
}

string[] lines = File.ReadAllLines("input.txt");
Console.WriteLine("Part 1: {0}", Part1(lines));
Console.WriteLine("Part 2: {0}", Part2(lines));
