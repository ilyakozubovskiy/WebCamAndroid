using System.IO;
using System.Net.Sockets;
using Xamarin.Forms;

namespace WebCamAndroid
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            image.Source = ImageSource.FromFile("mount.jpg");
            label.HorizontalTextAlignment = TextAlignment.Center;
            label.Text = "Waiting for images from Web-camera";
            Image_Loaded();
        }
        private async void Image_Loaded()
        {
            using (UdpClient client = new UdpClient(48654))
            {
                double size = 0;
                while (true)
                {
                    var data = await client.ReceiveAsync();
                    MemoryStream ms = new MemoryStream(data.Buffer);
                    image.Source = ImageSource.FromStream(() => ms);
                    size += data.Buffer.Length / 1048576d;
                    label.HorizontalTextAlignment = TextAlignment.Start;
                    label.Text = $"Bytes recieved: {size:0.0}";
                }
            }
        }
    }
}
