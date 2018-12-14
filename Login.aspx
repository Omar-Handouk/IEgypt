<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h1>User Login</h1>
        <hr />
        <asp:Label runat="server" Text="Email" />
        <asp:TextBox runat="server" ID="userEmail" placeholder="Enter Email" />
        
        <br />
        <br />

        <asp:Label runat="server" Text="Password" />
        <asp:TextBox runat="server" ID="userPassword" placeholder="Enter Password" />

        <br />
        <br />

        <asp:Button runat="server" ID="login" Text="Login" OnClick="login_Click" />
    </div>
    </form>
</body>
</html>
