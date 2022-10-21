<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Classrooms.aspx.cs" Inherits="DevmediaSchool.Classrooms" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <p>Classrooms.aspx</p>
        <br />
        <div style="width:750px; margin:0 auto; padding:0 auto; border:1px solid #CCCCCC; text-align:center">
            <p style="font-weight:bold; text-align:center">Classrooms</p>

            <asp:SqlDataSource ID="ClassroomsSqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:DevmediaSchoolConnectionString %>" ProviderName="System.Data.SqlClient" SelectCommand="GetClassrooms" UpdateCommand="UpdateClassroom" UpdateCommandType="StoredProcedure" DeleteCommand="DeleteClassroom" DeleteCommandType="StoredProcedure" InsertCommand="InsertClassroom" InsertCommandType="StoredProcedure">
                <UpdateParameters>
                    <asp:Parameter Name="ClassroomID" Type="Int32" />
                    <asp:Parameter Name="ClassroomSemester" Type="String" />
                    <asp:Parameter Name="CourseID" Type="Int32" />
                    <asp:Parameter Name="TeacherID" Type="Int32" />
                </UpdateParameters>
                <DeleteParameters>
                    <asp:Parameter Name="ClassroomID" Type="Int32" />
                </DeleteParameters>
                <InsertParameters>
                    <asp:ControlParameter Name="ClassroomSemester" ControlID="txtSemesterInsert" Type="String" />
                    <asp:ControlParameter Name="CourseID" ControlID="ddlCourseInsert" Type="Int32" />
                    <asp:ControlParameter Name="TeacherID" ControlID="ddlTeacherInsert" Type="Int32" />
                </InsertParameters>
            </asp:SqlDataSource>

            <asp:SqlDataSource ID="CoursesSqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:DevmediaSchoolConnectionString %>" ProviderName="System.Data.SqlClient" SelectCommand="SELECT CourseID, CourseName FROM Courses"></asp:SqlDataSource>

            <asp:SqlDataSource ID="TeacherSqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:DevmediaSchoolConnectionString %>" ProviderName="System.Data.SqlClient" SelectCommand="SELECT TeacherID, TeacherFirstName + ' ' + TeacherLastName AS TeacherName FROM Teachers"></asp:SqlDataSource>

            <asp:GridView ID="gvClassrooms" runat="server" Width="700" AutoGenerateColumns="false" DataKeyNames="ClassroomID" HorizontalAlign="Center" DataSourceID="ClassroomsSqlDataSource">
                <Columns>
                    <asp:HyperLinkField DataNavigateUrlFields="ClassroomID" DataNavigateUrlFormatString="Classrooms_Students.aspx?ClassroomID={0}" Text="View Students" />
                    <asp:TemplateField HeaderText="Semester">
                        <ItemTemplate><%# Eval("ClassroomSemester") %></ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtSemester" runat="server" Text='<%#Bind("ClassroomSemester") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Course Name">
                        <ItemTemplate><%# Eval("CourseName") %></ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlCourse" runat="server" DataSourceID="CoursesSqlDataSource" DataTextField="CourseName" DataValueField="CourseID" SelectedValue='<%# Bind("CourseID") %>'></asp:DropDownList>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Teacher Name">
                        <ItemTemplate><%# Eval("TeacherName") %></ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlTeacher" runat="server" DataSourceID="TeacherSqlDataSource" DataTextField="TeacherName" DataValueField="TeacherID" SelectedValue='<%# Bind("TeacherID") %>'></asp:DropDownList>
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:CommandField ShowEditButton="true" ShowDeleteButton="true" />
                </Columns>
            </asp:GridView>

            <br />

            <table border="0" cellpadding="0" cellspacing="0" style="width:500px; margin:0 auto; padding:0 auto">
                <tr>
                    <td style="width:90px">&nbsp;</td>
                    <td style="width:80px">
                        <asp:TextBox ID="txtSemesterInsert" runat="server" Width="60"></asp:TextBox>
                    </td>
                    <td style="width:110px">
                        <asp:DropDownList ID="ddlCourseInsert" runat="server" DataSourceID="CoursesSqlDataSource" DataTextField="CourseName" DataValueField="CourseID" Width="100"></asp:DropDownList>
                    </td>
                    <td style="width:110px">
                        <asp:DropDownList ID="ddlTeacherInsert" runat="server" DataSourceID="TeacherSqlDataSource" DataTextField="TeacherName" DataValueField="TeacherID" Width="100"></asp:DropDownList>
                    </td>
                    <td style="width:90px">
                        <asp:Button ID="btnAddInsert" runat="server" Text="Add" Width="50" OnClick="btnAddInsert_Click" />
                    </td>
                </tr>
            </table>
            <br />
        </div>
    </div>
    </form>
</body>
</html>
