<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Teachers_Insert.aspx.cs" Inherits="DevmediaSchool.Teachers_Insert" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <p>Teachers_Insert.aspx</p>
        <br />
        <div style="width:750px; margin:0 auto; padding:0 auto; border:1px solid #CCCCCC; text-align:center;">
            <p style="font-weight:bold; text-align:center">Teachers - Insert</p>
            <p style="text-align:left; padding-left:20px">First Name: <asp:TextBox ID="txtFirstName" runat="server" Width="600"></asp:TextBox></p>
            <p style="text-align:left; padding-left:20px">Last Name: <asp:TextBox ID="txtLastName" runat="server" Width="600"></asp:TextBox></p>
            <p style="text-align:left; padding-left:20px">Department: <asp:TextBox ID="txtDepartment" runat="server" Width="594"></asp:TextBox></p>
            <p style="text-align:left; padding-left:20px">E-Mail: <asp:TextBox ID="txtEmail" runat="server" Width="620"></asp:TextBox></p>
            <p style="text-align:left; padding-left:20px">Password: <asp:TextBox ID="txtPassword" runat="server" Width="200"></asp:TextBox></p>
            <p style="text-align:left; padding-left:20px"><asp:Button ID="btnInsert" runat="server" Text="Insert" OnClick="btnInsert_Click" />&nbsp;&nbsp;
                <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" />
            </p>
            <p style="text-align:left; padding-left:20px"><asp:Label ID="lblMsg" runat="server" Text=""></asp:Label></p>
            
        </div>

    </div>
    </form>
</body>
</html>
