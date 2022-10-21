using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Security.Cryptography;
using System.Web.Security;

namespace DevmediaSchool
{
    public partial class Teachers_Insert : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnInsert_Click(object sender, EventArgs e)
        {
            try
            {
                string strTeacherLastName = txtLastName.Text;
                string strTeacherFirstName = txtFirstName.Text;
                string strTeacherDepartment = txtDepartment.Text;
                string strUserEmail = txtEmail.Text;
                string strUserPassword = CreatePasswordHash(txtPassword.Text);

                string connStr = ConfigurationManager.ConnectionStrings["DevmediaSchoolConnectionString"].ConnectionString;

                SqlConnection con = new SqlConnection(connStr);
                con.Open();

                SqlCommand cmd = new SqlCommand("InsertTeacher", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@TeacherLastName", strTeacherLastName);
                cmd.Parameters.AddWithValue("@TeacherFirstName", strTeacherFirstName);
                cmd.Parameters.AddWithValue("@TeacherDepartment", strTeacherDepartment);
                cmd.Parameters.AddWithValue("@UserEmail", strUserEmail);
                cmd.Parameters.AddWithValue("@UserPassword", strUserPassword);

                cmd.ExecuteNonQuery();

                lblMsg.Text = "Teacher Inserted!";
                btnInsert.Enabled = false;

            }
            catch(Exception ex)
            {
                lblMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Teachers.aspx");
        }

        private static string CreatePasswordHash(string pwd)
        {
            string hashedpwd = FormsAuthentication.HashPasswordForStoringInConfigFile(pwd, "SHA1");
            return hashedpwd;
        }
    }
}