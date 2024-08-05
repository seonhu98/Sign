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
using System.Windows.Navigation;
using System.Windows.Shapes;

// OpenCV 사용을 위한 using
using OpenCvSharp;
using OpenCvSharp.WpfExtensions;

// Timer 사용을 위한 using
using System.Windows.Threading;
using Sign_1.Model;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Sign_1
{
    // OpenCvSharp 설치 시 Window를 명시적으로 사용해 주어야 함 (window -> System.Windows.Window)
    public partial class Filming : System.Windows.Window
    {
        // 필요한 변수 선언
        VideoCapture cam;
        Mat frame;
        DispatcherTimer timer;
        bool is_initCam, is_initTimer;

        Client TCP_IP = new Client();
        Json_Combine J_COMBINE = new Json_Combine();
        Image_Data image_data = new Image_Data();

        string temp_str = "";

        public Filming()
        {
            InitializeComponent();
        }

        private void windows_loaded(object sender, RoutedEventArgs e)
        {
            // 카메라, 타이머(0.01ms 간격) 초기화
            is_initCam = init_camera();
            is_initTimer = init_Timer(0.01);

            // 초기화 완료면 타이머 실행
            if (is_initTimer && is_initCam) timer.Start();
        }

        private bool init_Timer(double interval_ms)
        {
            try
            {
                timer = new DispatcherTimer();

                timer.Interval = TimeSpan.FromMilliseconds(interval_ms);
                timer.Tick += new EventHandler(timer_tick);

                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool init_camera()
        {
            try
            {
                // 0번 카메라로 VideoCapture 생성 (카메라가 없으면 안됨)
                cam = new VideoCapture(0);
                cam.FrameHeight = 350;
                cam.FrameWidth = 600;

                // 카메라 영상을 담을 Mat 변수 생성
                frame = new Mat();

                return true;
            }
            catch
            {
                return false;
            }
        }

        private void timer_tick(object sender, EventArgs e)
        {
            // 0번 장비로 생성된 VideoCapture 객체에서 frame을 읽어옴
            cam.Read(frame);
            // 읽어온 Mat 데이터를 Bitmap 데이터로 변경 후 컨트롤에 그려줌
            Cam_1.Source = OpenCvSharp.WpfExtensions.WriteableBitmapConverter.ToWriteableBitmap(frame);
        }

        private void back1_Click(object sender, RoutedEventArgs e)
        {
            menu mpage = new menu();
            mpage.Title = "menu";
            this.Content = mpage;
        }

        private void upload_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string save_name = DateTime.Now.ToString("yyyy-MM-dd-hh시mm분ss초");
                string file_path = save_name + "_image.jpg";
                OpenCvSharp.Size size = new OpenCvSharp.Size(150, 150);
                Cv2.Resize(frame, frame, (size));
                Cv2.ImWrite(file_path, frame);
                MessageBox.Show("이미지가 저장되었습니다: " + file_path, "저장 완료", MessageBoxButton.OK, MessageBoxImage.Information);
                Mat src = Cv2.ImRead(file_path);
      
                Cv2.ImShow("src", src);
                Cv2.WaitKey(0);
                Cv2.DestroyAllWindows();

                TCP_IP.Image_Data_Communication(file_path);

                Cpp_save_image(file_path);
                J_COMBINE.json_set(image_data);
                temp_str = TCP_IP.Json_Data_Communication(J_COMBINE.Json_data);
                JObject temp_json = JsonConvert.DeserializeObject<JObject>(temp_str);
                MessageBox.Show($"temp_json : {temp_json}");

            }
            catch (Exception ex)
            {
                MessageBox.Show("이미지 저장 중 오류가 발생했습니다: " + ex.Message, "오류", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        void Cpp_save_image(string file_path)
        {
            image_data.SELECT = (int)Check.Image_check;
            image_data.ID = MainWindow.Main_ID;
            image_data.IMAGE_DATA = file_path;
        }

    }
}