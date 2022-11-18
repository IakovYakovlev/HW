using System.ComponentModel;
using System.Windows;

namespace HW_22WPF.Views.Main.Accessory
{
    public partial class AddOrUpdate : Window
    {
        #region Поля

        public static readonly DependencyProperty IdProperty = DependencyProperty.Register(
            nameof(Id),
            typeof(int),
            typeof(AddOrUpdate),
            new PropertyMetadata(default(int)));

        [Description("Номер")]
        public int Id { get => (int)GetValue(IdProperty); set => SetValue(IdProperty, value); }

        public static readonly DependencyProperty SurnameProperty = DependencyProperty.Register(
            nameof(Surname),
            typeof(string),
            typeof(AddOrUpdate),
            new PropertyMetadata(default(string)));

        [Description("Фамилия")]
        public string Surname { get => (string)GetValue(SurnameProperty); set => SetValue(SurnameProperty, value); }

        public static readonly DependencyProperty FullNameProperty = DependencyProperty.Register(
            nameof(FullName),
            typeof(string),
            typeof(AddOrUpdate),
            new PropertyMetadata(default(string)));

        [Description("Имя")]
        public string FullName { get => (string)GetValue(FullNameProperty); set => SetValue(FullNameProperty, value); }

        public static readonly DependencyProperty PatronymicProperty = DependencyProperty.Register(
            nameof(Patronymic),
            typeof(string),
            typeof(AddOrUpdate),
            new PropertyMetadata(default(string)));

        [Description("Отчество")]
        public string Patronymic { get => (string)GetValue(PatronymicProperty); set => SetValue(PatronymicProperty, value); }

        public static readonly DependencyProperty PhoneNumberProperty = DependencyProperty.Register(
            nameof(PhoneNumber),
            typeof(string),
            typeof(AddOrUpdate),
            new PropertyMetadata(default(string)));

        [Description("Номер телефона")]
        public string PhoneNumber { get => (string)GetValue(PhoneNumberProperty); set => SetValue(PhoneNumberProperty, value); }

        public static readonly DependencyProperty AdressProperty = DependencyProperty.Register(
            nameof(Adress),
            typeof(string),
            typeof(AddOrUpdate),
            new PropertyMetadata(default(string)));

        [Description("Адрес")]
        public string Adress { get => (string)GetValue(AdressProperty); set => SetValue(AdressProperty, value); }

        public static readonly DependencyProperty DescriptionProperty = DependencyProperty.Register(
            nameof(Description),
            typeof(string),
            typeof(AddOrUpdate),
            new PropertyMetadata(default(string)));

        [Description("Описание")]
        public string Description { get => (string)GetValue(DescriptionProperty); set => SetValue(DescriptionProperty, value); }

        #endregion
        public AddOrUpdate()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
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
