using Microsoft.Extensions.DependencyInjection;

namespace HW_17.ViewModels
{
    internal class ViewModelLocator
    {
        public MainWindowViewModel MainWindowViewModel => App.Host.Services.GetRequiredService<MainWindowViewModel>();
        public DBConnectionTestViewModel DBConnectionTestViewModel => App.Host.Services.GetRequiredService<DBConnectionTestViewModel>();
    }
}
