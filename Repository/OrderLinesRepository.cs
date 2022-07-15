using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using ApplicationRestaurant.Data;
using ApplicationRestaurant.Models;
using ApplicationRestaurant.ValueObject;

namespace ApplicationRestaurant.Repository
{
	public class OrderLinesRepository
	{
		private readonly RestaurantAppContext context;
		private ProductsRepository productsRepository;

		public OrderLinesRepository()
		{
			this.context = new RestaurantAppContext();
			this.productsRepository = new ProductsRepository();
		}

		public List<OrderLines> getByOrderId(int id)
		{
			return this.context.OrderLines.Where(ol => ol.OrderId == id).ToList();
		}

		public Boolean addOrUpdate(int orderId, int productId )
        {
			try
			{
				var lineToAdd = this.context.OrderLines.Where(ol => ol.OrderId == orderId && ol.ProductId == productId).FirstOrDefault();

				if (lineToAdd != null)
                {
					lineToAdd.Quantity += 1;
					this.context.SaveChanges();
					return true;
                }

				Products product = this.productsRepository.getById(productId);
				OrderLines orderLine = new OrderLines();
				var id = this.context.OrderLines.OrderByDescending(o => o.Id).FirstOrDefault();
				if (id != null)
				{
					orderLine.Id = id.Id + 1;
				}
				else
				{
					orderLine.Id = 1;
				}
				orderLine.Name = product.Name;
				orderLine.ProductId = product.Id;
				orderLine.OrderId = orderId;
				orderLine.Quantity = 1;
				orderLine.Value = product.Price;

				this.context.OrderLines.Add(orderLine);
				this.context.SaveChanges();
				return true;
			}
			catch (Exception e)
			{
				return false;
			}
		}

		/*public Boolean createOrder(string name)
		{
			try
			{
				Orders order = new Orders();
				var id = this.context.Orders.OrderByDescending(o => o.Id).FirstOrDefault();
				if (id != null)
                {
					order.Id = id.Id + 1;
                } else {
					order.Id = 1;
				}
				this.context.Orders.Add(order);
				this.context.SaveChanges();
				return true;
			}
			catch (Exception e)
			{
				return false;
			}
		}*/

		public Boolean removeById(int id)
        {
			try
			{
				OrderLines orderLine = this.context.OrderLines.Where(ol => ol.Id == id).First();
				this.context.OrderLines.Remove(orderLine);
				this.context.SaveChanges();
				return true;
			}
			catch (Exception e)
			{
				return false;
			}
		}
	}
}
