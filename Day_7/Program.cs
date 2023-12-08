
// organize inputs by type

using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.ExceptionServices;

// Part 1
// class Program {

//   static Dictionary<char, int> cardScore = new(){
//       {'A',13},
//       {'K',12},
//       {'Q',11},
//       {'J',10},
//       {'T',9},
//       {'9',8},
//       {'8',7},
//       {'7',6},
//       {'6',5},
//       {'5',4},
//       {'4',3},
//       {'3',2},
//       {'2',1}
//      };

//   readonly struct CardsAndBid {
//     public CardsAndBid(string cards, int bid)
//       {
//           Bid = bid;
//           // Cards = Sort(cards, cardCount);
//           Cards = cards;
//       }
//     // private static string Sort(string cards,  Dictionary<char,int> cardCount) {
//     //   char[] sortedCards = [.. cards.OrderBy(card => -cardCount[card]).ThenBy(card => -cardScore[card])];
//     //   return new string(sortedCards);
//     // }
//     public string Cards {get;}
//     public int Bid {get;}
//   }

//   static void Main() {

//     List<CardsAndBid> fiveOfAKind = [];
//     List<CardsAndBid> fourOfAKind = [];
//     List<CardsAndBid> fullHouse = [];
//     List<CardsAndBid> threeOfAKind = [];
//     List<CardsAndBid> twoPair = [];
//     List<CardsAndBid> onePair = [];
//     List<CardsAndBid> highCard = [];

//     string[] lines = File.ReadAllLines("./input.txt");
//     foreach (string line in lines) {
//       string[] splitLine = line.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
//       int bid = int.Parse(splitLine[1]);
//       string cards = splitLine[0];
//       Dictionary<char, int> cardCounter = [];
//       foreach (char card in cards) {
//         if (cardCounter.TryGetValue(card, out int currentCount)) {
//           cardCounter[card]++;
//         } else {
//           cardCounter[card] = 1;
//         }
//       }
//       // five of a kind
//       if (cardCounter.Count == 1) {
//         fiveOfAKind.Add(new CardsAndBid(cards, bid));
//       }
//       // four of a kind
//       else if (cardCounter.Count == 2 && cardCounter.Values.Max() == 4) {
//         fourOfAKind.Add(new CardsAndBid(cards, bid));
//       }
//       // full house
//       else if (cardCounter.Count == 2 && cardCounter.Values.Max() == 3) {
//         fullHouse.Add(new CardsAndBid(cards, bid));
//       }
//       // three of a kind
//       else if (cardCounter.Count == 3 && cardCounter.Values.Max() == 3) {
//         threeOfAKind.Add(new CardsAndBid(cards, bid));
//       }
//       // two pair
//       else if (cardCounter.Count == 3 && cardCounter.Values.Max() == 2) {
//         twoPair.Add(new CardsAndBid(cards, bid));
//       }
//       // one pair
//       else if (cardCounter.Count == 4) {
//         onePair.Add(new CardsAndBid(cards, bid));
//       }
//       // high Card
//       else {
//         highCard.Add(new CardsAndBid(cards, bid));
//       }
//     }
//     // sort each group
//     // List<CardsAndBid>[] TypesOfHands = [fiveOfAKind, fourOfAKind, fullHouse, threeOfAKind, twoPair, onePair, highCard];
//     List<CardsAndBid>[] TypesOfHands = [highCard, onePair, twoPair, threeOfAKind, fullHouse, fourOfAKind, fiveOfAKind];

//     for (var i = 0; i < TypesOfHands.Length; i++) {
//       TypesOfHands[i] = [.. TypesOfHands[i].OrderBy(hand => cardScore[hand.Cards[0]]).ThenBy(hand => cardScore[hand.Cards[1]]).ThenBy(hand => cardScore[hand.Cards[2]]).ThenBy(hand => cardScore[hand.Cards[3]]).ThenBy(hand => cardScore[hand.Cards[4]])];
//     }

//     int sum = 0;
//     int count = 1;

//     foreach (List<CardsAndBid> type in TypesOfHands) {
//       foreach (CardsAndBid hand in type) {
//         sum += hand.Bid * count;
//         count++;
//       }
//     }
//     Console.WriteLine(sum);
//   }
// }

// Part 2
class Program {

  static Dictionary<char, int> cardScore = new(){
      {'A',13},
      {'K',12},
      {'Q',11},
      {'J',0},
      {'T',9},
      {'9',8},
      {'8',7},
      {'7',6},
      {'6',5},
      {'5',4},
      {'4',3},
      {'3',2},
      {'2',1}
     };

  readonly struct CardsAndBid {
    public CardsAndBid(string cards, int bid)
      {
          Bid = bid;
          Cards = cards;
      }
    public string Cards {get;}
    public int Bid {get;}
  }

  static void Main() {

    List<CardsAndBid> fiveOfAKind = [];
    List<CardsAndBid> fourOfAKind = [];
    List<CardsAndBid> fullHouse = [];
    List<CardsAndBid> threeOfAKind = [];
    List<CardsAndBid> twoPair = [];
    List<CardsAndBid> onePair = [];
    List<CardsAndBid> highCard = [];

    string[] lines = File.ReadAllLines("./input.txt");
    foreach (string line in lines) {
      string[] splitLine = line.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
      int bid = int.Parse(splitLine[1]);
      string cards = splitLine[0];
      Dictionary<char, int> cardCounter = [];
      int jokerCount = 0;
      foreach (char card in cards) {
        if (card == 'J') {
          jokerCount++;
        } else if (cardCounter.TryGetValue(card, out int currentCount)) {
          cardCounter[card]++;
        } else {
          cardCounter[card] = 1;
        }
      }
      //add joker count to highest number of highest card
      if (jokerCount < 5) {
        cardCounter[getJokerTarget(cardCounter)] += jokerCount;
      } else {
        cardCounter['J'] = 5;
      }
      // five of a kind
      if (cardCounter.Count == 1) {
        fiveOfAKind.Add(new CardsAndBid(cards, bid));
      }
      // four of a kind
      else if (cardCounter.Count == 2 && cardCounter.Values.Max() == 4) {
        fourOfAKind.Add(new CardsAndBid(cards, bid));
      }
      // full house
      else if (cardCounter.Count == 2 && cardCounter.Values.Max() == 3) {
        fullHouse.Add(new CardsAndBid(cards, bid));
      }
      // three of a kind
      else if (cardCounter.Count == 3 && cardCounter.Values.Max() == 3) {
        threeOfAKind.Add(new CardsAndBid(cards, bid));
      }
      // two pair
      else if (cardCounter.Count == 3 && cardCounter.Values.Max() == 2) {
        twoPair.Add(new CardsAndBid(cards, bid));
      }
      // one pair
      else if (cardCounter.Count == 4) {
        onePair.Add(new CardsAndBid(cards, bid));
      }
      // high Card
      else {
        highCard.Add(new CardsAndBid(cards, bid));
      }
    }
    // sort each group
    // List<CardsAndBid>[] TypesOfHands = [fiveOfAKind, fourOfAKind, fullHouse, threeOfAKind, twoPair, onePair, highCard];
    List<CardsAndBid>[] TypesOfHands = [highCard, onePair, twoPair, threeOfAKind, fullHouse, fourOfAKind, fiveOfAKind];
    for (var i = 0; i < TypesOfHands.Length; i++) {
      TypesOfHands[i] = [.. TypesOfHands[i].OrderBy(hand => cardScore[hand.Cards[0]]).ThenBy(hand => cardScore[hand.Cards[1]]).ThenBy(hand => cardScore[hand.Cards[2]]).ThenBy(hand => cardScore[hand.Cards[3]]).ThenBy(hand => cardScore[hand.Cards[4]])];
    }

    int sum = 0;
    int count = 1;

    foreach (List<CardsAndBid> type in TypesOfHands) {
      foreach (CardsAndBid hand in type) {
        sum += hand.Bid * count;
        count++;
      }
    }
    Console.WriteLine(sum);
  }
  static char getJokerTarget(Dictionary<char, int> cardCount) {
    int highestCount = cardCount.Values.Max();
    char target = 'J';
    foreach (char card in cardCount.Keys) {
      if (cardCount[card] == highestCount && cardScore[card] > cardScore[target]) {
        target = card;
      }
    }
    return target;
  }
}