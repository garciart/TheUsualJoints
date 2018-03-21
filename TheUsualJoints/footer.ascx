<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="footer.ascx.cs" Inherits="TheUsualJoints.footer" %>

<div class="footer">
    <div class="footer_copy">Copyright <a href="/Account/Login.aspx" class="login_link">©</a> 2011-<%=DateTime.Now.Year.ToString()%> The Usual Joints® - All Rights Reserved.</div>
    <nav role="navigation" class="footer_nav">
        <a href="/disclaimer.aspx" class="footer_nav" title="Read This!">Read This!</a> - <a href="/about-us.aspx" class="footer_nav" title="About Us">About Us</a> - <a href="/privacy-policy.aspx" class="footer_nav" title="Privacy Policy">Privacy Policy</a> - <a href="/terms-of-use.aspx" class="footer_nav" title="Terms of Use">Terms of Use</a> - <a href="/contact-us.aspx" class="footer_nav" title="Contact Us!">Contact Us!</a> - <a href="/site-map.aspx" class="footer_nav" title="Site Map">Site Map</a>
    </nav>
</div>
