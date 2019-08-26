using IOCO.Demo.Services.Http;

namespace IOCO.Demo.Services.Base
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
