using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using WiredBrainCoffee.CustomersApp.Data;
using WiredBrainCoffee.CustomersApp.Model;

namespace WiredBrainCoffee.CustomersApp.ViewModel
{
    public class CustomersViewModel : INotifyPropertyChanged
    {
        private readonly ICustomerDataProvider _customerDataProvider;
        private Customer? _selectedCustomer;

        public CustomersViewModel(ICustomerDataProvider customerDataProvider)
        {
            _customerDataProvider = customerDataProvider;
        }
        public ObservableCollection<Customer> Customers { get; } = new();

        public Customer? SelectedCustomer { 
            get => _selectedCustomer;
            set {
                _selectedCustomer = value;
                RaisePropertyChange();
                }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public async Task LoadAsync()
        {
            if (Customers.Any()) return;

            var customers = await _customerDataProvider.GetAllAsync();

            if (customers != null)
            {
                foreach (var customer in customers)
                {
                    Customers.Add(customer);
                }
            }
        }

        internal void Add()
        {
            var customer = new Customer { FirstName = "New" };
            Customers.Add(customer);
            SelectedCustomer = customer;
        }

        private void RaisePropertyChange([CallerMemberName]string? propertyName =null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
