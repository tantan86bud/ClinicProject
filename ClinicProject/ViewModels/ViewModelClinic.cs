using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicProject
{
    
   public  class ViewModelClinic
    {
        public ViewModelPatient viewModelPatient { get; set; }
        public ViewModelVisit viewModelVisit { get; set; }
        public ViewModelClinic()
        {
            viewModelPatient = new ViewModelPatient();
            viewModelVisit = new ViewModelVisit();
            viewModelPatient.viewModelVisit = viewModelVisit;


        }
    }
}
