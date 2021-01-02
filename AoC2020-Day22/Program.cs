using System;
using System.IO;
using System.Collections.Generic;

int Part1(List<int> deck1, List<int> deck2)
{
    while (deck1.Count > 0 && deck2.Count > 0)
    {
        if (deck1[0] > deck2[0])
        {
            deck1.Add(deck1[0]);
            deck1.Add(deck2[0]);
            deck1.RemoveAt(0);
            deck2.RemoveAt(0);
        }
        else
        {
            deck2.Add(deck2[0]);
            deck2.Add(deck1[0]);
            deck1.RemoveAt(0);
            deck2.RemoveAt(0);
        }
    }
    List<int> win = deck1.Count == 0 ? deck2 : deck1;
    int n = win.Count;
    int score = 0;
    for (int i = 0; i < n; i++)
    {
        score += win[i] * (n - i);
    }   
    return score;
}

int RecursiveGame(List<int> deck1, List<int> deck2, Dictionary<int, int> dp)
{
    var history = new List<int>();
    int initshot = (string.Join(',', deck1) + '|' + string.Join(',', deck2)).GetHashCode();
    while (true)
    {
        int snapshot = (string.Join(',', deck1) + '|' + string.Join(',', deck2)).GetHashCode();
        foreach (int s in history)
        {
            if (snapshot == s)
            {
                dp.Add(initshot, 1);
                return 1;
            }
        }
        history.Add(snapshot);
        if (deck1.Count == 0)
        {
            dp.Add(initshot, 2);
            return 2;
        }
        if (deck2.Count == 0)
        {
            dp.Add(initshot, 1);
            return 1;
        }
        int top1 = deck1[0];
        int top2 = deck2[0];
        deck1.RemoveAt(0);
        deck2.RemoveAt(0);
        int winner = -1;
        if (deck1.Count >= top1 && deck2.Count >= top2)
        {
            List<int> subdeck1 = deck1.GetRange(0, top1);
            List<int> subdeck2 = deck2.GetRange(0, top2);
            int currshot = (string.Join(',', subdeck1) + '|' + string.Join(',', subdeck2)).GetHashCode();
            if (dp.ContainsKey(currshot))
            {
                winner = dp[currshot];
            }
            else
            {
                winner = RecursiveGame(subdeck1, subdeck2, dp);
            }
        }
        else
        {
            winner = top1 > top2 ? 1 : 2;
        }
        if (winner == 1)
        {
            deck1.Add(top1);
            deck1.Add(top2);
        }
        else
        {
            deck2.Add(top2);
            deck2.Add(top1);
        }
    }
}

int Part2(List<int> deck1, List<int> deck2)
{
    var dp = new Dictionary<int, int>();
    int winner = RecursiveGame(deck1, deck2, dp);
    List<int> winningDeck = winner == 1 ? deck1 : deck2;
    int n = winningDeck.Count;
    int score = 0;
    for (int i = 0; i < n; i++)
    {
        score += winningDeck[i] * (n - i);
    }   
    return score;
}

string[] lines = File.ReadAllLines("input.txt");
var deck1 = new List<int>();
var deck2 = new List<int>();
int i = 1;
while (lines[i] != "")
{
    deck1.Add(Convert.ToInt32(lines[i]));
    i++;
}
i += 2;
for (; i < lines.Length; i++)
{
    deck2.Add(Convert.ToInt32(lines[i]));
}
Console.WriteLine("Part 1: {0}", Part1(new List<int>(deck1), new List<int>(deck2)));
Console.WriteLine("Part 2: {0}", Part2(new List<int>(deck1), new List<int>(deck2)));