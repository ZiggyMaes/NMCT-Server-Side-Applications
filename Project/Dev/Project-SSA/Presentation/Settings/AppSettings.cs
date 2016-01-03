using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace Presentation.Settings
{
    public class AppSettings
    {
        public static string FromMail
        {
            get { return WebConfigurationManager.AppSettings["FromMail"]; }
        }
        public static string SMTPServer
        {
            get { return WebConfigurationManager.AppSettings["SMTPServer"]; }
        }
        public static string SMTPPort
        {
            get { return WebConfigurationManager.AppSettings["SMTPPort"]; }
        }
        public static string Username
        {
            get { return WebConfigurationManager.AppSettings["Username"]; }
        }
        public static string Password
        {
            get { return WebConfigurationManager.AppSettings["Password"]; }
        }
    }
}
