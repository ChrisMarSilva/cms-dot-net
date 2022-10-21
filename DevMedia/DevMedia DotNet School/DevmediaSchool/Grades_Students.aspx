<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Grades_Students.aspx.cs" Inherits="DevmediaSchool.Grades_Students" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <p>Grades_Students.aspx</p>
        <div style="width:750px; margin:0 auto; padding:0 auto; border:1px solid #CCCCCC; text-align:center">
            <p style="font-weight:bold; text-align:center">Grades Students</p>

            <asp:SqlDataSource ID="GradesStudentsSqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:DevmediaSchoolConnectionString %>" ProviderName="System.Data.SqlClient" SelectCommand="GetGradesStudents" SelectCommandType="StoredProcedure" UpdateCommand="EditGrade" UpdateCommandType="StoredProcedure">
                <SelectParameters>
                    <asp:QueryStringParameter Name="ClassroomID" DbType="Int32" Direction="Input" QueryStringField="ClassroomID" />
                </SelectParameters>
                <UpdateParameters>
                    <asp:Parameter Name="GradeID" DbType="Int32" />
                    <asp:Parameter Name="StudentID" DbType="Int32" />
                    <asp:Parameter Name="CourseID" DbType="Int32" />
                    <asp:Parameter Name="Grade" DbType="Decimal" />
                </UpdateParameters>
            </asp:SqlDataSource>

            <asp:GridView runat="server" ID="gvGradesStudents" Width="700" AutoGenerateColumns="false" AllowPaging="true" DataKeyNames="GradeID" HorizontalAlign="Center" DataSourceID="GradesStudentsSqlDataSource">
                <Columns>
                    <asp:TemplateField HeaderText="StudentID">
                        <ItemTemplate><%#Eval("StudentID") %></ItemTemplate>
                        <EditItemTemplate><asp:Label ID="txtStudentID" runat="server" Text='<%# Bind("StudentID") %>'></asp:Label></EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Last Name">
                        <ItemTemplate><%#Eval("StudentLastName") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="First Name">
                        <ItemTemplate><%#Eval("StudentFirstName") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="CourseID">
                        <ItemTemplate><%#Eval("CourseID") %></ItemTemplate>
                        <EditItemTemplate><asp:Label ID="txtCourseID" runat="server" Text='<%# Bind("CourseID") %>'></asp:Label></EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Course">
                        <ItemTemplate><%#Eval("CourseName") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="GradeID">
                        <ItemTemplate><%#Eval("GradeID") %></ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Grade">
                        <ItemTemplate><%#Eval("Grade") %></ItemTemplate>
                        <EditItemTemplate><asp:TextBox ID="txtGrade" runat="server" Text='<%# Bind("Grade") %>'></asp:TextBox></EditItemTemplate>
                    </asp:TemplateField>
                    <asp:CommandField ShowEditButton="true" />
                </Columns>
            </asp:GridView>
        </div>
    
    </div>
    </form>
</body>
</html>
