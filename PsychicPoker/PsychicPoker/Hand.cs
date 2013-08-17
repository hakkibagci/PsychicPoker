using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PsychicPoker
{
    class Hand
    {
        List<Card> cardList;

        public Hand()
        {
            this.cardList = new List<Card>();
        }

        public Hand(Hand hand)
        {
            this.cardList = new List<Card>();
            this.cardList.AddRange(hand.cardList);
        }

        public void AddCard(Card card)
        {
            this.cardList.Add(card);
        }

        public void ReplaceCard(int index, Card newCard)
        {
            this.cardList[index] = newCard;
        }

        /// <summary>
        /// Calculates the hand value
        /// </summary>
        /// <returns></returns>
        public HandValue GetHandValue()
        {
            
            bool straight = true;
            bool flush = true;

            //sort the cards in the hands according to their face values
            this.cardList.Sort();

            //to find the number of cards for each face value
            Dictionary<FaceValue, int> kindCount = new Dictionary<FaceValue, int>();

            Card currentCard;

            for (int i = 0; i < this.cardList.Count; i++)
            {
                currentCard = this.cardList[i];

                //find the number of cards for each face value
                if (kindCount.ContainsKey(currentCard.Face))
                {
                    //existing face value, so increment the count
                    kindCount[currentCard.Face]++;
                }
                else
                {
                    //new face value, add a new entry
                    kindCount.Add(currentCard.Face, 1);
                }

                if (i < cardList.Count - 1)
                {
                    //check the straightness condition (also handle the special case T J Q K A in the second if)
                    if (currentCard.Face + 1 != cardList[i + 1].Face)
                        if (!(currentCard.Face == FaceValue.Ace && cardList[i + 1].Face == FaceValue.Ten))
                            straight = false;

                    //check the flush condition
                    if (currentCard.Suit != cardList[i + 1].Suit)
                        flush = false;
                }
            }

            //check the hand values in descending order
            if (straight && flush)
                return HandValue.straight_flush;

            //if there are at least 4 card with the same face value
            //then it is four-of-a-kind
            if (kindCount.Values.Max() > 3)
                return HandValue.four_of_a_kind;

            //if not four-of-a-kind and there are two group of cards the it is full-house
            if (kindCount.Count == 2)
                return HandValue.full_house;

            if (flush)
                return HandValue.flush;

            if (straight)
                return HandValue.straight;

            //if there are 3 cards from the same face value, it is three-of-a-kind
            if (kindCount.Values.Max() == 3)
                return HandValue.three_of_a_kind;

            //if there are 3 groups of cards it must be in the form of 2-2-1
            if (kindCount.Count == 3)
                return HandValue.two_pairs;

            //if there are 4 groups of cards it must be in the form of 2-1-1-1
            if (kindCount.Count == 4)
                return HandValue.one_pair;

            return HandValue.highest_card;
        }
    }
}
