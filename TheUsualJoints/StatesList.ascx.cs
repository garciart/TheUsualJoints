using System;
using TheUsualJoints.App_Start;

namespace TheUsualJoints
{
    public partial class StatesList : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Obtain the ID of the selected country
            string countryId = Request.QueryString["CountryID"];
            // Continue only if CountryID exists in the query string
            if (countryId != null)
            {
                // Catalog.GetStatesInCountry returns a DataTable
                // object containing state data, which is displayed by the DataList
                DataList1.DataSource = CatalogAccess.GetStatesInCountry(countryId, false);
                // Needed to bind the data bound controls to the data source
                DataList1.DataBind();
            }
        }
    }
}