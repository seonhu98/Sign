using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Sign_1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Security;
using System.Text.RegularExpressions;

namespace Sign_1
{
    /// <summary>
    /// Join.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Join : Page
    {

        User_Info USER_INFO = new User_Info();
        Client TCP_IP = new Client();
        Json_Combine J_COMBINE = new Json_Combine();

        string temp_str = "";
        static int[] count = new int[4];

        public Join()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PW_Overlap();
            NAME_Overlap();

            if (count[0] == 0)
            {
                MessageBox.Show("아이디 입력");
            }
            if (count[1] == 0)
            {
                MessageBox.Show("비밀번호 입력");
            }
            if (count[2] == 0)
            {
                MessageBox.Show("이름 입력");
            }
            if (count[3] == 0)
            {
                MessageBox.Show("핸드폰 번호 입력");
            }

            int sum = count[0] + count[1] + count[2] + count[3];

            if(sum == 4)
            {
                USER_INFO.SELECT = (int)Check.New_member;

                USER_INFO.ID = IDBox.Text;

                string password = ConvertSecureStringToString(PWBox.SecurePassword);
                USER_INFO.PW = password;

                USER_INFO.NAME = NAMEBox.Text;
                USER_INFO.PHONE_NUM = NUMBERBox.Text;

                J_COMBINE.json_set(USER_INFO);
                temp_str = TCP_IP.Json_Data_Communication(J_COMBINE.Json_data);

                JObject temp_json = JsonConvert.DeserializeObject<JObject>(temp_str);

                if (temp_json["RESULT"].ToObject<int>() == (int)Protocol_S.Success_New_member)
                {
                    IDBox.IsEnabled = true;
                    ID_Check.IsEnabled = true;
                    NUMBERBox.IsEnabled = true;
                    PHONE_Check.IsEnabled = true;


                    IDBox.Text = string.Empty;
                    PWBox.Password = string.Empty;  // SecureString을 Password 속성으로 설정
                    NAMEBox.Text = string.Empty;
                    NUMBERBox.Text = string.Empty;


                    MessageBox.Show("회원 가입 성공");
                }
                else
                {
                    MessageBox.Show("회원 가입 실패");
                }
            }
            else
            {
                MessageBox.Show("회원 조건 확인");
            }

        }

        private void back1_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Start.xaml", UriKind.Relative));
        }

        private void IBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            IDBox.Text = null;
        }

        private void PWBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            PWBox.Password = string.Empty;  // SecureString을 Password 속성으로 설정
        }

        private void NameBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            NAMEBox.Text = null;
        }

        private void NumberBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            NUMBERBox.Text = null;
        }


        private void ID_Check_Click(object sender, RoutedEventArgs e)
        {
            ID_Overlap();
        }

        private void PHONE_Check_Click(object sender, RoutedEventArgs e)
        {
            PHONE_Overlap();
        }


        void ID_Overlap()
        {
            USER_INFO.SELECT = (int)Check.Id_check;
            USER_INFO.ID = IDBox.Text;

            bool hasSpaces = USER_INFO.ID.Contains(" ");

            if (hasSpaces)
            {
                MessageBox.Show("공백 제거 해야됨");
                return;
            }


            J_COMBINE.json_set(USER_INFO);
            temp_str = TCP_IP.Json_Data_Communication(J_COMBINE.Json_data);

            JObject temp_json = JsonConvert.DeserializeObject<JObject>(temp_str);

            if (temp_json["SELECT"].ToObject<int>() == (int)Check.RECV)
            {
                if (temp_json["RESULT"].ToObject<int>() == (int)Protocol_S.Success_id_check)
                {
                    IDBox.IsEnabled = false;
                    ID_Check.IsEnabled = false;
                    MessageBox.Show("중복 아님");
                    count[0] = 1;
                }
                else
                {
                    MessageBox.Show("중복 다시 입력");
                }
            }
            else
            {
                MessageBox.Show("실패야 실패");
            }
        }

        void PW_Overlap()
        {

            USER_INFO.NAME = NAMEBox.Text;

            bool hasSpaces = USER_INFO.NAME.Contains(" ");

            if (hasSpaces)
            {
                MessageBox.Show("공백 제거 해야됨");
                return;
            }

            if (!string.IsNullOrWhiteSpace(USER_INFO.NAME))
            {
                count[1] = 1;
            }
            else
            {
                count[1] = 0;
            }
        }

        void NAME_Overlap()
        {

            // SecureString을 일반 문자열로 변환
            string password = ConvertSecureStringToString(PWBox.SecurePassword);
            USER_INFO.PW = password;

            bool hasSpaces = USER_INFO.PW.Contains(" ");

            if (hasSpaces)
            {
                MessageBox.Show("공백 제거 해야됨");
                return;
            }

            if (!string.IsNullOrWhiteSpace(USER_INFO.PW))
            {
                count[2] = 1;
            }
            else
            {
                count[2] = 0;
            }
        }

        void PHONE_Overlap()
        {
            USER_INFO.SELECT = (int)Check.PHONE_check;
            USER_INFO.PHONE_NUM = NUMBERBox.Text;

            bool hasSpaces = USER_INFO.PHONE_NUM.Contains(" ");

            if (hasSpaces)
            {
                MessageBox.Show("공백 제거 해야됨");
                return;
            }

            // 주민등록번호 형식 검증
            if (!IsValidPhoneNumber(USER_INFO.PHONE_NUM))
            {
                MessageBox.Show("올바른 주민등록번호 형식이 아닙니다.");
                return;
            }


            J_COMBINE.json_set(USER_INFO);
            temp_str = TCP_IP.Json_Data_Communication(J_COMBINE.Json_data);

            JObject temp_json = JsonConvert.DeserializeObject<JObject>(temp_str);

            if (temp_json["SELECT"].ToObject<int>() == (int)Check.RECV)
            {
                if (temp_json["RESULT"].ToObject<int>() == (int)Protocol_S.Success_phone_check)
                {
                    NUMBERBox.IsEnabled = false;
                    PHONE_Check.IsEnabled = false;
                    MessageBox.Show("중복 아님");
                    count[3] = 1;
                }
                else
                {
                    count[3] = 0;
                    MessageBox.Show("중복 다시 입력");
                }
            }
            else
            {
                MessageBox.Show("실패야 실패");
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

        private bool IsValidPhoneNumber(string input)
        {
            // 정규 표현식 패턴 (하이픈이 있는 경우와 없는 경우를 모두 허용)
            string pattern = @"^01[0-9]-?\d{4}-?\d{4}$";

            // 정규 표현식을 사용하여 패턴 매칭 확인
            return Regex.IsMatch(input, pattern);
        }

        
    }
}
