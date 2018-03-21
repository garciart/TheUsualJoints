using System;
using System.IO;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace TheUsualJoints.App_Start
{
    /// <summary>
    /// Summary description for basepage
    /// </summary>
    public class basepage : System.Web.UI.Page
    {
        public basepage()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        protected override void OnPreInit(EventArgs e)
        {
            HttpCapabilitiesBase browser = Request.Browser;

            if (Utilities.isMobile() != true && browser.IsMobileDevice != true)
            {
                Page.Theme = "desktop";
                // social_media_links.Visible = true;
            }
            else
            {
                Page.Theme = "mobile";
                if (!this.Page.ToString().Contains("default"))
                {
                    Page.MasterPageFile = Page.ResolveUrl("mobile.master");
                }
                // social_media_links.Visible = false;
            }
            base.OnPreInit(e);
        }

        protected override void OnInit(EventArgs e)
        {
            this.Page.Header.Controls.Add(new LiteralControl(String.Format("<meta name=\"copyright\" content=\"Copyright © 2011-{0} The Usual Joints® - All Rights Reserved\" />\n", DateTime.Now.Year.ToString())));
            this.Page.Header.Controls.Add(new LiteralControl(String.Format("<meta name=\"dcterms.rightsHolder\" content=\"Copyright © 2011-{0} The Usual Joints® - All Rights Reserved\" />\n", DateTime.Now.Year.ToString())));
            // Use DataBind() to allow <%#ResolveUrl("...")%> to work
            Page.Header.DataBind();
            // Page.DataBind();
            base.OnInit(e);
        }

        protected override void OnLoad(EventArgs e)
        {
            try
            {
                string pagename = Path.GetFileName(Request.Url.AbsolutePath);

                if (pagename.ToLower().Contains("default") != true)
                {
                    HtmlAgilityPack.HtmlDocument htmlDoc = new HtmlAgilityPack.HtmlDocument();
                    htmlDoc.Load(System.Web.Hosting.HostingEnvironment.MapPath("~/") + pagename + ".aspx");
                    if (htmlDoc.GetElementbyId("title_tag") != null)
                    {
                        this.Title = htmlDoc.GetElementbyId("title_tag").InnerText.ToString() + " - The Usual Joints";
                        this.MetaDescription = htmlDoc.GetElementbyId("title_tag").InnerText.ToString() + " - Questions? Email us or follow The Usual Joints on Facebook, Twitter and Google+ to find out what to do tonight and more!";
                        this.MetaKeywords = htmlDoc.GetElementbyId("title_tag").InnerText.ToString() + ", The Usual Joints";
                    }

                    /*
                    HtmlAgilityPack.HtmlNode node = htmlDoc.DocumentNode.SelectSingleNode("//*[self::h1]");
                    this.Title = node.ChildNodes[0].InnerHtml.ToString() + " - The Usual Joints";
                    this.MetaDescription = node.ChildNodes[0].InnerHtml.ToString() + " - Questions? Email us or follow The Usual Joints on Facebook, Twitter and Google+ to find out what to do tonight and more!";
                    this.MetaKeywords = node.ChildNodes[0].InnerHtml.ToString() + ", The Usual Joints";
                    */
                }
            }
            catch
            {

            }
            base.OnLoad(e);
        }

        /// <summary
        /// Used to move viewstate info to bottom of page
        /// Thanks, Scott Hanselman at http://www.hanselman.com/blog/movingviewstatetothebottomofthepage.aspx
        /// </summary>
        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            System.IO.StringWriter stringWriter = new System.IO.StringWriter();
            HtmlTextWriter htmlWriter = new HtmlTextWriter(stringWriter);
            base.Render(htmlWriter);
            string html = stringWriter.ToString();
            int StartPoint = html.IndexOf("<input type=\"hidden\" name=\"__VIEWSTATE\"");
            if (StartPoint >= 0)
            {
                int EndPoint = html.IndexOf("/>", StartPoint) + 2;
                string viewstateInput = html.Substring(StartPoint, EndPoint - StartPoint);
                html = html.Remove(StartPoint, EndPoint - StartPoint);
                int FormEndStart = html.IndexOf("</form>");
                if (FormEndStart >= 0)
                {
                    html = html.Insert(FormEndStart, viewstateInput);
                }
            }
            writer.Write(html);
        }

        public static class ClientMessageBox
        {
            public static void Show(string message, Control owner)
            {
                Page page = (owner as Page) ?? owner.Page;
                if (page == null) return;
                page.ClientScript.RegisterStartupScript(owner.GetType(),
                    "ShowMessage", string.Format("<script type='text/javascript'>alert('{0}')</script>",
                    message));
            }
        }
    }
}