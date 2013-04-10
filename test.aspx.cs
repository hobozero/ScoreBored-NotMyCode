using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;

namespace ScoreBoard
{
    public partial class test1 : System.Web.UI.Page
    {

        protected void Page_Init(object sender, EventArgs e)
        {
                
        }

protected void Page_Load(object sender, EventArgs e)
{
    ModalPopupExtender mpeScore = new ModalPopupExtender()
    {
        ID = "mpeSc",
        PopupControlID = popScore.ClientID,
        TargetControlID = "btnVote",
        BackgroundCssClass = "modalBackground",
        BehaviorID = "mpeScore"
    };

    mpeScore.ResolveControlID += mpe_ResolveControlID;

    Page.Form.Controls.Add(mpeScore);
}

protected void mpe_ResolveControlID(object sender, AjaxControlToolkit.ResolveControlEventArgs e)
{
    // The Ajax stuff needs to be inside a different ContentPlaceHolder on the master page because otherwise the master page CSS breaks it. 
    // The default Control Id resolution in ExtenderControlBase fails to find cmdDefer because it is contained inside a different ContentPlaceHolder 
    // So they raise this event for me and I resolve it for them. 
    switch (e.ControlID)
    {
        case "btnVote":
            e.Control = btnVote;
            break;
    }
}


    }
}
