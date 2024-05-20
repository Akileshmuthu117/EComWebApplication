using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EComWebApplication.DTO
{
    public class ProductDetail
    {
        public int ProdID { get; set; }
        public string ProdName { get; set; }
        public string ProdImage { get; set; }
        public decimal Price { get; set; }
        public string ProdDesc { get; set; }
    }
}