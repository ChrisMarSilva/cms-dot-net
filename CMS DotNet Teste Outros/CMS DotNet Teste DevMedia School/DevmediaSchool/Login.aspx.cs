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
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            bool passwordVerified = false;
            try
            {
                passwordVerified = VerifyPassword(txtEmail.Text, txtPassword.Text);
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
                return;
            }
            if (passwordVerified == true)
            {
                FormsAuthentication.RedirectFromLoginPage(txtEmail.Text, true);
            }
            else
            {
                lblMsg.Text = "Invalid username or password";
            }
        }

        private bool VerifyPassword(string suppliedUserEmail, string suppliedPassword)
        {
            string connStr = ConfigurationManager.ConnectionStrings["DevmediaSchoolConnectionString"].ConnectionString;
            bool passwordMatch = false;

            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand("TryLogin", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter sqlParam = cmd.Parameters.Add("@UserEmail", SqlDbType.NVarChar, 50);
            sqlParam.Value = suppliedUserEmail;

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read(); 

                string dbPasswordHash = reader.GetString(0);
                reader.Close();

                string hashedPassword = CreatePasswordHash(suppliedPassword);

                passwordMatch = hashedPassword.Equals(dbPasswordHash);
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message.ToString();
            }
            finally
            {
                conn.Close();
            }
            return passwordMatch;
        }

        private static string CreatePasswordHash(string pwd)
        {
            string hashedPwd = FormsAuthentication.HashPasswordForStoringInConfigFile(pwd, "SHA1");
            return hashedPwd;
        }
    }
}