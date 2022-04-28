using CommandDLL;
using System;
using System.Collections.Generic;
using System.Text;
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
    /// Логика взаимодействия для ContractInfo.xaml
    /// </summary>
    public partial class ContractInfo : Page
    {
        private ApiShell shell;
        private bool isIonInfo;

        public ContractInfo(ApiShell shell)
        {
            InitializeComponent();
            this.shell = shell;
        }

        private void ShowIonInfoAsync(object sender, RoutedEventArgs e)
        {
            textBox.Text = "Ведите имя иона";
            isIonInfo = true;
        }

        private void ShowIonTimeWorkAsync(object sender, RoutedEventArgs e)
        {
            textBox.Text = "Ведите имя иона";
            isIonInfo = false;
        }

        public async void GetTotalTBAsync(object sender, RoutedEventArgs e)
        {
            var result = await shell.GetTotalTbAsync();
            textBox.Text = result.ToString();
        }

        private async void ExecuteAsync(object sender, RoutedEventArgs e)
        {
            try
            {
                if (isIonInfo)
                {
                    var result = await shell.GetIonShortInfoAsync(argsBox.Text);
                    textBox.Text = $"Название: {result.IonName}\nТип: {result.Isotope}\nПробег в кремнии: {result.DistanceSI}\n";
                }
                else
                {
                    var result = await shell.GetContractsTimeworkAsync(argsBox.Text);
                    textBox.Text = GetStringFrom(result);
                }
            }
            catch (Exception exception)
            {
                textBox.Text = exception.Message;
            }
        }

        private string GetStringFrom(IEnumerable<CommandDLL.DTO.ContractTimeWorkByIon> result)
        {
            var raw = string.Empty;
            foreach (var item in result)
            {
                raw += $" Организация: {item.CompanyName}\n\t Время работы: {item.TotalTimeSpan.Days} дней, {item.TotalTimeSpan.ToString()}\n\n";
            }
            return raw;
        }
    }
}
