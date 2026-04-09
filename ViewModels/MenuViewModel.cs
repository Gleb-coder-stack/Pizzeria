using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using PizzeriaApp.Models;
using PizzeriaApp.Services;

namespace PizzeriaApp.ViewModels
{
    public class MenuViewModel : INotifyPropertyChanged
    {
        private readonly CartService _cartService;

        private ObservableCollection<Pizza> _pizzas;
        public ObservableCollection<Pizza> Pizzas
        {
            get => _pizzas;
            set
            {
                _pizzas = value;
                OnPropertyChanged();
            }
        }

        private int _cartItemCount;
        public int CartItemCount
        {
            get => _cartItemCount;
            set
            {
                _cartItemCount = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsCartBadgeVisible));
            }
        }

        public bool IsCartBadgeVisible => CartItemCount > 0;

        public ICommand SelectPizzaCommand { get; }
        public ICommand GoToCartCommand { get; }

        public MenuViewModel()
        {
            _cartService = CartService.Instance;

            LoadPizzas();
            RefreshCartCount();

            SelectPizzaCommand = new Command<Pizza>(OnSelectPizza);
            GoToCartCommand = new Command(OnGoToCart);

            _cartService.CartChanged += (s, e) => RefreshCartCount();
        }

        private async void LoadPizzas()
        {
            await DataService.LoadDataAsync();
            Pizzas = DataService.GetPizzasObservable();
        }

        public void RefreshCartCount()
        {
            CartItemCount = _cartService.GetItems().Sum(i => i.Quantity);
        }

        private async void OnSelectPizza(Pizza pizza)
        {
            if (pizza == null) return;

            NavigationParametersStore.Instance.SetParameter("SelectedPizza", pizza);
            await Shell.Current.GoToAsync("ConstructorPage");
        }

        private async void OnGoToCart()
        {
            await Shell.Current.GoToAsync("CartPage");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}