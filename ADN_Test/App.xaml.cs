using ADN_Test.Service;
using ADN_Test.ViewModels;
using System.Configuration;
using System.Data;
using System.Windows;

namespace ADN_Test
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            IEmbarqueService embarqueService = new EmbarqueService();

            var mainViewModel = new MainViewModel(embarqueService);

            var mainWindow = new Views.MainWindow
            {
                DataContext = mainViewModel
            };

            mainWindow.Show();
        }
    }

}
