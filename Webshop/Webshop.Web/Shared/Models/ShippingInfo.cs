using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Webshop.Web.Shared.Models
{
    public class ShippingInfo
    {
        public int ShippingMethodId { get; set; }
        public string ShippingMethodName { get; set; }
        public AddressInfo ShippingAddressInfo { get; set; }

        public JsonObject ToJson()
        {
            var json = new JsonObject();
            json.Add(nameof(ShippingMethodId), ShippingMethodId);
            json.Add(nameof(ShippingMethodName), ShippingMethodName);
            json.Add(nameof(ShippingAddressInfo), ShippingAddressInfo.ToJson());
            return json;
        }
    }
}
