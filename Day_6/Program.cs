using System.ComponentModel.DataAnnotations;

string[] lines = File.ReadAllLines("./input.txt");
string[] times = lines[0].Split(':', StringSplitOptions.TrimEntries)[1].Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
string[] distances = lines[1].Split(':', StringSplitOptions.TrimEntries)[1].Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

long total = 1;
for (var j = 0; j < times.Length; j++) {
  long raceTime = long.Parse(times[j]);
  long sum = 0;
  for (var i = 1; i < raceTime+1; i++) {
    if ((raceTime-i) * i > long.Parse(distances[j])) {
      sum++;
    }
  }
  total *= sum;
}

Console.WriteLine(total);
