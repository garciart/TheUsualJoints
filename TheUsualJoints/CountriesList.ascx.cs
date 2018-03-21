using System;
using TheUsualJoints.App_Start;

namespace TheUsualJoints
{
    public partial class CountriesList : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataList1.DataSource = CatalogAccess.GetCountries(false);
            DataList1.DataBind();
        }
    }
}