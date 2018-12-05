<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>

    <p>Hello, ASP.Net World!</p>

    <form id="form1" runat="server">
    
        <asp:Button runat="server" Text="Show all upcoming events" OnClick="showEvents" />

        <asp:TextBox runat="server" ID="eventID" />
    </form>

    <asp:Table runat="server" id="eventsTable">
            <asp:TableHeaderRow id="Table1HeaderRow" 
            BackColor="LightBlue"
            runat="server">
            <asp:TableHeaderCell 
                Scope="Column" 
                Text="Event ID" />
            <asp:TableHeaderCell  
                Scope="Column" 
                Text="Event Description" />
            <asp:TableHeaderCell 
                Scope="Column" 
                Text="Event Location" />
            <asp:TableHeaderCell 
                Scope="Column" 
                Text="Event City" />
            <asp:TableHeaderCell 
                Scope="Column" 
                Text="Event Date/Time" />
            <asp:TableHeaderCell 
                Scope="Column" 
                Text="Event Entertainer" />
        </asp:TableHeaderRow>             
        </asp:Table>
</body>
</html>
