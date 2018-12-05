using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        System.Diagnostics.Debug.WriteLine("Default Page Loaded");
    }

    public void showEvents(object sender, EventArgs e)
    {

        string EID = eventID.Text;

        string DB_ConnectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        SqlConnection DB_Connection = new SqlConnection(DB_ConnectionString);

        DB_Connection.Open();

        SqlCommand DB_Command = new SqlCommand("Show_Event", DB_Connection);

        DB_Command.CommandType = System.Data.CommandType.StoredProcedure;

        DB_Command.Parameters.Add(new SqlParameter("@event_id", EID));

        SqlDataReader Query_Output = DB_Command.ExecuteReader();

        while (Query_Output.Read())
        {
            TableRow newRecord = new TableRow();
            
            for (int i = 0; i < Query_Output.FieldCount - 2; ++i)
            {
                TableCell record = new TableCell();
                record.Text = Query_Output.GetValue(i).ToString();
                newRecord.Controls.Add(record);
            }

            eventsTable.Controls.Add(newRecord);
        }

        Query_Output.Close();
        DB_Connection.Close();
    }
}
