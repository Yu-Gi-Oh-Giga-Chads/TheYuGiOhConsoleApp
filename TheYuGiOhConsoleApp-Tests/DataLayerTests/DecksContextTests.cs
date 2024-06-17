using System.Collections.Generic;
using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using BusinessLayer;
using DataLayer;

namespace DataLayer.Tests
{
    public class DecksContextTests
    {
        private DbContextOptions<YuGiOhDbContext> _options;
        private YuGiOhDbContext _context;
        private DecksContext _decksContext;

        [SetUp]
        public void Setup()
        {
            _options = new DbContextOptionsBuilder<YuGiOhDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .ConfigureWarnings(warnings => warnings.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;

            _context = new YuGiOhDbContext(_options);
            _decksContext = new DecksContext(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public void CreateDeck_ShouldAddDeckToDatabase()
        {
            var deck = new Deck { Name = "Test Deck", Cards = new List<Card>() };
            _decksContext.Create(deck);

            var savedDeck = _context.Decks.Find(deck.Id);
            Assert.IsNotNull(savedDeck);
            Assert.AreEqual("Test Deck", savedDeck.Name);
        }

        [Test]
        public void ReadDeck_ShouldReturnDeckFromDatabase()
        {
            var deck = new Deck { Name = "Test Deck", Cards = new List<Card>() };
            _context.Decks.Add(deck);
            _context.SaveChanges();

            var savedDeck = _decksContext.Read(deck.Id);
            Assert.IsNotNull(savedDeck);
            Assert.AreEqual("Test Deck", savedDeck.Name);
        }

        [Test]
        public void ReadAllDecks_ShouldReturnAllDecksFromDatabase()
        {
            var deck1 = new Deck { Name = "Test Deck 1", Cards = new List<Card>() };
            var deck2 = new Deck { Name = "Test Deck 2", Cards = new List<Card>() };
            _context.Decks.AddRange(deck1, deck2);
            _context.SaveChanges();

            var decks = _decksContext.ReadAll();
            Assert.AreEqual(2, decks.Count);
        }

        [Test]
        public void UpdateDeck_ShouldUpdateDeckInDatabase()
        {
            var deck = new Deck { Name = "Test Deck", Cards = new List<Card>() };
            _context.Decks.Add(deck);
            _context.SaveChanges();

            deck.Name = "Updated Deck";
            _decksContext.Update(deck);

            var updatedDeck = _context.Decks.Find(deck.Id);
            Assert.AreEqual("Updated Deck", updatedDeck.Name);
        }

        [Test]
        public void DeleteDeck_ShouldRemoveDeckFromDatabase()
        {
            var deck = new Deck { Name = "Test Deck", Cards = new List<Card>() };
            _context.Decks.Add(deck);
            _context.SaveChanges();

            _decksContext.Delete(deck.Id);

            var deletedDeck = _context.Decks.Find(deck.Id);
            Assert.IsNull(deletedDeck);
        }
    }
}
