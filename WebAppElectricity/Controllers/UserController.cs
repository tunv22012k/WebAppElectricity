using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebAppElectricity.Models;
using WebAppElectricity.Services;

namespace WebAppElectricity.Controllers
{
    public class UserController : Controller
    {
        private readonly UserService userService;

        public UserController()
        {
            userService = new UserService();
        }


        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Users user)
        {
            var userLogin = userService.Authenticate(user.UserName, user.Password);
            if (userLogin != null)
            {
                // Tạo ticket FormsAuthentication với vai trò người dùng
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                    1,                                      // Version
                    userLogin.UserName,                     // User name
                    DateTime.Now,                           // Issue date
                    DateTime.Now.AddMinutes(30),            // Expiration
                    false,                                  // Persistent
                    $"{userLogin.Role}|{userLogin.UserId}|{userLogin.UserName}|{userLogin.Email}" // Users
                );

                // Mã hóa ticket
                string encryptedTicket = FormsAuthentication.Encrypt(ticket);

                // Tạo cookie
                HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                Response.Cookies.Add(authCookie);

                // set info user login
                Session["UserLogin"] = userLogin;

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không chính xác.");
            return View(user);
        }

        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear(); // Xóa tất cả dữ liệu session
            return RedirectToAction("Login");
        }
    }
}