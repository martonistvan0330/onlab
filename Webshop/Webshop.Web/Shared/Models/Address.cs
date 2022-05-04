using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Webshop.Web.Shared.Models
{
    public class Address
    {
        [Required]
        public string Country { get; set; }
        [Required]
        public string Region { get; set; }
        [Required]
        [StringLength(10, ErrorMessage = "ZIP Code too long")]
        [DataType(DataType.PostalCode)]
        public string ZipCode { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
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
