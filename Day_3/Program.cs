
using System.Data;
using System.Globalization;
using System.Numerics;
using System.Runtime.InteropServices;
// Part 1
// class Program {
//   struct IndexPair
//   {
//       public int Row { get; set; }
//       public int Col { get; set; }
//   }

//   static void Main() {

//     string[] lines = File.ReadAllLines("./input.txt");
//     int rowCount = lines.Length;
//     int columnCount = lines[0].Length;
//     char[][] matrix = new char[rowCount + 2][];
//     matrix[0] = Enumerable.Repeat('.', columnCount + 2).ToArray();
//     for (var i = 1; i <= lines.Length; i++) {
//       matrix[i] = ('.' + lines[i - 1] + '.').ToCharArray();
//     }
//     matrix[rowCount + 1] = Enumerable.Repeat('.', columnCount + 2).ToArray();
//     int total = 0;
//     for (int i = 1; i < matrix.Length-1; i++) {
//       int j = 1;
//       while (j < matrix[0].Length-1) {
//         if (char.IsDigit((matrix[i][j]))) {
//           List<IndexPair> numIndices = [];
//           string num = "";
//           int k = j;
//           while (k < matrix[0].Length-1 && char.IsDigit((char)matrix[i][k])) {
//             num += matrix[i][k];
//             numIndices.Add(new IndexPair{Row = i, Col = k});
//             k++;
//           }
//           if (isPartNumber(matrix, numIndices)) {
//             total += int.Parse(num);
//           }
//           numIndices = [];
//           num = "";
//           j = k;
//         } else {
//           j++;
//         }
//       }
//     }

//     Console.WriteLine("total " + total);
//   }

//   static bool isPartNumber(char[][] matrix, List<IndexPair> coords) {
//     foreach (var coord in coords) {
//       // if left edge of number
//       if (!char.IsDigit(matrix[coord.Row][coord.Col-1]) && (isSymbol(matrix[coord.Row+1][coord.Col]) || isSymbol(matrix[coord.Row][coord.Col-1]) || isSymbol(matrix[coord.Row-1][coord.Col]) || isSymbol(matrix[coord.Row-1][coord.Col-1]) || isSymbol(matrix[coord.Row+1][coord.Col-1]))) {
//         return true;
//       }
//       // if right edge of number
//       else if (!char.IsDigit(matrix[coord.Row][coord.Col+1]) && (isSymbol(matrix[coord.Row+1][coord.Col]) || isSymbol(matrix[coord.Row][coord.Col+1]) || isSymbol(matrix[coord.Row-1][coord.Col]) || isSymbol(matrix[coord.Row-1][coord.Col+1]) || isSymbol(matrix[coord.Row+1][coord.Col+1]))){
//         return true;
//       }
//       //else
//       else if (isSymbol(matrix[coord.Row+1][coord.Col]) || isSymbol(matrix[coord.Row-1][coord.Col])) {
//         return true;
//       }
//     }
//     return false;
//   }

//   static bool isSymbol(char value) {
//     return !char.IsDigit(value) && value != '.';
//   }
// }

// Part 2
class Program {
  struct IndexPair
  {
      public int Row { get; set; }
      public int Col { get; set; }
  }

  static void Main() {
    Dictionary<IndexPair, List<int>> potentialGears = new Dictionary<IndexPair, List<int>>();
    string[] lines = File.ReadAllLines("./input.txt");
    int rowCount = lines.Length;
    int columnCount = lines[0].Length;
    char[][] matrix = new char[rowCount + 2][];
    matrix[0] = Enumerable.Repeat('.', columnCount + 2).ToArray();
    for (var i = 1; i <= lines.Length; i++) {
      matrix[i] = ('.' + lines[i - 1] + '.').ToCharArray();
    }
    matrix[rowCount + 1] = Enumerable.Repeat('.', columnCount + 2).ToArray();
    int total = 0;
    for (int i = 1; i < matrix.Length-1; i++) {
      int j = 1;
      while (j < matrix[0].Length-1) {
        if (char.IsDigit((matrix[i][j]))) {
          List<IndexPair> numIndices = [];
          string num = "";
          int k = j;
          while (k < matrix[0].Length-1 && char.IsDigit((char)matrix[i][k])) {
            num += matrix[i][k];
            numIndices.Add(new IndexPair{Row = i, Col = k});
            k++;
          }
          foreach (IndexPair asteriskCoord in getAsteriskIdices(matrix, numIndices)) {
            if (potentialGears.TryGetValue(asteriskCoord, out List<int> existingList)) {
              existingList.Add(int.Parse(num));
            } else {
               potentialGears[asteriskCoord] = new List<int> { int.Parse(num) };
            }
          }
          numIndices = [];
          num = "";
          j = k;
        } else {
          j++;
        }
      }
    }
    Dictionary<IndexPair, int> usedPairs= new Dictionary<IndexPair, int>();
    int count = 1;
    foreach (List<int> gear in potentialGears.Values) {
      if (gear.Count == 2) {
        count++;
        total += gear[0] * gear[1];
      }
    }
    Console.WriteLine("total " + total);
  }

  static List<IndexPair> getAsteriskIdices(char[][] matrix, List<IndexPair> coords) {
    List<IndexPair> asteriskIndices = [];
    foreach (var coord in coords) {
      if (!char.IsDigit(matrix[coord.Row][coord.Col+1]) && matrix[coord.Row+1][coord.Col+1] == '*') {
        asteriskIndices.Add(new IndexPair {Row = coord.Row+1, Col = coord.Col+1});
      }
      if (!char.IsDigit(matrix[coord.Row][coord.Col-1]) && matrix[coord.Row+1][coord.Col-1] == '*') {
        asteriskIndices.Add(new IndexPair {Row = coord.Row+1, Col = coord.Col-1});
      }
      if (!char.IsDigit(matrix[coord.Row][coord.Col-1]) && matrix[coord.Row-1][coord.Col-1] == '*') {
        asteriskIndices.Add(new IndexPair {Row = coord.Row-1, Col = coord.Col-1});
      }
      if (!char.IsDigit(matrix[coord.Row][coord.Col+1]) && matrix[coord.Row-1][coord.Col+1] == '*') {
        asteriskIndices.Add(new IndexPair {Row = coord.Row-1, Col = coord.Col+1});
      }
      if (matrix[coord.Row+1][coord.Col] == '*') {
        asteriskIndices.Add(new IndexPair {Row = coord.Row+1, Col = coord.Col});
      }
      if (matrix[coord.Row-1][coord.Col] == '*') {
        asteriskIndices.Add(new IndexPair {Row = coord.Row-1, Col = coord.Col});
      }
      if (matrix[coord.Row][coord.Col-1] == '*') {
        asteriskIndices.Add(new IndexPair {Row = coord.Row, Col = coord.Col-1});
      }
      if (matrix[coord.Row][coord.Col+1] == '*') {
        asteriskIndices.Add(new IndexPair {Row = coord.Row, Col = coord.Col+1});
      }

    }
    return asteriskIndices;
  }

  static bool isSymbol(char value) {
    return !char.IsDigit(value) && value != '.';
  }
}

