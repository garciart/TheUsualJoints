<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/admin.Master" AutoEventWireup="true" CodeBehind="advertiser-add.aspx.cs" Inherits="TheUsualJoints.Admin.advertiser_add" %>
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
        <b>Note - (R) indicates required fields</b>
        <p></p>
        <table>
            <tr>
                <td>
                    <b>Name (R): </b>
                </td>
                <td>
                    <asp:TextBox ID="TextBox1" runat="server" MaxLength="50" Columns="50" onkeypress="return textboxMultilineMaxNumber(this,50)"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox1" ErrorMessage="This field cannot be blank" ForeColor="Red" Text="This field cannot be blank" ToolTip="This field cannot be blank" Font-Bold="True"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <p><b>Alternate Text (R): </b></p>
                </td>
                <td>
                    <p>
                        <asp:TextBox ID="TextBox2" runat="server" MaxLength="70" Columns="70" onkeypress="return textboxMultilineMaxNumber(this,70)"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBox2" ErrorMessage="This field cannot be blank" ForeColor="Red" Text="This field cannot be blank" ToolTip="This field cannot be blank" Font-Bold="True"></asp:RequiredFieldValidator>
                    </p>
                </td>
            </tr>
            <tr>
                <td>
                    <b>Advertiser Link: </b>
                </td>
                <td>
                    <asp:TextBox ID="TextBox3" runat="server" MaxLength="255" Columns="64" Rows="4" onkeypress="return textboxMultilineMaxNumber(this,255)" AutoCompleteType="BusinessUrl" TextMode="Url"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <p><b>Category: </b></p>
                </td>
                <td>
                    <p><asp:TextBox ID="TextBox4" runat="server" MaxLength="50" Columns="50" onkeypress="return textboxMultilineMaxNumber(this,50)"></asp:TextBox></p>
                </td>
            </tr>
            <tr>
                <td>
                    <b>Impressions: </b>
                </td>
                <td>
                    <asp:TextBox ID="TextBox5" runat="server" TextMode="Number"></asp:TextBox>
                    <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="TextBox5" Display="Dynamic" ErrorMessage="Enter a value between 1 and 100" ForeColor="Red" ToolTip="Enter a value between 1 and 100" Font-Bold="True" MaximumValue="100" MinimumValue="1" Type="Integer"></asp:RangeValidator>
                    (The higher the number, the more the ad appears)
                </td>
            </tr>
        </table>
        <hr />
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="conditional">
            <ContentTemplate>
                <p><b>Image: </b>(Max image size is 32 KB. Max image dimensions are 250 x 250 pixels)</p>
                <asp:FileUpload ID="FileUpload1" runat="server" />&nbsp;
                <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_Click" ValidationGroup="2" />
                <p>
                    <asp:Image ID="Image1" runat="server" />
                </p>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnUpload" />
            </Triggers>
        </asp:UpdatePanel>
        <hr />
        <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click" />
        &nbsp;
        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
    </div>
</asp:Content>
