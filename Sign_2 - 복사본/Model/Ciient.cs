using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.InteropServices.ComTypes;
using System.Windows;

namespace Sign_1.Model
{
    internal class Client
    {

        TcpClient tcpClient;
        NetworkStream stream;

        JObject j_file = new JObject();

        private void P_Server_connect()
        {
            tcpClient = new TcpClient("10.10.21.102", 12345);
            stream = tcpClient.GetStream();
        }
        private void C_Server_connect()
        {
            tcpClient = new TcpClient("10.10.21.120", 12345);
            stream = tcpClient.GetStream();
        }

        public static void Json_DataWrite(NetworkStream stream, JObject j_file)
        {
            string json = JsonConvert.SerializeObject(j_file);
            byte[] send_data = Encoding.UTF8.GetBytes(json);
            stream.WriteAsync(send_data, 0, send_data.Length);
        }

        public static string Read(NetworkStream stream)
        {
            byte[] recv_data = new byte[2000];
            int bytes = stream.Read(recv_data, 0, recv_data.Length);
            string responseData = Encoding.UTF8.GetString(recv_data, 0, bytes);

            return responseData;
        }

        public string Json_Data_Communication(JObject j_file)
        {
            C_Server_connect();

            Json_DataWrite(stream, j_file);
            string Read_data = Read(stream);
            TCP_close();

            return Read_data;
        }

        public void Image_DataWrite(string imagePath)
        {
            byte[] imageBytes = File.ReadAllBytes(imagePath);
            int imageSize = imageBytes.Length;

            byte[] imageSizeBytes = BitConverter.GetBytes(imageSize);
            stream.WriteAsync(imageSizeBytes, 0, imageSizeBytes.Length);
          
            stream.WriteAsync(imageBytes, 0, imageBytes.Length);

        }

        public string Image_Data_Communication(string imagePath)
        {
            P_Server_connect();
            Image_DataWrite(imagePath);
            string Read_data = Read(stream);
            TCP_close();

            return Read_data;
        }

        public void TCP_close()
        {
            stream.Close();
            tcpClient.Close();
        }
    }
}