using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

/*
Coordinate changes corresponding to moving directions:
e  (1,0)
w  (-1,0)
nw (-1,1)
sw (0,-1)
ne (0, 1)
se (1,-1)
*/

var black = new Dictionary<(int, int), bool>();

(int, int) Move((int, int) point, string dir)
{
  (int x, int y) = point;
  (int dx, int dy) = (0, 0);
  switch (dir)
  {
    case "e":
      (dx, dy) = (1, 0);
      break;
    case "w":
      (dx, dy) = (-1, 0);
      break;
    case "nw":
      (dx, dy) = (-1, 1);
      break;
    case "sw":
      (dx, dy) = (0, -1);
      break;
    case "ne":
      (dx, dy) = (0, 1);
      break;
    case "se":
      (dx, dy) = (1, -1);
      break;
  }
  return (x + dx, y + dy);
}

void Flip((int, int) point)
{
  if (black.ContainsKey(point))
  {
    black[point] = !black[point];
  }
  else
  {
    black.Add(point, true);
  }
}

bool IsBlack((int, int) point)
{
  return black.ContainsKey(point) && black[point];
}

int NbhdBlackCount((int, int) point)
{
  int count = 0;
  foreach (string dir in new string[] { "e", "w", "nw", "ne", "sw", "se" })
  {
    (int, int) nbhd = Move(point, dir);
    if (IsBlack(nbhd))
    {
      count++;
    }
  }
  return count;
}

int Part1(string[] lines)
{
  foreach (string line in lines)
  {
    int i = 0;
    (int, int) curr = (0, 0);
    while (i < line.Length)
    {
      char c = line[i];
      if (c == 'e' || c == 'w')
      {
        curr = Move(curr, c.ToString());
        i++;
      }
      else
      {
        curr = Move(curr, line.Substring(i, 2));
        i += 2;
      }
    }
    Flip(curr);
  }
  return black.Where(kv => kv.Value).Count();
}

void AddNbhds()
{
  var newblack = new Dictionary<(int, int), bool>(black);
  foreach ((int, int) point in black.Keys)
  {
    foreach (string dir in new string[] { "e", "w", "nw", "ne", "sw", "se" })
    {
      (int, int) nbhd = Move(point, dir);
      if (!newblack.ContainsKey(nbhd))
      {
        newblack.Add(nbhd, false);
      }
    }
  }
  black = newblack;
}

string [] lines = File.ReadAllLines("input.txt");
Console.WriteLine("Part 1: {0}", Part1(lines));

for (int day = 1; day <= 100; day++)
{
  AddNbhds();
  var newblack = new Dictionary<(int, int), bool>();
  foreach ((int, int) point in black.Keys)
  {
    int count = NbhdBlackCount(point);
    if (black[point] && (count == 0 || count > 2))
    {
      newblack.Add(point, false);
    }
    else if (!black[point] && count == 2)
    {
      newblack.Add(point, true);
    }
    else
    {
      newblack.Add(point, black[point]);
    }
  }
  black = newblack;
}
Console.WriteLine("Part2: {0}", black.Where(kv => kv.Value).Count());