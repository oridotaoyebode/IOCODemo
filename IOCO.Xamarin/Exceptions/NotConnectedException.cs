using System;

namespace IOCO.Demo.Exceptions
{
    public class NotConnectedException: Exception
    {
        public NotConnectedException():base("No internet connection. Please try again")
        {
            
        }
    }
}
