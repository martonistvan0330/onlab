using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Webshop.Web.Shared.Models
{
    public class PaymentInfo
    {
        public int PaymentMethodId { get; set; }
        public string PaymentMethodName { get; set; }
        public AddressInfo BillingAddressInfo { get; set; }

        public JsonObject ToJson()
        {
            var json = new JsonObject();
            json.Add(nameof(PaymentMethodId), PaymentMethodId);
            json.Add(nameof(PaymentMethodName), PaymentMethodName);
            json.Add(nameof(BillingAddressInfo), BillingAddressInfo.ToJson());
            return json;
        }
    }
}
