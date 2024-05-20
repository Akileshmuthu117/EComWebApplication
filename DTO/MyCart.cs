using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EComWebApplication.DTO
{
    public class MyCart
    {
        public int ProdID { get; set; }
        public string ProdName { get; set; }
        public int Qty { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
    }
}