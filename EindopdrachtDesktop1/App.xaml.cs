using EindopdrachtDesktop1.View;
using EindopdrachtDesktop1.ViewModel;
using System.Configuration;
using System.Data;
using System.Windows;


namespace EindopdrachtDesktop1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            MainWindow = new MainView()
            {
                DataContext = new MainViewModel()
            };
            MainWindow.Show();
        }

    }

}
