<%@ Page Title="" Language="C#" MasterPageFile="~/two-column.Master" AutoEventWireup="true" CodeBehind="about-us.aspx.cs" Inherits="TheUsualJoints.about_us" %>

<%@ Register Src="~/share-bar.ascx" TagPrefix="uc1" TagName="sharebar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h1 id="title_tag" runat="server" class="two_col_title_tag" style="margin-top:0;">About Us!</h1>
    <p><a href="/Default.aspx" title="Back to Home Page">[Back to Home Page]</a></p>
    <h2>The True Story...</h2>
    <span>The Usual Joints started on a Sunday afternoon in Salisbury, Maryland when a group of friends and soon-to-be friends left the Market Street Inn and headed around the corner to the now-defunct Escape Bar. Upon arrival, the group found Escape to be closing and the subsequent stop, Flavors (now MoJo's), closed all together. Suddenly, instead of exploring a night pregnant with enjoyment and opportunity, the revelers now faced the cold, black void of not knowing where to go and what to do. Some went to Break Time; most went home. One went to meet others at Specific Gravity; while enjoying his Lucky 7 Porter and incensed at what he felt was a waste (it had been a gorgeous day and it was a gorgeous night), the seeds for our first site, 'What's Up, Salisbury!', were planted!</span>
    <p>So here we are! If you ever are wondering what to do or where to go on the Eastern Shore, check us out! Never let another beautiful day get away!</p>
    <hr class="clear" />
    <h2>About Rob...</h2>
    <img src="images/about-rob.png" alt="About Rob..." title="About Rob..." class="about_img" />
    <p>Sick and tired of going to a bar and finding out that they are closed, they don't serve EVO or that his favorite bands are playing some place else, Rob, whose other skills include making empanadas, drinking beer and replacing throttle positioning sensors, created this web site to a) increase the amount of time he and his friends could make asses out of themselves and b) to show that there ARE things to do in Salisbury, MD.</p>
    <hr class="clear" />
    <h2>About Stephen...</h2>
    <img src="images/about-steve.png" alt="About Stephen..." title="About Stephen..." class="about_img" />
    <p>While Steve is actually from Salisbury, MD, he's hanging out on the Western Shore and has not completed his "About Me" page.</p>
    <hr class="clear" />
    <uc1:sharebar runat="server" ID="sharebar" />
    <p><a href="/Default.aspx" title="Back to Home Page">[Back to Home Page]</a></p>
</asp:Content>