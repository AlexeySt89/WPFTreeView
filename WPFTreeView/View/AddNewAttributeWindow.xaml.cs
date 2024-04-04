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
using System.Windows.Shapes;
using WPFTreeView.ViewModel;

namespace WPFTreeView.View
{
    /// <summary>
    /// Логика взаимодействия для AddNewAttributeWindow.xaml
    /// </summary>
    public partial class AddNewAttributeWindow : Window
    {
        public AddNewAttributeWindow()
        {
            InitializeComponent(); 
            DataContext = new DataManageVM();
        }
    }
}
