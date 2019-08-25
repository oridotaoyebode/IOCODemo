using System;

namespace IOCO.Xamarin.Exceptions
{
    public class NotConnectedException: Exception
    {
        public NotConnectedException():base("No internet connection. Please try again")
        {
            
        }
    }
}
