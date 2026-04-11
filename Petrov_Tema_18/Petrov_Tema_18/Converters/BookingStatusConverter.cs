using System;
using System.Globalization;
using System.Windows.Data;

namespace Petrov_Tema_18.Converters
{
    public class BookingStatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
            (bool)value ? "Занят" : "Свободен";

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            throw new NotImplementedException();
    }
}