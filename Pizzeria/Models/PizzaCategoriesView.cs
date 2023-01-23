using Microsoft.AspNetCore.Mvc.Rendering;

namespace Pizzeria.Models
{
    public class PizzaCategoriesView
    {
        public Pizza Pizza { get; set; }
        public List<Category>? Categories { get; set;}
        public List<SelectListItem>? Ingredients { get; set;}
        public List<string>? IngredientsSelectedFromMultipleSelect { get; set; }
    }
}
