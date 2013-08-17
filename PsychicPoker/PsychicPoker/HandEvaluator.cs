using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PsychicPoker
{
    class HandEvaluator
    {
        public static string FindBestHandValue(string inputLine)
        {
             string[] splits = inputLine.Split(' ');

             //validate the length of the input lines and number of card codes
             if (inputLine.Length != 29 || splits.Length != 10)
             {
                 throw new InvalidInputException("Invalid input line: " + inputLine);
             }
             else
             {
                 ParseResult parseResult;
                 string handString;
                 string deckString;

                 try
                 {
                     int halfLength = inputLine.Length / 2;

                     //get the card codes representing the hand
                     handString = inputLine.Substring(0, halfLength);

                     //get the card codes representing the deck
                     deckString = inputLine.Remove(0, halfLength + 1);

                     //parse the card codes and create corresponding Hand and Deck objects
                     parseResult = InputParser.Parse(handString, deckString);
                 }
                 catch(InvalidInputException ex)
                 {
                     //this exception is thrown by our own classes to indicate an invalid input
                     throw new InvalidInputException(ex.Message + inputLine);
                 }
                 catch
                 {
                     //any other unknown exception that may occur during input parsing
                     throw new InvalidInputException("Invalid input line: " + inputLine);
                 }

                 //this will be used to enumarate all possible (32) card combinations
                 int enumerator = 0;

                 //this is the temporary value of enumerator used in the loop
                 int temp;

                 Hand hand;
                 Deck deck;

                 //represents the best hand value
                 HandValue result = HandValue.highest_card;

                 HandValue current;

                 //in this loop we enumerate and try all possible hands
                 //to do this we use the binary representation of the numbers from 0 to 31
                 //00000 means no card is replaced (no card drawn from the deck)
                 //00001 means first card of the hand is replaced
                 //10001 means first and last cards of the hand is replaced and so on.
                 while (enumerator < 32)
                 {
                     temp = enumerator;

                     hand = new Hand(parseResult.Hand);
                     deck = new Deck(parseResult.Deck);

                     for (int i = 0; i < 5; i++)
                     {
                         //determine if the current card will be replaced
                         //if the last bit is 1, then the card in the current index is replaced
                         if ((temp & 1) == 1)
                         {
                             hand.ReplaceCard(i, deck.GetNext());
                         }

                         //we are done with this bit, so drop it
                         temp = temp >> 1;
                     }

                     //calculate the hand value
                     current = hand.GetHandValue();

                     if (current > result)
                         result = current;

                     //if it is a straight flush, no need to check other possible hands
                     if (result == HandValue.straight_flush)
                         break;

                     //advance to the next hand
                     enumerator++;
                 }

                 return "Hand: "+handString+ " Deck:" +deckString+ " Best hand: "+result.ToString();
             }
            
            
        }
    }
}
