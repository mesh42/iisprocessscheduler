<%@ Page Language="C#" MasterPageFile="~/Shared/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="IISProcessScheduler.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
    
        <asp:Label ID="lblWarmupState" runat="server" Text="lblWarmupState" EnableViewState="false"></asp:Label>
        &nbsp;<asp:LinkButton ID="lnkEnableWarmUp" runat="server" Visible="False" 
            EnableViewState="false" onclick="lnkEnableWarmUp_Click">Click here to enable PreWarmUp IIS Settings.</asp:LinkButton>
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
</asp:Content>
