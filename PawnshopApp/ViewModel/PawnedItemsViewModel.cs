using DevExpress.Mvvm;
using PawnshopApp.Entities;
using PawnshopApp.Pages;
using PawnshopApp.Services;
using PawnshopApp.Services.Interfaces;
using PawnshopApp.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PawnshopApp.ViewModel
{
    public class PawnedItemsViewModel : ViewModelBase
    {
        private ObservableCollection<PawnedItemViewModel> _pawnedItems = new();
        private PawnedItemViewModel _selectedPawnedItem;

        public ICommand SellPawnedItemCommand { get; }

        private readonly IPawnedItemService _pawnedItemService;

        private readonly ILoanService _loanService;

        private readonly IDocumentService _documentService;

        public ObservableCollection<PawnedItemViewModel> PawnedItems
        {
            get => _pawnedItems;
            set
            {
                _pawnedItems = value;
                RaisePropertiesChanged(nameof(PawnedItems)); ;
            }
        }

        public PawnedItemViewModel SelectedPawnedItem
        {
            get => _selectedPawnedItem;
            set
            {
                _selectedPawnedItem = value;
                RaisePropertiesChanged(nameof(SelectedPawnedItem));
            }
        }

        // Constructor
        public PawnedItemsViewModel()
        {
            _pawnedItemService = ServiceProviderContainer.GetService<IPawnedItemService>();
            _loanService = ServiceProviderContainer.GetService<ILoanService>();
            _documentService = ServiceProviderContainer.GetService<IDocumentService>();

            IEnumerable<PawnedItem> pawnedItems = _pawnedItemService.GetAll();

            foreach(PawnedItem pawnedItem in pawnedItems)
            {
                PawnedItems.Add(new PawnedItemViewModel(pawnedItem));
            }

            SellPawnedItemCommand = new DelegateCommand(SellItem);
        }

        private async void SellItem()
        {
            Loan loan = _loanService.GetByUUID(SelectedPawnedItem.LoanUUID);

            if(DateTime.Now < SelectedPawnedItem.ExpiryDate)
            {
                MessageBox.Show("Предмет не может быть продан");
                return;
            }

            if(loan.Status == LoanStatus.Closed)
            {
                MessageBox.Show("Предмет не может быть продан, долг выплачен");
                return;
            }

            SellPawnedItem sellPawnedItem = new SellPawnedItem();
            decimal price = sellPawnedItem.GetSellPrice();

            if (price < 0)
                return;

            PawnedItem pawnedItem = await _pawnedItemService.GetByUUIDAsync(SelectedPawnedItem.UUID);

            pawnedItem.IsSold = true;
            pawnedItem.EstimatedValue = price;

            await _pawnedItemService.UpdateAsync(pawnedItem);
            SelectedPawnedItem.Sold = "Продан";
        }
    }
}
