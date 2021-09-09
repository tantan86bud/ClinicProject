using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ClinicProject
{
    public class Patient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Gender { get; set; }
        public DateTime DateBirth { get; set; }
        public string Adress { get; set; }
        public string Telephone { get; set; }
        public List<Visit> Visits { get; set; }

        public Patient()
        {
            DateBirth = DateTime.Now;
        }


    }
}

