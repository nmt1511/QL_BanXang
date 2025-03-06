using QL_BanXang.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace QL_BanXang.Controllers
{
    public class QuanLyController : Controller
    {
        // GET: QuanLy
        QL_CayXangEntities db= new QL_CayXangEntities();
        public ActionResult Index()
        {
            ViewBag.DoanhThu = 0;
            ViewBag.TongHang = 0;
            ViewBag.tongDonHang2 = 0;
            ViewBag.tongDoanhThu = 0;
            return View();
        }
        [HttpGet]
        public ActionResult TKDoanhThuThang(int month)
        {
            var query = from bh in db.PHIEUBANs 
                        join ct in db.CTPHIEUBANs on bh.MaPB equals ct.MaPB
                        join hh in db.HANGHOAs on ct.MaHH equals hh.MaHH
                        join ctg in db.CTGIAs on hh.MaHH equals ctg.MaHH
                        where ctg.MaHH == ct.MaHH && ctg.MaQC == ct.MaQC
                        select new
                        {
                            CreatedDate=bh.NgayBan,
                            Quantity=ct.SoLuong,
                            Price=ct.DonGia,
                            OriginalPrice=ctg.GiaNhap
                        };
            if(month != 0)
            {
                query=query.Where(x => x.CreatedDate.Value.Month == month);
            }
            var result = query.GroupBy(x => DbFunctions.TruncateTime(x.CreatedDate)).Select(x => new
            {
                Date = x.Key.Value,
                TotalBuy = x.Sum(y => y.Quantity * y.OriginalPrice),
                TotalSell = x.Sum(y => y.Quantity * y.Price)
            }).Select(x => new
            {
                Date= x.Date,
                DoanhThu=x.TotalSell,
                LoiNhuan=x.TotalSell-x.TotalBuy
            });
            return Json(new {Data = result},JsonRequestBehavior.AllowGet);
        }
        public ActionResult QLnhanvien()
        {
            var dsnv = db.NHANVIENs.ToList();
            return View(dsnv);
        }
        [HttpGet]
        public ActionResult ThemUser()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult ThemUser([Bind(Include = "Username, Password, Chucvu")] USER us)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    if (string.IsNullOrEmpty(us.Username) || string.IsNullOrEmpty(us.Password) || string.IsNullOrEmpty(us.Chucvu))
                    {
                        ModelState.AddModelError("", "Vui lòng nhập đủ thông tin.");
                        return View(us);
                    }
                    if (db.USERS.Any(u => u.Username == us.Username))
                    {
                        ModelState.AddModelError("Username", "Username đã tồn tại.");
                        return View(us);
                    }
                    db.USERS.Add(us);
                    db.SaveChanges();
                    int userID = us.ID;
                    return RedirectToAction("ThemNVmoi", new {id=userID});
                }
                catch (Exception ex)
                {
                    ViewBag.Message="Không thành công!!" + ex.ToString();
                }
            }
            return View(us);
        }
        [HttpGet]
        public ActionResult ThemNVmoi(int id)
        {
            var user=db.USERS.Find(id);
            if(user==null)
            {
                return RedirectToAction("Error");
            }    
            var nhanvien=new NHANVIEN { ID = id };
            return View(nhanvien);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult ThemNVmoi([Bind(Include = "TenNV,GioiTinh,NgSinh,DiaChi,Sdt,Luong,ID")] NHANVIEN nhanvien)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    nhanvien.ID = db.USERS.Find(nhanvien.ID).ID;
                    db.NHANVIENs.Add(nhanvien);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    ViewBag.Message = "không thành công!!";
                }

            }
            return View(nhanvien);
        }
        [HttpGet]
        public ActionResult SuaTTNV(int? manv)
        {

            if (manv == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NHANVIEN nv = db.NHANVIENs.Find(manv);
            if (nv == null)
            {
                return HttpNotFound();
            }
            return View(nv);
        }
        [HttpPost, ActionName("SuaTTNV")]
        [ValidateAntiForgeryToken]
        public ActionResult SuaTTNVPost(int? manv)
        {
            if(manv == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var NvienToUpdate = db.NHANVIENs.Find(manv);
            if (TryUpdateModel(NvienToUpdate, "",
               new string[] { "TenNV","GioiTinh","NgSinh","DiaChi","Sdt","Luong"}))
            {
                try
                {
                    db.SaveChanges();
                    return RedirectToAction("QLnhanvien");
                }
                catch
                {
                    ModelState.AddModelError("", "Không thể lưu thay đổi. Hãy thử lại.");
                }
            }
            return View(NvienToUpdate);
        }
        [HttpGet]
        public ActionResult XoaNV(int? manv)
        {
            if (manv == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NHANVIEN Nvien = db.NHANVIENs.Find(manv);
            if (Nvien == null)
            {
                return HttpNotFound();
            }
            return View(Nvien);
        }
        [HttpPost, ActionName("XoaNV")]
        [ValidateAntiForgeryToken]
        public ActionResult XoaNVConfirm(int manv)
        {
            NHANVIEN nv = db.NHANVIENs.Find(manv);
            db.NHANVIENs.Remove(nv);
            USER us = db.USERS.Find(nv.ID);
            db.USERS.Remove(us);
            db.SaveChanges();
            return RedirectToAction("QLnhanvien");
        }
        public ActionResult HoatDong()
        {
            var dsnv = db.NHANVIENs.ToList();
            return View(dsnv);
        }
        public ActionResult GioLamNV(int? maNV, int? month = null)
        {
            // Lấy danh sách các ngày mà nhân viên đã làm trong tháng hiện tại hoặc tháng được chỉ định
            int currentMonth = month ?? DateTime.Now.Month;
            int currentYear = DateTime.Now.Year;

            var ngayLam = db.CHITIETNGAYLAMs.Where(c => c.MaNV == maNV && c.NgayLam.Month == currentMonth && c.NgayLam.Year == currentYear).Distinct().ToList();
            
            ViewBag.MaNV = maNV;
            ViewBag.Month = currentMonth;

            return View(ngayLam);
        }
        public ActionResult ChiTietNgayLam(int maNV, DateTime ngayLam)
        {
            // Lấy thông tin chi tiết về các ca làm việc của nhân viên trong một ngày nhất định
            var chiTiet = db.CHITIETNGAYLAMs.Where(c => c.MaNV == maNV && c.NgayLam == ngayLam).ToList();

            return View(chiTiet);
        }
        public ActionResult SPtheoCa(int maNV, DateTime NgayLam)
        {
            var ds = from pb in db.PHIEUBANs join ct in db.CTPHIEUBANs on pb.MaPB equals ct.MaPB
                     where pb.MaNV == maNV && pb.NgayBan == NgayLam 
                     group ct by new { ct.MaHH, ct.MaQC } into g
                    select new
                    {
                        MaHH = g.Key.MaHH, 
                        MaQC = g.Key.MaQC,
                        TongSoLuong=g.Sum(x => x.SoLuong)
                    };
            return View(ds);
        }
        public ActionResult DanhSachLuong(int? maNV, int? year)
        {
            if (maNV == null || year == null)
            {
                return HttpNotFound();
            }
            var danhSachLuong = db.LUONGs.Where(l => l.MaNV == maNV && l.NgayTinh.Value.Year == year).OrderBy(l => l.NgayTinh).ToList();

            ViewBag.MaNV = maNV;
            return View(danhSachLuong);
        }
        public ActionResult TinhLuong(int? idNV)
        {
            int thang = DateTime.Now.Month-1;
            int nam = DateTime.Now.Year;
            // Tính tổng số ca làm việc của mỗi nhân viên trong tháng
            var tongSoCaTheoNV = from cnl in db.CHITIETNGAYLAMs
                                 where cnl.MaNV == idNV && cnl.NgayLam.Month == thang && cnl.NgayLam.Year == nam
                                 group cnl by cnl.MaNV into g
                                 select new
                                 {
                                     MaNV = g.Key,
                                     TongSoCa = g.Count()
                                 };

            // Tính tổng số ca tối để cộng thêm tiền phụ cấp
            var tongSoCaToi = db.CHITIETNGAYLAMs.Count(cnl => cnl.MaCaTruc == 3 && cnl.MaNV==idNV && cnl.NgayLam.Month == thang && cnl.NgayLam.Year == nam);
            var luong = db.NHANVIENs.Find(idNV);
            var PCtoi=db.PHUCAPs.Find(1);
            // Lưu vào bảng LUONG
            foreach (var nv in tongSoCaTheoNV)
            {
                var tongLuong = (nv.TongSoCa - tongSoCaToi) * luong.Luong + tongSoCaToi * (luong.Luong + PCtoi.SoTien);

                var luongEntity = new LUONG
                {
                    MaNV = nv.MaNV,
                    SoCaSang = nv.TongSoCa-tongSoCaToi,
                    SoCaToi=tongSoCaToi,
                    TongLuongTheoCa = tongLuong,
                    NgayTinh=DateTime.Now.Date,
                    // Các trường thông tin khác của lương
                };

                db.LUONGs.Add(luongEntity);
            }

            db.SaveChanges();

            // Redirect hoặc trả về view thông báo thành công
            return RedirectToAction("DanhSachLuong", "QuanLy", new {maNV = idNV, year = nam}); // Chỉ định action và controller bạn muốn redirect đến
        }
        //ca trực
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
                    return RedirectToAction("CaTruc");
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
        [HttpGet]
        public ActionResult ThemNhanVienVaoCa()
        {
            ViewData["MaCaTruc"] = new SelectList(db.CATRUCs, "MaCaTruc", "TenCaTruc");
            ViewData["MaNV"] = new SelectList(db.NHANVIENs, "MaNV", "TenNV");
            ViewData["MaCot"] = new SelectList(db.COTBOMXANGs, "MaCot", "LoaiNhienlieu");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult ThemNhanVienVaoCa([Bind(Include = "MaCTNL,MaCaTruc,MaNV,NgayLam,MaCot")] CHITIETNGAYLAM ct)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.CHITIETNGAYLAMs.Add(ct);
                    db.SaveChanges();
                    return RedirectToAction("CaTruc");
                }
                catch
                {
                    ViewBag.Message = "không thành công!!";
                }

            }
            ViewData["MaCaTruc"] = new SelectList(db.CATRUCs, "MaCaTruc", "TenCaTruc", ct.CATRUC.MaCaTruc);
            ViewData["MaNV"] = new SelectList(db.NHANVIENs, "MaNV", "TenNV", ct.NHANVIEN.MaNV);
            ViewData["MaCot"] = new SelectList(db.COTBOMXANGs, "MaCot", "LoaiNhienlieu", ct.COTBOMXANG.MaCot);
            return View(ct);
        }
    }
}