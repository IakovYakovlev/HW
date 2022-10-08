using System.ComponentModel;
using System.Windows;

namespace HW_19.Views
{
    /// <summary>
    /// Interaction logic for AddOrCorrectAnimal.xaml
    /// </summary>
    public partial class AddOrCorrectAnimal : Window
    {
        #region Поля

        public static readonly DependencyProperty IdProperty = DependencyProperty.Register(
            nameof(Id),
            typeof(int),
            typeof(AddOrCorrectAnimal),
            new PropertyMetadata(default(int)));

        [Description("Номер")]
        public int Id { get => (int)GetValue(IdProperty); set => SetValue(IdProperty, value); }

        public static readonly DependencyProperty CNameProperty = DependencyProperty.Register(
            nameof(CName),
            typeof(string),
            typeof(AddOrCorrectAnimal),
            new PropertyMetadata(default(string)));

        [Description("Название")]
        public string CName { get => (string)GetValue(CNameProperty); set => SetValue(CNameProperty, value); }

        public static readonly DependencyProperty CTypeProperty = DependencyProperty.Register(
            nameof(CType),
            typeof(string),
            typeof(AddOrCorrectAnimal),
            new PropertyMetadata(default(string)));

        [Description("Тип")]
        public string CType { get => (string)GetValue(CTypeProperty); set => SetValue(CTypeProperty, value); }

        #endregion

        public AddOrCorrectAnimal()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }
    }
}
