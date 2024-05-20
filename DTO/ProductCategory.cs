using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EComWebApplication.DTO
{
    public class ProductCategory
    {
        public int CatID { get; set; }
        public string CatName { get; set; }
        public string CatImage { get; set; }
        public string CatDesc { get; set; }
    }
}