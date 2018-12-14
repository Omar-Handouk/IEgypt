using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Diagnostics;

public partial class DeleteRequest : System.Web.UI.Page
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

        SQL_QUERY = "SELECT A.id, A.information FROM [dbo].[New_Request] A WHERE A.viewer_id = " + Session["ID"] + " AND accept_status IS NULL";

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
            string desc = QUERY_READER.GetValue(1).ToString();

            item = new ListItem();
            item.Text = desc;
            item.Value = QUERY_READER.GetValue(0).ToString();

            content.Items.Add(item);
        }

        DB_CONNETION.Close();
        QUERY_READER.Close();
    }

    protected void delete_Click(object sender, EventArgs e)
    {
        DB_CONNETION.Open();

        SQL_QUERY = "Delete_new_request";

        DB_COMMAND = new SqlCommand(SQL_QUERY, DB_CONNETION);

        DB_COMMAND.CommandType = System.Data.CommandType.StoredProcedure;

        DB_COMMAND.Parameters.Add(new SqlParameter("@request_id", content.SelectedItem.Value));

        try
        {
            using (QUERY_READER = DB_COMMAND.ExecuteReader()) { }
            HttpContext.Current.Response.Write("<script>alert('" + "Success" + "')</script>");
        }
        catch (SqlException ex)
        {
            HttpContext.Current.Response.Write("<script>alert('" + ex.Message + "')</script>");
        }
        catch (Exception ex)
        {
            Debug.WriteLine(">>" + ex.Message);
        }

        DB_CONNETION.Close();
    }
}