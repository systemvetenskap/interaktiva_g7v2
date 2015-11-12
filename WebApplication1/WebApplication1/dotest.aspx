<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="dotest.aspx.cs" Inherits="WebApplication1.Employee.mytestsite" %>

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
            <h1>JE Bank Kompetensportal</h1>
        </div>

        <!-- Questions -->

        <div id="timerbox" runat ="server">
        <asp:Label id="LabelTimer" runat="server" value="Tid kvar:"></asp:Label>
        <span id="timer"></span>
            </div>
 
        <div class="questionbox" id="qDiv" runat="server">

        &nbsp;<asp:table class="tbl" id="table1" runat ="server"></asp:table> 
            <asp:Button class="btnsub" ID="btn1" text="Lämna in" runat="server" OnClick="btn1_Click" /><asp:Button class="btnsub" ID="btn2" text="Klar" runat="server" OnClick="btn2_Click" />        
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
               
                var testtype = <%=this.testType%>;
                var id = <%=this.userid%>;
                //Kod här när tiden är slut
                PageMethods.timeOut(testtype,id,onSuccess,onError);
                function onSuccess(){
                    alert("Tiden är ute.");
                    window.location.replace("mytests.aspx");
                }
                function onError(){
                    alert("Oväntat fel uppstod, kontakta administratören.");
                    window.location.replace("mytests.aspx?id="+id);
                }
                
                return;
            }
         document.getElementById("timer").innerHTML = minutes + " minuter " + seconds + " sekunder"+"<br><br>"+"<span style='font-size: 80%'>Tryck på lämna in när du är klar</span>";
     }
     function result() {
         var tpoints = <%=this.tpoints%>;
         var prod = <%=this.pPoints%>;
         var eth = <%=this.ethPoints%>;
         var eco = <%=this.ecPoints%>;
         var gr = <%=this.gr%>;
       
        if (gr < 2)
         {
            var grade = "Godkänd";
        }
        else{
            var grade ="Icke godkänd";
        }
        document.getElementById("LabelTimer").innerHTML = "Poäng: "+tpoints+" Betyg: "+grade+"<br><br>"+"<span style='color: green;'>Grönmarkerade fält = rätt svar</span>"+"<br>"+"<span style='color: red;'>Rödmarkerade fält = fel svar</span>"+"<br><br>"+"<span>Products: "+prod+"%</span>"+"<br>"+"<span>Economy: "+eco+"%</span>"+"<br>"+"<span>Ethics: "+eth+"%</span>"+"<br><br>"+"<span style='font-size: 65%'>Ditt prov är inlämnat</span>"+"<br>"+"<span style='font-size: 65%'> Tryck på klar för att gå tillbaka till Mina Prov</span>";
     }
         //function Check(me) {
         //    alert(me.name);
            
         //   var i = 0;
         //   var tot = 0;
            //for (i = 0; i < chkBoxCount.length; i++) {
            //    if (chkBoxCount[i].checked) {
            //        tot = tot + 1;
            //     
            //        if (tot > 2) {
            //        //Then unchecck the 4th checkbox
            //            chkBoxCount[i].checked = false;
            //        }
            //    }
            //}

            //if (tot > 3) {
            //    alert('inte mer än 3');
            //    return false;
            //}
            //else {
            //    return false;
            //}
        //}

     
    </script>
</body>
</html>
