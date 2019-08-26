using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IOCO.Demo.ViewModels;
using IOCO.Demo.ViewModels.Base;
using IOCO.Demo.Views;
using Xamarin.Forms;

namespace IOCO.Demo.Services.Navigation
{
    public partial class NavigationService : INavigationService
    {
        protected readonly Dictionary<Type, Type> Mappings;

        protected Application CurrentApplication => Application.Current;

        public NavigationService()
        {
            Mappings = new Dictionary<Type, Type>();

            CreatePageViewModelMappings();
        }

        public async Task InitializeAsync()
        {
            //if (string.IsNullOrEmpty(AppSettings.PartyId))
            //{
            //    await NavigateToAsync<LoginViewModel>();
            //}
            //else
            {
                await NavigateToAsync<MainViewModel>();
            }
            //await NavigateToAsync<MainViewModel>();

        }

        public Task NavigateToAsync<TViewModel>() where TViewModel : ViewModelBase => InternalNavigateToAsync(typeof(TViewModel), null);
        public Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase => InternalNavigateToAsync(typeof(TViewModel), parameter);

        public Task NavigateToAsync(Type viewModelType) => InternalNavigateToAsync(viewModelType, null);

        public Task NavigateToAsync(Type viewModelType, object parameter) => InternalNavigateToAsync(viewModelType, parameter);

        public async Task NavigateBackAsync()
        {
            await CurrentApplication.MainPage.Navigation.PopAsync();
        }






        protected virtual async Task InternalNavigateToAsync(Type viewModelType, object parameter)
        {
            var page = CreateAndBindPage(viewModelType, parameter);

            if (page is MainPage)
            {
                CurrentApplication.MainPage = new CustomNavigationPage(page);
            }
            else
            {
                if (CurrentApplication.MainPage is CustomNavigationPage navigationPage)
                {
                    await navigationPage.PushAsync(page);
                }
                else
                {
                    CurrentApplication.MainPage = new CustomNavigationPage(page);
                }
            }

            await (page.BindingContext as ViewModelBase).InitializeAsync(parameter);
        }

        protected Type GetPageTypeForViewModel(Type viewModelType)
        {
            if (!Mappings.ContainsKey(viewModelType))
            {
                throw new KeyNotFoundException($"No map for ${viewModelType} was found on navigation mappings");
            }

            return Mappings[viewModelType];
        }

        protected Page CreateAndBindPage(Type viewModelType, object parameter)
        {
            try
            {
                var pageType = GetPageTypeForViewModel(viewModelType);

                if (pageType == null)
                {
                    throw new Exception($"Mapping type for {viewModelType} is not a page");
                }

                var page = Activator.CreateInstance(pageType) as Page;
                var viewModel = Locator.Instance.Resolve(viewModelType) as ViewModelBase;
                page.BindingContext = viewModel;

                return page;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
           
        }


        void CreatePageViewModelMappings()
        {
            Mappings.Add(typeof(MainViewModel), typeof(MainPage));
            



        }

        
    }
}