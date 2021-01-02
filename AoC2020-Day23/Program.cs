using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

const string INPUT = "952438716";
// const string INPUT = "389125467";

void Part1()
{
  List<int> cups = INPUT.Select(c => c - '0').ToList();
  int n = INPUT.Length;
  int pStart = 0;
  for (int turn = 1; turn <= 100; turn++)
  {
    int start = cups[pStart];
    int[] buffer = new int[3];
    for (int i = 0; i < 3; i++)
    {
      buffer[i] = cups[(pStart + i + 1) % n];
    }
    for (int i = 0; i < 3; i++)
    {
      cups.Remove(buffer[i]);
    }
    int dest = start == 1 ? n : start - 1;
    while (cups.IndexOf(dest) == -1)
    {
      dest = dest == 1 ? n : dest - 1;
    }
    int pDest = cups.IndexOf(dest);
    cups.InsertRange((pDest + 1) % n, buffer);
    pStart = (cups.IndexOf(start) + 1) % n;
  }
  var sb = new StringBuilder();
  int p = cups.IndexOf(1);
  for (int i = 1; i < n; i++)
  {
    sb.Append((char)(cups[(p + i) % n] + '0'));
  }
  Console.WriteLine("Part 1: {0}", sb.ToString());
}

const int MAX = 1000000;
const int ROUND = 10000000;

void Part2()
{
  int[] pointers = new int[MAX + 1];
  pointers[0] = -1;
  for (int i = 0; i < INPUT.Length - 1; i++)
  {
    pointers[INPUT[i] - '0'] = INPUT[i + 1] - '0';
  }
  pointers[INPUT[INPUT.Length - 1] - '0'] = INPUT.Length + 1;
  for (int i = INPUT.Length + 1; i < MAX; i++)
  {
    pointers[i] = i + 1;
  }
  pointers[MAX] = INPUT[0] - '0';
  int start = INPUT[0] - '0';
  for (int turn = 1; turn <= ROUND; turn++)
  {
    int[] picked = new int[3];
    int p = start;
    for (int i = 0; i < 3; i++)
    {
      p = pointers[p];
      picked[i] = p;
    }
    int dest = start > 1 ? start - 1 : MAX;
    while (picked.Contains(dest))
    {
      dest = dest > 1 ? dest - 1 : MAX;
    }
    pointers[start] = pointers[picked[2]];
    pointers[picked[2]] = pointers[dest];
    pointers[dest] = picked[0];
    start = pointers[start];
  }
  Console.WriteLine("Part 2: {0}", (long)pointers[1] * (long)pointers[pointers[1]]);
}

Part1();
Part2();
