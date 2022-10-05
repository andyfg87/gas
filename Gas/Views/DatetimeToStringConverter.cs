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

            
            var userSelectedCulture = new CultureInfo("es");
            var datetime = (DateTimeOffset)value;
            var convertDate = datetime.Humanize(datetime, userSelectedCulture);
            //Add my own vocavulary
            /*convertDate = convertDate.Replace("now", "ahora").Replace("days","días").Replace("day","día").Replace("today","hoy").
                Replace("minutes ago","minutos").Replace("an hour ago", "un hora atras").Replace("hours","horas").Replace("hour","hora");*/
            return convertDate;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
