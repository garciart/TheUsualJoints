<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CitiesList.ascx.cs" Inherits="TheUsualJoints.CitiesList" %>

<asp:DataList ID="DataList1" runat="server">
    <HeaderTemplate>
        <h2>Choose a City:</h2>
    </HeaderTemplate>
    <ItemTemplate>
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# "~/Admin/test-admin.aspx?CountryID=" + Request.QueryString["CountryID"] + "&StateID="  + Request.QueryString["StateID"] + "&CityID=" + Eval("CityID") %>' Text='<%# Eval("CityName") %>' ToolTip='<%# Eval("CityName") %>'></asp:HyperLink>
    </ItemTemplate>
</asp:DataList>
