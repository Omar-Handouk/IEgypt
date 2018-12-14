<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RateContent.aspx.cs" Inherits="RateContent" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Rate Original Content</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h1>Rate Content</h1>

        <hr />
        <br />

        <asp:DropDownList runat="server" ID="content"></asp:DropDownList>
        <asp:DropDownList runat="server" ID="rating">
            <asp:ListItem Text="1" Value="1"></asp:ListItem>
            <asp:ListItem Text="2" Value="2"></asp:ListItem>
            <asp:ListItem Text="3" Value="3"></asp:ListItem>
            <asp:ListItem Text="4" Value="4"></asp:ListItem>
            <asp:ListItem Text="5" Value="5"></asp:ListItem>
        </asp:DropDownList>

        <asp:Button runat="server" Text="Rate" ID="rate" OnClick="rate_Click" />
    </div>
    </form>
</body>
</html>
