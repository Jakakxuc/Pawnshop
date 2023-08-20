using DevExpress.Mvvm;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace PawnshopApp.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private Page Documents;
        private Page Clients;
        private Page Information;
        private Page Loans;
        private Page PawnedItems;
        private Page AuthPage;

        private Page _currentPage;

        public static MainWindowViewModel Instance { get; private set; }

        private static bool _isAuthed;

        public bool IsAuthed
        {
            get => _isAuthed; 
            set 
            { 
                _isAuthed = value;
                RaisePropertiesChanged(nameof(IsAuthed));
                RaisePropertiesChanged(nameof(bAuthPage_Click));
                RaisePropertiesChanged(nameof(bDocumentsPage_Click));
                RaisePropertiesChanged(nameof(bClientsPage_Click));
                RaisePropertiesChanged(nameof(bInformationPage_Click));
                RaisePropertiesChanged(nameof(bLoansPage_Click));
                RaisePropertiesChanged(nameof(bPawnedItemsPage_Click));
            }
        }

        public void SetIsAuthed(bool isAuthed)
        {
            IsAuthed = isAuthed;
        }

        public Page CurrentPage
        {
            get => _currentPage;
            set
            {
                _currentPage = value;
                RaisePropertiesChanged(nameof(CurrentPage));
            }
        }

        public MainWindowViewModel()
        {
            Instance = this;
            Documents = new Pages.Documents();
            Clients = new Pages.Clients();
            Information = new Pages.Information();
            Loans = new Pages.Loans();
            PawnedItems = new Pages.PawnedItems();
            AuthPage = new Pages.AuthPage();

            CurrentPage = AuthPage;
        }

        public ICommand bAuthPage_Click
        {
            get
            {
                return new RelayCommand(() => CurrentPage = AuthPage, () => !IsAuthed, false);
            }
        }

        public ICommand bDocumentsPage_Click
        {
            get
            {
                return new RelayCommand(() => CurrentPage = Documents, () => IsAuthed, false);
            }
        }

        public ICommand bClientsPage_Click
        {
            get
            {
                return new RelayCommand(() => CurrentPage = Clients, () => IsAuthed, false);
            }

        }

        public ICommand bInformationPage_Click
        {
            get
            {
                return new RelayCommand(() => CurrentPage = Information, () => IsAuthed, false);
            }
        }

        public ICommand bLoansPage_Click
        {
            get
            {
                return new RelayCommand(() => CurrentPage = Loans, () => IsAuthed, false);
            }

        }

        public ICommand bPawnedItemsPage_Click
        {
            get
            {
                return new RelayCommand(() => CurrentPage = PawnedItems, () => IsAuthed, false);
            }
        }

    }
}
