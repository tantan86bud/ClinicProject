using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ClinicProject
{
    public class Patient:INotifyPropertyChanged
    {
        public int Id { get; set; }
        private string _name;
        private int _gender;
        private DateTime _dateBirth;
        private string _adress;
        private string _telephone;
        public DateTime DateBirth {
            get
            {
                return _dateBirth;
            }
            set
            {
                _dateBirth = value;
                NotifyPropertyChanged("DateBirth");
            }
        }
        public string Adress {
            get
            {
                return _adress;
            }
            set
            {
                _adress = value;
                NotifyPropertyChanged("Adress");
            }
        }
        public string Telephone {
            get
            {
                return _telephone;
            }
            set
            {
                _telephone = value;
                NotifyPropertyChanged("Telephone");
            }
        }
        public int Gender
        {

            get
            {
                return _gender;
            }
            set
            {
                _gender = value;
                NotifyPropertyChanged("Gender");
            }
        }
        public string Name
        {

            get
            {
                return _name;
            }
            set
            {
                _name = value;
                NotifyPropertyChanged("Name");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
     
       
        public List<Visit> Visits { get; set; }

        public Patient()
        {
            DateBirth = DateTime.Now;
        }


    }
}

