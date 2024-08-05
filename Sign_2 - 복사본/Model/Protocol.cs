using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Sign_1.Model
{
    enum Check
    {
        RECV = 12345, Id_check = 100, PHONE_check, New_member, Login_check = 200, Search_check = 300,
        Image_check = 500
    };
    enum Protocol_S
    {
        Success_id_check = 9, Success_phone_check, Success_New_member, Success_Login = 20, Success_Save = 30,

    };
    enum Protocol_F
    {
        Fail_id_check = -9, Fail_phone_check = -10, Fail_New_member = -11, Fail_Login = -20, Fail_Save = -30
    };

    public class User_Info
    {
        public int SELECT { get; set; }
        public string ID { get; set; }
        public string PW { get; set; }
        public string NAME { get; set; }
        public string PHONE_NUM { get; set; }

    }

    public class Image_Data
    {
        public int SELECT { get; set; }
        public string ID { get; set; }
        public string IMAGE_DATA { get; set; }
    }

    public class Data
    {
        public int SELECT { get; set; }
        public string ID { get; set; }
        public string SIGN_LANGUAGE { get; set; }
    }

    public class Result_P
    {
        public int SELECT { get; set; }
        public int RESULT { get; set; }
        public string DATA { get; set; }
    }



    public class Json_Combine
    {
        private JObject _json_data = new JObject();


        public JObject Json_data
        {
            get { return _json_data; }   // get method
            set { _json_data = value; }  // set method
        }

        public void json_set(User_Info j_list)
        {
            Json_data = JObject.FromObject(j_list);
        }

        public void json_set(Image_Data j_list)
        {
            Json_data = JObject.FromObject(j_list);
        }

        public void json_set(Data j_list)
        {
            Json_data = JObject.FromObject(j_list);
        }

    }

}