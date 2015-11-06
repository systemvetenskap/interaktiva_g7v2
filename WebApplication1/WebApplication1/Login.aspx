<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebApplication1.index" %>

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

            </ul> 
        </div>
        <!-- Questions -->

        <div class="loginbox" id="loginbox">
            <h3>Ange användarnamn och lösenord för att logga in</h3>
            <asp:Login ID="Login1" runat="server" RememberMeText="Kom ihåg mig" TitleText="">
            </asp:Login>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Login1" />
        </div>
        <!-- Footer -->
        <div class="footer">
        </div>
    </div> 
    
    </form>
</body>
</html>
