using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Global.EasyBool = false;
        Global.NormBool = false;
        Global.HardBool = false;
        Global.GnlBool = false;
        Global.PrgBool = false;
        Global.VgmBool = false;
    }


    protected void prgBtn_Click(object sender, EventArgs e)
    {
        Global.PrgBool = true;
        Server.Transfer("Game.aspx");
    }

    protected void gnlBtn_Click(object sender, EventArgs e)
    {
        Global.GnlBool = true;
        Server.Transfer("Game.aspx");
    }

    protected void vgmBtn_Click(object sender, EventArgs e)
    {
        Global.VgmBool = true;
        Server.Transfer("Game.aspx");
    }

    protected void easyTick_CheckedChanged(object sender, EventArgs e)
    {
        Global.EasyBool = true;
        Global.NormBool = false;
        Global.HardBool = false;
    }

    protected void normTick_CheckedChanged(object sender, EventArgs e)
    {
        Global.EasyBool = false;
        Global.NormBool = true;
        Global.HardBool = false;
    }

    protected void hardTick_CheckedChanged(object sender, EventArgs e)
    {
        Global.EasyBool = false;
        Global.NormBool = false;
        Global.HardBool = true;
    }
}