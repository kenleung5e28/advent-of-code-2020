using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

long Part1(string[] lines)
{
    string mask = null;
    var memory = new Dictionary<string, string>();
    foreach(string line in lines)
    {
        if (line.StartsWith("mask"))
        {
            mask = line.Split('=')[1].Trim();
        }
        else
        {
            string[] parts = line.Split('=');
            string address = parts[0].Split(']')[0].Substring(4);
            long value = long.Parse(parts[1].Trim());
            string raw = Convert.ToString(value, 2).PadLeft(36, '0');
            char[] data = new char[36];
            for (int i = 0; i < 36; i++)
            {
                data[i] = mask[i] == 'X' ? raw[i] : mask[i];
            }
            if (memory.ContainsKey(address))
            {
                memory[address] = new string(data);
            }
            else
            {
                memory.Add(address, new string(data));
            }
        }
    }
    long sum = 0;
    foreach (string v in memory.Values)
    {
        sum += Convert.ToInt64(v, 2);
    }
    return sum;
}

long Part2(string[] lines)
{
    char[] mask = null;
    var memory = new Dictionary<string, long>();
    foreach(string line in lines)
    {
        if (line.StartsWith("mask"))
        {
            mask = line.Split('=')[1].Trim().ToCharArray();
        }
        else
        {
            string[] parts = line.Split('=');
            long value = long.Parse(parts[1].Trim());
            Action<string, int> write = null;
            write = (address, start) => {
                int i = start;
                while (i < address.Length && address[i] != 'X')
                {
                    i += 1;
                }
                if (i == address.Length)
                {
                    if (memory.ContainsKey(address))
                    {
                        memory[address] = value;
                    }
                    else
                    {
                        memory.Add(address, value);
                    }
                }
                else
                {
                    char[] unmasked = address.ToCharArray();
                    unmasked[i] = '0';
                    write(new string(unmasked), i + 1);
                    unmasked[i] = '1';
                    write(new string(unmasked), i + 1);
                }
            };
            char[] address = Convert.ToString(long.Parse(parts[0].Split(']')[0].Substring(4)), 2).PadLeft(36, '0').ToCharArray();
            char[] masked = new char[mask.Length];
            for (int i = 0; i < mask.Length; i++)
            {
                switch (mask[i])
                {
                    case '0':
                        masked[i] = address[i];
                        break;
                    default:
                        masked[i] = mask[i];
                        break;
                }
            }
            write(new string(masked), 0);
        }
    }
    return memory.Values.Sum();
}

string[] lines = File.ReadAllLines("input.txt");
Console.WriteLine("Part 1: {0}", Part1(lines));
Console.WriteLine("Part 2: {0}", Part2(lines));