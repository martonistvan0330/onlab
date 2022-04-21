using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Webshop.Web.Shared.Models
{
    public class NewCustomer
    {
        public string Name { get; set; }
        public ShippingInfo ShippingInfo { get; set; }
        public PaymentInfo PaymentInfo { get; set; }

        public JsonObject ToJson()
        {
            var json = new JsonObject();
            json.Add(nameof(Name), Name);
            json.Add(nameof(ShippingInfo), ShippingInfo.ToJson());
            json.Add(nameof(PaymentInfo), PaymentInfo.ToJson());
            return json;
        }
    }
}
