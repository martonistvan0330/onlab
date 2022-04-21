using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Webshop.Web.Shared.Models
{
    public class Address
    {
        public string Country { get; set; }
        public string Region { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string Street { get; set; }

        public JsonObject ToJson()
        {
            var json = new JsonObject();
            json.Add(nameof(Country), Country);
            json.Add(nameof(Region), Region);
            json.Add(nameof(ZipCode), ZipCode);
            json.Add(nameof(City), City);
            json.Add(nameof(Street), Street);
            return json;
        }
    }
}
