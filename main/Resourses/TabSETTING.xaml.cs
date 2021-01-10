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

namespace main.Resourses
{
    /// <summary>
    /// Логика взаимодействия для TabSETTING.xaml
    /// </summary>
    public partial class TabSETTING : UserControl
    {
        public TabSETTING()
        {
            InitializeComponent();
            styleBox.SelectedIndex = Properties.Settings.Default.theame; //сохр тему
            langBox.SelectedIndex = Properties.Settings.Default.language; //сохр lang
        }

        private void LangSelector(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            string lang = (cb.SelectedItem as ComboBoxItem).Tag.ToString();
            var uri = new Uri("Resourses/Lang" + lang + ".xaml", UriKind.Relative);
            ResourceDictionary resourceDict = Application.LoadComponent(uri) as ResourceDictionary;
            Application.Current.Resources.Clear();
            Application.Current.Resources.MergedDictionaries.Add(resourceDict);
            Properties.Settings.Default.language = cb.SelectedIndex;
            Properties.Settings.Default.Save();
            
        }
        private void ThemeChange(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            string style = (cb.SelectedItem as ComboBoxItem).Tag.ToString();
            // определяем путь к файлу ресурсов
            var uri = new Uri("Resourses/Theme" + style + ".xaml", UriKind.Relative);
            // загружаем словарь ресурсов
            ResourceDictionary resourceDict = Application.LoadComponent(uri) as ResourceDictionary;
            // очищаем коллекцию ресурсов приложения
            Application.Current.Resources.Clear();
            // добавляем загруженный словарь ресурсов
            Application.Current.Resources.MergedDictionaries.Add(resourceDict);
            Properties.Settings.Default.theame = cb.SelectedIndex;
            Properties.Settings.Default.Save();
        }
    }
}
