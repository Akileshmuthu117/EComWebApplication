using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EComWebApplication.DBConnection
{
    public static class Connectivity
    {

        public static string configuration()
        {
            bool integratedSecurity = Convert.ToBoolean(System.Web.Configuration.WebConfigurationManager.AppSettings["integsec"]);
            SqlConnectionStringBuilder sb = new SqlConnectionStringBuilder();
            sb.DataSource = System.Web.Configuration.WebConfigurationManager.AppSettings["myservername"].ToString();
            sb.InitialCatalog = System.Web.Configuration.WebConfigurationManager.AppSettings["mydbname"].ToString();
            if (integratedSecurity)
            {
                sb.IntegratedSecurity = true;
            }
            else
            {
                sb.IntegratedSecurity = false;
                sb.UserID = System.Web.Configuration.WebConfigurationManager.AppSettings["userid"].ToString();
                sb.Password = System.Web.Configuration.WebConfigurationManager.AppSettings["mypwd"].ToString();
            }
            return sb.ConnectionString;
        }
    }
}