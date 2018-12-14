using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Diagnostics;

public partial class Buy : System.Web.UI.Page
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
        else
            this.fillData();
    }

    private void fillData()
    {
        DB_CONNETION.Open();

        SQL_QUERY = "SELECT * FROM Content A WHERE A.id = (SELECT B.id FROM Original_Content B WHERE A.id = B.id)";

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

        while (QUERY_READER.Read())
        {
            TableRow row = new TableRow();

            Button button = new Button();

            for (int i = 0; i < 7; ++i)
            {
                if (i == 0)
                {
                    button.ID = QUERY_READER.GetValue(0).ToString();
                    button.Text = "Buy";
                    button.Click += new EventHandler(buying_handler);

                    TableCell cell = new TableCell();
                    cell.Controls.Add(button);
                    row.Cells.Add(cell);
                }
                else if (i == 3)
                    continue;
                else
                {
                    TableCell cell = new TableCell();
                    cell.Text = QUERY_READER.GetValue(i).ToString();
                    row.Cells.Add(cell);
                }

                contentTable.Rows.Add(row);
            }
            
        }

        QUERY_READER.Close();
        DB_CONNETION.Close();
    }

    protected void buying_handler(object sender, EventArgs e)
    {
        DB_CONNETION.Open();

        string check = "SELECT review_status, filter_status, rating FROM Original_Content WHERE id = " + ((sender as Button).ID);

        DB_COMMAND = new SqlCommand(check, DB_CONNETION);

        try
        {
            QUERY_READER = DB_COMMAND.ExecuteReader();
            QUERY_READER.Read();
            //Debug.WriteLine(">>$" + ());

            if (QUERY_READER.GetValue(0).ToString().Equals("False") || QUERY_READER.GetValue(1).ToString().Equals("False") || Convert.ToInt32(QUERY_READER.GetValue(2)) < 4)
                throw new Exception("Rating is less than 4 or Content has not been approved");
            else
            {
                QUERY_READER.Close();

                SQL_QUERY = "EXEC Apply_existing_request @viewer_id = " + Session["ID"] + ", @original_content_id = " + (sender as Button).ID;

                DB_COMMAND = new SqlCommand(SQL_QUERY, DB_CONNETION);

                DB_COMMAND.ExecuteReader();

                HttpContext.Current.Response.Write("<script>alert('Success')</script>");
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
}