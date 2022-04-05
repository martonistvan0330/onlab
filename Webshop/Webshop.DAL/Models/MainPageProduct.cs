using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webshop.DAL.Models
{
    public class MainPageProduct
    {
        public int Id { get; set; }
        public readonly string Name;
        public readonly double Price;
        public readonly string ImageSource;

        public MainPageProduct(int id, string name, double price, string imageSource)
        {
            Id = id;
            Name = name;
            Price = price;
            ImageSource = imageSource;
        }
    }
}
