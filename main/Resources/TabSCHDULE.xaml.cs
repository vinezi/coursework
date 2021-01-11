using main.Models;
using main.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для TabSCHDULE.xaml
    /// </summary>
    public partial class TabSCHDULE : UserControl
    {
        private readonly string PATH = $"{Environment.CurrentDirectory}\\testDate";
        private BindingList<Schedule> _todoData;
        private FileIOService _fileIOService;

        public TabSCHDULE()
        {
            InitializeComponent();
            SelectDay.SelectedIndex = 0;
            ListBoxItem FirstItem = new ListBoxItem();
            FirstItem = (ListBoxItem)SelectDay.SelectedItem;
            FirstItem.Focus();
            //test.Items.Refresh();
        }
        private void SelectDay_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //test.Items.Refresh();
            string SelectedDay = ((ListBoxItem)SelectDay.SelectedItem).Tag.ToString();
            _fileIOService = new FileIOService(PATH + SelectedDay + ".json");
            _todoData = _fileIOService.LoadData();
            try
            {
                _todoData.ListChanged += _todoData_ListChanged;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            test.ItemsSource = _todoData;
            _todoData.ListChanged += _todoData_ListChanged;
            
        }

        private void _todoData_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.ItemAdded || e.ListChangedType == ListChangedType.ItemDeleted || e.ListChangedType == ListChangedType.ItemChanged)
            {
                try
                {
                    _fileIOService.SaveData(sender);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void TextTime_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox TextTime = sender as TextBox;
            if (Regex.IsMatch(TextTime.Text, "[^0-9:]"))
            {
                TextTime.Text = TextTime.Text.Remove(TextTime.Text.Length - 1);
                TextTime.SelectionStart = TextTime.Text.Length;
            }
        }
        private void TextCabinet_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox TextCabinet = sender as TextBox;
            if (Regex.IsMatch(TextCabinet.Text, "[^0-9/]"))
            {
                TextCabinet.Text = TextCabinet.Text.Remove(TextCabinet.Text.Length - 1);
                TextCabinet.SelectionStart = TextCabinet.Text.Length;
            }
        }

        private void TextBox_Loaded(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            textBox.Focus();
            if (textBox.Text != null) textBox.SelectAll();
        }
       
    }
}
