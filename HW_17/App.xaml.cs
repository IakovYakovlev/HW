using System;
using Microsoft.Extensions.Hosting;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Runtime.CompilerServices;
using HW_17.ViewModels;
using HW_17.Models.Access;
using HW_17.Models.SQL;
using HW_17.Services;
using HW_17.Data;
using HW_17.Models.DataContext;

namespace HW_17
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static bool IsDesignMode { get; private set; }

        private static IHost __host;

        public static IHost Host => __host ?? Program.CreateHostBuilder(Environment.GetCommandLineArgs()).Build();

        protected override async void OnStartup(StartupEventArgs e)
        {
            IsDesignMode = false;
            var host = Host;
            base.OnStartup(e);

            await host.StartAsync().ConfigureAwait(false);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            var host = Host;
            await host.StopAsync().ConfigureAwait(false);
            host.Dispose();
            __host = null;
        }

        public static void ConfigureServices(HostBuilderContext host, IServiceCollection services) => services
            .AddDatabase(host.Configuration.GetSection("Data"))
            .AddSingleton<MainWindowViewModel>()
            .AddSingleton<DBConnectionTestViewModel>()
            .AddTransient<IDataRepository<Product>, AccessDataRepository>()
            .AddTransient<IDataRepository<Person>, SQLDataRepository>()
            ;

        // Меняем в дизайнере папку по умолчанию
        public static string CurrentDirectory => IsDesignMode
            ? Path.GetDirectoryName(GetSourceCodePath())
            : Environment.CurrentDirectory;

        private static string GetSourceCodePath([CallerFilePath]string path = null) => path;
    }
}
