using System;
using System.Globalization;
using Xamarin.Forms;

namespace IOCO.Demo.StateControl
{
    public class StateToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is StateControl.State state && parameter is StateControl.State stateToCompare)
            {
                return state == stateToCompare;
            }

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return StateControl.State.None;
        }
    }
}
