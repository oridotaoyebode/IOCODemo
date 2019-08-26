using System.Collections.Generic;
using System.Threading.Tasks;

namespace IOCO.Demo.ViewModels.Base
{
    public abstract class BasePopupViewModelBase: ViewModelBase
    {
        private string _errorMessage;
        public string ErrorMessage
        {
            get => _errorMessage;
            set => this.SetProperty(ref _errorMessage, value);
        }

       
        public override Task InitializeAsync(object navigationData)
        {
            if (navigationData is Dictionary<string, string> messages)
            {
                Title = messages["title"];
                ErrorMessage = messages["errorMessage"];
            }

            return base.InitializeAsync(navigationData);
        }
    }
}
