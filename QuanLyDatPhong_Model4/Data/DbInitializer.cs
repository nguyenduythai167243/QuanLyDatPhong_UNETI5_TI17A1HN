// Họ và tên: Nguyễn Duy Thái
// Mã sinh viên: 23103100052
// Nội dung thực hiện: Module 4 - Khởi tạo dữ liệu mẫu kiểm thử quy trình lưu trú, công suất và trạng thái đặt phòng

using Microsoft.EntityFrameworkCore;
using QuanLyDatPhong_Model4.Models.Entities;

namespace QuanLyDatPhong_Model4.Data
{
    public static class DbInitializer
    {
        public static void Initialize(QuanLyDatPhongDbContext context)
        {
            context.Database.EnsureCreated();

            // Nếu đã có dữ liệu loại phòng thì không seed lại
            if (context.LoaiPhongs.Any())
            {
                return;
            }

            // 1. Khởi tạo 05 Loại phòng
            var loaiPhongs = new List<LoaiPhong>
            {
                new LoaiPhong { TenLoaiPhong = "Standard (STD) - Tiêu chuẩn", MoTa = "Phòng tiêu chuẩn ấm cúng, đầy đủ tiện nghi cơ bản, view thành phố", SucChuaToiDa = 2, GiaThamKhao = 450000, TrangThai = true },
                new LoaiPhong { TenLoaiPhong = "Superior (SUP) - Nâng cấp", MoTa = "Phòng nâng cấp thoáng đãng, nội thất hiện đại, ban công rộng", SucChuaToiDa = 2, GiaThamKhao = 750000, TrangThai = true },
                new LoaiPhong { TenLoaiPhong = "Deluxe (DLX) - Cao cấp", MoTa = "Phòng cao cấp hướng biển, bồn tắm sục, minibar miễn phí", SucChuaToiDa = 3, GiaThamKhao = 1200000, TrangThai = true },
                new LoaiPhong { TenLoaiPhong = "Suite (SUT) - Hạng sang", MoTa = "Phòng tổng thống hạng sang, phòng khách riêng biệt, tiện nghi 5 sao", SucChuaToiDa = 4, GiaThamKhao = 2500000, TrangThai = true },
                new LoaiPhong { TenLoaiPhong = "Family (FAM) - Gia đình", MoTa = "Phòng dành cho gia đình với 2 giường đôi lớn, không gian bếp nhỏ", SucChuaToiDa = 5, GiaThamKhao = 1800000, TrangThai = true }
            };
            context.LoaiPhongs.AddRange(loaiPhongs);
            context.SaveChanges();

            // 2. Khởi tạo 30 Phòng (Tầng 1 đến Tầng 5, có Sẵn sàng, Đang bảo trì, Ngừng kinh doanh)
            var phongs = new List<Phong>();
            int[] loaiPhongIds = loaiPhongs.Select(l => l.MaLoaiPhong).ToArray();

            string[] soPhongs = new string[]
            {
                "P101", "P102", "P103", "P104", "P105", "P106",
                "P201", "P202", "P203", "P204", "P205", "P206",
                "P301", "P302", "P303", "P304", "P305", "P306",
                "P401", "P402", "P403", "P404", "P405", "P406",
                "P501", "P502", "P503", "P504", "P505", "P506"
            };

            for (int i = 0; i < soPhongs.Length; i++)
            {
                int tang = (i / 6) + 1;
                int lpIndex = i % loaiPhongIds.Length;
                int maLp = loaiPhongIds[lpIndex];
                var lp = loaiPhongs[lpIndex];

                string trangThai = "Sẵn sàng";
                if (soPhongs[i] == "P104" || soPhongs[i] == "P305" || soPhongs[i] == "P502")
                {
                    trangThai = "Đang bảo trì";
                }
                else if (soPhongs[i] == "P206" || soPhongs[i] == "P405")
                {
                    trangThai = "Ngừng kinh doanh";
                }

                phongs.Add(new Phong
                {
                    SoPhong = soPhongs[i],
                    MaLoaiPhong = maLp,
                    Tang = tang,
                    SucChua = lp.SucChuaToiDa,
                    GiaMotDem = lp.GiaThamKhao + (tang * 20000), // Tầng cao hơn có phụ phí nhẹ
                    DienTich = 25.0 + (lpIndex * 10),
                    TienNghi = "Điều hòa 2 chiều, TV thông minh 55 inch, Nước nóng lạnh, Wifi 6 tốc độ cao, Két an toàn, Bàn làm việc",
                    MoTa = $"Phòng {soPhongs[i]} nằm tại tầng {tang}, không gian yên tĩnh, sạch sẽ, đón ánh sáng tự nhiên.",
                    TrangThai = trangThai
                });
            }
            context.Phongs.AddRange(phongs);
            context.SaveChanges();

            // 3. Khởi tạo Tài khoản: 02 Admin, 03 Lễ tân, 30 Khách hàng
            var taiKhoans = new List<TaiKhoan>
            {
                // Admin
                new TaiKhoan { TenDangNhap = "admin", MatKhau = "123456", HoTen = "Nguyễn Duy Thái (Admin)", Email = "thaiduy@uneti.edu.vn", VaiTro = "Admin", TrangThai = true },
                new TaiKhoan { TenDangNhap = "admin2", MatKhau = "123456", HoTen = "Trần Quản Trị", Email = "quantri@uneti.edu.vn", VaiTro = "Admin", TrangThai = true },
                // Lễ tân
                new TaiKhoan { TenDangNhap = "letan", MatKhau = "123456", HoTen = "Lê Thị Thu Thảo (Lễ tân trưởng)", Email = "thuthao.letan@hotel.vn", VaiTro = "LeTan", TrangThai = true },
                new TaiKhoan { TenDangNhap = "letan2", MatKhau = "123456", HoTen = "Phạm Minh Tuấn (Lễ tân ca ngày)", Email = "minhtuan.letan@hotel.vn", VaiTro = "LeTan", TrangThai = true },
                new TaiKhoan { TenDangNhap = "letan3", MatKhau = "123456", HoTen = "Vũ Hoàng Yến (Lễ tân ca đêm)", Email = "hoangyen.letan@hotel.vn", VaiTro = "LeTan", TrangThai = true }
            };

            // 30 Tài khoản khách hàng
            for (int i = 1; i <= 30; i++)
            {
                taiKhoans.Add(new TaiKhoan
                {
                    TenDangNhap = $"khach{i:D2}",
                    MatKhau = "123456",
                    HoTen = $"Khách hàng {i:D2}",
                    Email = $"khachhang{i:D2}@gmail.com",
                    VaiTro = "KhachHang",
                    TrangThai = true
                });
            }
            context.TaiKhoans.AddRange(taiKhoans);
            context.SaveChanges();

            // 4. Khởi tạo 30 Khách hàng
            string[] hoDem = { "Nguyễn", "Trần", "Lê", "Phạm", "Hoàng", "Huỳnh", "Phan", "Vũ", "Võ", "Đặng", "Bùi", "Đỗ" };
            string[] tenLot = { "Văn", "Thị", "Đức", "Thành", "Minh", "Thu", "Ngọc", "Hải", "Tuấn", "Mai", "Quang", "Anh" };
            string[] tenChinh = { "An", "Bình", "Cường", "Dũng", "Giang", "Hương", "Khánh", "Linh", "Nam", "Phúc", "Quân", "Sơn", "Tâm", "Vy", "Yến" };

            var khachHangs = new List<KhachHang>();
            var khachTkList = taiKhoans.Where(t => t.VaiTro == "KhachHang").ToList();

            for (int i = 0; i < 30; i++)
            {
                string ho = hoDem[i % hoDem.Length];
                string lot = tenLot[(i * 3) % tenLot.Length];
                string ten = tenChinh[(i * 5) % tenChinh.Length];
                string fullName = $"{ho} {lot} {ten}";

                // Cập nhật họ tên tài khoản cho đồng bộ
                khachTkList[i].HoTen = fullName;

                khachHangs.Add(new KhachHang
                {
                    MaTaiKhoan = khachTkList[i].MaTaiKhoan,
                    HoTen = fullName,
                    NgaySinh = new DateTime(1985 + (i % 20), (i % 12) + 1, (i % 25) + 1),
                    GioiTinh = (i % 2 == 0) ? "Nam" : "Nữ",
                    SoDienThoai = $"09{i:D2}{(123456 + i * 789):D6}",
                    Email = khachTkList[i].Email,
                    DiaChi = $"{10 + i * 2} Phố {(i % 2 == 0 ? "Bà Triệu, Hoàn Kiếm, Hà Nội" : "Nguyễn Trãi, Thanh Xuân, Hà Nội")}",
                    SoGiayToTuyThan = $"001200{i:D6}",
                    QuocTich = (i == 5) ? "Hàn Quốc" : (i == 10) ? "Nhật Bản" : "Việt Nam",
                    TrangThai = true
                });
            }
            context.KhachHangs.AddRange(khachHangs);
            context.SaveChanges();

            // 5. Khởi tạo 45 Đặt phòng (Đa dạng trạng thái, có gối đầu, có quá hạn nhận phòng)
            DateTime today = DateTime.Today;
            var datPhongs = new List<DatPhong>();
            var availablePhongs = phongs.Where(p => p.TrangThai == "Sẵn sàng").ToList();

            // Nhóm 1: "Chờ xác nhận" (8 đơn để Lễ tân thực hiện duyệt/từ chối)
            for (int i = 0; i < 8; i++)
            {
                var p = availablePhongs[i % 5];
                var kh = khachHangs[i];
                DateTime checkIn = today.AddDays(2 + i);
                DateTime checkOut = checkIn.AddDays(2 + (i % 3));
                int soDem = (checkOut - checkIn).Days;

                datPhongs.Add(new DatPhong
                {
                    MaKhachHang = kh.MaKhachHang,
                    MaPhong = p.MaPhong,
                    NgayNhanPhong = checkIn,
                    NgayTraPhong = checkOut,
                    SoNguoi = Math.Min(2, p.SucChua),
                    YeuCauDacBiet = i % 2 == 0 ? "Cần phòng tầng cao, yên tĩnh, chuẩn bị giường đôi" : "Nhận phòng muộn sau 18h",
                    NgayDat = today.AddDays(-1),
                    TongTienDuKien = soDem * p.GiaMotDem,
                    TrangThai = "Chờ xác nhận"
                });
            }

            // Nhóm 2: "Đã xác nhận" (10 đơn: gồm sắp nhận hôm nay, tương lai, và 2 đơn QUÁ HẠN để test cảnh báo)
            // 2 đơn quá hạn nhận phòng (Hôm nay > NgayNhanPhong)
            datPhongs.Add(new DatPhong
            {
                MaKhachHang = khachHangs[8].MaKhachHang,
                MaPhong = availablePhongs[5].MaPhong,
                NgayNhanPhong = today.AddDays(-2), // Đã quá hạn 2 ngày
                NgayTraPhong = today.AddDays(1),
                SoNguoi = 2,
                YeuCauDacBiet = "Khách thông báo có thể tới muộn",
                NgayDat = today.AddDays(-5),
                TongTienDuKien = 3 * availablePhongs[5].GiaMotDem,
                TrangThai = "Đã xác nhận",
                NgayXuLy = today.AddDays(-4),
                GhiChuLeTan = "Đã gọi xác nhận giữ phòng đến 19h"
            });

            datPhongs.Add(new DatPhong
            {
                MaKhachHang = khachHangs[9].MaKhachHang,
                MaPhong = availablePhongs[6].MaPhong,
                NgayNhanPhong = today.AddDays(-1), // Đã quá hạn 1 ngày
                NgayTraPhong = today.AddDays(2),
                SoNguoi = 1,
                YeuCauDacBiet = "Không có",
                NgayDat = today.AddDays(-4),
                TongTienDuKien = 3 * availablePhongs[6].GiaMotDem,
                TrangThai = "Đã xác nhận",
                NgayXuLy = today.AddDays(-3),
                GhiChuLeTan = "Chờ khách đến nhận phòng"
            });

            // 2 đơn nhận phòng HÔM NAY (Sẵn sàng để Lễ tân bấm 'Nhận phòng')
            datPhongs.Add(new DatPhong
            {
                MaKhachHang = khachHangs[10].MaKhachHang,
                MaPhong = availablePhongs[7].MaPhong,
                NgayNhanPhong = today,
                NgayTraPhong = today.AddDays(2),
                SoNguoi = 2,
                YeuCauDacBiet = "Phòng không hút thuốc",
                NgayDat = today.AddDays(-2),
                TongTienDuKien = 2 * availablePhongs[7].GiaMotDem,
                TrangThai = "Đã xác nhận",
                NgayXuLy = today.AddDays(-1),
                GhiChuLeTan = "Khách hẹn 14h check-in"
            });

            datPhongs.Add(new DatPhong
            {
                MaKhachHang = khachHangs[11].MaKhachHang,
                MaPhong = availablePhongs[8].MaPhong,
                NgayNhanPhong = today,
                NgayTraPhong = today.AddDays(3),
                SoNguoi = 2,
                YeuCauDacBiet = "Thêm 01 gối phụ",
                NgayDat = today.AddDays(-3),
                TongTienDuKien = 3 * availablePhongs[8].GiaMotDem,
                TrangThai = "Đã xác nhận",
                NgayXuLy = today.AddDays(-2),
                GhiChuLeTan = "Đã chuẩn bị phòng sạch sẽ"
            });

            // 6 đơn tương lai
            for (int i = 0; i < 6; i++)
            {
                var p = availablePhongs[(i + 9) % availablePhongs.Count];
                var kh = khachHangs[12 + i];
                DateTime checkIn = today.AddDays(3 + i);
                DateTime checkOut = checkIn.AddDays(2);

                datPhongs.Add(new DatPhong
                {
                    MaKhachHang = kh.MaKhachHang,
                    MaPhong = p.MaPhong,
                    NgayNhanPhong = checkIn,
                    NgayTraPhong = checkOut,
                    SoNguoi = 2,
                    YeuCauDacBiet = "View đẹp",
                    NgayDat = today.AddDays(-1),
                    TongTienDuKien = 2 * p.GiaMotDem,
                    TrangThai = "Đã xác nhận",
                    NgayXuLy = today,
                    GhiChuLeTan = "Xác nhận thành công qua điện thoại"
                });
            }

            // Nhóm 3: "Đã nhận phòng" (10 đơn: Đang lưu trú, để test thêm dịch vụ & Trả phòng)
            for (int i = 0; i < 10; i++)
            {
                var p = availablePhongs[(i + 15) % availablePhongs.Count];
                var kh = khachHangs[18 + i];
                DateTime checkIn = today.AddDays(- (i % 3 + 1));
                DateTime checkOut = today.AddDays(i % 2 + 1);
                int soDem = (checkOut - checkIn).Days;

                datPhongs.Add(new DatPhong
                {
                    MaKhachHang = kh.MaKhachHang,
                    MaPhong = p.MaPhong,
                    NgayNhanPhong = checkIn,
                    NgayTraPhong = checkOut,
                    SoNguoi = 2,
                    YeuCauDacBiet = "Đã nhận thẻ phòng đầy đủ",
                    NgayDat = checkIn.AddDays(-3),
                    TongTienDuKien = soDem * p.GiaMotDem,
                    NgayNhanThucTe = checkIn.AddHours(14),
                    TrangThai = "Đã nhận phòng",
                    NgayXuLy = checkIn.AddDays(-1),
                    GhiChuLeTan = "Đã hoàn tất thủ tục nhận phòng và chụp CCCD"
                });
            }

            // Nhóm 4: "Đã trả phòng" (12 đơn: Đã xong, có hóa đơn)
            for (int i = 0; i < 12; i++)
            {
                var p = availablePhongs[i % availablePhongs.Count];
                var kh = khachHangs[i];
                DateTime checkIn = today.AddDays(- (15 + i * 2));
                DateTime checkOut = checkIn.AddDays(2 + (i % 3));
                int soDem = (checkOut - checkIn).Days;

                datPhongs.Add(new DatPhong
                {
                    MaKhachHang = kh.MaKhachHang,
                    MaPhong = p.MaPhong,
                    NgayNhanPhong = checkIn,
                    NgayTraPhong = checkOut,
                    SoNguoi = 2,
                    YeuCauDacBiet = "Không có",
                    NgayDat = checkIn.AddDays(-5),
                    TongTienDuKien = soDem * p.GiaMotDem,
                    NgayNhanThucTe = checkIn.AddHours(13),
                    NgayTraThucTe = checkOut.AddHours(11),
                    TrangThai = "Đã trả phòng",
                    NgayXuLy = checkIn.AddDays(-2),
                    GhiChuLeTan = "Khách đã thanh toán đủ và trả phòng hài lòng"
                });
            }

            // Nhóm 5: Đặt phòng GỐI ĐẦU (Khách A trả ngày X, Khách B nhận ngày X cùng phòng)
            var pGoiDau = availablePhongs[0];
            DateTime ngayGoiDau = today.AddDays(10);
            datPhongs.Add(new DatPhong
            {
                MaKhachHang = khachHangs[0].MaKhachHang,
                MaPhong = pGoiDau.MaPhong,
                NgayNhanPhong = ngayGoiDau.AddDays(-3),
                NgayTraPhong = ngayGoiDau, // Trả ngày 10
                SoNguoi = 2,
                YeuCauDacBiet = "Đặt phòng đợt 1 (trả trưa ngày gối đầu)",
                NgayDat = today.AddDays(-1),
                TongTienDuKien = 3 * pGoiDau.GiaMotDem,
                TrangThai = "Đã xác nhận",
                NgayXuLy = today,
                GhiChuLeTan = "Khách hẹn trả phòng lúc 11h"
            });

            datPhongs.Add(new DatPhong
            {
                MaKhachHang = khachHangs[1].MaKhachHang,
                MaPhong = pGoiDau.MaPhong,
                NgayNhanPhong = ngayGoiDau, // Nhận ngày 10 (GỐI ĐẦU HỢP LỆ)
                NgayTraPhong = ngayGoiDau.AddDays(3),
                SoNguoi = 2,
                YeuCauDacBiet = "Đặt phòng gối đầu đợt 2 (nhận chiều ngày gối đầu)",
                NgayDat = today.AddDays(-1),
                TongTienDuKien = 3 * pGoiDau.GiaMotDem,
                TrangThai = "Đã xác nhận",
                NgayXuLy = today,
                GhiChuLeTan = "Gối đầu hợp lệ: nhận sau 14h ngày gối đầu"
            });

            // Nhóm 6: "Từ chối" (2 đơn) và "Đã hủy" (2 đơn)
            datPhongs.Add(new DatPhong
            {
                MaKhachHang = khachHangs[28].MaKhachHang,
                MaPhong = availablePhongs[1].MaPhong,
                NgayNhanPhong = today.AddDays(5),
                NgayTraPhong = today.AddDays(7),
                SoNguoi = 4,
                YeuCauDacBiet = "Phòng 4 người lớn nhưng chọn phòng đơn",
                NgayDat = today.AddDays(-2),
                TongTienDuKien = 2 * availablePhongs[1].GiaMotDem,
                TrangThai = "Từ chối",
                NgayXuLy = today.AddDays(-1),
                GhiChuLeTan = "Từ chối do số người vượt quá sức chứa tối đa của phòng"
            });

            datPhongs.Add(new DatPhong
            {
                MaKhachHang = khachHangs[29].MaKhachHang,
                MaPhong = availablePhongs[2].MaPhong,
                NgayNhanPhong = today.AddDays(8),
                NgayTraPhong = today.AddDays(10),
                SoNguoi = 2,
                YeuCauDacBiet = "Khách hủy do thay đổi lịch trình",
                NgayDat = today.AddDays(-3),
                TongTienDuKien = 2 * availablePhongs[2].GiaMotDem,
                TrangThai = "Đã hủy",
                NgayXuLy = today.AddDays(-1),
                GhiChuLeTan = "Khách gọi hủy phòng trước hạn quy định"
            });

            context.DatPhongs.AddRange(datPhongs);
            context.SaveChanges();

            // 6. Khởi tạo 25 Dịch vụ phát sinh
            var dichVuList = new List<DichVuPhatSinh>();
            var activeDatPhongs = datPhongs.Where(dp => dp.TrangThai == "Đã nhận phòng" || dp.TrangThai == "Đã trả phòng").ToList();

            string[] tenDv = { "Nước khoáng Lavie 500ml", "Nước ngọt Coca Cola", "Bia Heineken", "Giặt ủi quần áo nhanh", "Bữa sáng Buffet quốc tế", "Đưa đón sân bay 4 chỗ", "Trái cây chào mừng tươi" };
            decimal[] giaDv = { 20000, 25000, 40000, 80000, 150000, 300000, 100000 };

            for (int i = 0; i < 25; i++)
            {
                var dp = activeDatPhongs[i % activeDatPhongs.Count];
                int dvIdx = i % tenDv.Length;
                int soLuong = (i % 3) + 1;

                dichVuList.Add(new DichVuPhatSinh
                {
                    MaDatPhong = dp.MaDatPhong,
                    TenDichVu = tenDv[dvIdx],
                    SoLuong = soLuong,
                    DonGia = giaDv[dvIdx],
                    NgaySuDung = dp.NgayNhanPhong.AddHours(15 + (i % 10)),
                    GhiChu = $"Sử dụng tại phòng {dp.Phong?.SoPhong ?? "khách sạn"}",
                    TrangThai = "Đã ghi nhận"
                });
            }
            context.DichVuPhatSinhs.AddRange(dichVuList);
            context.SaveChanges();

            // 7. Khởi tạo 18 Hóa đơn (cho các đơn Đã trả phòng và 6 đơn Đã thanh toán / Chưa thanh toán)
            var hoaDonList = new List<HoaDon>();
            var daTraPhongs = datPhongs.Where(dp => dp.TrangThai == "Đã trả phòng").ToList();

            for (int i = 0; i < daTraPhongs.Count; i++)
            {
                var dp = daTraPhongs[i];
                int soDem = dp.SoDemDuKien;
                decimal tienPhong = dp.TongTienDuKien;
                decimal tienDv = context.DichVuPhatSinhs.Where(d => d.MaDatPhong == dp.MaDatPhong && d.TrangThai != "Đã hủy").Sum(d => d.SoLuong * d.DonGia);
                decimal giamGia = (i % 3 == 0) ? 50000 : 0;
                decimal tongThanhToan = tienPhong + tienDv - giamGia;

                hoaDonList.Add(new HoaDon
                {
                    MaDatPhong = dp.MaDatPhong,
                    SoDem = soDem,
                    TienPhong = tienPhong,
                    TienDichVu = tienDv,
                    GiamGia = giamGia,
                    TongThanhToan = tongThanhToan,
                    HinhThucThanhToan = (i % 3 == 0) ? "Tiền mặt" : (i % 3 == 1) ? "Chuyển khoản" : "Thẻ",
                    NgayLap = dp.NgayTraThucTe ?? dp.NgayTraPhong,
                    TrangThai = (i % 4 == 0) ? "Chưa thanh toán" : "Đã thanh toán"
                });
            }
            context.HoaDons.AddRange(hoaDonList);
            context.SaveChanges();
        }
    }
}
