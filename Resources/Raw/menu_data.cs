using System.Collections.ObjectModel;
using PizzeriaApp.Models;

namespace PizzeriaApp.Services
{
    public static class DataService
    {
        private static List<Pizza> _pizzas;
        private static List<Ingredient> _ingredients;

        public static Task LoadDataAsync()
        {
            _pizzas = new List<Pizza>
            {
                new Pizza { Id = 1, Name = "Маргарита", Description = "Томатный соус, моцарелла, базилик, оливковое масло", BasePrice = 350, ImageUrl = "margherita.png" },
                new Pizza { Id = 2, Name = "Пепперони", Description = "Томатный соус, моцарелла, пепперони, орегано", BasePrice = 420, ImageUrl = "pepperoni.png" },
                new Pizza { Id = 3, Name = "Четыре сыра", Description = "Моцарелла, горгонзола, пармезан, фонталь", BasePrice = 480, ImageUrl = "four_cheese.png" },
                new Pizza { Id = 4, Name = "Гавайская", Description = "Томатный соус, моцарелла, курица, ананас", BasePrice = 450, ImageUrl = "hawaiian.png" },
                new Pizza { Id = 5, Name = "Карбонара", Description = "Сливочный соус, бекон, пармезан, яйцо", BasePrice = 500, ImageUrl = "carbonara.png" }
            };

            _ingredients = new List<Ingredient>
            {
                new Ingredient { Id = 1, Name = "Сыр моцарелла", Price = 50 },
                new Ingredient { Id = 2, Name = "Бекон", Price = 80 },
                new Ingredient { Id = 3, Name = "Грибы", Price = 45 },
                new Ingredient { Id = 4, Name = "Оливки", Price = 40 },
                new Ingredient { Id = 5, Name = "Ветчина", Price = 70 },
                new Ingredient { Id = 6, Name = "Томаты черри", Price = 35 },
                new Ingredient { Id = 7, Name = "Перец халапеньо", Price = 30 },
                new Ingredient { Id = 8, Name = "Пармезан", Price = 60 }
            };

            return Task.CompletedTask;
        }

        public static List<Pizza> GetPizzas() => _pizzas;
        public static List<Ingredient> GetIngredients() => _ingredients;
        public static ObservableCollection<Pizza> GetPizzasObservable() => new ObservableCollection<Pizza>(_pizzas);

        public static ObservableCollection<Ingredient> GetIngredientsObservable()
        {
            var result = new ObservableCollection<Ingredient>();
            foreach (var ing in _ingredients)
            {
                result.Add(new Ingredient { Id = ing.Id, Name = ing.Name, Price = ing.Price, IsSelected = false });
            }
            return result;
        }
    }
}