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
    /// Interaction logic for CreateDocument.xaml
    /// </summary>
    public partial class CreateDocument : Window
    {
        public CreateDocument()
        {
            InitializeComponent();
            LoanDatePicker.SelectedDate = DateTime.Now.Date;
        }

        private void CreateDocument_Click(object sender, RoutedEventArgs e)
        {
            if (PawnedItemsList.Items.Count == 0)
            {
                MessageBox.Show("Введите данные о залоговых предметах");
                return;
            }

            DialogResult = true;
            Close();
        }

        public DocumentViewModel GetDocumentInfo()
        {
            bool? result = ShowDialog();

            List<PawnedItemViewModel> pawnedItemsViewModels = new();
            foreach(var item in PawnedItemsList.Items)
            {
                PawnedItemViewModel pawnedItemViewModel = item as PawnedItemViewModel;
                pawnedItemsViewModels.Add(pawnedItemViewModel);
            }

            Guid customerUUID = (CustomerComboBox.SelectedItem as CustomerViewModel).CustomerUUID;

            if (result == true)
            {
                return new DocumentViewModel()
                {
                    Name = DocumentNameTextBox.Text,
                    PawnedItems = pawnedItemsViewModels,
                    CustomerUUID = customerUUID,
                    Comment = DocumentCommentTextBox.Text
                };
            }

            return null;
        }
    }
}
