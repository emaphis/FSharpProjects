 // Example 2-9 - defines several types inside of two namespaces.

namespace PlayingCards

// PlayingCards.Suit
type Suit =
    | Spade
    | Club
    | Diamond
    | Heart

// PlayingCards.PlayingCard
type PlayingCard =
    | Ace of Suit
    | King of Suit
    | Queen of Suit
    | Jack of Suit
    | ValueCard of int * Suit



namespace PlayingCards.Poker

// PlayingCard.Poker.PokerPlayer
type PokerPlayer = {
    Name : string
    Money : int
    Position : int
}
