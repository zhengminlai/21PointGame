using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _21Magic
{
    /// <summary>
    /// 庄家
    /// </summary>
    class Dealer:Person
    {
        public bool returnIsContinue()
        {
            return (this.returnTotalPoint() < 17);
        }
    }
}
