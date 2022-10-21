<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="DevmediaSchool.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="margin:0 auto; padding:0 auto;">
        <p>Login.aspx</p>
        <br />
        <div style="width:400px; margin:0 auto; padding:0 auto; border:1px solid #CCCCCC">
            <p style="font-weight:bold; text-align:center">Login</p>
            <p style="padding-left:10px">E-mail: <asp:TextBox ID="txtEmail" runat="server" Width="320"></asp:TextBox></p>
            <p style="padding-left:10px">Password: <asp:TextBox ID="txtPassword" runat="server" Width="300"></asp:TextBox></p>
            <p style="padding-left:10px"><asp:Button ID="btnLogin" runat="server" Text="Login" Width="100" OnClick="btnLogin_Click" /></p>
            <p style="padding-left:10px"><asp:Label ID="lblMsg" runat="server" Text=""></asp:Label></p>
        </div>
    </div>
    </form>
</body>
</html>
