using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace ClinicProject
{
    public class ViewModelPatient : ViewModelBase
    {
        private  Patient _patient;
        private ObservableCollection<Patient> _patients;
        public HttpClient client;

        public string RestFulServicePath;
        private ICommand _AddCommand;
        private ICommand _EditCommand;
        private ICommand _SaveCommand;
        private ICommand _DeleteCommand;
        

        
        private bool _expand;
        private bool _IsEnabledPatientEdit;
        private bool _AddVisitButton;
        public bool Expand
        {
            get
            {
                return _expand;
            }
            set
            {
                _expand = value;
                NotifyPropertyChanged("Expand");
}
 }
        public bool IsEnabledPatientEdit
        {
            get
            {
                return _IsEnabledPatientEdit;
            }
            set
            {
                _IsEnabledPatientEdit = value;
                NotifyPropertyChanged("IsEnabledPatientEdit");
            }
        }
        public bool AddVisitButton
        {
            get
            {
                return _AddVisitButton;
            }
            set
            {
                _AddVisitButton = value;
                NotifyPropertyChanged("AddVisitButton");
            }
        }
        
        public   Patient Patient
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
        public ObservableCollection<Patient> Patients
        {
            get
            {
                return _patients;
            }
            set
            {
                _patients = value;
                NotifyPropertyChanged("Patients");
            }
        }
       
        public ICommand AddCommand
        {
            get
            {
                if (_AddCommand == null)
                {
                    _AddCommand = new RelayCommand(param => this.AddPatient(), null);
                }
                return _AddCommand;
            }
        }
        public ICommand EditCommand
        {
            get
            {
                if (_EditCommand == null)
                {
                    _EditCommand = new RelayCommand(param => this.EditPatient(), null);
                }
                return _EditCommand;
            }
        }
        public ICommand SaveCommand
        {
            get
            {
                if (_SaveCommand == null)
                {
                    _SaveCommand = new RelayCommand(param => this.SavePatient(), null);
                }
                return _SaveCommand;
            }
        }
        public ICommand DeleteCommand
        {
            get
            {
                if (_DeleteCommand == null)
                {
                    _DeleteCommand = new RelayCommand(param => this.DeletePatient(), null);
                }
                return _DeleteCommand;
            }
        }
     


        public ViewModelPatient()
        {
            
            RestFulServicePath = ConfigurationManager.AppSettings.Get("RESTFulServicePatient");
            Patients = new ObservableCollection<Patient>();
            client = new HttpClient();
            GetApiAsync(RestFulServicePath, client, Patients);
            Patients.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(Patients_CollectionChanged);
            AddVisitButton = true;
        }

        void Patients_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            IsEnabledPatientEdit = false;
            NotifyPropertyChanged("Patients");
        }
        
        private void AddPatient()
        {
            Patient = new Patient();
                      
            Expand = true;
            IsEnabledPatientEdit = true;
            AddVisitButton = false;

        }
        static async void GetApiAsync(string path, HttpClient client, ObservableCollection<Patient> Patients)

        {
            HttpResponseMessage mes=null; 
            try
                {
                    mes= await client.GetAsync(path);
                
                 }
            catch (System.Net.Http.HttpRequestException e)
                 {
                MessageBox.Show(e.Message, "Внимание");
                 }


            if (mes != null)
            {
                var content = mes.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                var model = JsonConvert.DeserializeObject<List<Patient>>(content);
                foreach (Patient pt in model)
                {
                    Patients.Add(pt);

                }
            }
        }
        private void EditPatient()
        {
            if (Patient != null)
            {
                Expand = true;
                IsEnabledPatientEdit = true;
                AddVisitButton = false;
            }
            

        }
        private void SavePatient()
        {
            
                if (Patient != null && IsEnabledPatientEdit == true)
                {
                    if (Patient.Id == 0)
                    {
                        PostApiAsync(RestFulServicePath, client, Patient);
                    }
                    else
                    {
                        PutApiAsync(RestFulServicePath, client, Patient);
                    }
                }
            AddVisitButton = true;
            IsEnabledPatientEdit = false;
        }
        public async void PostApiAsync(string path, HttpClient client, Patient patient)

        {
            string myContent = JsonConvert.SerializeObject(patient);
            var httpContent = new StringContent(myContent, Encoding.UTF8, "application/json");
          
            var mes = await client.PostAsync(path, httpContent) ;
            
            var content = mes.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            try
            {
                Patient pt = JsonConvert.DeserializeObject<Patient>(content);
                Patient.Id = pt.Id;
                Patients.Add(pt);
            }
            catch 
            {
                MessageBox.Show("Не удалось сохранить объект в базу данных", "Ошибка", MessageBoxButton.OK);
            }
        }
        public async void PutApiAsync(string path, HttpClient client, Patient patient)

        {



            string myContent = JsonConvert.SerializeObject(patient);
            var httpContent = new StringContent(myContent, Encoding.UTF8, "application/json");

            var mes = await client.PutAsync(path, httpContent);
            var content = mes.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            try
            {
                Patient pt = JsonConvert.DeserializeObject<Patient>(content);
                Patient.Id = pt.Id;
            }
            catch
            {
                MessageBox.Show("Не удалось обновить объект в базе данных", "Ошибка", MessageBoxButton.OK);
            }


        }
        private void DeletePatient()
        {
            if (Patient!=null && Patient.Id !=0)
            {
                DeleteApiAsync(RestFulServicePath + "/" + Patient.Id, client, Patient);
            }
            AddVisitButton = true;
            IsEnabledPatientEdit = false;

        }
        public async void DeleteApiAsync(string path, HttpClient client, Patient patient)

        {
           
            var mes = await client.DeleteAsync(path);
            Patients.Remove(patient);
        }
       
    }
}
