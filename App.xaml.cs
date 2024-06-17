using System.Configuration;
using System.Data;
using System.Windows;

namespace Pokemons
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show("Произошла ошибка: "+e.Exception.Message, "Exeption Sample",
                MessageBoxButton.OK, MessageBoxImage.Error);
            e.Handled = true;
        }
    }

}
