using EComWebApplication.DBConnection;
using EComWebApplication.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace EComWebApplication.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }



        public ActionResult CateGoryList()
        {
            return View();
        }


        public ActionResult ProductList(int categoryid)
        {
            ViewBag.CateGoryID = categoryid;
            return View();
        }

        public ActionResult MyCart()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ImagePreview(string filename, string type)
        {
            string fileDirectory = (type == "cat") ? WebConfigurationManager.AppSettings["categoryfolder"] : WebConfigurationManager.AppSettings["imagefolder"];
            var filePath = Path.Combine(fileDirectory, filename + ".jpg");
            byte[] imageArray = System.IO.File.ReadAllBytes(@filePath);
            return File(new MemoryStream(imageArray), "image/png");
        }

        [HttpGet]
        public JsonResult GetProductCatory()
        {
            string imageUrl = WebConfigurationManager.AppSettings["imageretreiveurl"];

            using (SqlConnection conn = new SqlConnection(Connectivity.configuration()))
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand("SP_GetCategoryList", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable dtble = new DataTable();
                adp.Fill(dtble);
                conn.Close();
                List<ProductCategory> prodCategory = new List<ProductCategory>();

                foreach (DataRow hrow in dtble.Rows)
                {
                    prodCategory.Add(new ProductCategory
                    {
                        CatID = Convert.ToInt32(hrow["CatID"]),
                        CatName = hrow["CatName"].ToString(),
                        CatImage = string.Format(imageUrl, hrow["CatImage"].ToString(), "cat"),
                        CatDesc = hrow["CatDesc"].ToString()
                    });
                }

                return new JsonResult
                {
                    Data = prodCategory,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
        }

        [HttpGet]
        public JsonResult GetProductList(int categoryid)
        {

            string imageUrl = WebConfigurationManager.AppSettings["imageretreiveurl"];

            using (SqlConnection conn = new SqlConnection(Connectivity.configuration()))
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand("SP_GetProductList", conn);
                cmd.Parameters.AddWithValue("@CatID", categoryid);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable dtble = new DataTable();
                adp.Fill(dtble);
                conn.Close();
                List<ProductDetail> product = new List<ProductDetail>();

                foreach (DataRow hrow in dtble.Rows)
                {
                    product.Add(new ProductDetail
                    {
                        ProdID = Convert.ToInt32(hrow["ProdID"]),
                        ProdName = hrow["ProdName"].ToString(),
                        ProdImage = string.Format(imageUrl, hrow["ProdImg"].ToString(), "prod"),
                        Price = Convert.ToDecimal(hrow["Price"]),
                        ProdDesc = hrow["ProdDesc"].ToString()
                    });
                }

                return new JsonResult
                {
                    Data = product,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
        }

        [HttpGet]
        public JsonResult GetCartProductQty(int productid)
        {
            using (SqlConnection conn = new SqlConnection(Connectivity.configuration()))
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand($"select Qty from MyCart where ProdID={productid}", conn);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable dtble = new DataTable();
                adp.Fill(dtble);
                conn.Close();
                return new JsonResult
                {
                    Data = dtble.Rows.Count > 0 ? dtble.Rows[0][0] : "",
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
        }

        [HttpPost]
        public ActionResult InsertUpdateCart(int productid, int qty)
        {
            using (SqlConnection conn = new SqlConnection(Connectivity.configuration()))
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand("SP_InsertUpdateCart", conn);
                cmd.Parameters.AddWithValue("@ProdID", productid);
                cmd.Parameters.AddWithValue("@qty", qty);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                conn.Close();
            }

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }


        [HttpPost]
        public ActionResult deletecartproduct(int productid)
        {
            using (SqlConnection conn = new SqlConnection(Connectivity.configuration()))
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand($"delete from MyCart where ProdID={productid}", conn);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                conn.Close();
            }

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }



        [HttpGet]
        public JsonResult GetMyCartProduct()
        {
            using (SqlConnection conn = new SqlConnection(Connectivity.configuration()))
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand("SP_GetMyCartData", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable dtble = new DataTable();
                adp.Fill(dtble);
                conn.Close();
                List<MyCart> myCart = new List<MyCart>();

                foreach (DataRow hrow in dtble.Rows)
                {
                    myCart.Add(new MyCart
                    {
                        ProdID = Convert.ToInt32(hrow["ProdID"]),
                        ProdName = hrow["ProdName"].ToString(),
                        Qty = Convert.ToInt32(hrow["Qty"]),
                        Price = Convert.ToDecimal(hrow["Price"]),
                        TotalPrice = Convert.ToDecimal(hrow["TotalPrice"])
                    });
                }
                return new JsonResult
                {
                    Data = myCart,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };

            }
        }
    }
}