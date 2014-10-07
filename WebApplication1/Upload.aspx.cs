using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace WebApplication1
{
    public partial class Upload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["Log"] == null)
                {
                    Response.Redirect("Default.aspx");
                    Response.Write("<SCRIPT LANGUAGE = JavaScript > alert( Acces not allowed)</SCRIPT>");
                }
            }
            catch { }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string extn = string.Empty;
            Label1.Visible = false;
            if (FileUpload1.HasFile)
            {
                extn = System.IO.Path.GetExtension(FileUpload1.FileName);
                if (extn == ".csv" || extn == ".xlsx")
                {
                    FileUpload1.SaveAs(Server.MapPath("~/ups/") + Session["Log"] + ".csv");
                    emptyupload.Text = "Uploaded sucessfully";
                    emptyupload.ForeColor = System.Drawing.Color.ForestGreen;
                    emptyupload.Visible = true;
                }
                else
                {
                    emptyupload.Visible = true;
                    emptyupload.ForeColor = System.Drawing.Color.DarkRed;
                    emptyupload.Text = "Invalid file format";
                }
            }
            else
            {
                emptyupload.ForeColor = System.Drawing.Color.DarkRed;
                emptyupload.Text = "Choose your file";
                emptyupload.Visible = true;
            }
        }


        public static void Log(string logMessage, TextWriter w)
        {
            w.WriteLine("  {0}", logMessage);

        }



        protected void LinkButton1_Click1(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("login.aspx");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            string extn = string.Empty;
            emptyupload.Visible = false;
            if (FileUpload2.HasFile)
            {
                extn = System.IO.Path.GetExtension(FileUpload2.FileName);
                if (extn == ".csv" || extn == ".xlsx")
                {
                    FileUpload2.SaveAs(Server.MapPath("~/ups/spec/") + Session["Log"] + ".csv");
                    Label1.Text = "Uploaded sucessfully";
                    Label1.ForeColor = System.Drawing.Color.ForestGreen;
                    Label1.Visible = true;
                    StreamWriter flwrite = File.CreateText(Server.MapPath("~/ups/spec/") + Session["Log"] + ".txt");
                    flwrite.WriteLine(TxtPhone.Text);
                    flwrite.Close();
                }
                else
                {
                    Label1.Visible = true;
                    Label1.ForeColor = System.Drawing.Color.DarkRed;
                    Label1.Text = "Invalid file format";
                }
            }
            else
            {
                Label1.ForeColor = System.Drawing.Color.DarkRed;
                Label1.Text = "Choose your file";
                Label1.Visible = true;
            }
        }
    }
}