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
        [HttpGet]
        public IActionResult Get()
        {
            using (PizzaContext context = new PizzaContext())
            {
                List<Pizza> list = context.Pizzas.Include(pizza => pizza.Ingredients).ToList<Pizza>();
                return Ok(list);
            }
        }
    }
}
