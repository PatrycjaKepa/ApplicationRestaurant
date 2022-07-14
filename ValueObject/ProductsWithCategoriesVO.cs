using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationRestaurant.ValueObject
{
    public class ProductsWithCategoriesVO
    {
        public int Id { get;}
        public string Name { get; }
        public string CategoryName { get;}
        public decimal Price { get; }

        public ProductsWithCategoriesVO(int id, string name, string categoryName, decimal price)
        {
            this.Id = id;
            this.Name = name;
            this.CategoryName = categoryName;
            this.Price = price;
        }
    }
}
