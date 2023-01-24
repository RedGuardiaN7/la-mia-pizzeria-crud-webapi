using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pizzeria.Database;
using Pizzeria.Models;

namespace Pizzeria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzasController : ControllerBase
    {
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            using (PizzaContext context = new PizzaContext())
            {
               Pizza pizza = context.Pizzas.Where(pizza => pizza.Id == id).FirstOrDefault();

                if (pizza == null)
                {
                    return NotFound();
                }

                return Ok(pizza);
            }
        }

        [HttpGet]
        public IActionResult Get(string? search) 
        { 
            using (PizzaContext context = new PizzaContext())
            {
                List<Pizza> pizzas = new List<Pizza>();

                if (search is null || search == "")
                {
                    pizzas = context.Pizzas.Include(pizza => pizza.Ingredients)
                                           .Include(pizza => pizza.Category)
                                           .ToList<Pizza>();
                }
                else
                {
                    search = search.ToLower();

                    pizzas = context.Pizzas.Where(pizza => pizza.Name.ToLower().Contains(search))
                        .Include(pizza => pizza.Ingredients)
                        .Include(pizza => pizza.Category)
                        .ToList<Pizza>();
                }

                return Ok(pizzas);

                }
            }
        }
    }
}
