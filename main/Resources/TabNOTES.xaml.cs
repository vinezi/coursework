using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;
using main.Models;
using main.Services;
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
        private FileIOService _fileIOService;
        public TabNOTES()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            _fileIOService = new FileIOService(PATH);
            _notesDataList = _fileIOService.LoadNoteData();
            try
            {
                _notesDataList.ListChanged += _notesDataList_ListChanged;
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
                    _fileIOService.SaveData(sender);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                  
                }
            }
        }
    }
}

