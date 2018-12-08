using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SCO : System.Web.UI.Page
{
    private SqlConnection DB_CONNETION;
    private SqlCommand DB_COMMAND;
    private SqlDataReader QUERY_READER;

    private string DB_CONNECTION_STRING = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
    private string SQL_QUERY;
    protected void Page_Load(object sender, EventArgs e)
    {
        DB_CONNETION = new SqlConnection(DB_CONNECTION_STRING);

        if (!IsPostBack)
        {
            this.fillContributorNames();
        }
    }

    private void fillContributorNames()
    {
        DB_CONNETION.Open();

        SQL_QUERY = "SELECT A.id, A.first_name, A.middle_name, A.last_name FROM dbo.[User] A, Contributor B WHERE A.id = B.id";

        DB_COMMAND = new SqlCommand(SQL_QUERY, DB_CONNETION);

        try
        {
            QUERY_READER = DB_COMMAND.ExecuteReader();
        }
        catch(SqlException ex)
        {
            System.Diagnostics.Debug.WriteLine(ex.Message);
        }
        catch(Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex.Message);
        }

        ListItem empty = new ListItem();
        empty.Text = "None";
        empty.Value = "";
        contributorNames.Items.Add(empty);

        while (QUERY_READER.Read())
        {
            string name = QUERY_READER.GetValue(1).ToString() + " " + QUERY_READER.GetValue(2).ToString() + " " + QUERY_READER.GetValue(3).ToString();

            ListItem item = new ListItem();
            item.Text = name;
            item.Value = QUERY_READER.GetValue(0).ToString();

            contributorNames.Items.Add(item);
        }

        DB_CONNETION.Close();
        QUERY_READER.Close();
    }

    protected void showContributors_Click(object sender, EventArgs e)
    {
        DB_CONNETION.Open();

        DB_COMMAND = new SqlCommand("Order_Contributor", DB_CONNETION);

        DB_COMMAND.CommandType = System.Data.CommandType.StoredProcedure;

        try
        {
            QUERY_READER = DB_COMMAND.ExecuteReader();

        }
        catch (SqlException ex)
        {
            System.Web.HttpContext.Current.Response.Write("<script> alert('" + ex.Message + "')</script>");
        }

        while (QUERY_READER.Read())
        {
            TableRow row = new TableRow();

            for (int i = 0; i < 15; ++i)
            {
                if (i == 0 || i == 4 || i == 5 || i == 12 || i == 13)
                    continue;

                TableCell cell = new TableCell();

                cell.Text = QUERY_READER.GetValue(i).ToString();
                row.Cells.Add(cell);
            }

            contributorTable.Rows.Add(row);
        }

        QUERY_READER.Close();
        DB_CONNETION.Close();
    }

    protected void showOriginalContent_Click(object sender, EventArgs e)
    {
        DB_CONNETION.Open();

        DB_COMMAND = new SqlCommand("Show_Original_Content", DB_CONNETION);

        DB_COMMAND.CommandType = System.Data.CommandType.StoredProcedure;

        if (contributorNames.SelectedItem.Value.Equals(""))
            DB_COMMAND.Parameters.Add(new SqlParameter("@contributor_id", DBNull.Value));
        else
            DB_COMMAND.Parameters.Add(new SqlParameter("@contributor_id", contributorNames.SelectedItem.Value));
        
        try
        {
            QUERY_READER = DB_COMMAND.ExecuteReader();
            
        }
        catch (SqlException ex)
        {
            System.Diagnostics.Debug.WriteLine(ex.Message);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex.Message);
        }

        List < List<int> > Content_User = new List< List<int> >();
        List<TableRow> rows = new List<TableRow>();


        while (QUERY_READER.Read())
        {
            int contributor = Convert.ToInt32(QUERY_READER.GetValue(9));
            int contentID = Convert.ToInt32(QUERY_READER.GetValue(0));

            List<int> IDs = new List<int>();
            IDs.Add(contentID);
            IDs.Add(contributor);
            Content_User.Add(IDs);

            TableRow row = new TableRow();
            row.ID = "content#" + contentID.ToString();

            TableCell cell;

            for (int i = 0; i < 13; ++i)
            {
                if (i == 5 || i == 7 || i == 8 || i == 10 || i == 11 || i == 12)
                {
                    cell = new TableCell();
                    cell.Text = QUERY_READER.GetValue(i).ToString();
                    row.Cells.Add(cell);
                }
            }

            rows.Add(row);
        }

        QUERY_READER.Close();

        
        for (int i = 0; i < Content_User.Count; ++i)
        {
            
            int contentID = Content_User[i][0];
            int userID = Content_User[i][1];
            
            string[] info = getContributor(userID);
            
            for (int j = 0; j < info.Length; ++j)
            {
                TableCell cell = new TableCell();

                cell.Text = info[j];
                rows[i].Cells.Add(cell);
            }

            contentTable.Rows.Add(rows[i]);
        }

        DB_CONNETION.Close();
    }

    protected string[] getContributor(int contributor)
    {

        SQL_QUERY = "SELECT A.email, A.first_name, A.middle_name, A.last_name, B.portfolio_link FROM dbo.[User] A, Contributor B WHERE A.id = B.id AND B.id = " + Convert.ToString(contributor);

        DB_COMMAND = new SqlCommand(SQL_QUERY, DB_CONNETION);
        
        try
        {
            QUERY_READER = DB_COMMAND.ExecuteReader();
        }
        catch (SqlException ex)
        {
            System.Diagnostics.Debug.WriteLine(ex.Message);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex.Message);
        }

        string[] info = new string[5];

        while (QUERY_READER.Read())
        {
            info[0] = QUERY_READER.GetValue(0).ToString();
            info[1] = QUERY_READER.GetValue(1).ToString();
            info[2] = QUERY_READER.GetValue(2).ToString();
            info[3] = QUERY_READER.GetValue(3).ToString();
            info[4] = QUERY_READER.GetValue(4).ToString();
        }

        QUERY_READER.Close();

        return info;
    }
}