// Part 1
// string[] lines = File.ReadAllLines("input.txt");
// string direction = lines[0];
// Dictionary<string, Dictionary<char, string>> map = [];
// for (var i = 2; i < lines.Length; i++) {
//   string line = lines[i].Replace('('.ToString(), string.Empty);
//   line = line.Replace(')'.ToString(), string.Empty);
//   string[] splitString = line.Split(new char[] {'=',','}, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
//   Dictionary<char, string> leftRight = new()
//   {
//     {'L', splitString[1]},
//     {'R', splitString[2]}
//   };
//   // Console.WriteLine(splitString[0] + " " + splitString[1] + " " + splitString[2]);
//   map[splitString[0]] = leftRight;
// }
// int total = 0;
// int j = 0;
// string currentNode = "AAA";
// while (currentNode[^1] != 'Z') {
//   total++;
//   currentNode = map[currentNode][direction[j]];
//   j++;
//   j %= direction.Length;
// }
//14681
//20221
//16897
//16343
//21883
//13019

// Console.WriteLine(total);

// Part 2

string[] lines = File.ReadAllLines("input.txt");
string direction = lines[0];
List<string> startingPoints = [];
Dictionary<string, Dictionary<char, string>> map = [];
for (var i = 2; i < lines.Length; i++) {
  string line = lines[i].Replace('('.ToString(), string.Empty);
  line = line.Replace(')'.ToString(), string.Empty);
  string[] splitString = line.Split(new char[] {'=',','}, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
  if (splitString[0][2] == 'A') {
    startingPoints.Add(splitString[0]);
  }
  Dictionary<char, string> leftRight = new()
  {
    {'L', splitString[1]},
    {'R', splitString[2]}
  };
  // Console.WriteLine(splitString[0] + " " + splitString[1] + " " + splitString[2]);
  map[splitString[0]] = leftRight;
}

Console.WriteLine(startingPoints.Count);
// List<string> currentNodes = startingPoints;
List<int> pathLenghts = [];
foreach(string start in startingPoints) {
  int total = 0;
  int j = 0;
  string currentNode = start;
  while (currentNode[^1] != 'Z') {
    total++;
    currentNode = map[currentNode][direction[j]];
    j++;
    j %= direction.Length;
  }
  Console.WriteLine(total);
  pathLenghts.Add(total);
}


// get lowest common multiple of path lengths from Google