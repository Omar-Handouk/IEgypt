<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Event.aspx.cs" Inherits="Event" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

    <head runat="server">
    
        <title>Create An Event</title>

    </head>

    <body>
        
        <h1>Create an event</h1>

        <hr />
        <br />

        <form id="eventForm" runat="server">
    
            <div id="citySection">
               
                <asp:Label runat="server" Text="City of event: "/>
                
                <asp:TextBox  runat="server" ID="city" placeholder="Enter city" />

            </div>

            <br />
            <br />

            <div id="locationSection">
               
                <asp:Label runat="server" Text="Location of event: " />
                
                <asp:TextBox  runat="server" ID="location" placeholder="Enter location" />

            </div>

            <br />
            <br />

            <div id="dateTimeSection">
               
                <asp:Label runat="server" Text="Date/Time of event: " />
                
                <asp:TextBox  runat="server" ID="dateTime" TextMode="DateTime" placeholder="Enter Date/Time of Event (MM-DD-YYYY HH:MM:SS (AM or PM))" Width="400px"/>

            </div>

            <br />
            <br />

            <div id="entertainerSection">
               
                <asp:Label runat="server" Text="Entertainer: " />
                
                <asp:TextBox  TextMode="SingleLine" runat="server" ID="entertainer" placeholder="Enter entertainer name" />

            </div>

            <br />
            <br />

            <div id="descriptionSection">
               
                <asp:Label runat="server" Text="Description of event: " />
                
                <asp:TextBox  TextMode="MultiLine" runat="server" ID="description" placeholder="Enter description" />

            </div>

            <hr />
     

            <div runat="server" ID="photosAndVideos">
                <h2 style="width: 649px">Add Addtional Photos & Videos - Please insert different values</h2>
                <asp:TextBox ID="numberOfPhotos" runat="server" placeholder="Number of additional photos" Width="200px"></asp:TextBox>
                <asp:TextBox ID="numberOfVideos" runat="server" placeholder="Number of additional videos" Width="200px"></asp:TextBox>
                <asp:Button ID="addBoxes" runat="server" Text="Add" OnClick="addBoxes_Click"/><br />
                <asp:Label runat="server" Text="Disclaimer: If additional boxes are added the form will reset" />
                <hr />
                <div runat="server" id="photoSection">
                
                <h4>Event Photos - Please Insert Different Links</h4>
                
                <hr />
                <br />

                <asp:Label runat="server" Text="Photo Link: "></asp:Label><br />
                <asp:TextBox runat="server" TextMode="Url" ID="photo_0" placeholder="Enter Photo URL" ></asp:TextBox>
                
            </div>

            <hr />

            <div runat="server" id="videoSection">
                
                <h4>Event Videos - Please Insert Different Links</h4>
                
                <hr />
                <br />

                <asp:Label runat="server" Text="Video Link: "></asp:Label><br />
                <asp:TextBox runat="server" ID="video_0" placeholder="Enter Video URL" ></asp:TextBox>
                
            </div>
            </div>
            
            <br />
            <br />

            <asp:CheckBox ID="createAdvert" runat="server" Text="Create Advertisment for event"/>

            <br />
            <br />

            <asp:Button ID="submitEvent" runat="server" Text="Create Event" OnClick="submitEvent_Click"/>

        </form>
    
    </body>

</html>
