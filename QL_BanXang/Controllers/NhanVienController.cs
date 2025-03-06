using QL_BanXang.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

using System.Data.Entity;
using System.Data;
using System.Data.SqlClient;
using System.Data.Entity.Core.EntityClient;
using static System.Data.Entity.Infrastructure.Design.Executor;

namespace QL_BanXang.Controllers
{
    public class NhanVienController : Controller
    {
        //cột bơm
        //nhà cung cấp
        //loại sản phẩm
       
        QL_CayXangEntities db = new QL_CayXangEntities();
        public ActionResult Index()
        {
            return View();
        }
        //cột bơm
        public ActionResult danhsachcotbom()
        {
            var currentDate = DateTime.Today;
            var cotBomXangInfo = db.COTBOMXANGs
                .Join(db.CHITIETNGAYLAMs,
                    cot => cot.MaCot,
                    chiTiet => chiTiet.MaCot,
                    (cot, chiTiet) => new { CotBomXang = cot, ChiTietNgayLam = chiTiet })
                .Where(joinResult => DbFunctions.TruncateTime(joinResult.ChiTietNgayLam.NgayLam) == currentDate)
                .Select(joinResult => joinResult.CotBomXang)
                .ToList();
    //        var dh = db.COTBOMXANGs.ToList();
            return View(cotBomXangInfo);
        }
        [HttpGet]
        public ActionResult Suacotbom(int? id)
        {
            var chiTietNgayLam = db.CHITIETNGAYLAMs.FirstOrDefault(c => c.MaCot == id);
            if (chiTietNgayLam == null)
            {
                return HttpNotFound();
            }

            return View(chiTietNgayLam);
        }
        [HttpPost, ActionName("Suacotbom")]
        [ValidateAntiForgeryToken]
        public ActionResult Suacotbom1(int? id)
        {

            var update = db.CHITIETNGAYLAMs.Find(id);
            if (TryUpdateModel(update, "",
               new string[] { "ChiSoDau", "ChiSoCuoi",}))
            {
                try
                {
                    db.SaveChanges();

                    return RedirectToAction("danhsachcotbom");
                }
                catch (DataException /* dex */)
                {

                    ModelState.AddModelError("", "lỗi :(");
                }
            }
            return View(update);
        }




        [HttpGet]
        public ActionResult suacot(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var cotBomXang = db.COTBOMXANGs.Find(id);
            if (cotBomXang == null)
            {
                return HttpNotFound();
            }

            DateTime today = DateTime.Today;

            var chiTietNgayLam = db.CHITIETNGAYLAMs
                .Where(c => c.NgayLam == today && c.MaCot == cotBomXang.MaCot)
                .ToList();

            // Truyền danh sách các bản ghi CHITIETNGAYLAM phù hợp vào view để hiển thị hoặc chỉnh sửa
            return View(chiTietNgayLam);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult suacotpost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Tìm thông tin của cột bơm
            var cotBomXang = db.COTBOMXANGs.Find(id);
            if (cotBomXang == null)
            {
                return HttpNotFound();
            }

            // Lấy ngày hiện tại
            DateTime today = DateTime.Today;

            // Tìm các bản ghi trong CHITIETNGAYLAM có ngày và mã cột trùng khớp
            var chiTietNgayLam = db.CHITIETNGAYLAMs
                .Where(c => c.NgayLam == today && c.MaCot == cotBomXang.MaCot)
                .ToList();

            // Kiểm tra xem có bản ghi nào hay không
            if (chiTietNgayLam.Any())
            {
                // Thực hiện các thay đổi bạn muốn ở đây

                try
                {
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (DataException /* dex */)
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                    // Hoặc xử lý lỗi theo cách bạn muốn
                }
            }

            // Trả về view nếu không có bản ghi nào phù hợp
            return View();
        }




















        //nhà cung cấp
        public ActionResult nhacungcap()
        {
            var dh = db.NHACUNGCAPs.ToList();
            return View(dh);
        }
        [HttpGet]
        public ActionResult Themnhacungcap()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Themnhacungcap([Bind(Include = "MaNCC,TenNCC,DiaChi,Sdt,Email")] NHACUNGCAP ct)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.NHACUNGCAPs.Add(ct);
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
        public ActionResult Xoanhacungcap(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NHACUNGCAP sp = db.NHACUNGCAPs.Find(id);
            if (sp == null)
            {
                return HttpNotFound();
            }
            return View(sp);
        }

        [HttpPost, ActionName("Xoanhacungcap")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmedtl(int id)
        {
            NHACUNGCAP sp = db.NHACUNGCAPs.Find(id);
            db.NHACUNGCAPs.Remove(sp);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Suanhacungcap(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NHACUNGCAP sp = db.NHACUNGCAPs.Find(id);
            if (sp == null)
            {
                return HttpNotFound();
            }
            return View(sp);
        }
        [HttpPost, ActionName("Suanhacungcap")]
        [ValidateAntiForgeryToken]
        public ActionResult SuanhacungcapPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var studentToUpdate = db.NHACUNGCAPs.Find(id);
            if (TryUpdateModel(studentToUpdate, "",
               new string[] { "MaNCC", "TenNCC", "DiaChi", "Sdt", "Email" }))
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


        //loại sản phẩm
        public ActionResult loaisanpham()
        {
            var dh = db.LOAIHANGs.ToList();
            return View(dh);
        }
        [HttpGet]
        public ActionResult Themloaisanpham()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Themloaisanpham([Bind(Include = "MaLoai,TenLoai")] LOAIHANG ct)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.LOAIHANGs.Add(ct);
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
        public ActionResult XoaLoaiSP(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LOAIHANG sp = db.LOAIHANGs.Find(id);
            if (sp == null)
            {
                return HttpNotFound();
            }
            return View(sp);
        }

        [HttpPost, ActionName("XoaLoaiSP")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmedtl1(int id)
        {
            LOAIHANG sp = db.LOAIHANGs.Find(id);
            db.LOAIHANGs.Remove(sp);
            db.SaveChanges();
            return RedirectToAction("Index");
        }






        [HttpGet]
        public ActionResult Themphieubanhang()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Themphieubanhang([Bind(Include = "MaPB,NgayBan,MaNV")] PHIEUBAN ct)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.PHIEUBANs.Add(ct);
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

    }
}