using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace ClinicProject
{
   
    public  class ViewModelVisit : ViewModelBase
    {
        private Visit _visit;
        private ObservableCollection<Visit> _visits;
        
        public HttpClient client;

        public string RestFulServicePath;
        private ICommand _AddCommand;
        private ICommand _EditCommand;
        private ICommand _SaveCommand;
        private ICommand _DeleteCommand;
      
        //ApplicationContext db;
        private bool _expand;
        private bool _IsEnabledVisitEdit;
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
        public bool IsEnabledVisitEdit
        {
            get
            {
                return _IsEnabledVisitEdit;
            }
            set
            {
                _IsEnabledVisitEdit = value;
                NotifyPropertyChanged("IsEnabledVisitEdit");
            }
        }

        public Visit Visit
        {
            get
            {
                return _visit;
            }
            set
            {
                _visit = value;
                NotifyPropertyChanged("Visit");
            }
        }
        public ObservableCollection<Visit> Visits
        {
            get
            {
                return _visits;
            }
            set
            {
                _visits = value;
                NotifyPropertyChanged("Visits");
            }
        }

        public ICommand AddCommand
        {
            get
            {
                if (_AddCommand == null)
                {
                    _AddCommand = new RelayCommand(param => this.AddVisit(), null);
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
                    _EditCommand = new RelayCommand(param => this.EditVisit(), null);
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
                    _SaveCommand = new RelayCommand(param => this.SaveVisit(), null);
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
                    _DeleteCommand = new RelayCommand(param => this.DeleteVisit(), null);
                }
                return _DeleteCommand;
            }
        }


        public ViewModelVisit()
        {

            RestFulServicePath = ConfigurationManager.AppSettings.Get("RESTFulServiceVisit");
            Visits = new ObservableCollection<Visit>();
            client = new HttpClient();
            GetApiAsync(RestFulServicePath, client, Visits);
            Visits.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(Visits_CollectionChanged);

        }

        void Visits_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            NotifyPropertyChanged("Visits");
        }

        private void AddVisit()
        {
            Visit = new Visit();
            Visit.DateVisit= DateTime.Now;
            NotifyPropertyChanged("Visit");
            Expand = true;
            IsEnabledVisitEdit = true;




        }
        static async void GetApiAsync(string path, HttpClient client, ObservableCollection<Visit> Visits)

        {



            HttpResponseMessage mes = null;
            try
            {
                mes = await client.GetAsync(path);

            }
            catch (System.Net.Http.HttpRequestException e)
            {
                MessageBox.Show(e.Message, "Внимание"); 
            }


            if (mes != null)
            {

                var content = mes.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                try
                {
                    var model = JsonConvert.DeserializeObject<List<VisitDTO>>(content);

                    foreach (VisitDTO visit in model)
                    {
                        Visit vst = new Visit();
                        vst.Id = visit.Id;
                        vst.DateVisit = visit.DateVisit;
                        vst.TypeVisit = visit.TypeVisit;
                        vst.Diagnosis = visit.Diagnosis;
                        vst.Patient = new Patient();
                        vst.Patient.Name = visit.NamePatient;
                        vst.Patient.Id = visit.IdPatient;



                        Visits.Add(vst);

                    }

                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Внимание");
                }
            }
        }
        private void EditVisit()
        {
            Expand = true;
            IsEnabledVisitEdit = true;

        }
        private void SaveVisit()
        {if (Visit != null && IsEnabledVisitEdit ==true)
            {

                if (Visit.Id == 0)
                {
                    PostApiAsync(RestFulServicePath, client, Visit);
                }
                else
                {
                    PutApiAsync(RestFulServicePath, client, Visit);
                }
            }
            IsEnabledVisitEdit = false;
        }
        public async void PostApiAsync(string path, HttpClient client, Visit visit)

        {
            string myContent = JsonConvert.SerializeObject(visit);
            var httpContent = new StringContent(myContent, Encoding.UTF8, "application/json");

            var mes = await client.PostAsync(path, httpContent);
           
            try
            {
                var content = mes.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            
            visit.Id = JsonConvert.DeserializeObject<int>(content);
            Visits.Add(visit);
            }
            catch
            {
                MessageBox.Show("Не удалось сохранить объект в базу данных", "Ошибка", MessageBoxButton.OK);
            }
        }
        public async void PutApiAsync(string path, HttpClient client, Visit visit)

        {



            string myContent = JsonConvert.SerializeObject(visit);
            var httpContent = new StringContent(myContent, Encoding.UTF8, "application/json");

            var mes = await client.PutAsync(path, httpContent);

            var content = mes.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            try
            {
                int visId = JsonConvert.DeserializeObject<int>(content);
                Visit.Id = visId;
                //NotifyPropertyChanged("Visit");
            }
            catch
            {
                MessageBox.Show("Не удалось обновить объект в базе данных", "Ошибка", MessageBoxButton.OK);
            }

        }
        private void DeleteVisit()
        {
            
            if (Visit!=null && Visit.Id != 0)
            {
                DeleteApiAsync(RestFulServicePath + "/" + Visit.Id, client, Visit);
            }
            IsEnabledVisitEdit = false;

        }
        public async void DeleteApiAsync(string path, HttpClient client, Visit visit)

        {

            var mes = await client.DeleteAsync(path);
            Visits.Remove(visit);
        }

    }
}
