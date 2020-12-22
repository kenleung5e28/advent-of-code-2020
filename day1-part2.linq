<Query Kind="Program" />

void Main()
{
	var ns = new List<int>();
	foreach (string line in File.ReadLines(@"D:\Dev\advent-of-code-2020\day1-input.txt"))
	{
		ns.Add(int.Parse(line));
	}
	for (int i = 0; i < ns.Count - 2; i++) {
		int result = FindTwo(ns, 2020 - ns[i], i + 1);
		if (result > -1) {
			Console.WriteLine(ns[i] * result);
			break;
		}
	}
}

// You can define other methods, fields, classes and namespaces here
int FindTwo(List<int> ns, int total, int start)
{
	for (int i = start; i < ns.Count - 1; i++)
	{
		for (int j = i + 1; j < ns.Count; j++)
		{
			int sum = ns[i] + ns[j];
			if (sum == total)
			{
				return ns[i] * ns[j];
			}
		}
	}
	return -1;
}