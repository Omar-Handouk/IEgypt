using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.Configuration;

public partial class Search : System.Web.UI.Page {

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
            this.fillDropDowns();
        }
    }

    public void fillDropDowns()
    {
        DB_CONNETION.Open();

        ListItem empty = new ListItem();

        empty.Text = "None";
        empty.Value = "";

        contentCategory.Items.Add(empty);
        contentType.Items.Add(empty);

        SQL_QUERY = "SELECT TYPE FROM Content_Type";
        DB_COMMAND = new SqlCommand(SQL_QUERY, DB_CONNETION);
        QUERY_READER = DB_COMMAND.ExecuteReader();

        while (QUERY_READER.Read())
        {
            ListItem item = new ListItem();

            item.Text = QUERY_READER.GetValue(0).ToString();
            item.Value = QUERY_READER.GetValue(0).ToString();

            contentType.Items.Add(item);
        }

        QUERY_READER.Close();

        SQL_QUERY = "SELECT TYPE FROM Category";
        DB_COMMAND = new SqlCommand(SQL_QUERY, DB_CONNETION);
        QUERY_READER = DB_COMMAND.ExecuteReader();

        while (QUERY_READER.Read())
        {
            ListItem item = new ListItem();

            item.Text = QUERY_READER.GetValue(0).ToString();
            item.Value = QUERY_READER.GetValue(0).ToString();

            contentCategory.Items.Add(item);
        }

        QUERY_READER.Close();
        DB_CONNETION.Close();
    }

    protected void contentSearch_Click(object sender, EventArgs e)
    {
        DB_CONNETION.Open();

        DB_COMMAND = new SqlCommand("Original_Content_Search", DB_CONNETION);

        if (contentType.SelectedItem.Value.Equals(""))
            DB_COMMAND.Parameters.Add(new SqlParameter("@typename", DBNull.Value));
        else
            DB_COMMAND.Parameters.Add(new SqlParameter("@typename", contentType.SelectedItem.Value));

        if (contentCategory.SelectedItem.Value.Equals(""))
            DB_COMMAND.Parameters.Add(new SqlParameter("@categoryname", DBNull.Value));
        else
            DB_COMMAND.Parameters.Add(new SqlParameter("@categoryname", contentCategory.SelectedItem.Value));

        DB_COMMAND.CommandType = System.Data.CommandType.StoredProcedure;
        

        try
        {
            QUERY_READER = DB_COMMAND.ExecuteReader();

            while (QUERY_READER.Read())
            {
                TableRow row = new TableRow();

                for (int i = 7; i <= 12; ++i)
                {
                    if (i == 9)
                        continue;

                    TableCell cell = new TableCell();

                    cell.Text = QUERY_READER.GetValue(i).ToString();
                    row.Cells.Add(cell);
                }

                contentTable.Rows.Add(row);
            }

            QUERY_READER.Close();
        }
        catch (SqlException ex)
        {
            System.Web.HttpContext.Current.Response.Write("<script> alert('" + ex.Message.ToString() + "')</script>");
        }

        DB_CONNETION.Close();
    }

    protected void contributorSearch_Click(object sender, EventArgs e)
    {
        DB_CONNETION.Open();

        DB_COMMAND = new SqlCommand("Contributor_Search", DB_CONNETION);

        DB_COMMAND.CommandType = System.Data.CommandType.StoredProcedure;
        DB_COMMAND.Parameters.Add(new SqlParameter("@fullname", contributorName.Text));
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

    protected void contentType_SelectedIndexChanged(object sender, EventArgs e)
    {
        System.Diagnostics.Debug.WriteLine(contentType.SelectedItem.Value);
    }

    protected void contentCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        
    }
}