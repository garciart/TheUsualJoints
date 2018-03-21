using TheUsualJoints.App_Start;
using System;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;

namespace TheUsualJoints.Admin
{
    public partial class test_admin : TheUsualJoints.App_Start.basepage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Load the grid only the first time the page is loaded
            if (!Page.IsPostBack)
            {
                // Load the Test Subject Grid
                BindGrid();
            }
        }

        // Populate the GridView with data
        private void BindGrid()
        {
            // CatalogAccess.GetTestSubjects returns a DataTable object containing test data, which is read into the GridView
            // Set SubjectActive to false for all data
            GridView1.DataSource = CatalogAccess.GetTestSubjects(false);
            // Bind the data bound controls to the data source
            GridView1.DataBind();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            int nameID = 0;
            if (CatalogAccess.AddTestSubject(TextBox1.Text, TextBox2.Text, TextBox4.Text, TextBox3.Text, CheckBox1.Checked, out nameID) == true)
            {
                Label6.Text = String.Format("Yay! Test Name Added! The ID is {0}.", nameID);
                Label6.ForeColor = System.Drawing.Color.Green;
                Label6.Visible = true;
            }
            else
            {
                Label6.Text = "Oops! Something Went Wrong!";
                Label6.ForeColor = System.Drawing.Color.Red;
                Label6.Visible = true;
            }
        }

        protected void TextBox3_TextChanged(object sender, EventArgs e)
        {
            TextBox4.Text = (DateTime.Now.Year - DateTime.Parse(TextBox3.Text).Year).ToString();
        }

        // enter edit mode
        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            // Set the row for which to enable edit mode
            GridView1.EditIndex = e.NewEditIndex;
            // Set status message
            Label6.Text = String.Format("Editing row #{0}...", (e.NewEditIndex + 1).ToString());
            // Load the Test Subject Grid
            BindGrid();
        }

        // Cancel edit mode
        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            // Cancel edit mode
            GridView1.EditIndex = -1;
            // Set status message
            Label6.Text = "Editing canceled";
            // Load the Test Subject Grid
            BindGrid();
        }

        // Update row
        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            // Retrieve updated data
            int testID = int.Parse(GridView1.DataKeys[e.RowIndex].Value.ToString());
            string lastName = ((TextBox)GridView1.Rows[e.RowIndex].Cells[0].Controls[0]).Text;
            string firstName = ((TextBox)GridView1.Rows[e.RowIndex].Cells[1].Controls[0]).Text;
            DateTime birthDate = DateTime.Parse(((TextBox)GridView1.Rows[e.RowIndex].Cells[2].Controls[0]).Text);
            int age = (int)((DateTime.Now - birthDate).Days / 365.242);
            bool active = ((CheckBox)GridView1.Rows[e.RowIndex].Cells[4].Controls[0].FindControl("CheckBox1")).Checked;
            // Execute the update command
            bool success = CatalogAccess.UpdateTestSubject(testID.ToString(), lastName.ToString(), firstName.ToString(), birthDate.ToString(), age.ToString(), active);
            // Cancel edit mode
            GridView1.EditIndex = -1;
            // Display status message
            Label6.Text = success ? "Update successful" : "Update failed";
            // Reload the grid
            BindGrid();
        }

        // Delete a record
        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            // Get the ID of the record to be deleted
            int testID = int.Parse(GridView1.DataKeys[e.RowIndex].Value.ToString());
            // Execute the delete command
            bool success = CatalogAccess.DeleteTestSubject(testID.ToString());
            // Cancel edit mode
            GridView1.EditIndex = -1;
            // Display status message
            Label6.Text = success ? "Delete successful" : "Delete failed";
            // Reload the grid
            BindGrid();
        }
    }
}