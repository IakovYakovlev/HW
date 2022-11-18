using HW_22WPF.Services;
using IdentityShared;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;


namespace HW_22WPF.Views.Main.Accessory
{
    public partial class LoginWindow : Window
    {
        public string UserEmail { get; set; }

        public string Token { get; set; }

        public string UserRole { get; set; }

        public LoginWindow()
        {
            InitializeComponent();
        }

        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            IUserService _userService = App.Host.Services.GetRequiredService<UserService>();

            var model = new LoginViewModel
            {
                Email = txtEmail.Text,
                Password = txtPassword.Password
            };

            var responseObject = _userService.LoginUserAsync(model).Result;

            if (responseObject.IsSuccess)
            {
                UserEmail = txtEmail.Text;
                Token = responseObject.Message;
                UserRole = responseObject.UserRole;

                this.DialogResult = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Ошибка при входе!", "Информация", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
