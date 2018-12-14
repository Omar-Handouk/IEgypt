using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Diagnostics;


public partial class RateContent : System.Web.UI.Page
{
    private SqlConnection DB_CONNETION;
    private SqlCommand DB_COMMAND;
    private SqlDataReader QUERY_READER;

    private string DB_CONNECTION_STRING = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
    private string SQL_QUERY;

    private int USER_ID;
    private string USER_TYPE;
    protected void Page_Load(object sender, EventArgs e)
    {
        DB_CONNETION = new SqlConnection(DB_CONNECTION_STRING);

        USER_ID = Convert.ToInt32(Session["ID"]);
        USER_TYPE = Convert.ToString(Session["type"]);

        if (!USER_TYPE.Equals("Viewer"))
            Response.Redirect("Default.aspx");

        if (!IsPostBack)
            this.fillData();
    }

    private void fillData()
    {
        DB_CONNETION.Open();

        SQL_QUERY = "SELECT B.rating, A.link, A.uploaded_at, A.category_type, A.subcategory_name, A.type, A.id FROM [dbo].[Content] A, [dbo].[Original_Content] B WHERE A.id = B.id";

        DB_COMMAND = new SqlCommand(SQL_QUERY, DB_CONNETION);

        try
        {
            QUERY_READER = DB_COMMAND.ExecuteReader();
        }
        catch (SqlException ex)
        {
            HttpContext.Current.Response.Write("<script>alert('" + ex.Message + "')</script>");
        }
        catch (Exception ex)
        {
            Debug.WriteLine(">>" + ex.Message);
        }

        ListItem item;

        while (QUERY_READER.Read())
        {
            string desc = "";

            for (int i = 0; i < 6; ++i)
            {
                if (i == 5)
                    desc += QUERY_READER.GetValue(i).ToString();
                else
                    desc += QUERY_READER.GetValue(i).ToString() + "-";
            }

            item = new ListItem();
            item.Text = desc;
            item.Value = QUERY_READER.GetValue(6).ToString();

            content.Items.Add(item);
        }

        DB_CONNETION.Close();
        QUERY_READER.Close();
    }

    protected void rate_Click(object sender, EventArgs e)
    {
        DB_CONNETION.Open();

        Boolean error = checkErrors(Convert.ToInt32(content.SelectedItem.Value));

        SQL_QUERY = "Rating_original_content";

        DB_COMMAND = new SqlCommand(SQL_QUERY, DB_CONNETION);

        DB_COMMAND.CommandType = System.Data.CommandType.StoredProcedure;

        DB_COMMAND.Parameters.Add(new SqlParameter("@orignal_content_id", content.SelectedItem.Value));
        DB_COMMAND.Parameters.Add(new SqlParameter("@rating_value", rating.SelectedItem.Value));
        DB_COMMAND.Parameters.Add(new SqlParameter("@viewer_id", Session["ID"]));

        

        try
        {
            if (error)
            {
                throw new Exception("Content Has not been approved, you can not rate it");
            }
            else
            {
                using (QUERY_READER = DB_COMMAND.ExecuteReader()) { }
                HttpContext.Current.Response.Write("<script>alert('" + "Success" + "')</script>");
            }
        }
        catch (SqlException ex)
        {
            HttpContext.Current.Response.Write("<script>alert('" + ex.Message + "')</script>");
        }
        catch (Exception ex)
        {
            HttpContext.Current.Response.Write("<script>alert('" + ex.Message + "')</script>");
        }

        DB_CONNETION.Close();
    }

    private Boolean checkErrors(int ID)
    {
        SQL_QUERY = "SELECT review_status, filter_status FROM Original_Content WHERE id = " + ID;

        DB_COMMAND = new SqlCommand(SQL_QUERY, DB_CONNETION);

        Boolean check = false;

        try
        {
            QUERY_READER = DB_COMMAND.ExecuteReader();
        }
        catch (SqlException ex)
        {
            HttpContext.Current.Response.Write("<script>alert('" + ex.Message + "')</script>");
        }
        catch (Exception ex)
        {
            Debug.WriteLine(">>" + ex.Message);
        }

        while (QUERY_READER.Read())
        {
            if (QUERY_READER.GetValue(0).ToString().Equals("False") || QUERY_READER.GetValue(1).ToString().Equals("False"))
                check = true;
        }

        QUERY_READER.Close();

        return check;
    }
}