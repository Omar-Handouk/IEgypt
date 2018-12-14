using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Diagnostics;
public partial class RequestContent : System.Web.UI.Page
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

        SQL_QUERY = "SELECT A.ID, A.first_name, A.middle_name, A.last_name FROM [dbo].[User] A, Contributor B WHERE A.ID = B.ID";

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

        ListItem item = new ListItem();
        item.Text = "Any Contributor";
        item.Value = "";

        contributorName.Items.Add(item);

        while (QUERY_READER.Read())
        {
            string name = QUERY_READER.GetValue(1).ToString() + " " + QUERY_READER.GetValue(2).ToString() + " " + QUERY_READER.GetValue(3).ToString();

            item = new ListItem();
            item.Text = name;
            item.Value = QUERY_READER.GetValue(0).ToString();

            contributorName.Items.Add(item);
        }

        DB_CONNETION.Close();
        QUERY_READER.Close();
    }

    protected void submitRequest_Click(object sender, EventArgs e)
    {
        DB_CONNETION.Open();

        SQL_QUERY = "Apply_New_Request";

        DB_COMMAND = new SqlCommand(SQL_QUERY, DB_CONNETION);

        DB_COMMAND.CommandType = System.Data.CommandType.StoredProcedure;

        DB_COMMAND.Parameters.Add(new SqlParameter("@information", description.Text));
        
        if (contributorName.SelectedItem.Value.Equals(""))
            DB_COMMAND.Parameters.Add(new SqlParameter("@contributor_id", DBNull.Value));
        else
            DB_COMMAND.Parameters.Add(new SqlParameter("@contributor_id", contributorName.SelectedItem.Value));

        DB_COMMAND.Parameters.Add(new SqlParameter("@viewer_id", Session["ID"]));

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