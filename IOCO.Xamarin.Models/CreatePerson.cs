using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace IOCO.Models
{
    public class CreatePerson
    {
        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("birthDate")]
        public DateTime? BirthDate { get; set; }
    }
}
