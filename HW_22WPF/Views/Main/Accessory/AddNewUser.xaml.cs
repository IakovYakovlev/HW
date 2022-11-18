using HW_22WPF.Services;
using IdentityShared;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text.RegularExpressions;
using System.Windows;

namespace HW_22WPF.Views.Main.Accessory
{
    /// <summary>
    /// Interaction logic for AddNewUser.xaml
    /// </summary>
    public partial class AddNewUser : Window
    {
        public AddNewUser()
        {
            InitializeComponent();
        }

        private async void btnOk_Click(object sender, RoutedEventArgs e)
        {
            if(String.IsNullOrEmpty(txtEmail.Text.Trim()) 
                || String.IsNullOrEmpty(txtPassword.Password.Trim())
                || String.IsNullOrEmpty(txtPasswordConfirmation.Password.Trim()))
            {
                MessageBox.Show("Необходимо заполнить все поля.", "Информация", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string email = txtEmail.Text;
            Regex regex = new Regex(@"^[\w-]+(\.[\w-]+)*@([a-z0-9-]+(\.[a-z0-9-]+)*?\.[a-z]{2,6}|(\d{1,3}\.){3}\d{1,3})(:\d{4})?$");
            Match match = regex.Match(email);

            if (!match.Success)
            {
                MessageBox.Show("\"Поле электронная почта должно содержать @. Пример -> \\\"name@name.ru\\\"\"", "Информация", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (txtPassword.Password != txtPasswordConfirmation.Password)
            {
                MessageBox.Show("Пароли не совпадают.", "Информация", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            IUserService _userService = App.Host.Services.GetRequiredService<UserService>();

            var user = new RegisterViewModel
            {
                Email = txtEmail.Text,
                Password = txtPassword.Password,
                ConfirmPassword = txtPasswordConfirmation.Password
            };

            await _userService.RegisterUserAsync(user);

            this.DialogResult = true;
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
