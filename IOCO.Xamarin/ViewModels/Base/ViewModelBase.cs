using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using IOCO.Demo.Services.Navigation;
using IOCO.Demo.StateControl;
using Xamarin.Forms;

namespace IOCO.Demo.ViewModels.Base
{
    public abstract class ViewModelBase : MvvmHelpers.BaseViewModel
    {
        private bool _isDeviceConnected;
        private string _pageTitle;
        private string _loadingText = "Please be patient while we get your info...";
        private string _errorText;

        private bool _showNoResults = false;
       // protected readonly IDialogService DialogService;
        protected readonly INavigationService NavigationService;
        protected ViewModelBase()
        {
            //DialogService = Locator.Instance.Resolve<IDialogService>();
            NavigationService = Locator.Instance.Resolve<INavigationService>();
        }

        private State _state;
        public State State
        {
            get => _state;
            set => this.SetProperty(ref _state, value);
        }



        public bool ShowNoResults
        {
            get => _showNoResults;

            set
            {
                _showNoResults = value;
                OnPropertyChanged();
            }
        }


        public string PageTitle
        {
            get => _pageTitle;

            set
            {
                _pageTitle = value;
                ErrorText = "No results found.";
                OnPropertyChanged();
            }
        }

        public string LoadingText
        {
            get => _loadingText;

            set
            {
                _loadingText = value;
                OnPropertyChanged();
            }
        }

        private ICommand _goBackCommand;

        public ICommand GoBackCommand
        {
            get
            {
                return _goBackCommand ?? (_goBackCommand = new Command(async () =>
                {
                    try
                    {
                        await NavigationService.NavigateBackAsync();
                    }
                    catch (System.Exception)
                    {
                        await NavigationService.NavigateBackAsync();
                    }
                }));
            }
        }

       

       

        public bool Answer { get; set; }

        protected async Task OpenGenericPopupModal(Exception exception = null, string error = "")
        {
            string message = string.Empty;
            string title = string.Empty;
#if DEBUG

            message = exception == null ? error : exception.Message;
            title = "Error";


#else
            if (exception?.GetType() == typeof(NotConnectedException))
            {
                message = exception.Message;
            }
            else
            {
                message =
                "We are experiencing issues loading the data you have requested. Please try again or contact support";
            }

            title = "This looks bad :)";
#endif
            var dictionary = new Dictionary<string, string> {{"title", title}, {"errorMessage", message}};
            //await NavigationService.NavigateToPopupAsync<ExtendedNavigationViewModel>(dictionary, true);
        }



        
       
        public string ErrorText
        {
            get => _errorText;

            set
            {
                _errorText = value;
                OnPropertyChanged();
            }
        }

        public virtual Task InitializeAsync(object navigationData) => Task.FromResult(false);
    }
}