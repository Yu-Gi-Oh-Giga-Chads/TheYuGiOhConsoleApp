using BusinessLayer;
using DataLayer;
using PresentationLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public class DisplayDeckOptionsController
    {
        private YuGiOhDbContext context;
        private CardsContext cardsContext;
        private List<Card> cards;
        private DisplayDeckMenu deckMenu;
        public DisplayDeckOptionsController(DisplayDeckMenu deckMenu)
        {
            this.deckMenu = deckMenu;
            context = new YuGiOhDbContext();
            cardsContext = new CardsContext(context);
            cards = cardsContext.ReadAll();
        }
        public int DeckOptions()
        {
            int selectedIndex = deckMenu.selectedIndex;
            switch (selectedIndex)
            {
                case 2:
                    ShowDeckList();
                    break;
                case 3:
                    AddCard();
                    break;
                case 4:
                    RemoveCard();
                    break;
                default:
                    break;
            }
            return selectedIndex;
        }
        public void ShowDeckList()
        {
            while (true)
            {
                cards = deckMenu.deck.Cards;
                string[] options = new string[cards.Count + 1];
                for (int i = 0; i < cards.Count; i++)
                {
                    options[i] = deckMenu.deck.Copies[i] + "X " + cards[i].Name;
                }
                options[cards.Count] = "Back";
                Menu deckList = new Menu("", options);
                int selectedIndex = deckList.Run();
                if (selectedIndex == options.Length - 1)
                {
                    break;
                }
                else
                {
                    DisplayCardMenu cardMenu = new DisplayCardMenu(cards[selectedIndex]);
                    DisplayCardOptionsController cardMenuController = new DisplayCardOptionsController(cardMenu);
                    do
                    {
                        cardMenu.RunMenu();
                        cardMenuController.CardOptions();
                    } while (cardMenu.SelectedIndex != cardMenu.Options.Length - 1);
                }
            }
        }
        public void AddCard()
        {
            SearchBar addCards = new SearchBar(true, deckMenu.deck);
            addCards.Run();
        }
        public void RemoveCard()
        {
            while (true)
            {
                List<Card> cards = deckMenu.deck.Cards;
                string[] options = new string[cards.Count + 1];
                for (int i = 0; i < cards.Count; i++)
                {
                    options[i] = deckMenu.deck.Copies[i] + "X " + cards[i].Name;
                }
                options[cards.Count] = "Back";
                Menu deckList = new Menu("", options);
                int selectedIndex = deckList.Run();
                if (selectedIndex == options.Length - 1)
                {
                    break;
                }
                else
                {
                    deckMenu.deck.Copies[selectedIndex]--;
                    if (deckMenu.deck.Copies[selectedIndex] == 0)
                    {
                        deckMenu.deck.Cards.RemoveAt(selectedIndex);
                        deckMenu.deck.Copies.RemoveAt(selectedIndex);
                    }
                }
            }
        }
    }
}
