using QL_BanXang.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace QL_BanXang.Controllers
{
    public class BanhangController : Controller
    {
        QL_CayXangEntities db = new QL_CayXangEntities();
        public ActionResult CaTruc()
        {
            var dh = db.CATRUCs.ToList();
            return View(dh);
        }
        [HttpGet]
        public ActionResult ThemCaTruc()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult ThemCaTruc([Bind(Include = "TenCaTruc,GioBatDau,GioKetThuc")] CATRUC ct)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.CATRUCs.Add(ct);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    ViewBag.Message = "không thành công!!";
                }

            }
            return View(ct);
        }
        [HttpGet]
        public ActionResult Xoacatruc(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CATRUC sp = db.CATRUCs.Find(id);
            if (sp == null)
            {
                return HttpNotFound();
            }
            return View(sp);
        }

        [HttpPost, ActionName("Xoacatruc")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmedtl(int id)
        {
            CATRUC sp = db.CATRUCs.Find(id);
            db.CATRUCs.Remove(sp);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Suacatruc(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CATRUC sp = db.CATRUCs.Find(id);
            if (sp == null)
            {
                return HttpNotFound();
            }
            return View(sp);
        }
        [HttpPost, ActionName("Suacatruc")]
        [ValidateAntiForgeryToken]
        public ActionResult SuaTheLoaiPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var studentToUpdate = db.CATRUCs.Find(id);
            if (TryUpdateModel(studentToUpdate, "",
               new string[] { "TenCaTruc", "GioBatDau", "GioKetThuc" }))
            {
                try
                {
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (DataException /* dex */)
                {
            
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            return View(studentToUpdate);
        }
    }
}