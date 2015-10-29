<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="quest.aspx.cs" Inherits="WebApplication1.quest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div id="questionbox">
    
        <asp:Label ID="lblQuestion" runat="server" Text="Label"></asp:Label>
        <br />
        <br />
    
        <asp:RadioButton ID="RadioButton1" runat="server" />
        <br />
        <asp:RadioButton ID="RadioButton2" runat="server" />
        <br />
        <asp:RadioButton ID="RadioButton3" runat="server" />
        <br />
        <asp:RadioButton ID="RadioButton4" runat="server" />

        <br />
        <br />

    </div>
        <p>
        <asp:Button ID="Button1" runat="server" Text="Nästa" />
        </p>
    </form>
</body>
</html>
