﻿using System;
using System.Threading.Tasks;
using IOCO.Demo.ViewModels.Base;

namespace IOCO.Demo.Services.Navigation
{
    public interface INavigationService
    {
        Task InitializeAsync();

        Task NavigateToAsync<TViewModel>() where TViewModel : ViewModelBase;


        Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase;



        Task NavigateBackAsync();

     

      
    }
}
