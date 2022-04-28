using CommandDLL;
using CommandDLL.DTO;
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
    /// Логика взаимодействия для IonInfoPage.xaml
    /// </summary>
    public partial class IonInfoPage : Page
    {
        private ApiShell shell;

        public IonInfoPage()
        {
            InitializeComponent();
        }


        private async Task ShowTotalTimeByIonAsync() 
        {
            try
            {
                var result = await shell.GetIonTotalTimeUsingAsync();
                textBox.Text = CreateTextFrom(result);
            }
            catch (Exception e) 
            {
                textBox.Text = e.Message;                              
            }
        }

        private string CreateTextFrom(IEnumerable<TotalIonTimeUsing> result)
        {
            var result = string.Empty;
            foreach (var item in result)
            {

            }

        }

        private void ShowContractsBegin() 
        {
        
        }
    }
}
