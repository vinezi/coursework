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
using System.Globalization;

namespace main.Resourses
{
    /// <summary>
    /// Логика взаимодействия для TabSETTING.xaml
    /// </summary>
    public partial class TabSETTING : UserControl
    {
        string language;
        public TabSETTING()
        {
            language = "ru-RU";
            InitializeComponent();

            List<string> styles = new List<string> { "Resourses/ThemeLIGHT", "Resourses/ThemeDARK" };
            styleBox.SelectionChanged += ThemeChange;
            //styleBox.ItemsSource = styles;
            //styleBox.SelectedItem = "Resourses/ThemeDARK";
        }

        private void CbLangSelector(object sender, SelectionChangedEventArgs e)
        {

            ComboBox cb = sender as ComboBox;
            language = (cb.SelectedItem as ComboBoxItem).Tag.ToString();

            if (language != null)
            {
                CultureInfo lang = new CultureInfo(language);

                if (lang != null)
                {
                    App.Language = lang;
                }

            }
        }

        private void ThemeChange(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            string style = (cb.SelectedItem as ComboBoxItem).Tag.ToString();
            // определяем путь к файлу ресурсов
            var uri = new Uri(style + ".xaml", UriKind.Relative);
            // загружаем словарь ресурсов
            ResourceDictionary resourceDict = Application.LoadComponent(uri) as ResourceDictionary;
            // очищаем коллекцию ресурсов приложения
            Application.Current.Resources.Clear();
            // добавляем загруженный словарь ресурсов
            Application.Current.Resources.MergedDictionaries.Add(resourceDict);
        }/**/
    }
}
