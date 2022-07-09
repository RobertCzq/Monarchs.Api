using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monarchs.Common.Models
{
    public class MonarchJsonModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("nm")]
        public string Name { get; set; }
        [JsonProperty("cty")]
        public string Country { get; set; }
        [JsonProperty("hse")]
        public string House { get; set; }
        [JsonProperty("yrs")]
        public string Years { get; set; }

        public MonarchJsonModel(int id, string name, string country, string house, string years)
        {
            Id = id;
            Name = name;
            Country = country;
            House = house;
            Years = years;
        }
    }
}
