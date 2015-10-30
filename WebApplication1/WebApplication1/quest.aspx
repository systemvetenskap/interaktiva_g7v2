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

            <script type="text/javascript">
                function coundDown(secs, elem)
                {
                    var element = document.getElementById(elem);
                     element.innerHTML = "Tid kvar: " + secs + "";
                    if (secs < 1)
                    {
                        clearTimeout(timer);
                        //Kod här för när timern är slut
                       
                    }
                    secs--;
                    var timer = setTimeout('countDown('+secs+',"'+elem+'")',1000);
                }
                </script>
    <div class="questionbox">
        <asp:Label ID="LabelTimer" runat="server" Text="Label"></asp:Label>
        <asp:table class="tbl" id="table1" runat ="server"></asp:table>
        
        </div>
            <script type="text/javascript">countDown(1800,"LabelTimer");</script>
        <!-- Footer -->
        <div class="footer">
        </div>
    
    </div>
    </form>
</body>
</html>
