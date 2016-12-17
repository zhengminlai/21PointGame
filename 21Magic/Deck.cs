using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _21Magic
{
    /// <summary>
    /// 一副牌
    /// </summary>
    class Deck
    {
        private Card[] deckCards;
        private int currentIndex;
        internal Card[] DeckCards
        {
            get
            {
                return deckCards;
            }

            set
            {
                deckCards = value;
            }
        }

        public int CurrentIndex
        {
            get
            {
                return currentIndex;
            }

            set
            {
                currentIndex = value;
            }
        }

        public Deck()
        {
            DeckCards = new Card[52];
            int count = 0;
            currentIndex = 0;
            //把牌放到数组中
            for (int i = 0; i < 4; i++)
                for (int j = 1; j <= 13; j++)
                {
                    Color cardColor=getColorByNum(i);
                    DeckCards[count] = new Card(cardColor, j);
                    count++;
                }
            //洗牌
            shuffle();
        }
        
        public Color getColorByNum(int i)
        {
            Color cardColor = Color.Heart;
            switch (i)
            {
                case 0:
                    cardColor = Color.Heart;
                    break;
                case 1:
                    cardColor = Color.Diamond;
                    break;
                case 2:
                    cardColor = Color.Spade;
                    break;
                case 3:
                    cardColor = Color.Club;
                    break;
                default: break;
            }
            return cardColor;
        }
        /// <summary>
        /// 发牌
        /// </summary>
        /// <returns>Card</returns>
        public Card dealCard()
        {
            if (currentIndex >= 52)
            {
                return null;
            }
            Card card = new Card(DeckCards[currentIndex].Color, DeckCards[currentIndex].Point);
            currentIndex++;
            return card;
        }
        /// <summary>
        /// 生成随机种子
        /// </summary>
        /// <returns>种子</returns>
        public static int GetRandomSeed()
        {
            byte[] bytes = new byte[4];
            System.Security.Cryptography.RNGCryptoServiceProvider rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            rng.GetBytes(bytes);
            return BitConverter.ToInt32(bytes, 0);
        }
        /// <summary>
        /// 洗牌.
        /// </summary>
        public void shuffle()
        {
            for (int i = 0; i < 52; i++)
            {
                Random r = new Random(GetRandomSeed());
                int randNum = r.Next(0, DeckCards.Length);

                Random r1 = new Random(GetRandomSeed());
                int randNum_1 = r1.Next(0, DeckCards.Length);
                //选好两个随机下标，然后将它们对应的牌进行交换
                Card tmp = new Card(DeckCards[randNum].Color, DeckCards[randNum].Point);
                Card tmp_1 = new Card(DeckCards[randNum_1].Color, DeckCards[randNum_1].Point);
                DeckCards[randNum] = tmp_1;
                DeckCards[randNum_1] = tmp;
            }
        }
    }
}