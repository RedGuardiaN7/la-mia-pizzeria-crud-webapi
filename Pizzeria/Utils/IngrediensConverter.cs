using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.SqlServer.Server;
using Pizzeria.Database;
using Pizzeria.Models;

namespace Pizzeria.Utils
{
    public static class IngrediensConverter
    {
        public static List<SelectListItem> getIngredientsForMultipleSelect()
        {
            using (PizzaContext db = new PizzaContext())
            {
                List<Ingredient> DbIngredients = db.Ingredients.ToList<Ingredient>();

                // Creare una lista di SelectListItem e tradurci al suo interno tutti i nostri Tag che provengono da Db
                List<SelectListItem> MultipleSelectList = new List<SelectListItem>();

                foreach (Ingredient ingredient in DbIngredients)
                {
                    SelectListItem SingleMultipleSelect = new SelectListItem() { Text = ingredient.Name, Value = ingredient.Id.ToString() };
                    MultipleSelectList.Add(SingleMultipleSelect);
                }

                return MultipleSelectList;
            }
        }
    }
}
