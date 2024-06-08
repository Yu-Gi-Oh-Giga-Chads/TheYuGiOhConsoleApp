using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class Card
    {
        [Key]
        public int CardId { get; set; }

        public string? Name { get; set; }
        public string? Type { get; set; }
        public string? FrameType { get; set; }
        public string? Desc { get; set; }
        public int ATK { get; set; }
        public int DEF { get; set; }
        public int Level { get; set; }
        public string? Race { get; set; }
        public string? Attribute { get; set; }
        [Url]
        public string? Ygoprodeck_url { get; set; }
        public List<Deck> Decks { get; set; }
        public Card() 
        {
            Decks = new List<Deck>();
        }
    }
}
