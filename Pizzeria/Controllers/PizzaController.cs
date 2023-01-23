using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.SqlServer.Server;
using Pizzeria.Database;
using Pizzeria.Models;
using Pizzeria.Utils;

namespace Pizzeria.Controllers
{
    public class PizzaController : Controller
    {
        public IActionResult Index()
        {
            using (PizzaContext db = new PizzaContext())
            {
                List<Pizza> PizzaList = db.Pizzas.ToList<Pizza>();
                return View("Index", PizzaList);
            }
        }

        public IActionResult Details(int id)
        {
            using (PizzaContext db = new PizzaContext())
            {
                // LINQ: syntax methos
                Pizza FoundPizza = db.Pizzas
                    .Where(DbPizza => DbPizza.Id == id)
                    .Include(pizza => pizza.Category)
                    .Include(pizza => pizza.Ingredients)
                    .FirstOrDefault();

                if (FoundPizza != null)
                {
                    return View(FoundPizza);
                }

                return NotFound("La pizza che stai cercando non esiste!");
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            using (PizzaContext db = new PizzaContext())
            {
                List<Category> categoriesFromDb = db.Categories.ToList<Category>();

                PizzaCategoriesView modelForView = new PizzaCategoriesView();
                modelForView.Pizza = new Pizza();

                modelForView.Categories = categoriesFromDb;
                modelForView.Ingredients = IngrediensConverter.getIngredientsForMultipleSelect();

                return View("Create", modelForView);
            }


        }
        [HttpGet]
        public ActionResult Edit(int id) 
        { 
            using (PizzaContext db = new PizzaContext())
            {
                Pizza PizzaToEdit = db.Pizzas
                    .Where(DbPizza => DbPizza.Id == id)
                    .Include(pizza => pizza.Ingredients)
                    .FirstOrDefault();

                if (PizzaToEdit == null)
                {
                    
                    return NotFound("La pizza che cerchi di modificare non esiste!");
                }

                List<Category> categories= db.Categories.ToList<Category>();

                PizzaCategoriesView modelForView = new PizzaCategoriesView();
                modelForView.Pizza = PizzaToEdit;
                modelForView.Categories = categories;
                
                List<Ingredient> DbIngredientList = db.Ingredients.ToList<Ingredient>();

                List<SelectListItem> OptionsForSelect = new List<SelectListItem>();

                foreach (Ingredient ingredient in DbIngredientList)
                {
                    bool ItWasSelected = PizzaToEdit.Ingredients.Any(SelectedIngredients => SelectedIngredients.Id == ingredient.Id);

                    SelectListItem optionSingleSelect = new SelectListItem() { Text = ingredient.Name, Value = ingredient.Id.ToString(), Selected = ItWasSelected };
                    OptionsForSelect.Add(optionSingleSelect);
                }

                modelForView.Ingredients = OptionsForSelect;

                return View("Edit", modelForView);
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            using (PizzaContext db = new PizzaContext())
            {
                // LINQ: syntax methos
                Pizza PizzaToDelete = db.Pizzas
                    .Where(DbPizza => DbPizza.Id == id)
                    .Include(pizza => pizza.Category)
                    .Include(pizza => pizza.Ingredients)
                    .FirstOrDefault();

                if (PizzaToDelete != null)
                {
                    return View(PizzaToDelete);
                }

                return NotFound("La pizza che stai cercando non esiste!");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PizzaCategoriesView formData)
        {
            if (!ModelState.IsValid)
            {
                using (PizzaContext db = new PizzaContext())
                {
                    List<Category> categories = db.Categories.ToList<Category>();

                    formData.Categories = categories;

                    formData.Ingredients = IngrediensConverter.getIngredientsForMultipleSelect();
                }
                return View("Create", formData);
            }

            using(PizzaContext db = new PizzaContext())
            {
                if(formData.IngredientsSelectedFromMultipleSelect != null)
                {
                    formData.Pizza.Ingredients = new List<Ingredient>();

                    foreach (string IngredientId in formData.IngredientsSelectedFromMultipleSelect)
                    {
                        int IngredientIdSelected = int.Parse(IngredientId);
                        Ingredient ingredient = db.Ingredients.Where(DbIngredient => DbIngredient.Id == IngredientIdSelected).FirstOrDefault();

                        formData.Pizza.Ingredients.Add(ingredient);
                    }
                }

                db.Pizzas.Add(formData.Pizza);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, PizzaCategoriesView formData)
        {
            if (!ModelState.IsValid)
            {
                using (PizzaContext db = new PizzaContext())
                {
                    List<Category> categories = db.Categories.ToList<Category>();

                    formData.Categories = categories;
                }

                return View("Edit", formData);
            }

            using (PizzaContext db = new PizzaContext())
            {
                Pizza PizzaToEdit = db.Pizzas
                    .Where(DbPizza => DbPizza.Id == id)
                    .Include(pizza => pizza.Ingredients)
                    .FirstOrDefault();
                if (PizzaToEdit != null)
                {
                    PizzaToEdit.Name = formData.Pizza.Name;
                    PizzaToEdit.Description = formData.Pizza.Description;
                    PizzaToEdit.Price = formData.Pizza.Price;
                    PizzaToEdit.Image = formData.Pizza.Image;
                    PizzaToEdit.CategoryId = formData.Pizza.CategoryId;

                    PizzaToEdit.Ingredients.Clear();

                    if (formData.IngredientsSelectedFromMultipleSelect != null)
                    {
                        foreach (string IngredientId in formData.IngredientsSelectedFromMultipleSelect)
                        {
                            int IngredientIdSelected = int.Parse(IngredientId);
                            Ingredient ingredient = db.Ingredients.Where(DbIngredient => DbIngredient.Id == IngredientIdSelected).FirstOrDefault();

                            PizzaToEdit.Ingredients.Add(ingredient);
                        }

                    }
                    
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                else
                {
                    return NotFound("La pizza che volevi modificare non esiste!");
                }
                
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, PizzaCategoriesView formdata) 
        {
            using (PizzaContext db = new PizzaContext())
            {
                Pizza PizzaToDelete = db.Pizzas
                    .Where(DbPizza => DbPizza.Id == id)
                    .FirstOrDefault();
                if (PizzaToDelete != null)
                {
                    db.Pizzas.Remove(PizzaToDelete);
                    db.SaveChanges() ;
                    return RedirectToAction("Index");
                }
                else
                {
                    return NotFound();
                }
            }
        }
            
    }
}
