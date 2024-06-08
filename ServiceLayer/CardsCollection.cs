using BusinessLayer;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public class CardsCollection
    {
        private YuGiOhDbContext context;
        private CardsContext cardsContext;
        private List<Card> cards;
        public CardsCollection()
        {
            context = new YuGiOhDbContext();
            cardsContext = new CardsContext(context);
            cards = this.cardsContext.ReadAll();
        }
        public void Run()
        {
            SearchBar searchBar = new SearchBar();
            searchBar.Run();
        }
    }
}
