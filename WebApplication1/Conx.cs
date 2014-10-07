using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace WebApplication1
{
    public class Conx
    {
        public static string CnString = "Data Source=.;Initial Catalog=BillingLogin;Persist Security Info=True;User ID=sa;Password=qwertz";
        public static SqlConnection cn = new SqlConnection(CnString);
        public static DataSet Ds = new DataSet("Mondataset");
        public static SqlDataAdapter Da;
        public static SqlDataAdapter dapass;
    }
}