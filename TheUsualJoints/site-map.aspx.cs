using System;
using System.IO;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace TheUsualJoints
{
    public partial class site_map : TheUsualJoints.App_Start.basepage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                HtmlGenericControl ul = new HtmlGenericControl("ul");
                PlaceHolder1.Controls.Add(ul);

                DirectoryInfo Dir = new DirectoryInfo(System.Web.Hosting.HostingEnvironment.MapPath("~/"));
                FileInfo[] FileList = Dir.GetFiles("*.aspx", SearchOption.TopDirectoryOnly);
                foreach (FileInfo FI in FileList)
                {
                    HtmlAgilityPack.HtmlDocument htmlDoc = new HtmlAgilityPack.HtmlDocument();
                    htmlDoc.Load(FI.FullName);

                    if (htmlDoc.DocumentNode != null && (
                        FI.Name.ToLower().Contains("city") != true &&
                        FI.Name.ToLower().Contains("default") != true &&
                        FI.Name.ToLower().Contains("error") != true &&
                        FI.Name.ToLower().Contains("register") != true &&
                        FI.Name.ToLower().Contains("restaurant") != true &&
                        FI.Name.ToLower().Contains("site-map") != true))
                    {
                        HtmlGenericControl li = new HtmlGenericControl("li");
                        HyperLink HyperLink1 = new HyperLink();

                        if (htmlDoc.GetElementbyId("title_tag") != null)
                        {
                            HyperLink1.Text = htmlDoc.GetElementbyId("title_tag").InnerText.ToString();
                            HyperLink1.ToolTip = htmlDoc.GetElementbyId("title_tag").InnerText.ToString();
                            HyperLink1.NavigateUrl = "~/" + FI.Name;
                            li.Controls.Add(HyperLink1);
                            ul.Controls.Add(li);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
    }
}