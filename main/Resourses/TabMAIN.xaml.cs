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
using center;

namespace main.Resourses
{
    /// <summary>
    /// Логика взаимодействия для TabMAIN.xaml
    /// </summary>
    public partial class TabMAIN : UserControl
    {
        public TabMAIN()
        {
            InitializeComponent();
        }
        private void btest(object sender, RoutedEventArgs e)
        {
            vinezi Valute = new vinezi("");
            lbValut.Content = Valute.GetValute();
        }
    }
}
