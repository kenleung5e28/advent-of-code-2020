using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

int Part1(List<int> jolts)
{
    int[] diffs = new int[3];
    jolts.Sort();
    diffs[jolts[0] - 1] += 1;
    for (int i = 1; i < jolts.Count; i++)
    {
        diffs[jolts[i] - jolts[i - 1] - 1] += 1;
    }
    diffs[2] += 1;
    return diffs[0] * diffs[2];
}

long ways(List<int> jolts, long[] dp, int start)
{
    if (dp[start] > 0)
        return dp[start];
    long ans = 0;
    for (int j = 1; j <= 3 && start + j < jolts.Count; j++)
    {
        if (jolts[start + j] - jolts[start] <= 3)
        {
            ans += ways(jolts, dp, start + j);
        }
    }
    dp[start] = ans;
    return ans;
}

long Part2(List<int> jolts)
{
    jolts.Sort();
    int n = jolts.Count;
    long[] dp = new long[n];
    dp[n - 1] = 1;
    long result = 0;
    for (int j = 0; j <= 2; j++)
    {
        if (jolts[j] <= 3)
        {
            result += ways(jolts, dp, j);
        }
    }
    return result;
}

string[] lines = File.ReadAllLines("input.txt");
List<int> jolts = lines.Select(x => int.Parse(x)).ToList();
Console.WriteLine("Part 1: {0}", Part1(jolts));
Console.WriteLine("Part 2: {0}", Part2(jolts));