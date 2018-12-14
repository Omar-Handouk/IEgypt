<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditDeleteComment.aspx.cs" Inherits="EditDeleteComment" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h1>Edit and Delete Comments</h1>

        <hr />
        <br />

        Choose Comment: <asp:DropDownList runat="server" ID="comments" AutoPostBack="true" OnSelectedIndexChanged="comments_SelectedIndexChanged">
                    <asp:ListItem Selected="True" Text="-----" Value=""></asp:ListItem>
                 </asp:DropDownList>
        <br />
        <br />

        <asp:Table runat="server" ID="commentInfo">

            <asp:TableHeaderRow runat="server">
                <asp:TableHeaderCell runat="server" Scope="Column" Text="Comment Text" BackColor="Cyan"></asp:TableHeaderCell>
                <asp:TableHeaderCell runat="server" Scope="Column" Text="Content Link" BackColor="Cyan"></asp:TableHeaderCell>
                <asp:TableHeaderCell runat="server" Scope="Column" Text="Date Written" BackColor="Cyan"></asp:TableHeaderCell>
            </asp:TableHeaderRow>


        </asp:Table>

        <br />
        <br />

        <asp:Label runat="server" Text="Edit Comment: " Visible="false" ID="commentLabel"></asp:Label> <asp:TextBox Visible="false" runat="server" ID="commentText" placeholder="Edit Comment Here" TextMode="MultiLine"></asp:TextBox>

        <br />
        <br />

        <asp:Button runat="server" Text="Delete Comment" ID="deleteComment" OnClick="deleteComment_Click" />
        <asp:Button runat="server" Text="Edit Comment" ID="editComment" OnClick="editComment_Click" />
    </div>
    </form>
</body>
</html>
