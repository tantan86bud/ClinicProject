
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ClinicProject
{
    /// <summary>
    /// Логика взаимодействия для MainClinicWindow.xaml
    /// </summary>
    public partial class MainClinicWindow : Window
    {
        public MainClinicWindow()
        {
           
                        
            InitializeComponent();
        }

       

        private void AddVisit_Click(object sender, RoutedEventArgs e)
        {
            if ((TabItemPatient.DataContext as ViewModelPatient).Patient != null)
            {
                TabControl.SelectedItem = TabItemVisit;
            
                (TabItemVisit.DataContext as ViewModelVisit).Visit = new Visit();

                (TabItemVisit.DataContext as ViewModelVisit).Visit.Patient = (TabItemPatient.DataContext as ViewModelPatient).Patient;
                (TabItemVisit.DataContext as ViewModelVisit).Expand = true;
                (TabItemVisit.DataContext as ViewModelVisit).IsEnabledVisitEdit = true;
            }
          
            
        }

       

        private void SavePatient_Click(object sender, RoutedEventArgs e)
        {
            BindingExpression be;
            foreach (var tb in GridEditPatient.Children)
            {
                if (tb is TextBox)
                {
                    be = (tb as TextBox).GetBindingExpression(TextBox.TextProperty);
                    be.UpdateSource();
                }

            }
            be = rbMen.GetBindingExpression(RadioButton.IsCheckedProperty);
            be.UpdateSource();
            be = dpDate.GetBindingExpression(DatePicker.SelectedDateProperty);
            be.UpdateSource();
        }

        private void SaveVisit_Click(object sender, RoutedEventArgs e)
        {
            BindingExpression be;
            foreach (var tb in GridEditVisit.Children)
            {
                if (tb is TextBox)
                {
                    be = (tb as TextBox).GetBindingExpression(TextBox.TextProperty);
                    be.UpdateSource();
                }

            }
            be = rbTypeVisitFirst.GetBindingExpression(RadioButton.IsCheckedProperty);
            be.UpdateSource();
            be = dpDateVisit.GetBindingExpression(DatePicker.SelectedDateProperty);
            be.UpdateSource();
            VisitsDataGrid.Items.Refresh();
        }
    }
}
