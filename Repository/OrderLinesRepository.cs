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

		public OrderLinesRepository() // konstruktor otwierający połączenie z bazą danych oraz utworzenie nowej pozycji w niej
		{
			this.context = new RestaurantAppContext();
			this.productsRepository = new ProductsRepository();
		}

		public List<OrderLines> getByOrderId(int id)// dodanie nowego zamówienia do listy
		{
			return this.context.OrderLines.Where(ol => ol.OrderId == id).ToList();
		}

		public Boolean addOrUpdate(int orderId, int productId ) // dodaje lub aktualizuje zamówienie
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
				var id = this.context.OrderLines.OrderByDescending(o => o.Id).FirstOrDefault();// sprawdza czy zamówienie już istnieje i przypisuje jej wartość id +1
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

		public Boolean removeById(int id) // usuwa Id zamówienia  po wcisnięciu usuń zamówienie
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
