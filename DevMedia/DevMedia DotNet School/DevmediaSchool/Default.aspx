<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="DevmediaSchool.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <p>Default.aspx</p>
        <br />
        <div style="width:400px; margin:0 auto; padding:0 auto; border: 1px solid #CCCCCC">
            <p style="font-weight:bold; text-align:center">Menu</p>
            <p style="padding-left:10px"><asp:HyperLink ID="hlTeachers" runat="server" NavigateUrl="~/Teachers.aspx">- Teachers</asp:HyperLink></p>
            <p style="padding-left:10px"><asp:HyperLink ID="hlCourses" runat="server" NavigateUrl="~/Courses.aspx">- Courses</asp:HyperLink></p>
            <p style="padding-left:10px"><asp:HyperLink ID="hlStudents" runat="server" NavigateUrl="~/Students.aspx">- Students</asp:HyperLink></p>
            <p style="padding-left:10px"><asp:HyperLink ID="hlClassrooms" runat="server" NavigateUrl="~/Classrooms.aspx">- Classrooms</asp:HyperLink></p>
            <p style="padding-left:10px"><asp:HyperLink ID="hlGrades" runat="server" NavigateUrl="~/Grades.aspx">- Grades</asp:HyperLink></p>
        </div>
        
    
    </div>
    </form>
</body>
</html>
