using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using ApplicationRestaurant.Data;
using ApplicationRestaurant.Models;

namespace ApplicationRestaurant.Repository
{
	public class UserRepository
	{
		private readonly RestaurantAppContext context;

		public UserRepository()
		{
			this.context = new RestaurantAppContext();
		}

		public List<Users> getAll()
        {
			return this.context.Users.ToList();
        }

		public Users getByName(string name)
        {
			try
            {
				Users user = this.context.Users.Where(u => u.UserName == name).FirstOrDefault<Users>();
				return user;
            } catch (Exception e){
				return null;
			}
        }

		public Boolean createUser(string name, string password)
        {
			try
            {
				Users user = new Users();
				user.UserName = name;
				user.Password = password;
				user.Role = "Waiter";
				var id = this.context.Orders.OrderByDescending(o => o.Id).FirstOrDefault();
				if (id != null)
				{
					user.Id = id.Id + 1;
				}
				else
				{
					user.Id = 1;
				}
				this.context.Users.Add(user);
				this.context.SaveChanges();
				return true;
			} catch (Exception e)
            {
				return false;
            }
        }
	}
}
