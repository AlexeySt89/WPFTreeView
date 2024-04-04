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
    /// Логика взаимодействия для DeleteAttributeWindow.xaml
    /// </summary>
    public partial class DeleteAttributeWindow : Window
    {
        public DeleteAttributeWindow()
        {
            InitializeComponent();
            DataContext = new DataManageVM();
        }
    }
}
