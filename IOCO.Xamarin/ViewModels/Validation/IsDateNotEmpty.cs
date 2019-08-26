using System;
using System.Collections.Generic;
using System.Text;

namespace IOCO.Demo.ViewModels.Validation
{
    public class IsDateNotEmpty<T>: IValidationRule<T>
    {
        public string ValidationMessage { get; set; }
        public bool Check(T value)
        {
            if (value == null)
            {
                return false;
            }

            var date = DateTime.TryParse(value.ToString(), out _);

            return date;
        }
    }
}
