﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using ApplicationRestaurant.Data;
using ApplicationRestaurant.Models;
using ApplicationRestaurant.ValueObject;

namespace ApplicationRestaurant.Repository
{
	public class ProductsRepository
	{
		private readonly RestaurantAppContext context;

		public ProductsRepository() // otwiera połączenie z tabelą produkty 
		{
			this.context = new RestaurantAppContext();
		}

		public List<Products> getAll()
		{
			return this.context.Products.ToList();
		}

		public Products getById(int id) // nadaje numer id produktom
        {
			try
            {
				return this.context.Products.Where(p => p.Id == id).First();
			} 
			catch (Exception e)
            {
				return null;
            }
        }

		public List<ProductsWithCategoriesVO> getProductWithCategoriesByCategoryName(string name) // dodaje do tabeeli nową pozycje
		{
			try
			{
				var list = this.context.Products
					.Join(
						this.context.Categories,
						p => p.CategoryId,
						c => c.Id,
						(p, c) => new
						{
							Id = p.Id,
							Name = p.Name,
							CategoryName = c.Name,
							Price = p.Price
						}
					)
					.Where(s => s.CategoryName == name) // przypisanie do właściwej kategorii
					.Select(pwc => new ProductsWithCategoriesVO(pwc.Id, pwc.Name, pwc.CategoryName, pwc.Price)
					).ToList();
				return list;
			}
			catch (Exception e)
			{
				return null;
			}
		}
	}
}