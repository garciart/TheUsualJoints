<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="status-bar.ascx.cs" Inherits="TheUsualJoints.status_bar" %>

<script type="text/javascript">
    function confirm_logout() 
    { 
        if (confirm("Are you sure you want to logout?")==true) 
            return true; 
        else 
            return false; 
    }
</script>

<div>
    <asp:LoginView ID="LoginView1" runat="server">
        <AnonymousTemplate>

        </AnonymousTemplate>
        <LoggedInTemplate>
            <div class="bottom_spacer">
                <asp:LoginName ID="LoginName1" runat="server" FormatString="Welcome, {0}! | " />
                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="/Default.aspx" Text="Home" ToolTip="Home"></asp:HyperLink>
                &nbsp;|&nbsp;
                <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged"></asp:DropDownList>
                &nbsp;|&nbsp;
                <asp:LoginStatus ID="LoginStatus1" runat="server" LogoutAction="Redirect" LogoutPageUrl="/Default.aspx" onclick="return confirm('Are you sure you want to log out?');" OnLoggedOut="LoginStatus1_LoggedOut" OnLoggingOut="LoginStatus1_LoggingOut" />
            </div>
        </LoggedInTemplate>
    </asp:LoginView>
</div>
