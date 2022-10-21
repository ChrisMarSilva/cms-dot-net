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
    public partial class Teachers : System.Web.UI.Page
    {
        string Sort_Direction = "TeacherID ASC";
        DataView dvTeachers = new DataView();

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                ViewState["SortExpr"] = Sort_Direction;
                FillGrid();
            }
        }

        public void FillGrid()
        {
            string connStr = ConfigurationManager.ConnectionStrings["DevmediaSchoolConnectionString"].ConnectionString;

            SqlConnection conn = new SqlConnection(connStr);

            conn.Open();

            SqlCommand cmd = new SqlCommand("GetTeachers", conn);

            SqlDataReader drTeachers = cmd.ExecuteReader();

            DataTable dtTeachers = new DataTable();

            dtTeachers.Load(drTeachers);

            dvTeachers = dtTeachers.DefaultView;
            dvTeachers.Sort = ViewState["SortExpr"].ToString();

            //if (ViewState["FilterLastName"] != null)
            //{
            //    dvTeachers.RowFilter = "TeacherLastName LIKE '*" + ViewState["FilterLastName"] + "*'";
            //}

            if (ViewState["FilterLastName"] != null || ViewState["FilterDepartment"] != null)
            {
                string strFilter = "";

                if(ViewState["FilterLastName"] != null)
                {
                    strFilter = "TeacherLastName LIKE '*" + ViewState["FilterLastName"].ToString() + "*'";
                
                    if(ViewState["FilterDepartment"] != null)
                    {
                        strFilter = strFilter + " AND TeacherDepartment LIKE '*" + ViewState["FilterDepartment"].ToString() + "*'";
                    }
                }
                else if(ViewState["FilterDepartment"] != null)
                {
                    strFilter = strFilter + "TeacherDepartment LIKE '*" + ViewState["FilterDepartment"].ToString() + "*'";
                }

                dvTeachers.RowFilter = strFilter;
            }

            gvTeachers.DataSource = dvTeachers;
            gvTeachers.DataBind();
        }

        protected void gvTeachers_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvTeachers.EditIndex = -1;
            FillGrid();
        }

        protected void gvTeachers_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvTeachers.EditIndex = e.NewEditIndex;
            FillGrid();
        }

        protected void gvTeachers_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int intID = Convert.ToInt32(gvTeachers.DataKeys[e.RowIndex].Value.ToString());

            string strLastName = ((TextBox)gvTeachers.Rows[e.RowIndex].FindControl("txtLastName")).Text;
            string strFirstName = ((TextBox)gvTeachers.Rows[e.RowIndex].FindControl("txtFirstName")).Text;
            string strDateJoin = ((TextBox)gvTeachers.Rows[e.RowIndex].FindControl("txtDateJoin")).Text;
            string strDepartment = ((TextBox)gvTeachers.Rows[e.RowIndex].FindControl("txtDepartment")).Text;

            string connStr = ConfigurationManager.ConnectionStrings["DevmediaSchoolConnectionString"].ConnectionString;

            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();

            SqlCommand cmd = new SqlCommand("UpdateTeacher", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@TeacherID", intID);
            cmd.Parameters.AddWithValue("@TeacherLastName", strLastName);
            cmd.Parameters.AddWithValue("@TeacherFirstName", strFirstName);
            cmd.Parameters.AddWithValue("@TeacherDateJoin", Convert.ToDateTime(strDateJoin));
            cmd.Parameters.AddWithValue("@TeacherDepartment", strDepartment);

            cmd.ExecuteNonQuery();

            gvTeachers.EditIndex = -1;

            FillGrid();
            
        }

        protected void gvTeachers_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int intID = Convert.ToInt32(gvTeachers.DataKeys[e.RowIndex].Value.ToString());

            string connStr = ConfigurationManager.ConnectionStrings["DevmediaSchoolConnectionString"].ConnectionString;

            SqlConnection con = new SqlConnection(connStr);
            con.Open();

            SqlCommand cmd = new SqlCommand("DeleteTeacher", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@TeacherID", intID);
            cmd.Parameters.AddWithValue("@LoginID", -1);

            cmd.ExecuteNonQuery();

            FillGrid();
        }

        protected void btnInsert_Click(object sender, EventArgs e)
        {
            Response.Redirect("Teachers_Insert.aspx");
        }

        protected void gvTeachers_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvTeachers.PageIndex = e.NewPageIndex;
            FillGrid();
        }

        protected void gvTeachers_Sorting(object sender, GridViewSortEventArgs e)
        {
            string[] SortOrder = ViewState["SortExpr"].ToString().Split(' ');
            if(SortOrder[0] == e.SortExpression)
            {
                if(SortOrder[1] == "ASC")
                {
                    ViewState["SortExpr"] = e.SortExpression + " " + "DESC";
                }
                else
                {
                    ViewState["SortExpr"] = e.SortExpression + " " + "ASC";
                }
            }
            else
            {
                ViewState["SortExpr"] = e.SortExpression + " " + "ASC";
            }

            FillGrid();
        }

        protected void btnFilterLastName_Click(object sender, EventArgs e)
        {
            ViewState["FilterLastName"] = txtFilterLastName.Text.ToString();
            FillGrid();
        }

        protected void btnResetLastName_Click(object sender, EventArgs e)
        {
            ViewState["FilterLastName"] = null;
            FillGrid();
        }

        protected void ddlDepartment_IndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlDepartmentTemp = (DropDownList)sender;
            if(ddlDepartmentTemp.SelectedValue != "All")
            {
                ViewState["FilterDepartment"] = ddlDepartmentTemp.SelectedValue.ToString();
                FillGrid();
            }
            else
            {
                ViewState["FilterDepartment"] = null;
                FillGrid();
            }
        }

        protected void ddlDeparment_SetValue(object sender, EventArgs e)
        {
            if(ViewState["FilterDepartment"] != null)
            {
                DropDownList ddlDepartmentTemp = (DropDownList)gvTeachers.HeaderRow.FindControl("ddlDepartment");
                ddlDepartmentTemp.SelectedValue = ViewState["FilterDepartment"].ToString();
            }
        }

        protected void gvTeachers_Command(object sender, GridViewCommandEventArgs e)
        {
            if(e.CommandName.Equals("Sort"))
            {
                string[] SortOrder = ViewState["SortExpr"].ToString().Split(' ');
                if(SortOrder[0] == e.CommandArgument.ToString())
                {
                    if(SortOrder[1] == "ASC")
                    {
                        ViewState["SortExpr"] = e.CommandArgument.ToString() + " " + "DESC";
                    }
                    else
                    {
                        ViewState["SortExpr"] = e.CommandArgument.ToString() + " " + "ASC";
                    }
                }
                else
                {
                    ViewState["SortExpr"] = e.CommandArgument.ToString() + " " + "ASC";
                }

                FillGrid();
            }
        }

        protected void btnExportToXML_Click(object sender, EventArgs e)
        {
            FillGrid();

            try
            {
                DataTable dtXML = new DataTable();
                dtXML = dvTeachers.ToTable("Teachers");
                dtXML.WriteXml(@"c:\Users\Euclides\Desktop\Teachers.xml");
                lblMsg.Text = "Export to XML OK!";
            }
            catch(Exception ex)
            {
                lblMsg.Text = "Error to Export to XML: " + ex.Message.ToString();
            }
        }

        protected void btnExportToExcel_Click(object sender, EventArgs e)
        {
            FillGrid();

            try 
            { 
                DataTable dtExcel = new DataTable();
                dtExcel = dvTeachers.ToTable("Teachers");

                string attachment = "attachment; filename=teachers.xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vnd.ms-excel";
                string tab = "";
                foreach (DataColumn dc in dtExcel.Columns)
                {
                    Response.Write(tab + dc.ColumnName);
                    tab = "\t";
                }
                Response.Write("\n");
                int i;
                foreach(DataRow drExcel in dtExcel.Rows)
                {
                    tab = "";
                    for(i=0; i<dtExcel.Columns.Count; i++)
                    {
                        Response.Write(tab + drExcel[i].ToString());
                        tab = "\t";
                    }
                    Response.Write("\n");
                }
                Response.End();

            }
            catch(Exception ex)
            {
                lblMsg.Text = "Error to Export to Excel: " + ex.Message.ToString();
            }
        }
        

    }
}