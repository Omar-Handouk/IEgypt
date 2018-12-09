using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["ID"] == null)
        {
            loginButton.Enabled = true;
            logoutButton.Enabled = false;
        }
        else
        {
            loginButton.Enabled = false;
            logoutButton.Enabled = true;
        }
    }

    protected void loginButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("Login.aspx");
    }

    protected void registerButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("Register.aspx");
    }

    protected void searchButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("Search.aspx");
    }

    protected void logoutButton_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Response.Cookies["userID"].Expires = DateTime.Now.AddDays(-1);
        Response.Cookies["userType"].Expires = DateTime.Now.AddDays(-1);
        Response.Redirect("Default.aspx");
    }
}