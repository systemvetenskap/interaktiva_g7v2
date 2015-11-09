<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="testres.aspx.cs" Inherits="WebApplication1.testres" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>All tests</title>
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
                <li><a href = "mytests.aspx">Mina prov</a></li>
                <li><a class="provresultat" href = "testres.aspx">Provresultat</a></li>
            </ul> 
        </div>
          
         <!-- Tests -->     
             &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;     
             <asp:Label ID="LabelLicens" class="Button Licens" runat="server" Text="Licens" Font-Size="Large"></asp:Label>

            &nbsp;<asp:DropDownList ID="DropDownListLicensed" Class="Dropdown Licens" runat="server" Font-Size="Large">
                <asp:ListItem Value="Alla">Alla</asp:ListItem>
                <asp:ListItem Value="Licensed">Licenserad</asp:ListItem>
                <asp:ListItem Value="Icke licensed">Ej licenserad</asp:ListItem>
            </asp:DropDownList>


            &nbsp;<asp:Label ID="LabelBetyg" class="Button Betyg" runat="server" Text="Betyg" Font-Size="Large"></asp:Label>

            &nbsp;<asp:DropDownList ID="DropDownListGrade" Class="Dropdown Grade" runat="server" Font-Size="Large">
                <asp:ListItem Value="Inga betyg">Inga betyg</asp:ListItem>
                <asp:ListItem Value="Alla">Alla</asp:ListItem>
                <asp:ListItem Value="Godkänd">Godkänd</asp:ListItem>
                <asp:ListItem Value="Underkänd">Underkänd</asp:ListItem>
            </asp:DropDownList>


            &nbsp;<asp:Label ID="LabelLeader" class="Button Ledare" runat="server" Text="Ledare" Font-Size="Large"></asp:Label>

             &nbsp;<asp:DropDownList ID="DropDownListLeader" runat="server" Class="Dropdown Leader" DataTextField="Desc" DataValueField="leader_id" Font-Size="Large">
                 <asp:ListItem Value="Alla">Alla</asp:ListItem>
            </asp:DropDownList>

           

            &nbsp;<asp:Button ID="ButtonSearchTest" Text="Sök" OnServerClick="ListShows" Width="93px"  runat="server" Font-Size="Large" />

            <br />
            <br />

            <asp:GridView ID="GridViewMyTests" class="GridViewMyTests GridView" runat="server" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="fullname" HeaderText="Namn" />
                    <asp:BoundField DataField="licensed" HeaderText="Licens" />
                    <asp:BoundField DataField="name" HeaderText="Test namn" />
                    <asp:BoundField DataField="grade" HeaderText="Betyg" />
                    <asp:BoundField DataField="points" HeaderText="Poäng" />
                    <asp:BoundField DataField="maxdate" HeaderText="Datum"  />
                    <asp:BoundField DataField="leader" HeaderText="Ledare" />
                    <asp:HyperLinkField
                        DataNavigateUrlFields="testid"
                        DataNavigateUrlFormatString="oldtest.aspx"
                        DataTextField="testid"
                        HeaderText="Hämta test"
                        SortExpression="testid" />
                </Columns>
                
            </asp:GridView>

        <!-- Footer -->
        <div class="footer">
        </div>
    </div> 
    
    </form>
</body>
</html>
