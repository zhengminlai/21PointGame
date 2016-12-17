using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _21Magic
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int MAXPLAYER = 4;
        private const int MAXCARD = 11;
        private int playerNo = 0;
        private GameRoom gameRoom;
        private Image[,] imageArray = new Image[MAXPLAYER,MAXCARD];
        private Point[] imageInitializePosition = new Point[MAXPLAYER];
        private bool gameFinish = false;
        private bool isFirstStart = true;
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// cardName卡牌名字如下
        /// heitao、hongtao、caohua、fangkuai
        /// 加上1、2、3、4、5、6、7、8、9、10、j、q、k
        /// 如heitaoj
        /// playerNumber = 0~max
        /// cardNumber = 1~max
        /// </summary>
        /// <param name="playerNumber"></param>
        /// <param name="cardNumber"></param>
        /// <param name="cardName"></param>
        public void ChangeCardImage(int playerNumber, int cardNumber, string cardName)
        {
            if (playerNumber >= 0 && playerNumber < MAXPLAYER && cardNumber>0 && cardNumber<MAXCARD)
            {
                if (imageArray[playerNumber, cardNumber] != null)
                {
                    string card = "../resource/" + cardName + ".jpg";
                    Uri baseUri = new Uri(System.Environment.CurrentDirectory);
                    imageArray[playerNumber, cardNumber].Source = new BitmapImage(new Uri(baseUri, card));
                    imageArray[playerNumber, cardNumber].Visibility = Visibility.Visible;
                }
            }
        }
        public void Initialize()
        {
            for (int i = 0; i < MAXPLAYER; i++)
            {
                for (int j = 1; j < MAXCARD; j++)
                {
                    string name = "imageNpc";
                    name += i;
                    name += j;
                    imageArray[i, j] = (Image)FindName(name);
                    if (imageArray[i, j] != null)
                    {
                        imageArray[i, j].Visibility = Visibility.Hidden;
                    }
                }
            }
            if (!isFirstStart)
            {
                int[] playersMoney = getPlayersMoney();
                gameRoom = new GameRoom();
                playerNo = 0;
                gameFinish = false;
                for (int i = 0; i < gameRoom.Players.Length; i++)
                {
                    gameRoom.Players[i].Money = playersMoney[i];
                }
            }
            else
            {
                gameRoom = new GameRoom();
                playerNo = 0;
                gameFinish = false;
                isFirstStart = false;
            }
        }
        /// <summary>
        /// 拿到几个玩家目前的本钱
        /// </summary>
        /// <returns></returns>
        private int[] getPlayersMoney()
        {
            int[] a = new int[gameRoom.Players.Length];
            for (int i = 0; i < a.Length; i++)
            {
                a[i] = gameRoom.Players[i].Money;
            }
            return a;
        }
        private void ButtonStart_Click(object sender, RoutedEventArgs e)
        {
            Initialize();
            MoneyChange();
            buttonAddMoney.Visibility = Visibility.Visible;
        }
        /// <summary>
        /// 要牌
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonGetCard_Click(object sender, RoutedEventArgs e)
        {
            buttonAddMoney.Visibility = Visibility.Hidden;
            if (!gameFinish)
            {
                if (gameRoom.Players[playerNo].Money <= 0)
                {
                    MessageBox.Show("你的本钱已经输完了哦0.0,无法参与游戏=-=");
                    MessageBox.Show("切换下一个玩家");
                }
                else
                {
                    Card card = gameRoom.dealOneCardToPlayer(playerNo);
                    string cardString = CardChange(card.Color, card.Point);
                    ChangeCardImage(playerNo, gameRoom.GetPlayerCardNumbers(playerNo), cardString);
                    MoneyChange();
                    if (gameRoom.IsPlayerOut(playerNo))
                    {
                        MessageBox.Show("你的点数超过21，你输了哦0.0");
                        buttonAddMoney.Visibility = Visibility.Visible;
                        playerNo++;
                        if (playerNo != MAXPLAYER - 1)
                        {
                            MessageBox.Show("切换下一个玩家");
                        }
                        else
                        {
                            DealerAction();
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 不要牌
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonDontGetCard_Click(object sender, RoutedEventArgs e)
        {
            buttonAddMoney.Visibility=Visibility.Visible;
            if (!gameFinish)
            {
                playerNo++;
                if (playerNo != MAXPLAYER - 1)
                {
                    MessageBox.Show("切换下一个玩家");
                }
                else
                {
                    DealerAction();
                }
            }
        }
        /// <summary>
        /// 押注
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonAddMoney_Click(object sender, RoutedEventArgs e)
        {
            if (!gameFinish)
            {
                if ((100 + gameRoom.Players[playerNo].Bet.BetMoney) > gameRoom.Players[playerNo].Money)
                {
                    MessageBox.Show("余额不足，无法加倍押注！");
                }
                else
                {
                    gameRoom.Players[playerNo].Bet.addBetMoney();
                }
            }
            MoneyChange();
        }
        /// <summary>
        /// 计算手牌点数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonCalculateScore_Click(object sender, RoutedEventArgs e)
        {
            if (!gameFinish)
            {
                MessageBox.Show("当前玩家手牌点数："+ gameRoom.returnPlayerTotalPoint(playerNo).ToString());
            }
        }
        /// <summary>
        /// 根据花色和点数显示对应的牌
        /// </summary>
        /// <param name="color"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        private string CardChange(Color color, int point)
        {
            string card = "";
            switch (color)
            {
                case Color.Heart:
                    card = "hongtao";
                    break;
                case Color.Diamond:
                    card = "fangkuai";
                    break;
                case Color.Club:
                    card = "caohua";
                    break;
                case Color.Spade:
                    card = "heitao";
                    break;
                default:
                    break;
            }
            switch (point)
            {
                case 11:
                    card += "j";
                    break;
                case 12:
                    card += "q";
                    break;
                case 13:
                    card += "k";
                    break;
                default:
                    card += point;
                    break;
            }
            return card;
        }
        /// <summary>
        /// 庄家开始行动
        /// </summary>
        public void DealerAction()
        {
            MessageBox.Show("庄家开始行动！");
            buttonAddMoney.Visibility = Visibility.Hidden;
            Card card = gameRoom.dealOneCardToDealer();
            string cardString = CardChange(card.Color, card.Point);
            ChangeCardImage(playerNo, gameRoom.GetDealerCardNumbers(), cardString);
            while (gameRoom.isDealerContinue())
            {
                card = gameRoom.dealOneCardToDealer();
                cardString = CardChange(card.Color, card.Point);
                ChangeCardImage(playerNo, gameRoom.GetDealerCardNumbers(), cardString);
                if (gameRoom.IsDealerOut())
                {
                    gameFinish = true;
                    break;
                }
            }
            int dealerPoint = gameRoom.returnDealerTotalCount();
            MessageBox.Show("庄家的点数："+ dealerPoint.ToString());
            string resultInfo = "胜负情况：\n";
            for (int i=0;i<gameRoom.Players.Length;i++)
            {
                int player_i_point = gameRoom.Players[i].returnTotalPoint();
                string playerName = gameRoom.Players[i].Name.ToString();
                string playerVictoryMsg = "\t庄家Karn：" + dealerPoint.ToString() + "点，玩家" + playerName + "：" + player_i_point
                                          + "点，玩家" + playerName + "胜，且赢得" + gameRoom.Players[i].Bet.BetMoney + "元！\n";
                string dealerVictoryMsg = "\t庄家Karn：" + dealerPoint.ToString() + "点，玩家" + playerName + "：" + player_i_point
                                          + "点，玩家" + playerName + "败，且输掉" + gameRoom.Players[i].Bet.BetMoney + "元！\n";
                if (dealerPoint > 21)
                {
                    if (player_i_point > 21)
                    {
                        resultInfo += dealerVictoryMsg;
                        gameRoom.Players[i].Money -= gameRoom.Players[i].Bet.BetMoney;
                    }
                    else
                    {
                        resultInfo += playerVictoryMsg;
                        gameRoom.Players[i].Money += gameRoom.Players[i].Bet.BetMoney;
                    }
                }
                else
                {
                    if (player_i_point > 21)
                    {
                        resultInfo += dealerVictoryMsg;
                        gameRoom.Players[i].Money -= gameRoom.Players[i].Bet.BetMoney;
                    }
                    else if (player_i_point > dealerPoint)
                    {
                        resultInfo += playerVictoryMsg;
                        gameRoom.Players[i].Money += gameRoom.Players[i].Bet.BetMoney;
                    }
                    else
                    {
                        resultInfo += dealerVictoryMsg;
                        gameRoom.Players[i].Money -= gameRoom.Players[i].Bet.BetMoney;
                    }
                }
                gameRoom.Players[i].Bet.BetMoney = 100;
            }
            gameFinish = true;
            MessageBox.Show(resultInfo);
            MoneyChange();
        }
        /// <summary>
        /// 显示玩家本钱和押注情况
        /// </summary>
        public void MoneyChange()
        { 
            labelmoney1.Content = "本钱:" + gameRoom.Players[0].Money;
            labelmoney2.Content = "本钱:" + gameRoom.Players[1].Money;
            labelmoney3.Content = "本钱:" + gameRoom.Players[2].Money;
            labelmoney1X.Content = "押注:" + gameRoom.Players[0].Bet.BetMoney;
            labelmoney2X.Content = "押注:" + gameRoom.Players[1].Bet.BetMoney;
            labelmoney3X.Content = "押注:" + gameRoom.Players[2].Bet.BetMoney;
            labelpoint1.Content = "点数:" + gameRoom.returnPlayerTotalPoint(0);
            labelpoint2.Content = "点数:" + gameRoom.returnPlayerTotalPoint(1);
            labelpoint3.Content = "点数:" + gameRoom.returnPlayerTotalPoint(2);
        }
    }
}