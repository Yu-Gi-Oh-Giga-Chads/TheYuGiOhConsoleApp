using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PresentationLayer;
using DataLayer;
using BusinessLayer;

namespace ServiceLayer
{
    public class SearchBar
    {
        public delegate void SearchEventHandler(object sender, SearchEventArgs e);

        public event SearchEventHandler Search;
        private YuGiOhDbContext context;
        private CardsContext cardsContext;
        public List<Card> cards;
        public List<Card> allResults;
        public List<Card> results;
        public int selectedIndex;
        private bool withDeck = false;
        private Deck deck;
        private string prefix;
        private string prompt = @"
   ______      ____          __  _           
  / ____/___  / / /__  _____/ /_(_)___  ____ 
 / /   / __ \/ / / _ \/ ___/ __/ / __ \/ __ \
/ /___/ /_/ / / /  __/ /__/ /_/ / /_/ / / / /
\____/\____/_/_/\___/\___/\__/_/\____/_/ /_/ 
";
        public string Input { get; set; }
        public SearchBar()
        {
            context = new YuGiOhDbContext();
            cardsContext = new CardsContext(context);
            Input = "";
            cards = cardsContext.ReadAll();
            allResults = new List<Card>();
            results = new List<Card>();
        }
        public SearchBar(bool withDeck, Deck deck)
            : this()
        {
            this.withDeck = withDeck;
            this.deck = deck;
        }

        public void Run()
        {
            Console.Clear();
            Console.WriteLine(prompt);
            Console.WriteLine("Search for cards:");
            string input = Input;
            Console.WriteLine(Input);
            DisplayResults();
            Console.SetCursorPosition(Input.Length, 8);
            var key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.Backspace && input.Length > 0)
            {
                input = input.Substring(0, input.Length - 1);
                Console.Write("\b \b");
            }
            else if (key.Key == ConsoleKey.Enter && (Input.ToLower().Equals("exit") || Input.ToLower().Equals("back")))
            {
                Console.Clear();
                return;
            }
            else if (key.Key == ConsoleKey.DownArrow)
            {
                selectedIndex = Show();
            }
            else if (!char.IsControl(key.KeyChar))
            {
                input += key.KeyChar;
                Console.Write(key.KeyChar);
            }
            allResults.Clear();
            results.Clear();
            Input = input;
            OnSearch(new SearchEventArgs(input));
        }
        protected void GetResults()
        {
            int cardsCount = 15;
            if (allResults.Count < 15)
            {
                cardsCount = allResults.Count;
            }
            for (int i = 0; i < cardsCount; i++)
            {
                results.Add(allResults[i]);
            }
        }
        protected virtual void OnSearch(SearchEventArgs e)
        {
            foreach (var card in cards)
            {
                if (card.Name.Contains(e.Query))
                {
                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < e.Query.Length; i++)
                    {
                        sb.Append(card.Name[i]);
                    }
                    string part = sb.ToString();
                    if (Input.Equals(part))
                    {
                        allResults.Add(card);
                    }
                }
            }
            GetResults();
            selectedIndex = 0;
            Console.WriteLine();
            DisplayResults();
            Run();
        }
        private void DisplayResults()
        {
            for (int i = 0; i < results.Count; i++)
            {
                string currOption = results[i].Name;
                if (i == selectedIndex)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                if (withDeck)
                {
                    if (deck.Cards.Contains(results[i]))
                    {
                        prefix = deck.Copies[deck.Cards.IndexOf(results[i])] + "X ";
                    }
                    else
                    {
                        prefix = "   ";
                    }
                }
                Console.WriteLine($"{prefix}{currOption}");
            }
            Console.ResetColor();
        }
        public int Show()
        {
            bool enterPressed = true;
            ConsoleKey keyPressed;
            do
            {
                Console.Clear();
                Console.WriteLine(prompt);
                Console.WriteLine("Search for cards:");
                Console.WriteLine(Input);
                DisplayResults();
                Console.SetCursorPosition(Input.Length, 8);
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                keyPressed = keyInfo.Key;
                if (keyPressed == ConsoleKey.UpArrow)
                {
                    selectedIndex--;
                    if (selectedIndex < 0)
                    {
                        selectedIndex = 0;
                        enterPressed = false;
                        break;
                    }
                }
                else if (keyPressed == ConsoleKey.DownArrow)
                {
                    selectedIndex++;
                    if (selectedIndex == results.Count)
                    {
                        selectedIndex = 0;
                    }
                }
            } while (keyPressed != ConsoleKey.Enter);
            if (enterPressed)
            {
                if (withDeck)
                {
                    if (deck.Cards.Contains(results[selectedIndex]))
                    {
                        if (deck.Copies[deck.Cards.IndexOf(results[selectedIndex])] < 3)
                        {
                            deck.Copies[deck.Cards.IndexOf(results[selectedIndex])]++;
                        }
                    }
                    else
                    {
                        deck.Cards.Add(results[selectedIndex]);
                        deck.Copies.Add(1);
                    }
                }
                else
                {
                    DisplayCardMenu cardMenu = new DisplayCardMenu(results[selectedIndex]);
                    DisplayCardOptionsController cardController = new DisplayCardOptionsController(cardMenu);
                    int selected = 0;
                    do
                    {
                        cardMenu.RunMenu();
                        selected = cardController.CardOptions();
                    } while (selected != cardMenu.Options.Length - 1);
                }
            }
            return selectedIndex;
        }
    }
}
