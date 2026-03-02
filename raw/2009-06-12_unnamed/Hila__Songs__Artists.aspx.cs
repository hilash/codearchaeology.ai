using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using localhost;


public partial class Artists : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Service s = new Service();
        Artists_Pop.DataSource = s.ArtistPopTable();
        Artists_Pop.DataBind();

    }
}
