using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace IOCO.Models
{
    public partial class Employee
    {
        [JsonProperty("employeeId")]
        public int EmployeeId { get; set; }

        [JsonProperty("personId")]
        public int PersonId { get; set; }

        [JsonProperty("employeeNumber")]
        public string EmployeeNumber { get; set; }

        [JsonProperty("employedDate")]
        public DateTime? EmployedDate { get; set; }

        [JsonProperty("terminatedDate")]
        public DateTime? TerminatedDate { get; set; }
    }

    public partial class Employee
    {
        public static List<Employee> FromJson(string json) => JsonConvert.DeserializeObject<List<Employee>>(json, Converter.Settings);
    }
}
