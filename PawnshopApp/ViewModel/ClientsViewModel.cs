using DevExpress.Mvvm;
using PawnshopApp.Entities;
using PawnshopApp.Pages;
using PawnshopApp.Services;
using PawnshopApp.Services.Interfaces;
using PawnshopApp.Utils;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace PawnshopApp.ViewModel
{
    public class ClientsViewModel : ViewModelBase
    {
        public ObservableCollection<CustomerViewModel> Clients { get; } = new ObservableCollection<CustomerViewModel>();

        private CustomerViewModel _selectedClient;

        public ICommand OpenAddCustomerFormCommand { get; }

        private readonly ICustomerService _customerService;

        public CustomerViewModel SelectedClient
        {
            get => _selectedClient;
            set
            {
                _selectedClient = value;
                RaisePropertiesChanged(nameof(SelectedClient));
            }
        }

        public ClientsViewModel()
        {
            // Initialize Clients list with sample data

            _customerService  = ServiceProviderContainer.GetService<ICustomerService>();

            IEnumerable<Customer> customers = _customerService.GetAll();
            foreach (Customer customer in customers)
            {
                Clients.Add(new CustomerViewModel(customer));
            }
            // Add more clients as needed

            OpenAddCustomerFormCommand = new DelegateCommand(OpenAddCustomerForm);
        }


        private async void OpenAddCustomerForm()
        {
            // Implement logic to open the AddCustomerForm window
            AddCustomerForm addCustomerForm = new AddCustomerForm();
            CustomerViewModel newCustomer = addCustomerForm.GetCustomerInfo();
            if (newCustomer != null)
            {
                Customer addedCustomer = await _customerService.AddAsync(new Customer()
                {
                    Name = newCustomer.Name,
                    Surname = newCustomer.Surname,
                    PhoneNumber = newCustomer.PhoneNumber,
                });
                Clients.Add(new CustomerViewModel(addedCustomer));
                SelectedClient = newCustomer;
            }
        }
    }
}
