<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DeleteRequest.aspx.cs" Inherits="DeleteRequest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Delete Placed Request</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h1>Delete Requested Content</h1><asp:Label runat="server" Text="Disclaimer-Requests that are shown have already been processed"></asp:Label>

        <hr />
        <br />

        <asp:DropDownList runat="server" ID="content"></asp:DropDownList>

        <asp:Button runat="server" Text="Delete Request" ID="delete" OnClick="delete_Click" />
    </div>
    </form>
</body>
</html>
