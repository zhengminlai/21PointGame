using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _21Magic
{
    /// <summary>
    /// 闲家赌局
    /// </summary>
    class Bet
    {
        /// <summary>
        /// 押注的钱
        /// </summary>
        private int betMoney;
        public Bet()
        {
            BetMoney = 100;
        }
        public Bet(int betMoney)
        {
            this.BetMoney = betMoney;
        }

        public int BetMoney
        {
            get
            {
                return betMoney;
            }

            set
            {
                betMoney = value;
            }
        }

        public void addBetMoney()
        {
            betMoney += 100;
        }
    }
}