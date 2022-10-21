<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Grades.aspx.cs" Inherits="DevmediaSchool.Grades" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <p>Grades.aspx</p>
        <br />
        <div style="width:750px; margin:0 auto;padding:0 auto; border:1px solid #CCCCCC; text-align:center">
            <p style="font-weight:bold; text-align:center">Grades</p>

            <asp:SqlDataSource ID="GradesSqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:DevmediaSchoolConnectionString %>" ProviderName="System.Data.SqlClient" SelectCommand="GetGradesClassrooms">
            </asp:SqlDataSource>

            <asp:GridView ID="gvGrades" runat="server" Width="700" AutoGenerateColumns="false" DataKeyNames="ClassroomID" HorizontalAlign="Center" DataSourceID="GradesSqlDataSource">
                <Columns>
                    <asp:TemplateField HeaderText="Course Name">
                        <ItemTemplate><%# Eval("CourseName") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Teacher Name">
                        <ItemTemplate><%# Eval("TeacherName") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Semester">
                        <ItemTemplate><%# Eval("ClassroomSemester") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:HyperLinkField DataNavigateUrlFields="ClassroomID" DataNavigateUrlFormatString="Grades_Students.aspx?ClassroomID={0}" Text="View Students" />
                </Columns>
            </asp:GridView>

        </div>
    </div>
    </form>
</body>
</html>
