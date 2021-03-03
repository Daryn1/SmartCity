using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartCity.Web.Models.Bus
{
    public class ManageBusWorkerViewModel
    {   
        public long Id { get; set; }
        public string License { get; set; }
        public string Bus { get; set; }
    }
}
