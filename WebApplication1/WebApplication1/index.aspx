<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="WebApplication1.index" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Home</title>
    <link href="stilmall.css" type="text/css" rel="stylesheet" /> 
    <%--<script src="javascript.js"></script> --%>
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
            <asp:TextBox ID="TextBox1" Text="användarnamn" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:TextBox ID="TextBox2" Text="Lösenord" runat="server"></asp:TextBox>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Login1" />
                 <br />
            <br />
            <asp:Button class="btnemp" ID="btnemp" text="Logga in som teammedlem" runat="server" OnClick="btnemp_Click" />
            <asp:Button class="btnlead" ID="btnlead" text="Logga in som teamledare" runat="server" OnClick="btnlead_Click" />
            
        </div>
        <!-- Footer -->
        <div class="footer">
        </div>
    </div> 
    
    </form>
</body>
</html>
