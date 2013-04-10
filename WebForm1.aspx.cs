using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;

namespace ScoreBoard
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ModalPopupExtender mpeScore = new ModalPopupExtender()
            {
                ID = "mpeSc",
                PopupControlID = popScore.ClientID,
                TargetControlID = btnVote.ClientID,
                BackgroundCssClass = "modalBackground",
                BehaviorID = "mpeScore"
            };
            Page.Form.Controls.Add(mpeScore);
        }
    }
}
