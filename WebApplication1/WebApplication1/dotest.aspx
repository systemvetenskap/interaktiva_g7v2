<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="dotest.aspx.cs" Inherits="WebApplication1.Employee.mytestsite" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml"> 
<head runat="server">
    <title>Home</title>
    <link href="/stilmall.css" type="text/css" rel="stylesheet" /> 
  

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
    

        <div id="timerbox" runat ="server">
        <asp:Label id="LabelTimer" runat="server" value="Tid kvar:"></asp:Label>
        <span id="timer"></span>
            </div>
        <div class="questionbox" id="qDiv" runat="server">

        &nbsp;<asp:table class="tbl" id="table1" runat ="server"></asp:table> 
            <asp:Button class="btnsub" ID="btn1" text="Lämna in" runat="server" OnClick="btn1_Click" />      
        </div>
        <!-- Footer -->
        <div class="footer">
        </div>
    </div> 
    
    </form>
        <script type="text/javascript">
         var minutes = 30;
         var seconds = 00;
         var run = <%=this.timerVar%>;
        
       

        if (run < 2)
        {
     
         var counter = setInterval(timer, 1000) //körs varje sekund

            
        }
        else{
            result();
        }
     function timer() {

            
            seconds = seconds - 1
            if (seconds <= 0) {
                minutes -= 1;
                seconds += 59;
            }
            if (minutes <= 1) {
                minutes == 1;
                seconds = 0;
                clearInterval(counter);
                alert("Tiden är slut");
                //Kod här när tiden är slut
                window.location.replace("mytests.aspx");
                return;
            }
            document.getElementById("timer").innerHTML = minutes + " minuter " + seconds + " sekunder";
     }
     function result() {
         var tpoints = <%=this.tpoints%>;
         var gr = <%=this.gr%>;
         var ec = <%=this.ecPoints%>;
         var et = <%=this.ethPoints%>;
         var pr = <%=this.pPoints%>;
        
         var etotal = ec / 8;
        if (gr < 2)
         {
            var grade = "Godkänd";

        }
        else{
            var grade ="Icke godkänd";
        }
         document.getElementById("LabelTimer").innerHTML = "Poäng:"+tpoints+" Betyg: "+grade+"<br><br>"+"<span style='color: green;'>Grönmarkerade fält = rätt svar</span>"+"<br>"+"<span style='color: red;'>Rödmarkerade fält = fel svar</span>"+"<br>"+"<span>Products: "+etotal+"</span>";
     }
     
    </script>
</body>
</html>
