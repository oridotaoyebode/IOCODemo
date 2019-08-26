using System;
using System.Collections.Generic;
using System.Text;
using IOCO.Demo.Services.Base;
using IOCO.Demo.Services.Http;
using IOCO.Demo.Services.Json;
using IOCO.Demo.Services.Person;

namespace IOCO.Demo.Services.Employee
{
    public class EmployeeService: BaseService<Models.Employee>, IEmployeeService

    {
        public EmployeeService(IHttpService httpService, IJsonService jsonService) : base(httpService, jsonService)
        {
        }
    }
}
