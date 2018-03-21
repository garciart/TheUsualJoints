<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/admin.Master" AutoEventWireup="true" CodeBehind="restaurant-image.aspx.cs" Inherits="TheUsualJoints.Admin.restaurant_image" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <script type="text/javascript">
        function textboxMultilineMaxNumber(txt, maxLen) {
            try {
                if (txt.value.length > (maxLen - 1))
                    return false;
            }
            catch (e) {
            }
        }
    </script>
    <div class="admin_div1">
        <h1><asp:Label ID="Label1" runat="server" Text="Label"></asp:Label></h1>
        <asp:Image ID="RestaurantImage" runat="server" />
            <p>1. <asp:FileUpload ID="FileUpload1" runat="server" /></p>
            <p>
                2. <asp:Button runat="server" id="UploadButton" text="Upload" onclick="UploadButton_Click" />
                <asp:Label ID="WarningLabel" runat="server"></asp:Label>
            </p>
            <p>3. <asp:Button ID="UploadCompleteButton" runat="server" Text="Done or Cancel" OnClick="UploadCompleteButton_Click" /></p>
        <hr />
        <p>
            <asp:Button ID="deletePhotoButton" runat="server" BackColor="Red" Font-Bold="True" ForeColor="Yellow" OnClick="deletePhotoButton_Click" Text="Delete Photo" OnClientClick="return confirm('Are you sure you want to delete this photo?');"/>
        </p>
    </div>
</asp:Content>