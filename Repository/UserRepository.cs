using System.Collections.Generic;
using System.Linq;
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

		
	}
}
