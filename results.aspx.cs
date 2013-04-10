using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace ScoreBoard
{
    public partial class results : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Database db = DatabaseFactory.CreateDatabase("cnGrammit");
            DataSet ds = db.ExecuteDataSet("usp_Z_searchBoards", Request["s"].URLDecodeToName(), 25);

            if (ds.Tables[0].Rows.Count > 0)
            {

                rptNewBoards.DataSource = ds;
                rptNewBoards.DataBind();

            }
            else
            {
                lblNoResults.Text = "No results found for search text <strong>" + Request["s"].URLDecodeToName() + "</strong>";
            }
        }
    }
}
