using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Xml.Serialization;
using System.Xml;
using System.Diagnostics;
using System.Xml.Linq;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Web.Security;

namespace ScoreBoard
{

    public class ScoreBored
    {
        //private static XmlSerializer _serializer = new XmlSerializer(typeof(ScoreBored), string.Empty);

        public ScoreBored()
        {
        }

        public static string Folder = "~/boards/";

        #region props
        public string Name { get; set; }
        public string Description { get; set; }
        [XmlAttribute]
        public string UserId { get; set; }

        [XmlIgnore]
        public string UserName { get; set; }
        [XmlAttribute]
        public DateTime Created { get; set; }

        public string ProName { get; set; }
        public string ConName { get; set; }


        public List<Score> Pros { get; set; }
        public List<Score> Cons { get; set; }
        public Dictionary<Int16, string > Phrases {get; set;}

        public int ID { get; set; }

        public bool PhraseIs { get; set; }

        [XmlIgnore]
        public int ProTotal
        {
            get { return this.Pros.Sum(w => w.RatingAdjusted); }
        }

        [XmlIgnore]
        public int ConTotal
        {
            get { return this.Cons.Sum(w => w.RatingAdjusted); }
        }
        #endregion

        /// <summary>
        /// Checks if a scoreboard with this name already exists under a different user
        /// </summary>
        /// <returns></returns>
        public bool CanUpdate(string editorId)
        {
            string path = HttpContext.Current.Server.MapPath(Folder + this.Name + ".xml");

            if (!File.Exists(path))
                return true;
            else
            {
                XDocument doc = XDocument.Load(path);

                XAttribute UserId = (from xmlUserId in doc.Elements("ScoreBored").Attributes("UserId")
                                     //where xml2.Element("ID").Value == variable
                                     select xmlUserId).FirstOrDefault();
                if (UserId.ToString() == editorId)
                    return true;
                else
                    return false;
            }
        }

        public static ScoreBored Load(string boredName)
        {
            ScoreBored frankenstein = null;

            Database db = DatabaseFactory.CreateDatabase("cnGrammit");

            //remove the "_" chars first
            using (IDataReader rdr = db.ExecuteReader("[usp_Z_Score_getBoard]", boredName))
            {
                if (rdr.Read())
                {
                    frankenstein = new ScoreBored()
                    {
                        ID = Convert.ToInt32(rdr["ID"]),
                        UserId = rdr["UserID"].ToString(),
                        UserName = rdr["UserName"].ToString(),
                        ConName = rdr["CON_NAME"].ToString(),
                        Cons = new List<Score>(),
                        Created = DateTime.Parse(rdr["DT_CREATED"].ToString()),
                        Description = rdr["DESCRIPTION"].ToString(),
                        Name = rdr["NAME"].ToString(),
                        ProName = rdr["PRO_NAME"].ToString(),
                        Pros = new List<Score>(),
                        Phrases = new Dictionary<short, string>(),
                        PhraseIs = Convert.ToBoolean(rdr["IS_PHRASE"])

                    };
                }
                else
                {
                    return null;
                }
                if (rdr.NextResult())
                {
                    while (rdr.Read()){
                        Score score = new Score(){
                             Created = DateTime.Parse(rdr["DT_CREATED"].ToString()),
                              RatingRaw = Convert.ToInt32(rdr["SCORE"]),
                               Text =  rdr["WORDS"].ToString(),
                                UserId = rdr["UserID"].ToString(),
                                ID = Convert.ToInt32(rdr["ID"]),
                                 Votes = new List<Vote>(Convert.ToInt32(rdr["VOTES"])),
                                  IsPro = Convert.ToBoolean(rdr["IS_PRO"])
                        };

                        if (score.IsPro)
                        {
                            frankenstein.Pros.Add(score);
                        }
                        else
                            frankenstein.Cons.Add(score);
                    }
                }

                if (rdr.NextResult())
                {
                    while (rdr.Read())
                    {
                        frankenstein.Phrases.Add(Convert.ToInt16(rdr["MARGIN"]), rdr["PHRASE"].ToString());
                    }
                }

                if (rdr.NextResult())
                {
                    while (rdr.Read())
                    {
                        Score parentScore = frankenstein.FindScore(Convert.ToBoolean(rdr["IS_PRO"]), Convert.ToInt32(rdr["SCORE_ID"]));

                        Vote vote = new Vote()
                        {
                            ID = Convert.ToInt32(rdr["ID"]),
                            Comment = rdr["COMMENT"].ToString(),
                            UserId = rdr["UserId"].ToString(),
                            UserName = rdr["UserName"].ToString(),
                            Weight = Convert.ToInt16(rdr["WEIGHT"]),
                            Dt = Convert.ToDateTime(rdr["DT_CREATED"]),
                            Parent = parentScore
                        };
                        parentScore.Votes.Add(vote);
                    }
                }
            }

            if (frankenstein.Pros.Count == 0)
            {
                frankenstein.Pros.Add(
                    new Score()
                    {
                        ID = 0,
                        RatingRaw = 0,
                         Text="Vote to add points"
                    });
            }
            if (frankenstein.Cons.Count == 0)
            {
                frankenstein.Cons.Add(
                    new Score()
                    {
                        ID = 0,
                        RatingRaw = 0,
                        Text = "Vote to add points"
                    });
            }

            return frankenstein;
        }

        public Score FindScore(bool isPro, int scoreID){

            List<Score> scoreList = (isPro) ? this.Pros : this.Cons;

            for (int i = 0; i < scoreList.Count; i++)
            {
                Score score = scoreList[i];
                if (score.ID == scoreID)
                    return score;
            }

            return null;
        }

        public Vote FindVote(int voteID)
        {
            Vote rtnVote = null;
            for (int i = 0; i < this.Pros.Count; i++)
            {
                Score score = Pros[i];
                rtnVote = score.FindVote(voteID);
                if (rtnVote != null)
                    return rtnVote;
            }
            for (int i = 0; i < this.Cons.Count; i++)
            {
                Score score = Cons[i];
                rtnVote = score.FindVote(voteID);
                if (rtnVote != null)
                    return rtnVote;
            }

            return null;
        }

        //XML Implementation
        //string path = HttpContext.Current.Server.MapPath(Folder + boredName + ".xml");

        //using (XmlTextReader rdr = new XmlTextReader(path))
        //{
        //    try
        //    {
        //        frankenstein = (ScoreBored)_serializer.Deserialize(rdr);
        //    }
        //    catch (Exception ex)
        //    {
        //        string error = string.Empty;
        //        do
        //        {
        //            error += "\r\n EXCEPTION \r\n" + ex.Message;
        //            ex = ex.InnerException;
        //        } while (ex != null);
        //        Debug.WriteLine("Error Trying to deserialize" + error);
        //    }
        //}

        //    return frankenstein;
        //}

        public void Save()
        {
            //Xml Implementation
            //string xmlFile = XmlSerialize();
            //string path = HttpContext.Current.Server.MapPath(Folder + this.Name + ".xml");
            //File.WriteAllText(path, xmlFile);

            //TODO:  Need a transaction.  so lazy

            Database db = DatabaseFactory.CreateDatabase("cnGrammit");


            if (this.ID < 1)
            {
                this.ID = Convert.ToInt32(
                        db.ExecuteScalar("dbo.usp_Z_Score_addUpdateBoard", new Guid(UserId), Name, Description, ProName, ConName, PhraseIs)
                        );
                foreach (short key in this.Phrases.Keys)
                {
                    db.ExecuteScalar("usp_Z_Score_addPHRASE", this.ID, this.Phrases[key], key);
                }
            }
            else
            {
                throw new System.NotImplementedException("Need to update by ID Procedure");
            }

        }

        public void Update(string oldName)
        {
            Database db = DatabaseFactory.CreateDatabase("cnGrammit");
            this.ID = Convert.ToInt32(
                db.ExecuteScalar("dbo.usp_Z_Score_updateBoard", oldName, Name, Description, ProName, ConName, PhraseIs)
                );

            foreach (short key in this.Phrases.Keys)
            {
                db.ExecuteNonQuery("usp_Z_Score_updatePHRASE", this.ID, this.Phrases[key], key);
            }
        }

        public void AddScore(Score score)
        {
            Database db = DatabaseFactory.CreateDatabase("cnGrammit");

            score.ID = Convert.ToInt32(db.ExecuteScalar("dbo.usp_Z_Score_addScore", new Guid(score.UserId), this.ID, score.RatingRaw, score.Text, score.IsPro));

            List<Score> list = (score.IsPro) ? this.Pros : this.Cons;


            //remove placeholder
            if (list.Count == 1 && list[0].RatingRaw == 0)
                list.RemoveAt(0);

            list.Add(score);
        }

        public void ReplyToVote(int voteID, MembershipUser user , string comment)
        {
            Vote vote = FindVote(voteID);

            if (vote != null)
            {
                Score score = vote.Parent;

                string originalUser = string.Format("<strong>@{0}</strong>:  ", vote.UserName);

                score.AddVote(user, originalUser + comment, 0);
            }
        }

        public void DeleteScore(int scoreID)
        {
            bool isPro = true;
            Score score = this.FindScore(true, scoreID);
            if (score == null){
                score = this.FindScore(false, scoreID);
                isPro = false;
            }

            if (score != null)
            {
                Database db = DatabaseFactory.CreateDatabase("cnGrammit");

                object rtnVal = db.ExecuteNonQuery(CommandType.Text, "DELETE from Z_SB_SCORE where ID = " + scoreID.ToString());

                List<Score> scores = (isPro) ? this.Pros : this.Cons;
                scores.Remove(score);
            }
        }

        public void DeleteVote(int voteID)
        {
            Vote vote = FindVote(voteID);
            if (vote != null)
            {
                Score score = vote.Parent;

                score.DeleteVote(vote);
            }
        }


        //public string XmlSerialize()
        //{
        //    MemoryStream ms = new MemoryStream();

        //    XmlTextWriter tw = new XmlTextWriter(ms, null);
        //    _serializer.Serialize(tw, this);

        //    // Rewind the Stream
        //    ms.Seek(0, SeekOrigin.Begin);

        //    StreamReader sr = new StreamReader(ms);
        //    return sr.ReadToEnd();
        //}

        public string GetStatus()
        {
            int proTotal = this.ProTotal;
            int conTotal = this.ConTotal;

            return GetStatus(this.ProTotal, this.ConTotal);

        }

        public string GetStatus(int proTotal, int conTotal)
        {
            float votes = proTotal + conTotal;
            float proPercent = (float)proTotal / votes;

            short margin = 0;

            if (proPercent >= 0 && proPercent < .25)
                margin = 3;
            else if (proPercent >= .25 && proPercent < .4)
                margin = 2;
            else if (proPercent >= .4 && proPercent < .5)
                margin = 1;
            else if (proPercent == .5)
                margin = 0;
            else if (proPercent > .5 && proPercent < .61)
                margin = -1;
            else if (proPercent >= .61 && proPercent < .76)
                margin = -2;
            else if (proPercent >= .76)
                margin = -3;
            
            if (proTotal == conTotal && !Phrases.ContainsKey(0))
            {
                return "We have a tie (until someone votes)";
            }
            else
            {
                if (this.IsProCon)
                {
                    if (PhraseIs)
                    {
                        return string.Format("{0} is {1}",
                            (this.Name == string.Empty) ? "[No Name Yet]" : this.Name.TrimEnd(new char[2] {'?', '!'}), this.Phrases[margin]);
                    }
                    else
                        return this.Phrases[margin];
                }
                else
                {
                    short absMargin = Math.Abs(margin);
                    if (PhraseIs)
                    {
                        return ((proTotal > conTotal) ? this.ProName : this.ConName) + 
                            " is " + this.Phrases[absMargin];
                    }
                    else{
                        return ((proTotal > conTotal) ? this.ProName : this.ConName) +
                            " is " + this.Phrases[absMargin] + " " +
                            ((proTotal < conTotal) ? this.ProName : this.ConName);
                    }
                }
            }

        }

        public void SaveMySql()
        {
            //using (MySqlConnection cnx = new MySqlConnection(cnString))
            //{
            //    string cmdText = "usp_AddBoard";
            //    using (MySqlCommand cmd = new MySqlCommand(cmdText, cnx))
            //    {
            //        // Set the command type to StoredProcedure
            //        cmd.CommandType = CommandType.StoredProcedure;

            //        // Create the parameter
            //        cmd.Parameters.Add(new MySqlParameter("?matchType", Convert.ToInt32(rdoType.SelectedValue)));
            //        cmd.Parameters.Add(new MySqlParameter("?name", name));
            //        cmd.Parameters.Add(new MySqlParameter("?description", ));
            //        cmd.Parameters.Add(new MySqlParameter("?UserId", Membership.GetUser().ProviderUserKey));
            //        cmd.Parameters.Add(new MySqlParameter("?pro", pro));
            //        cmd.Parameters.Add(new MySqlParameter("?con", con));
            //        cmd.Parameters.Add(new MySqlParameter("?proPhrase",  proPhrase));
            //        cmd.Parameters.Add(new MySqlParameter("?conPhrase", conPhrase));

            //        cnx.Open();
            //        cmd.ExecuteNonQuery();
            //        cnx.Close();
            //    }
            //}

        }


        public bool IsProCon
        {
            get
            {
                return (this.ProName.ToLower() == "pro" && this.ConName.ToLower() == "con");

            }

        }
    }
}
