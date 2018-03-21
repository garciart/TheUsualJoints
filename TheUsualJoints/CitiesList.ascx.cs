using System;
using TheUsualJoints.App_Start;

namespace TheUsualJoints
{
    public partial class CitiesList : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Obtain the IDs of the selected country and state
            string countryId = Request.QueryString["CountryID"];
            string stateId = Request.QueryString["StateID"];
            // Continue only if CountryID and StateID exist in the query string
            if (countryId != null && stateId != null)
            {
                // Catalog.GetCitiesInState returns a DataTable
                // object containing city data, which is displayed by the DataList
                DataList1.DataSource = CatalogAccess.GetCitiesInState(stateId, false);
                // Needed to bind the data bound controls to the data source
                DataList1.DataBind();
            }
        }
    }
}