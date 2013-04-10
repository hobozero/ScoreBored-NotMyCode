using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;


namespace ScoreBoard.Services
{
    /// <summary>
    /// Summary description for searchService
    /// </summary>
    [WebService(Namespace = "http://scorebored.net/2010/06")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class searchService : System.Web.Services.WebService
    {

        [WebMethod]
        public string[] SearchBored(string prefixText, int count)
        {
            ScoreBoard.AppCode.ScoreDataDataContext dc = new ScoreBoard.AppCode.ScoreDataDataContext();
            return (from r in dc.searchBored(prefixText, count)
                          select r.NAME).ToArray();
        }
    }
}
