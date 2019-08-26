using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace IOCO.Models
{
    public class CreateEmployee
    {
        [JsonProperty("personId")]
        public int PersonId { get; set; }

        [JsonProperty("employeeNumber")]
        public string EmployeeNumber { get; set; }

        [JsonProperty("employedDate")]
        public DateTime? EmployedDate { get; set; }

        [JsonProperty("terminatedDate")]
        public DateTime? TerminatedDate { get; set; }
    }
}
