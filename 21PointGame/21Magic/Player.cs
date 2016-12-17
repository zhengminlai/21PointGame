using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _21Magic
{
    /// <summary>
    /// 闲家
    /// </summary>
    class Player:Person
    {
        private Bet bet;
        private int money;
        public Player()
        {
            Bet = new Bet();
            Money = 10000;
        }

        public int Money
        {
            get
            {
                return money;
            }

            set
            {
                money = value;
            }
        }

        internal Bet Bet
        {
            get
            {
                return bet;
            }

            set
            {
                bet = value;
            }
        }
    }
}