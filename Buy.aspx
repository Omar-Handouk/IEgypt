<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Buy.aspx.cs" Inherits="Buy" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Buy Original Content</title>
</head>
<body>
    <form id="buyContent" runat="server">
    <div>
        <h1>Buy Available Content</h1>
        <hr />
        <br />

        <asp:Table runat="server" ID="contentTable">

                <asp:TableHeaderRow runat="server">
                    <asp:TableHeaderCell runat="server" Scope="Column" Text="Buy" />
                    <asp:TableHeaderCell runat="server" Scope="Column" Text="Link"/>
                    <asp:TableHeaderCell runat="server" Scope="Column" Text="Upload Date"/>
                    <asp:TableHeaderCell runat="server" Scope="Column" Text="Category"/>
                    <asp:TableHeaderCell runat="server" Scope="Column" Text="Subcategory"/>
                    <asp:TableHeaderCell runat="server" Scope="Column" Text="Type"/>
                </asp:TableHeaderRow>

                

            </asp:Table>
    </div>
    </form>
</body>
</html>
