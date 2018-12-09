using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;

public partial class User_CP : System.Web.UI.Page
{
    private SqlConnection DB_CONNETION;
    private SqlCommand DB_COMMAND;
    private SqlDataReader QUERY_READER;

    private string DB_CONNECTION_STRING = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
    private string SQL_QUERY;
    protected void Page_Load(object sender, EventArgs e)
    {
        DB_CONNETION = new SqlConnection(DB_CONNECTION_STRING);

        if (Session["ID"] == null)
        {
            Response.Redirect("Default.aspx");
        }
        else
        {
            string userType = this.userType(Convert.ToInt32(Session["ID"]));
            this.adjustFields(userType);

            if (!IsPostBack)
                this.fillBoxes();

        }

    }

    private void fillBoxes()
    {
        DB_CONNETION.Open();

        DB_COMMAND = new SqlCommand("Show_Profile", DB_CONNETION);

        DB_COMMAND.CommandType = System.Data.CommandType.StoredProcedure;

        DB_COMMAND.Parameters.Add(new SqlParameter("@user_id", Convert.ToInt32(Session["ID"])));

        DB_COMMAND.Parameters.Add("@email", System.Data.SqlDbType.VarChar, 40).Direction = System.Data.ParameterDirection.Output;
        DB_COMMAND.Parameters.Add("@password", System.Data.SqlDbType.VarChar, 50).Direction = System.Data.ParameterDirection.Output;
        DB_COMMAND.Parameters.Add("@firstname", System.Data.SqlDbType.VarChar, 20).Direction = System.Data.ParameterDirection.Output;
        DB_COMMAND.Parameters.Add("@middlename", System.Data.SqlDbType.VarChar, 20).Direction = System.Data.ParameterDirection.Output;
        DB_COMMAND.Parameters.Add("@lastname", System.Data.SqlDbType.VarChar, 20).Direction = System.Data.ParameterDirection.Output;
        DB_COMMAND.Parameters.Add("@birth_date", System.Data.SqlDbType.Date).Direction = System.Data.ParameterDirection.Output;
        DB_COMMAND.Parameters.Add("@working_place_name", System.Data.SqlDbType.VarChar, 20).Direction = System.Data.ParameterDirection.Output;
        DB_COMMAND.Parameters.Add("@working_place_type", System.Data.SqlDbType.VarChar, 20).Direction = System.Data.ParameterDirection.Output;
        DB_COMMAND.Parameters.Add("@wokring_place_description", System.Data.SqlDbType.VarChar, 100).Direction = System.Data.ParameterDirection.Output;
        DB_COMMAND.Parameters.Add("@specilization", System.Data.SqlDbType.VarChar, 100).Direction = System.Data.ParameterDirection.Output;
        DB_COMMAND.Parameters.Add("@portofolio_link", System.Data.SqlDbType.VarChar, 40).Direction = System.Data.ParameterDirection.Output;
        DB_COMMAND.Parameters.Add("@years_experience", System.Data.SqlDbType.Int).Direction = System.Data.ParameterDirection.Output;
        DB_COMMAND.Parameters.Add("@hire_date", System.Data.SqlDbType.Date).Direction = System.Data.ParameterDirection.Output;
        DB_COMMAND.Parameters.Add("@working_hours", System.Data.SqlDbType.Int).Direction = System.Data.ParameterDirection.Output;
        DB_COMMAND.Parameters.Add("@payment_rate", System.Data.SqlDbType.Float).Direction = System.Data.ParameterDirection.Output;

       try
        {
            using (QUERY_READER = DB_COMMAND.ExecuteReader())
            {
                email.Text = Convert.ToString(DB_COMMAND.Parameters["@email"].Value);
                password.Text = Convert.ToString(DB_COMMAND.Parameters["@password"].Value);
                firstName.Text = Convert.ToString(DB_COMMAND.Parameters["@firstname"].Value);
                middleName.Text = Convert.ToString(DB_COMMAND.Parameters["@middlename"].Value);
                lastName.Text = Convert.ToString(DB_COMMAND.Parameters["@lastname"].Value);
                birthDate.Text = Convert.ToString(DB_COMMAND.Parameters["@birth_date"].Value);
                workplaceName.Text = Convert.ToString(DB_COMMAND.Parameters["@working_place_name"].Value);
                workingPlaceType.Text = Convert.ToString(DB_COMMAND.Parameters["@working_place_type"].Value);
                workingPlaceDescription.Text = Convert.ToString(DB_COMMAND.Parameters["@wokring_place_description"].Value);
                specialization.Text = Convert.ToString(DB_COMMAND.Parameters["@specilization"].Value);
                portofolioLink.Text = Convert.ToString(DB_COMMAND.Parameters["@portofolio_link"].Value);
                yearsOfExperience.Text = Convert.ToString(DB_COMMAND.Parameters["@years_experience"].Value);
                hireDate.Text = Convert.ToString(DB_COMMAND.Parameters["@hire_date"].Value);
                workingHours.Text = Convert.ToString(DB_COMMAND.Parameters["@working_hours"].Value);
                paymentRate.Text = Convert.ToString(DB_COMMAND.Parameters["@payment_rate"].Value);
            }
        }
        catch (SqlException ex)
        {
            System.Web.HttpContext.Current.Response.Write("<script>alert('" + ex.Message + "')</script>");
        }
        catch (Exception ex)
        {
            System.Web.HttpContext.Current.Response.Write("<script>alert('" + ex.Message + "')</script>");
        }

        DB_CONNETION.Close();
    }

    protected void Confirm_Click(object sender, EventArgs e)
    {
        DB_CONNETION.Open();

        DB_COMMAND = new SqlCommand("Edit_Profile", DB_CONNETION);

        DB_COMMAND.CommandType = System.Data.CommandType.StoredProcedure;

        DB_COMMAND.Parameters.Add(new SqlParameter("@user_id", Convert.ToInt32(Session["ID"])));
        DB_COMMAND.Parameters.Add(new SqlParameter("@email", email.Text));
        DB_COMMAND.Parameters.Add(new SqlParameter("@password", password.Text));
        DB_COMMAND.Parameters.Add(new SqlParameter("@firstname", firstName.Text));
        DB_COMMAND.Parameters.Add(new SqlParameter("@middlename", middleName.Text));
        DB_COMMAND.Parameters.Add(new SqlParameter("@lastname", lastName.Text));
        DB_COMMAND.Parameters.Add(new SqlParameter("@birth_date", birthDate.Text));

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
            DB_COMMAND.Parameters.Add(new SqlParameter("@working_place_description", DBNull.Value));
        else
            DB_COMMAND.Parameters.Add(new SqlParameter("@working_place_description", workingPlaceDescription.Text));
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
        
        try
        {
            using (QUERY_READER = DB_COMMAND.ExecuteReader()) ;
            System.Web.HttpContext.Current.Response.Write("<script>alert('" + "Operation Successful" + "')</script>");
        }
        catch (SqlException ex)
        {
            System.Web.HttpContext.Current.Response.Write("<script>alert('" + ex.Message + "')</script>");
        }
        catch (Exception ex)
        {
            System.Web.HttpContext.Current.Response.Write("<script>alert('" + ex.Message + "')</script>");
        }

        DB_CONNETION.Close();
    }

    private string userType(int userID)
    {
        DB_CONNETION.Open();

        DB_COMMAND = new SqlCommand("User_Type", DB_CONNETION);

        DB_COMMAND.CommandType = System.Data.CommandType.StoredProcedure;

        DB_COMMAND.Parameters.Add(new SqlParameter("@userID", userID));

        DB_COMMAND.Parameters.Add("@userType", System.Data.SqlDbType.VarChar, 25).Direction = System.Data.ParameterDirection.Output;

        string userType = "";

        try
        {
            using (QUERY_READER = DB_COMMAND.ExecuteReader())
                userType = Convert.ToString(DB_COMMAND.Parameters["@userType"].Value);
        }
        catch (SqlException ex)
        {
            Debug.WriteLine(ex.Message);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }

        if (userType.Equals(""))
            throw new Exception("User Not Found");

        DB_CONNETION.Close();

        return userType;
    }

    private void adjustFields(string userType)
    {
        if (userType.Equals("Viewer"))
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
        else if (userType.Equals("Contributor"))
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

    protected void Deactivate_Click(object sender, EventArgs e)
    {
        DB_CONNETION.Open();

        DB_COMMAND = new SqlCommand("Deactivate_Profile", DB_CONNETION);

        DB_COMMAND.CommandType = System.Data.CommandType.StoredProcedure;

        DB_COMMAND.Parameters.Add(new SqlParameter("@user_id", Convert.ToInt32(Session["ID"])));

        try
        {
            using (QUERY_READER = DB_COMMAND.ExecuteReader())
            System.Web.HttpContext.Current.Response.Write("<script>alert('" + "Operation Successful" + "')</script>");
        }
        catch (SqlException ex)
        {
            System.Web.HttpContext.Current.Response.Write("<script>alert('" + ex.Message + "')</script>");
        }
        catch (Exception ex)
        {
            System.Web.HttpContext.Current.Response.Write("<script>alert('" + ex.Message + "')</script>");
        }

        Session.Clear();
        Response.Cookies["userID"].Expires = DateTime.Now.AddDays(-1);
        Response.Cookies["userType"].Expires = DateTime.Now.AddDays(-1);
        Response.Redirect("Default.aspx");

        DB_CONNETION.Close();
    }
}