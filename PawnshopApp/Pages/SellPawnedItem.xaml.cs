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
    /// Interaction logic for SellPawnedItem.xaml
    /// </summary>
    public partial class SellPawnedItem : Window
    {
        public SellPawnedItem()
        {
            InitializeComponent();
        }

        private void Sell_Click(object sender, RoutedEventArgs e)
        {
            string sellAmount = SellAmountTextBox.Text;

            if (string.IsNullOrEmpty(sellAmount) || !decimal.TryParse(sellAmount, out decimal result))
            {
                MessageBox.Show("Введите число");
                return;
            }

            DialogResult = true;
            Close();
        }

        public decimal GetSellPrice()
        {
            bool? result = ShowDialog();
            if (result == true)
            {
               return decimal.Parse(SellAmountTextBox.Text);
            }

            return -1;
        }
    }
}
