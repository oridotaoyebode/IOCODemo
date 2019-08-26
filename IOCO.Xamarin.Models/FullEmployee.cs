using System;
using System.Collections.Generic;
using System.Text;

namespace IOCO.Models
{
    public class FullEmployee
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmployeeNumber { get; set; }
        public DateTime? EmployedDate { get; set; }
        public DateTime? TerminatedDate { get; set; }
        public DateTime? BirthDate { get; set; }
    }
}
