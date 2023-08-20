using DevExpress.Mvvm;
using PawnshopApp.Entities;
using PawnshopApp.Pages;
using PawnshopApp.Services;
using PawnshopApp.Services.BusinessLogic.Interfaces;
using PawnshopApp.Services.Interfaces;
using PawnshopApp.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PawnshopApp.ViewModel
{
    public class DocumentsViewModel : ViewModelBase
    {
        public ObservableCollection<DocumentViewModel> Documents { get; } = new ObservableCollection<DocumentViewModel>();
        public ObservableCollection<PawnedItemViewModel> PawnedItems { get; } = new ObservableCollection<PawnedItemViewModel>();
        public ObservableCollection<LoanViewModel> Loans { get; } = new ObservableCollection<LoanViewModel>();
        public ObservableCollection<PaymentViewModel> Payments { get; } = new ObservableCollection<PaymentViewModel>();

        private DocumentViewModel _selectedDocument;

        private readonly IDocumentService _documentService;
        private readonly ILoanService _loanService;
        private readonly IInsuranceService _insuranceService;
        private readonly IPawnedItemService _pawnedItemService;
        private readonly IDocumentNumberService _documentNumberService;
        private readonly IPaymentService _paymentService;

        public ICommand OpenAddDocumentFormCommand { get; }
        public ICommand AddPaymentCommand { get; }

        public DocumentViewModel SelectedDocument
        {
            get => _selectedDocument;
            set
            {
                Document document = new Document()
                {
                    UUID = value.UUID,
                    Comment = value.Comment,
                    CustomerUUID = value.CustomerUUID,
                    Name = value.Name,
                    Number = value.Number
                };
                DocumentViewModel doc = new(document);
                SetPawnedItems(doc.PawnedItems);
                SetLoans(doc.LoansViewModel);
                SetPayments(doc.PaymentViewModels);
                _selectedDocument = value;
                RaisePropertiesChanged(nameof(SelectedDocument));
            }
        }

        void SetPawnedItems(List<PawnedItemViewModel> items)
        {
            PawnedItems.Clear();
            foreach (var item in items)
            {
                PawnedItems.Add(item);
            }
        }

        void SetLoans(List<LoanViewModel> items)
        {
            Loans.Clear();
            foreach (var item in items)
            {
                Loans.Add(item);
            }
        }


        void SetPayments(List<PaymentViewModel> items)
        {
            Payments.Clear();
            foreach (var item in items)
            {
                item.LoanName = Loans.First().Name;
                Payments.Add(item);
            }
        }


        public DocumentsViewModel()
        {
            _documentService = ServiceProviderContainer.GetService<IDocumentService>();
            _loanService = ServiceProviderContainer.GetService<ILoanService>();
            _insuranceService = ServiceProviderContainer.GetService<IInsuranceService>();
            _pawnedItemService = ServiceProviderContainer.GetService<IPawnedItemService>();
            _documentNumberService = ServiceProviderContainer.GetService<IDocumentNumberService>();
            _paymentService = ServiceProviderContainer.GetService<IPaymentService>();

            foreach (Document document in _documentService.GetAll())
            {
                Documents.Add(new DocumentViewModel(document));
            }

            OpenAddDocumentFormCommand = new DelegateCommand(OpenAddDocumentForm);
            AddPaymentCommand = new DelegateCommand(OpenAddPaymentForm);
        }

        private async void OpenAddPaymentForm()
        {
            Loan loan = _loanService.GetByUUID(Loans.First().UUID);

            if(loan.Status == LoanStatus.Closed)
            {
                MessageBox.Show("Долг выплачен!");
                return;
            }

            AddPayment addPaymentForm = new AddPayment();
            PaymentViewModel paymentViewModel = addPaymentForm.GetPaymentInfo();

            if (paymentViewModel == null)
                return;

            Payment payment = new Payment()
            {
                PaymentAmount = paymentViewModel.PaymentAmount,
                PaymentDate = paymentViewModel.PaymentDate,
                LoanUUID = Loans.First().UUID
            };

            await _paymentService.AddAsync(payment);

            _selectedDocument.PaymentViewModels.Add(new PaymentViewModel(payment));

            SetPayments(_selectedDocument.PaymentViewModels);

            if (Payments.Sum(p => p.PaymentAmount) >= Loans.First().LoanAmount)
            {
                loan.Status = LoanStatus.Closed;
                await _loanService.UpdateAsync(loan);
            }
        }

        private async void OpenAddDocumentForm()
        {
            CreateDocument addCustomerForm = new CreateDocument();
            DocumentViewModel newDocument = addCustomerForm.GetDocumentInfo();

            if(newDocument is null)
                return;

            long number = _documentNumberService.GetNextNumber();
            Document document = new Document()
            {
                CreationDate = DateTime.Now.Date,
                CustomerUUID = newDocument.CustomerUUID,
                Name = newDocument.Name,
                Number = number,
                Comment = newDocument.Comment
            };

            decimal loanAmount = newDocument.PawnedItems.Sum(pi => pi.EstimatedValue);

            Loan loan = new Loan()
            {
                DocumentUUID = document.UUID,
                Status = LoanStatus.Active,
                InterestRate = 5,
                LoanDate = DateTime.Now.Date,
                LoanAmount = loanAmount
            };

            List<PawnedItem> pawnedItems = new List<PawnedItem>();
            foreach(var item in newDocument.PawnedItems)
            {
                pawnedItems.Add(new PawnedItem()
                {
                    Description = item.Description,
                    EstimatedValue = item.EstimatedValue,
                    ExpiryDate = item.ExpiryDate,
                    LoanUUID = loan.UUID,
                    PawnDate = DateTime.Now.Date,
                    IsSold = false
                });
            }

            List<Insurance> insurances = new();
            foreach(var item in pawnedItems)
            {
                insurances.Add(new Insurance()
                {
                    InsuranceAmount = item.EstimatedValue,
                    InsuranceDate = item.PawnDate,
                    PawnedItemUUID = item.UUID,
                });
            }

            //Сохраняем синхронно
            //По хорошему нужно делать транзакцию 
            await _documentService.AddAsync(document);
            await _loanService.AddAsync(loan);

            foreach(var item in pawnedItems)
            {
                await _pawnedItemService.AddAsync(item);
            }

            foreach(var item in insurances)
            {
                await _insuranceService.AddAsync(item);
            }

            var currentDoc = new DocumentViewModel(document);
            Documents.Add(currentDoc);
            SelectedDocument = currentDoc;
        }
    }
}
