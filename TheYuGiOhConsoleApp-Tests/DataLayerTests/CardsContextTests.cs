using BusinessLayer;
using DataLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheYuGiOhConsoleApp_Tests.DataLayerTests
{
    internal class CardsContextTests
    {
        private DbContextOptions<YuGiOhDbContext> _options;
        private YuGiOhDbContext _context;
        private CardsContext _cardsContext;
    
        [SetUp]
        public void Setup()
        {
            _options = new DbContextOptionsBuilder<YuGiOhDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .ConfigureWarnings(warnings => warnings.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;

            _context = new YuGiOhDbContext(_options);
            _cardsContext = new CardsContext(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public void CreateCard_ShouldAddCardToDatabase()
        {
            var card = new Card { Name = "Test Card", Desc = "This is a test card", Decks = new List<Deck>() };
            _cardsContext.Create(card);

            var savedCard = _context.Cards.Find(card.CardId);
            Assert.IsNotNull(savedCard);
            Assert.AreEqual("Test Card", savedCard.Name);
        }

        [Test]
        public void ReadCard_ShouldReturnCardFromDatabase()
        {
            var card = new Card { Name = "Test Card", Desc = "This is a test card", Decks = new List<Deck>() };
            _context.Cards.Add(card);
            _context.SaveChanges();

            var savedCard = _cardsContext.Read(card.CardId);
            Assert.IsNotNull(savedCard);
            Assert.AreEqual("Test Card", savedCard.Name);
        }

        [Test]
        public void ReadAllCards_ShouldReturnAllCardsFromDatabase()
        {
            var card1 = new Card { Name = "Test Card 1", Desc = "This is a test card 1", Decks = new List<Deck>() };
            var card2 = new Card { Name = "Test Card 2", Desc = "This is a test card 2", Decks = new List<Deck>() };
            _context.Cards.AddRange(card1, card2);
            _context.SaveChanges();

            var cards = _cardsContext.ReadAll();
            Assert.AreEqual(2, cards.Count);
        }

        [Test]
        public void UpdateCard_ShouldUpdateCardInDatabase()
        {
            var card = new Card { Name = "Test Card", Desc = "This is a test card", Decks = new List<Deck>() };
            _context.Cards.Add(card);
            _context.SaveChanges();

            card.Name = "Updated Card";
            _cardsContext.Update(card);

            var updatedCard = _context.Cards.Find(card.CardId);
            Assert.AreEqual("Updated Card", updatedCard.Name);
        }

        [Test]
        public void DeleteCard_ShouldRemoveCardFromDatabase()
        {
            var card = new Card { Name = "Test Card", Desc = "This is a test card", Decks = new List<Deck>() };
            _context.Cards.Add(card);
            _context.SaveChanges();

            _cardsContext.Delete(card.CardId);

            var deletedCard = _context.Cards.Find(card.CardId);
            Assert.IsNull(deletedCard);
        }
    }
}
