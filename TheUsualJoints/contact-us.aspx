<%@ Page Title="" Language="C#" MasterPageFile="~/two-column.Master" AutoEventWireup="true" CodeBehind="contact-us.aspx.cs" Inherits="TheUsualJoints.contact_us" %>

<%@ Register Src="~/share-bar.ascx" TagPrefix="uc1" TagName="sharebar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h1 id="title_tag" runat="server" class="two_col_title_tag" style="margin-top:0;">Contact Us!</h1>
    <p><a href="/Default.aspx" title="Back to Home Page">[Back to Home Page]</a></p>
    <h2>What's on your mind?</h2>
    <ul>
        <li>Are you a local bar, club or venue that wants a listing on our site?</li>
        <li>Are you a band, DJ or entertainer who wants to let people know about a local upcoming show?</li>
        <li>Are you a local non-political, non-religious or non-discriminatory organization that wants to promote an event or have a link on our site?</li>
        <li>Are you a business that wants to reach our demographic?</li>
        <li>Or do you just want to send us a message or a suggestion?</li>
    </ul>
    <p>Contact us! We started The Usual Joints to know where to go and what to do on the Eastern Shore and we want you to know too!</p>
    <p>This is a free service for most venues and entertainers. If you want to advertise on our site, take a look at our <a href="media-kit.pdf" target="_blank" title="Our Media Kit">media kit</a> (You will need <a href="http://get.adobe.com/reader/" target="_blank" title="Click here to get Adobe® Reader®">Adobe® Reader®</a>) or send us a message!</p>
    <uc1:sharebar runat="server" ID="sharebar" />
    <br />
    <hr />
    <h2>And visit all our pages!</h2>
    <div class="center_text">
        <div>
            <p>
                <a href="http://www.whatsupsalisbury.com" title="Salisbury, Delmar, Fruitland and more!"><img src="images/clear-salisbury-md-us.png" alt="Salisbury, Delmar, Fruitland and more!" class="contact_link_img" /></a>
            </p>
            <p>
                <img alt="Links!" src="images/social-media-bar-inline.png" usemap="#salisbury_social_media_bar_map" height="32" width="146" />
                <map name="salisbury_social_media_bar_map">
                    <area shape="rect" coords="0,0,32,32" href="http://www.facebook.com/whatsupsalisbury" target="_blank" title="Follow us on Facebook" alt="Facebook" />
                    <area shape="rect" coords="38,0,70,32" href="http://twitter.com/#!/whatsupsalisbur" target="_blank" title="Follow us on Twitter" alt="Twitter" />
                    <area shape="rect" coords="76,0,108,32" href="http://plus.google.com/104022033668409459037" target="_blank" title="Follow us on Google+" alt="Google+" />
                    <area shape="rect" coords="114,0,146,32" href="mailto:info@whatsupsalisbury.com" target="_self" title="Email Us!" alt="Email Us!" />
                </map>
            </p>
        </div>
        <hr class="clear" />
        <div>
            <p>
                <a href="http://www.whatsuprehoboth.com" title="Rehoboth Beach, Dewey Beach, Lewes and more!"><img src="images/clear-rehoboth-beach-de-us.png" alt="Rehoboth Beach, Dewey Beach, Lewes and more!" class="contact_link_img" /></a>
            </p>
            <p>
                <img alt="Links!" src="images/social-media-bar-inline.png" usemap="#rehoboth_social_media_bar_map" height="32" width="146" />
                <map name="rehoboth_social_media_bar_map">
                    <area shape="rect" coords="0,0,32,32" href="http://www.facebook.com/whatsuprehoboth" target="_blank" title="Follow us on Facebook" alt="Facebook" />
                    <area shape="rect" coords="38,0,70,32" href="http://twitter.com/#!/whatsuprehoboth" target="_blank" title="Follow us on Twitter" alt="Twitter" />
                    <area shape="rect" coords="76,0,108,32" href="http://plus.google.com/107795644957862039428" target="_blank" title="Follow us on Google+" alt="Google+" />
                    <area shape="rect" coords="114,0,146,32" href="mailto:info@whatsuprehoboth.com" target="_self" title="Email Us!" alt="Email Us!" />
                </map>
            </p>
        </div>
        <hr class="clear" />
        <div>
            <p>
                <a href="http://www.whatsupocmd.com" title="Ocean City, Berlin and more!"><img src="images/clear-ocean-city-md-us.png" alt="Ocean City, Berlin and more!" class="contact_link_img" /></a>
            </p>
            <p>
                <img alt="Links!" src="images/social-media-bar-inline.png" usemap="#ocmd_social_media_bar_map" height="32" width="146" />
                <map name="ocmd_social_media_bar_map">
                    <area shape="rect" coords="0,0,32,32" href="http://www.facebook.com/whatsupocmd" target="_blank" title="Follow us on Facebook" alt="Facebook" />
                    <area shape="rect" coords="38,0,70,32" href="http://twitter.com/#!/whatsupocmd" target="_blank" title="Follow us on Twitter" alt="Twitter" />
                    <area shape="rect" coords="76,0,108,32" href="http://plus.google.com/116555923897660775551" target="_blank" title="Follow us on Google+" alt="Google+" />
                    <area shape="rect" coords="114,0,146,32" href="mailto:info@whatsupocmd.com" target="_self" title="Email Us!" alt="Email Us!" />
                </map>
            </p>

        </div>
        <br />
    </div>
    <a href="/Default.aspx" title="Back to Home Page">[Back to Home Page]</a>
</asp:Content>