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
            <h1>JE Bank</h1>
        </div>
        <!-- Navigation -->
        <div class="nav">
            <ul class="clear">
                <li><a class="home" href = "mytests.aspx">Mina prov</a></li>
                <li><a class="home" href = "/TestLeader/TestResults.aspx">Provresultat</a></li>

            </ul> 
        </div>
    
        <!-- Questions -->
            <div id="mytests" class="mytests">
                <asp:Label ID="LabelLicensetest" runat="server" Text="Licensieringstest: "></asp:Label>
                <br /> <br />
                <asp:Label ID="LabelUpdatetest" runat="server" Text="Årlig Kunskapsupdatering: "></asp:Label><br/><br />
                <asp:Button ID="btnLicenseTest" runat="server" Text="Starta Licensieringstestet" OnClick="btnLicenseTest_Click"  href = "dotest.aspx" />
                &nbsp;<asp:Button ID="btnUpdateTest" runat="server" Text="Starta Kunskapsupdatering" OnClick="btnUpdateTest_Click"  href = "dotest.aspx" />
                </div>
    
    </div>
    </form>
</body>
</html>
