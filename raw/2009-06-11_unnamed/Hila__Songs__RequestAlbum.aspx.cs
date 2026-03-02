using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using localhost;

public partial class RequestAlbum : System.Web.UI.Page
{

    bool ArtistNotInDB;// 0 if Is in DB, else 1

    protected void Page_Load(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 1;
        Validator_Artist.Visible = ((LB_Artists.SelectedItem == null) && (TB_Other_Artist.Text == "")) ? true : false;  

        if (!IsPostBack)
        {
            ArtistNotInDB = true;
            // Show list of artists
            Service s = new Service();
            DataTable dt = s.ArtistNameLike("");

            foreach (DataRow dr in dt.Rows)
            {
                ListItem li = new ListItem();
                li.Value = dr[0].ToString();
                li.Text = dr[1].ToString();
                LB_Artists.Items.Add(li);
            }

            for (int i = 1900; i < 2009; i++)
            {
                ListItem li = new ListItem();
                li.Value = i.ToString();
                DDL_Album_Year.Items.Add(li);
            }
        }
    }
    protected void TB_Artist_Search_TextChanged(object sender, EventArgs e)
    {
        LB_Artists.Items.Clear();
        string str = TB_Artist_Search.Text;

        //PanelAddSong.Visible = true;
        Service s = new Service();

        DataTable dt = s.ArtistNameLike(str);

        foreach (DataRow dr in dt.Rows)
        {
            ListItem li = new ListItem();
            li.Value = dr[0].ToString();
            li.Text = dr[1].ToString();
            LB_Artists.Items.Add(li);
        }
        LB_Artists.DataBind();
    }
    protected void Send_Click(object sender, ImageClickEventArgs e)
    {
        string Artist_Name;
        int Artist_ID;
        int Album_Year;

        Service s = new Service();

        if ((string.Equals(TB_Other_Artist.Text, "") == false) || (LB_Artists.SelectedItem!=null))
        {
            if (ArtistNotInDB == true)
            {
                Artist_Name = TB_Other_Artist.Text;
                Artist_ID = 0;
                s.InsertRequestArtist("", TB_Other_Artist.Text, "", "", "Add");
            }
            else
            {
                Artist_Name = LB_Artists.SelectedItem.Text;
                Artist_ID = int.Parse(LB_Artists.SelectedItem.Value);
            }

            if ( DDL_Album_Year.SelectedValue == null )
                Album_Year = 0;
            else Album_Year = int.Parse(DDL_Album_Year.SelectedValue.ToString());

            MultiView1.ActiveViewIndex = 0;

            s.InsertRequestAlbum(TB_Album_Name.Text, Artist_ID, Artist_Name, TB_Pic.Text, Album_Year, "Add");
            
            Response.AddHeader("REFRESH", "3;URL=Default.aspx");
        }
    }

    protected void LB_Artists_SelectedIndexChanged(object sender, EventArgs e)
    {
        ArtistNotInDB = false;
    }

    protected void TB_Other_Artist_TextChanged(object sender, EventArgs e)
    {
        ArtistNotInDB = true;
    }
    protected void LB_Artists_TextChanged(object sender, EventArgs e)
    {
        ArtistNotInDB = false;
    }
}
