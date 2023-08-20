using PawnshopApp.Services.BusinessLogic;
using PawnshopApp.Services.BusinessLogic.Interfaces;
using PawnshopApp.Utils;
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
    /// Interaction logic for AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page
    {

        private readonly IAuthenticationService _authenticationService;

        public AuthPage()
        {
            InitializeComponent();
            _authenticationService = ServiceProviderContainer.GetService<IAuthenticationService>();
        }

        private async void GetPasswordMask_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string username = LoginTextBox.Text;
                if (string.IsNullOrEmpty(username))
                {
                    MessageBox.Show("Введите логин");
                        }

                var mask = await _authenticationService.GetPasswordMaskAsync(username);


                MessageBox.Show($"Введите следующии позиции пароля: {string.Join(" ,", mask.Select(i => i + 1).ToArray())}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string username = LoginTextBox.Text;
                string password = PasswordBox.Password;

                await _authenticationService.CheckLoginAndPasswordAsync(username, password);

                MessageBox.Show("Авторизация прошла успешно");
                var mainWindow = Application.Current.MainWindow as MainWindow;
                MainWindowViewModel.Instance.SetIsAuthed(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private async void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string username = LoginTextBox.Text;
                string password = PasswordBox.Password;

                if(string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                {
                    MessageBox.Show("Введите логин и пароль");
                    return;
                }

                await _authenticationService.RegisterUserAsync(username, password);
                MessageBox.Show("Вы зарегистрированы");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }
    }
}
