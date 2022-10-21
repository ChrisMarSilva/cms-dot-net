<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Courses.aspx.cs" Inherits="DevmediaSchool.Courses" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <p>Courses.aspx</p>
        <br />
        <div style="width:750px; margin:0 auto; padding:0 auto; border:1px solid #CCCCCC; text-align:center">
            <p style="font-weight:bold; text-align:center">Courses</p>

            <asp:SqlDataSource ID="CoursesSqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:DevmediaSchoolConnectionString %>" ProviderName="System.Data.SqlClient" SelectCommand="SELECT * FROM [Courses]" DeleteCommand="DELETE FROM [Courses] WHERE [CourseID] = @CourseID" InsertCommand="INSERT INTO [Courses] ([CourseName], [CourseDescription]) VALUES (@CourseName, @CourseDescription)" UpdateCommand="UPDATE [Courses] SET [CourseName] = @CourseName, [CourseDescription] = @CourseDescription WHERE [CourseID] = @CourseID">
                <DeleteParameters>
                    <asp:Parameter Name="CourseID" Type="Int32" />
                </DeleteParameters>
                <InsertParameters>
                    <asp:Parameter Name="CourseName" Type="String" />
                    <asp:Parameter Name="CourseDescription" Type="String" />
                </InsertParameters>
                <UpdateParameters>
                    <asp:Parameter Name="CourseName" Type="String" />
                    <asp:Parameter Name="CourseDescription" Type="String" />
                    <asp:Parameter Name="CourseID" Type="Int32" />
                </UpdateParameters>
            </asp:SqlDataSource>

            <asp:GridView ID="gvCourses" runat="server" DataKeyNames="CourseID" Width="500px" DataSourceID="CoursesSqlDataSource" AutoGenerateColumns="False" HorizontalAlign="Center">
                <Columns>
                    <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
                    <asp:BoundField DataField="CourseID" HeaderText="ID" />
                    <asp:BoundField DataField="CourseName" HeaderText="Name" />
                    <asp:BoundField DataField="CourseDescription" HeaderText="Description" />
                </Columns>
            </asp:GridView>
            <br />
            <p style="font-weight:bold; text-align:center">Courses - Details</p>

            <asp:DetailsView ID="dvCourses" runat="server" AllowPaging="true" AutoGenerateRows="false" HorizontalAlign="Center" DataKeyNames="CourseID" DataSourceID="CoursesSqlDataSource" EnableViesState="False">
                <Fields>
                    <asp:BoundField DataField="CourseID" HeaderText="ID" InsertVisible="false" ReadOnly="true" SortExpression="CourseID" />
                    <asp:BoundField DataField="CourseName" HeaderText="Name" SortExpression="CourseName" />
                    <asp:BoundField DataField="CourseDescription" HeaderText="Description" SortExpression="CourseDescription" />
                    <asp:CommandField ShowDeleteButton="true" ShowEditButton="true" ShowInsertButton="true" />
                </Fields>
            </asp:DetailsView>
        </div>

    </div>
    </form>
</body>
</html>
