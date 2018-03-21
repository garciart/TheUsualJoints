<%@ Page Title="" Language="C#" MasterPageFile="~/three-column.Master" AutoEventWireup="true" CodeBehind="restaurant.aspx.cs" Inherits="TheUsualJoints.restaurant" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="/lightbox/js/jquery-1.11.0.min.js"></script>
	<script src="/lightbox/js/lightbox.min.js"></script>
    <link href="/lightbox/css/lightbox.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="restaurant_div1">
        <h1 id="title_tag" runat="server" class="title_tag"><asp:Label ID="restaurant_Label1" runat="server" Text="Welcome to The Usual Joints!"></asp:Label></h1>
        <p><asp:HyperLink ID="restaurant_HyperLink1" runat="server" ToolTip="Back to Home Page" NavigateUrl="/Default.aspx" Visible="false" Text="[Back to Home Page]"></asp:HyperLink></p>
        <div class="restaurant_div2">
            <div class="restaurant_div3">
                <div class="restaurant_img1_div">
                    <img id="restaurant_img1" runat="server" src="images/noimageloaded.png" alt="No Image Loaded" class="restaurant_img1" />
                </div>
                <br />
                <i><asp:Label ID="restaurant_Label2" runat="server"></asp:Label></i>
                <asp:Label ID="restaurant_Label3" runat="server"></asp:Label>
                <h2>Address:</h2>
                <asp:Label ID="restaurant_Label4" runat="server"></asp:Label>
                <asp:Label ID="restaurant_Label5" runat="server"></asp:Label>
                <br />
            </div>
            <div class="restaurant_div4">
                <a id="restaurant_a1" runat="server" href="https://maps.google.com" target="_blank">
                    <asp:Image ID="restaurant_Image1" runat="server" CssClass="restaurant_Image1" ImageUrl="~/images/noimageloaded.png" />
                </a>
            </div>
        </div>
        <h2 class="clear">Contact Info:</h2>
        <asp:Label ID="restaurant_Label6" runat="server"></asp:Label>
        <asp:Label ID="restaurant_Label7" runat="server"></asp:Label>
        <asp:Label ID="restaurant_Label8" runat="server"></asp:Label>
        <asp:Label ID="restaurant_Label9" runat="server"></asp:Label>
        <asp:Label ID="restaurant_Label10" runat="server"></asp:Label>
        <asp:Label ID="restaurant_Label11" runat="server"></asp:Label>
        <asp:Label ID="restaurant_Label12" runat="server"></asp:Label>
        <asp:Label ID="restaurant_Label13" runat="server"></asp:Label>
        <asp:Label ID="restaurant_Label14" runat="server"></asp:Label>
        <asp:Label ID="restaurant_Label15" runat="server"></asp:Label>
        <p>
            <asp:Panel ID="Panel1" runat="server"></asp:Panel>
        </p>
        <h2>Weekly Schedule:</h2>
        <div class="background_pic">
            <asp:DataList ID="DataList1" runat="server" CssClass="restaurant_schedule">
                <ItemTemplate>
                    <h3 class="schedule_titles">&nbsp;<i><%# Enum.Parse(typeof(DayOfWeek), Eval("Weekday").ToString()) %>:</i></h3>
                    <p>
                        <ul>
                            <%#(String.IsNullOrEmpty(Eval("HoursOfOperation").ToString()) ? "" : String.Format("<li><span class=\"schedule_titles\">Today's Hours:</span> {0}<br /></li>", Eval("HoursOfOperation")))%>
                            <%#(String.IsNullOrEmpty(Eval("HappyHourTimes").ToString()) ? "" : String.Format("<li><span class=\"schedule_titles\">Happy Hour:</span> {0}<br /></li>", Eval("HappyHourTimes")))%>
                            <%#(String.IsNullOrEmpty(Eval("FoodAndDrinkSpecials").ToString()) ? "" : String.Format("<li><span class=\"schedule_titles\">What to Eat:</span> {0}<br /></li>", Eval("FoodAndDrinkSpecials")))%>
                            <%#(String.IsNullOrEmpty(Eval("RestaurantEvents").ToString()) ? "" : String.Format("<li><span class=\"schedule_titles\">What To Do:</span> {0}<br /></li>", Eval("RestaurantEvents")))%>
                        </ul>
                    </p>
                </ItemTemplate>
                <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
            </asp:DataList>
        </div>
        <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
        <p><asp:HyperLink ID="restaurant_HyperLink2" runat="server" ToolTip="Back to Home Page" NavigateUrl="/Default.aspx" Visible="false" Text="[Back to Home Page]"></asp:HyperLink></p>
        <hr />
        <p class="center_text"><em><strong><asp:Label ID="restaurant_Label20" runat="server" Text="Please remember that restaurants and bars may change their information, hours, entertainment and specials without notice. For the most current information, contact the restaurant or bar using the phone number or email listed above."></asp:Label></strong></em></p>
        <!--Load scripts after page-->
        <script type="text/javascript" src="http://www.fandango.com/affiliatewidget_srchban_728x90.js?a=3469553&r=603_479917" defer="defer"></script>
    </div>
</asp:Content>