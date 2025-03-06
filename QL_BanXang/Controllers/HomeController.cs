using QL_BanXang.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QL_BanXang.Controllers
{
    public class HomeController : Controller
    {
        QL_CayXangEntities db=new QL_CayXangEntities();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection collection)
        {
            var tendn = collection["username"];
            var matkhau = collection["password"];
            if (string.IsNullOrEmpty(tendn))
            {
                ViewBag.Loi1 = "Phải nhập tên  đăng nhập";

            }
            if (string.IsNullOrEmpty(matkhau))
            {
                ViewBag.Loi2 = "Phải nhập mật khẩu";

            }
            else
            {
                USER us = db.USERS.SingleOrDefault(n => n.Username == tendn && n.Password == matkhau);
                if(us != null)
                {
                    string chucvu = us.Chucvu.ToString();
                    if (chucvu == "Quản Lý")
                    {
                        Session["TaikhoanQL"] = us;
                        return RedirectToAction("Index", "QuanLy");
                    }
                    else if (chucvu == "Nhân Viên")
                    {
                        Session["TaikhoanNV"] = us;
                        return RedirectToAction("Index", "NhanVien");
                    }
                }    
                else
                {
                    ViewBag.Thongbao = "Tên đăng nhập hoặc mật khẩu không đúng";
                }
            }
            return View();
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login", "Home");
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}