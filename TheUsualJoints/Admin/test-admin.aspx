<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/admin.Master" AutoEventWireup="true" CodeBehind="test-admin.aspx.cs" Inherits="TheUsualJoints.Admin.test_admin" %>

<%@ Register Src="~/CountriesList.ascx" TagPrefix="uc1" TagName="CountriesList" %>
<%@ Register Src="~/StatesList.ascx" TagPrefix="uc1" TagName="StatesList" %>
<%@ Register Src="~/CitiesList.ascx" TagPrefix="uc1" TagName="CitiesList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <div style="padding:12px;">
        <h1>Test Admin!</h1>
        <h2><asp:Label ID="Label6" runat="server"></asp:Label></h2>
        <hr />
        <div>
            <h2>Update Panel Check</h2>
            Page Time: <%= DateTime.Now.ToString("T") %>
            <br /><br />
            <asp:UpdatePanel id="up1" runat="server">
                <ContentTemplate>
                    <div style="border:1px solid black; padding:12px;">
                        UpdatePanel Time: <%= DateTime.Now.ToString("T") %>
                        <br /><br />
                        <asp:Button id="btn" Text="Update" runat="server" />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <hr />
        <div>
            <h2>SELECT, UPDATE and DELETE Check</h2>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" DataKeyNames="TestID">
                <Columns>
                    <asp:BoundField DataField="LastName" HeaderText="Last Name" SortExpression="LastName" />
                    <asp:BoundField DataField="FirstName" HeaderText="First Name" SortExpression="FirstName" />
                    <asp:TemplateField HeaderText="Birthday" SortExpression="BirthDate">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("BirthDate", "{0:yyyy-MM-dd}") %>' TextMode="Date"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("BirthDate", "{0:d}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Age" HeaderText="Age" SortExpression="Age" ReadOnly="True" />
                    <asp:TemplateField HeaderText="Active" SortExpression="Active">
                        <EditItemTemplate>
                            <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Convert.ToBoolean(Eval("Active")) %>' />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Convert.ToBoolean(Eval("Active")) %>' Enabled="false" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
                </Columns>
            </asp:GridView>
        </div>
        <hr />
        <div>
            <h2>ADD Check</h2>
            <table>
                <tr>
                    <td><asp:Label ID="Label1" runat="server" Text="Last Name:"></asp:Label></td>
                    <td><asp:TextBox ID="TextBox1" runat="server"></asp:TextBox></td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="RequiredFieldValidator" ControlToValidate="TextBox1" Display="Dynamic" ValidationGroup="add"></asp:RequiredFieldValidator>
                </tr>
                <tr>
                    <td><p><asp:Label ID="Label2" runat="server" Text="First Name:"></asp:Label></p></td>
                    <td><p><asp:TextBox ID="TextBox2" runat="server"></asp:TextBox></p></td>
                </tr>
                <tr>
                    <td><asp:Label ID="Label3" runat="server" Text="Birthday:"></asp:Label></td>
                    <td><asp:TextBox ID="TextBox3" runat="server" TextMode="Date" OnTextChanged="TextBox3_TextChanged"></asp:TextBox></td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="RequiredFieldValidator" ControlToValidate="TextBox3" Display="Dynamic" ValidationGroup="add"></asp:RequiredFieldValidator>
                </tr>
                <tr>
                    <td><p><asp:Label ID="Label4" runat="server" Text="Age:"></asp:Label></p></td>
                    <td><p><asp:TextBox ID="TextBox4" runat="server" TextMode="Number" Enabled="false" ></asp:TextBox></p></td>
                </tr>
                <tr>
                    <td><asp:Label ID="Label5" runat="server" Text="Active:"></asp:Label></td>
                    <td><asp:CheckBox ID="CheckBox1" runat="server" /></td>
                </tr>
            </table>
        </div>
        <hr />
        <div>
            <uc1:CountriesList runat="server" id="CountriesList" />
            <uc1:StatesList runat="server" ID="StatesList" />
            <uc1:CitiesList runat="server" ID="CitiesList" />
        </div>
        <hr />
        <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />
    </div>
</asp:Content>
