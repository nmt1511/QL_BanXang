using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QL_BanXang.Models;
using System.Net;
using System.Web.UI;
using System.IO;
using System.Data.Entity;
using System.Data;

namespace QL_BanXang.Controllers
{
    public class ProductController : Controller

    {
       QL_CayXangEntities db = new QL_CayXangEntities();
        // GET: Product
        public ActionResult Index()
        {
            var sp =db.HANGHOAs.ToList();
            
            return View(sp);
            
        }
        public ActionResult DauNhot()
        {
            var Menu = from tl in db.LOAIHANGs where tl.MaLoai >= 1 && tl.MaLoai <= 10 select tl;
            return PartialView(Menu);
        }
        public ActionResult DungDichRuaXeMay()
        {
            var Menu = from tl in db.LOAIHANGs where tl.MaLoai >= 11 && tl.MaLoai <= 15 select tl;
            return PartialView(Menu);
        }
        public ActionResult DungDichRuaXeHoi()
        {
            var Menu = from tl in db.LOAIHANGs where tl.MaLoai >= 16 && tl.MaLoai <= 17 select tl;
            return PartialView(Menu);
        }
        public ActionResult MoBo()
        {
            var Menu = from tl in db.LOAIHANGs where tl.MaLoai >= 18 && tl.MaLoai <= 19 select tl;
            return PartialView(Menu);
        }
        public ActionResult Xang()
        {
            var Menu = from tl in db.LOAIHANGs where tl.MaLoai >= 20 && tl.MaLoai <= 25 select tl;
            return PartialView(Menu);
        }
        [HttpGet]
        public ActionResult ThemSP() 
        {
            
            ViewBag.MaLoai = new SelectList(db.LOAIHANGs, "MaLoai", "TenLoai");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult ThemSP([Bind(Include = "MaHH,Ten,MaLoai,Mota")] HANGHOA sp)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.HANGHOAs.Add(sp);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    ViewBag.Message = "không thành công!!";
                }

            }
            ViewBag.MaLoai = new SelectList(db.LOAIHANGs, "MaLoai", "TenLoai", sp.LOAIHANG.MaLoai);
            return View(sp);
        }
        [HttpGet]
        public ActionResult XoaSP(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HANGHOA sp = db.HANGHOAs.Find(id);
            if (sp == null)
            {
                return HttpNotFound();
            }
            return View(sp);
        }

        [HttpPost, ActionName("XoaSP")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HANGHOA book = db.HANGHOAs.Find(id);
            db.HANGHOAs.Remove(book);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HANGHOA book = db.HANGHOAs.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }
        [HttpGet]
        public ActionResult SuaSP(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HANGHOA km = db.HANGHOAs.Find(id);
            if (km == null)
            {
                return HttpNotFound();
            }
            return View(km);
        }

        [HttpPost, ActionName("SuaSP")]
        [ValidateAntiForgeryToken]
        public ActionResult Sua(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var studentToUpdate = db.HANGHOAs.Find(id);
            if (TryUpdateModel(studentToUpdate, "",
               new string[] { "MaHH", "Mota", "Ten", }))
            {
                try
                {
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (DataException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            return View(studentToUpdate);
        }
    }
}