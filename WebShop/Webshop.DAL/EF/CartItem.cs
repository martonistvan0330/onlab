﻿namespace Webshop.DAL.EF
{
    public class CartItem
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public double Price { get; set; }
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int SizeId { get; set; }

        public Cart Cart { get; set; }
        public Product Product { get; set; }
        public Size Size { get; set; }
    }
}
