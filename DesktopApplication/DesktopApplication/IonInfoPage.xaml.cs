using CommandDLL;
using CommandDLL.DTO;
using Domain.DTO;
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
    public partial class IonInfoPage : Page
    {
        private ApiShell shell;

        public IonInfoPage(ApiShell shell)
        {
            InitializeComponent();
            this.shell = shell;
        }


        private async void ShowTotalTimeByIonAsync(object sender, RoutedEventArgs e) 
        {
            try
            {
                var result = await shell.GetIonTotalTimeUsingAsync();
                textBox.Text = CreateTextFrom(result);
            }
            catch (Exception exceptino) 
            {
                textBox.Text = exceptino.Message;                              
            }
        }

        private string CreateTextFrom(IEnumerable<TotalIonTimeUsing> result)
        {
            var raw = string.Empty;
            foreach (var item in result)
            {
                raw += $"{item.IonName} \n\t  Количество дней: {item.TotalTime.Days}, время {item.TotalTime.ToString()}\n\n";
            }
            return raw;
        }

        private async void ShowContractsBeginAsync(object sender, RoutedEventArgs e) 
        {
            try
            {
                var result = await shell.GetContractsBeginsAsync();
                textBox.Text = CreateTextFrom(result);
            }
            catch (Exception exceptino)
            {
                textBox.Text = exceptino.Message;
            }
        }

        private string CreateTextFrom(IEnumerable<ContractBegin> result)
        {
            var raw = string.Empty;
            foreach (var item in result)
            {
                raw += $"{item.CompanyName} \n\t  Начало работ: {item.WorkBegin.ToString()}\n\n";
            }
            return raw;
        }
    }
}
