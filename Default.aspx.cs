using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Microsoft.Practices.EnterpriseLibrary.Data;


namespace ScoreBoard
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Database db = DatabaseFactory.CreateDatabase("cnGrammit");
                DataSet ds = db.ExecuteDataSet("usp_Z_Score_getAllBoards", 10);

                rptNewBoards.DataSource = ds;
                rptNewBoards.DataBind();

                
            }
        }

        protected void rptNewBoards_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

        }
    }
}
