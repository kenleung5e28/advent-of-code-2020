using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text;

bool Occupy(string[] map)
{
    int m = map.Length;
    int n = map[0].Length;
    string[] newmap = new string[m];
    bool changed = false;
    for (int i = 0; i < m; i++)
    {
        var sb = new StringBuilder(n);
        for (int j = 0; j < n; j++)
        {
            if (map[i][j] == 'L')
            {
                bool alter = true;
                if (i > 0)
                {
                    if ((j > 0 && map[i - 1][j - 1] == '#') || map[i - 1][j] == '#' || (j < n - 1 && map[i - 1][j + 1] == '#'))
                        alter = false;
                }
                if ((j > 0 && map[i][j - 1] == '#') || (j < n - 1 && map[i][j + 1] == '#'))
                    alter = false;
                if (i < m - 1)
                {
                    if ((j > 0 && map[i + 1][j - 1] == '#') || map[i + 1][j] == '#' || (j < n - 1 && map[i + 1][j + 1] == '#'))
                        alter = false;
                }
                if (alter)
                {
                    changed = true;
                    sb.Append('#');
                }
                else
                {
                    sb.Append(map[i][j]);
                }
            }
            else if (map[i][j] == '#')
            {
                int count = 0;
                if (i > 0)
                {
                    if (j > 0 && map[i - 1][j - 1] == '#')
                        count += 1;
                    if (map[i - 1][j] == '#') 
                        count += 1;
                    if (j < n - 1 && map[i - 1][j + 1] == '#')
                        count += 1;
                }
                if (j > 0 && map[i][j - 1] == '#') 
                    count += 1;
                if (j < n - 1 && map[i][j + 1] == '#')
                    count += 1;
                if (i < m - 1)
                {
                    if (j > 0 && map[i + 1][j - 1] == '#')
                        count += 1;
                    if (map[i + 1][j] == '#')
                        count += 1;
                    if (j < n - 1 && map[i + 1][j + 1] == '#')
                        count += 1;
                }
                if (count >= 4)
                {
                    changed = true;
                    sb.Append('L');
                }
                else
                {
                    sb.Append(map[i][j]);
                }
            }
            else
            {
                sb.Append(map[i][j]);
            }
        }
        newmap[i] = sb.ToString();
    }
    for (int i = 0; i < m; i++)
    {
        map[i] = newmap[i];
    }
    return changed;
}

bool OccupyReal(string[] map)
{
    int m = map.Length;
    int n = map[0].Length;
    Func<int, int, int, int, char> LookAt = (x, y, xdir, ydir) => {
        if (xdir == 0 && ydir == 0)
        {
            return '.';
        }
        if (xdir > 0)
        {
            xdir = 1;
        }
        else if (xdir < 0)
        {
            xdir = -1;
        }
        if (ydir > 0)
        {
            ydir = 1;
        }
        else if (ydir < 0)
        {
            ydir = -1;
        }
        while (x + xdir < m && y + ydir < n && x + xdir >= 0 && y + ydir >= 0)
        {
            x += xdir;
            y += ydir;
            char c = map[x][y];
            if (c == '#' || c == 'L')
            {
                return c;
            }
        }
        return '.';
    };
    string[] newmap = new string[m];
    bool changed = false;
    for (int i = 0; i < m; i++)
    {
        var sb = new StringBuilder(n);
        for (int j = 0; j < n; j++)
        {
            if (map[i][j] == 'L')
            {
                bool alter = true;
                for (int xdir = -1; xdir <= 1; xdir++)
                {
                    for (int ydir = -1; ydir <= 1; ydir++)
                    {
                        if (LookAt(i, j, xdir, ydir) == '#')
                        {
                            alter = false;
                        }
                    }
                }
                if (alter)
                {
                    changed = true;
                    sb.Append('#');
                }
                else
                {
                    sb.Append(map[i][j]);
                }
            }
            else if (map[i][j] == '#')
            {
                int count = 0;
                for (int xdir = -1; xdir <= 1; xdir++)
                {
                    for (int ydir = -1; ydir <= 1; ydir++)
                    {
                        if (LookAt(i, j, xdir, ydir) == '#')
                        {
                            count += 1;
                        }
                    }
                }
                if (count >= 5)
                {
                    changed = true;
                    sb.Append('L');
                }
                else
                {
                    sb.Append(map[i][j]);
                }
            }
            else
            {
                sb.Append(map[i][j]);
            }
        }
        newmap[i] = sb.ToString();
    }
    for (int i = 0; i < m; i++)
    {
        map[i] = newmap[i];
    }
    return changed;
}

int CountOccupied(string[] map)
{
    int count = 0;
    foreach (string row in map)
    {
        foreach (char c in row)
        {
            if (c == '#')
            {
                count += 1;
            }
        }
    }
    return count;
}

int Part1(string[] lines)
{
    string[] map = new string[lines.Length];
    lines.CopyTo(map, 0);
    bool changed = true;
    while (changed)
    {
        changed = Occupy(map);
    }
    // foreach (string row in map)
    // {
    //     foreach (char c in row)
    //     {
    //         Console.Write(c);
    //     }
    //     Console.WriteLine();
    // }
    return CountOccupied(map);
}

int Part2(string[] lines)
{
    string[] map = new string[lines.Length];
    lines.CopyTo(map, 0);
    bool changed = true;
    while (changed)
    {
        changed = OccupyReal(map);
    }
    return CountOccupied(map);
}

string[] lines = File.ReadAllLines("input.txt");
Console.WriteLine("Part 1: {0}", Part1(lines));
Console.WriteLine("Part 2: {0}", Part2(lines));
