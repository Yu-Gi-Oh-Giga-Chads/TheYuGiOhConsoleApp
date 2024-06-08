using BusinessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class CardsContext:IDB<Card, int>
    {
        private readonly YuGiOhDbContext _cardsDbContext;
        public CardsContext(YuGiOhDbContext cardsDbContext)
        {
            _cardsDbContext = cardsDbContext;
        }

        public void Create(Card entity)
        {
            try
            {
                _cardsDbContext.Cards.Add(entity);
                _cardsDbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Card Read(int key, bool useNavigational = false, bool isReadonly = false)
        {
            try
            {
                IQueryable<Card> query = _cardsDbContext.Cards;
                if (useNavigational)
                {
                    query.Include(c => c.Decks);
                }
                if (isReadonly)
                {
                    query.AsNoTrackingWithIdentityResolution();
                }
                return query.SingleOrDefault(c => c.CardId == key);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<Card> ReadAll(bool useNavigational = false, bool isReadonly = false)
        {
            try
            {
                IQueryable<Card> query = _cardsDbContext.Cards;
                if (useNavigational)
                {
                    query.Include(c => c.Decks);
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

        public void Update(Card entity, bool useNavigational = false, bool isReadonly = false)
        {
            try
            {
                Card card = Read(entity.CardId, useNavigational, false);
                if (card is null)
                {
                    throw new Exception($"Card with id = {entity.CardId} does not exist");
                }
                _cardsDbContext.Cards.Entry(card).CurrentValues.SetValues(entity);
                if (useNavigational)
                {
                    List<Deck> decks = new List<Deck>();
                    foreach (var item in entity.Decks)
                    {
                        Deck deck = _cardsDbContext.Decks.Find(item.Id);
                        if (deck is null)
                        {
                            decks.Add(item);
                        }
                        else
                        {
                            decks.Add(deck);
                        }
                    }
                    card.Decks = decks;
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
                Card card = Read(key, useNavigational, false);
                if (card is null)
                {
                    throw new Exception($"Card with id = {key} does not exist");
                }
                _cardsDbContext.Cards.Remove(card);
                _cardsDbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
