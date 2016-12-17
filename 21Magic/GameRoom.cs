using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _21Magic
{
    /// <summary>
    /// 游戏室
    /// </summary>
    class GameRoom
    {
        private Player[] players;
        private Dealer dealer;
        private Deck deck;
        /// <summary>
        /// 初始化游戏大厅
        /// </summary>
        public GameRoom()
        {
            players = new Player[3];
            for (int i = 0; i < players.Length; i++)
            {
                players[i] = new Player();
                switch (i)
                {
                    case 0 :
                        players[i].Name = "Prinny";
                        break;
                    case 1:
                        players[i].Name = "Jack";
                        break;
                    case 2:
                        players[i].Name = "Kinkin";
                        break;
                }
            }
            
            dealer = new Dealer();
            dealer.Name = "Karn";
            deck = new Deck();
        }
        /// <summary>
        /// 发一张牌给闲家
        /// </summary>
        /// <param name="i">第i个闲家</param>
        /// <returns></returns>
        public Card dealOneCardToPlayer(int i)
        {
            Card card = deck.dealCard();
            players[i].getCard(card);
            return card;
        }
        /// <summary>
        /// 返回闲家的总点数
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public int returnPlayerTotalPoint(int i)
        {
           return players[i].returnTotalPoint();
        }
        /// <summary>
        /// 给庄家发一张牌
        /// </summary>
        /// <returns></returns>
        public Card dealOneCardToDealer()
        {
            Card card = deck.dealCard();
            dealer.getCard(card);
            return card;
        }
        /// <summary>
        /// 返回庄家是否继续要牌
        /// </summary>
        /// <returns>true 代表继续要牌</returns>
        public bool isDealerContinue()
        {
            return dealer.returnIsContinue();
        }
        /// <summary>
        /// 返回庄家手牌点数
        /// </summary>
        /// <returns></returns>
        public int returnDealerTotalCount()
        {
            return dealer.returnTotalPoint();
        }
        /// <summary>
        /// 返回玩家手中的牌数
        /// </summary>
        /// <param name="No"></param>
        /// <returns></returns>
        public int GetPlayerCardNumbers(int No)
        {
            return players[No].returnTotalCardNumInHand();
        }

        public int GetDealerCardNumbers()
        {
            return dealer.returnTotalCardNumInHand();
        }

        public bool IsPlayerOut(int playerNo)
        {
            if (players[playerNo].returnTotalPoint() > 21)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool IsDealerOut()
        {
            if (dealer.returnTotalPoint() > 21)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
        #region getters&setters
        internal Player[] Players
        {
            get
            {
                return players;
            }

            set
            {
                players = value;
            }
        }
        internal Dealer Dealer
        {
            get
            {
                return dealer;
            }

            set
            {
                dealer = value;
            }
        }
        internal Deck Deck
        {
            get
            {
                return deck;
            }

            set
            {
                deck = value;
            }
        }
        #endregion 
    }
}