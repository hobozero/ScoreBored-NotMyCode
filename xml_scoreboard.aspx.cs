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
using System.Xml;

namespace BsAs
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bind();
            }
        }

        protected void rdoPro_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoPro.Checked)
            {
                rdoCon.Checked = false;
            }
        }

        protected void rdoCon_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoCon.Checked)
            {
                rdoPro.Checked = false;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            XmlDocument doc;
            if (rdoPro.Checked)
            {
                 doc = XmlDataSourcePro.GetXmlDocument();
                XmlNode node = doc.DocumentElement.FirstChild.Clone();
                node.Attributes["rate"].Value = Rating1.CurrentRating.ToString();
                node.Attributes["txt"].Value = txtEntry.Text;
                node.Attributes["type"].Value = "pro";
                doc.DocumentElement.AppendChild(node);
                XmlDataSourcePro.Save();
                grdPro.DataBind();
            }
            else
            {
                doc = XmlDataSourceCon.GetXmlDocument();
                XmlNode node = doc.DocumentElement.FirstChild.Clone();
                node.Attributes["rate"].Value = Rating1.CurrentRating.ToString();
                node.Attributes["txt"].Value = txtEntry.Text;
                node.Attributes["type"].Value = "con";
                doc.DocumentElement.AppendChild(node);
                XmlDataSourceCon.Save();
                grdCon.DataBind();
            }
            txtEntry.Text = string.Empty;

            Bind();

            
        }

        void Bind()
        {
            XmlDocument doc = XmlDataSourcePro.GetXmlDocument();
            int proPts = 0;
            
            foreach (XmlNode node in doc.SelectNodes("/ScoreBoard/Point[@type=\"pro\"]")){
                proPts += Convert.ToInt32(node.Attributes["rate"].Value);
            }
            int conPts = 0;
            foreach (XmlNode node in doc.SelectNodes("/ScoreBoard/Point[@type=\"con\"]")){
                conPts+= Convert.ToInt32(node.Attributes["rate"].Value);
            }
            string strStatus = (proPts > conPts) ? "rules" : "sucks";
            if (proPts == conPts)
            {
                strStatus = "is status quo";
            }

            int dif = System.Math.Abs(proPts - conPts);

            lblProPts.Text = proPts.ToString();
            lblconPts.Text = conPts.ToString();

            lblWinner.Text = string.Format("Buenos Aires currently {0} by {1} points", strStatus, dif.ToString());
        }
    }
}
