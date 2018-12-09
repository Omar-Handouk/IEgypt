<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Home</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button runat="server" Text="Login" ID="loginButton" OnClick="loginButton_Click" Enabled="true"/>
        <asp:Button ID="logoutButton" runat="server" Text="Logout" Enabled="false" OnClick="logoutButton_Click"/>
        <asp:Button runat="server" Text="Register" ID="registerButton" OnClick="registerButton_Click" />
        <asp:Button runat="server" Text="Search" ID="searchButton" OnClick="searchButton_Click" />
    </div>
    </form>
</body>
</html>
