using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PsychicPoker
{
    /// <summary>
    /// Creates the corresponding Hand and Deck objects given the strings representing the hand and deck respectively
    /// </summary>
    class InputParser
    {
        const int CardCount = 5;

        public static ParseResult Parse(string handString, string deckString)
        {

            Hand hand = CreateHand(handString);

            Deck deck = CreateDeck(deckString);

            return new ParseResult(hand, deck);
        }

        private static Hand CreateHand(string handString)
        {
            Hand hand = new Hand();

            string[] cardCodes = handString.Split(' ');

            for (int i = 0; i < CardCount; i++)
            {
                hand.AddCard(CreateCard(cardCodes[i]));
            }

            return hand;
        }

        private static Deck CreateDeck(string deckString)
        {
            Deck deck = new Deck();

            string[] cardCodes = deckString.Split(' ');

            //insert the cards into deck in reverse order
            for (int i = CardCount - 1; i >= 0; i--)
            {
                deck.AddCard(CreateCard(cardCodes[i]));
            }

            return deck;
        }

        private static Card CreateCard(string cardCode)
        {
            FaceValue face = GetFaceValue(cardCode[0]);
            Suit suit = GetSuit(cardCode[1]);

            return new Card(suit, face);
        }

        private static FaceValue GetFaceValue(char faceCode)
        {
            switch (faceCode)
            {
                case 'A':
                    return FaceValue.Ace;
                case 'T':
                    return FaceValue.Ten;
                case 'J':
                    return FaceValue.Jack;
                case 'Q':
                    return FaceValue.Queen;
                case 'K':
                    return FaceValue.King;
                default:
                    
                    FaceValue result; //A face value between [2-9]
                    if (Enum.TryParse<FaceValue>(faceCode.ToString(), out result))
                    {
                        return result;
                    }
                    else
                    {
                        throw new InvalidInputException("Invalid face-value code: ");
                    }
            }
        }

        private static Suit GetSuit(char suitCode)
        {
            switch (suitCode)
            {
                case 'C':
                    return Suit.Clubs;
                case 'D':
                    return Suit.Diamonds;
                case 'H':
                    return Suit.Hearts;
                case 'S':
                    return Suit.Spades;
                default:
                    throw new InvalidInputException("Invalid suit code: ");
            }
        }

    }
}
