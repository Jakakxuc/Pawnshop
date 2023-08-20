using DevExpress.Mvvm;
using PawnshopApp.Entities;
using PawnshopApp.Pages;
using PawnshopApp.Services.Interfaces;
using PawnshopApp.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PawnshopApp.ViewModel
{
    public class CreateDocumentViewModel : ViewModelBase
    {
        public ObservableCollection<CustomerViewModel> Customers { get; } = new ObservableCollection<CustomerViewModel>();

        public CustomerViewModel SelectedCustomer { get; set; }

        public string DocumentName { get; set; }

        public DateTime LoanExpiryDate { get; set; }

        public ObservableCollection<LoanViewModel> LoansViewModel { get; } = new ObservableCollection<LoanViewModel>();

        public ObservableCollection<PawnedItemViewModel> PawnedItems { get; } = new ObservableCollection<PawnedItemViewModel>();

        public ICommand OpenAddLoanFormCommand { get; }

        public ICommand OpenAddPawnedItemFormCommand { get; }

        public ICommand CreateDocumentCommand { get; }

        public CreateDocumentViewModel()
        {
            var customers = ServiceProviderContainer.GetService<ICustomerService>().GetAll().Select(c=>new CustomerViewModel(c)).ToList();
            foreach (var customer in customers)
            {
                Customers.Add(customer);
            }

            OpenAddPawnedItemFormCommand = new DelegateCommand(OpenAddPawnedItemForm);
            CreateDocumentCommand = new DelegateCommand(CreateDocument);
        }


        private void OpenAddPawnedItemForm()
        {
            AddPawnedItem addPawnedItemForm = new AddPawnedItem();
            PawnedItemViewModel pawnedItemViewModel = addPawnedItemForm.GetPawnedItemInfo();
            if(pawnedItemViewModel != null)
            {
                PawnedItems.Add(new PawnedItemViewModel()
                {
                    Sold = "Не продан",
                    Description = pawnedItemViewModel.Description,
                    EstimatedValue = pawnedItemViewModel.EstimatedValue,
                    PawnDate = DateTime.Now.Date,
                    ExpiryDate = LoanExpiryDate.Date
                });
            }
        }

        private void CreateDocument()
        {
            Document document = new Document
            {
                CustomerUUID = SelectedCustomer.CustomerUUID,
                Name = DocumentName
            };
        }
    }
}
