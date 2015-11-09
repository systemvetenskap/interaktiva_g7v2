<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="oldtest.aspx.cs" Inherits="WebApplication1.oldtest" %>

<!DOCTYPE html>


<html xmlns="http://www.w3.org/1999/xhtml"> 
<head runat="server">
    <title>Home</title>
    <link href="/stilmall.css" type="text/css" rel="stylesheet" /> 
    <script src="http://code.jquery.com/jquery-1.10.1.min.js"></script>
  

</head>
<body>
    <form id="form1" runat="server">
     <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"> </asp:ScriptManager>   
      
       
         <!-- Container -->
        <div class="container"> 
        
       
        <!-- Header -->
        <div class="header">          
            <h1>JE Bank</h1>
        </div>
        <!-- Navigation -->
        <div class="nav">
            <ul class="clear">
               <%-- <li><a class="home" href = "MyTests.aspx">Mina prov</a></li>
                <li><a class="home" href = "/TestLeader/TestResults.aspx">Provresultat</a></li>--%>

            </ul> 
        </div>
    
        <!-- Questions -->
    

        
      
        <span id="timer"></span>
            </div>
        <div class="questionbox" id="qDiv" runat="server">

        &nbsp;<asp:table class="tbl" id="table1" runat ="server"></asp:table> 
              
        </div>
        <!-- Footer -->
        <div class="footer">
        </div>
 
    
    </form>

</body>
</html>
