using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

string[] lines = File.ReadAllLines("input.txt");
Func<Dictionary<int, Tile>> prepareTiles = () => 
{
  var tiles = new Dictionary<int, Tile>();
  int i = 0;
  while (i < lines.Length)
  {
    int id = Convert.ToInt32(lines[i].Substring(5, 4));
    i++;
    string[] tile = new string[10];
    for (int j = 0; j < 10; j++)
    {
      tile[j] = lines[i + j];
    }
    tiles.Add(id, new Tile(tile));
    i += 11;
  }
  return tiles;
};
var tiles = prepareTiles();
int[] tileIds = tiles.Keys.ToArray();
foreach (int id in tileIds)
{
  foreach (int other in tileIds)
  {
    if (other == id)
    {
      continue;
    }
    Tile t = tiles[other];
    Tile me = tiles[id];
    foreach (string edge in new string[] { t.Top, Reverse(t.Top), t.Bottom, Reverse(t.Bottom), t.Left, Reverse(t.Left), t.Right, Reverse(t.Right) })
    {
      if (edge == me.Top)
      {
        me.TopNbhd = other;
      }
      else if (edge == me.Bottom)
      {
        me.BottomNbhd = other;
      }
      else if (edge == me.Left)
      {
        me.LeftNbhd = other;
      }
      else if (edge == me.Right)
      {
        me.RightNbhd = other;
      }
    }
  }
}
long part1Answer = 1;
foreach (int id in tiles.Where(kv => kv.Value.CountNbhds() == 2).Select(kv => kv.Key))
{
  part1Answer *= id;
}
Console.WriteLine("Part 1: {0}", part1Answer);
int seaDim = (int)Math.Round(Math.Sqrt(tileIds.Length));
int[][] seaConfig = new int[seaDim][];
for (int i = 0; i < seaDim; i++)
{
  seaConfig[i] = new int[seaDim];
}
foreach (int id in tileIds)
{
  Tile tile = tiles[id];
  if (tile.TopNbhd == 0 && tile.LeftNbhd == 0 && tile.BottomNbhd > 0 && tile.RightNbhd > 0)
  {
    seaConfig[0][0] = id;
    break;
  }
}
for (int row = 0; row < seaDim; row++)
{
  for (int col = 1; col < seaDim; col++)
  {
    seaConfig[row][col] = tiles[seaConfig[row][col - 1]].RightNbhd;
    tiles[seaConfig[row][col]] = AdjustToMatchLeft(tiles[seaConfig[row][col]], tiles[seaConfig[row][col - 1]].Right);
  }
  if (row < seaDim - 1)
  {
    seaConfig[row + 1][0] = tiles[seaConfig[row][0]].BottomNbhd;
    tiles[seaConfig[row + 1][0]] = AdjustToMatchTop(tiles[seaConfig[row + 1][0]], tiles[seaConfig[row][0]].Bottom);
  }
}
int dim = seaDim * 8;
string[] sea = new string[dim];
for (int row = 0; row < seaDim; row++)
{
  for (int i = 0; i < 8; i++)
  {
    var sb = new StringBuilder();
    for (int col = 0; col < seaDim; col++)
    {
      sb.Append(tiles[seaConfig[row][col]].Map[i + 1].Substring(1, 8));
    }
    sea[row * 8 + i] = sb.ToString();
  }
}
string[] monster = new string[3];
monster[0] = "                  # ";
monster[1] = "#    ##    ##    ###";
monster[2] = " #  #  #  #  #  #   ";
int monsterVolume = 1 + 8 + 6;
Tile seaTile = new Tile(sea);
Regex[] pattern = new Regex[3];
for (int i = 0; i < 3; i++)
{
  pattern[i] = new Regex(monster[i].Replace(' ', '.'));
}
Func<int> orientAndCountMonster = () => 
{
  for (int k = 0; k <= 1; k++)
  {
    for (int j = 0; j <= 3; j++)
    {
      int monsterCount = 0;
      sea = seaTile.Map;
      for (int row = 0; row < dim - 2; row++)
      {
        for (int i = 0; i < dim - 20; i++)
        {
          if (pattern[0].Match(sea[row].Substring(i, 20)).Success && pattern[1].Match(sea[row + 1].Substring(i, 20)).Success && pattern[2].Match(sea[row + 2].Substring(i, 20)).Success)
          {
            monsterCount++;
          }
        }
      }
      if (monsterCount > 0)
      {
        return monsterCount;
      }
      seaTile = Rotate(seaTile);
    }
    seaTile = Filp(seaTile);
  }
  return 0;
};
int monsterCount = orientAndCountMonster();
int totalSharps = CountSharps(seaTile);
int part2Answer = totalSharps - monsterCount * monsterVolume;
Console.WriteLine("Part 2: {0}", part2Answer);


// Functions and Classes

string Reverse(string s)
{
  return new string(s.Reverse().ToArray());
}

int CountSharps(Tile tile)
{
  string[] map = tile.Map;
  int count = 0;
  foreach(string row in map)
  {
    foreach (char c in row)
    {
      if (c == '#')
      {
        count++;
      }
    }
  }
  return count;
}

Tile Rotate(Tile tile)
{
  int n = tile.Map.Length;
  char[][] map = tile.Map.Select(row => row.ToCharArray()).ToArray();
  char[][] newmap = new char[n][];
  for (int row = 0; row < n; row++)
  {
    newmap[row] = new char[n];
  }
  for (int row = 0; row < n; row++)
  {
    for (int col = 0; col < n; col++)
    {
      newmap[col][n - 1 - row] = map[row][col]; 
    }
  }
  Tile transformed = new Tile(newmap.Select(row => new string(row)).ToArray());
  transformed.RightNbhd = tile.TopNbhd;
  transformed.BottomNbhd = tile.RightNbhd;
  transformed.LeftNbhd = tile.BottomNbhd;
  transformed.TopNbhd = tile.LeftNbhd;
  return transformed;
}

Tile Filp(Tile tile)
{
  int n = tile.Map.Length;
  string[] newmap = tile.Map.Reverse().ToArray();
  Tile transformed = new Tile(newmap);
  transformed.TopNbhd = tile.BottomNbhd;
  transformed.BottomNbhd = tile.TopNbhd;
  transformed.LeftNbhd = tile.LeftNbhd;
  transformed.RightNbhd = tile.RightNbhd;
  return transformed;
}

Tile AdjustToMatchLeft(Tile tile, string side)
{
  Tile adjusting = tile;
  for (int k = 0; k <= 1; k++)
  {
    for (int i = 0; i <= 3; i++)
    {
      if (adjusting.Left == side)
      {
        return adjusting;
      }
      adjusting = Rotate(adjusting);
    }
    adjusting = Filp(adjusting);
  }
  throw new Exception("No matching transformation.");
}

Tile AdjustToMatchTop(Tile tile, string side)
{
  Tile adjusting = tile;
  for (int k = 0; k <= 1; k++)
  {
    for (int i = 0; i <= 3; i++)
    {
      if (adjusting.Top == side)
      {
        return adjusting;
      }
      adjusting = Rotate(adjusting);
    }
    adjusting = Filp(adjusting);
  }
  throw new Exception("No matching transformation.");
}


class Tile
{
  public string[] Map { get; }
  public string Top { get; }
  public string Left { get; }
  public string Bottom { get; }
  public string Right { get; }
  public int TopNbhd { get; set; }
  public int LeftNbhd { get; set; }
  public int BottomNbhd { get; set; }
  public int RightNbhd { get; set; }

  public Tile(string[] map)
  {
    int n = map.Length;
    Map = map;
    Top = map[0];
    Bottom = map[n - 1];
    Left = new string(map.Select(row => row[0]).ToArray());
    Right = new string(map.Select(row => row[n - 1]).ToArray());
    TopNbhd = 0;
    LeftNbhd = 0;
    BottomNbhd = 0;
    RightNbhd = 0;
  }

  public int CountNbhds()
  {
    int count = 0;
    if (TopNbhd > 0)
    {
      count++;
    }
    if (BottomNbhd > 0)
    {
      count++;
    }
    if (LeftNbhd > 0)
    {
      count++;
    }
    if (RightNbhd > 0)
    {
      count++;
    }
    return count;
  }

}