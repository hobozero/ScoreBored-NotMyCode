using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace ScoreBoard
{
    public partial class embedded : System.Web.UI.Page
    {
        string name = string.Empty;
        string nameURL = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Database db = DatabaseFactory.CreateDatabase("cnGrammit");
                string nameParam = Request["n"];
                if (nameParam != null)
                {
                    name = nameParam.URLDecodeToName();

                    using (IDataReader rdr = db.ExecuteReader("usp_Z_Score_getEmbed", name))
                    {
                        if (rdr.Read())
                        {
                            ttl.Text = name;
                            desc.Text = rdr["DESCRIPTION"].ToString();
                            pro.Text = rdr["PRO"].ToString();
                            con.Text = rdr["CON"].ToString();

                        }
                        else
                        {
                            ttl.Text = "ScoreBored.net";
                            desc.Text = "Decisions are hard.  Stop making them.";
                            pro.Text = "67";
                            con.Text = "70";
                        }
                    }
                }
                else
                {
                    ttl.Text = "ScoreBored.net";
                    desc.Text = "Decisions are hard.  Stop making them.";
                    pro.Text = "67";
                    con.Text = "70";
                }
            }
        }


        public string GetUrl()
        {
            if (Request["n"] != null)
                return HttpRuntime.Cache["basePath"].ToString() + "bored/" + Request["n"].ToString().URLEncodeName();
            else
                return HttpRuntime.Cache["basePath"].ToString();

        }
    }
}
