// Part 1
// using System.Runtime.InteropServices;

// string[] lines = File.ReadAllLines("./input.txt");
// int total = 0;
// foreach (string line in lines) {
//   string[] splitString = line.Split(new char[] {':','|'}, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
//   string[] winningNumbers = splitString[1].Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
//   string[] myNumbers = splitString[2].Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
//   Dictionary<string, int> matchingNums = [];
//   foreach (string num in myNumbers) {
//     matchingNums[num] = 0;
//   }
// int sum = 0;
//   foreach (string num in winningNumbers) {
//     if (matchingNums.TryGetValue(num, out int currentCount)) {
//       sum += 1;
//     }
//   }
//   total += (int)Math.Pow(2,sum-1);
// }

// Console.WriteLine("total " + total);

// Part 2
using System;
using System.Linq;

string[] lines = File.ReadAllLines("./input.txt");
int[] cardCount = Enumerable.Repeat(1, lines.Length).ToArray();
int currentIndex = 0;
foreach (string line in lines) {
  string[] splitString = line.Split(new char[] {':','|'}, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
  string cardNumber = splitString[0].Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)[1];
  string[] winningNumbers = splitString[1].Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
  string[] myNumbers = splitString[2].Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
  Dictionary<string, int> matchingNums = [];
  foreach (string num in myNumbers) {
    matchingNums[num] = 0;
  }
  int sum = 0;
  foreach (string num in winningNumbers) {
    if (matchingNums.TryGetValue(num, out int currentCount)) {
      sum += 1;
    }
  }
  for (var i = currentIndex+1; i < currentIndex+1+sum; i++) {
    cardCount[i] += cardCount[currentIndex];
  }
  currentIndex++;
}

Console.WriteLine("total " + cardCount.Sum());