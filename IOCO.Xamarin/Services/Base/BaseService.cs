using System.Collections.Generic;
using System.Threading.Tasks;
using IOCO.Demo.Services.Http;
using IOCO.Demo.Services.Json;
using IOCO.Models;

namespace IOCO.Demo.Services.Base
{
    public abstract class BaseService<T> : IBaseService<T> where T : class
    {
        private readonly IJsonService _jsonService;

        protected BaseService(IHttpService httpService, IJsonService jsonService)
        {
            _jsonService = jsonService;
            HttpService = httpService;
        }

        private IHttpService HttpService { get; set; }

        public async Task<List<T>> Get(string url)
        {
            var apiResponse = await HttpService.GetAsync(url);
            if (apiResponse.Success)
            {
                return _jsonService.Deserialize<List<T>>(apiResponse.Response, Converter.Settings);
            }

            return null;
        }

        public async Task<T> Get(string url, int id)
        {
            var apiResponse = await HttpService.GetAsync($"{url}/{id}");
            if (apiResponse.Success)
            {
                return _jsonService.Deserialize<T>(apiResponse.Response, Converter.Settings);
            }

            return null;
        }

        public async Task<T> Create(string url, T data)
        {
            var apiResponse = await HttpService.PostAsync(url, data);
            if (apiResponse.Success)
            {
                return _jsonService.Deserialize<T>(apiResponse.Response, Converter.Settings);
            }

            return null;
        }

        public async Task<bool> Delete(string url, int id)
        {
            var apiResponse = await HttpService.DeleteAsync($"{url}/{id}");
            if (apiResponse.Success)
            {
                return true;
            }
            return false;
        }

        public async Task<T> Update(string url, int id, T data)
        {
            var apiResponse = await HttpService.UpdateAsync($"{url}/{id}", data);
            if (apiResponse.Success)
            {
                return _jsonService.Deserialize<T>(apiResponse.Response, Converter.Settings);
            }
            return null;
        }
    }
}
