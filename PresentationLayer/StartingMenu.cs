using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer
{
    public class StartingMenu
    {
        private string prompt = @"
██╗   ██╗██╗   ██╗       ██████╗ ██╗       ██████╗ ██╗  ██╗██╗                           
╚██╗ ██╔╝██║   ██║      ██╔════╝ ██║      ██╔═══██╗██║  ██║██║                           
 ╚████╔╝ ██║   ██║█████╗██║  ███╗██║█████╗██║   ██║███████║██║                           
  ╚██╔╝  ██║   ██║╚════╝██║   ██║██║╚════╝██║   ██║██╔══██║╚═╝                           
   ██║   ╚██████╔╝      ╚██████╔╝██║      ╚██████╔╝██║  ██║██╗                           
   ╚═╝    ╚═════╝        ╚═════╝ ╚═╝       ╚═════╝ ╚═╝  ╚═╝╚═╝                           
                                                                                         
███╗   ███╗ █████╗ ███████╗████████╗███████╗██████╗     ██████╗ ██╗   ██╗███████╗██╗     
████╗ ████║██╔══██╗██╔════╝╚══██╔══╝██╔════╝██╔══██╗    ██╔══██╗██║   ██║██╔════╝██║     
██╔████╔██║███████║███████╗   ██║   █████╗  ██████╔╝    ██║  ██║██║   ██║█████╗  ██║     
██║╚██╔╝██║██╔══██║╚════██║   ██║   ██╔══╝  ██╔══██╗    ██║  ██║██║   ██║██╔══╝  ██║     
██║ ╚═╝ ██║██║  ██║███████║   ██║   ███████╗██║  ██║    ██████╔╝╚██████╔╝███████╗███████╗
╚═╝     ╚═╝╚═╝  ╚═╝╚══════╝   ╚═╝   ╚══════╝╚═╝  ╚═╝    ╚═════╝  ╚═════╝ ╚══════╝╚══════╝
            
";
        public string[] Options = {
            "Cards Collection",
            "Deck Editor",
            "Info",
            "Exit"
        };

        public Menu StartMenu { get; set; }
        public int SelectedIndex { get; set; }
        public StartingMenu()
        {
            StartMenu = new Menu(prompt, Options);
        }
        public int RunMenu()
        {
            SelectedIndex = StartMenu.Run();
            return SelectedIndex;
        }
    }
}
