using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Data.Linq;

namespace CardPlayer
{
    public partial class _Default : System.Web.UI.Page
    {
        List<string> hands = new List<string>();
        int cardsInHand = 5;
        protected void Page_Load(object sender, EventArgs e)
        {
            List<Cards.Sproc_DeckResult> cards = new List<Cards.Sproc_DeckResult>();
            List<Cards.Sproc_DeckResult> hand = new List<Cards.Sproc_DeckResult>();

            using (Cards.DataBase db = new Cards.DataBase("Data Source=delllaptop;Initial Catalog=cards;Integrated Security=SSPI;"))
            {
                cards = db.Sproc_Deck(1).ToList();
            }
            //cards = new List<Cards.Sproc_DeckResult>();
            //for (int suits = 0; suits < cardsInHand; suits++)
            //{
            //    for (int c = 0; c < cardsInHand; c++)
            //    {
            //        cards.Add(new Cards.Sproc_DeckResult
            //        {
            //            CardTypeID = c.ToString(),
            //            SuitTypeID = suits.ToString(),
            //            SuitValue = suits,
            //            Value = c
            //        });
            //    }
            //}
            DateTime start = DateTime.Now;
            RecursiveLoop(hand, cards);
            double finish = DateTime.Now.Subtract(start).TotalMilliseconds;
            string a = "";
        }
        private void RecursiveLoop(List<Cards.Sproc_DeckResult> hand, List<Cards.Sproc_DeckResult> allCards)
        {
            foreach (Cards.Sproc_DeckResult card in allCards)
            {
                hand.Add(card);
                if (hand.Count == cardsInHand)
                {
                    string t = "";
                    foreach (Cards.Sproc_DeckResult c in hand)
                    {
                        t += " " + c.CardTypeID + c.SuitTypeID;
                    }
                    hands.Add(t);
                }
                else
                {
                    RecursiveLoop(hand, allCards.Where(ac => !hand.Contains(ac)).OrderBy(ac => ac.Value).OrderBy(ac => ac.SuitValue).ToList());
                }
                hand.Remove(card);
            }
        }

    }
}
