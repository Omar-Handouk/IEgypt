<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SCO.aspx.cs" Inherits="SCO" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="show_OriginalContent_Contributors" runat="server">

    <div runat="server" ID="ContributorsDiv">

        <h1>Show Contributors</h1>

        <asp:Button runat="server" Text="Show" ID="showContributors" OnClick="showContributors_Click" />

        <br />

        <hr />

        <asp:Table runat="server" ID="contributorTable">

                <asp:TableHeaderRow runat="server">
                    <asp:TableHeaderCell runat="server" Scope="Column" Text="Year Of Experience"/>
                    <asp:TableHeaderCell runat="server" Scope="Column" Text="Portofolio Link"/>
                    <asp:TableHeaderCell runat="server" Scope="Column" Text="Specialization"/>
                    <asp:TableHeaderCell runat="server" Scope="Column" Text="E-mail"/>
                    <asp:TableHeaderCell runat="server" Scope="Column" Text="First Name"/>
                    <asp:TableHeaderCell runat="server" Scope="Column" Text="Middle Name"/>
                    <asp:TableHeaderCell runat="server" Scope="Column" Text="Last Name"/>
                    <asp:TableHeaderCell runat="server" Scope="Column" Text="Birth Date"/>
                    <asp:TableHeaderCell runat="server" Scope="Column" Text="Age"/>
                    <asp:TableHeaderCell runat="server" Scope="Column" Text="Active"/>
                </asp:TableHeaderRow>

                

            </asp:Table>
    </div>
    
    <hr />

    <div runat="server" id="OriginalContentDiv">

        <h1>Show Original Content</h1>

        <asp:Button runat="server" Text="Show" ID="showOriginalContent" OnClick="showOriginalContent_Click" />
        <asp:DropDownList runat="server" ID="contributorNames" AutoPostBack="true">

        </asp:DropDownList>
        <br />

        <hr />

        <asp:Table runat="server" ID="contentTable">
            <asp:TableHeaderRow runat="server">
                <asp:TableHeaderCell runat="server" Scope="Column" Text="Rating" />
                <asp:TableHeaderCell runat="server" Scope="Column" Text="Link" />
                <asp:TableHeaderCell runat="server" Scope="Column" Text="Upload Date" />
                <asp:TableHeaderCell runat="server" Scope="Column" Text="Category" />
                <asp:TableHeaderCell runat="server" Scope="Column" Text="Subcategory" />
                <asp:TableHeaderCell runat="server" Scope="Column" Text="Type" />
                <asp:TableHeaderCell runat="server" Scope="Column" Text="E-mail" />
                <asp:TableHeaderCell runat="server" Scope="Column" Text="First Name" />
                <asp:TableHeaderCell runat="server" Scope="Column" Text="Middle Name" />
                <asp:TableHeaderCell runat="server" Scope="Column" Text="Last Name" />
                <asp:TableHeaderCell runat="server" Scope="Column" Text="Portofolio Link" />
            </asp:TableHeaderRow>
        </asp:Table>

    </div>
    </form>
</body>
</html>
