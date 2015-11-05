<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="mytests.aspx.cs" Inherits="WebApplication1.mytests" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>My tests</title>
    <link href="stilmall.css" type="text/css" rel="stylesheet" /> 
</head>
<body>
    <form id="form1" runat="server">
         <!-- Container -->
        <div class="container"> 

        <!-- Header -->
        <div class="header">          
            <h1>JE Bank</h1>
        </div>
            
        <!-- Navigation -->
        <div class="nav">
            <ul class="clear">
                <li><a class="home" href = "index.html">Home</a></li>
            </ul> 
        </div>
            <!-- Navigation -->
            <asp:GridView ID="GridViewMyTests" runat="server" AutoGenerateColumns="false">
                <Columns>
                    <asp:BoundField DataField="first_name" HeaderText="Namn" />
                    <asp:BoundField DataField="last_name" HeaderText="Efternamn" />
                    <asp:BoundField DataField="licensed" HeaderText="Licens" />
                    <asp:BoundField DataField="name" HeaderText="Test namn" />
                    <asp:BoundField DataField="grade" HeaderText="Betyg" />
                    <asp:BoundField DataField="points" HeaderText="Poäng" />
                    <asp:BoundField DataField="date" HeaderText="Datum" />
                    <asp:BoundField DataField="firstname" HeaderText="Ledarens namn" />
                    <asp:BoundField DataField="lastname" HeaderText="Ledarens efternamn" />
                </Columns>
                
            </asp:GridView>

        <!-- Footer -->
        <div class="footer">
        </div>
    </div> 
    
    </form>
</body>
</html>
