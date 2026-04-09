using System.Collections.Generic;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using PizzeriaApp.Models;

namespace PizzeriaApp.Services
{
	public static class DataService
	{
		private static List<Pizza> _pizzas;
		private static List<Ingredient> _ingredients;
		private static bool _isLoaded = false;

		public static async Task LoadDataAsync()
		{
			if (_isLoaded) return;

			try
			{
				using var stream = await FileSystem.OpenAppPackageFileAsync("menu_data.json");
				using var reader = new StreamReader(stream);
				var json = await reader.ReadToEndAsync();

				var menuData = JsonSerializer.Deserialize<MenuData>(json);

				if (menuData != null)
				{
					_pizzas = menuData.Pizzas;
					_ingredients = menuData.Ingredients;
				}
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine($"Ошибка загрузки данных: {ex.Message}");
				// Запасные данные на случай ошибки
				LoadFallbackData();
			}

			_isLoaded = true;
		}

		private static void LoadFallbackData()
		{
			_pizzas = new List<Pizza>
			{
				new Pizza { Id = 1, Name = "Маргарита", Description = "Томатный соус, моцарелла, базилик", BasePrice = 350 },
				new Pizza { Id = 2, Name = "Пепперони", Description = "Томатный соус, моцарелла, пепперони", BasePrice = 420 },
				new Pizza { Id = 3, Name = "Четыре сыра", Description = "Моцарелла, горгонзола, пармезан, фонталь", BasePrice = 480 },
				new Pizza { Id = 4, Name = "Гавайская", Description = "Томатный соус, моцарелла, курица, ананас", BasePrice = 450 }
			};

			_ingredients = new List<Ingredient>
			{
				new Ingredient { Id = 1, Name = "Сыр моцарелла", Price = 50 },
				new Ingredient { Id = 2, Name = "Бекон", Price = 80 },
				new Ingredient { Id = 3, Name = "Грибы", Price = 45 },
				new Ingredient { Id = 4, Name = "Оливки", Price = 40 }
			};
		}

		public static List<Pizza> GetPizzas()
		{
			return _pizzas ?? new List<Pizza>();
		}

		public static List<Ingredient> GetIngredients()
		{
			return _ingredients ?? new List<Ingredient>();
		}

		public static ObservableCollection<Pizza> GetPizzasObservable()
		{
			return new ObservableCollection<Pizza>(GetPizzas());
		}

		public static ObservableCollection<Ingredient> GetIngredientsObservable()
		{
			var ingredients = GetIngredients();
			var result = new ObservableCollection<Ingredient>();
			foreach (var ing in ingredients)
			{
				result.Add(new Ingredient
				{
					Id = ing.Id,
					Name = ing.Name,
					Price = ing.Price,
					IsSelected = false
				});
			}
			return result;
		}
	}
}