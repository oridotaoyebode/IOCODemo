using System;
using System.Collections.Generic;
using System.Text;
using IOCO.Demo.Services.Base;
using IOCO.Demo.Services.Http;
using IOCO.Demo.Services.Json;

namespace IOCO.Demo.Services.Person
{
    public class PeopleService: BaseService<Models.Person>, IPeopleService
    {
        public PeopleService(IHttpService httpService, IJsonService jsonService) : base(httpService, jsonService)
        {
        }
    }
}
