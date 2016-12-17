using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _21Magic
{
    class Person
    {
        private Hand hand;
        private string name;
        public Person()
        {
            hand = new Hand();
        }

        internal string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        internal Hand Hand
        {
            get
            {
                return hand;
            }

            set
            {
                hand = value;
            }
        }

        public void getCard(Card card)
        {
            Hand.addDealedCard(card);
        }
        public int returnTotalPoint()
        {
            return Hand.countTotalPoint();
        }
        public int returnTotalCardNumInHand()
        {
            return Hand.returnCardNum();
        }
    }
}