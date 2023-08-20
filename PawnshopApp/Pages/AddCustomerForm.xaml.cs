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
using System.Windows.Shapes;

namespace PawnshopApp.Pages
{
    /// <summary>
    /// Interaction logic for AddCustomerForn.xaml
    /// </summary>
    public partial class AddCustomerForm : Window
    {
        public AddCustomerForm()
        {
            InitializeComponent();
        }

        private void AddCustomer_Click(object sender, RoutedEventArgs e)
        {
            string name = NameTextBox.Text;
            string surname = SurnameTextBox.Text;
            string phoneNumber = PhoneNumberTextBox.Text;

            if(string.IsNullOrEmpty(name) || string.IsNullOrEmpty(surname) || string.IsNullOrEmpty(phoneNumber)) 
            {
                MessageBox.Show("Введите данные");
                return;
            }

            DialogResult = true;
            Close();
        }

        public CustomerViewModel GetCustomerInfo()
        {
            bool? result = ShowDialog();
            if (result == true)
            {
                return new CustomerViewModel()
                {
                    Name = NameTextBox.Text,
                    Surname = SurnameTextBox.Text,
                    PhoneNumber = PhoneNumberTextBox.Text,
                };
            }
            return null;
        }
    }
}
