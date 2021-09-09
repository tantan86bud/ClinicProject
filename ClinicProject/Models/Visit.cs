using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ClinicProject
{
    public class Visit : INotifyPropertyChanged
    {
        private Patient _patient;
        public int Id { get; set; }
        public int PatientId { get; set; }
        public Patient Patient
        {
            get
            {
                return _patient;
            }
            set
            {
                _patient = value;
                NotifyPropertyChanged("Patient");
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
        public DateTime DateVisit { get; set; }
        public string TypeVisit { get; set;  }
        public string Diagnosis { get; set;  }
        public Visit() {
            DateVisit = DateTime.Now;
            TypeVisit = "1";

        }
       


    }
}
