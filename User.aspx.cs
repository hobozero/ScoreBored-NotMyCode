using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data;
using System.Web.Security;

namespace ScoreBoard
{
    public partial class User : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && Request["u"] != null)
            {

                MembershipUser user = Membership.GetUser(Request["u"].ToString());

                if (user != null){

                    Database db = DatabaseFactory.CreateDatabase("cnGrammit");
                    DataSet ds = db.ExecuteDataSet("usp_Z_Score_getMyBoards", user.ProviderUserKey);

                    lblUser.Text = user.UserName;

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        rptNewBoards.DataSource = ds;
                        rptNewBoards.DataBind();

                    }
                }
            }
        }
    }
}
