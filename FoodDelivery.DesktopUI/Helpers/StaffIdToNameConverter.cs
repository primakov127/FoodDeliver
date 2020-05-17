using FoodDelivery.DesktopUI.Library.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace FoodDelivery.DesktopUI.Helpers
{
    public class StaffIdToNameConverter : IMultiValueConverter
    {

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[1] != null && values[0] is string)
            {
                var staffs = (values[1] as IEnumerable<StaffModel>);
                return staffs.Where(staff => staff.UserId == ((string)values[0])).FirstOrDefault().Name;
            }

            return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
