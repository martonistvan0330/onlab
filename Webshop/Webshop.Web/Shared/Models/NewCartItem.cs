using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Webshop.Web.Shared.Models
{
    public class NewCartItem
    {
        public int ProductId { get; set; }
        public int Amount { get; set; }
        public int SizeId { get; set; }

        public JsonObject ToJson()
        {
            var json = new JsonObject();
            json.Add(nameof(ProductId), ProductId);
            json.Add(nameof(Amount), Amount);
            json.Add(nameof(SizeId), SizeId);
            return json;
        }
    }
}
