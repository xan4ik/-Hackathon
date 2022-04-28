using CommandDLL;
using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для StatusPage.xaml
    /// </summary>
    public partial class StatusPage : Page
    {
        private ApiShell shell;
        private bool isReport;

        public StatusPage(ApiShell shell)
        {
            InitializeComponent();
            this.shell = shell;
        }

        private void ShowSessionStatusAsync(object sender, RoutedEventArgs e)
        {
            textBox.Text = "Ведите номер сеанса и нажмите кнопку выполнить";
            isReport = true;
        }

        private void ShowSessionBeginAsync(object sender, RoutedEventArgs e) 
        {
            textBox.Text = "Ведите номер сеанса и нажмите кнопку выполнить";
            isReport = false;
        }

        public void DocumentInfo(object sender, RoutedEventArgs e) 
        {
            textBox.Text = "xx-xxxx";
        }

        private async void ExecuteAsync(object sender, RoutedEventArgs e) 
        {
            try
            {
                if (int.TryParse(argsBox.Text, out int value))
                {
                    if (isReport)
                    {
                        var result = await shell.GetSessionReportAsync(value);
                        textBox.Text = $"Сессия №{result.SessionNumber}\nCостояние: {result.Status}";
                    }
                    else 
                    {
                        var result = await shell.GetSessionBeginAsync(value);
                        textBox.Text = $"Сессия №{value}\nНачало сеанса: {result.ToString()}";
                    }
                }
                else
                {
                    textBox.Text = "Указан не правильный номер";
                }
            }
            catch (Exception exception) 
            {
                textBox.Text = exception.Message;
            }
        }
    }
}
