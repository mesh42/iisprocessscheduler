<%@ Page EnableViewState="false" Language="C#" MasterPageFile="~/Shared/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="IISProcessScheduler.Default" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="iisproc" Namespace="IISProcessScheduler.Shared.UserControls" %>
<%@ Register src="~/Shared/UserControls/ServiceStatusControl.ascx" tagname="ServiceStatusControl" tagprefix="iisproc" %>
<%@ Register TagPrefix="iisproc" TagName="ServiceController" Src="~/Shared/UserControls/ServiceController.ascx" %>
<%@ Register TagPrefix="iisproc" TagName="LogoControl" Src="~/Shared/UserControls/LogoControl.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <asp:Label ID="lblWarmupState" runat="server" Text="lblWarmupState" EnableViewState="false"></asp:Label>
        &nbsp;<br />
        <asp:DataList SkinId="ProcessList" ID="dlHistory" runat="server" Enabled="False" EnableViewState="false">
        <HeaderTemplate>
            Process History
        </HeaderTemplate>
        <ItemTemplate>
            <div style="height:24px; width: 24px;border-left: 0px solid black;float:left;padding:0px 2px 0px 2px"><iisproc:ServiceStatusControl ID="ServiceStatusControl1" runat="server" ProcessInfo="<%# Container.DataItem %>" /></div>
            <div style="height:24px; width: 150px; border-left: 0px solid black;float:left;padding:0px 2px 0px 2px; vertical-align:middle"><%# ((IProcessInfo)Container.DataItem).Date %></div>
            <div style="height:24px; width: 50px; border-left: 0px solid black;float:left;padding:0px 2px 0px 2px;vertical-align:middle"><%# ((IProcessInfo)Container.DataItem).Result %></div>
            <div style="height:24px; width: 50px; text-align:right; border-left: 0px solid black;float:left;padding:0px 2px 0px 2px; vertical-align:middle"><%# ((IProcessInfo)Container.DataItem).TimeElapsed %>ms</div>
            <div style="height:24px; border-left: 0px solid black;float:left;padding:0px 2px 0px 2px;vertical-align:middle"><%# ((IProcessInfo)Container.DataItem).Resource %></div>
        </ItemTemplate>
        </asp:DataList>
        <br />
    </div>
        <script  type="text/javascript">
            function updateDateKey(value) {
                var behavior = $find('dp1');
                if (behavior) {
                    behavior.populate(value);
                }
            }
            Sys.Application.add_load(function () { updateDateKey('G'); });
    </script>

            <ajaxToolkit:DynamicPopulateExtender ID="dp" BehaviorID="dp1" runat="server"
            TargetControlID="Panel1"
            ClearContentsDuringUpdate="true"
            ServiceMethod="GetHtml"
            UpdatingCssClass="dynamicPopulate_Updating" />
            <asp:Panel ID="Panel1" runat="server">hithr</asp:Panel>

</asp:Content>
