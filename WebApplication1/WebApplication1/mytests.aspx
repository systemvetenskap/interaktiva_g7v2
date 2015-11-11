<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="mytests.aspx.cs" Inherits="WebApplication1.mytests" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Mina prov</title>
    <link href="/stilmall.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="container">
        
        <!-- Header -->
        <div class="header">          
            <h1>JE Bank Kompetensportal</h1>
        </div>
        <!-- Navigation -->
        <div class="nav">
            <ul class="clear">
                <li><a class="mytestsmenu" href = "mytests.aspx">Mina prov</a></li>
                <li id="provresultat" runat="server"><a href = "testres.aspx">Provresultat</a></li>

            </ul> 
        </div>
    
        <!-- Questions -->
            <div id="mytests" class="mytests">
                <asp:Label ID="LabelLicensetest" runat="server" Text="Licensieringstest: "></asp:Label>
                <asp:Label ID="lblLicens" runat="server" Text="Label"></asp:Label>
                <br /> <br />
                <asp:Label ID="LabelUpdatetest" runat="server" Text="Årlig Kunskapsuppdatering: "></asp:Label>
                <asp:Label ID="lblAku" runat="server" Text="Label"></asp:Label>
                <br/><br />
                <asp:Button ID="btnLicenseTest" runat="server" Text="Starta Licensieringstestet" OnClick="btnLicenseTest_Click"  href = "dotest.aspx" Font-Size="Large" />
                <asp:Button ID="btnUpdateTest" runat="server" Text="Starta Kunskapsuppdatering" OnClick="btnUpdateTest_Click"  href = "dotest.aspx" Font-Size="Large" />
                </div>

        <div class="tests">
             <asp:GridView ID="GridViewTests" class="GridViewTests GridView" runat="server" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="date" HeaderText="Datum" HtmlEncode="false" DataFormatString="{0:m}" />
                    <asp:BoundField DataField="name" HeaderText="Testyp" />
                    <asp:BoundField DataField="grade" HeaderText="Betyg" />
                    <asp:BoundField DataField="points" HeaderText="Poäng" />
                    <asp:BoundField DataField="leader" HeaderText="Ledare" />
                    <asp:HyperLinkField DataNavigateUrlFields="testid" DataNavigateUrlFormatString="oldtest.aspx?id={0}" Text="Hämta Prov" HeaderText="Hämta prov" Target="_blank"  />
                </Columns>
                
            </asp:GridView>
        </div>    
    </div>
    </form>
</body>
</html>
