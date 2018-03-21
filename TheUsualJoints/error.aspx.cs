using System;
using System.Web;

namespace TheUsualJoints
{
    public partial class error : TheUsualJoints.App_Start.basepage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Handle invalid requests where HTML encoding is passed in the querystring.
            HttpContext context = HttpContext.Current;
            context.RewritePath(context.Server.HtmlDecode(context.Request.Url.PathAndQuery));
            string error = (Request.QueryString["code"] == null) ? "0" : Request.QueryString["code"];
            // HtmlMeta metaDescription = new HtmlMeta();
            // metaDescription.Name = "description";
            switch (int.Parse(error))
            {
                case 400:
                    // 400 Bad Request
                    this.Title = "The Usual Joints | 400 Bad Request";
                    this.MetaDescription = "400 Bad Request: The request had bad syntax or was inherently impossible to be satisfied.";
                    // metaDescription.Content = "400 Bad Request: The request had bad syntax or was inherently impossible to be satisfied.";
                    // Header.Controls.Add(metaDescription);
                    Label1.Text = "400 Bad Request";
                    Label2.Text = "The request had bad syntax or was inherently impossible to be satisfied.";
                    break;
                case 401:
                    // 401 Unauthorized
                    this.Title = "The Usual Joints | 401 Unauthorized";
                    this.MetaDescription = "401 Unauthorized: The parameter to this message gives a specification of authorization schemes which are acceptable.";
                    // metaDescription.Content = "401 Unauthorized: The parameter to this message gives a specification of authorization schemes which are acceptable.";
                    // Header.Controls.Add(metaDescription);
                    Label1.Text = "401 Unauthorized";
                    Label2.Text = "The parameter to this message gives a specification of authorization schemes which are acceptable.";
                    break;
                case 403:
                    // 403 Forbidden
                    this.Title = "The Usual Joints | 403 Forbidden";
                    this.MetaDescription = "403 Forbidden: The request is for something forbidden. Authorization will not help.";
                    // metaDescription.Content = "403 Forbidden: The request is for something forbidden. Authorization will not help.";
                    // Header.Controls.Add(metaDescription);
                    Label1.Text = "403 Forbidden";
                    Label2.Text = "The request is for something forbidden. Authorization will not help.";
                    break;
                case 404:
                    // 404 Not Found
                    this.Title = "The Usual Joints | 404 Not Found";
                    this.MetaDescription = "404 Not Found: The server has not found anything matching the URI given.";
                    // metaDescription.Content = "404 Not Found: The server has not found anything matching the URI given.";
                    // Header.Controls.Add(metaDescription);
                    Label1.Text = "404 Not Found";
                    Label2.Text = "The server has not found anything matching the URI given.";
                    break;
                case 409:
                    // 409 Conflict
                    this.Title = "The Usual Joints | 409 Conflict";
                    this.MetaDescription = "409 Conflict: The request could not be completed due to a conflict with the current state of the resource.";
                    // metaDescription.Content = "409 Conflict: The request could not be completed due to a conflict with the current state of the resource.";
                    // Header.Controls.Add(metaDescription);
                    Label1.Text = "409 Conflict";
                    Label2.Text = "The request could not be completed due to a conflict with the current state of the resource.";
                    break;
                case 500:
                    // 500 Internal Server Error
                    this.Title = "The Usual Joints | 500 Internal Server Error";
                    this.MetaDescription = "500 Internal Server Error: The server encountered an unexpected condition which prevented it from fulfilling the request.";
                    // metaDescription.Content = "500 Internal Server Error: The server encountered an unexpected condition which prevented it from fulfilling the request.";
                    // Header.Controls.Add(metaDescription);
                    Label1.Text = "500 Internal Server Error";
                    Label2.Text = "The server encountered an unexpected condition which prevented it from fulfilling the request.";
                    break;
                case 501:
                    // 501 Not Implemented
                    this.Title = "The Usual Joints | 501 Not Implemented";
                    this.MetaDescription = "501 Not Implemented: The server does not support the functionality required to fulfill the request.";
                    // metaDescription.Content = "501 Not Implemented: The server does not support the functionality required to fulfill the request.";
                    // Header.Controls.Add(metaDescription);
                    Label1.Text = "501 Not Implemented";
                    Label2.Text = "The server does not support the functionality required to fulfill the request.";
                    break;
                default:
                    this.Title = "The Usual Joints | Unknown Error";
                    this.MetaDescription = "Unknown Error: We do not recognize HTTP Status Code.";
                    // metaDescription.Content = "Unknown Error: We do not recognize HTTP Status Code.";
                    // Header.Controls.Add(metaDescription);
                    Label1.Text = "Unknown Error";
                    Label2.Text = "We do not recognize HTTP Status Code, but we're working on it!";
                    break;
            }
        }
    }
}