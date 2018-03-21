<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/admin.Master" AutoEventWireup="true" CodeBehind="restaurant-events.aspx.cs" Inherits="TheUsualJoints.Admin.restaurant_events" %>

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
        <hr />
            <b>Quick Link to This Restaurant's 
            <asp:HyperLink ID="InfoQuickLink" runat="server">Information</asp:HyperLink> or 
            <asp:HyperLink ID="ScheduleQuickLink" runat="server">Schedule</asp:HyperLink></b>
        <hr />
        <p>
            <b>Choose another restaurant here:</b> <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged"></asp:DropDownList>
        </p>
        <b>How many events do you want to insert:</b> <asp:TextBox ID="RowsToAdd_TextBox" runat="server" TextMode="Number"></asp:TextBox>
        <asp:Button ID="btnAddRow" runat="server" Text="Add Rows" OnClick="btnAddRow_Click" />
        <asp:RequiredFieldValidator ID="RowsToAdd_TextBox_RequiredFieldValidator" runat="server" ControlToValidate="RowsToAdd_TextBox" Display="Dynamic" ErrorMessage="This field cannot be blank" ForeColor="Red" ToolTip="This field cannot be blank" Font-Bold="True" ValidationGroup="Group1"></asp:RequiredFieldValidator>
        <asp:RangeValidator ID="RowsToAdd_TextBox_RangeValidator" runat="server" ControlToValidate="RowsToAdd_TextBox" Display="Dynamic" ErrorMessage="Enter a value between 1 and 255" ForeColor="Red" ToolTip="Enter a value between 1 and 255" Font-Bold="True" MinimumValue="1" MaximumValue="255" ValidationGroup="Group1"></asp:RangeValidator>
        <br /><br />
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="6">
            <HeaderStyle BackColor="DarkGray" />
            <AlternatingRowStyle BackColor="DarkGray" />
            <Columns>
                <asp:TemplateField HeaderText="Entry #">
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <b><%# Container.DataItemIndex +1 %></p>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Special Event Information">
                    <ItemTemplate>
                        <b>Description (50 characters max):</b> <asp:TextBox ID="ShortDescription_TextBox" runat="server" MaxLength="50" Columns="50" onkeypress="return textboxMultilineMaxNumber(this,50)"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="ShortDescription_RequiredFieldValidator" runat="server" ControlToValidate="ShortDescription_TextBox" Display="Dynamic" ErrorMessage="This field cannot be blank" ForeColor="Red" Text="*" ToolTip="This field cannot be blank" Font-Bold="True" ValidationGroup="Group1"></asp:RequiredFieldValidator>
                        <br />
                        <p>
                            <b>Date Start:</b> <asp:TextBox ID="SpecialEventStart_TextBox" runat="server" TextMode="Date"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="SpecialEventStart_RequiredFieldValidator" runat="server" ControlToValidate="SpecialEventStart_TextBox" Display="Dynamic" ErrorMessage="This field cannot be blank" ForeColor="Red" Text="*" ToolTip="This field cannot be blank" Font-Bold="True" ValidationGroup="Group1"></asp:RequiredFieldValidator>
                        </p>
                        <p>
                            <b>Date End:</b> <asp:TextBox ID="SpecialEventEnd_TextBox" runat="server" TextMode="Date"></asp:TextBox>
                        </p>
                        <p>
                            <b>Hours of Operation (50 characters max):</b> <asp:TextBox ID="SpecialHours_TextBox" runat="server" MaxLength="50" Columns="50" onkeypress="return textboxMultilineMaxNumber(this,50)"></asp:TextBox>
                        </p>
                        <p>
                            <b>Happy Hour (50 characters max):</b> <asp:TextBox ID="SpecialHappyHour_TextBox" runat="server" MaxLength="50" Columns="50" onkeypress="return textboxMultilineMaxNumber(this,50)"></asp:TextBox>
                            <div style="text-align:right"><i><b>Should I </b></i><asp:CheckBox ID="SpecialHappyHour_CheckBox" runat="server" Text="Leave This Field Blank?" /></div>
                        </p>
                        <hr />
                            <b>Happy Hour Specials (300 characters max):</b><br />
                            <asp:TextBox ID="SpecialHHSpecials_TextBox" runat="server" Rows="3" MaxLength="300" Columns="100" TextMode="MultiLine" onkeypress="return textboxMultilineMaxNumber(this,300)"></asp:TextBox>
                            <div style="text-align:right"><i><b>Should I </b></i><asp:DropDownList ID="SpecialHH_ARBFlag_DropDownList" runat="server">
                                <asp:ListItem Enabled="True" Selected="True" Text="Append this to the Regularly Scheduled Event? (Default)" Value="0"/>
                                <asp:ListItem Enabled="True" Selected="False" Text="Replace the Regularly Scheduled Event?" Value="1"/>
                                <asp:ListItem Enabled="True" Selected="False" Text="Leave Regularly Scheduled Event Blank?" Value="2"/>
                            </asp:DropDownList></div>
                        <hr />
                            <b>Food and Drink Specials (300 characters max):</b><br />
                            <asp:TextBox ID="SpecialFoodDrinkSpecials_TextBox" runat="server" Rows="3" MaxLength="300" Columns="100" TextMode="MultiLine" onkeypress="return textboxMultilineMaxNumber(this,300)"></asp:TextBox>
                            <div style="text-align:right"><i><b>Should I </b></i><asp:DropDownList ID="SpecialFD_ARBFlag_DropDownList" runat="server">
                                <asp:ListItem Enabled="True" Selected="True" Text="Append this to the Regularly Scheduled Event? (Default)" Value="0"/>
                                <asp:ListItem Enabled="True" Selected="False" Text="Replace the Regularly Scheduled Event?" Value="1"/>
                                <asp:ListItem Enabled="True" Selected="False" Text="Leave Regularly Scheduled Event Blank?" Value="2"/>
                            </asp:DropDownList></div>
                        <hr />
                            <b>Special Event (300 characters max):</b><br />
                            <asp:TextBox ID="SpecialEvent_TextBox" runat="server" Rows="3" MaxLength="300" Columns="100" TextMode="MultiLine" onkeypress="return textboxMultilineMaxNumber(this,300)"></asp:TextBox>
                            <div style="text-align:right"><i><b>Should I </b></i><asp:DropDownList ID="SpecialEvent_ARBFlag_DropDownList" runat="server">
                                <asp:ListItem Enabled="True" Selected="True" Text="Append this to the Regularly Scheduled Event? (Default)" Value="0"/>
                                <asp:ListItem Enabled="True" Selected="False" Text="Replace the Regularly Scheduled Event?" Value="1"/>
                                <asp:ListItem Enabled="True" Selected="False" Text="Leave Regularly Scheduled Event Blank?" Value="2"/>
                            </asp:DropDownList></div>
                        <br />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <RowStyle VerticalAlign="Top" />
        </asp:GridView>
        <br />
        <asp:Panel ID="Panel1" runat="server" Visible="false">
            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
            &nbsp;
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CausesValidation="false" ValidateRequestMode="Disabled" />
            &nbsp;
            <asp:Label ID="lblMsg" runat="server" ></asp:Label>
            <br /><br />
        </asp:Panel>
        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" OnRowCancelingEdit="GridView2_RowCancelingEdit" OnRowDeleting="GridView2_RowDeleting" OnRowEditing="GridView2_RowEditing" OnRowUpdating="GridView2_RowUpdating" AllowPaging="True" OnPageIndexChanging="GridView2_PageIndexChanging" PageSize="50" CellPadding="6">
            <Columns>
                <asp:TemplateField HeaderText="Start Date">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemTemplate>
                        <asp:Label ID="SpecialEventStart_Label" runat="server" Text='<%# Bind("SpecialEventStart", "{0:ddd, dd MMM yy}") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Wrap="False" />
                    <EditItemTemplate>
                        <asp:TextBox ID="SpecialEventStart_TextBox" runat="server" Text='<%# Bind("SpecialEventStart", "{0:yyyy-MM-dd}") %>' TextMode="Date"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="SpecialEventStart_RequiredFieldValidator" runat="server" ControlToValidate="SpecialEventStart_TextBox" Display="Dynamic" ErrorMessage="This field cannot be blank" ForeColor="Red" Text="*" ToolTip="This field cannot be blank" Font-Bold="True" ValidationGroup="Group2"></asp:RequiredFieldValidator>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="End Date">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemTemplate>
                        <asp:Label ID="SpecialEventEnd_Label" runat="server" Text='<%# String.IsNullOrEmpty(Eval("SpecialEventEnd").ToString()) ? Eval("SpecialEventStart", "{0:ddd, dd MMM yy}") : Eval("SpecialEventEnd", "{0:ddd, dd MMM yy}") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Wrap="False" />
                    <EditItemTemplate>
                        <asp:TextBox ID="SpecialEventEnd_TextBox" runat="server" Text='<%# String.IsNullOrEmpty(Eval("SpecialEventEnd").ToString()) ? Eval("SpecialEventStart", "{0:yyyy-MM-dd}") : Eval("SpecialEventEnd", "{0:yyyy-MM-dd}") %>' TextMode="Date"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ID" Visible="False">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemTemplate>
                        <asp:Label ID="SpecialEventID_Label" runat="server" Text='<%# Eval("SpecialEventID") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:Label ID="SpecialEventID_Label" runat="server" Text='<%# Eval("SpecialEventID") %>'></asp:Label>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Event">
                    <ItemTemplate>
                        <asp:Label ID="ShortDescription_Label" runat="server" Text='<%# Bind("ShortDescription") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Left" Width="1024px" />
                    <HeaderStyle HorizontalAlign="Left" />
                    <EditItemTemplate>
                        <b>Description (50 characters max):</b> <asp:TextBox ID="ShortDescription_TextBox" runat="server" MaxLength="50" Columns="50" Text='<%# Bind("ShortDescription") %>' onkeypress="return textboxMultilineMaxNumber(this,50)"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="ShortDescription_RequiredFieldValidator" runat="server" ControlToValidate="ShortDescription_TextBox" Display="Dynamic" ErrorMessage="This field cannot be blank" ForeColor="Red" Text="*" ToolTip="This field cannot be blank" Font-Bold="True" ValidationGroup="Group2"></asp:RequiredFieldValidator>
                        <br />
                        <p>
                            <b>Hours of Operation (50 characters max):</b> <asp:TextBox ID="SpecialHours_TextBox" runat="server" MaxLength="50" Columns="50" Text='<%# Bind("SpecialHours") %>' onkeypress="return textboxMultilineMaxNumber(this,50)"></asp:TextBox>
                        </p>
                        <p>
                            <b>Happy Hour (50 characters max):</b> <asp:TextBox ID="SpecialHappyHour_TextBox" runat="server" MaxLength="50" Columns="50" Text='<%# Bind("SpecialHappyHour") %>' onkeypress="return textboxMultilineMaxNumber(this,50)"></asp:TextBox>
                            <div style="text-align:right"><i><b>Should I </b></i><asp:CheckBox ID="SpecialHappyHour_CheckBox" runat="server" Text="Leave This Field Blank?" /></div>
                        </p>
                        <hr />
                            <b>Happy Hour Specials (300 characters max):</b><br />
                            <asp:TextBox ID="SpecialHHSpecials_TextBox" runat="server" Rows="3" MaxLength="300" Columns="100" TextMode="MultiLine" Text='<%# Bind("SpecialHHSpecials") %>' onkeypress="return textboxMultilineMaxNumber(this,300)"></asp:TextBox>
                            <div style="color:red;"><i><b>- Should I </b></i><asp:DropDownList ID="SpecialHH_ARBFlag_DropDownList" runat="server" SelectedValue='<%# Bind("SpecialHH_ARBFlag") %>'>
                                <asp:ListItem Enabled="True" Text="Append this to the Regularly Scheduled Event?" Value="0"/>
                                <asp:ListItem Enabled="True" Text="Replace the Regularly Scheduled Event?" Value="1"/>
                                <asp:ListItem Enabled="True" Text="Leave Regularly Scheduled Event Blank?" Value="2"/>
                            </asp:DropDownList></div>
                        <hr />
                            <b>Food and Drink Specials (300 characters max):</b><br />
                            <asp:TextBox ID="SpecialFoodDrinkSpecials_TextBox" runat="server" Rows="3" MaxLength="300" Columns="100" TextMode="MultiLine" Text='<%# Bind("SpecialFoodDrinkSpecials") %>' onkeypress="return textboxMultilineMaxNumber(this,300)"></asp:TextBox>
                            <div style="color:red;"><i><b>- Should I </b></i><asp:DropDownList ID="SpecialFD_ARBFlag_DropDownList" runat="server" SelectedValue='<%# Bind("SpecialFD_ARBFlag") %>'>
                                <asp:ListItem Enabled="True" Text="Append this to the Regularly Scheduled Event?" Value="0"/>
                                <asp:ListItem Enabled="True" Text="Replace the Regularly Scheduled Event?" Value="1"/>
                                <asp:ListItem Enabled="True" Text="Leave Regularly Scheduled Event Blank?" Value="2"/>
                            </asp:DropDownList></div>
                        <hr />
                            <b>Special Event (300 characters max):</b><br />
                            <asp:TextBox ID="SpecialEvent_TextBox" runat="server" Rows="3" MaxLength="300" Columns="100" TextMode="MultiLine" Text='<%# Bind("SpecialEvent") %>' onkeypress="return textboxMultilineMaxNumber(this,300)"></asp:TextBox>
                            <div style="color:red;"><i><b>- Should I </b></i><asp:DropDownList ID="SpecialEvent_ARBFlag_DropDownList" runat="server" SelectedValue='<%# Bind("SpecialEvent_ARBFlag") %>'>
                                <asp:ListItem Enabled="True" Text="Append this to the Regularly Scheduled Event?" Value="0"/>
                                <asp:ListItem Enabled="True" Text="Replace the Regularly Scheduled Event?" Value="1"/>
                                <asp:ListItem Enabled="True" Text="Leave Regularly Scheduled Event Blank?" Value="2"/>
                            </asp:DropDownList></div>
                        <br />
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False">
                    <EditItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" CommandName="Update" Text="Update" />
                        &nbsp;
                        <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Edit" Text="Edit" />
                        &nbsp;
                        <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Delete" Text="Delete" OnClientClick="return confirm('Are you sure you want to delete this event?');" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
            <PagerSettings Mode="NumericFirstLast" />
        </asp:GridView>
    </div>
</asp:Content>