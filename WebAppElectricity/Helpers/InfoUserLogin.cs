using System.Web;
using System.Web.Security;
using WebAppElectricity.Models;

namespace WebAppElectricity.Helpers
{
    public class InfoUserLogin
    {
        public static Users GetLoggedInUserInfo()
        {
            HttpCookie authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                string[] userData = authTicket.UserData.Split('|');

                var userInfo = new Users
                {
                    Role        = userData[0],
                    UserId      = int.Parse(userData[1]),
                    UserName    = userData[2],
                    Email       = userData[3]
                };

                return userInfo;
            }
            return null;
        }
    }
}