<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/admin.Master" AutoEventWireup="true" CodeBehind="restaurant-image-edit.aspx.cs" Inherits="TheUsualJoints.Admin.restaurant_image_edit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="admin_div1">
        <h1><asp:Label ID="Label1" runat="server" Text="Label"></asp:Label></h1>
        <b>Note - (R) indicates required fields</b>
        <br>
        <p><b>Logo: </b></p>
        <asp:Image ID="RestaurantLogo_Image" runat="server" />
        <p><asp:Button ID="changeImage_Button0" runat="server" CommandArgument="0" OnClick="RestaurantImageButton_Click" Text="Change Logo" /></p>
        <p>(Max image size is 32 KB. Max image dimensions are 250 x 250 pixels)</p>
        <hr />
        <p><b><asp:Label ID="photoLabel1" runat="server"></asp:Label></b></p>
        <asp:Image ID="Restaurant_Photo_01" runat="server" />
        <p><asp:Button ID="changeImage_Button1" runat="server" CommandArgument="1" OnClick="RestaurantImageButton_Click" Text="Change or Delete Image" /></p>
        <p>(Max image size is 512 KB. Max image dimensions are 768 x 768 pixels)</p>
        <hr />
        <p><b><asp:Label ID="photoLabel2" runat="server"></asp:Label></b></p>
        <asp:Image ID="Restaurant_Photo_02" runat="server" />
        <p><asp:Button ID="changeImage_Button2" runat="server" CommandArgument="2" OnClick="RestaurantImageButton_Click" Text="Change or Delete Image" /></p>
        <p>(Max image size is 512 KB. Max image dimensions are 768 x 768 pixels)</p>
        <hr />
        <p><b><asp:Label ID="photoLabel3" runat="server"></asp:Label></b></p>
        <asp:Image ID="Restaurant_Photo_03" runat="server" />
        <p><asp:Button ID="changeImage_Button3" runat="server" CommandArgument="3" OnClick="RestaurantImageButton_Click" Text="Change or Delete Image" /></p>
        <p>(Max image size is 512 KB. Max image dimensions are 768 x 768 pixels)</p>
        <hr />
        <p><b><asp:Label ID="photoLabel4" runat="server"></asp:Label></b></p>
        <asp:Image ID="Restaurant_Photo_04" runat="server" />
        <p><asp:Button ID="changeImage_Button4" runat="server" CommandArgument="4" OnClick="RestaurantImageButton_Click" Text="Change or Delete Image" /></p>
        <p>(Max image size is 512 KB. Max image dimensions are 768 x 768 pixels)</p>
        <hr />
        <p><b><asp:Label ID="photoLabel5" runat="server"></asp:Label></b></p>
        <asp:Image ID="Restaurant_Photo_05" runat="server" />
        <p><asp:Button ID="changeImage_Button5" runat="server" CommandArgument="5" OnClick="RestaurantImageButton_Click" Text="Change or Delete Image" /></p>
        <p>(Max image size is 512 KB. Max image dimensions are 768 x 768 pixels)</p>
        <hr />
        <br />
        <asp:Button ID="btnDone" runat="server" Text="Done" OnClick="btnDone_Click" />
    </div>
</asp:Content>
