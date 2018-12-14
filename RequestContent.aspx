<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RequestContent.aspx.cs" Inherits="RequestContent" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Request New Content</title>
</head>
<body>
    <form id="requestContent" runat="server">
    <div>
        <h1>Request New Content</h1>
        <hr />
        <br />

        <asp:Label runat="server" Text="Request Description: " />
        <asp:TextBox runat="server" TextMode="MultiLine" ID="description" pleaceholder="Enter Content Description" />

        <br />
        <br />

        <asp:DropDownList runat="server" ID="contributorName" >
            
        </asp:DropDownList>

        <br />
        <br />

        <asp:Button runat="server" ID="submitRequest" Text="Order" OnClick="submitRequest_Click" />

    </div>
    </form>
</body>
</html>
