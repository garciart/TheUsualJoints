<%@ Page Title="" Language="C#" MasterPageFile="~/two-column.Master" AutoEventWireup="true" CodeBehind="error.aspx.cs" Inherits="TheUsualJoints.error" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h1 id="title_tag" runat="server" class="two_col_title_tag" style="margin-top:0;">Oops! Something went wrong!</h1>
    <p><a href="/Default.aspx" title="Back to Home Page">[Back to Home Page]</a></p>
    <img class="error_img" src="images/error.gif" alt="Oops! Something went wrong!" />
    <br />
    <p>We're sorry, but the following error has occurred: <asp:Label ID="Label1" runat="server" Text="Error Code" CssClass="error_label"></asp:Label></p>
    <br />
    <ul><li><asp:Label ID="Label2" runat="server" Text="Error Description" CssClass="error_label"></asp:Label></li></ul>
    <br />
    <p>We've been notified and we're working on it. Meantime, click <a href="site-map.aspx" title="Site Map">here</a> to get to our site map, which lists all the pages on our web site!</p>
    <p>If you have any questions, contact us at <a href="mailto:info@theusualjoints.com"><span itemprop="email">info@theusualjoints.com</span></a>.</p>
    <p><a href="/Default.aspx" title="Back to Home Page">[Back to Home Page]</a></p>
</asp:Content>