using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

bool isValid<T>(Dictionary<T, bool> fields)
{
	return !fields.ContainsValue(false);
}

void Part1()
{
	string[] lines = File.ReadAllLines("input.txt");
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

void Part2()
{
	string[] lines = File.ReadAllLines("input.txt");
	int validCount = 0;
	var rules = new Dictionary<string, Func<string, bool>>()
	{
		["byr"] = s =>
		{
			if (!Regex.Match(s, @"^\d{4}$").Success)
			{
				return false;
			}
			int year = int.Parse(s);
			return year >= 1920 && year <= 2002;
		},
		["iyr"] = s =>
		{
			if (!Regex.Match(s, @"^\d{4}$").Success)
			{
				return false;
			}
			int year = int.Parse(s);
			return year >= 2010 && year <= 2020;
		},
		["eyr"] = s =>
		{
			if (!Regex.Match(s, @"^\d{4}$").Success)
			{
				return false;
			}
			int year = int.Parse(s);
			return year >= 2020 && year <= 2030;
		},
		["hgt"] = s =>
		{
			Match match = Regex.Match(s, @"^(\d+)(cm|in)$");
			if (!match.Success)
			{
				return false;
			}
			int value = int.Parse(match.Groups[1].Value);
			string unit = match.Groups[2].Value;
			if (unit == "cm") {
				return value >= 150 && value <= 193;
			}
			return value >= 59 && value <= 76;
		},
		["hcl"] = s => Regex.Match(s, @"^#[0-9a-f]{6}$").Success,
		["ecl"] = s => new string[] { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" }.Contains(s),
		["pid"] = s => Regex.Match(s, @"^\d{9}$").Success
	};
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
				string[] kv = entry.Split(':');
				if (fields.ContainsKey(kv[0]))
				{
					fields[kv[0]] = rules[kv[0]](kv[1]);
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

Console.WriteLine("Part 1:");
Part1();
Console.WriteLine("Part 2:");
Part2();