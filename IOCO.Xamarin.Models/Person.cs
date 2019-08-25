using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace IOCO.Xamarin.Models
{
    public partial class Person
    {
        [JsonProperty("personId")]
        public int PersonId { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("birthDate")]
        public DateTimeOffset? BirthDate { get; set; }
    }

    public partial class Person
    {
        public static List<Person> FromJson(string json) => JsonConvert.DeserializeObject<List<Person>>(json, Converter.Settings);
    }

    
}
