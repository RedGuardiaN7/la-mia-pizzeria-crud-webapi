/*
 * using Pizzeria.Models;

namespace Pizzeria.Utils
{
    public class PizzaData
    {
        private static List<Pizza> pizzas;

        private static List<string> ImagesLinks;

        private static List<string> PizzaNames;



        public static List<Pizza> GetPizzas()
        {
            if (pizzas != null) 
            {
                return pizzas;
            }

            pizzas = new List<Pizza>();

            PizzaNames = new List<string>()
            {   "Margherita",
                "Prosciutto crudo",
                "Capricciosa",
                "Quattro stagioni",
                "Diavola",
                "Bergamasca",
                "Roberta",
                "Pizza della casa"
            };

            ImagesLinks = new List<string>() 
            {   "https://pizzeria2000.it/wp-content/uploads/2019/10/MARGHERITA.2-600x450.jpg",
                "https://pizzeria2000.it/wp-content/uploads/2019/10/PROSCIUTTO-600x450.jpg",
                "https://pizzeria2000.it/wp-content/uploads/2019/10/CAPRICCIOSA-1-600x450.jpg",
                "https://pizzeria2000.it/wp-content/uploads/2019/10/QUATTRO-STAGIONI-600x450.jpg",
                "https://pizzeria2000.it/wp-content/uploads/2019/10/DIAVOLA-1-600x450.jpg",
                "https://pizzeria2000.it/wp-content/uploads/2019/10/BERGAMASCA-1-600x450.jpg",
                "https://pizzeria2000.it/wp-content/uploads/2019/10/ROBERTA-600x450.jpg",
                "https://pizzeria2000.it/wp-content/uploads/2019/10/DELLA-CASA-1-600x450.jpg"};

            for (int i = 0; i < 8; i++) 
            {
                Pizza NewPizza = new Pizza(i, PizzaNames[i], "Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium.", ImagesLinks[i]);
                pizzas.Add(NewPizza);
            }

            return pizzas;
        }

    }
}
*/