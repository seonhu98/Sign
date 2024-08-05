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
    /// Result.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Result : Page
    {
        public Result()
        {
            InitializeComponent();
        }

        private void back1_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/menu.xaml", UriKind.Relative));
        }
    }
}
