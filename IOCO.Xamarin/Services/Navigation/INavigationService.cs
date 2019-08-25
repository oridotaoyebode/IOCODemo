using System;
using System.Threading.Tasks;
using IOCO.Xamarin.ViewModels.Base;

namespace IOCO.Xamarin.Services.Navigation
{
    public interface INavigationService
    {
        Task InitializeAsync();

        Task NavigateToAsync<TViewModel>() where TViewModel : ViewModelBase;


        Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase;



        Task NavigateBackAsync();

     

      
    }
}
