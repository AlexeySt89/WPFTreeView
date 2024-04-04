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
using WPFTreeView.ViewModel;

namespace WPFTreeView.View
{

    public partial class MainWindow : Window
    {
        public static TreeView AllObjects;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new DataManageVM();
            AllObjects = ViewAllObjects;
        }
    }
}