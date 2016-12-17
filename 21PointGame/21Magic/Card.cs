using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _21Magic
{
    /// <summary>
    /// 红桃、方块、黑桃、梅花
    /// </summary>
    enum Color { Heart,Diamond,Spade,Club};
    /// <summary>
    /// 卡牌
    /// </summary>
    class Card
    {
        private Color color;
        private int point;

        public Card(Color color, int point)
        {
            this.Color = color;
            this.Point = point;
        }

        public int Point
        {
            get
            {
                return point;
            }

            set
            {
                point = value;
            }
        }

        internal Color Color
        {
            get
            {
                return color;
            }

            set
            {
                color = value;
            }
        }
    }
}
