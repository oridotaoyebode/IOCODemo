using System;
using Autofac;
using IOCO.Xamarin.Services.Http;
using IOCO.Xamarin.Services.Json;
using IOCO.Xamarin.Services.Navigation;
using NavigationService = IOCO.Xamarin.Services.Navigation.NavigationService;

namespace IOCO.Xamarin.ViewModels.Base
{
    public class Locator
    {
        IContainer container;
        readonly ContainerBuilder containerBuilder;
        private static readonly Lazy<Locator> _appContainer = new Lazy<Locator>(() => new Locator());

        public static Locator Instance => _appContainer.Value;

        public Locator()
        {
            containerBuilder = new ContainerBuilder();

            //containerBuilder.RegisterType<DialogService>().As<IDialogService>();
            containerBuilder.RegisterType<NavigationService>().As<INavigationService>();
            containerBuilder.RegisterType<NavigationService>().As<INavigationService>();
            containerBuilder.RegisterType<HttpService>().As<IHttpService>();
            containerBuilder.RegisterType<JsonService>().As<IJsonService>();
            

        }

        public T Resolve<T>() => container.Resolve<T>();

        public object Resolve(Type type) => container.Resolve(type);

        public void Register<TInterface, TImplementation>() where TImplementation : TInterface => containerBuilder.RegisterType<TImplementation>().As<TInterface>();

        public void Register<T>() where T : class => containerBuilder.RegisterType<T>();

        public void Build() => container = containerBuilder.Build();
    }
}