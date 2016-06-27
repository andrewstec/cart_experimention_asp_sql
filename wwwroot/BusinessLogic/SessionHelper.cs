using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ShoppingCartApplication.BusinessLogic
{
    class SessionHelper
    {
        public const string SESSION_START = "Session_Start";
        public const string SESSION_UPDATED = "Session_Updated";
        public const string SESSION_END = "Session_End";

        public string SessionID
        {
            get
            {
                if (HttpContext.Current.Session.SessionID != null)
                {
                    return HttpContext.Current.Session.SessionID;
                }
                else
                {
                    return null;
                }
            }
        }

        public DateTime StartTime
        {
            get
            {
                try
                {
                    return (DateTime)HttpContext.Current.Session[SESSION_START];
                }
                catch
                {
                    InitializeSession();
                }
                return (DateTime)HttpContext.Current.Session[SESSION_START];
            }
        }

        public DateTime EndTime
        {
            get
            {
                return (DateTime)HttpContext.Current.Session[SESSION_END];
            }
        }

        public void UpdateSession()
        {
            if (SessionID == null)
            {
                InitializeSession();
                HttpContext.Current.Session[SESSION_END] = DateTime.Now.AddMinutes(60);
            }
        }
        public void InitializeSession()
        {
            HttpContext.Current.Session[SESSION_START] = DateTime.Now;
            HttpContext.Current.Session[SESSION_END] = DateTime.Now.AddMinutes(60);
        }

        public void ClearSession()
        {
            if (SessionID != null)
            {
                HttpContext.Current.Session.Clear();
                HttpContext.Current.Session.Abandon();
            }
        }
    }
}
