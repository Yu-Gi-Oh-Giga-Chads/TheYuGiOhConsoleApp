using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class Deck
    {
        [Key]

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public List<Card> Cards { get; set; }
        public List<int> Copies { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime LastEdited { get; set; }

        public Deck()
        {
            Cards = new List<Card>();
            Copies = new List<int>();
            DateCreated = DateTime.Now;
            LastEdited = DateTime.Now;
        }

        public Deck(string name)
            : this()
        {
            Name = name;
        }
    }
}
