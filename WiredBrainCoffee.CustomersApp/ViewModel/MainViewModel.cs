using System;
using System.Threading.Tasks;
using WiredBrainCoffee.CustomersApp.Command;

namespace WiredBrainCoffee.CustomersApp.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public CustomersViewModel CustomersViewModel { get; }
        public ProductsViewModel ProductsViewModel { get; }
        private ViewModelBase? _selectedViewModel;
        public DelegateCommand SelectViewModelCommand { get; }

        public MainViewModel(CustomersViewModel customersViewModel, ProductsViewModel productsViewModel)
        {
            CustomersViewModel = customersViewModel;
            ProductsViewModel = productsViewModel;
            SelectedViewModel = CustomersViewModel;
            SelectViewModelCommand = new DelegateCommand(SelectViewModel);
        }

        

        public ViewModelBase? SelectedViewModel
		{
			get => _selectedViewModel;
			set 
			{ 
				_selectedViewModel = value;
				RaisePropertyChange();
			}
		}

        public override async Task LoadAsync() 
        { 
            if(SelectedViewModel is not null)
            {
                await SelectedViewModel.LoadAsync();
            }
        }

        private async void SelectViewModel(object? parameter)
        {
            SelectedViewModel = parameter as ViewModelBase;
            await LoadAsync();
        }

    }
}
