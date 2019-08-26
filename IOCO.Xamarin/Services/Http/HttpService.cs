using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using IOCO.Demo.Exceptions;
using IOCO.Demo.Services.Json;
using IOCO.Models;
using Newtonsoft.Json;
using Xamarin.Essentials;

namespace IOCO.Demo.Services.Http
{
    public class HttpService: IHttpService
    {
        private readonly IJsonService _jsonService;
        private readonly HttpClient _httpclient;
        public HttpService(IJsonService jsonService)
        {
            _jsonService = jsonService;
            _httpclient = new HttpClient
            {
                BaseAddress = new Uri("https://techtestapi.azurewebsites.net/api/")

            };

        }

        public async Task<HttpResponse> GetAsync(string url, Dictionary<string, string> parameters = null, int timeout = 60, Dictionary<string, string> customHeaders = null)
        {

            
            try
            {
                if (Connectivity.NetworkAccess != NetworkAccess.Internet)
                {
                    throw new NotConnectedException();
                    
                }

                _httpclient.Timeout = TimeSpan.FromSeconds(timeout);
                _httpclient.DefaultRequestHeaders.Clear();

                if (customHeaders != null && customHeaders.Any())
                {
                    foreach (var customHeader in customHeaders)
                    {
                        _httpclient.DefaultRequestHeaders.Add(customHeader.Key, customHeader.Value);
                    }
                }
               
                _httpclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var parameter = string.Empty;
                    if (parameters != null)
                    {
                        parameter = parameters.Aggregate(parameter, (current, keyvalue) => current + $"{keyvalue.Key}={keyvalue.Value}&");
                    }
                    var uriLink = !string.IsNullOrEmpty(parameter) ? $"{url}?{parameter}" : url;
                    if (uriLink.EndsWith("&"))
                    {
                        uriLink = uriLink.Substring(0, uriLink.Length - 1);
                    }
                    var s = await _httpclient.GetAsync(uriLink).ConfigureAwait(false);
                    if (s.IsSuccessStatusCode)
                    {
                        var response = await s.Content.ReadAsStringAsync();
                        return new HttpResponse() { ErrorMessage = string.Empty, Response = response, Success = true };
                    }

                    return new HttpResponse()
                    {
                        ErrorMessage = await s.Content.ReadAsStringAsync().ConfigureAwait(false),
                        Response = string.Empty,
                        Success = false
                    };
                
            }
            catch (TaskCanceledException exception)
            {
                Debug.WriteLine("**** TASK CANCELLED EXCEPTION." + exception);

                if (!exception.CancellationToken.IsCancellationRequested)
                {
                    throw new TimeoutException(
                        "The connection timed out; please check your internet connection and try again.", exception);
                }

                throw;
            }
            catch (JsonException exception)
            {
                Debug.WriteLine("**** JSON EXCEPTION." + exception);
                throw;
            }
            catch (Exception exception)
            {
                Debug.WriteLine("**** GENERAL EXCEPTION." + exception);
                throw;
            }
        }

        public async Task<HttpResponse> PostAsync<T>(string url, T content = default, Dictionary<string, string> formUrlEncoded = null, Dictionary<string, string> parameters = null,
            int timeout = 60, Dictionary<string, string> customHeaders = null)
        {
            try
            {
                if (Connectivity.NetworkAccess != NetworkAccess.Internet)
                {
                    throw new NotConnectedException();

                }

                _httpclient.Timeout = TimeSpan.FromSeconds(timeout);
                _httpclient.DefaultRequestHeaders.Clear();
                    if (customHeaders != null && customHeaders.Any())
                    {
                        foreach (var customHeader in customHeaders)
                        {
                        _httpclient.DefaultRequestHeaders.Add(customHeader.Key, customHeader.Value);
                        }

                       
                    }
               
                var parameter = string.Empty;
                    var formencoded = string.Empty;
                    var serialized = string.Empty;
                    if (parameters != null)
                    {
                        parameter = parameters.Aggregate(parameter, (current, keyvalue) => current + $"{keyvalue.Key}={keyvalue.Value}&");
                    }
                    if (formUrlEncoded != null)
                    {
                        formencoded = formUrlEncoded.Aggregate(formencoded, (current, keyvalue) => current + $"{keyvalue.Key}={keyvalue.Value}&");
                        serialized = formencoded;
                    }

                    var uriLink = !string.IsNullOrEmpty(parameter) ? $"{url}?{parameter}" : url;
                    if (uriLink.EndsWith("&"))
                    {
                        uriLink = uriLink.Substring(0, uriLink.Length - 1);
                    }
                    if (content != null && content as string != string.Empty)
                    {
                        serialized = _jsonService.Serialize(content, Converter.Settings);//
                    }

                    if (serialized.EndsWith("&"))
                    {
                        serialized = serialized.Substring(0, serialized.Length - 1);
                    }
                    HttpContent httpContent = new StringContent(serialized, Encoding.UTF8, string.IsNullOrEmpty(formencoded) ? "application/json" : "application/x-www-form-urlencoded");
                    _httpclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(string.IsNullOrEmpty(formencoded) ? "application/json" : "application/x-www-form-urlencoded"));
                //httpContent.Headers.ContentType = new MediaTypeHeaderValue(string.IsNullOrEmpty(formencoded) ? "application/json" : "application/x-www-form-urlencoded");
                var s = await _httpclient.PostAsync(uriLink.Replace(',', '.'), httpContent).ConfigureAwait(false);
                    if (s.IsSuccessStatusCode)
                    {
                        var response = await s.Content.ReadAsStringAsync();
                        return new HttpResponse() { ErrorMessage = string.Empty, Response = response, Success = true };
                    }
                    return new HttpResponse()
                    {
                        ErrorMessage = await s.Content.ReadAsStringAsync(),
                        Response = string.Empty,
                        Success = false
                    };
                
            }
            catch (TaskCanceledException exception)
            {
                Debug.WriteLine("**** TASK CANCELLED EXCEPTION." + exception);

                if (!exception.CancellationToken.IsCancellationRequested)
                {
                    throw new TimeoutException(
                        "The connection timed out; please check your internet connection and try again.", exception);
                }

                throw;
            }
            catch (JsonException exception)
            {
                Debug.WriteLine("**** JSON EXCEPTION." + exception);
                throw;
            }
            catch (Exception exception)
            {
                Debug.WriteLine("**** GENERAL EXCEPTION." + exception);
                throw;
            }
        }

        public async Task<HttpResponse> UpdateAsync<T>(string url, T content = default, Dictionary<string, string> parameters = null,
            int timeout = 60, Dictionary<string, string> customHeaders = null)
        {
            try
            {

                if (Connectivity.NetworkAccess != NetworkAccess.Internet)
                {
                    throw new NotConnectedException();

                }


                _httpclient.Timeout = TimeSpan.FromSeconds(timeout);
                   
                    if (customHeaders != null && customHeaders.Any())
                    {
                        foreach (var customHeader in customHeaders)
                        {
                        _httpclient.DefaultRequestHeaders.Add(customHeader.Key, customHeader.Value);
                        }

                       
                    }

                _httpclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var parameter = string.Empty;
                    var formencoded = string.Empty;
                    var serialized = string.Empty;
                    if (parameters != null)
                    {
                        parameter = parameters.Aggregate(parameter,
                            (current, keyvalue) => current + $"{keyvalue.Key.ToLower()}={keyvalue.Value.ToLower()}&");
                    }

                    var uriLink = !string.IsNullOrEmpty(parameter) ? $"{url}?{parameter}" : url;

                    if (uriLink.EndsWith("&"))
                    {
                        uriLink = uriLink.Substring(0, uriLink.Length - 1);
                    }

                    if (content != null && content as string != string.Empty)
                    {
                        serialized = _jsonService.Serialize(content, Converter.Settings);
                    }



                    HttpContent httpContent = new StringContent(serialized, Encoding.UTF8,
                        string.IsNullOrEmpty(formencoded) ? "application/json" : "application/x-www-form-urlencoded");
                    httpContent.Headers.ContentType = new MediaTypeHeaderValue(string.IsNullOrEmpty(formencoded)
                        ? "application/json"
                        : "application/x-www-form-urlencoded");
                    //HttpRequestMessage request = new HttpRequestMessage
                    //{
                    //    Method = new HttpMethod("PATCH"),
                    //    RequestUri = new Uri(uriLink.Replace(',', '.')),
                    //    Content = httpContent,
                    //};

                    var s = await _httpclient.PutAsync(uriLink.Replace(',', '.'), httpContent)
                        .ConfigureAwait(false);
                    if (s.IsSuccessStatusCode)
                    {
                        var response = await s.Content.ReadAsStringAsync();
                        return new HttpResponse() {ErrorMessage = string.Empty, Response = response, Success = true};
                    }

                    return new HttpResponse()
                    {
                        ErrorMessage = await s.Content.ReadAsStringAsync(),
                        Response = string.Empty,
                        Success = false
                    };
                
            }
            catch (TaskCanceledException exception)
            {
                Debug.WriteLine("**** TASK CANCELLED EXCEPTION." + exception);

                if (!exception.CancellationToken.IsCancellationRequested)
                {
                    throw new TimeoutException(
                        "The connection timed out; please check your internet connection and try again.", exception);
                }

                throw;
            }
            catch (JsonException exception)
            {
                Debug.WriteLine("**** JSON EXCEPTION." + exception);
                throw;
            }
            catch (Exception exception)
            {
                Debug.WriteLine("**** GENERAL EXCEPTION." + exception);
                throw;
            }
        }

        public async Task<HttpResponse> DeleteAsync(string url, Dictionary<string, string> parameters = null, int timeout = 60,
            Dictionary<string, string> customHeaders = null)
        {
            try
            {

                if (Connectivity.NetworkAccess != NetworkAccess.Internet)
                {
                    throw new NotConnectedException();

                }


                _httpclient.Timeout = TimeSpan.FromSeconds(timeout);

                if (customHeaders != null && customHeaders.Any())
                {
                    foreach (var customHeader in customHeaders)
                    {
                        _httpclient.DefaultRequestHeaders.Add(customHeader.Key, customHeader.Value);
                    }


                }

                _httpclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var parameter = string.Empty;
                var formencoded = string.Empty;
                var serialized = string.Empty;
                if (parameters != null)
                {
                    parameter = parameters.Aggregate(parameter,
                        (current, keyvalue) => current + $"{keyvalue.Key.ToLower()}={keyvalue.Value.ToLower()}&");
                }

                var uriLink = !string.IsNullOrEmpty(parameter) ? $"{url}?{parameter}" : url;

                if (uriLink.EndsWith("&"))
                {
                    uriLink = uriLink.Substring(0, uriLink.Length - 1);
                }

        
                var s = await _httpclient.DeleteAsync(uriLink.Replace(',', '.'))
                    .ConfigureAwait(false);
                if (s.IsSuccessStatusCode)
                {
                    var response = await s.Content.ReadAsStringAsync();
                    return new HttpResponse() { ErrorMessage = string.Empty, Response = response, Success = true };
                }

                return new HttpResponse()
                {
                    ErrorMessage = await s.Content.ReadAsStringAsync(),
                    Response = string.Empty,
                    Success = false
                };

            }
            catch (TaskCanceledException exception)
            {
                Debug.WriteLine("**** TASK CANCELLED EXCEPTION." + exception);

                if (!exception.CancellationToken.IsCancellationRequested)
                {
                    throw new TimeoutException(
                        "The connection timed out; please check your internet connection and try again.", exception);
                }

                throw;
            }
            catch (JsonException exception)
            {
                Debug.WriteLine("**** JSON EXCEPTION." + exception);
                throw;
            }
            catch (Exception exception)
            {
                Debug.WriteLine("**** GENERAL EXCEPTION." + exception);
                throw;
            }
        }
    }
}
