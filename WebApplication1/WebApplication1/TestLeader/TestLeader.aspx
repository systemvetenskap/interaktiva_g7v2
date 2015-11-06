<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestLeader.aspx.cs" Inherits="WebApplication1.TestResults" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Home</title>
    <link href="/stilmall.css" type="text/css" rel="stylesheet" /> 
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
                <li><a class="home" href = "/Employee/Employee.aspx">Mina prov</a></li>
                <li><a class="home" href = "Testleader/TestResults.aspx">Provresultat</a></li>
            </ul>
        </div>

        <!-- Footer -->
        <div class="footer">
        </div>
    </div> 
    
    </form>
</body>
</html>
