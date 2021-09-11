using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Weather.Models
{
    public class JordanWeather
    {
        public Dictionary<string, object> location { get; set; }

        public Dictionary<string, object> current { get; set; }
    }
}