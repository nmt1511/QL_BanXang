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
    public class NhapHangController : Controller
    {
        // GET: NhapHang
        QL_CayXangEntities db = new QL_CayXangEntities();
        public ActionResult PhieuNhap()
        {
            var sp = db.PHIEUNHAPs.ToList();

            return View(sp);
        }
        [HttpGet]
        public ActionResult ThemPhieuNhap()
        {

            ViewBag.MaNCC = new SelectList(db.NHACUNGCAPs, "MaNCC", "TenNCC");
            ViewBag.MaNV = new SelectList(db.NHANVIENs, "MaNV", "TenNV");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult ThemPhieuNhap([Bind(Include ="NgayNhap,MaNCC,MaNV")] PHIEUNHAP sp)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.PHIEUNHAPs.Add(sp);
                    db.SaveChanges();
                    return RedirectToAction("ChiTietPhieuNhap","NhapHang", new {mapn=sp.MaPN });
                }
                catch
                {
                    ViewBag.Message = "không thành công!!";
                }

            }
            ViewBag.MaNCC = new SelectList(db.NHACUNGCAPs, "MaNCC", "TenNCC");
            ViewBag.MaNV = new SelectList(db.NHANVIENs, "MaNV", "TenNV");
            return View(sp);
        }
        public ActionResult ChiTietPhieuNhap(int mapn)
        {
            var sp = db.CTPHIEUNHAPs.Where(c => c.MaPN == mapn).ToList();
            ViewBag.PN = mapn;
            return View(sp);

        }
        private int LayMaPNMoiNhat()
        {
            var pnMoiNhat = db.PHIEUNHAPs.OrderByDescending(pn => pn.MaPN).FirstOrDefault();
            if (pnMoiNhat != null)
            {
                return pnMoiNhat.MaPN;
            }
            return 0; // Trường hợp không có phiếu nhập nào trong cơ sở dữ liệu
        }

        [HttpGet]
        public ActionResult ThemSPPN(int mapn)
        {
            ViewBag.MaHH = new SelectList(db.HANGHOAs, "MaHH", "Ten");
            ViewBag.MaQC = new SelectList(db.QUYCACHes, "MaQC", "TenQC");
            // Truyền MaPN mới nhất vào view để sử dụng khi tạo chi tiết phiếu nhập
            ViewBag.MaPN = mapn;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult ThemSPPN([Bind(Include = "MaCTPN,MaPN,MaHH,MaQC,GiaNhap,SoLuong")] CTPHIEUNHAP sp)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.CTPHIEUNHAPs.Add(sp);
                    db.SaveChanges();
                    return RedirectToAction("ChiTietPhieuNhap","NhapHang", new {mapn=sp.MaPN});
                }
                catch
                {
                    ViewBag.Message = "không thành công!!";
                }

            }
            ViewBag.MaHH = new SelectList(db.HANGHOAs, "MaHH", "Ten");
            ViewBag.MaQC = new SelectList(db.QUYCACHes, "MaQC", "TenQC");
            return View(sp);
        }
        
    }
}