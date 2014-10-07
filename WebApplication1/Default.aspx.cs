using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace WebApplication1
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Log"] != null)
            {
                Response.Redirect("Upload.aspx");
            }
        }

        protected void Button1_Click1(object sender, EventArgs e)
        {
            try
            {
                SqlCommand oCommand = new SqlCommand("SELECT * FROM Member WHERE email ='" + TxtIdentifiant.Text + "'", Conx.cn);
                Conx.Da = new SqlDataAdapter(oCommand);
                Conx.Da.Fill(Conx.Ds, "mem");
                int nbr = 0;
                for (int i = 0; i < Conx.Ds.Tables["mem"].Rows.Count; i++)
                {
                    if (Equals(Convert.ToString(Conx.Ds.Tables["mem"].Rows[i]["password"]), TxtPassword.Text))
                    {
                        Session["Log"] = TxtIdentifiant.Text;
                        nbr = i;
                        Response.Redirect("Upload.aspx");
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        
    }
}