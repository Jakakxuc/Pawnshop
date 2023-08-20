using PawnshopApp.Entities;
using PawnshopApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PawnshopApp.Pages
{
    /// <summary>
    /// Interaction logic for Loans.xaml
    /// </summary>
    public partial class Loans : Page
    {
        public Loans()
        {
            InitializeComponent();
        }

        private void LoansListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                var selectedLoan = e.AddedItems[0] as LoanViewModel;
                if (selectedLoan != null)
                {
                    var viewModel = (LoansViewModel)DataContext;
                    viewModel.SelectedLoan = selectedLoan;
                }
            }
        }
    }
}
