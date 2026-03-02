using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using localhost;

public partial class RequestArtist : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 1;
    }
    protected void Send_Click(object sender, ImageClickEventArgs e)
    {
        Service s = new Service();
        if ((string.Equals(TB_Artist_Name.Text, "") == false) && (string.Equals(TB_Artist_Name.Text, null) == false))
        {
            if (s.IsArtistNameInDB(TB_Artist_Name.Text) == true)
                VAL_Artist_Name.Visible = true;
            else
            {
                MultiView1.ActiveViewIndex = 0;

                s.InsertRequestArtist(DDL_Artist_Type.SelectedValue.ToString(),TB_Artist_Name.Text, TB_Artist_Bio.Text, TB_Pic.Text, "Add");

               // s.InsertRequestArtist("band",TB_Artist_Name.Text, "", "", "Add");

                Response.AddHeader("REFRESH", "3;URL=Default.aspx");
            }
        }
    }
}
