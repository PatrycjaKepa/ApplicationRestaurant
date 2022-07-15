using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using ApplicationRestaurant.Data;
using ApplicationRestaurant.Models;
using ApplicationRestaurant.ValueObject;

namespace ApplicationRestaurant.Repository
{
	public class OrdersRepository
	{
		private readonly RestaurantAppContext context;

		public OrdersRepository()
		{
			this.context = new RestaurantAppContext();
		}

		public List<Orders> getAll()
		{
			return this.context.Orders.OrderByDescending(o => o.Id).ToList();
		}

		public Boolean createOrder(string name)
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
		}

		public Boolean removeById(int id)
        {
			try
			{
				Orders order = this.context.Orders.Where(o => o.Id == id).First();
				this.context.Orders.Remove(order);
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
