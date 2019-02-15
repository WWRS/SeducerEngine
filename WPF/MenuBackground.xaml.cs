using System.Windows.Controls;

namespace WPF
{
    public partial class MenuBackground : UserControl
    {
        public MenuBackground()
        {
            InitializeComponent();

            MainGrid.Effect = new GridEffect();
        }
    }
}
