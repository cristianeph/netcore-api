using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RetoBackendApp.Models
{
    public class Client
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Birthdate { get; set; }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
