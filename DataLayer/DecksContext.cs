using BusinessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class DecksContext:IDB<Deck, int>
    {
        private readonly YuGiOhDbContext _cardsDbContext;
        public DecksContext(YuGiOhDbContext cardsDbContext)
        {
            _cardsDbContext = cardsDbContext;
        }

        public void Create(Deck entity)
        {
            try
            {
                _cardsDbContext.Decks.Add(entity);
                _cardsDbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Deck Read(int key, bool useNavigational = false, bool isReadonly = false)
        {
            try
            {
                IQueryable<Deck> query = _cardsDbContext.Decks;
                if (useNavigational)
                {
                    query.Include(d => d.Cards);
                }
                if (isReadonly)
                {
                    query.AsNoTrackingWithIdentityResolution();
                }
                return query.SingleOrDefault(d => d.Id == key);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<Deck> ReadAll(bool useNavigational = false, bool isReadonly = false)
        {
            try
            {
                IQueryable<Deck> query = _cardsDbContext.Decks;
                if (useNavigational)
                {
                    query.Include(d => d.Cards);
                }
                if (isReadonly)
                {
                    query.AsNoTrackingWithIdentityResolution();
                }
                return query.ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Update(Deck entity, bool useNavigational = false, bool isReadonly = false)
        {
            try
            {
                Deck deck = Read(entity.Id, useNavigational, false);
                if (deck is null)
                {
                    throw new Exception($"Deck with id = {entity.Id} does not exist");
                }
                _cardsDbContext.Decks.Entry(deck).CurrentValues.SetValues(entity);
                if (useNavigational)
                {
                    List<Card> cards = new List<Card>();
                    foreach (var item in entity.Cards)
                    {
                        Card card = _cardsDbContext.Cards.Find(item.CardId);
                        if (card is null)
                        {
                            cards.Add(item);
                        }
                        else
                        {
                            cards.Add(card);
                        }
                    }
                    deck.Cards = cards;
                }
                _cardsDbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Delete(int key, bool useNavigational = false, bool isReadonly = false)
        {
            try
            {
                Deck deck = Read(key, useNavigational, false);
                if (deck is null)
                {
                    throw new Exception($"Deck with id = {key} does not exist");
                }
                _cardsDbContext.Decks.Remove(deck);
                _cardsDbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
