using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace main.Models
{
    class NotesModel: INotifyPropertyChanged
    {
        public DateTime Date { get; set; } = DateTime.Now;

        private string _notes;

        private string _tb;

        public string Notes
        {
            get { return _notes; }
            set 
            {
                if (_notes == value)
                    return;
                _notes = value;
                OnPropertyChanged("Notes");
            }
        }
       /* public string Text
        {
            get { return _text; }
            set 
            {
                if (_text == value)
                    return;
                _text = value;
                OnPropertyChanged("Text");
            }
        }*/
        public string Tb
        {
            get { return _tb; }
            set
            {
                if (_tb == value)
                    return;
                _tb = value;
                OnPropertyChanged("Tb");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = "")
        {
            
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); //проверка на null
            
        }
    }
}
