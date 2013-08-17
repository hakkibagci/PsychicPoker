using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PsychicPoker
{
    class Deck
    {
        Stack<Card> cards;

        public Deck()
        {
            this.cards = new Stack<Card>();
        }

        public Deck(Deck deck)
        {
            List<Card> cardList = deck.cards.ToList();
            cardList.Reverse();
            this.cards = new Stack<Card>(cardList);
          
        }
        
        public void AddCard(Card card)
        {
            this.cards.Push(card);
        }

        public Card GetNext()
        {
            return this.cards.Pop();
        }
    }
}
