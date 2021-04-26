using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace los_api.Models
{
    public partial class Product
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public string imageUrl { get; set; }
        public int price { get; set; }
    }

    public partial class Stock
    {
        [Key]
        public int id { get; set; }
        public int productId { get; set; }
        public int amount { get; set; }
    }
}
