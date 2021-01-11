using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace main.Models
{
    class Schedule: INotifyPropertyChanged
    {
        private string _textTime;
        private string _textSubject;
        private string _textCabinet;
        private string _textTeacher;

        public string TextTime
        {
            get { return _textTime; }
            set
            {
                if (_textTime == value)
                    return;
                _textTime = value;
                OnPropertyChange("TextTime");
            }
        }

        public string TextSubject
        {
            get { return _textSubject; }
            set
            {
                if (_textSubject == value)
                    return;
                _textSubject = value;
                OnPropertyChange("TextSubject");
            }
        }

        public string TextCabinet
        {
            get { return _textCabinet; }
            set
            {
                if (_textCabinet == value)
                    return;
                _textCabinet = value;
                OnPropertyChange("TextCabinet");
            }
        }

        public string TextTeacher
        {
            get { return _textTeacher; }
            set
            {
                if (_textTeacher == value)
                    return;
                _textTeacher = value;
                OnPropertyChange("TextTeacher");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChange(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
