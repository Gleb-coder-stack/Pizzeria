using Microsoft.Maui.Controls;
using PizzeriaApp.ViewModels;

namespace PizzeriaApp.Views
{
    public partial class ConstructorPage : ContentPage
    {
        public ConstructorPage()
        {
            InitializeComponent();

            var viewModel = new ConstructorViewModel();
            BindingContext = viewModel;

            LoadPizzaParameter(viewModel);
        }

        private async void LoadPizzaParameter(ConstructorViewModel viewModel)
        {
            var pizza = Services.NavigationParametersStore.Instance.GetParameter<Models.Pizza>("SelectedPizza");
            if (pizza != null)
            {
                await Task.Run(() => viewModel.Initialize(pizza));
                Title = pizza.Name;
            }
        }
    }
}