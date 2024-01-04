using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace weatherForm.DTO
{
    public class Current
    {
        public double temp_c { get; set; }
        public double temp_f { get; set;}
        public double wind_mph { get; set; }
        public double wind_kph { get; set; }
        public string wind_dir { get; set; }
        public int cloud { get; set; }
        public condition condition { get; set; }
    }
}
