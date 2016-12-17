using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _21Magic
{
    /// <summary>
    /// 手牌类
    /// </summary>
    class Hand
    {
        private List<Card> cardList;
        public Hand()
        {
            cardList = new List<Card>();
        }
        #region getters&setters
        internal List<Card> CardList
        {
            get
            {
                return cardList;
            }

            set
            {
                cardList = value;
            }
        }
        #endregion

        /// <summary>
        /// 计算手牌点数
        /// </summary>
        /// <returns></returns>
        public int countTotalPoint()
        {
            int totalPointCount = 0;
            foreach (Card card in cardList)
            {
                int point = card.Point;
                if (card.Point > 10)
                    point = 10;
                totalPointCount += point;
            }
            //对A进行处理
            foreach (Card card in cardList)
            {
                if(card.Point==1)
                {
                    if (totalPointCount + 10 <= 21)
                        totalPointCount += 10;
                }
            }
            return totalPointCount;
        }

        public void addDealedCard(Card card)
        {
            cardList.Add(card);
        }

        public int returnCardNum()
        {
            return CardList.Count;
        }
    }
}