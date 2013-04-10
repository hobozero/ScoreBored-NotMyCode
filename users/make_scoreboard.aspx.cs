using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using MySql.Data.MySqlClient;
using System.Data;
using System.Web.Security;

namespace ScoreBoard
{
    public partial class make_scoreboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack){
                if (Request["edit"] != null)
                {
                    EditBoard(Request["edit"].ToString().URLDecodeToName());
                }
            }

        }

        protected void rdoType_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlMatch.Visible = (rdoType.SelectedValue != "1");
            rowName.Visible = pnlProCon.Visible = (rdoType.SelectedValue == "1");

            //no event arguments whn just being set due to edit
            if (e != null)
            {
                upForm.Update();
                upHead.Update();

                string script = string.Format("pcStatus=pcName=chkIs=isPreName=mStatus=cont1=cont2=preCont1=preCont2=null;");

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "proCon", script, true);
            }

            if (rdoType.SelectedValue == "1")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "startup", "proConPre(); ", true);
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "startup", "matchPre(); ", true);

            
        }

        protected void btnGo_Click(object sender, EventArgs e)
        {
            string cnString = WebConfigurationManager.ConnectionStrings["LocalMySqlServer"].ToString();
            ScoreBored scoreBored = CreateBored();

            MembershipUser user = Membership.GetUser();

            bool success = false;

            try
            {
                if (Request["edit"] != null)
                {
                    string oldName = Request["edit"].ToString().URLDecodeToName();
                    scoreBored.Update(oldName);
                    Cache.Remove(oldName);
                }
                else
                {
                    scoreBored.Save();
                }
                success = true;


            }
            catch (Exception ex)
            {
                lblFeedback.Text ="Sorry the name you chose is already taken.  Try a new name";
            }

            if (success)
                Response.Redirect("/bored/" + scoreBored.Name.URLEncodeName());
            
        //}
        }

        protected ScoreBored CreateBored()
        {
            ScoreBored scoreBored = null;

            MembershipUser user = Membership.GetUser();

            //Contestants should be broken into their own table

            string boardName = (rdoType.SelectedIndex == 0) ? 
                (txtName.Text.Trim() == string.Empty)? "[Board Name]" : txtName.Text.MakeName() :
                txtMatch1.Text + " vs " + txtMatch2.Text;
            

            scoreBored = new ScoreBored()
            {
                Name = boardName,
                Description = txtTheme.Text,
                UserId = user.ProviderUserKey.ToString(),
                ProName = (rdoType.SelectedIndex == 0)? "Pro" : (txtMatch1.Text.Trim() == string.Empty) ? "[Contestant1]" : txtMatch1.Text.Trim(),
                ConName= (rdoType.SelectedIndex == 0) ? "Con" : (txtMatch2.Text.Trim() == string.Empty) ? "[Contestant2]" : txtMatch2.Text.Trim(),
                 PhraseIs = chkIs.Checked,
                  Phrases = new Dictionary<short,string>()
            };

            //ProCon
            if (rdoType.SelectedIndex == 0)
            {
                scoreBored.Phrases.Add(-3, txtPro3.Text);
                scoreBored.Phrases.Add(-2, txtPro2.Text);
                scoreBored.Phrases.Add(-1, txtPro1.Text);

                scoreBored.Phrases.Add(0, this.txtConTie.Text);

                scoreBored.Phrases.Add(1, txtCon1.Text);
                scoreBored.Phrases.Add(2, txtCon2.Text);
                scoreBored.Phrases.Add(3, txtCon3.Text);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "startup", "proConPre(); ", true);
            }
            else
            {
                scoreBored.Phrases.Add(0, txtVStie.Text);
                scoreBored.Phrases.Add(1, txtVSphrase1.Text);
                scoreBored.Phrases.Add(2, txtVSphrase2.Text);
                scoreBored.Phrases.Add(3, txtVSphrase3.Text);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "startup", "matchPre(); ", true);
            }

            return scoreBored;
        }

        protected void EditBoard(string boardName)
        {
            
            ScoreBored board = ScoreBored.Load(boardName);

            MembershipUser user = Membership.GetUser();

            if (board != null && board.UserId == user.ProviderUserKey.ToString())
            {

                txtTheme.Text = board.Description;

                h1.InnerText = "Edit Board";
                editWarn.Text = "Note:  Edits to the name of a Pros and Cons List or the Competitor Names in a Tournament will change the URL for the board.  This will invalidate social networking bookmarks and embedded mini-boards";

                rdoType.SelectedIndex = (board.IsProCon) ? 0 : 1;
                rdoType_SelectedIndexChanged(this, null);

                if (board.IsProCon)
                {
                    txtName.Text = board.Name;
                    txtMatch1.Text = "Pro";
                    txtMatch2.Text = "Con";


                    txtPro3.Text = board.Phrases[-3];
                    txtPro2.Text = board.Phrases[-2]; 
                    txtPro1.Text = board.Phrases[-1];

                    txtCon1.Text = board.Phrases[1];
                    txtCon2.Text = board.Phrases[2];
                    txtCon3.Text = board.Phrases[3];

                    chkIs.Checked = board.PhraseIs;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "startup", "proConPre(); ", true);
                }
                else{
                    txtMatch1.Text = board.ProName;
                    txtMatch2.Text = board.ConName;

                    txtVSphrase1.Text = board.Phrases[1];
                    txtVSphrase2.Text = board.Phrases[2];
                    txtVSphrase3.Text = board.Phrases[3];
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "startup", "matchPre(); ", true);
                }

            }
            else
            {
                Response.Redirect(Request.AppRelativeCurrentExecutionFilePath);

            }
        }

    }
}
