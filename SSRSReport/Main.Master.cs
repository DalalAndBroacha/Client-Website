using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


namespace SSRSReport
{
    public partial class Main : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            

        }
        protected void NavigationMenu_MenuItemClick(Object sender, MenuEventArgs e)
        {
            string winStatus = "";
            string[] words = e.Item.Value.Split(',');
            if (words.Length > 0)
            {
                Session["NavigationId"] = words[1];
                winStatus = words[0];
                if (winStatus.ToString().Equals("1"))
                {
                    string url = words[2];
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "OpenWin", "<script>openNewWin('" + url + "')</script>");
                }
                else
                {
                    Response.Redirect(words[2]);
                }
            }
        }
    }
}