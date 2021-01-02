using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

int Part1(string[] lines)
{
    char facing = 'E';
    int x = 0;
    int y = 0;
    Action<char, int> move = (dir, dist) =>
    {
        switch (dir)
        {
            case 'N':
                y += dist;
                break;
            case 'E':
                x += dist;
                break;
            case 'S':
                y -= dist;
                break;
            case 'W':
                x -= dist;
                break;
        }
    };
    Action<int> rotate = angle => {
        char[] clock = new char[]{ 'N', 'E', 'S', 'W' };
        int turns = angle / 90;
        int curr = Array.IndexOf(clock, facing);
        facing = clock[(curr + turns) % 4];
    };
    foreach (string line in lines)
    {
        char instr = line[0];
        int param = int.Parse(line.Substring(1));
        switch (instr)
        {
            case 'F':
                move(facing, param);
                break;
            case 'N':
            case 'E':
            case 'S':
            case 'W':
                move(instr, param);
                break;
            case 'L':
                rotate(360 - param);
                break;
            case 'R':
                rotate(param);
                break;
        }
    }
    return (x < 0 ? -x : x) + (y < 0 ? -y : y);
}

int Part2(string[] lines)
{
    int x = 0;
    int y = 0;
    int wx = 10;
    int wy = 1;
    Action<int> moveShip = time =>
    {
       x += time * wx;
       y += time * wy;
    };
    Action<char, int> moveWp = (dir, dist) =>
    {
        switch (dir)
        {
            case 'N':
                wy += dist;
                break;
            case 'E':
                wx += dist;
                break;
            case 'S':
                wy -= dist;
                break;
            case 'W':
                wx -= dist;
                break;
        }
    };
    Action<int> rotate = angle => {
        int turns = angle / 90;
        for (int i = 0; i < turns; i++)
        {
            (wx, wy) = (wy, -wx);
        }
    };
    foreach (string line in lines)
    {
        char instr = line[0];
        int param = int.Parse(line.Substring(1));
        switch (instr)
        {
            case 'F':
                moveShip(param);
                break;
            case 'N':
            case 'E':
            case 'S':
            case 'W':
                moveWp(instr, param);
                break;
            case 'L':
                rotate(360 - param);
                break;
            case 'R':
                rotate(param);
                break;
        }
    }
    return (x < 0 ? -x : x) + (y < 0 ? -y : y);
}

string[] lines = File.ReadAllLines("input.txt");
Console.WriteLine("Part 1: {0}", Part1(lines));
Console.WriteLine("Part 2: {0}", Part2(lines));
