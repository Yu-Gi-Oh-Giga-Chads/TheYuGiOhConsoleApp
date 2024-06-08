using BusinessLayer;
using DataLayer;
using Microsoft.EntityFrameworkCore;
using PresentationLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public class SearchDecks
    {
        public delegate void SearchEventHandler(object sender, SearchEventArgs e);

        private readonly YuGiOhDbContext context;
        private DecksContext decksContext;

        public event SearchEventHandler Search;
        public List<Deck> decks;
        public List<Deck> allResults;
        public List<Deck> results;
        private bool forRemove = false;
        public int selectedIndex;
        private string prompt = @"
    ____            __   __    _      __ 
   / __ \___  _____/ /__/ /   (_)____/ /_
  / / / / _ \/ ___/ //_/ /   / / ___/ __/
 / /_/ /  __/ /__/ ,< / /___/ (__  ) /_  
/_____/\___/\___/_/|_/_____/_/____/\__/ 
";
        public string Input { get; set; }
        private SearchDecks()
        {
            Input = "";
            decks = new List<Deck>();
            allResults = new List<Deck>();
            results = new List<Deck>();
        }
        public SearchDecks(List<Deck> decks)
            : this()
        {
            this.decks = decks;
        }
        public SearchDecks(List<Deck> decks, bool forRemove)
            : this(decks)
        {
            this.forRemove = forRemove;
            context = new YuGiOhDbContext();
            decksContext = new DecksContext(context);
        }

        public void Run()
        {
            Console.Clear();
            Console.WriteLine(prompt);
            Console.WriteLine("Search for decks:");
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
            int decksCount = 15;
            if (allResults.Count < 15)
            {
                decksCount = allResults.Count;
            }
            for (int i = 0; i < decksCount; i++)
            {
                results.Add(allResults[i]);
            }
            results = results.OrderBy(d => d.LastEdited).ToList();
        }
        protected virtual void OnSearch(SearchEventArgs e)
        {
            foreach (var deck in decks)
            {
                if (deck.Name.Contains(e.Query))
                {
                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < e.Query.Length; i++)
                    {
                        sb.Append(deck.Name[i]);
                    }
                    string part = sb.ToString();
                    if (Input.Equals(part))
                    {
                        allResults.Add(deck);
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
                Console.WriteLine(currOption);
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
                Console.WriteLine("Search for deck:");
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
                if (forRemove)
                {
                    decks.Remove(results[selectedIndex]);
                    decksContext.Delete(results[selectedIndex].Id);
                    context.Entry(results[selectedIndex]).State = EntityState.Deleted;
                }
                else
                {
                    DisplayDeckMenu deckMenu = new DisplayDeckMenu(results[selectedIndex]);
                    DisplayDeckOptionsController deckController = new DisplayDeckOptionsController(deckMenu);
                    int selected = 0;
                    do
                    {
                        selected = deckMenu.RunMenu();
                        deckController.DeckOptions();
                    } while (selected != deckMenu.options.Length - 1);
                }
            }
            return selectedIndex;
        }
    }
}
