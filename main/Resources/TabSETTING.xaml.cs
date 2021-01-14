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

namespace main.Resources
{
    /// <summary>
    /// Логика взаимодействия для TabSETTING.xaml
    /// </summary>
    public partial class TabSETTING : UserControl
    {
        public TabSETTING()
        {
            InitializeComponent();
            styleBox.SelectedIndex = Properties.Settings.Default.theame; //сохр them
            langBox.SelectedIndex = Properties.Settings.Default.language; //сохр lang
            apiBox.SelectedIndex = Properties.Settings.Default.apiKey; //сохр api
            tbYourKey.Text = Properties.Settings.Default.yourKey;
        }

        private void LangSelector(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            string lang = (cb.SelectedItem as ComboBoxItem).Tag.ToString();
            var uri = new Uri("Resources/Lang" + lang + ".xaml", UriKind.Relative);
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
            var uri = new Uri("Resources/Theme" + style + ".xaml", UriKind.Relative);
            // загружаем словарь ресурсов
            ResourceDictionary resourceDict = Application.LoadComponent(uri) as ResourceDictionary;
            // очищаем коллекцию ресурсов приложения
            Application.Current.Resources.Clear();
            // добавляем загруженный словарь ресурсов
            Application.Current.Resources.MergedDictionaries.Add(resourceDict);
            Properties.Settings.Default.theame = cb.SelectedIndex;
            Properties.Settings.Default.Save();
        }
        private void ApiKeyChange(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            switch (cb.SelectedIndex)
            {
                case 0:
                    Properties.Settings.Default.currentApiKey = Properties.Settings.Default.basicKey;
                    break;
                case 1:
                    Properties.Settings.Default.currentApiKey = Properties.Settings.Default.backupKey;
                    break;
                case 2:
                    Properties.Settings.Default.currentApiKey = Properties.Settings.Default.yourKey;
                    break;
                default:
                    break;
            }
            Properties.Settings.Default.apiKey = cb.SelectedIndex;
            Properties.Settings.Default.Save();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (tbYourKey.Text.Length >0 && tbYourKey.Text.Length <=45)
            {
                Properties.Settings.Default.yourKey = tbYourKey.Text;
                Properties.Settings.Default.currentApiKey = Properties.Settings.Default.yourKey;
                Properties.Settings.Default.Save();
            }
        }
    }
}
