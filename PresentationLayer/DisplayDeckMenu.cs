using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer
{
    public class DisplayDeckMenu
    {
        public Deck deck;
        private string prompt = @"
 _______                       __              ______             ______          
/       \                     /  |            /      |           /      \         
$$$$$$$  |  ______    _______ $$ |   __       $$$$$$/  _______  /$$$$$$  |______  
$$ |  $$ | /      \  /       |$$ |  /  |        $$ |  /       \ $$ |_ $$//      \ 
$$ |  $$ |/$$$$$$  |/$$$$$$$/ $$ |_/$$/         $$ |  $$$$$$$  |$$   |  /$$$$$$  |
$$ |  $$ |$$    $$ |$$ |      $$   $$<          $$ |  $$ |  $$ |$$$$/   $$ |  $$ |
$$ |__$$ |$$$$$$$$/ $$ \_____ $$$$$$  \        _$$ |_ $$ |  $$ |$$ |    $$ \__$$ |
$$    $$/ $$       |$$       |$$ | $$  |      / $$   |$$ |  $$ |$$ |    $$    $$/ 
$$$$$$$/   $$$$$$$/  $$$$$$$/ $$/   $$/       $$$$$$/ $$/   $$/ $$/      $$$$$$/  
             
";
        public string[] options;
        public int selectedIndex;
        public Menu deckMenu;

        public DisplayDeckMenu(Deck deckV)
        {
            deck = deckV;
            deck.LastEdited = DateTime.Now;
            options =
                [
                    $"Id: {deck.Id}",
                    $"Name: {deck.Name}",
                    $"DeckList",
                    $"Add cards to deck",
                    $"Remove cards from deck",
                    $"Date created: {deck.DateCreated}",
                    $"Back"
                ];
            deckMenu = new Menu(prompt, options);

        }
        public int RunMenu()
        {
            selectedIndex = deckMenu.Run();
            return selectedIndex;
        }
    }
}
