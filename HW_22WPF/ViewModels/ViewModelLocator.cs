using Microsoft.Extensions.DependencyInjection;

namespace HW_22WPF.ViewModels
{
    internal class ViewModelLocator
    {
        public MainWindowViewModel MainWindowViewModel => App.Host.Services.GetRequiredService<MainWindowViewModel>();
        public AdministratorWindowViewModel AdministratorWindowViewModel => App.Host.Services.GetRequiredService<AdministratorWindowViewModel>();
    }
}
