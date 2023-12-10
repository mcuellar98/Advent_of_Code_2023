// Part 1
// string[] lines = File.ReadAllLines("./input.txt");
// int total = 0;
// List<string> nums = [];


// foreach (string line in lines) {
//   foreach (char character in line) {
//     if (Char.IsNumber(character)) {
//       nums.Add(character.ToString());
//     }
//   }
//   total += int.Parse(nums[0] + nums[^1]);
//   nums = [];
// }

//Part 2

using System.ComponentModel;

class Program {
  class IndexValueObj
  {
      public int Index { get; set; }
      public string? Value { get; set; }
  }

  static void Main() {
    List<IndexValueObj> indicesAndValues = [];

    string[] lines = File.ReadAllLines("./input.txt");
    int total = 0;

    Dictionary<string, string> numStrings = new()
    {
      { "one", "1" },
      { "two", "2" },
      { "three", "3" },
      { "four", "4" },
      { "five", "5" },
      { "six", "6" },
      { "seven", "7" },
      { "eight", "8" },
      { "nine", "9" },
      { "1", "1" },
      { "2", "2" },
      { "3", "3" },
      { "4", "4" },
      { "5", "5" },
      { "6", "6" },
      { "7", "7" },
      { "8", "8" },
      { "9", "9" },
    };
        int counter = 1;
        foreach (string line in lines)
        {
            foreach (var key in numStrings.Keys)
            {
                List<int> indices = getIndicesOfNum(line, key);
                foreach (int index in indices) {
                  if (index != -1)
                  {
                      indicesAndValues.Add(new IndexValueObj { Index = index, Value = numStrings[key] });
                  }
                }
            }
            foreach (var key in indicesAndValues) {
            }
            Console.WriteLine(counter.ToString() + ' ' + getSum(indicesAndValues));
            counter++;
            total += getSum(indicesAndValues);
            indicesAndValues = [];
        }
        Console.WriteLine(total);
    }

  static int getSum(List<IndexValueObj> list) {
    list = [.. list.OrderBy(obj => obj.Index)];
    return int.Parse(list[0].Value + list[list.Count()-1].Value);
  }

  static List<int> getIndicesOfNum(string line, string num) {
    List<int> result = [];
    for (var i = 0; i < line.Length; i++) {
      if (line[i] == num[0]) {
        string currentSubstring = "";
        int j = i;
        while (j - i <= num.Length && j < line.Length) {
          currentSubstring+= line[j];
          if (currentSubstring == num) {
            result.Add(i);
          }
          j++;
        }
      }
    }
    return result;
  }
}

