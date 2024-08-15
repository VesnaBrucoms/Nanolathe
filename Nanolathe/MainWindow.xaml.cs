using Nanolathe.InputOutput;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Nanolathe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //Hpi.Extract("C:\\Users\\etste\\Documents\\Projects\\TA Modding Tool\\testing", "AFark.ufo", "C:\\Users\\etste\\Documents\\Projects\\TA Modding Tool\\testing");
            Text.Read("C:\\Users\\etste\\Documents\\Projects\\TA Modding Tool\\testing\\AFark\\download", "ARMFARK.TDF");
        }
    }
}