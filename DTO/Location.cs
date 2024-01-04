using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace weatherForm.DTO
{
    public class Location
    {
        public string  name { get; set; }
        public string region { get; set; }
        public string country { get; set; }
        public string localtime { get; set; }
        public double lat { get; set; }
        public double lon { get; set; }


    }
}
