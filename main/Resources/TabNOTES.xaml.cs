using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;
using main.Models;
using main.rzmIO;
using System;

namespace main.Resources
{
    /// <summary>
    /// Логика взаимодействия для TabNOTES.xaml
    /// </summary>
    public partial class TabNOTES : UserControl
    {

        private readonly string PATH = $"{Environment.CurrentDirectory }\\notesDataList.json";
        private BindingList<NotesModel> _notesDataList;
        private RzmIO _rzmIO;
        public TabNOTES()
        {
            InitializeComponent();
        }

        private void dgNote_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            _rzmIO = new RzmIO(PATH);

            try
            {
                _notesDataList = _rzmIO.LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                
            }

            

            dgNote.ItemsSource = _notesDataList;
            _notesDataList.ListChanged += _notesDataList_ListChanged;
        }


        private void _notesDataList_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.ItemAdded || e.ListChangedType == ListChangedType.ItemDeleted || e.ListChangedType == ListChangedType.ItemChanged)
            {
                try
                {
                    _rzmIO.SaveData(sender);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                  
                }
            }
        }

        

    }
}

