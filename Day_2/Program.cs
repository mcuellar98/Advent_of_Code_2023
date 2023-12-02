// Part1
// string[] lines = File.ReadAllLines("./input.txt");
// int total = 0;
// Dictionary<string, int> maxColors = new() {
//  {"red", 12},
//  {"green", 13},
//  {"blue", 14}
// };

// foreach (string line in lines) {
//   string[] split1 = line.Split(":");
//   string[] gameNum = split1[0].Split(" ");
//   int ID = int.Parse(gameNum[1]);
//   string[] colorsAndNums = split1[1].Split(new char[] {',',';'}, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
//   foreach (string pair in colorsAndNums) {
//     string[] splitPair = pair.Split(' ');
//     int num = int.Parse(splitPair[0]);
//     string color = (splitPair[1]);
//     if (num > maxColors[color]) {
//       ID = 0;
//       break;
//     }
//   }
//   total += ID;
// }

// Console.WriteLine(total);

// Part 2

using System.Reflection.Metadata.Ecma335;

string[] lines = File.ReadAllLines("./input.txt");
double total = 0;
Dictionary<string, double> maxOfEachColor = new() {
 {"red", 0},
 {"green",0},
 {"blue", 0}
};

foreach (string line in lines) {
  string[] split1 = line.Split(":");
  string[] gameNum = split1[0].Split(" ");
  string[] colorsAndNums = split1[1].Split(new char[] {',',';'}, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
  foreach (string pair in colorsAndNums) {
    string[] splitPair = pair.Split(' ');
    double num = double.Parse(splitPair[0]);
    string color = (splitPair[1]);
    if (num > maxOfEachColor[color]) {
      maxOfEachColor[color] = num;
    }
  }
  total += getMinPower(maxOfEachColor);
  foreach (var key in maxOfEachColor.Keys) {
    maxOfEachColor[key] = 0;
  }
}

static double getMinPower(Dictionary<string, double> obj) {
    double total = 1;
    foreach (var value in obj.Values) {
      total *= value;
    }
    return total;
  }

Console.WriteLine(total);