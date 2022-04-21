using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webshop.DAL.Models
{
    public class Cart
    {
        public readonly int Id;
        public readonly string UserId;

        public Cart(int id, string userId)
        {
            Id = id;
            UserId = userId;
        }
    }
}
