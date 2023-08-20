using Microsoft.IdentityModel.Tokens;
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
using System.Windows.Shapes;

namespace PawnshopApp.Pages
{
    /// <summary>
    /// Interaction logic for AddPaymenr.xaml
    /// </summary>
    public partial class AddPayment : Window
    {
        public AddPayment()
        {
            InitializeComponent();
            PaymentDatePicker.SelectedDate = DateTime.Now.Date;
        }

        private void AddPayment_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(PaymentAmountTextBox.Text) || !decimal.TryParse(PaymentAmountTextBox.Text, out decimal result))
            {
                MessageBox.Show("Неверные данные");
                return;
            }

            DialogResult = true;
            Close();
        }

        public PaymentViewModel GetPaymentInfo()
        {
            bool? result = ShowDialog();
            if (result == true)
            {
                return new PaymentViewModel()
                {
                    PaymentAmount = decimal.Parse(PaymentAmountTextBox.Text),
                    PaymentDate = PaymentDatePicker.SelectedDate.GetValueOrDefault(),
                };
            }
            return null;
        }
    }
}
