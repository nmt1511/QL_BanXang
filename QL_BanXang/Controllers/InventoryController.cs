using QL_BanXang.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.IO;
using OfficeOpenXml;
using System.Data.Entity;
using System.Data;

namespace QL_BanXang.Controllers
{
    public class InventoryController : Controller
    {
        // GET: Inventory
        QL_CayXangEntities db = new QL_CayXangEntities();
        public ActionResult TonKho()
        {
            // Lấy danh sách chi tiết đơn hàng dựa trên MaDH
            var danhSachChiTietDonHang = db.CTGIAs.OrderBy(p => p.SLton);

            if (danhSachChiTietDonHang == null || !danhSachChiTietDonHang.Any())
            {
                return HttpNotFound();
            }

            return View(danhSachChiTietDonHang);
        }
        public ActionResult ExportToExcel()
        {
            var danhSachChiTietDonHang = db.CTGIAs.OrderBy(p => p.SLton).ToList();

            // Tạo một package Excel
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Sản Phẩm Tồn Kho");

                // Đặt tiêu đề cho các cột
                worksheet.Cells[1, 1].Value = "STT";
                worksheet.Cells[1, 2].Value = "Tên Hàng";
                worksheet.Cells[1, 3].Value = "Quy Cách";
                worksheet.Cells[1, 4].Value = "Loại Hàng";
                worksheet.Cells[1, 5].Value = "Giá Nhập";
                worksheet.Cells[1, 6].Value = "Giá Bán";
                worksheet.Cells[1, 7].Value = "Số Lượng Tồn";

                // Đổ dữ liệu vào các ô
                int row = 2; int i = 1;
                foreach (var item in danhSachChiTietDonHang)
                {
                    worksheet.Cells[row, 1].Value = i;
                    worksheet.Cells[row, 2].Value = item.HANGHOA.Ten;
                    worksheet.Cells[row, 3].Value = item.QUYCACH.TenQC;
                    worksheet.Cells[row, 4].Value = item.HANGHOA.LOAIHANG.TenLoai;
                    worksheet.Cells[row, 5].Value = item.GiaNhap;
                    worksheet.Cells[row, 6].Value = item.GiaBan;
                    worksheet.Cells[row, 7].Value = item.SLton;
                    row++; i++;
                }

                // Thêm ngày giờ hiện tại vào file Excel
                worksheet.Cells[row, 1].Value = "Ngày giờ lưu:";
                worksheet.Cells[row, 2].Value = DateTime.Now.ToString(); // Ngày giờ hiện tại

                // Lưu file Excel vào một memory stream
                MemoryStream memoryStream = new MemoryStream();
                package.SaveAs(memoryStream);
                memoryStream.Position = 0;

                // Trả về file Excel
                return File(memoryStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "TonKho.xlsx");
            }

        }
        

    }
}