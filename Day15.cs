using System;
using System.Linq;
using System.Collections.Generic;

const string input = "17,1,3,16,19,0";

long SpokenAt(long lastTurn)
{
    var spoken = new Dictionary<long, (long, long)>();
    long turn = 1;
    long last = -1;
    foreach (long n in input.Split(',').Select(x => Convert.ToInt64(x)))
    {
        spoken.Add(n, (turn, -1));
        last = n;
        turn++;
    }
    for (; turn <= lastTurn; turn++)
    {
        long curr = spoken[last].Item2 == -1 ? 0 : spoken[last].Item1 - spoken[last].Item2;
        if (spoken.ContainsKey(curr))
        {
            spoken[curr] = (turn, spoken[curr].Item1);
        }
        else
        {
            spoken.Add(curr, (turn, - 1));
        }
        last = curr;
    }
    return last;
}
Console.WriteLine("Part 1: {0}", SpokenAt(2020L));
Console.WriteLine("Part 2: {0}", SpokenAt(30000000L));