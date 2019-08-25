using IOCO.Xamarin.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IOCO.Xamarin.Services.Http
{
    public interface IHttpService
    {
        Task<HttpResponse> GetAsync(string url, Dictionary<string, string> parameters = null,
            int timeout = 60,
            Dictionary<string, string> customHeaders = null);

        Task<HttpResponse> PostAsync<T>(string url, T content = default,
            Dictionary<string, string> formUrlEncoded = null, Dictionary<string, string> parameters = null,
            int timeout = 60, 
            Dictionary<string, string> customHeaders = null);

        Task<HttpResponse> UpdateAsync<T>(string url, T content = default,
            Dictionary<string, string> parameters = null, 
            int timeout = 60,
            Dictionary<string, string> customHeaders = null);
    }
}