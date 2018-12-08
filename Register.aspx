<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Register</title>
    
</head>
<body>
    <form id="registerationForm" runat="server">
    <div>
        <h1>Registeration Form</h1>

        <hr />
        
        <asp:Label runat="server" Text="User Type: " />
        <asp:DropDownList runat="server" ID="userTypeList" OnSelectedIndexChanged="userTypeList_SelectedIndexChanged" AutoPostBack="true">
            <asp:ListItem Text="Viewer" Value="viewer" />
            <asp:ListItem Text="Contributor" Value="contributor" />
            <asp:ListItem Text="Authorized Reviewer" Value="Authorized Reviewer" />
            <asp:ListItem Text="Content Manager" Value="content Manager" />
        </asp:DropDownList>

        <br />
        <br />

        <asp:Label runat="server" Text="E-mail: " />
        <asp:TextBox runat="server" ID="email" placeholder="Enter E-mail" Width="250px"/>

        <br />
        <br />

        <asp:Label runat="server" Text="Password: " />
        <asp:TextBox runat="server" ID="password" placeholder="Enter Password" Width="250px" />

        <br />
        <br />

        <asp:Label runat="server" Text="First Name: " />
        <asp:TextBox runat="server" ID="firstName" placeholder="Enter First Name" Width="250px" />

        <br />
        <br />

        <asp:Label runat="server" Text="Middle Name: " />
        <asp:TextBox runat="server" ID="middleName" placeholder="Enter Middle Name" Width="250px" />

        <br />
        <br />

        <asp:Label runat="server" Text="Last Name: " />
        <asp:TextBox runat="server" ID="lastName" placeholder="Enter Last Name" Width="250px" />

        <br />
        <br />

        <asp:Label runat="server" Text="Birth Date: " />
        <asp:TextBox runat="server" ID="birthDate" placeholder="Enter Birthdate (DD-MM-YYYY)" Width="250px" />

        <br />
        <br />

        <h3>Leave below empty if does not apply</h3>
        <hr />

        <!--Viewer-->

        <asp:Label runat="server" Text="Workplace Name: " />
        <asp:TextBox runat="server" ID="workplaceName" placeholder="Enter Workplace Name" Width="250px" />
        
        <br />
        <br />

        <asp:Label runat="server" Text="Working Place Type: " />
        <asp:TextBox runat="server" ID="workingPlaceType" placeholder="Enter Working Place Type" Width="250px" />

        <br />
        <br />

        <asp:Label runat="server" Text="Working Place Description: " />
        <asp:TextBox runat="server" ID="workingPlaceDescription" placeholder="Enter Working Place Description" Width="250px" />

        <br />
        <br />

        <!--Contributor-->

        <asp:Label runat="server" Text="Specialization: " />
        <asp:TextBox runat="server" ID="specialization" placeholder="Enter Specialization" Width="250px" Enabled="false"/>

        <br />
        <br />

        <asp:Label runat="server" Text="Portofolio Link: " />
        <asp:TextBox runat="server" ID="portofolioLink" placeholder="Enter Portofolio Link" Width="250px" Enabled="false"/>

        <br />
        <br />

        <asp:Label runat="server" Text="Years of Experience: " />
        <asp:TextBox runat="server" ID="yearsOfExperience" placeholder="Enter Years Of Experience" Width="250px" Enabled="false"/>

        <br />
        <br />

        <!--Staff-->

        <asp:Label runat="server" Text="Hire Date: " />
        <asp:TextBox runat="server" ID="hireDate" placeholder="Enter Hiring Date (DD-MM-YYYY)" Width="250px" Enabled="false"/>

        <br />
        <br />

        <asp:Label runat="server" Text="Working Hours: " />
        <asp:TextBox runat="server" ID="workingHours" placeholder="Enter Working Hours" Width="250px" Enabled="false"/>

        <br />
        <br />

        <asp:Label runat="server" Text="Payment Rate: " />
        <asp:TextBox runat="server" ID="paymentRate" placeholder="Enter Payment Rate" Width="250px" Enabled="false"/>

        <br />
        <br />

        <asp:Button runat="server" ID="register" OnClick="register_Click" Text="Submit"/>
    </div>
    </form>
</body>
</html>
