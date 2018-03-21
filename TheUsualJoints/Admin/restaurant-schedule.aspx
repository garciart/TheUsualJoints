<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/admin.Master" AutoEventWireup="true" CodeBehind="restaurant-schedule.aspx.cs" Inherits="TheUsualJoints.Admin.restaurant_schedule" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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
        <hr />
            <b>Quick Link to This Restaurant's 
            <asp:HyperLink ID="InfoQuickLink" runat="server">Information</asp:HyperLink> or
            <asp:HyperLink ID="EventQuickLink" runat="server">Special Events</asp:HyperLink></b>
        <hr />
        <p>
            <b>Choose another restaurant here:</b> <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged"></asp:DropDownList>
        </p>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="6">
            <HeaderStyle BackColor="DarkGray" />
            <AlternatingRowStyle BackColor="DarkGray" />
            <Columns>
                <asp:TemplateField HeaderText="ID" Visible="False">
                    <ItemTemplate>
                        <p><asp:Label ID="RestaurantRoutineID_Label" runat="server" Text='<%# Eval("RestaurantRoutineID") %>'></asp:Label></p>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Day">
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <p><b><asp:Label ID="Weekday_Label" runat="server" Text='<%# Enum.Parse(typeof(DayOfWeek), Eval("Weekday").ToString()) %>'></asp:Label></b></p>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Regular Schedule">
                    <ItemTemplate>
                        <p>
                            <b>Hours of Operation (50 characters max):</b> <asp:TextBox ID="HoursOfOperation_TextBox" runat="server" MaxLength="50" Columns="50" Text='<%# Bind("HoursOfOperation") %>' onkeypress="return textboxMultilineMaxNumber(this,50)"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="HoursOfOperation_RequiredFieldValidator" runat="server" ControlToValidate="HoursOfOperation_TextBox" ErrorMessage="This field cannot be blank" ForeColor="Red" Text="This field cannot be blank" ToolTip="This field cannot be blank" Font-Bold="True"></asp:RequiredFieldValidator>
                        </p>
                        <p>
                            <b>Happy Hour (50 characters max):</b> <asp:TextBox ID="HappyHourTimes_TextBox" runat="server" MaxLength="50" Columns="50" Text='<%# Bind("HappyHourTimes") %>' onkeypress="return textboxMultilineMaxNumber(this,50)"></asp:TextBox>
                        </p>
                        <hr />
                            <p><b>Happy Hour Specials (300 characters max):</b></p>
                            <asp:TextBox ID="HappyHourSpecials_TextBox" runat="server" Rows="3" MaxLength="300" Columns="100" TextMode="MultiLine" Text='<%# Bind("HappyHourSpecials") %>' onkeypress="return textboxMultilineMaxNumber(this,300)"></asp:TextBox>
                        <hr />
                            <p><b>Food and Drink Specials (300 characters max):</b></p>
                            <asp:TextBox ID="FoodAndDrinkSpecials_TextBox" runat="server" Rows="3" MaxLength="300" Columns="100" TextMode="MultiLine" Text='<%# Bind("FoodAndDrinkSpecials") %>' onkeypress="return textboxMultilineMaxNumber(this,300)"></asp:TextBox>
                        <hr />
                            <p><b>Special Event (300 characters max):</b></p>
                            <asp:TextBox ID="RestaurantEvents_TextBox" runat="server" Rows="3" MaxLength="300" Columns="100" TextMode="MultiLine" Text='<%# Bind("RestaurantEvents") %>' onkeypress="return textboxMultilineMaxNumber(this,300)"></asp:TextBox>
                        <br />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <RowStyle VerticalAlign="Top" />
        </asp:GridView>
        <br />
        <asp:Panel ID="Panel1" runat="server">
            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
            &nbsp;
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
            &nbsp;
            <asp:Label ID="lblMsg" runat="server" ></asp:Label>
            <br /><br />
        </asp:Panel>
    </div>
</asp:Content>