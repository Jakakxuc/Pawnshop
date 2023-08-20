using DevExpress.Mvvm;
using PawnshopApp.Entities;
using PawnshopApp.Services.Interfaces;
using PawnshopApp.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PawnshopApp.ViewModel
{
    public class AddLoanViewModel : ViewModelBase
    {
        public decimal LoanAmount { get; set; }
        public decimal InterestRate { get; set; }
        public DateTime LoanDate { get; set; }

        public AddLoanViewModel()
        {
            LoanDate = DateTime.Now; // Set default loan date to current date
        }

        public ICommand AddLoanCommand => new DelegateCommand(AddLoan);

        private void AddLoan()
        {
            Loan newLoan = new Loan
            {
                LoanAmount = LoanAmount,
                InterestRate = InterestRate,
                LoanDate = LoanDate,
            };

            CloseWindow();
        }

        private void CloseWindow()
        {
            // Implement logic to close the window
            // You can use the Messenger or other patterns to communicate with the view
        }
    }
}
