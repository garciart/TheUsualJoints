<%@ Page Title="" Language="C#" MasterPageFile="~/two-column.Master" AutoEventWireup="true" CodeBehind="site-map.aspx.cs" Inherits="TheUsualJoints.site_map" %>

<%@ Register Src="~/share-bar.ascx" TagPrefix="uc1" TagName="sharebar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h1 id="title_tag" runat="server" class="two_col_title_tag" style="margin-top:0;">Site Map</h1>
    <p><a href="/Default.aspx" title="Back to Home Page">[Back to Home Page]</a></p>
    <p>Our Site Map: An index of all the pages on our web site in case you get lost!</p>
    <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
    <uc1:sharebar runat="server" ID="sharebar" />
    <p><a href="/Default.aspx" title="Back to Home Page">[Back to Home Page]</a></p>
</asp:Content>