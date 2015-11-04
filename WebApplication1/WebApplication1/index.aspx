<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="WebApplication1.index" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Home</title>
    <link href="stilmall.css" type="text/css" rel="stylesheet" /> 
    <script src="javascript.js"></script> 
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
                <li><a style="background-color:#eb9b00" class="home" href = "index.aspx">Start</a></li>
                <li><a class="home" href = "LicenseTest.aspx">Licensieringstest</a></li>
                <li><a class="home" href = "UpdateTest.aspx">Kunskapsupdatering</a></li>
                <li><a class="home" href = "TestResults.aspx">Provresultat</a></li>
            </ul> 
        </div>
        <!-- Questions -->

        <div class="loginbox" id="loginbox">
            <h3>Ange användarnamn och lösenord för att logga in</h3>
            <asp:Label id="lblUsername" runat="server" Text="Användarnamn"></asp:Label><br>
            <asp:TextBox ID="textboxusername" runat="server"/>
            <br />
            <br>
            <asp:Label id="lblPassword" runat="server" Text="Lösenord"></asp:Label>
            <br />
            <asp:TextBox ID="textboxpassword" runat="server"/>
            <br />
            <br>

            <asp:Label id="LabelStatusLogin" runat="server" Text="Label"></asp:Label>
            <br />
            <br>
            <asp:Button class="btnLogIn" ID="btn1" text="Logga in" runat="server" OnClick="btn1_Click"/>      
        </div>
        <!-- Footer -->
        <div class="footer">
        </div>
    </div> 
    
    </form>
</body>
</html>
