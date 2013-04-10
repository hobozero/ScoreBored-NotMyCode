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

namespace ScoreBoard.users
{
    public partial class _default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Database db = DatabaseFactory.CreateDatabase("cnGrammit");
                DataSet ds = db.ExecuteDataSet("usp_Z_Score_getMyBoards", Membership.GetUser().ProviderUserKey);


                if (ds.Tables[0].Rows.Count > 0)
                {
                    rptNewBoards.DataSource = ds;
                    rptNewBoards.DataBind();

                }
                else
                {
                    pnlHowTo.Visible = true;
                }


                


            }
        }
    }
}
