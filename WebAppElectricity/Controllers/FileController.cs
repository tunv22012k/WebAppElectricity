using System;
using System.Web.Mvc;
using WebAppElectricity.Services;
using WebAppElectricity.Helpers;
using WebAppElectricity.Models;
using System.Collections.Generic;

namespace WebAppElectricity.Controllers
{
    public class FileController : Controller
    {
        private readonly FileService fileService;

        public FileController()
        {
            fileService = new FileService();
        }

        public ActionResult Index()
        {
            var userLogin = InfoUserLogin.GetLoggedInUserInfo();
            var listCategory = fileService.GetDataCategory(userLogin.UserId.ToString());
            ViewBag.listCategory = listCategory;
            return View();
        }

        [HttpPost]
        public JsonResult CreateCategory(string UserId, string CategoryName)
        {
            try
            {
                bool isSuccess = fileService.CreateCategory(UserId, CategoryName);
                return Json(new { success = isSuccess });
            }
            catch (Exception ex)
            {
                throw new Exception("Delete err: " + ex.Message);
            }
        }
    }
}