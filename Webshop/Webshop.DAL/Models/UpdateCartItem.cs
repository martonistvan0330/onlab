using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webshop.DAL.Models
{
    public class UpdateCartItem
    {
        public readonly int Id;
        public readonly int SizeId;
        public readonly int Amount;

        public UpdateCartItem(int id, int sizeId, int amount)
        {
            Id = id;
            SizeId = sizeId;
            Amount = amount;
        }
    }
}
