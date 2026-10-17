using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace QuanLyDatPhong_UNETI5_TI17A1HN.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateWithFullSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LoaiPhongs",
                columns: table => new
                {
                    MaLoaiPhong = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenLoaiPhong = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    SucChuaToiDa = table.Column<int>(type: "int", nullable: false),
                    GiaThamKhao = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoaiPhongs", x => x.MaLoaiPhong);
                });

            migrationBuilder.CreateTable(
                name: "TaiKhoans",
                columns: table => new
                {
                    MaTaiKhoan = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenDangNhap = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MatKhau = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    HoTen = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    VaiTro = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaiKhoans", x => x.MaTaiKhoan);
                });

            migrationBuilder.CreateTable(
                name: "Phongs",
                columns: table => new
                {
                    MaPhong = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SoPhong = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tang = table.Column<int>(type: "int", nullable: false),
                    SucChua = table.Column<int>(type: "int", nullable: false),
                    GiaMotDem = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    DienTich = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    TienNghi = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    TrangThai = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    MaLoaiPhong = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phongs", x => x.MaPhong);
                    table.ForeignKey(
                        name: "FK_Phongs_LoaiPhongs_MaLoaiPhong",
                        column: x => x.MaLoaiPhong,
                        principalTable: "LoaiPhongs",
                        principalColumn: "MaLoaiPhong",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KhachHangs",
                columns: table => new
                {
                    MaKhachHang = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaTaiKhoan = table.Column<int>(type: "int", nullable: false),
                    HoTen = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NgaySinh = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GioiTinh = table.Column<bool>(type: "bit", nullable: false),
                    SoDienThoai = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DiaChi = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SoGiayToTuyThan = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    QuocTich = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KhachHangs", x => x.MaKhachHang);
                    table.ForeignKey(
                        name: "FK_KhachHangs_TaiKhoans_MaTaiKhoan",
                        column: x => x.MaTaiKhoan,
                        principalTable: "TaiKhoans",
                        principalColumn: "MaTaiKhoan",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DatPhongs",
                columns: table => new
                {
                    MaDatPhong = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaKhachHang = table.Column<int>(type: "int", nullable: false),
                    MaPhong = table.Column<int>(type: "int", nullable: false),
                    NgayNhanPhong = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayTraPhong = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SoNguoi = table.Column<int>(type: "int", nullable: false),
                    YeuCauDacBiet = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    NgayDat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TongTienDuKien = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NgayNhanThucTe = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NgayTraThucTe = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TrangThai = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NgayXuLy = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GhiChuLeTan = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DatPhongs", x => x.MaDatPhong);
                    table.ForeignKey(
                        name: "FK_DatPhongs_KhachHangs_MaKhachHang",
                        column: x => x.MaKhachHang,
                        principalTable: "KhachHangs",
                        principalColumn: "MaKhachHang",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DatPhongs_Phongs_MaPhong",
                        column: x => x.MaPhong,
                        principalTable: "Phongs",
                        principalColumn: "MaPhong",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DichVuPhatSinhs",
                columns: table => new
                {
                    MaDichVuPhatSinh = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaDatPhong = table.Column<int>(type: "int", nullable: false),
                    TenDichVu = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    DonGia = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    NgaySuDung = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    TrangThai = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DichVuPhatSinhs", x => x.MaDichVuPhatSinh);
                    table.ForeignKey(
                        name: "FK_DichVuPhatSinhs_DatPhongs_MaDatPhong",
                        column: x => x.MaDatPhong,
                        principalTable: "DatPhongs",
                        principalColumn: "MaDatPhong",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HoaDons",
                columns: table => new
                {
                    MaHoaDon = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaDatPhong = table.Column<int>(type: "int", nullable: false),
                    SoDem = table.Column<int>(type: "int", nullable: false),
                    TienPhong = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TienDichVu = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GiamGia = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TongTienThanhToan = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    HinhThucThanhToan = table.Column<int>(type: "int", nullable: false),
                    NgayLap = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TrangThai = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoaDons", x => x.MaHoaDon);
                    table.ForeignKey(
                        name: "FK_HoaDons_DatPhongs_MaDatPhong",
                        column: x => x.MaDatPhong,
                        principalTable: "DatPhongs",
                        principalColumn: "MaDatPhong",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "LoaiPhongs",
                columns: new[] { "MaLoaiPhong", "GiaThamKhao", "MoTa", "SucChuaToiDa", "TenLoaiPhong", "TrangThai" },
                values: new object[,]
                {
                    { 1, 500000m, "Phòng tiêu chuẩn ấm cúng, tiện nghi cơ bản, phù hợp 1-2 người", 2, "Standard (STD)", true },
                    { 2, 800000m, "Phòng chất lượng cao, diện tích rộng rãi với tầm nhìn thoáng đãng", 3, "Superior (SUP)", true },
                    { 3, 1200000m, "Phòng cao cấp sang trọng với ban công riêng và bồn tắm nằm", 4, "Deluxe (DLX)", true },
                    { 4, 2000000m, "Phòng thượng hạng đẳng cấp VIP với phòng khách riêng biệt và quầy bar", 4, "Suite (SUT)", true },
                    { 5, 1600000m, "Phòng gia đình diện tích lớn, thiết kế 2 giường đôi thoải mái cho cả nhà", 5, "Family Room (FAM)", true }
                });

            migrationBuilder.InsertData(
                table: "TaiKhoans",
                columns: new[] { "MaTaiKhoan", "Email", "HoTen", "MatKhau", "TenDangNhap", "TrangThai", "VaiTro" },
                values: new object[,]
                {
                    { 1, "admin01@hotel.com", "Quản Trị Viên 1", "admin123", "admin", true, "Admin" },
                    { 2, "admin02@hotel.com", "Quản Trị Viên 2", "admin123", "admin02", true, "Admin" },
                    { 3, "letan01@hotel.com", "Lễ Tân Ca Sáng", "123456", "letan01", true, "LeTan" },
                    { 4, "letan02@hotel.com", "Lễ Tân Ca Chiều", "123456", "letan02", true, "LeTan" },
                    { 5, "letan03@hotel.com", "Lễ Tân Ca Đêm", "123456", "letan03", true, "LeTan" },
                    { 6, "kh01@gmail.com", "Nguyễn Văn An", "123456", "khach01", true, "KhachHang" },
                    { 7, "kh02@gmail.com", "Trần Thị Bình", "123456", "khach02", true, "KhachHang" },
                    { 8, "kh03@gmail.com", "Lê Văn Cường", "123456", "khach03", true, "KhachHang" },
                    { 9, "kh04@gmail.com", "Phạm Thị Dung", "123456", "khach04", true, "KhachHang" },
                    { 10, "kh05@gmail.com", "Hoàng Văn Em", "123456", "khach05", true, "KhachHang" },
                    { 11, "kh06@gmail.com", "Vũ Thị Giang", "123456", "khach06", true, "KhachHang" },
                    { 12, "kh07@gmail.com", "Đặng Văn Hải", "123456", "khach07", true, "KhachHang" },
                    { 13, "kh08@gmail.com", "Bùi Thị Hạnh", "123456", "khach08", true, "KhachHang" },
                    { 14, "kh09@gmail.com", "Đỗ Văn Hùng", "123456", "khach09", true, "KhachHang" },
                    { 15, "kh10@gmail.com", "Hồ Thị Hương", "123456", "khach10", true, "KhachHang" },
                    { 16, "kh11@gmail.com", "Ngô Văn Khoa", "123456", "khach11", true, "KhachHang" },
                    { 17, "kh12@gmail.com", "Dương Thị Lan", "123456", "khach12", true, "KhachHang" },
                    { 18, "kh13@gmail.com", "Lý Văn Minh", "123456", "khach13", true, "KhachHang" },
                    { 19, "kh14@gmail.com", "Mai Thị Nga", "123456", "khach14", true, "KhachHang" },
                    { 20, "kh15@gmail.com", "Trịnh Văn Nam", "123456", "khach15", true, "KhachHang" },
                    { 21, "kh16@gmail.com", "Lương Thị Oanh", "123456", "khach16", true, "KhachHang" },
                    { 22, "kh17@gmail.com", "Phan Văn Phúc", "123456", "khach17", true, "KhachHang" },
                    { 23, "kh18@gmail.com", "Tô Thị Quỳnh", "123456", "khach18", true, "KhachHang" },
                    { 24, "kh19@gmail.com", "Võ Văn Sang", "123456", "khach19", true, "KhachHang" },
                    { 25, "kh20@gmail.com", "Đoàn Thị Thảo", "123456", "khach20", true, "KhachHang" },
                    { 26, "kh21@gmail.com", "Thái Văn Tuấn", "123456", "khach21", true, "KhachHang" },
                    { 27, "kh22@gmail.com", "Lâm Thị Uyên", "123456", "khach22", true, "KhachHang" },
                    { 28, "kh23@gmail.com", "Hà Văn Vinh", "123456", "khach23", true, "KhachHang" },
                    { 29, "kh24@gmail.com", "Cao Thị Xuân", "123456", "khach24", true, "KhachHang" },
                    { 30, "kh25@gmail.com", "Đinh Văn Yên", "123456", "khach25", true, "KhachHang" },
                    { 31, "kh26@gmail.com", "Chu Thị Ánh", "123456", "khach26", true, "KhachHang" },
                    { 32, "kh27@gmail.com", "Tạ Văn Bắc", "123456", "khach27", true, "KhachHang" },
                    { 33, "kh28@gmail.com", "Kiều Thị Chi", "123456", "khach28", true, "KhachHang" },
                    { 34, "kh29@gmail.com", "Nghiêm Văn Đức", "123456", "khach29", true, "KhachHang" },
                    { 35, "kh30@gmail.com", "Hứa Thị Hoa", "123456", "khach30", true, "KhachHang" },
                    { 36, "khoa01@gmail.com", "Phạm Bị Khóa", "123456", "khoa01", false, "KhachHang" }
                });

            migrationBuilder.InsertData(
                table: "KhachHangs",
                columns: new[] { "MaKhachHang", "DiaChi", "Email", "GioiTinh", "HoTen", "MaTaiKhoan", "NgaySinh", "QuocTich", "SoDienThoai", "SoGiayToTuyThan", "TrangThai" },
                values: new object[,]
                {
                    { 1, "Hà Nội", "kh01@gmail.com", true, "Nguyễn Văn An", 6, new DateTime(1990, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "VN", "0901000001", "0010000001", true },
                    { 2, "Đà Nẵng", "kh02@gmail.com", false, "Trần Thị Bình", 7, new DateTime(1991, 5, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "VN", "0901000002", "0010000002", true },
                    { 3, "TP HCM", "kh03@gmail.com", true, "Lê Văn Cường", 8, new DateTime(1992, 5, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "VN", "0901000003", "0010000003", true },
                    { 4, "Hải Phòng", "kh04@gmail.com", false, "Phạm Thị Dung", 9, new DateTime(1993, 5, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "VN", "0901000004", "0010000004", true },
                    { 5, "Cần Thơ", "kh05@gmail.com", true, "Hoàng Văn Em", 10, new DateTime(1994, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "VN", "0901000005", "0010000005", true },
                    { 6, "Nha Trang", "kh06@gmail.com", false, "Vũ Thị Giang", 11, new DateTime(1995, 5, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "VN", "0901000006", "0010000006", true },
                    { 7, "Huế", "kh07@gmail.com", true, "Đặng Văn Hải", 12, new DateTime(1996, 5, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "VN", "0901000007", "0010000007", true },
                    { 8, "Vũng Tàu", "kh08@gmail.com", false, "Bùi Thị Hạnh", 13, new DateTime(1997, 5, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "VN", "0901000008", "0010000008", true },
                    { 9, "Quảng Ninh", "kh09@gmail.com", true, "Đỗ Văn Hùng", 14, new DateTime(1998, 5, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "VN", "0901000009", "0010000009", true },
                    { 10, "Đà Lạt", "kh10@gmail.com", false, "Hồ Thị Hương", 15, new DateTime(1999, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "VN", "0901000010", "0010000010", true },
                    { 11, "Hà Nội", "kh11@gmail.com", true, "Ngô Văn Khoa", 16, new DateTime(1990, 5, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "VN", "0901000011", "0010000011", true },
                    { 12, "Đà Nẵng", "kh12@gmail.com", false, "Dương Thị Lan", 17, new DateTime(1991, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "VN", "0901000012", "0010000012", true },
                    { 13, "TP HCM", "kh13@gmail.com", true, "Lý Văn Minh", 18, new DateTime(1992, 5, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "VN", "0901000013", "0010000013", true },
                    { 14, "Hải Phòng", "kh14@gmail.com", false, "Mai Thị Nga", 19, new DateTime(1993, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "VN", "0901000014", "0010000014", true },
                    { 15, "Cần Thơ", "kh15@gmail.com", true, "Trịnh Văn Nam", 20, new DateTime(1994, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "VN", "0901000015", "0010000015", true },
                    { 16, "Nha Trang", "kh16@gmail.com", false, "Lương Thị Oanh", 21, new DateTime(1995, 5, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "VN", "0901000016", "0010000016", true },
                    { 17, "Huế", "kh17@gmail.com", true, "Phan Văn Phúc", 22, new DateTime(1996, 5, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "VN", "0901000017", "0010000017", true },
                    { 18, "Vũng Tàu", "kh18@gmail.com", false, "Tô Thị Quỳnh", 23, new DateTime(1997, 5, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "VN", "0901000018", "0010000018", true },
                    { 19, "Quảng Ninh", "kh19@gmail.com", true, "Võ Văn Sang", 24, new DateTime(1998, 5, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "VN", "0901000019", "0010000019", true },
                    { 20, "Đà Lạt", "kh20@gmail.com", false, "Đoàn Thị Thảo", 25, new DateTime(1999, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "VN", "0901000020", "0010000020", true },
                    { 21, "Hà Nội", "kh21@gmail.com", true, "Thái Văn Tuấn", 26, new DateTime(1990, 5, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "VN", "0901000021", "0010000021", true },
                    { 22, "Đà Nẵng", "kh22@gmail.com", false, "Lâm Thị Uyên", 27, new DateTime(1991, 5, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "VN", "0901000022", "0010000022", true },
                    { 23, "TP HCM", "kh23@gmail.com", true, "Hà Văn Vinh", 28, new DateTime(1992, 5, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "VN", "0901000023", "0010000023", true },
                    { 24, "Hải Phòng", "kh24@gmail.com", false, "Cao Thị Xuân", 29, new DateTime(1993, 5, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "VN", "0901000024", "0010000024", true },
                    { 25, "Cần Thơ", "kh25@gmail.com", true, "Đinh Văn Yên", 30, new DateTime(1994, 5, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "VN", "0901000025", "0010000025", true },
                    { 26, "Nha Trang", "kh26@gmail.com", false, "Chu Thị Ánh", 31, new DateTime(1995, 5, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "VN", "0901000026", "0010000026", true },
                    { 27, "Huế", "kh27@gmail.com", true, "Tạ Văn Bắc", 32, new DateTime(1996, 5, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "VN", "0901000027", "0010000027", true },
                    { 28, "Vũng Tàu", "kh28@gmail.com", false, "Kiều Thị Chi", 33, new DateTime(1997, 5, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "VN", "0901000028", "0010000028", true },
                    { 29, "Quảng Ninh", "kh29@gmail.com", true, "Nghiêm Văn Đức", 34, new DateTime(1998, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "VN", "0901000029", "0010000029", true },
                    { 30, "Đà Lạt", "kh30@gmail.com", false, "Hứa Thị Hoa", 35, new DateTime(1999, 5, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "VN", "0901000030", "0010000030", true }
                });

            migrationBuilder.InsertData(
                table: "Phongs",
                columns: new[] { "MaPhong", "DienTich", "GiaMotDem", "MaLoaiPhong", "MoTa", "SoPhong", "SucChua", "Tang", "TienNghi", "TrangThai" },
                values: new object[,]
                {
                    { 1, 25m, 500000m, 1, "Phòng Standard tầng 1 view sân vườn", "101", 2, 1, "Wifi, Smart TV, Điều hòa, Nóng lạnh, Minibar", "Sẵn sàng" },
                    { 2, 25m, 500000m, 1, "Phòng Standard tầng 1 yên tĩnh", "102", 2, 1, "Wifi, Smart TV, Điều hòa, Nóng lạnh, Minibar", "Sẵn sàng" },
                    { 3, 25m, 500000m, 1, "Phòng Standard tầng 1 gần sảnh", "103", 2, 1, "Wifi, Smart TV, Điều hòa, Nóng lạnh, Minibar", "Sẵn sàng" },
                    { 4, 35m, 800000m, 2, "Phòng Superior tầng 1 view hồ bơi", "104", 3, 1, "Wifi, Smart TV, Điều hòa, Nóng lạnh, Minibar", "Sẵn sàng" },
                    { 5, 35m, 800000m, 2, "Phòng Superior tầng 1 thoáng mát", "105", 3, 1, "Wifi, Smart TV, Điều hòa, Nóng lạnh, Minibar", "Sẵn sàng" },
                    { 6, 35m, 800000m, 2, "Phòng chuyển đổi làm kho lưu trữ vật tư", "106", 3, 1, "Wifi, Smart TV, Điều hòa, Nóng lạnh, Minibar", "Ngừng kinh doanh" },
                    { 7, 25m, 500000m, 1, "Phòng Standard tầng 2 ban công nhỏ", "201", 2, 2, "Wifi, Smart TV, Điều hòa, Nóng lạnh, Minibar", "Sẵn sàng" },
                    { 8, 25m, 500000m, 1, "Phòng Standard tầng 2 view vườn cây", "202", 2, 2, "Wifi, Smart TV, Điều hòa, Nóng lạnh, Minibar", "Sẵn sàng" },
                    { 9, 35m, 800000m, 2, "Phòng Superior tầng 2 nội thất gỗ sang trọng", "203", 3, 2, "Wifi, Smart TV, Điều hòa, Nóng lạnh, Minibar", "Sẵn sàng" },
                    { 10, 35m, 800000m, 2, "Phòng Superior đang nâng cấp hệ thống điều hòa", "204", 3, 2, "Wifi, Smart TV, Điều hòa, Nóng lạnh, Minibar", "Đang bảo trì" },
                    { 11, 35m, 800000m, 2, "Phòng Superior tầng 2 view phố", "205", 3, 2, "Wifi, Smart TV, Điều hòa, Nóng lạnh, Minibar", "Sẵn sàng" },
                    { 12, 45m, 1200000m, 3, "Phòng Deluxe tầng 2 bồn tắm lớn", "206", 4, 2, "Wifi, Smart TV, Điều hòa, Nóng lạnh, Minibar", "Sẵn sàng" },
                    { 13, 35m, 800000m, 2, "Phòng Superior tầng 3 view thoáng", "301", 3, 3, "Wifi, Smart TV, Điều hòa, Nóng lạnh, Minibar", "Sẵn sàng" },
                    { 14, 35m, 800000m, 2, "Phòng Superior tầng 3 gần thang máy", "302", 3, 3, "Wifi, Smart TV, Điều hòa, Nóng lạnh, Minibar", "Sẵn sàng" },
                    { 15, 45m, 1200000m, 3, "Phòng Deluxe tầng 3 ban công rộng", "303", 4, 3, "Wifi, Smart TV, Điều hòa, Nóng lạnh, Minibar", "Sẵn sàng" },
                    { 16, 45m, 1200000m, 3, "Phòng Deluxe tầng 3 view thành phố", "304", 4, 3, "Wifi, Smart TV, Điều hòa, Nóng lạnh, Minibar", "Sẵn sàng" },
                    { 17, 45m, 1200000m, 3, "Phòng Deluxe đang sơn sửa lại tường", "305", 4, 3, "Wifi, Smart TV, Điều hòa, Nóng lạnh, Minibar", "Đang bảo trì" },
                    { 18, 55m, 1600000m, 5, "Phòng Family tầng 3 không gian ấm cúng", "306", 5, 3, "Wifi, Smart TV, Điều hòa, Nóng lạnh, Minibar", "Sẵn sàng" },
                    { 19, 45m, 1200000m, 3, "Phòng Deluxe tầng 4 view hoàng hôn", "401", 4, 4, "Wifi, Smart TV, Điều hòa, Nóng lạnh, Minibar", "Sẵn sàng" },
                    { 20, 45m, 1200000m, 3, "Phòng Deluxe tầng 4 nội thất hiện đại", "402", 4, 4, "Wifi, Smart TV, Điều hòa, Nóng lạnh, Minibar", "Sẵn sàng" },
                    { 21, 65m, 2000000m, 4, "Phòng Suite VIP tầng 4 có quầy bar mini", "403", 4, 4, "Wifi, Smart TV, Điều hòa, Nóng lạnh, Minibar", "Sẵn sàng" },
                    { 22, 65m, 2000000m, 4, "Phòng Suite VIP tầng 4 ban công panorama", "404", 4, 4, "Wifi, Smart TV, Điều hòa, Nóng lạnh, Minibar", "Sẵn sàng" },
                    { 23, 55m, 1600000m, 5, "Phòng Family tầng 4 đầy đủ giường cho 5 người", "405", 5, 4, "Wifi, Smart TV, Điều hòa, Nóng lạnh, Minibar", "Sẵn sàng" },
                    { 24, 55m, 1600000m, 5, "Phòng chuyển đổi làm khu kỹ thuật tòa nhà", "406", 5, 4, "Wifi, Smart TV, Điều hòa, Nóng lạnh, Minibar", "Ngừng kinh doanh" },
                    { 25, 65m, 2000000m, 4, "Phòng Suite Penthouse tầng thượng cao cấp", "501", 4, 5, "Wifi, Smart TV, Điều hòa, Nóng lạnh, Minibar", "Sẵn sàng" },
                    { 26, 65m, 2000000m, 4, "Phòng Suite đang bảo trì hệ thống bồn sục Jacuzzi", "502", 4, 5, "Wifi, Smart TV, Điều hòa, Nóng lạnh, Minibar", "Đang bảo trì" },
                    { 27, 65m, 2000000m, 4, "Phòng Suite tầng thượng view biển ngoạn mục", "503", 4, 5, "Wifi, Smart TV, Điều hòa, Nóng lạnh, Minibar", "Sẵn sàng" },
                    { 28, 55m, 1600000m, 5, "Phòng Family tầng 5 tiện nghi cao cấp", "504", 5, 5, "Wifi, Smart TV, Điều hòa, Nóng lạnh, Minibar", "Sẵn sàng" },
                    { 29, 55m, 1600000m, 5, "Phòng Family tầng 5 view toàn cảnh thành phố", "505", 5, 5, "Wifi, Smart TV, Điều hòa, Nóng lạnh, Minibar", "Sẵn sàng" },
                    { 30, 55m, 1600000m, 5, "Phòng trưng bày mẫu kiến trúc khách sạn", "506", 5, 5, "Wifi, Smart TV, Điều hòa, Nóng lạnh, Minibar", "Ngừng kinh doanh" }
                });

            migrationBuilder.InsertData(
                table: "DatPhongs",
                columns: new[] { "MaDatPhong", "GhiChuLeTan", "MaKhachHang", "MaPhong", "NgayDat", "NgayNhanPhong", "NgayNhanThucTe", "NgayTraPhong", "NgayTraThucTe", "NgayXuLy", "SoNguoi", "TongTienDuKien", "TrangThai", "YeuCauDacBiet" },
                values: new object[,]
                {
                    { 1, "Gối đầu đợt 1", 1, 1, new DateTime(2026, 9, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 1, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 4, 11, 30, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 9, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 1500000m, "Đã trả phòng", null },
                    { 2, "Gối đầu đợt 2", 2, 1, new DateTime(2026, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 4, 14, 15, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 7, 11, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 1500000m, "Đã trả phòng", null },
                    { 3, "Gối đầu đợt 3", 3, 1, new DateTime(2026, 10, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 7, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 10, 11, 30, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 1500000m, "Đã trả phòng", null },
                    { 4, "Gối đầu đợt 4", 4, 1, new DateTime(2026, 10, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 10, 14, 30, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 13, 11, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 1500000m, "Đã trả phòng", null },
                    { 5, "Khách gia đình", 5, 4, new DateTime(2026, 9, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 2, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 5, 11, 30, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 9, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 2400000m, "Đã trả phòng", null },
                    { 6, "Gối đầu chuỗi 2", 6, 4, new DateTime(2026, 10, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 5, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 8, 11, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 2400000m, "Đã trả phòng", null },
                    { 7, "Gối đầu chuỗi 2", 7, 4, new DateTime(2026, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 8, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 11, 11, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 2400000m, "Đã trả phòng", null },
                    { 8, "Gối đầu chuỗi 2", 8, 4, new DateTime(2026, 10, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 11, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 14, 11, 30, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 2400000m, "Đã trả phòng", null },
                    { 9, "Check-in đúng giờ", 9, 7, new DateTime(2026, 9, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 3, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 6, 11, 30, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 9, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 1500000m, "Đã trả phòng", null },
                    { 10, "Gối đầu chuỗi 3", 10, 7, new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 6, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 9, 11, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 1500000m, "Đã trả phòng", null },
                    { 11, "Gối đầu chuỗi 3", 11, 7, new DateTime(2026, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 9, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 12, 11, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 1500000m, "Đã trả phòng", null },
                    { 12, "Khách công tác", 12, 13, new DateTime(2026, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 3, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 6, 11, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 2400000m, "Đã trả phòng", null },
                    { 13, "Gối đầu chuỗi 4", 13, 13, new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 6, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 10, 11, 30, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 3200000m, "Đã trả phòng", null },
                    { 14, "Gối đầu chuỗi 4", 14, 13, new DateTime(2026, 10, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 10, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 13, 11, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 2400000m, "Đã trả phòng", null },
                    { 15, "Phòng Deluxe VIP", 15, 19, new DateTime(2026, 9, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 2, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 5, 11, 30, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 9, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 3600000m, "Đã trả phòng", null },
                    { 16, "Gối đầu chuỗi 5", 16, 19, new DateTime(2026, 10, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 5, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 9, 11, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 4800000m, "Đã trả phòng", null },
                    { 17, "Gối đầu chuỗi 5", 17, 19, new DateTime(2026, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 9, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 13, 11, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 4800000m, "Đã trả phòng", null },
                    { 18, "Khách VIP Suite", 18, 25, new DateTime(2026, 10, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 10, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 14, 11, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 8000000m, "Đã trả phòng", null },
                    { 19, "Đang lưu trú", 19, 1, new DateTime(2026, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 13, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2026, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 2500000m, "Đã nhận phòng", null },
                    { 20, "Đang lưu trú", 20, 4, new DateTime(2026, 10, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 14, 14, 10, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2026, 10, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 3200000m, "Đã nhận phòng", null },
                    { 21, "Đang lưu trú", 21, 7, new DateTime(2026, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 12, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2026, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 3000000m, "Đã nhận phòng", null },
                    { 22, "Đang lưu trú", 22, 13, new DateTime(2026, 10, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 13, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2026, 10, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 4800000m, "Đã nhận phòng", null },
                    { 23, "Đang lưu trú", 23, 19, new DateTime(2026, 10, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 13, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2026, 10, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 6000000m, "Đã nhận phòng", null },
                    { 24, "Đang lưu trú", 24, 9, new DateTime(2026, 10, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 15, 14, 20, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2026, 10, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 3200000m, "Đã nhận phòng", null },
                    { 25, "Đang lưu trú", 25, 15, new DateTime(2026, 10, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 16, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2026, 10, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 4800000m, "Đã nhận phòng", null },
                    { 26, "Khách VIP Suite", 26, 21, new DateTime(2026, 10, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 16, 15, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2026, 10, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 10000000m, "Đã nhận phòng", null },
                    { 27, "Gối đầu sau đơn 19", 27, 1, new DateTime(2026, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2026, 10, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2026, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 1500000m, "Đã xác nhận", null },
                    { 28, "Gối đầu sau đơn 20", 28, 4, new DateTime(2026, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2026, 10, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2026, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 3200000m, "Đã xác nhận", null },
                    { 29, "Gối đầu sau đơn 21", 29, 7, new DateTime(2026, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2026, 10, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2026, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 1500000m, "Đã xác nhận", null },
                    { 30, "Gối đầu sau đơn 22", 30, 13, new DateTime(2026, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2026, 10, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2026, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 3200000m, "Đã xác nhận", null },
                    { 31, "Gối đầu sau đơn 23", 1, 19, new DateTime(2026, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2026, 10, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2026, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 4800000m, "Đã xác nhận", null },
                    { 32, "Đã cọc 50%", 2, 2, new DateTime(2026, 10, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2026, 10, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2026, 10, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 1000000m, "Đã xác nhận", null },
                    { 33, "Khách thân thiết", 3, 5, new DateTime(2026, 10, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2026, 10, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2026, 10, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 2400000m, "Đã xác nhận", null },
                    { 34, "Đã gọi xác nhận", 4, 11, new DateTime(2026, 10, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2026, 10, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2026, 10, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 2400000m, "Đã xác nhận", null },
                    { 35, "Khách đoàn nhỏ", 5, 17, new DateTime(2026, 10, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2026, 10, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2026, 10, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 4800000m, "Đã xác nhận", null },
                    { 36, "", 6, 1, new DateTime(2026, 10, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2026, 10, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 2, 1500000m, "Chờ xác nhận", null },
                    { 37, "", 7, 4, new DateTime(2026, 10, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2026, 10, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 3, 2400000m, "Chờ xác nhận", null },
                    { 38, "", 8, 8, new DateTime(2026, 10, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2026, 10, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 2, 1500000m, "Chờ xác nhận", null },
                    { 39, "", 9, 14, new DateTime(2026, 10, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2026, 10, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 3, 2400000m, "Chờ xác nhận", null },
                    { 40, "", 10, 20, new DateTime(2026, 10, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2026, 10, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 4, 3600000m, "Chờ xác nhận", null },
                    { 41, "Số người vượt quá sức chứa", 11, 1, new DateTime(2026, 10, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2026, 10, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2026, 10, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 1500000m, "Từ chối", null },
                    { 42, "Phòng đang có khách trùng lịch", 12, 10, new DateTime(2026, 10, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2026, 10, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2026, 10, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 2400000m, "Từ chối", null },
                    { 43, "Phòng đang bảo trì Jacuzzi", 13, 26, new DateTime(2026, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2026, 10, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2026, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 6000000m, "Từ chối", null },
                    { 44, "Khách báo bận công tác đột xuất", 14, 3, new DateTime(2026, 10, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2026, 10, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2026, 10, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 1500000m, "Đã hủy", null },
                    { 45, "Khách thay đổi lịch trình", 15, 16, new DateTime(2026, 10, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2026, 10, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2026, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 3600000m, "Đã hủy", null }
                });

            migrationBuilder.InsertData(
                table: "DichVuPhatSinhs",
                columns: new[] { "MaDichVuPhatSinh", "DonGia", "GhiChu", "MaDatPhong", "NgaySuDung", "SoLuong", "TenDichVu", "TrangThai" },
                values: new object[,]
                {
                    { 1, 20000m, "Minibar phòng 101", 1, new DateTime(2026, 10, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Nước suối khoáng", 0 },
                    { 2, 50000m, "Áo sơ mi", 1, new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Giặt là nhanh", 0 },
                    { 3, 150000m, "Buffet tại nhà hàng", 2, new DateTime(2026, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Ăn sáng buffet", 0 },
                    { 4, 25000m, "Nước ngọt phòng 101", 2, new DateTime(2026, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Coca cola", 0 },
                    { 5, 150000m, "Xe tay ga 1 ngày", 3, new DateTime(2026, 10, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Thuê xe máy", 0 },
                    { 6, 20000m, "Minibar", 3, new DateTime(2026, 10, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Nước suối khoáng", 0 },
                    { 7, 50000m, "Quần âu", 4, new DateTime(2026, 10, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Giặt là nhanh", 0 },
                    { 8, 300000m, "Xe 4 chỗ đón sân bay", 5, new DateTime(2026, 10, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Đưa đón sân bay", 0 },
                    { 9, 45000m, "Quầy bar hồ bơi", 6, new DateTime(2026, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Nước ép cam tươi", 0 },
                    { 10, 35000m, "Minibar", 7, new DateTime(2026, 10, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "Bia lon Heineken", 0 },
                    { 11, 150000m, "Buffet sáng", 8, new DateTime(2026, 10, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Ăn sáng buffet", 0 },
                    { 12, 200000m, "Set trà bánh tại phòng", 9, new DateTime(2026, 10, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Trà chiều set VIP", 0 },
                    { 13, 20000m, "Minibar", 10, new DateTime(2026, 10, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "Nước suối khoáng", 0 },
                    { 14, 350000m, "Dịch vụ Spa khách sạn", 11, new DateTime(2026, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Massage thư giãn", 0 },
                    { 15, 40000m, "Phục vụ tại sảnh", 12, new DateTime(2026, 10, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Nước ép dưa hấu", 0 },
                    { 16, 50000m, "Váy dạ hội", 13, new DateTime(2026, 10, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Giặt là nhanh", 0 },
                    { 17, 30000m, "Cà phê sáng", 14, new DateTime(2026, 10, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Cà phê đen đá", 0 },
                    { 18, 300000m, "Xe 7 chỗ", 15, new DateTime(2026, 10, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Đưa đón sân bay", 0 },
                    { 19, 25000m, "Minibar", 16, new DateTime(2026, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Nước ngọt lon", 0 },
                    { 20, 400000m, "Set bò bít tết", 17, new DateTime(2026, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Bữa tối Room Service", 0 },
                    { 21, 650000m, "Vang Pháp VIP", 18, new DateTime(2026, 10, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Rượu vang đỏ", 0 },
                    { 22, 20000m, "Minibar", 19, new DateTime(2026, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Nước suối khoáng", 0 },
                    { 23, 150000m, "Buffet tại phòng", 20, new DateTime(2026, 10, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Ăn sáng buffet", 0 },
                    { 24, 150000m, "Xe số 1 ngày", 21, new DateTime(2026, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Thuê xe máy", 1 },
                    { 25, 60000m, "Khách hủy giặt", 22, new DateTime(2026, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Dịch vụ giặt ủi", 1 }
                });

            migrationBuilder.InsertData(
                table: "HoaDons",
                columns: new[] { "MaHoaDon", "GiamGia", "HinhThucThanhToan", "MaDatPhong", "NgayLap", "SoDem", "TienDichVu", "TienPhong", "TongTienThanhToan", "TrangThai" },
                values: new object[,]
                {
                    { 1, 0m, 1, 1, new DateTime(2026, 10, 4, 11, 45, 0, 0, DateTimeKind.Unspecified), 3, 90000m, 1500000m, 1590000m, 0 },
                    { 2, 0m, 0, 2, new DateTime(2026, 10, 7, 11, 15, 0, 0, DateTimeKind.Unspecified), 3, 375000m, 1500000m, 1875000m, 0 },
                    { 3, 50000m, 2, 3, new DateTime(2026, 10, 10, 11, 45, 0, 0, DateTimeKind.Unspecified), 3, 190000m, 1500000m, 1640000m, 0 },
                    { 4, 0m, 1, 4, new DateTime(2026, 10, 13, 11, 20, 0, 0, DateTimeKind.Unspecified), 3, 100000m, 1500000m, 1600000m, 0 },
                    { 5, 100000m, 2, 5, new DateTime(2026, 10, 5, 11, 40, 0, 0, DateTimeKind.Unspecified), 3, 300000m, 2400000m, 2600000m, 0 },
                    { 6, 0m, 0, 6, new DateTime(2026, 10, 8, 11, 30, 0, 0, DateTimeKind.Unspecified), 3, 90000m, 2400000m, 2490000m, 0 },
                    { 7, 0m, 1, 7, new DateTime(2026, 10, 11, 11, 15, 0, 0, DateTimeKind.Unspecified), 3, 140000m, 2400000m, 2540000m, 0 },
                    { 8, 50000m, 2, 8, new DateTime(2026, 10, 14, 11, 50, 0, 0, DateTimeKind.Unspecified), 3, 450000m, 2400000m, 2800000m, 0 },
                    { 9, 0m, 1, 9, new DateTime(2026, 10, 6, 11, 35, 0, 0, DateTimeKind.Unspecified), 3, 200000m, 1500000m, 1700000m, 0 },
                    { 10, 0m, 0, 10, new DateTime(2026, 10, 9, 11, 10, 0, 0, DateTimeKind.Unspecified), 3, 80000m, 1500000m, 1580000m, 0 },
                    { 11, 100000m, 2, 11, new DateTime(2026, 10, 12, 11, 25, 0, 0, DateTimeKind.Unspecified), 3, 350000m, 1500000m, 1750000m, 0 },
                    { 12, 0m, 1, 12, new DateTime(2026, 10, 6, 11, 15, 0, 0, DateTimeKind.Unspecified), 3, 80000m, 2400000m, 2480000m, 0 },
                    { 13, 0m, 0, 13, new DateTime(2026, 10, 10, 11, 40, 0, 0, DateTimeKind.Unspecified), 4, 50000m, 3200000m, 3250000m, 0 },
                    { 14, 0m, 1, 14, new DateTime(2026, 10, 13, 11, 10, 0, 0, DateTimeKind.Unspecified), 3, 60000m, 2400000m, 2460000m, 0 },
                    { 15, 0m, 1, 15, new DateTime(2026, 10, 5, 11, 50, 0, 0, DateTimeKind.Unspecified), 3, 300000m, 3600000m, 3900000m, 1 },
                    { 16, 100000m, 2, 16, new DateTime(2026, 10, 9, 11, 15, 0, 0, DateTimeKind.Unspecified), 4, 50000m, 4800000m, 4750000m, 1 },
                    { 17, 200000m, 1, 17, new DateTime(2026, 10, 13, 11, 30, 0, 0, DateTimeKind.Unspecified), 4, 400000m, 4800000m, 5000000m, 1 },
                    { 18, 500000m, 1, 18, new DateTime(2026, 10, 14, 11, 45, 0, 0, DateTimeKind.Unspecified), 4, 650000m, 8000000m, 8150000m, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_DatPhongs_MaKhachHang",
                table: "DatPhongs",
                column: "MaKhachHang");

            migrationBuilder.CreateIndex(
                name: "IX_DatPhongs_MaPhong",
                table: "DatPhongs",
                column: "MaPhong");

            migrationBuilder.CreateIndex(
                name: "IX_DichVuPhatSinhs_MaDatPhong",
                table: "DichVuPhatSinhs",
                column: "MaDatPhong");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDons_MaDatPhong",
                table: "HoaDons",
                column: "MaDatPhong",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_KhachHangs_MaTaiKhoan",
                table: "KhachHangs",
                column: "MaTaiKhoan",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Phongs_MaLoaiPhong",
                table: "Phongs",
                column: "MaLoaiPhong");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DichVuPhatSinhs");

            migrationBuilder.DropTable(
                name: "HoaDons");

            migrationBuilder.DropTable(
                name: "DatPhongs");

            migrationBuilder.DropTable(
                name: "KhachHangs");

            migrationBuilder.DropTable(
                name: "Phongs");

            migrationBuilder.DropTable(
                name: "TaiKhoans");

            migrationBuilder.DropTable(
                name: "LoaiPhongs");
        }
    }
}
