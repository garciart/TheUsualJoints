<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/admin.Master" AutoEventWireup="true" CodeBehind="restaurant-image-2.aspx.cs" Inherits="TheUsualJoints.Admin.restaurant_image_2" %>
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
        function UploadComplete(sender, args) {
            window.location.reload(false);
        }
    </script>
    <div class="admin_div1">
        <h1><asp:Label ID="Label1" runat="server" Text="Label"></asp:Label></h1>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:Image ID="RestaurantImage" runat="server" />
                <p>
                    <asp:Label ID="WarningLabel" runat="server"></asp:Label>
                </p>
                <atk:AsyncFileUpload ID="AsyncFileUploadImage" runat="server" OnUploadedComplete="FileUploadComplete" OnClientUploadComplete="UploadComplete" ClientIDMode="AutoID" />
            </ContentTemplate>
        </asp:UpdatePanel>
        <p>
            <asp:Button ID="deletePhotoButton" runat="server" OnClick="deletePhotoButton_Click" Text="Delete Photo" OnClientClick="return confirm('Are you sure you want to delete this photo?');" />
        </p>
        <p><asp:Button ID="UploadCompleteButton" runat="server" Text="Done" OnClick="UploadCompleteButton_Click" /></p>
    </div>
</asp:Content>