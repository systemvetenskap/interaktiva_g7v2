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
            <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
            <asp:GridView ID="GridViewMyTests" runat="server"></asp:GridView>

        <!-- Footer -->
        <div class="footer">
        </div>
    </div> 
    
    </form>
</body>
</html>
