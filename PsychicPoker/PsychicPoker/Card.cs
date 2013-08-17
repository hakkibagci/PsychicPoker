using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PsychicPoker
{
    /// <summary>
    /// Represents a card, provides a mechanism to be compared 
    /// with other cards by implementing IComparable interface.
    /// </summary>
    class Card:IComparable<Card>
    {
        Suit suit;

        internal Suit Suit
        {
            get { return suit; }
        }

        FaceValue face;

        internal FaceValue Face
        {
            get { return face; }
        }

        public Card(Suit suit, FaceValue face)
        {
            this.suit = suit;
            this.face = face;
        }

    
        public int CompareTo(Card other)
        {
            if (this.face > other.face)
                return 1;
            if (this.face < other.face)
                return -1;
            return 0;
        }
    }
}
