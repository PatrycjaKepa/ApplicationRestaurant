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
	}
}
