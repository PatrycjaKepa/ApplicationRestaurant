using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using ApplicationRestaurant.Data;
using ApplicationRestaurant.Models;
///<summary>Klasa odpowiedzialna za rejestrowanie nowych użytkowników</summary>
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

		public Users getByName(string name) // tworzymy użytkownika
        {
			try
            {
				Users user = this.context.Users.Where(u => u.UserName == name).FirstOrDefault<Users>();
				return user;
            } catch (Exception e){
				return null;
			}
        }

		public Boolean createUser(string name, string password)//  dodajemy użytkownika
        {
			try
            {
				Users user = new Users();
				user.UserName = name;
				user.Password = password;
				user.Role = "Waiter";
				var id = this.context.Users.OrderByDescending(o => o.Id).FirstOrDefault();
				if (id != null) // dodawanie użytkownika po id jeśli jest to pierwszy użytkownik przypisujemy mu wartość 1 każdemu następnemu wartość o jeden większą
				{
					user.Id = id.Id + 1;
				}
				else
				{
					user.Id = 1;
				}
				this.context.Users.Add(user);
				this.context.SaveChanges(); // zapisywanie zmian po każdym dodaniu użytkownika ponieważ tego wymaga flash
				return true;
			} catch (Exception e) 
            {
				return false;
            }
        }
	}
}
