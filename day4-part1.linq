<Query Kind="Program" />

void Main()
{
	string[] lines = File.ReadAllLines(@"D:\Dev\advent-of-code-2020\day4-input.txt");
	int validCount = 0;
	var fields = new Dictionary<string, bool>()
	{
		["byr"] = false, 
		["iyr"] = false,
		["eyr"] = false,
		["hgt"] = false,
		["hcl"] = false,
		["ecl"] = false,
		["pid"] = false
	};
	foreach (string line in lines)
	{
		if (line == "")
		{
			if (isValid(fields))
			{
				validCount += 1;
			}
			fields = new Dictionary<string, bool>()
			{
				["byr"] = false,
				["iyr"] = false,
				["eyr"] = false,
				["hgt"] = false,
				["hcl"] = false,
				["ecl"] = false,
				["pid"] = false
			};
		} 
		else 
		{
			string[] entries = line.Split(' ');
			foreach (string entry in entries)
			{
				string key = entry.Split(':')[0];
				if (fields.ContainsKey(key))
				{
					fields[key] = true;
				}
			}
		}
	}
	if (isValid(fields))
	{
		validCount += 1;
	}
	Console.WriteLine(validCount);
}

// You can define other methods, fields, classes and namespaces here
bool isValid<T>(Dictionary<T, bool> fields)
{
	return !fields.ContainsValue(false);
}
