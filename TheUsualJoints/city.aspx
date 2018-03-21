<%@ Page Title="" Language="C#" MasterPageFile="~/three-column.Master" AutoEventWireup="true" CodeBehind="city.aspx.cs" Inherits="TheUsualJoints.city" %>

<%@ Import Namespace="TheUsualJoints.App_Start" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="city_div1">
        <h1 id="title_tag" class="title_tag" runat="server"><asp:Label ID="city_Label1" runat="server" Text="Welcome to The Usual Joints!"></asp:Label></h1>
        <p><asp:HyperLink ID="city_HyperLink1" runat="server" ToolTip="Back to Home Page" NavigateUrl="/Default.aspx" Visible="false" Text="[Back to Home Page]"></asp:HyperLink></p>
        <h2><asp:Label ID="city_Label2" runat="server" Text="Looking for where to go or what to do tonight? Dinner to dancing: Check out our list of restaurants, clubs and bars below!"></asp:Label></h2>
        <hr />
        <div class="city_table">
        <asp:DataList ID="DataList1" runat="server" CssClass="city_DataList1" ItemStyle-VerticalAlign="Top" OnItemDataBound="DataList1_ItemDataBound">
            <ItemTemplate>
                <h2 class="ProductTitle">
                    <a href="<%# Link.ToRestaurant(Request.QueryString["CityID"], Eval("RestaurantID").ToString()) %>" title="Click here for more about <%# HttpUtility.HtmlEncode(Eval("RestaurantName").ToString()) %>!">
                        <%# HttpUtility.HtmlEncode(Eval("RestaurantName").ToString()) %>
                    </a>
                </h2>
                <table>
                    <tr class="city_table1_tr">
                        <td class="city_table1_image_td">
                            <a href="<%# Link.ToRestaurant(Request.QueryString["CityID"], Eval("RestaurantID").ToString()) %>" title="Click here for more about <%# HttpUtility.HtmlEncode(Eval("RestaurantName").ToString()) %>!">
                                <img id="city_img" runat="server" class="city_table1_image" src="/images/noimageloaded.png" alt="No Image Loaded" />
                            </a>
                        </td>
                        <td>
                            <div id="navcontainer" class="navcontainer">
                                <ul>
                                    <%#(String.IsNullOrEmpty(Eval("RestaurantMotto").ToString()) ? "" : String.Format("<li><span class=\"titles\">{0}</span><br /></li>", Eval("RestaurantMotto")))%>
                                    <%#(String.IsNullOrEmpty(Eval("HoursOfOperation").ToString()) ? "" : String.Format("<li><span class=\"titles\">Today's Hours:</span> {0}<br /></li>", Eval("HoursOfOperation")))%>
                                    <%#(String.IsNullOrEmpty(Eval("HappyHourTimes").ToString()) ? "" : String.Format("<li><span class=\"titles\">Happy Hour:</span> {0}<br /></li>", Eval("HappyHourTimes")))%>
                                    <%#(String.IsNullOrEmpty(Eval("FoodAndDrinkSpecials").ToString()) ? "" : String.Format("<li><span class=\"titles\">What to Eat:</span> {0}<br /></li>", Eval("FoodAndDrinkSpecials")))%>
                                    <%#(String.IsNullOrEmpty(Eval("RestaurantEvents").ToString()) ? "" : String.Format("<li><span class=\"titles\">What To Do:</span> {0}<br /></li>", Eval("RestaurantEvents")))%>
                                    <li><a href="<%# Link.ToRestaurant(Request.QueryString["CityID"], Eval("RestaurantID").ToString()) %>" title="Click here for more about <%# HttpUtility.HtmlEncode(Eval("RestaurantName").ToString()) %>!">Click here for more info & directions!</a></li>
                                </ul>
                            </div>
                        </td>
                    </tr>
                </table>
            </ItemTemplate>
        </asp:DataList>
        </div>
        <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
        <p><asp:HyperLink ID="city_HyperLink2" runat="server" ToolTip="Back to Home Page" NavigateUrl="/Default.aspx" Visible="false" Text="[Back to Home Page]"></asp:HyperLink></p>
        <hr />
        <p class="center_text"><em><strong><asp:Label ID="city_Label4" runat="server" CssClass="city_disclaimer" Text="Please remember that bars, clubs and restaurants may change their hours, events and specials without notice. Always check with your bartender or server for the most current information"></asp:Label></strong></em></p>
    </div>
</asp:Content>