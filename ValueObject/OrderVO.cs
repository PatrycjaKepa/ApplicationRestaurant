using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
///<summary>klasa która sprawdza czy zamówienie zostało utworzone</summary>
namespace ApplicationRestaurant.ValueObject
{
    public class OrderVO
    {
        public int Id { get; }

        public OrderVO(int id)
        {
            this.Id = id;
        }
    }
}
