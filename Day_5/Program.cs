
string[] lines = File.ReadAllLines("./input.txt");
Dictionary<string, List<List<long>>> mapNames = [];
string mapName = "";
// Part 1
// string[] seeds = lines[0].Split(':', StringSplitOptions.TrimEntries)[1].Split(" ", StringSplitOptions.TrimEntries);
//Part 2
// List<string> seeds = [];
string[] seedRanges = lines[0].Split(':', StringSplitOptions.TrimEntries)[1].Split(" ", StringSplitOptions.TrimEntries);
// for (var i = 0; i < seedRanges.Length; i++) {
//   if (i % 2 == 0) {
//     for (var j = 0; j < int.Parse(seedRanges[i+1]); j++) {
//       seeds.Add((long.Parse(seedRanges[i]) + j) + "");
//     }
//   }
// }
for (var i = 2; i < lines.Length; i++) {
  string line = lines[i];
  if (line.Length == 0) {continue; };
  if (line[^1] == ':') {
    mapName = line.Split(" ", StringSplitOptions.TrimEntries)[0];
    mapNames[mapName] = [];
  } else {
    string[] splitString = line.Split(" ", StringSplitOptions.TrimEntries);
    long destRangeStart = long.Parse(splitString[0]);
    long sourceRangeStart = long.Parse(splitString[1]);
    long rangeLength = long.Parse(splitString[2]);
    List<long> info = [destRangeStart, sourceRangeStart, rangeLength];
    mapNames[mapName].Add(info);
  }
}
List<long> locations = [];
for (var i = 0; i < seedRanges.Length; i++) {
  if (i % 2 == 0) {
    // Console.WriteLine(seedRanges[i]);
    for (var j = 0; j < long.Parse(seedRanges[i+1]); j++) {
      long seed = long.Parse(seedRanges[i]) + j;
      locations.Add(getVal("humidity-to-location", getVal("temperature-to-humidity",getVal("light-to-temperature", getVal("water-to-light", getVal("fertilizer-to-water", getVal("soil-to-fertilizer", getVal("seed-to-soil", (seed)))))))));
    }
  }
}


Console.WriteLine(locations.Min());

long getVal (string name, long num) {
  foreach (List<long> list in mapNames[name]) {
    long difference = list[1] - list[0];
    if (num >= list[1] && num < list[1]+list[2]) {
      return num - difference;
    }
  }
  return num;
}

// Console.WriteLine(reverseGetVal("humidity-to-location", 66));

// long reverseGetVal (string name, long num) {
//   foreach (List<long> list in mapNames[name]) {
//     long difference = list[1] - list[0];
//     if (num >= list[1] && num < list[1]+list[2]) {
//       return num + difference;
//     }
//   }
//   return num;
// }


