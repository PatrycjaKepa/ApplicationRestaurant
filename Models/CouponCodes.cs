﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
///<summary>Dodajemy klase CouponCodes która będzie odpowiadać za dodawanie kuponów rabatowych o różnych wartościach </summary>
namespace ApplicationRestaurant.Models
{
    public partial class CouponCodes
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }
    }
}