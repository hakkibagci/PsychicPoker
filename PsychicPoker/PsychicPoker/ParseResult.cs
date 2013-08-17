using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PsychicPoker
{
    /// <summary>
    /// Stores the Hand and Deck objects resulting from the parse of an input line.
    /// </summary>
    class ParseResult
    {
        Hand hand;

        internal Hand Hand
        {
            get { return hand; }
        }

        Deck deck;

        internal Deck Deck
        {
            get { return deck; }
        }

        
        public ParseResult(Hand hand, Deck deck)
        {
            this.hand = hand;
            this.deck = deck;
        }
    }
}
