using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;

public partial class Login : System.Web.UI.Page
{
    private SqlConnection DB_CONNETION;
    private SqlCommand DB_COMMAND;
    private SqlDataReader QUERY_READER;

    private string DB_CONNECTION_STRING = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
    private string SQL_QUERY;

    protected void Page_Load(object sender, EventArgs e)
    {
        DB_CONNETION = new SqlConnection(DB_CONNECTION_STRING);
    }

    protected void login_Click(object sender, EventArgs e)
    {
        DB_CONNETION.Open();

        DB_COMMAND = new SqlCommand("User_login", DB_CONNETION);
        DB_COMMAND.CommandType = System.Data.CommandType.StoredProcedure;
        DB_COMMAND.Parameters.Add(new SqlParameter("@email", userEmail.Text));
        DB_COMMAND.Parameters.Add(new SqlParameter("@password", userPassword.Text));
        DB_COMMAND.Parameters.Add("@user_id", System.Data.SqlDbType.Int).Direction = System.Data.ParameterDirection.Output;

        int UID = 0;
        
        try
        {
            using (QUERY_READER = DB_COMMAND.ExecuteReader())
            {
                UID = Convert.ToInt32(DB_COMMAND.Parameters["@user_id"].Value);
            }
        }
        catch (SqlException ex)
        {
            System.Web.HttpContext.Current.Response.Write("<script>alert('" + ex.Message + "')</script>");
        }
        catch (Exception ex)
        {
            System.Web.HttpContext.Current.Response.Write("<script>alert('" + ex.Message + "')</script>");
        }

        string UType = userType(UID);

        Session["ID"] = UID;
        Session["type"] = UType;

        HttpCookie userIDCookie = new HttpCookie("userID");
        userIDCookie.Value = UID.ToString();

        HttpCookie userTypeCookie = new HttpCookie("userType");
        userTypeCookie.Value = UType;

        Response.Cookies.Add(userIDCookie);
        Response.Cookies.Add(userTypeCookie);

        Response.Redirect("Default.aspx?type=" + UType + "&ID=" + UID);

        DB_CONNETION.Close();
    }

    private string userType(int userID)
    {
        DB_COMMAND = new SqlCommand("User_Type", DB_CONNETION);

        DB_COMMAND.CommandType = System.Data.CommandType.StoredProcedure;

        DB_COMMAND.Parameters.Add(new SqlParameter("@userID", userID));

        DB_COMMAND.Parameters.Add("@userType", System.Data.SqlDbType.VarChar, 25).Direction = System.Data.ParameterDirection.Output;

        string userType = "";
        
        try
        {
            using (QUERY_READER = DB_COMMAND.ExecuteReader())
                userType = Convert.ToString(DB_COMMAND.Parameters["@userType"].Value);
        }
        catch (SqlException ex)
        {
            Debug.WriteLine(ex.Message);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }

        if (userType.Equals(""))
            throw new Exception("User Not Found");

        return userType;
    }

}