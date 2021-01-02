using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;

const int CYCLE = 6;
const int zdim = 2 * CYCLE + 1;

int CountActiveNbhd(List<List<char[]>> map, int z0, int x0, int y0, int zdim, int ydim, int xdim)
{
    int count = 0;
    for (int dz = -1; dz <= 1; dz++)
    {
        for (int dy = -1; dy <= 1; dy++)
        {
            for (int dx = -1; dx <= 1; dx++)
            {
                if ((dx, dy, dz) == (0, 0, 0))
                {
                    continue;
                }
                (int z, int x, int y) = (z0 + dz, x0 + dx, y0 + dy);
                if (z >= 0 && x >= 0 && y >=0 && z < zdim && x < xdim && y < ydim)
                {
                    if (map[z][x][y] == '#')
                    {
                        count++;
                    }
                }
            }
        }
    }
    return count;
}

long Part1(string[] lines)
{
    int ydim = lines[0].Length + 2 * CYCLE;
    int xdim = lines.Length + 2 * CYCLE;
    var initshot = new List<char[]>();
    for (int i = 0; i < CYCLE; i++)
    {
        initshot.Add(Enumerable.Repeat('.', ydim).ToArray());
    }
    foreach (string row in lines.Select(s => new string('.', CYCLE) + s + new string('.', CYCLE)))
    {
        initshot.Add(row.ToCharArray());
    }
    for (int i = 0; i < CYCLE; i++)
    {
        initshot.Add(Enumerable.Repeat('.', ydim).ToArray());
    };
    var snapshots = new List<List<char[]>>(zdim);
    snapshots.Add(initshot);
    for (int cycle = 1; cycle <= CYCLE; cycle++)
    {
        var leftshot = new List<char[]>();
        var rightshot = new List<char[]>();
        for (int i = 0; i < xdim; i++)
        {
            leftshot.Add(Enumerable.Repeat('.', ydim).ToArray());
            rightshot.Add(Enumerable.Repeat('.', ydim).ToArray());   
        }
        snapshots.Insert(0, leftshot);
        snapshots.Add(rightshot);
    }
    for (int cycle = 1; cycle <= CYCLE; cycle++)
    {
        var update = new List<List<char[]>>();
        for (int z = 0; z < zdim; z++)
        {
            var newshot = new List<char[]>();
            for (int x = 0; x < xdim; x++)
            {
                char[] newrow = new char[ydim];
                Array.Copy(snapshots[z][x], newrow, ydim);
                newshot.Add(newrow);
            }
            update.Add(newshot);
        }
        for (int z = 0; z < zdim; z++)
        {
            for (int x = 0; x < xdim; x++)
            {
                for (int y = 0; y < ydim; y++)
                {
                    int activeCount = CountActiveNbhd(snapshots, z, x, y, zdim, xdim, ydim);
                    if (snapshots[z][x][y] == '#' && (activeCount < 2 || activeCount > 3))
                    {
                        update[z][x][y] = '.';
                    }
                    else if (snapshots[z][x][y] == '.' && activeCount == 3)
                    {
                        update[z][x][y] = '#';
                    }
                }
            }
        }
        snapshots = update;
    }
    long count = 0;
    for (int z = 0; z < zdim; z++)
    {
        for (int x = 0; x < xdim; x++)
        {
            for (int y = 0; y < ydim; y++)
            {
                if (snapshots[z][x][y] == '#')
                {
                    count++;
                }
            }
        }
    }
    return count;
}

int CountActiveNbhd4Dim(Dictionary<(int, int, int, int), char> map, int x0, int y0, int z0, int w0)
{
    int count = 0;
    for (int dx = -1; dx <= 1; dx++)
    {
        for (int dy = -1; dy <= 1; dy++)
        {
            for (int dz = -1; dz <= 1; dz++)
            {
                for (int dw = -1; dw <= 1; dw++)
                {
                    if ((dx, dy, dz, dw) == (0, 0, 0, 0))
                    {
                        continue;
                    }
                    (int x, int y, int z, int w) = (x0 + dx, y0 + dy, z0 + dz, w0 + dw);
                    if (map.ContainsKey((x, y, z, w)))
                    {
                        if (map[(x, y, z, w)] == '#')
                        {
                            count++;
                        }
                    }
                }
            }
        }
    }
    return count;
}

long Part2(string[] lines)
{
    var map = new Dictionary<(int, int, int, int), char>();
    for (int x = 0; x < lines.Length; x++)
    {
        for (int y = 0; y < lines[0].Length; y++)
        {
            map.Add((x, y, 0, 0), lines[x][y]);
        }
    }
    (int xmin, int xmax) = (-CYCLE, lines.Length - 1 + CYCLE);
    (int ymin, int ymax) = (-CYCLE, lines[0].Length - 1 + CYCLE);
    (int zmin, int zmax) = (-CYCLE, CYCLE);
    (int wmin, int wmax) = (-CYCLE, CYCLE);
    for (int cycle = 1; cycle <= CYCLE; cycle++)
    {
        var newmap = new Dictionary<(int, int, int, int), char>();
        for (int x = xmin; x <= xmax; x++)
        {
            for (int y = ymin; y <= ymax; y++)
            {
                for (int z = zmin; z <= zmax; z++)
                {
                    for (int w = wmin; w <= wmax; w++)
                    {
                        int count = CountActiveNbhd4Dim(map, x, y, z, w);
                        if (!map.ContainsKey((x, y, z, w)) || map[(x, y, z, w)] == '.')
                        {
                            newmap.Add((x, y, z, w), count == 3 ? '#' : '.');
                        }
                        else
                        {
                            newmap.Add((x, y, z, w), count == 2 || count == 3 ? '#' : '.');
                        }
                    }
                }
            }
        }
        map = newmap;
    }
    return map.Values.Where(c => c == '#').LongCount();
}

string[] lines = File.ReadAllLines("input.txt");
Console.WriteLine("Part 1: {0}", Part1(lines));
Console.WriteLine("Part 1: {0}", Part2(lines));