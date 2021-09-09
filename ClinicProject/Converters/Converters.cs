using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace ClinicProject
{

    public class RadioBoolToIntConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int integer = (int)value;
            if (integer == int.Parse(parameter.ToString()))
                return true;
            else
                return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value == false && int.Parse(parameter.ToString())== 1)
            {

                return 0;

            }
            else
            { return 1; }
        }
    }
    public class GenderConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int integer = (int)value;
            if (integer == 1)
                return "Мужской";
            else
                return "Женский";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string str = (string)value;
            if (str == "Мужской")
                return 1;
            else
                return 0;
        }
    }

    public class RadioBoolToIntConverterTypeVisit : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        { 
            
            if (value != null && value.ToString() ==parameter.ToString())
                return true;
            else
                return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value == false && parameter.ToString() == "1")// int.Parse(parameter.ToString()) == 1)
            {

                return "2";

            }
            else
            { return "1"; }
        }
    }
    public class TypeVisitConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            
            if (value!=null && (string)value == "1")
                return "Первичный";
            else
                return "Вторичный";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string str = (string)value;
            if (str == "Первичный")
                return "1";
            else
                return "2";
        }
    }



}
