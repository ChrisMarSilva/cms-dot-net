<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Students.aspx.cs" Inherits="DevmediaSchool.Students" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <p>Students.aspx</p>
        <br />
        <div style="width:750px; margin:0 auto; padding:0 auto; border:1px solid #CCCCCC; text-align:center">
            <p style="font-weight:bold; text-align:center">Students</p>

            <asp:SqlDataSource ID="StudentSqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:DevmediaSchoolConnectionString %>" ProviderName="System.Data.SqlClient" SelectCommand="GetStudents" DeleteCommand="DeleteStudent" DeleteCommandType="StoredProcedure" SelectCommandType="StoredProcedure" UpdateCommand="UpdateStudent" UpdateCommandType="StoredProcedure" InsertCommand="InsertStudent" InsertCommandType="StoredProcedure">
                <DeleteParameters>
                    <asp:Parameter Name="StudentID" Type="Int32" />
                    <asp:Parameter Name="LoginID" Type="Int32" />
                </DeleteParameters>
                <UpdateParameters>
                    <asp:Parameter Name="StudentID" Type="Int32" />
                    <asp:Parameter Name="StudentLastName" Type="String" />
                    <asp:Parameter Name="StudentFirstName" Type="String" />
                    <asp:Parameter Name="StudentBirthday" Type="DateTime" />
                    <asp:Parameter Name="StudentAddress" Type="String" />
                    <asp:Parameter Name="StudentCity" Type="String" />
                    <asp:Parameter Name="StudentUF" Type="String" />
                    <asp:Parameter Name="StudentPhone" Type="String" />
                    <asp:Parameter Name="StudentDateJoin" Type="DateTime" />
                    <asp:Parameter Name="UserEmail" Type="String" />
                    <asp:Parameter Name="UserPassword" Type="String" />
                </UpdateParameters>
                <InsertParameters>
                    <asp:Parameter Name="StudentLastName" Type="String" />
                    <asp:Parameter Name="StudentFirstName" Type="String" />
                    <asp:Parameter Name="StudentBirthday" Type="DateTime" />
                    <asp:Parameter Name="StudentAddress" Type="String" />
                    <asp:Parameter Name="StudentCity" Type="String" />
                    <asp:Parameter Name="StudentUF" Type="String" />
                    <asp:Parameter Name="StudentPhone" Type="String" />
                    <asp:Parameter Name="UserEmail" Type="String" />
                    <asp:Parameter Name="UserPassword" Type="String" />
                </InsertParameters>
            </asp:SqlDataSource>

            <asp:DetailsView ID="dvStudents" runat="server" AllowPaging="True" AutoGenerateRows="False" HorizontalAlign="Center" DataSourceID="StudentSqlDataSource" DataKeyNames="StudentID" EnableViewState="False">
                <Fields>
                    <asp:BoundField DataField="StudentID" HeaderText="ID" InsertVisible="false" ReadOnly="true" SortExpression="StudentID" />
                    <asp:BoundField DataField="StudentLastName" HeaderText="Last Name" SortExpression="StudentLastName" />
                    <asp:BoundField DataField="StudentFirstName" HeaderText="First Name" SortExpression="StudentFirstName" />
                    <asp:BoundField DataField="StudentBirthday" HeaderText="Birthday" SortExpression="StudentBirthday" />
                    <asp:BoundField DataField="StudentAddress" HeaderText="Address" SortExpression="StudentAddress" />
                    <asp:BoundField DataField="StudentCity" HeaderText="City" SortExpression="StudentCity" />
                    <asp:BoundField DataField="StudentUF" HeaderText="UF" SortExpression="StudentUF" />
                    <asp:BoundField DataField="StudentPhone" HeaderText="Phone" SortExpression="StudentPhone" />
                    
                    <asp:BoundField DataField="StudentDateJoin" HeaderText="Date Join" InsertVisible="false" SortExpression="StudentDateJoin" />
                    
                    <asp:TemplateField HeaderText="Email">
                        <ItemTemplate><%# Eval("UserEmail") %></ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEmail" runat="server" Text='<%# Bind("UserEmail") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <InsertItemTemplate>
                            <asp:TextBox ID="txtEmail" runat="server" Text='<%# Bind("UserEmail") %>'></asp:TextBox>
                        </InsertItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Password">
                        <ItemTemplate><%# Eval("UserPassword") %></ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtPassword" runat="server" Text='<%# Bind("UserPassword") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <InsertItemTemplate>
                            <asp:TextBox ID="txtPassword" runat="server" Text='<%# Bind("UserPassword") %>'></asp:TextBox>
                        </InsertItemTemplate>
                    </asp:TemplateField>

                    <asp:CommandField ShowDeleteButton="true" ShowEditButton="true" ShowInsertButton="true" />
                </Fields>
            </asp:DetailsView>
            <br />
            <p style="text-align:center"><asp:Label ID="lblMsg" runat="server" Text=""></asp:Label></p>
        </div>
    </div>
    </form>
</body>
</html>
