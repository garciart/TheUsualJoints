<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/admin.Master" AutoEventWireup="true" CodeBehind="restaurant-add.aspx.cs" Inherits="TheUsualJoints.Admin.restaurant_add_plain" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <style>
        td { padding: 6px 0; }
    </style>
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
        <p><b>Note - (R) indicates required fields</b></p>
        <table>
            <tr>
                <td>
                    <b>Name (R): </b>
                </td>
                <td>
                    <asp:TextBox ID="RestaurantName_TextBox" runat="server" MaxLength="60" Columns="60" onkeypress="return textboxMultilineMaxNumber(this,60)"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RestaurantName_RequiredFieldValidator" runat="server" ControlToValidate="RestaurantName_TextBox" ErrorMessage="This field cannot be blank" ForeColor="Red" Text="This field cannot be blank" ToolTip="This field cannot be blank" Font-Bold="True"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <b>Active: </b>
                </td>
                <td>
                    <asp:CheckBox ID="RestaurantActive_CheckBox" runat="server"/>
                </td>
            </tr>
            <tr>
                <td>
                    <b>Priority (R): </b>
                </td>
                <td>
                    <asp:TextBox ID="RestaurantPriority_TextBox" runat="server" TextMode="Number"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RestaurantPriority_RequiredFieldValidator" runat="server" ControlToValidate="RestaurantPriority_TextBox" Display="Dynamic" ErrorMessage="This field cannot be blank" ForeColor="Red" ToolTip="This field cannot be blank" Font-Bold="True"></asp:RequiredFieldValidator>
                    <asp:RangeValidator ID="RestaurantPriority_RangeValidator" runat="server" ControlToValidate="RestaurantPriority_TextBox" Display="Dynamic" ErrorMessage="Enter a value between 1 and 100" ForeColor="Red" ToolTip="Enter a value between 1 and 100" Font-Bold="True" MaximumValue="100" MinimumValue="1" Type="Integer"></asp:RangeValidator>
                    (The lower the number, the closer it appears to the top)
                </td>
            </tr>
            <tr>
                <td>
                    <b>Tier: </b>
                </td>
                <td>
                    <asp:DropDownList ID="RestaurantTier_DropDownList" runat="server">
                        <asp:ListItem Value="1" Text="1 - Basic" />
                        <asp:ListItem Value="2" Text="2 - Premium" />
                        <asp:ListItem Value="3" Text="3 - Ultimate" />
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <b>Motto: </b>
                </td>
                <td>
                    <asp:TextBox ID="RestaurantMotto_TextBox" runat="server" MaxLength="60" Columns="60" onkeypress="return textboxMultilineMaxNumber(this,60)"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <b>Cuisine: </b>
                </td>
                <td>
                    <asp:TextBox ID="RestaurantCuisine_TextBox" runat="server" MaxLength="60" Columns="60" onkeypress="return textboxMultilineMaxNumber(this,60)"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <b>Street Address (R): </b>
                </td>
                <td>
                    <asp:TextBox ID="RestaurantStreetAddress_TextBox" runat="server" MaxLength="120" Columns="60" Rows="2" onkeypress="return textboxMultilineMaxNumber(this,120)" AutoCompleteType="BusinessStreetAddress"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RestaurantStreetAddress_RequiredFieldValidator" runat="server" ControlToValidate="RestaurantStreetAddress_TextBox" Display="Dynamic" ErrorMessage="This field cannot be blank" ForeColor="Red" ToolTip="This field cannot be blank" Font-Bold="True"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <b>City (R): </b>
                </td>
                <td>
                    <asp:TextBox ID="RestaurantCity_TextBox" runat="server" MaxLength="60" Columns="60" onkeypress="return textboxMultilineMaxNumber(this,60)" AutoCompleteType="BusinessCity"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RestaurantCity_RequiredFieldValidator" runat="server" ControlToValidate="RestaurantCity_TextBox" Display="Dynamic" ErrorMessage="This field cannot be blank" ForeColor="Red" ToolTip="This field cannot be blank" Font-Bold="True"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <b>Associated City (R): </b>
                </td>
                <td>
                    <asp:DropDownList ID="CityDropDownList" runat="server"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <b>State (R): </b>
                </td>
                <td>
                    <asp:TextBox ID="RestaurantState_TextBox" runat="server" MaxLength="2" Columns="2" onkeypress="return textboxMultilineMaxNumber(this,2)" AutoCompleteType="BusinessState"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RestaurantState_RequiredFieldValidator" runat="server" ControlToValidate="RestaurantState_TextBox" Display="Dynamic" ErrorMessage="This field cannot be blank" ForeColor="Red" ToolTip="This field cannot be blank" Font-Bold="True"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <b>Country (R): </b>
                </td>
                <td>
                    <asp:TextBox ID="RestaurantCountry_TextBox" runat="server" MaxLength="2" Columns="2" onkeypress="return textboxMultilineMaxNumber(this,2)" AutoCompleteType="BusinessCountryRegion"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RestaurantCountry_RequiredFieldValidator" runat="server" ControlToValidate="RestaurantCountry_TextBox" Display="Dynamic" ErrorMessage="This field cannot be blank" ForeColor="Red" ToolTip="This field cannot be blank" Font-Bold="True"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <b>ZIP Code (R): </b>
                </td>
                <td>
                    <asp:TextBox ID="RestaurantZipCode_TextBox" runat="server" MaxLength="6" Columns="6" onkeypress="return textboxMultilineMaxNumber(this,120)" AutoCompleteType="BusinessZipCode"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RestaurantZipCode_RequiredFieldValidator" runat="server" ControlToValidate="RestaurantZipCode_TextBox" Display="Dynamic" ErrorMessage="This field cannot be blank" ForeColor="Red" ToolTip="This field cannot be blank" Font-Bold="True"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <b>Phone Number (R): </b>
                </td>
                <td>
                    <asp:TextBox ID="RestaurantPhone_TextBox" runat="server" MaxLength="14" Columns="14" onkeypress="return textboxMultilineMaxNumber(this,14)" AutoCompleteType="BusinessPhone" TextMode="Phone"></asp:TextBox> (i.e., 14105555555)
                    <asp:RequiredFieldValidator ID="RestaurantPhone_RequiredFieldValidator" runat="server" ControlToValidate="RestaurantPhone_TextBox" Display="Dynamic" ErrorMessage="This field cannot be blank" ForeColor="Red" ToolTip="This field cannot be blank" Font-Bold="True"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RestaurantPhone_RegularExpressionValidator" runat="server" ControlToValidate="RestaurantPhone_TextBox" Display="Dynamic" ErrorMessage="Must contain only numbers" ForeColor="Red" ToolTip="Must contain only numbers" Font-Bold="True" ValidationExpression="^[0-9]+$" ></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <b>Fax Number: </b>
                </td>
                <td>
                    <asp:TextBox ID="RestaurantFax_TextBox" runat="server" MaxLength="14" Columns="14" onkeypress="return textboxMultilineMaxNumber(this,14)" AutoCompleteType="BusinessFax" TextMode="Phone"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RestaurantFax_RegularExpressionValidator" runat="server" ControlToValidate="RestaurantFax_TextBox" Display="Dynamic" ErrorMessage="Must contain only numbers" ForeColor="Red" ToolTip="Must contain only numbers" Font-Bold="True" ValidationExpression="^[0-9]+$" ></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <b>Email Address (R): </b>
                </td>
                <td>
                    <asp:TextBox ID="RestaurantEmail_TextBox" runat="server" MaxLength="255" Columns="64" Rows="4" onkeypress="return textboxMultilineMaxNumber(this,255)" AutoCompleteType="Email" TextMode="Email"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RestaurantEmail_RequiredFieldValidator" runat="server" ControlToValidate="RestaurantEmail_TextBox" Display="Dynamic" ErrorMessage="This field cannot be blank" ForeColor="Red" ToolTip="This field cannot be blank" Font-Bold="True"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RestaurantEmail_RegularExpressionValidator" runat="server" ControlToValidate="RestaurantEmail_TextBox" Display="Dynamic" ErrorMessage="Invalid Email Address" ForeColor="Red" ToolTip="Invalid Email Address" Font-Bold="True" ValidationExpression="^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$" ></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <b>Web Site: </b>
                </td>
                <td>
                    <asp:TextBox ID="RestaurantWebSite_TextBox" runat="server" MaxLength="255" Columns="64" Rows="4" onkeypress="return textboxMultilineMaxNumber(this,255)" AutoCompleteType="BusinessUrl" TextMode="Url"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <b>Social Link 1: </b>
                </td>
                <td>
                    <asp:TextBox ID="RestaurantSocialLink1_TextBox" runat="server" MaxLength="255" Columns="64" Rows="4" onkeypress="return textboxMultilineMaxNumber(this,255)" AutoCompleteType="BusinessUrl" TextMode="Url"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <b>Social Link 2: </b>
                </td>
                <td>
                    <asp:TextBox ID="RestaurantSocialLink2_TextBox" runat="server" MaxLength="255" Columns="64" Rows="4" onkeypress="return textboxMultilineMaxNumber(this,255)" AutoCompleteType="BusinessUrl" TextMode="Url"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <b>Social Link 3: </b>
                </td>
                <td>
                    <asp:TextBox ID="RestaurantSocialLink3_TextBox" runat="server" MaxLength="255" Columns="64" Rows="4" onkeypress="return textboxMultilineMaxNumber(this,255)" AutoCompleteType="BusinessUrl" TextMode="Url"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <b>About Us: </b>
                </td>
                <td>
                    <asp:TextBox ID="RestaurantAboutUs_TextBox" runat="server" MaxLength="1000" Columns="64" Rows="8" onkeypress="return textboxMultilineMaxNumber(this,1000)" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
        </table>
        <hr />
        <br />
        <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" />
        &nbsp;
        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
    </div>
</asp:Content>
