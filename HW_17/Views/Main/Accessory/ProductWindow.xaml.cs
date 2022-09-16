using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace HW_17.Views.Main.Accessory
{
    /// <summary>
    /// Interaction logic for ProductWindow.xaml
    /// </summary>
    public partial class ProductWindow : Window
    {
        #region Поля

        public static readonly DependencyProperty IDProperty = DependencyProperty.Register(
            nameof(ID),
            typeof(int),
            typeof(ProductWindow),
            new PropertyMetadata(default(int)));

        [Description("Номер")]
        public int ID { get => (int)GetValue(IDProperty); set => SetValue(IDProperty, value); }

        public static readonly DependencyProperty EmailProperty = DependencyProperty.Register(
            nameof(Email),
            typeof(string),
            typeof(ProductWindow),
            new PropertyMetadata(default(string)));

        [Description("Email")]
        public string Email { get => (string)GetValue(EmailProperty); set => SetValue(EmailProperty, value); }

        public static readonly DependencyProperty ProductCodeProperty = DependencyProperty.Register(
            nameof(ProductCode),
            typeof(string),
            typeof(ProductWindow),
            new PropertyMetadata(default(string)));

        [Description("Код товара")]
        public string ProductCode { get => (string)GetValue(ProductCodeProperty); set => SetValue(ProductCodeProperty, value); }

        public static readonly DependencyProperty ProductNameProperty = DependencyProperty.Register(
            nameof(ProductName),
            typeof(string),
            typeof(ProductWindow),
            new PropertyMetadata(default(string)));

        [Description("Название товара")]
        public string ProductName { get => (string)GetValue(ProductNameProperty); set => SetValue(ProductNameProperty, value); }

        #endregion

        public ProductWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(ProductCode) || String.IsNullOrEmpty(ProductName))
            {
                MessageBox.Show("Необходимо заполнить все поля", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            DialogResult = true;
            Close();
        }
    }
}
