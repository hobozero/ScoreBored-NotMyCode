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
using System.Web.Configuration;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using AjaxControlToolkit;

namespace ScoreBoard
{
    public partial class DynamicScoreboard : System.Web.UI.Page
    {
        /// <summary>
        /// Loaded with LoadBored()
        /// </summary>
        public ScoreBored _board = null;
        MembershipUser user = null;

        protected void Page_Init(object sender, EventArgs e)
        {
            user = Membership.GetUser();
            if (user != null)
            {

                //<cc1:ModalPopupExtender ID="mpeSc" runat="server" PopupControlID="popScore" CancelControlID="cancelVote"
                //TargetControlID="btnVote" BackgroundCssClass="modalBackground" BehaviorID="mpeVote" />
                popScore.Visible = true;
                ModalPopupExtender mpeScore = new ModalPopupExtender()
                {
                    ID = "mpeSc",
                    PopupControlID = popScore.ClientID,
                    CancelControlID = cancelVote.ClientID,
                    TargetControlID = btnVote.ID,   //will note resolve, done in exception
                    BackgroundCssClass = "modalBackground",
                    BehaviorID = "mpeScore"
                };
                mpeScore.ResolveControlID += mpe_ResolveControlID;
                this.Form.Controls.Add(mpeScore);


                //<cc1:PopupControlExtender ID="PCECom" runat="server" BehaviorID="comPopBhr" Position="Left" OffsetX="-250" OffsetY="38" 
                //PopupControlID="pnlVote" TargetControlID="btnComDummy" />
                pnlVote.Visible = true;
                PopupControlExtender popVote = new PopupControlExtender()
                {
                    ID = "PCECom",
                    PopupControlID = pnlVote.ClientID,
                    TargetControlID = btnComDummy.ID,   //will note resolve, done in exception
                    BehaviorID = "comPopBhr",
                    Position = PopupControlPopupPosition.Left,
                    OffsetX = -250,
                    OffsetY = 38

                };
                popVote.ResolveControlID += mpe_ResolveControlID;
                upComs.ContentTemplateContainer.Controls.Add(popVote);
            }
            else
            {
                btnVote.Attributes.Add("onclick", "$find('mpbSignup').show();");

            }

            if (!IsPostBack)
            {
                if (Request["cat"] != null)
                {
                    bool loaded = BindAll();
                    LoadEmbed();

                    if (user != null && loaded && _board.UserId == user.ProviderUserKey.ToString())
                    {
                        lnkEdit.Visible = true;
                        lnkEdit.InnerText = "Edit";
                        lnkEdit.HRef = "/users/make_scoreboard.aspx?edit=" + Request["cat"];
                    }
                }
            }
            else
            {
                //might be partial postback of comment deleted.  Check for commandNAme of "delCom"

            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public string GetColumnHeader(bool isPro)
        {
            return (isPro) ? _board.ProName : _board.ConName;
        }

        public string GetScore(bool isPro)
        {
            if (isPro)
                return _board.ProTotal.ToString();
            else
                return _board.ConTotal.ToString();
        }

        
        protected void Login1_LoginError(object sender, EventArgs e)
        {
            System.Web.UI.WebControls.Login login1 = sender as System.Web.UI.WebControls.Login;
            login1.FailureText = "FAIL.  Please try again or <a href='" +
                this.ResolveUrl("~/RecoverPassword.aspx") + "'>reset</a> your password";

            this.MPESignup.Show();
        }


        #region button clicking
        protected void btnVote_Click(object sender, EventArgs e)
        {
            if (User.Identity.IsAuthenticated)
            {
                LoadBored();

                Score rating = new Score()
                {
                    Created = DateTime.UtcNow,
                    RatingRaw = int.Parse(Rating1.CurrentRating.ToString()),
                    Text = txtEntry.Text,
                    UserId = Membership.GetUser().ProviderUserKey.ToString(),
                     Votes = new List<Vote>(1),
                      IsPro = (sender == btnPro)
                    
                };
                _board.AddScore(rating);

                BindAll();

                txtEntry.Text = string.Empty;

                upScore.Update();
                upBoards.Update();
                upComs.Update();
            }
            else
            {
                MPESignup.Show();
                Rating1.CurrentRating = 1;
            }
        }

        protected void comment_Click(object sender, EventArgs e)
        {
            if (User.Identity.IsAuthenticated)
            {
                MembershipUser user = Membership.GetUser();

                //ImageButton button = sender as ImageButton;
                //string upDown = button.CommandArgument;
                LoadBored();

                short weight = Convert.ToInt16(this.comWt.Value);
                //could be the vote or score ID depending on weight value
                int sourceID = Convert.ToInt32(comCd.Value);

                //A weight of 100 is a reply to
                if (weight == 100)
                {
                    _board.ReplyToVote(sourceID, Membership.GetUser(), txtComment.Text);
                }
                else{

                    Score score = _board.FindScore(true, sourceID);
                    if (score == null)
                        score = _board.FindScore(false, sourceID);

                    if (score != null){
                        score.AddVote(user, txtComment.Text, weight);
                    }
                }

                if (weight != 0 && weight < 100)
                {
                    BindAll();
                    upOut.Update();
                    
                }
                else
                {
                    BindComments();
                    upOut.Update();
                }
            }
            else
            {
                MPESignup.Show();
            }

            txtComment.Text = string.Empty;
            upComBox.Update();
        }
        #endregion

        #region Binding
        protected bool BindAll()
        {
            user = Membership.GetUser();

            LoadBored();

            if (_board == null)
            {
                this.Header.Title = "Scoreboard Not Found";
                lblBoardName.Text = "No such scoreboard found";
                lblDesc.Text = "Try searching for it";
                lblStatus.Text = "Tilt Tilt Tilt";
                return false;
            }

            lCreated.Text = " by <a href='../user.aspx?u=" + _board.UserName + "'>" + _board.UserName + "</a>";

            this.Header.Title = _board.Name + 
                ((_board.IsProCon)? ((_board.Name.EndsWith("?")? string.Empty : ":" ))+ "  Weighing the Pros and Cons" : string.Empty) + 
                " - " + _board.Description;

            lblBoardName.Text = _board.Name;
            lblDesc.Text = _board.Description;
            lblStatus.Text = _board.GetStatus();

            
            grdPro.DataSource = _board.Pros;
            grdPro.DataBind();
            grdCon.DataSource = _board.Cons;
            grdCon.DataBind();

            if (ScriptManager.GetCurrent(this.Page).IsInAsyncPostBack)
                upBoards.Update();

            btnPro.Text = "Vote " + _board.ProName;
            btnCon.Text = "Vote " + _board.ConName;

            BindComments();

            return true;

            //MySql implementation
            //DataTable data = new DataTable();
            //string cnString = WebConfigurationManager.ConnectionStrings["LocalMySqlServer"].ToString();
            //MySqlConnection conn = new MySqlConnection(cnString);
            //MySqlDataAdapter da = new MySqlDataAdapter("SHOW TABLES", conn);
            //MySqlCommandBuilder cb = new MySqlCommandBuilder(da);
            //da.Fill(data);
        }

        protected void BindComments()
        {
            //Kludge create lists to bind to of scores that have comments only
            List<Score> commentScores = new List<Score>();
            for (int i = 0; i < _board.Pros.Count; i++)
            {
                Score score = _board.Pros[i];
                if (score.Votes != null && score.Votes.Count > 0)
                {
                    commentScores.Add(score);
                }
            }
            for (int i = 0; i < _board.Cons.Count; i++)
            {
                Score score = _board.Cons[i];
                if (score.Votes != null && score.Votes.Count > 0)
                {
                    commentScores.Add(score);
                }
            }
            //TODO:  Sort comments by most recent.
            rpProCom.DataSource = commentScores;
            rpProCom.DataBind();
        }

        public void LoadBored()
        {
            if (_board != null)
            {
                Cache[_board.Name] = _board;
            }
            else
            {
                string boredName = string.Empty;
                if (ViewState["boardName"] == null)
                {
                    ViewState["boardName"] = boredName = Request["cat"].ToString().URLDecodeToName();
                }
                else
                {
                    boredName = ViewState["boardName"].ToString();
                }

                _board = Cache[boredName] as ScoreBored;

                if (_board == null)
                {
                    try
                    {
                        Cache[boredName] = _board = ScoreBored.Load(boredName);
                    }
                    catch { }
                }
            }
        }

        protected void LoadEmbed()
        {
            string embed = string.Format("<iframe align='center' src='{0}' frameBorder='0' width='350' height='80' scrolling='no' ></iframe>",
                 HttpRuntime.Cache["basePath"].ToString() + "embedded.aspx?n=" + ViewState["boardName"].ToString().URLEncodeName()
                 );


            lEmbed.Text = Server.HtmlEncode(embed);
            embedSam.Text = embed;
        }

        protected void grdPro_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                //e.Row.Cells[0].CssClass = "score";
                //e.Row.Cells[1].CssClass = "score";
                //if (sender == this.grdPro)
                //    e.Row.Cells[1].Text = board.ProTotal.ToString();
                //else
                //    e.Row.Cells[1].Text = board.ProTotal.ToString();
                //e.Row.Cells.RemoveAt(1);
            }
            else if (e.Row.RowType == DataControlRowType.DataRow)
            {

                int scoreIdx = (sender == grdPro) ? 1 : 2;
                int textIdx = (sender == grdPro) ? 2 : 1;

                //activates the #board td.score class
                e.Row.Cells[scoreIdx].CssClass = "sc";

                Score score = e.Row.DataItem as Score;

                bool alreadyVoted = (user == null) ? false : score.AlreadyVoted(user.ProviderUserKey.ToString());

                HtmlGenericControl btnUp = e.Row.Cells[textIdx].FindControl("btnUp") as HtmlGenericControl;
                HtmlGenericControl btnDown = e.Row.Cells[textIdx].FindControl("btnDown") as HtmlGenericControl;
                HtmlGenericControl btnCom = e.Row.Cells[textIdx].FindControl("btnCom") as HtmlGenericControl;


                //enable the delete button
                if (score.Votes != null && score.Votes.Count == 0)
                {
                    user = (user == null) ? Membership.GetUser() : user;
                    if (user != null && score.UserId == user.ProviderUserKey.ToString())
                    {
                        LinkButton deleteBtn = e.Row.Cells[textIdx].FindControl("btnDel") as LinkButton;
                        deleteBtn.CommandArgument = score.ID.ToString();
                        deleteBtn.EnableViewState = true;
                        deleteBtn.Visible = true;
                        deleteBtn.Text = "delete";
                        deleteBtn.CssClass = "del";
                    }
                }

                string voteFunction = (user == null) ? "$find('mpbSignup').show();" :
                    (score.RatingRaw == 0) ? "$find('mpeScore').show();" : ("showCom(event," + score.ID.ToString() + ", {0}, this);");

                if (alreadyVoted)
                {
                    //format whether comments can be added
                    btnUp.Attributes.Add("class", "up-ya");
                    btnDown.Attributes.Add("class", "down-ya");
                }
                else
                {
                    btnUp.Attributes.Add("onclick", string.Format(voteFunction, "1"));
                    btnDown.Attributes.Add("onclick", string.Format(voteFunction, "-1"));
                }
                btnCom.Attributes.Add("onclick", string.Format(voteFunction, "0"));
               
                //Panel comments = e.Row.Cells[0].FindControl("comments") as Panel;
                //for (int i = 0; i < score.Votes.Count; i++)
                //{
                //    Vote vote = score.Votes[i];
                //    HtmlGenericControl comment = new HtmlGenericControl("div");
                //    comment.Attributes.Add("class", "comment");

                //    if (vote.Comment == string.Empty)
                //        if (vote.Weight > 0)
                //            vote.Comment = "vote +1";
                //        else if (vote.Weight < 0)
                //            vote.Comment = "vote -1";

                //    comment.InnerText = vote.Comment;
                //    comments.Controls.Add(comment);

                //}

            }
        }

        protected void rpVotes_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                user = (user == null) ? Membership.GetUser() : user;

                Vote vote = ((Vote)e.Item.DataItem);
                HtmlAnchor reply = e.Item.FindControl("reply") as HtmlAnchor;
                
                
                if (user != null && ((Vote)e.Item.DataItem).UserName == user.UserName)
                {
                    LinkButton btnDelete = e.Item.FindControl("btnD") as LinkButton;
                    btnDelete.CommandArgument = vote.ID.ToString();
                    btnDelete.Visible = true;
                    
                    reply.Visible = false;
                }
                else
                {
                    //Score parentScore = ((RepeaterItem)(((Repeater)((Control)(e.Item)).Parent).Parent)).DataItem as Score;

                    //a weight of 100 indicates a response
                    string voteFunction = (user == null) ? "$find('mpbSignup').show();" : ("showCom(event," + vote.ID.ToString() + ", 100, this);");

                    reply.Attributes.Add("onclick",voteFunction);
                }
            }
        }

        protected void rpProCom_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Score score = (Score)e.Item.DataItem;
                Label lblScore = e.Item.FindControl("lScore") as Label;

                int adjustedRating = score.RatingAdjusted;

                if (_board.IsProCon)
                {
                    lblScore.Text = (score.IsPro) ? ("+" + adjustedRating.ToString()) : ("-" + adjustedRating.ToString());
                }
                else
                {
                    lblScore.Text = (score.IsPro) ? (_board.ProName + " " + adjustedRating) : (_board.ConName + " " + (adjustedRating * -1).ToString());
                }

            }
        }

        #endregion

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
                case "btnComDummy":
                    e.Control = btnComDummy;
                    break;
            }
        }

        #region deletes
        protected void DeleteComment(object sender, EventArgs eventArgs)
        {
            LinkButton btn = sender as LinkButton;
            int voteID = int.Parse(btn.CommandArgument);

            LoadBored();
            
            Vote vote = _board.FindVote(voteID);
            _board.DeleteVote(voteID);

            //If it changes our ability to re-Vote, rebind the board too
            if (vote == null ||  vote.Weight == 1 || vote.Weight == -1)
            {
                BindAll();
                upBoards.Update();
            }
            else
            {
                BindComments();
            }
            
            
            Cache[_board.Name] = _board;

            
            upOut.Update();
        }

        protected void deleteScore(object sender, EventArgs eventArgs)
        {
            LinkButton sourceBtn = sender as LinkButton;
            int scoreID = int.Parse(sourceBtn.CommandArgument);

            LoadBored();

            _board.DeleteScore(scoreID);

            BindAll();

            Cache[_board.Name] = _board;
        }
        #endregion

        #region viral
        public string EncodeTitle
        {
            get
            {
                // ?=%3F
                // &=%26
                // /=%2F

                string encoded = HttpUtility.UrlEncode( 
                        this.Title.Replace("?", "%3F"));
                return encoded;
            }
        }

        public string EncodeUrl
        {
            get
            {
                string url = Server.UrlEncode(Cache["basePath"] + Request.RawUrl.Substring(1).Replace("¿", "%BF"));
                return url;
            }
        }
        
        public string UrlParam
        {
            get
            {
                // ?=%3F
                // &=%26
                // /=%2F

                //http://scorebored.net/bored/Should_I_was_offf_my_eyebrows%C2%BF
                //http://scorebored.net/bored/Should_I_was_offf_my_eyebrows%23

                string test = HttpUtility.UrlPathEncode( Cache["basePath"] + Request.RawUrl.Substring(1) );

                //http%3A%2F%2Flocalhost%3A1882%2Fbored%2FShould_I_was_offf_my_eyebrows%C2%BF
                //http%3a%2f%2flocalhost%3a1882%2fbored%2fShould_I_was_offf_my_eyebrows%c2%bf
                string encoded = Server.UrlEncode(
                       Cache["basePath"] + Request.RawUrl.Substring(1));
                return encoded;
            }
        }
        #endregion
    }
}