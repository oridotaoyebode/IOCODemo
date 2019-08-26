using System.Collections.Generic;
using System.Linq;
using MvvmHelpers;
using Xamarin.Forms;

namespace IOCO.Demo.ViewModels.Validation
{
    public class ValidatableObject<T> : BindableObject, IValidity
    {
        T mainValue;
        string firstError;
        bool isValid;

        public List<IValidationRule<T>> Validations { get; }

        public ObservableRangeCollection<string> Errors { get;   }
        public string FirstError
        {
            get => firstError;

            set
            {
                firstError = value;
                OnPropertyChanged();
            }
        }

        public T Value
        {
            get => mainValue;

            set
            {
                mainValue = value;
                OnPropertyChanged();
                
               
            }
        }

        public bool IsValid
        {
            get => isValid;

            set
            {
                isValid = value;
                Errors.Clear();
                OnPropertyChanged();
            }
        }

        public ValidatableObject()
        {
            isValid = true;
            Errors = new ObservableRangeCollection<string>();
            Validations = new List<IValidationRule<T>>();
        }

        public bool Validate()
        {
            Errors.Clear();
            FirstError = string.Empty;
            var errors = Validations.Where(v => !v.Check(Value))                        
                .Select(v => v.ValidationMessage);

            foreach (var error in errors)
            {
                Errors.Add(error);
                FirstError = error;
            }

            IsValid = !Errors.Any();
            
            return IsValid;
        }
    }
}
