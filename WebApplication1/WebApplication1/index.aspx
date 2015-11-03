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
                <li><a class="home" href = "index.aspx">Start</a></li>
                <li><a class="home" href = "LicenseTest.aspx">Licensieringstest</a></li>
                <li><a class="home" href = "UpdateTest.aspx">Kunskapsupdatering</a></li>
                <li><a class="home" href = "TestResults.aspx">Provresultat</a></li>
            </ul> 
        </div>
        <!-- Questions -->

        <div class="loginbox">
            <h3>Ange användarnamn och lösenord för att logga in</h3>
            <asp:Label ID="lblUsername" runat="server" Text="Användarnamn"></asp:Label><br>
            <input id="username" type="text" /><br />
            <br>
            <asp:Label ID="lblPassword" runat="server" Text="Lösenord"></asp:Label><br>
            <input id="password" type="text" /><br />
            <br>
            <asp:Button class="btnLogIn" ID="btn1" text="Logga in" runat="server"/>      
        </div>
        <!-- Footer -->
        <div class="footer">
        </div>
    </div> 
    
    </form>
</body>
</html>
