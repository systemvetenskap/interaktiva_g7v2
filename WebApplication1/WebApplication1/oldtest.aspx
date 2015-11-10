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
        
        <!-- Info om testet -->
        <div class="OldTestInfo">

            <div class="NameInfo">
            <asp:Label class="HeadlineLabels LabelFullname" ID="LabelFullNameHeadline" runat="server" Text="Namn:"></asp:Label>
            <asp:Label class="Label" ID="LabelFullName" runat="server" Text="Label"></asp:Label>
            </div>

            <div class="TestInfo">
            <asp:Label class="HeadlineLabels" ID="LabelTestNameHeadline" runat="server" Text="Test typ:"></asp:Label>
            <asp:Label class="Label" ID="LabelTestName" runat="server" Text="Label"></asp:Label>

            <asp:Label class="HeadlineLabels" ID="LabelTestPointsHeadline" runat="server" Text="Poäng:"></asp:Label>
            <asp:Label class="Label" ID="LabelTestPoints" runat="server" Text="Label"></asp:Label>

            <asp:Label class="HeadlineLabels" ID="LabelTestGradeHeadline" runat="server" Text="Betyg:"></asp:Label>
            <asp:Label class="Label" ID="LabelTestGrade" runat="server" Text="Label"></asp:Label>

            <asp:Label class="HeadlineLabels" ID="LabelTestDateHeadline" runat="server" Text="Datum:"></asp:Label>
            <asp:Label class="Label" ID="LabelTestDate" runat="server" Text="Label"></asp:Label>
            
            </div>

       </div> 
        <!-- Questions -->
        <div class="questionbox" id="questionbox" runat="server">

        &nbsp;<asp:table class="tbl" id="table1" runat ="server"></asp:table> 
        </div>

    </div>
    
    </form>

</body>
</html>
