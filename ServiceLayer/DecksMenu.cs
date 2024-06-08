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
    public class DecksMenu
    {
        private YuGiOhDbContext context;
        private DecksContext decksContext;
        private List<Deck> decks;
        private Menu decksMenu;
        public int SelectedIndex;
        public string[] Options =
            [
                "View Decklist",
                "Create deck",
                "Remove deck",
                "Back"
            ];
        private string prompt = @"
    ____            __   __    _      __ 
   / __ \___  _____/ /__/ /   (_)____/ /_
  / / / / _ \/ ___/ //_/ /   / / ___/ __/
 / /_/ /  __/ /__/ ,< / /___/ (__  ) /_  
/_____/\___/\___/_/|_/_____/_/____/\__/ 
";

        public DecksMenu()
        {
            context = new YuGiOhDbContext();
            decksContext = new DecksContext(context);
            decks = decksContext.ReadAll();
            decksMenu = new Menu(prompt, Options);
        }
        public void RunMenu()
        {
            do
            {
                SelectedIndex = decksMenu.Run();
                DecksMenuOptions();
            } while (SelectedIndex != Options.Length - 1);
        }
        public void DecksMenuOptions()
        {
            switch (SelectedIndex)
            {
                case 0:
                    ViewDecklist();
                    break;
                case 1:
                    AddDeck();
                    break;
                case 2:
                    RemoveDeck();
                    decks = decksContext.ReadAll();
                    context.ChangeTracker.Clear();
                    break;
                default:
                    break;
            }
        }
        private void ViewDecklist()
        {
            SearchDecks searchDecks = new SearchDecks(decks);
            searchDecks.Run();
        }
        private void AddDeck()
        {
            Console.Write("Enter deck name: ");
            string deckName = Console.ReadLine();
            Deck newDeck = new Deck(deckName);
            if (decks.Count == 0)
                newDeck.Id = 0;
            else
                newDeck.Id = decks[decks.Count() - 1].Id + 1;
            decksContext.Create(newDeck);
            decks.Add(newDeck);
        }
        private void RemoveDeck()
        {
            SearchDecks searchDecks = new SearchDecks(decks, true);
            searchDecks.Run();
        }
    }
}
