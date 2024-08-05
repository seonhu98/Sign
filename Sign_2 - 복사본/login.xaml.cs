using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Sign_1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
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
    /// login.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class login : Page
    {
        User_Info USER_INFO = new User_Info();
        Client TCP_IP = new Client();
        Json_Combine J_COMBINE = new Json_Combine();

        string temp_str = "";

        public login()
        {
            InitializeComponent();
        }

        private void Login1_Click(object sender, RoutedEventArgs e)
        {
            Login_info();
            Login_Check();
           // NavigationService.Navigate(new Uri("/menu.xaml", UriKind.Relative));
        }

        private void Join1_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Join.xaml", UriKind.Relative));
        }

        private void back1_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Start.xaml", UriKind.Relative));
        }

        private void TextBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            IDBox.Text = null;
        }



        void Login_info()
        {
            USER_INFO.ID = IDBox.Text;

            // SecureString을 일반 문자열로 변환
            string password = ConvertSecureStringToString(PWBox.SecurePassword);
            USER_INFO.PW = password;
        }

        void Login_Check()
        {
            bool num = true;

            USER_INFO.SELECT = (int)Check.Login_check;

            if (string.IsNullOrEmpty(USER_INFO.ID) && string.IsNullOrEmpty(USER_INFO.PW))
            {
                MessageBox.Show("아이디 비밀번호 입력");
                num = false;
            }
            else if (string.IsNullOrEmpty(USER_INFO.ID))
            {
                MessageBox.Show("아이디 입력");
                num = false;
            }
            else if (string.IsNullOrEmpty(USER_INFO.PW))
            {
                MessageBox.Show("비밀번호 입력");
                num = false;
            }

            if (num)
            {
                J_COMBINE.json_set(USER_INFO);
                temp_str = TCP_IP.Json_Data_Communication(J_COMBINE.Json_data);

                JObject temp_json = JsonConvert.DeserializeObject<JObject>(temp_str);
               
                if (temp_json["SELECT"].ToObject<int>() == (int)Check.RECV)
                {
                    if (temp_json["RESULT"].ToObject<int>() == (int)Protocol_S.Success_Login)
                    {
                        MainWindow.Main_ID = USER_INFO.ID;
                        NavigationService.Navigate(new Uri("/menu.xaml", UriKind.Relative));
                    }
                    else
                    {
                        MessageBox.Show("아이디 비밀번호 불일치");
                    }
                }
                else
                {
                    MessageBox.Show("실패야 실패");
                }
            }


        }

        private string ConvertSecureStringToString(SecureString secureString)
        {
            if (secureString == null)
                return string.Empty;

            IntPtr bstr = IntPtr.Zero;
            try
            {
                bstr = System.Runtime.InteropServices.Marshal.SecureStringToBSTR(secureString);
                return System.Runtime.InteropServices.Marshal.PtrToStringBSTR(bstr);
            }
            finally
            {
                if (bstr != IntPtr.Zero)
                    System.Runtime.InteropServices.Marshal.FreeBSTR(bstr);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/menu.xaml", UriKind.Relative));
        }
    }
}
