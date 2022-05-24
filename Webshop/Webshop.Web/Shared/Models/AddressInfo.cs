using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Webshop.Web.Shared.Models
{
    public class AddressInfo
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public Address Address { get; set; }
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        public JsonObject ToJson()
        {
            var json = new JsonObject();
            json.Add(nameof(FirstName), FirstName);
            json.Add(nameof(LastName), LastName);
            json.Add(nameof(Address), Address?.ToJson());
            json.Add(nameof(PhoneNumber), PhoneNumber);
            return json;
        }

		public AddressInfo()
		{
            Address = new();
		}
    }
}
