using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pizzeria.Models
{
    public class Pizza
    {
        public int Id { get; set; }
        [Column(TypeName = "varchar(150)")]
        [Required(ErrorMessage = "Il nome della pizza è obbligatorio!")]
        [StringLength(50, ErrorMessage = "Il nome della pizza è troppo lungo! (>50 caratteri)")]
        public string Name { get; set; }
        [Column(TypeName = "text")]
        [Required(ErrorMessage = "La descrizione della pizza è obbligatoria!")]
        [StringLength(300, ErrorMessage = "La descrizione della pizza è troppo lunga! (>300 caratteri)")]
        public string Description { get; set; }
        [Column(TypeName = "varchar(300)")]
        [Url(ErrorMessage = "L'url inserito è invalido!")]
        public string Image { get; set; }

        [Column(TypeName = "float")]
        [Required(ErrorMessage = "Il prezzo della pizza è obbligatorio")]
        [Range(0, 10000, ErrorMessage = "Il prezzo deve essere superiore a 0!")]
        public float Price { get; set; }

        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        public List<Ingredient>? Ingredients { get; set; }

        public Pizza()
        {

        }

        public Pizza(int id, string name, string description, string image)
        {
            Id = id;
            Name = name;
            Description = description;
            Image = image;
        }
    }
}
