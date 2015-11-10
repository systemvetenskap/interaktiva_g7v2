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
    
        <!-- Questions -->
        <div class="questionbox" id="questionbox" runat="server">

        &nbsp;<asp:table class="tbl" id="table1" runat ="server"></asp:table> 
              
        </div>
    </div>
    
    </form>

</body>
</html>
