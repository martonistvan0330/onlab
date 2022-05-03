using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop.DAL.EF;

namespace Webshop.DAL.Repositories.Extensions
{
	public static class OrderRepositoryExtensions
	{
		public static IQueryable<Order> FilterByUserId(this IQueryable<Order> orders, string userId)
		{
			return orders.Where(o => o.Customer.UserId.Equals(userId));
		}

		public static IQueryable<Order> FindById(this IQueryable<Order> orders, int orderId)
		{
			return orders.Where(o => o.Id == orderId);
		}

		public static IQueryable<Order> WithStatus(this IQueryable<Order> orders)
			=> orders.Include(o => o.Status);

		public static IQueryable<Order> WithCustomer(this IQueryable<Order> orders)
			=> orders.Include(o => o.Customer);

		public static IQueryable<Order> WithShippingInfo(this IQueryable<Order> orders)
		{
			return orders
						.Include(o => o.Customer)
							.ThenInclude(c => c.ShippingInfo.ShippingMethod)
						.Include(o => o.Customer)
							.ThenInclude(c => c.ShippingInfo.ShippingAddressInfo)
								.ThenInclude(sai => sai.Address);
		}

		public static IQueryable<Order> WithPaymentInfo(this IQueryable<Order> orders)
		{
			return orders
						.Include(o => o.Customer)
							.ThenInclude(c => c.PaymentInfo.PaymentMethod)
						.Include(o => o.Customer)
							.ThenInclude(c => c.PaymentInfo.BillingAddressInfo)
								.ThenInclude(bai => bai.Address);
		}

		public static IQueryable<Order> WithOrderItems(this IQueryable<Order> orders)
		{
			return orders
						.Include(o => o.OrderItems)
							.ThenInclude(oi => oi.Product)
								.ThenInclude(p => p.ProductImages)
						.Include(o => o.OrderItems)
							.ThenInclude(oi => oi.Size);
		}

		public static Models.OrderDetails GetOrderDetails(this Order dbRecord)
		{
			var orderItems = new List<Models.OrderItem>();
			var total = 0.0;
			foreach (var orderItem in dbRecord.OrderItems)
			{
				orderItems.Add(new Models.OrderItem(
										orderItem.Product.Name,
										orderItem.Size.Name,
										orderItem.Amount,
										orderItem.Price,
										orderItem.Product.ProductImages.First(pi => pi.MainImage).ImageSource));
				total += orderItem.Price;
			}
			var customer = dbRecord.Customer;
			var shippingInfo = customer.ShippingInfo;
			var shippingAddressInfo = shippingInfo.ShippingAddressInfo;
			var shippingAddress = shippingAddressInfo.Address;
			var paymentInfo = customer.PaymentInfo;
			var billingAddressInfo = paymentInfo.BillingAddressInfo;
			var billingAddress = billingAddressInfo.Address;
			return new Models.OrderDetails(
								"email@email.com",
								shippingInfo.ShippingMethod.Method,
								new Models.AddressInfo(
										shippingAddressInfo.FirstName,
										shippingAddressInfo.LastName,
										new Models.Address(
												shippingAddress.Country,
												shippingAddress.Region,
												shippingAddress.ZipCode,
												shippingAddress.City,
												shippingAddress.Street
											),
										shippingAddressInfo.PhoneNumber
									),
								paymentInfo.PaymentMethod.Method,
								new Models.AddressInfo(
										billingAddressInfo.FirstName,
										billingAddressInfo.LastName,
										new Models.Address(
												billingAddress.Country,
												billingAddress.Region,
												billingAddress.ZipCode,
												billingAddress.City,
												billingAddress.Street
											),
										billingAddressInfo.PhoneNumber
									),
								orderItems.ToArray(),
								total);
		}

		public static async Task<IReadOnlyCollection<Models.Order>> GetOrders(this IQueryable<Order> orders)
		{
			return await orders
							.WithOrderItems()
							.WithStatus()
							.Select(dbRec => dbRec.GetOrder())
							.ToArrayAsync();
		}

		public static IReadOnlyCollection<Models.ProductStockWithId> GetProductStocks(this Order order)
		{
			return order.OrderItems.Select(oi => oi.GetProductStock()).ToArray();
		}

		public static  Models.Order GetOrder(this Order dbRecord)
		{
			return new Models.Order(
                dbRecord.Id,
                dbRecord.Status.Name,
                dbRecord.OrderItems.Sum(oi => oi.Amount * oi.Price),
                dbRecord.Status.Name == "Cancelled" || dbRecord.Status.Name == "Delivered");
		}

		public static Models.ProductStockWithId GetProductStock(this OrderItem dbRecord)
		{
			return new Models.ProductStockWithId(
				dbRecord.ProductId,
				dbRecord.SizeId,
				dbRecord.Amount);
		}
	}
}
