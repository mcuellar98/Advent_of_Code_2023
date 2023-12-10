using System.Diagnostics.CodeAnalysis;

string[] lines = File.ReadAllLines("input.txt");
int total = 0;
foreach (string line in lines) {
  string[] strInput = line.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
  int[] input = strInput.Select(numString => int.Parse(numString)).ToArray();
  total += getNextVal(input);
}

Console.WriteLine(total);

// Part 1
// int getNextVal(int[] array) {
//   int[] subArray = new int[array.Length-1];
//   bool allZeros = true;
//   for (var i = 0; i < array.Length-1; i++) {
//     subArray[i] = array[i+1] - array[i];
//     if (array[i+1] - array[i] != 0) {
//       allZeros = false;
//     }
//   }
//   if (allZeros) {
//     return array[^1];
//   } else {
//     return array[^1] + getNextVal(subArray);
//   }
// }

// Part 2
int getNextVal(int[] array) {
  int[] subArray = new int[array.Length-1];
  bool allZeros = true;
  for (var i = array.Length-1; i > 0; i--) {
    subArray[i-1] = array[i] - array[i-1];
    if (array[i] - array[i-1] != 0) {
      allZeros = false;
    }
  }
  if (allZeros) {
    return array[0];
  } else {
    return array[0] - getNextVal(subArray);
  }
}