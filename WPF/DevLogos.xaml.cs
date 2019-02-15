using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace WPF
{
    public partial class DevLogos : UserControl
    {
        private readonly Action _callback;
        
        public DevLogos(Action callback)
        {
            InitializeComponent();

            _callback = callback;
            
            Img.Source = new BitmapImage(new Uri(Path.Combine("file:///", Directory.GetCurrentDirectory(), @"Assets\logo.png")));
        }

        private void FadeComplete(object sender, EventArgs e)
        {
            _callback.Invoke();
        }
    }
}
