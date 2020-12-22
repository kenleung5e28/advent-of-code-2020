<Query Kind="Program" />

void Main()
{
	var ns = new List<int>();
	foreach (string line in File.ReadLines(@"D:\Dev\advent-of-code-2020\day1-input.txt"))
	{
		ns.Add(int.Parse(line));
	}
	bool done = false;
	for (int i = 0; i < ns.Count - 1; i++)
	{
		for (int j = i + 1; j < ns.Count; j++) {
			int sum = ns[i] + ns[j];
			if (sum == 2020) {
				Console.WriteLine(ns[i] * ns[j]);
				done = true;
				break;
			}
		}
		if (done)
		{
			break;
		}
	}
}

// You can define other methods, fields, classes and namespaces here
