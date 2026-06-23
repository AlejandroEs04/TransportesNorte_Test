using System.Windows.Controls;
using System.Windows.Input;

namespace ADN_Test.Views
{
    public partial class DashboardView : UserControl
    {
        public DashboardView()
        {
            InitializeComponent();
        }

        private void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is ViewModels.DashboardViewModel vm && sender is DataGrid dg && dg.SelectedItem is Models.Embarque embarque)
            {
                if (vm.AbrirDetalleCommand.CanExecute(embarque))
                    vm.AbrirDetalleCommand.Execute(embarque);
            }
        }
    }
}
