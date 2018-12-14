using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Diagnostics;

public partial class EditDeleteComment : System.Web.UI.Page
{
    private SqlConnection DB_CONNETION;
    private SqlCommand DB_COMMAND;
    private SqlDataReader QUERY_READER;

    private string DB_CONNECTION_STRING = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
    private string SQL_QUERY;

    private int USER_ID;
    private string USER_TYPE;

    private List<List<string>> commentRepo = new List<List<string>>();

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

        SQL_QUERY = "SELECT A.text, C.link, A.date, A.original_content_id FROM [dbo].[Comment] A, [dbo].[Original_Content] B, [dbo].[Content] C WHERE A.viewer_id = " + Session["ID"] + " AND A.original_content_id = B.id AND C.id = B.id";

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

            string text = "Comment: " + QUERY_READER.GetValue(0).ToString() + " --- Link: " + QUERY_READER.GetValue(1).ToString();
            string id = QUERY_READER.GetValue(0).ToString() + "_" + QUERY_READER.GetValue(1).ToString() + "_" + (String.Format("{0:yyyy-MM-dd HH:mm:ss.fff}", Convert.ToDateTime(QUERY_READER["date"]))) + "_" + QUERY_READER.GetValue(3).ToString();

            ListItem item = new ListItem();
            item.Text = text;
            item.Value = id;

            comments.Items.Add(item);
        }
        
        DB_CONNETION.Close();
        QUERY_READER.Close();
    }

    protected void comments_SelectedIndexChanged(object sender, EventArgs e)
    {
        commentLabel.Visible = true;
        commentText.Visible = true;

        string[] splitter = comments.SelectedItem.Value.Split('_');

        TableRow row = new TableRow();
        
        for (int i = 0; i < 3; ++i)
        {
            if (i == 0)
                commentText.Text = splitter[0];

            TableCell cell = new TableCell();

            cell.Text = splitter[i];

            row.Cells.Add(cell);
        }

        commentInfo.Rows.Add(row);
    }

    protected void deleteComment_Click(object sender, EventArgs e)
    {
        if (comments.SelectedItem.Value.Equals(""))
            HttpContext.Current.Response.Write("<script>alert('Please Choose A Comment')</script>");
        else
        {
            DB_CONNETION.Open();

            SQL_QUERY = "Delete_comment";

            DB_COMMAND = new SqlCommand(SQL_QUERY, DB_CONNETION);

            DB_COMMAND.CommandType = System.Data.CommandType.StoredProcedure;

            string[] splitter = comments.SelectedItem.Value.Split('_');

            for (int i = 0; i < splitter.Length; ++i)
                Debug.WriteLine(splitter[i]);

            DB_COMMAND.Parameters.Add(new SqlParameter("@viewer_id", Session["ID"]));
            DB_COMMAND.Parameters.Add(new SqlParameter("@original_content_id", splitter[3]));
            DB_COMMAND.Parameters.Add(new SqlParameter("@written_time", splitter[2]));

            try
            {
                DB_COMMAND.ExecuteNonQuery();

                HttpContext.Current.Response.Write("<script>alert('" + "Comment Deleted Successfully" + "')</script>");

                WaitNSeconds(2, "EditDeleteComment.aspx");
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

    private void WaitNSeconds(int segundos, string path)
    {
        if (segundos < 1) return;
        DateTime _desired = DateTime.Now.AddSeconds(segundos);
        while (DateTime.Now < _desired)
        {
            Debug.WriteLine(">>Sleeping<<");
            Response.Redirect(path);
        }
    }

    protected void editComment_Click(object sender, EventArgs e)
    {
        if (comments.SelectedItem.Value.Equals(""))
            HttpContext.Current.Response.Write("<script>alert('Please Choose A Comment')</script>");
        else
        {
            DB_CONNETION.Open();

            SQL_QUERY = "Edit_comment";

            DB_COMMAND = new SqlCommand(SQL_QUERY, DB_CONNETION);

            DB_COMMAND.CommandType = System.Data.CommandType.StoredProcedure;

            string[] splitter = comments.SelectedItem.Value.Split('_');

            for (int i = 0; i < splitter.Length; ++i)
                Debug.WriteLine(splitter[i]);

            DB_COMMAND.Parameters.Add(new SqlParameter("@comment_text", commentText.Text));
            DB_COMMAND.Parameters.Add(new SqlParameter("@viewer_id", Session["ID"]));
            DB_COMMAND.Parameters.Add(new SqlParameter("@original_content_id", splitter[3]));
            DB_COMMAND.Parameters.Add(new SqlParameter("@last_written_time", splitter[2]));
            DB_COMMAND.Parameters.Add(new SqlParameter("@updated_written_time", DateTime.Now));
            
            try
            {
                DB_COMMAND.ExecuteNonQuery();

                HttpContext.Current.Response.Write("<script>alert('" + "Comment Edited Successfully" + "')</script>");
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
}