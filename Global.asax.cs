using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace ScoreBoard
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Class is called only on the first request
        /// </summary>
        private class AppStart
        {
            static bool _init = false;
            private static Object _lock = new Object();
            
            /// <summary>
            /// Does nothing after first request
            /// </summary>
            /// <param name="context"></param>
            public static void Start(HttpContext context)
            {
                if (_init)
                {
                    return;
                }
                //create class level lock in case multiple sessions start simultaneously
                lock (_lock)
                {
                    if (!_init)
                    {
                        string server = context.Request.ServerVariables["SERVER_NAME"];
                        string port = context.Request.ServerVariables["SERVER_PORT"];
                        HttpRuntime.Cache.Insert("basePath", "http://" + server + ":" + port + "/");
                    }
                }
            }
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            //initializes Cache on first request
            AppStart.Start(HttpContext.Current);
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

            //Thanks Scott!
        //http://weblogs.asp.net/scottgu/archive/2007/02/26/tip-trick-url-rewriting-with-asp-net.aspx
            string fullOrigionalpath = Request.Url.ToString().ToLower();

            if (fullOrigionalpath.Contains("/bored/"))
            {
                string category = fullOrigionalpath.Substring(fullOrigionalpath.LastIndexOf('/')+1);

                if (category != string.Empty)
                {

                    //Note:  This path should be changed in productions to "/Dynamic_ScoreBoard.aspx?cat=..."
                    Context.RewritePath("/Dynamic_ScoreBoard.aspx?cat=" + category);
                }
            }
            if (fullOrigionalpath.Contains("/search/"))
            {
                string category = fullOrigionalpath.Substring(fullOrigionalpath.LastIndexOf('/') + 1);

                if (category != string.Empty)
                {

                    //Note:  This path should be changed in productions to "/Dynamic_ScoreBoard.aspx?cat=..."
                    Context.RewritePath("/results.aspx?s=" + category);
                }
            }
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}