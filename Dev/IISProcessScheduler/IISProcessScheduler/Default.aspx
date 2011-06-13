<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="IISProcessScheduler.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Label ID="lblWarmupState" runat="server" Text="lblWarmupState"></asp:Label>
        <br />
        <br />
        <asp:LinkButton ID="lnkPauseResume" runat="server" 
            onclick="lnkPauseResume_Click">lnkPauseResume</asp:LinkButton>
        <br />
        <br />
        <br />
        <asp:Label ID="lblHistory" runat="server" Text="lblHistory"></asp:Label>
        <br />
        <asp:LinkButton ID="lblRefresh" runat="server">Refresh</asp:LinkButton>
    
    </div>
    </form>
</body>
</html>
