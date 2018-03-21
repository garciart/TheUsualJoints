<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="StatesList.ascx.cs" Inherits="TheUsualJoints.StatesList" %>

<asp:DataList ID="DataList1" runat="server">
    <HeaderTemplate>
        <h2>Choose a State:</h2>
    </HeaderTemplate>
    <ItemTemplate>
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# "~/Admin/test-admin.aspx?CountryID=" + Request.QueryString["CountryID"] + "&StateID=" + Eval("StateID") %>' Text='<%# Eval("StateName") %>' ToolTip='<%# Eval("StateName") %>'></asp:HyperLink>
    </ItemTemplate>
</asp:DataList>
