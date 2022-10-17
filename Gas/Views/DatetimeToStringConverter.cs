using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;
using Humanizer;
using Humanizer.Inflections;

namespace Gas.Views
{
    public class DatetimeToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)            
                return string.Empty;

            
            var userSelectedCulture = new CultureInfo("es-ES");
            var datetime = (DateTimeOffset)value;
            var dateToday = DateTimeOffset.UtcNow;
            var convertDate = datetime.Humanize(culture: userSelectedCulture);           
            
            return convertDate;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
