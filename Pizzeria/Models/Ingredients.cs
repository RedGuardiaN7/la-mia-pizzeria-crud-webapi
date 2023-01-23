namespace Pizzeria.Models
{
    public class Ingredient
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public List<Pizza> Pizza { get; set; }
        public Ingredient() { }

    }
}
