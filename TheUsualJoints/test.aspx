<%@ Page Title="" Language="C#" MasterPageFile="~/two-column.Master" AutoEventWireup="true" CodeBehind="test.aspx.cs" Inherits="TheUsualJoints.test" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h1 id="title_tag" runat="server" class="two_col_title_tag" style="margin-top:0;"><asp:Label ID="Label1" runat="server" Text="Label"></asp:Label></h1>
    <asp:Panel ID="Panel1" runat="server"></asp:Panel>
</asp:Content>