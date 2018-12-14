using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Diagnostics;

public partial class WriteComment : System.Web.UI.Page
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
    protected void submit_Click(object sender, EventArgs e)
    {
        DB_CONNETION.Open();

        SQL_QUERY = "Write_comment";

        DB_COMMAND = new SqlCommand(SQL_QUERY, DB_CONNETION);

        DB_COMMAND.CommandType = System.Data.CommandType.StoredProcedure;

        DB_COMMAND.Parameters.Add(new SqlParameter("@comment_text", comment.Text));
        DB_COMMAND.Parameters.Add(new SqlParameter("@viewer_id", Session["ID"]));
        DB_COMMAND.Parameters.Add(new SqlParameter("@original_content_id", content.SelectedItem.Value));
        DB_COMMAND.Parameters.Add(new SqlParameter("@written_time", DateTime.Now));

        try
        {
            DB_COMMAND.ExecuteReader();

            HttpContext.Current.Response.Write("<script>alert('" + "Comment Added Successfully" + "')</script>");
        }
        catch (SqlException ex)
        {
            HttpContext.Current.Response.Write("<script>alert('" + ex.Errors + "')</script>");
        }
        catch (Exception ex)
        {
            Debug.WriteLine(">>" + ex.Message);
        }

        DB_CONNETION.Close();
    }
}