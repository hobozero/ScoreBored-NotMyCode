using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Web.Security;

namespace ScoreBoard
{
    public class Score
    {
        /// <summary>
        /// Score rating without up/down votes
        /// </summary>
        protected int _rawRating = -1;

        public string Text { get; set; }
        public string UserId { get; set; }
        /// <summary>
        /// Whether this score belongs to the Pro side of the board
        /// </summary>
        public bool IsPro { get; set; }
        public int RatingRaw
        {
            get
            {
                return _rawRating;
            }
            set
            {
                _rawRating = value;
            }
        }

        public int RatingAdjusted
        {
            get
            {
                Double adjustedRating = _rawRating;

                if (Votes != null)
                {
                    for (int i = 0; i < this.Votes.Count; i++)
                    {
                        adjustedRating += Votes[i].Weight * .5;
                    }
                }

                return (int)adjustedRating;
            }
        }
        public DateTime Created { get; set; }
        public int ID { get; set; }
        /// <summary>
        /// Bind string for datamembers to get Comment text
        /// </summary>
        public string Count
        {
            get
            {
                //get votes that are not comments
                int agree = (Votes == null) ? 0 : Votes.Where(a => a.Weight == 1).Count();
                int disagree = (Votes == null) ? 0 : Votes.Where(a => a.Weight == -1).Count();
                int total = Votes.Count;

                //No Votes
                if (agree == 0 && disagree == 0)
                {
                    if (total > 0)
                        return "comments";
                    else
                        return string.Empty;

                }

                return "+" + agree.ToString() + "  -" + disagree.ToString(); 

            }
        }

        public List<Vote> Votes { get; set; }

        public void AddVote(MembershipUser user , string comment, short weight)
        {
            Database db = DatabaseFactory.CreateDatabase("cnGrammit");

            if (comment.Length > 500)
                comment = comment.Substring(0, 496)+"...";

            object rtnVal = db.ExecuteScalar("dbo.usp_Z_Score_addVOTE", user.ProviderUserKey, this.ID, comment, weight);

            if (rtnVal != DBNull.Value)
            {
                int voteID = Convert.ToInt32(rtnVal);

                Vote vote = new Vote()
                {
                    ID = Convert.ToInt32(rtnVal),
                    Comment = comment,
                    UserId = user.ProviderUserKey.ToString(),
                    UserName = user.UserName.ToLower(), 
                    Weight = weight,
                    Parent = this,
                    Dt = DateTime.UtcNow

                };

                this.Votes.Add(vote);
            }
        }

        public void DeleteVote(Vote vote)
        {
            Database db = DatabaseFactory.CreateDatabase("cnGrammit");

            object rtnVal = db.ExecuteNonQuery( CommandType.Text, "DELETE from Z_SB_VOTE where ID = " + vote.ID.ToString());

            this.Votes.Remove(vote);
        }

        public bool AlreadyVoted(string userId)
        {
            if (Votes == null) return false;
            if (this.UserId == userId) return true;

            for (int i=0;  i < Votes.Count; i++)
            {
                Vote vote = Votes[i];
                if (vote.UserId == userId && (vote.Weight == 1 || vote.Weight == -1))
                    return true;
            }

            return false;
        }

        public Vote FindVote(int voteID)
        {
            if (Votes == null)
                return null;
            for (int i = 0; i < this.Votes.Count; i++)
            {
                if (Votes[i].ID == voteID)
                    return Votes[i];
            }

            return null;
        }
    }

    public class Vote {
        string _userName = string.Empty;
        public string UserName { get { return _userName; } set { _userName = value.ToLower(); } }
        public string UserId {get; set; }
        public string Comment {get; set; }
        public Int16 Weight {get; set; }
        public DateTime Dt { get; set; }
        public int ID { get; set; }
        public string ImgClass
        {
            get
            {
                if (Weight > 0)
                {
                    return "c-iUp";
                }
                else if (Weight < 0)
                    return "c-iDn";
                else
                    return "c-iCom";
            }
        }
        public Score Parent { get; set; }

    }

}
