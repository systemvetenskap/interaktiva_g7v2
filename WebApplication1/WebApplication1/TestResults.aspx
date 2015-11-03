<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestResults.aspx.cs" Inherits="WebApplication1.TestResults" %>

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
                <li><a style="background-color:#eb9b00" class="home" href = "TestResults.aspx">Provresultat</a></li>
            </ul> 
        </div>
        <!-- Questions -->
<%--        <div class="questionbox">

        &nbsp;<asp:table class="tbl" id="table1" runat ="server"></asp:table> 
            <asp:Button class="btnsub" ID="btn1" text="Lämna in" runat="server" OnClick="btn1_Click" />      
        </div>--%>
        <!-- Footer -->
        <div class="footer">
        </div>
    </div> 
    
    </form>
</body>
</html>
