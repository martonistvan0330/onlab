using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Webshop.Web.Shared.Models
{
    public class NewProduct
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [Range(1000, 200000)]
        public double Price { get; set; } = 1000;
        [Required]
        [Range(1, int.MaxValue)]
        public int CategoryId { get; set; }
        [Required]
        [Range(0, 100)]
        public int XS { get; set; } = 0;
        [Required]
        [Range(0, 100)]
        public int S { get; set; } = 0;
        [Required]
        [Range(0, 100)]
        public int M { get; set; } = 0;
        [Required]
        [Range(0, 100)]
        public int L { get; set; } = 0;
        [Required]
        [Range(0, 100)]
        public int XL { get; set; } = 0;
        [Required]
        [Range(0, 100)]
        public int XXL { get; set; } = 0;

        public JsonObject ToJson()
        {
            var json = new JsonObject();
            json.Add(nameof(Name), Name);
            json.Add(nameof(Price), Price);
            json.Add(nameof(CategoryId), CategoryId);
            json.Add(nameof(XS), XS);
            json.Add(nameof(S), S);
            json.Add(nameof(M), M);
            json.Add(nameof(L), L);
            json.Add(nameof(XL), XL);
            json.Add(nameof(XXL), XXL);
            return json;
        }
    }
}
