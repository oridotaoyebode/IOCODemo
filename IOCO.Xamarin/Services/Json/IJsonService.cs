using Newtonsoft.Json;

namespace IOCO.Xamarin.Services.Json
{
    public interface IJsonService
    {
        T Deserialize<T>(string json, JsonSerializerSettings serializerSettings);
        string Serialize<T>(T content, JsonSerializerSettings serializerSettings);
    }
}
