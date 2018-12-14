using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Diagnostics;
public partial class Event : System.Web.UI.Page
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
    }

    /*
     * Let the user create an event
     * Add choice to create and advertisment
     */

    protected void submitEvent_Click(object sender, EventArgs e)
    {
        DB_CONNETION.Open();

        SQL_QUERY = "Viewer_create_event";

        DB_COMMAND = new SqlCommand(SQL_QUERY, DB_CONNETION);

        DB_COMMAND.CommandType = System.Data.CommandType.StoredProcedure;

        DB_COMMAND.Parameters.Add(new SqlParameter("@city", Convert.ToString(city.Text)));
        DB_COMMAND.Parameters.Add(new SqlParameter("@event_date_time", Convert.ToString(dateTime.Text)));
        DB_COMMAND.Parameters.Add(new SqlParameter("@description", Convert.ToString(description.Text)));
        DB_COMMAND.Parameters.Add(new SqlParameter("@entertainer", Convert.ToString(entertainer.Text)));
        DB_COMMAND.Parameters.Add(new SqlParameter("@viewer_id", USER_ID));
        DB_COMMAND.Parameters.Add(new SqlParameter("@location", Convert.ToString(location.Text)));

        DB_COMMAND.Parameters.Add("@event_id", System.Data.SqlDbType.Int).Direction = System.Data.ParameterDirection.Output;

        try
        {
            int EVENT_ID;

            using (QUERY_READER = DB_COMMAND.ExecuteReader())
            {
                EVENT_ID = Convert.ToInt32(DB_COMMAND.Parameters["@event_id"].Value);
            }

            if (createAdvert.Checked)
            {
                SQL_QUERY = "Viewer_create_ad_from_event";

                DB_COMMAND = new SqlCommand(SQL_QUERY, DB_CONNETION);

                DB_COMMAND.CommandType = System.Data.CommandType.StoredProcedure;

                DB_COMMAND.Parameters.Add(new SqlParameter("@event_id", EVENT_ID));

                using (QUERY_READER = DB_COMMAND.ExecuteReader()) { }
            }

            int numberOfPhoto = numberOfPhotos.Text.Equals("") ? 0 : Convert.ToInt32(numberOfPhotos.Text);

            SQL_QUERY = "Viewer_upload_event_photo";

            for (int i = 0; i <= numberOfPhoto; ++i)
            {
                DB_COMMAND = new SqlCommand(SQL_QUERY, DB_CONNETION);

                DB_COMMAND.CommandType = System.Data.CommandType.StoredProcedure;

                DB_COMMAND.Parameters.Add(new SqlParameter("@event_id", EVENT_ID));
                DB_COMMAND.Parameters.Add(new SqlParameter("@link", Request.Form["photo_" + i]));

                using (QUERY_READER = DB_COMMAND.ExecuteReader()) { }
            }

            int numberOfVideo = numberOfVideos.Text.Equals("") ? 0 : Convert.ToInt32(numberOfVideos.Text);

            SQL_QUERY = "Viewer_upload_event_video";

            for (int i = 0; i <= numberOfVideo; ++i)
            {
                DB_COMMAND = new SqlCommand(SQL_QUERY, DB_CONNETION);

                DB_COMMAND.CommandType = System.Data.CommandType.StoredProcedure;

                DB_COMMAND.Parameters.Add(new SqlParameter("@event_id", EVENT_ID));
                DB_COMMAND.Parameters.Add(new SqlParameter("@link", Request.Form["photo_" + i]));

                using (QUERY_READER = DB_COMMAND.ExecuteReader()) { }
            }

            HttpContext.Current.Response.Write("<script>alert('Event Created Successfully')</script>");
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

    protected void addBoxes_Click(object sender, EventArgs e)
    {
        int numberOfPhotos = Request.Form["numberOfPhotos"] != "" ? int.Parse(Request.Form["numberOfPhotos"]) : 0;

        for (int i = 1; i <= numberOfPhotos; ++i)
        {
            Label newLabel = new Label();
            newLabel.Text = "Photo link: ";
            newLabel.Style.Add("display", "block");

            TextBox newBox = new TextBox();
            newBox.ID = "photo_" + i;
            newBox.Attributes.Add("placeholder", "Enter Photo URL");
            newBox.Style.Add("display", "inline");

            photoSection.Controls.Add(newLabel);
            photoSection.Controls.Add(newBox);
        }

        int numberOfVideos = Request.Form["numberOfVideos"] != "" ? int.Parse(Request.Form["numberOfVideos"]) : 0;

        for (int i = 1; i <= numberOfVideos; ++i)
        {
            Label newLabel = new Label();
            newLabel.Text = "Video link: ";
            newLabel.Style.Add("display", "block");

            TextBox newBox = new TextBox();
            newBox.ID = "video_" + i;
            newBox.Attributes.Add("placeholder", "Enter Video URL");
            newBox.Style.Add("display", "inline");

            videoSection.Controls.Add(newLabel);
            videoSection.Controls.Add(newBox);
        }
    }
}