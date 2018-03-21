using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TheUsualJoints.App_Start;

namespace TheUsualJoints
{
    public partial class test : TheUsualJoints.App_Start.basepage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                if (HttpContext.Current.User.IsInRole("Administrator"))
                {
                    Label1.Text = "You're an Administrator!";
                }
                else if (HttpContext.Current.User.IsInRole("Manager"))
                {
                    Label1.Text = "You're a Manager!";
                }
                else if (HttpContext.Current.User.IsInRole("Owner"))
                {
                    Label1.Text = "You're an Owner!";
                }
                else if (HttpContext.Current.User.IsInRole("Advertiser"))
                {
                    Label1.Text = "You're an Advertiser!";
                }
                else
                {
                    Label1.Text = "You don't belong here!";
                }
            }
            else
            {
                Label1.Text = "Why you ain't got no job?";
            }

            int x;
            for (x = 0; x <= 100; x++)
            {
                try
                {
                    RestaurantDetails rd = CatalogAccess.GetRestaurantDetails(x.ToString());
                    Panel1.Controls.Add(new LiteralControl(Utilities.CleanReplace(rd.RestaurantName, "-") + "<br />"));
                }
                catch
                {
                    continue;
                }
            }
        }
    }
}