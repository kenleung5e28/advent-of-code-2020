using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

(int, Dictionary<string, SortedSet<string>>) Part1(List<(SortedSet<string>, SortedSet<string>)> data)
{
  var possibilities = new Dictionary<string, SortedSet<string>>();
  foreach((SortedSet<string> ingredients, SortedSet<string> allergens) in data)
  {
    foreach (string allergen in allergens)
    {
      if (possibilities.ContainsKey(allergen))
      {
        possibilities[allergen].IntersectWith(ingredients);
      }
      else
      {
        possibilities.Add(allergen, new SortedSet<string>(ingredients));
      }
    }
  }
  var allergicIngredients = new SortedSet<string>();
  foreach (SortedSet<string> v in possibilities.Values)
  {
    allergicIngredients.UnionWith(v);
  }
  int count = 0;
  foreach (var kv in data)
  {
    count += kv.Item1.Except(allergicIngredients).Count();
  }
  return (count, possibilities);
}

string Part2(Dictionary<string, SortedSet<string>> possibilities)
{
  var associations = new SortedDictionary<string, string>();
  bool changed = false;
  do
  {
    changed = false;
    string target = null;
    foreach ((string allergen, SortedSet<string> ingredients) in possibilities)
    {
      if (ingredients.Count == 1)
      {
        target = ingredients.Min;
        associations.Add(allergen, target);
        possibilities.Remove(allergen);
        break;
      }
    }
    foreach ((string allergen, SortedSet<string> ingredients) in possibilities)
    {
      if (ingredients.Contains(target))
      {
        ingredients.Remove(target);
        changed = true;
      }
    }
  }
  while (changed);
  return string.Join(',', associations.Select(kv => kv.Value));
}

string[] lines = File.ReadAllLines("input.txt");
var data = new List<(SortedSet<string>, SortedSet<string>)>();
string separator = " (contains ";
foreach (string line in lines)
{
  int sep = line.IndexOf(separator);
  string ingredients = line.Substring(0, sep);
  string allergens = line.Substring(sep + separator.Length, line.Length - 1 - sep - separator.Length);
  data.Add((new SortedSet<string>(ingredients.Split(' ')), new SortedSet<string>(allergens.Split(", "))));
}
(int, Dictionary<string, SortedSet<string>>) part1Answer = Part1(data);
Console.WriteLine("Part 1: {0}", part1Answer.Item1);
Console.WriteLine("Part 2: {0}", Part2(part1Answer.Item2));