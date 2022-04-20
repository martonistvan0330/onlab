using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Webshop.Web.Shared.Models
{
    public class UpdateCartItem
    {
        public int Id { get; set; }
        public int SizeId { get; set; }
        public int Amount { get; set; }

        public JsonObject ToJson()
        {
            var json = new JsonObject();
            json.Add(nameof(Id), Id);
            json.Add(nameof(SizeId), SizeId);
            json.Add(nameof(Amount), Amount);
            return json;
        }
    }
}
