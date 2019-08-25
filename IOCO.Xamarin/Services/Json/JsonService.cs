using Newtonsoft.Json;
using System;

namespace IOCO.Xamarin.Services.Json
{
    public class JsonService: IJsonService
    {
        public T Deserialize<T>(string json, JsonSerializerSettings serializerSettings)
        {
            
            try
            {
                return JsonConvert.DeserializeObject<T>(json, serializerSettings);
            }
            catch (Exception)
            {
                return default(T);
            }

        }

        public string Serialize<T>(T content, JsonSerializerSettings serializerSettings)
        {
            string json;
            try
            {
               json=  JsonConvert.SerializeObject(content, Formatting.None,serializerSettings);
            }
            catch (Exception)
            {
                json = string.Empty;
            }

            return json;
        }
    }
}
