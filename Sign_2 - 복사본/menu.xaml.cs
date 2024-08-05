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
using System.Windows.Shapes;

namespace Sign_1
{
    /// <summary>
    /// menu.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class menu : Page
    {
        public menu()
        {
            InitializeComponent();
        }

        private void C1_Click(object sender, RoutedEventArgs e)
        {
           Window window = new Filming();
            window.Show();
        }

        private void C_2_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Result.xaml", UriKind.Relative));
        }
    }
}
