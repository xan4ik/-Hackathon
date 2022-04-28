using CommandDLL;
using Domain.DTO;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DesktopApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ApiShell shell = new ApiShell();
        public MainWindow()
        {

        }

        private async void Auth(object sender, RoutedEventArgs e) 
        {
            UserProfile profile = new UserProfile()
            {
                Login = "Admin",
                Password = "12345",
                IsBot = false
            };

            await shell.TrySignInAsync(profile);
        }

        private void InfoButtonClick(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new ContractInfo(shell);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new IonInfoPage(shell);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new StatusPage(shell);
        }
    }
}
