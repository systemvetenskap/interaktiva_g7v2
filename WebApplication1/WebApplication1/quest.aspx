<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="quest.aspx.cs" Inherits="WebApplication1.quest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Home</title>
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
        <!-- Questions -->
    <div class="questionbox">
    
        <asp:table class="tbl" id="table1" runat ="server">
        
        </asp:table>
        </div>
        <!-- Footer -->
        <div class="footer">
        </div>
    
    </div>
    </form>
</body>
</html>
