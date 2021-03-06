using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webshop.DAL.Models
{
	public class OrderItem
	{
		public readonly string ProductName;
		public readonly string SizeName;
		public readonly int Amount;
		public readonly double Price;
		public readonly byte[] Image;

		public OrderItem(string productName, string sizeName, int amount, double price, byte[] image)
		{
			ProductName = productName;
			SizeName = sizeName;
			Amount = amount;
			Price = price;
			Image = image;
		}
	}
}
