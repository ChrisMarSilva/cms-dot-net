<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Classrooms_Students.aspx.cs" Inherits="DevmediaSchool.Classrooms_Students" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <p>Classrooms_Students.aspx</p>
        <div style="width:750px; margin:0 auto; padding:0 auto; border:1px solid #CCCCCC; text-align:center">
            <p style="font-weight:bold; text-align:center">Classrooms Students</p>

            <asp:SqlDataSource ID="ClassroomsStudentsSqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:DevmediaSchoolConnectionString %>" ProviderName="System.Data.SqlClient" SelectCommand="GetClassroomStudents" SelectCommandType="StoredProcedure">
                <SelectParameters>
                    <asp:QueryStringParameter Name="ClassroomID" DbType="Int32" Direction="Input" QueryStringField="ClassroomID" />
                </SelectParameters>
            </asp:SqlDataSource>

            <asp:GridView ID="gvClassRoomsStudents" runat="server" Width="700" AutoGenerateColumns="false" DataKeyNames="StudentID" HorizontalAlign="Center" DataSourceID="ClassroomsStudentsSqlDataSource">
                <Columns>
                    <asp:TemplateField HeaderText="ID">
                        <ItemTemplate><%#Eval("StudentID") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Last Name">
                        <ItemTemplate><%#Eval("StudentLastName") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="First Name">
                        <ItemTemplate><%#Eval("StudentFirstName") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Birthday">
                        <ItemTemplate><%#Eval("StudentBirthday") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Address">
                        <ItemTemplate><%#Eval("StudentAddress") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="City">
                        <ItemTemplate><%#Eval("StudentCity") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="UF">
                        <ItemTemplate><%#Eval("StudentUF") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Phone">
                        <ItemTemplate><%#Eval("StudentPhone") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Date Join">
                        <ItemTemplate><%#Eval("StudentDateJoin") %></ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

        </div>
    </div>
    </form>
</body>
</html>
