using PresentationLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public class MainController
    {
        private StartingMenu startingMenu;
        public int SelectedIndex { get; set; }
        public MainController()
        {
            startingMenu = new StartingMenu();
        }
        public void Start()
        {
            do
            {
                SelectedIndex = startingMenu.RunMenu();
                StartingOptions();
            } while (SelectedIndex != startingMenu.Options.Length - 1);

        }
        public void StartingOptions()
        {
            switch (SelectedIndex)
            {
                case 0:
                    Collection();
                    break;
                case 1:
                    DecksMenu();
                    break;
                case 2:
                    Info();
                    break;
                case 3:
                    Exit();
                    break;
            }
        }
        public void Collection()
        {
            CardsCollection cardsCollection = new CardsCollection();
            cardsCollection.Run();
        }
        public void DecksMenu()
        {
            DecksMenu decksMenu = new DecksMenu();
            decksMenu.RunMenu();
        }
        public void Info()
        {
            string prompt = @"With this program you can create and save your powerful Yu-Gi-Oh! decks
and be one step closer to being the King of Games!
Use Up and Dwon arrows to go through the menus, then Enter to go to the desired menu.
For more information call us at: 0884230196";
            Menu infoMenu = new Menu(prompt, ["Back"]);
            infoMenu.Run();
        }
        public void Exit()
        {
            string exitMessage = @" 
███╗   ███╗██╗   ██╗    ████████╗██╗   ██╗██████╗ ███╗   ██╗ ██╗
████╗ ████║╚██╗ ██╔╝    ╚══██╔══╝██║   ██║██╔══██╗████╗  ██║ ██║
██╔████╔██║ ╚████╔╝        ██║   ██║   ██║██████╔╝██╔██╗ ██║ ██║
██║╚██╔╝██║  ╚██╔╝         ██║   ██║   ██║██╔══██╗██║╚██╗██║ ╚═╝
██║ ╚═╝ ██║   ██║          ██║   ╚██████╔╝██║  ██║██║ ╚████║ ██╗
╚═╝     ╚═╝   ╚═╝          ╚═╝    ╚═════╝ ╚═╝  ╚═╝╚═╝  ╚═══╝ ╚═╝
";
            Console.Clear();
            Console.WriteLine(exitMessage);
        }
    }
}
