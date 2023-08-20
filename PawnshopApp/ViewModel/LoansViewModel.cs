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
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PawnshopApp.ViewModel
{
    public class LoansViewModel : ViewModelBase
    {
        private ObservableCollection<LoanViewModel> _loans = new();
        private LoanViewModel _selectedLoan;

        private readonly ILoanService _loanService;
        private readonly IDocumentService _documentService;

        public ObservableCollection<LoanViewModel> Loans
        {
            get => _loans;
            set
            {
                _loans = value;
                RaisePropertiesChanged(nameof(Loans));
            }
        }

        public LoanViewModel SelectedLoan
        {
            get => _selectedLoan;
            set
            {
                _selectedLoan = value;
                RaisePropertiesChanged(nameof(SelectedLoan));
            }
        }

        // Constructor
        public LoansViewModel()
        {
            _loanService = ServiceProviderContainer.GetService<ILoanService>();
            _documentService = ServiceProviderContainer.GetService<IDocumentService>();


            IEnumerable<Loan> loans = _loanService.GetAll();
            IEnumerable<Document> documents = _documentService.GetAll();
            foreach (Loan loan in loans)
            {
                Document? document = documents.FirstOrDefault(d => d.UUID == loan.UUID);
                _loans.Add(new LoanViewModel(loan)) ;
            }
        }

    }
}
