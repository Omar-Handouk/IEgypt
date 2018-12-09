<%@ Page Language="C#" AutoEventWireup="true" CodeFile="User_CP.aspx.cs" Inherits="User_CP" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="userControlPanel" runat="server">
    <div>
        <h1>Control Panel</h1>

        <hr />

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

        <asp:Button runat="server" ID="Confirm" OnClick="Confirm_Click" Text="Submit"/>
        <asp:Button runat="server" ID="Deactivate" OnClick="Deactivate_Click" Text="De-Activate Account"/>
    </div>
    </form>
</body>
</html>
