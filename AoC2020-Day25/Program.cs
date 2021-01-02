using System;
using System.IO;
using System.Linq;

// int[] keys = new int[] { 5764801, 17807724 };
string[] lines = File.ReadAllLines("input.txt");
int[] keys = lines.Select(line => Convert.ToInt32(line)).ToArray();
int[] loopSizes = new int[2];
for (int i = 0; i < 2; i++)
{
    int subjectNumber = 7;
    int value = 1;
    int size = 1;
    while (true)
    {
        value = (value * subjectNumber) % 20201227;
        if (value == keys[i])
        {
            loopSizes[i] = size;
            break;
        }
        size++;
    }
}
long encryptionKey = 1;
for (int i = 0; i < loopSizes[1]; i++)
{
    encryptionKey = (encryptionKey * keys[0]) % 20201227;
}
Console.WriteLine(encryptionKey);