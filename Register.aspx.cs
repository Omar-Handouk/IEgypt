using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Register : System.Web.UI.Page
{
    private SqlConnection DB_CONNETION;
    private SqlCommand DB_COMMAND;
    private SqlDataReader QUERY_READER;

    private string DB_CONNECTION_STRING = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
    private string SQL_QUERY;
    protected void Page_Load(object sender, EventArgs e)
    {
        DB_CONNETION = new SqlConnection(DB_CONNECTION_STRING);
    }

    protected void register_Click(object sender, EventArgs e)
    {
        DB_CONNETION.Open();

        DB_COMMAND = new SqlCommand("Register_User", DB_CONNETION);

        DB_COMMAND.CommandType = System.Data.CommandType.StoredProcedure;

        DB_COMMAND.Parameters.Add(new SqlParameter("@usertype", userTypeList.SelectedItem.Value));
        DB_COMMAND.Parameters.Add(new SqlParameter("@email", email.Text));
        DB_COMMAND.Parameters.Add(new SqlParameter("@password", password.Text));
        DB_COMMAND.Parameters.Add(new SqlParameter("@firstname", firstName.Text));
        DB_COMMAND.Parameters.Add(new SqlParameter("@middlename", middleName.Text));
        DB_COMMAND.Parameters.Add(new SqlParameter("@lastname", lastName.Text));
        DB_COMMAND.Parameters.Add(new SqlParameter("@birth_date", birthDate.Text));

        //Staff (Mandetory if staff) (DropDownList --> Authorized Reviewer, content Manager
        if (hireDate.Text.Equals(""))
            DB_COMMAND.Parameters.Add(new SqlParameter("@hire_date", DBNull.Value));
        else
            DB_COMMAND.Parameters.Add(new SqlParameter("@hire_date", hireDate.Text));

        if (workingHours.Text.Equals(""))
            DB_COMMAND.Parameters.Add(new SqlParameter("@working_hours", DBNull.Value));
        else
            DB_COMMAND.Parameters.Add(new SqlParameter("@working_hours", Convert.ToInt32(workingHours.Text)));

        if (paymentRate.Text.Equals(""))
            DB_COMMAND.Parameters.Add(new SqlParameter("@payment_rate", DBNull.Value));
        else
            DB_COMMAND.Parameters.Add(new SqlParameter("@payment_rate", Convert.ToDecimal(paymentRate.Text)));

        // Viewer (Either all are nulls or none are)
        if (workplaceName.Text.Equals(""))
            DB_COMMAND.Parameters.Add(new SqlParameter("@working_place_name", DBNull.Value));
        else
            DB_COMMAND.Parameters.Add(new SqlParameter("@working_place_name", workplaceName.Text));

        if (workingPlaceType.Text.Equals(""))
            DB_COMMAND.Parameters.Add(new SqlParameter("@working_place_type", DBNull.Value));
        else
            DB_COMMAND.Parameters.Add(new SqlParameter("@working_place_type", workingPlaceType.Text));

        if (workingPlaceDescription.Text.Equals(""))
            DB_COMMAND.Parameters.Add(new SqlParameter("@wokring_place_description", DBNull.Value));
        else
            DB_COMMAND.Parameters.Add(new SqlParameter("@wokring_place_description", workingPlaceDescription.Text));
        //-----

        //Contributor
        if (specialization.Text.Equals(""))
            DB_COMMAND.Parameters.Add(new SqlParameter("@specilization", DBNull.Value));
        else
            DB_COMMAND.Parameters.Add(new SqlParameter("@specilization", specialization.Text));

        if (portofolioLink.Text.Equals(""))
            DB_COMMAND.Parameters.Add(new SqlParameter("@portofolio_link", DBNull.Value));
        else
            DB_COMMAND.Parameters.Add(new SqlParameter("@portofolio_link", portofolioLink.Text));

        if (yearsOfExperience.Text.Equals(""))
            DB_COMMAND.Parameters.Add(new SqlParameter("@years_experience", DBNull.Value));
        else
            DB_COMMAND.Parameters.Add(new SqlParameter("@years_experience", yearsOfExperience.Text));
        //-----

        //ID OUTPUT
        DB_COMMAND.Parameters.Add("@user_id", System.Data.SqlDbType.Int).Direction = System.Data.ParameterDirection.Output;

        int UID = 0;
        Boolean regSuccess = false;

        try
        {
            using (QUERY_READER = DB_COMMAND.ExecuteReader())
            {
                UID = Convert.ToInt32(DB_COMMAND.Parameters["@user_id"].Value);
                regSuccess = true;
                //System.Diagnostics.Debug.Write(UID);
            }
        }
        catch (SqlException ex)
        {
            System.Web.HttpContext.Current.Response.Write("<script>alert('" + ex.Message+ "')</script>");
        }
        catch (Exception ex)
        {
            System.Web.HttpContext.Current.Response.Write("<script>alert('" + ex.Message + "')</script>");
        }

        if (regSuccess)
        {
            Response.Redirect("Login.aspx?type=" + userTypeList.Text + "&ID=" + UID);
            System.Diagnostics.Debug.WriteLine(">>Redirected to login");
        }

        DB_CONNETION.Close();
    }

    protected void userTypeList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (userTypeList.SelectedItem.Text.Equals("Viewer"))
        {
            workplaceName.Enabled = true;
            workingPlaceType.Enabled = true;
            workingPlaceDescription.Enabled = true;
            specialization.Enabled = false;
            portofolioLink.Enabled = false;
            yearsOfExperience.Enabled = false;
            hireDate.Enabled = false;
            workingHours.Enabled = false;
            paymentRate.Enabled = false;
        }
        else if (userTypeList.SelectedItem.Text.Equals("Contributor"))
        {
            workplaceName.Enabled = false;
            workingPlaceType.Enabled = false;
            workingPlaceDescription.Enabled = false;
            specialization.Enabled = true;
            portofolioLink.Enabled = true;
            yearsOfExperience.Enabled = true;
            hireDate.Enabled = false;
            workingHours.Enabled = false;
            paymentRate.Enabled = false;
        }
        else 
        {
            workplaceName.Enabled = false;
            workingPlaceType.Enabled = false;
            workingPlaceDescription.Enabled = false;
            specialization.Enabled = false;
            portofolioLink.Enabled = false;
            yearsOfExperience.Enabled = false;
            hireDate.Enabled = true;
            workingHours.Enabled = true;
            paymentRate.Enabled = true;
        }
    }
}