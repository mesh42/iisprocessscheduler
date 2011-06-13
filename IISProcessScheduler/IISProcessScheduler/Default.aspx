<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="IISProcessScheduler.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server" EnableViewState="false">
    <div>
    
        <asp:Label ID="lblWarmupState" runat="server" Text="lblWarmupState" EnableViewState="false"></asp:Label>
        <br />
        <br />
        <asp:LinkButton ID="lnkPauseResume" runat="server" 
            onclick="lnkPauseResume_Click" EnableViewState="false">lnkPauseResume</asp:LinkButton>
        <br />
        <br />
        <br />
        <asp:Label ID="lblHistory" runat="server" Text="lblHistory" EnableViewState="false"></asp:Label>
        <br />
        <asp:LinkButton ID="lblRefresh" runat="server" EnableViewState="false">Refresh</asp:LinkButton>
    
    </div>
    </form>
</body>
</html>
