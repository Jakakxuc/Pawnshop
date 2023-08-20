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
    /// Interaction logic for AddPawnedItem.xaml
    /// </summary>
    public partial class AddPawnedItem : Window
    {
        public AddPawnedItem()
        {
            InitializeComponent();
        }

        private void AddPawnedItem_Click(object sender, RoutedEventArgs e)
        {
            string estimatedValue = EstimatedValueTextBox.Text;
            string description = DescriptionTextBox.Text;

            if (string.IsNullOrEmpty(estimatedValue) || string.IsNullOrEmpty(description))
            {
                MessageBox.Show("Введите данные");
                return;
            }

            if (!decimal.TryParse(estimatedValue, out decimal result))
            {
                MessageBox.Show("Неверный формат оценки");
                return;
            }

            DialogResult = true;
            Close();
        }

        public PawnedItemViewModel GetPawnedItemInfo()
        {
            bool? result = ShowDialog();
            if (result == true)
            {
                return new PawnedItemViewModel()
                {
                    Description = DescriptionTextBox.Text,
                    EstimatedValue = decimal.Parse(EstimatedValueTextBox.Text)
                };
            };

            return null;
        }
    }
}
