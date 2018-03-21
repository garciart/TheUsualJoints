<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CountriesList.ascx.cs" Inherits="TheUsualJoints.CountriesList" %>

<asp:DataList ID="DataList1" runat="server">
    <HeaderTemplate>
        <h2>Choose a Country:</h2>
    </HeaderTemplate>
    <ItemTemplate>
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# "~/Admin/test-admin.aspx?CountryID=" + Eval("CountryID") %>' Text='<%# Eval("CountryName") %>' ToolTip='<%# Eval("CountryName") %>'></asp:HyperLink>
    </ItemTemplate>
</asp:DataList>
