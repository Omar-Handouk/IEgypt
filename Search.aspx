<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Search.aspx.cs" Inherits="Search" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Search</title>
</head>
<body>
    <form id="searchForm" runat="server">
        <div id="contentSection">
            
            <asp:Label runat="server" Text="Search for Content" />
            
            <br />
            
            <asp:Label runat="server" Text="Content Type" />
            <asp:dropdownlist runat="server" ID="contentType" OnSelectedIndexChanged="contentType_SelectedIndexChanged">
            </asp:dropdownlist>
            
            <br />

            <asp:Label runat="server" Text="Content Category" />
            <asp:DropDownList runat="server" ID="contentCategory" OnSelectedIndexChanged="contentCategory_SelectedIndexChanged">
            </asp:DropDownList>
            
            <asp:Button runat="server" ID="contentSearch" Text="Search Content" OnClick="contentSearch_Click" />

            <br />

            <asp:Table runat="server" ID="contentTable">

                <asp:TableHeaderRow runat="server">
                    <asp:TableHeaderCell runat="server" Scope="Column" Text="Link"/>
                    <asp:TableHeaderCell runat="server" Scope="Column" Text="Upload Date"/>
                    <asp:TableHeaderCell runat="server" Scope="Column" Text="Category"/>
                    <asp:TableHeaderCell runat="server" Scope="Column" Text="Subcategory"/>
                    <asp:TableHeaderCell runat="server" Scope="Column" Text="Type"/>
                </asp:TableHeaderRow>

                

            </asp:Table>

        </div>
    
        <hr />

        <div id="contributorSection">
            
            <asp:Label runat="server" Text="Search for Contributor" />
            
            <br />
            
            <asp:TextBox runat="server" ID="contributorName" placeholder="Enter Contributor Name" />
            
            <asp:Button runat="server" ID="contributorSearch" Text="Search Contributors" OnClick="contributorSearch_Click" />
            
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

    </form>
</body>
</html>
