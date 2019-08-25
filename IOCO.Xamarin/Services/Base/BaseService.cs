using IOCO.Xamarin.Services.Http;

namespace IOCO.Xamarin.Services.Base
{
    public abstract class BaseService
    {
        protected BaseService(IHttpService httpService)
        {
            HttpService = httpService;
        }

        protected IHttpService HttpService { get; private set; }
    }
}
