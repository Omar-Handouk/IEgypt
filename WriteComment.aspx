<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WriteComment.aspx.cs" Inherits="WriteComment" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Comment</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h1>Write A Comment on Content</h1>

        <asp:Label runat="server" Text="Comment: "></asp:Label>
        <asp:TextBox runat="server" TextMode="MultiLine" ID="comment" placeholder="Comment Here" />

        <br />
        <br />

        <asp:Label runat="server" Text="Content: "></asp:Label>
        <asp:DropDownList runat="server" ID="content"></asp:DropDownList>

        <br />
        <br />

        <asp:Button runat="server" ID="submit" Text="Submit" OnClick="submit_Click" />
    </div>
    </form>
</body>
</html>
