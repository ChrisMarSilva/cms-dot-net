<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Teachers.aspx.cs" Inherits="DevmediaSchool.Teachers" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <p>Teachers.aspx</p>
        <br />
        <div style="width:750px; margin:0 auto; padding:0 auto; border:1px solid #CCCCCC; text-align:center">
            <p style="font-weight:bold; text-align:center">Teachers</p>

            <p style="text-align:center"><asp:Button ID="btnInsert" runat="server" Text="Insert New Teacher" OnClick="btnInsert_Click" /></p>

            <p style="text-align:center">Filter by Last Name: 
                <asp:TextBox ID="txtFilterLastName" runat="server"></asp:TextBox>&nbsp;
                <asp:Button ID="btnFilterLastName" runat="server" Text="Filter" OnClick="btnFilterLastName_Click" />&nbsp;
                <asp:Button ID="btnResetLastName" runat="server" Text="Reset" OnClick="btnResetLastName_Click" />
            </p>

            <asp:GridView ID="gvTeachers" runat="server" Width="500" AllowPaging="true" PageSize="5" AllowSorting="true" AutoGenerateColumns="false" DataKeyNames="TeacherID" HorizontalAlign="Center" OnRowCancelingEdit="gvTeachers_RowCancelingEdit" OnRowEditing="gvTeachers_RowEditing" OnRowUpdating="gvTeachers_RowUpdating" OnRowDeleting="gvTeachers_RowDeleting" OnPageIndexChanging="gvTeachers_PageIndexChanging" OnSorting="gvTeachers_Sorting">
                
                <Columns>
                    <asp:TemplateField HeaderText="ID" SortExpression="TeacherID">
                        <ItemTemplate><%#Eval("TeacherID") %></ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Last Name" SortExpression="TeacherLastName">
                        <ItemTemplate><%#Eval("TeacherLastName") %></ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtLastName" runat="server" Text='<%#Eval("TeacherLastName") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>

                     <asp:TemplateField HeaderText="First Name" SortExpression="TeacherFirstName">
                        <ItemTemplate><%#Eval("TeacherFirstName") %></ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtFirstName" runat="server" Text='<%#Eval("TeacherFirstName") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Date Join" SortExpression="TeacherDateJoin">
                        <ItemTemplate><%#Eval("TeacherDateJoin") %></ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtDateJoin" runat="server" Text='<%#Eval("TeacherDateJoin") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Department" SortExpression="TeacherDepartment">
                        <ItemTemplate><%#Eval("TeacherDepartment") %></ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtDepartment" runat="server" Text='<%#Eval("TeacherDepartment") %>'></asp:TextBox>
                        </EditItemTemplate>

                        <HeaderTemplate>
                            <asp:LinkButton ID="linkDepartment" runat="server" Text="Department" CommandName="Sort" CommandArgument="TeacherDepartment"></asp:LinkButton>

                            <asp:DropDownList ID="ddlDepartment" runat="server" AutoPostBack="true" OnPreRender="ddlDeparment_SetValue" OnSelectedIndexChanged="ddlDepartment_IndexChanged">
                                <asp:ListItem Text="All" Value="All" Selected="True" />
                                <asp:ListItem Text="Biology" Value="Biology" />
                                <asp:ListItem Text="Chemical" Value="Chemical" />
                                <asp:ListItem Text="Literature" Value="Literature" />
                                <asp:ListItem Text="Mathematics" Value="Mathematics" />
                                <asp:ListItem Text="Physics" Value="Physics" />
                                <asp:ListItem Text="Science" Value="Science" />
                            </asp:DropDownList>
                                
                        </HeaderTemplate>
                    </asp:TemplateField>

                    <asp:CommandField ShowEditButton="true" ButtonType="Link" />
                    <asp:CommandField ShowDeleteButton="true" ButtonType="Link" />

                </Columns>

                <PagerSettings Mode="NumericFirstLast" PageButtonCount="3" FirstPageText="First" LastPageText="Last" />

            </asp:GridView>

            <br />

            <p style="text-align:center">
                <asp:Button ID="btnExportToXML" runat="server" Text="Export to XML" OnClick="btnExportToXML_Click" />&nbsp;
                <asp:Button ID="btnExportToExcel" runat="server" Text="Export to Excel" OnClick="btnExportToExcel_Click" />
            </p>

            <p style="text-align:center">
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
            </p>

        </div>
    
    </div>
    </form>
</body>
</html>
