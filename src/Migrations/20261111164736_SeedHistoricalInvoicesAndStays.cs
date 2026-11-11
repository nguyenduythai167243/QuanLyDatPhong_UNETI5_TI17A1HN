using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace QuanLyDatPhong_UNETI5_TI17A1HN.Migrations
{
    /// <inheritdoc />
    public partial class SeedHistoricalInvoicesAndStays : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "DatPhongs",
                columns: new[] { "MaDatPhong", "GhiChuLeTan", "MaKhachHang", "MaPhong", "NgayDat", "NgayNhanPhong", "NgayNhanThucTe", "NgayTraPhong", "NgayTraThucTe", "NgayXuLy", "SoNguoi", "TongTienDuKien", "TrangThai", "YeuCauDacBiet" },
                values: new object[,]
                {
                    { 101, "Lưu trú lịch sử 01/2026", 1, 25, new DateTime(2026, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 1, 5, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 1, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 1, 8, 11, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 6000000m, "Đã trả phòng", null },
                    { 102, "Lưu trú lịch sử 01/2026", 2, 15, new DateTime(2026, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 1, 14, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 1, 17, 11, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 3600000m, "Đã trả phòng", null },
                    { 103, "Lưu trú lịch sử 02/2026", 3, 23, new DateTime(2026, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 2, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 2, 4, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 2, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 2, 8, 11, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 6400000m, "Đã trả phòng", null },
                    { 104, "Lưu trú lịch sử 02/2026", 4, 25, new DateTime(2026, 2, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 2, 15, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 2, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 2, 19, 11, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 2, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 8000000m, "Đã trả phòng", null },
                    { 105, "Lưu trú lịch sử 03/2026", 5, 5, new DateTime(2026, 3, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 3, 6, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 3, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 3, 9, 11, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 3, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 2400000m, "Đã trả phòng", null },
                    { 106, "Lưu trú lịch sử 03/2026", 6, 19, new DateTime(2026, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 3, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 3, 17, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 3, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 3, 21, 11, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 4800000m, "Đã trả phòng", null },
                    { 107, "Lưu trú lịch sử 04/2026", 7, 15, new DateTime(2026, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 4, 5, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 4, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 4, 9, 11, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 4800000m, "Đã trả phòng", null },
                    { 108, "Lưu trú lịch sử 04/2026", 8, 27, new DateTime(2026, 4, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 4, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 4, 18, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 4, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 4, 21, 11, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 4, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 6000000m, "Đã trả phòng", null },
                    { 109, "Lưu trú lịch sử 05/2026", 9, 28, new DateTime(2026, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 5, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 5, 3, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 5, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 5, 7, 11, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 6400000m, "Đã trả phòng", null },
                    { 110, "Lưu trú lịch sử 05/2026", 10, 15, new DateTime(2026, 5, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 5, 15, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 5, 20, 11, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 5, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 6000000m, "Đã trả phòng", null },
                    { 111, "Lưu trú lịch sử 06/2026", 11, 25, new DateTime(2026, 6, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 6, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 6, 4, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 6, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 6, 9, 11, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 6, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 10000000m, "Đã trả phòng", null },
                    { 112, "Lưu trú lịch sử 06/2026", 12, 23, new DateTime(2026, 6, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 6, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 6, 14, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 6, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 6, 19, 11, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 6, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 8000000m, "Đã trả phòng", null },
                    { 113, "Lưu trú lịch sử 07/2026", 13, 27, new DateTime(2026, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 7, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 7, 3, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 7, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 7, 9, 11, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 12000000m, "Đã trả phòng", null },
                    { 114, "Lưu trú lịch sử 07/2026", 14, 19, new DateTime(2026, 7, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 7, 15, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 7, 20, 11, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 7, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 6000000m, "Đã trả phòng", null },
                    { 115, "Lưu trú lịch sử 08/2026", 15, 28, new DateTime(2026, 8, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 8, 5, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 8, 10, 11, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 8, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 8000000m, "Đã trả phòng", null },
                    { 116, "Lưu trú lịch sử 08/2026", 16, 25, new DateTime(2026, 8, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 8, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 8, 16, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 8, 20, 11, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 8, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 8000000m, "Đã trả phòng", null },
                    { 117, "Lưu trú lịch sử 09/2026", 17, 15, new DateTime(2026, 9, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 9, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 9, 4, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 9, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 9, 8, 11, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 9, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 4800000m, "Đã trả phòng", null },
                    { 118, "Lưu trú lịch sử 09/2026", 18, 5, new DateTime(2026, 9, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 9, 15, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 9, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 9, 18, 11, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 9, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 2400000m, "Đã trả phòng", null },
                    { 119, "Lưu trú lịch sử 11/2026", 19, 25, new DateTime(2026, 10, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 11, 2, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 11, 5, 11, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 6000000m, "Đã trả phòng", null },
                    { 120, "Lưu trú lịch sử 11/2026", 20, 23, new DateTime(2026, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 11, 5, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 11, 8, 11, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 4800000m, "Đã trả phòng", null },
                    { 121, "Lưu trú lịch sử 01/2025", 21, 15, new DateTime(2025, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 6, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 10, 11, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 4800000m, "Đã trả phòng", null },
                    { 122, "Lưu trú lịch sử 02/2025", 22, 25, new DateTime(2025, 2, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 2, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 2, 8, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 2, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 2, 13, 11, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 2, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 10000000m, "Đã trả phòng", null },
                    { 123, "Lưu trú lịch sử 03/2025", 23, 23, new DateTime(2025, 3, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 10, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 14, 11, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 6400000m, "Đã trả phòng", null },
                    { 124, "Lưu trú lịch sử 04/2025", 24, 19, new DateTime(2025, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 4, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 4, 7, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 4, 12, 11, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 6000000m, "Đã trả phòng", null },
                    { 125, "Lưu trú lịch sử 05/2025", 25, 27, new DateTime(2025, 5, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 5, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 5, 9, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 5, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 5, 13, 11, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 5, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 8000000m, "Đã trả phòng", null },
                    { 126, "Lưu trú lịch sử 06/2025", 26, 25, new DateTime(2025, 6, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 6, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 6, 6, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 6, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 6, 12, 11, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 6, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 12000000m, "Đã trả phòng", null },
                    { 127, "Lưu trú lịch sử 07/2025", 27, 28, new DateTime(2025, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 7, 10, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 7, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 7, 16, 11, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 9600000m, "Đã trả phòng", null },
                    { 128, "Lưu trú lịch sử 08/2025", 28, 23, new DateTime(2025, 8, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 8, 5, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 8, 10, 11, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 8, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 8000000m, "Đã trả phòng", null },
                    { 129, "Lưu trú lịch sử 09/2025", 29, 15, new DateTime(2025, 9, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 9, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 9, 8, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 9, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 9, 12, 11, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 9, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 4800000m, "Đã trả phòng", null },
                    { 130, "Lưu trú lịch sử 10/2025", 15, 27, new DateTime(2025, 10, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 11, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 16, 11, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 10000000m, "Đã trả phòng", null },
                    { 131, "Lưu trú lịch sử 11/2025", 1, 19, new DateTime(2025, 11, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 11, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 11, 9, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 11, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 11, 13, 11, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 11, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 4800000m, "Đã trả phòng", null },
                    { 132, "Lưu trú lịch sử 12/2025", 2, 25, new DateTime(2025, 12, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 12, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 12, 4, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 12, 10, 11, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 12, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 12000000m, "Đã trả phòng", null },
                    { 133, "Lưu trú lịch sử 12/2025", 3, 5, new DateTime(2025, 12, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 12, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 12, 16, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 12, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 12, 19, 11, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 12, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 2400000m, "Đã trả phòng", null },
                    { 134, "Lưu trú lịch sử 01/2024", 4, 15, new DateTime(2024, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 1, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 1, 8, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 1, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 1, 11, 11, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 3600000m, "Đã trả phòng", null },
                    { 135, "Lưu trú lịch sử 02/2024", 5, 23, new DateTime(2024, 2, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 2, 10, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 2, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 2, 14, 11, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 2, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 6400000m, "Đã trả phòng", null },
                    { 136, "Lưu trú lịch sử 03/2024", 6, 5, new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 3, 12, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 3, 15, 11, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 2400000m, "Đã trả phòng", null },
                    { 137, "Lưu trú lịch sử 04/2024", 7, 19, new DateTime(2024, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 4, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 4, 6, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 4, 10, 11, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 4800000m, "Đã trả phòng", null },
                    { 138, "Lưu trú lịch sử 05/2024", 8, 25, new DateTime(2024, 5, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 5, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 5, 9, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 5, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 5, 13, 11, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 5, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 8000000m, "Đã trả phòng", null },
                    { 139, "Lưu trú lịch sử 06/2024", 9, 28, new DateTime(2024, 6, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 6, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 6, 8, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 6, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 6, 13, 11, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 6, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 8000000m, "Đã trả phòng", null },
                    { 140, "Lưu trú lịch sử 07/2024", 10, 27, new DateTime(2024, 7, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 7, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 7, 11, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 7, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 7, 16, 11, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 7, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 10000000m, "Đã trả phòng", null },
                    { 141, "Lưu trú lịch sử 08/2024", 11, 23, new DateTime(2024, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 8, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 8, 7, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 8, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 8, 11, 11, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 6400000m, "Đã trả phòng", null },
                    { 142, "Lưu trú lịch sử 09/2024", 12, 15, new DateTime(2024, 9, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 9, 10, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 9, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 9, 13, 11, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 9, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 3600000m, "Đã trả phòng", null },
                    { 143, "Lưu trú lịch sử 10/2024", 13, 25, new DateTime(2024, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 12, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 16, 11, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 8000000m, "Đã trả phòng", null },
                    { 144, "Lưu trú lịch sử 11/2024", 14, 19, new DateTime(2024, 11, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 6, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 9, 11, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 3600000m, "Đã trả phòng", null },
                    { 145, "Lưu trú lịch sử 12/2024", 15, 27, new DateTime(2024, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 12, 5, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 12, 10, 11, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 10000000m, "Đã trả phòng", null },
                    { 146, "Lưu trú lịch sử 12/2024", 16, 2, new DateTime(2024, 12, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 12, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 12, 18, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 12, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 12, 21, 11, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 12, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 1500000m, "Đã trả phòng", null },
                    { 147, "Lưu trú lịch sử 01/2023", 17, 5, new DateTime(2023, 1, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 1, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 1, 9, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 1, 12, 11, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 1, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 2400000m, "Đã trả phòng", null },
                    { 148, "Lưu trú lịch sử 02/2023", 18, 15, new DateTime(2023, 2, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 2, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 2, 11, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 2, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 2, 14, 11, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 2, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 3600000m, "Đã trả phòng", null },
                    { 149, "Lưu trú lịch sử 03/2023", 19, 2, new DateTime(2023, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 3, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 3, 7, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 3, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 3, 11, 11, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 2000000m, "Đã trả phòng", null },
                    { 150, "Lưu trú lịch sử 04/2023", 20, 23, new DateTime(2023, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 4, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 4, 14, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 4, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 4, 17, 11, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 4800000m, "Đã trả phòng", null },
                    { 151, "Lưu trú lịch sử 05/2023", 21, 19, new DateTime(2023, 5, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 5, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 5, 6, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 5, 10, 11, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 5, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 4800000m, "Đã trả phòng", null },
                    { 152, "Lưu trú lịch sử 06/2023", 22, 25, new DateTime(2023, 6, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 6, 10, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 6, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 6, 14, 11, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 6, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 8000000m, "Đã trả phòng", null },
                    { 153, "Lưu trú lịch sử 07/2023", 23, 28, new DateTime(2023, 7, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 7, 8, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 7, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 7, 13, 11, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 7, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 8000000m, "Đã trả phòng", null },
                    { 154, "Lưu trú lịch sử 08/2023", 24, 15, new DateTime(2023, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 8, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 8, 12, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 8, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 8, 16, 11, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 4800000m, "Đã trả phòng", null },
                    { 155, "Lưu trú lịch sử 09/2023", 25, 5, new DateTime(2023, 9, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 9, 5, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 9, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 9, 8, 11, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 9, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 2400000m, "Đã trả phòng", null },
                    { 156, "Lưu trú lịch sử 10/2023", 26, 23, new DateTime(2023, 10, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 10, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 10, 9, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 10, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 10, 13, 11, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 10, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 6400000m, "Đã trả phòng", null },
                    { 157, "Lưu trú lịch sử 11/2023", 27, 19, new DateTime(2023, 11, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 11, 11, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 11, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 11, 14, 11, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 11, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 3600000m, "Đã trả phòng", null },
                    { 158, "Lưu trú lịch sử 12/2023", 28, 25, new DateTime(2023, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 12, 7, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 12, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 12, 11, 11, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 8000000m, "Đã trả phòng", null },
                    { 159, "Lưu trú lịch sử 12/2023", 29, 2, new DateTime(2023, 12, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 12, 17, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 12, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 12, 20, 11, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 12, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 1500000m, "Đã trả phòng", null },
                    { 160, "Lưu trú lịch sử 01/2022", 14, 2, new DateTime(2022, 1, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 1, 10, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 1, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 1, 13, 11, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 1, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 1500000m, "Đã trả phòng", null },
                    { 161, "Lưu trú lịch sử 02/2022", 1, 5, new DateTime(2022, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 2, 12, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 2, 15, 11, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 2400000m, "Đã trả phòng", null },
                    { 162, "Lưu trú lịch sử 03/2022", 2, 15, new DateTime(2022, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 3, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 3, 8, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 3, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 3, 11, 11, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 3600000m, "Đã trả phòng", null },
                    { 163, "Lưu trú lịch sử 04/2022", 3, 5, new DateTime(2022, 4, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 4, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 4, 11, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 4, 15, 11, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 4, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 3200000m, "Đã trả phòng", null },
                    { 164, "Lưu trú lịch sử 05/2022", 4, 19, new DateTime(2022, 5, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 5, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 5, 9, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 5, 12, 11, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 5, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 3600000m, "Đã trả phòng", null },
                    { 165, "Lưu trú lịch sử 06/2022", 5, 23, new DateTime(2022, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 6, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 6, 7, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 6, 11, 11, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 6400000m, "Đã trả phòng", null },
                    { 166, "Lưu trú lịch sử 07/2022", 6, 25, new DateTime(2022, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 7, 12, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 7, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 7, 16, 11, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 8000000m, "Đã trả phòng", null },
                    { 167, "Lưu trú lịch sử 08/2022", 7, 15, new DateTime(2022, 8, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 8, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 8, 6, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 8, 10, 11, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 8, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 4800000m, "Đã trả phòng", null },
                    { 168, "Lưu trú lịch sử 09/2022", 8, 5, new DateTime(2022, 9, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 9, 10, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 9, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 9, 13, 11, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 9, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 2400000m, "Đã trả phòng", null },
                    { 169, "Lưu trú lịch sử 10/2022", 9, 23, new DateTime(2022, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 10, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 10, 8, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 10, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 10, 11, 11, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 4800000m, "Đã trả phòng", null },
                    { 170, "Lưu trú lịch sử 11/2022", 10, 19, new DateTime(2022, 11, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 11, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 11, 14, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 11, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 11, 17, 11, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 11, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 3600000m, "Đã trả phòng", null },
                    { 171, "Lưu trú lịch sử 12/2022", 11, 25, new DateTime(2022, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 12, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 12, 9, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 12, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 12, 13, 11, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 8000000m, "Đã trả phòng", null },
                    { 172, "Lưu trú lịch sử 12/2022", 12, 2, new DateTime(2022, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 12, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 12, 19, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 12, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 12, 22, 11, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 1500000m, "Đã trả phòng", null }
                });

            migrationBuilder.InsertData(
                table: "DichVuPhatSinhs",
                columns: new[] { "MaDichVuPhatSinh", "DonGia", "GhiChu", "MaDatPhong", "NgaySuDung", "SoLuong", "TenDichVu", "TrangThai" },
                values: new object[,]
                {
                    { 101, 300000m, "Dịch vụ kỳ 01/2026", 101, new DateTime(2026, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Đưa đón sân bay", 0 },
                    { 102, 150000m, "Dịch vụ kỳ 01/2026", 102, new DateTime(2026, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Ăn sáng buffet", 0 },
                    { 103, 400000m, "Dịch vụ kỳ 02/2026", 103, new DateTime(2026, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Bữa tối Room Service", 0 },
                    { 104, 650000m, "Dịch vụ kỳ 02/2026", 104, new DateTime(2026, 2, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Rượu vang đỏ", 0 },
                    { 105, 50000m, "Dịch vụ kỳ 03/2026", 105, new DateTime(2026, 3, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Giặt là nhanh", 0 },
                    { 106, 350000m, "Dịch vụ kỳ 03/2026", 106, new DateTime(2026, 3, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Massage thư giãn", 0 },
                    { 107, 150000m, "Dịch vụ kỳ 04/2026", 107, new DateTime(2026, 4, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Ăn sáng buffet", 0 },
                    { 108, 300000m, "Dịch vụ kỳ 04/2026", 108, new DateTime(2026, 4, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Đưa đón sân bay", 0 },
                    { 109, 200000m, "Dịch vụ kỳ 05/2026", 109, new DateTime(2026, 5, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Trà chiều set VIP", 0 },
                    { 110, 150000m, "Dịch vụ kỳ 05/2026", 110, new DateTime(2026, 5, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Thuê xe máy", 0 },
                    { 111, 650000m, "Dịch vụ kỳ 06/2026", 111, new DateTime(2026, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Rượu vang đỏ", 0 },
                    { 112, 150000m, "Dịch vụ kỳ 06/2026", 112, new DateTime(2026, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "Ăn sáng buffet", 0 },
                    { 113, 400000m, "Dịch vụ kỳ 07/2026", 113, new DateTime(2026, 7, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Bữa tối Room Service", 0 },
                    { 114, 350000m, "Dịch vụ kỳ 07/2026", 114, new DateTime(2026, 7, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Massage thư giãn", 0 },
                    { 115, 300000m, "Dịch vụ kỳ 08/2026", 115, new DateTime(2026, 8, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Đưa đón sân bay", 0 },
                    { 116, 200000m, "Dịch vụ kỳ 08/2026", 116, new DateTime(2026, 8, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Trà chiều set VIP", 0 },
                    { 117, 150000m, "Dịch vụ kỳ 09/2026", 117, new DateTime(2026, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Ăn sáng buffet", 0 },
                    { 118, 50000m, "Dịch vụ kỳ 09/2026", 118, new DateTime(2026, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Giặt là nhanh", 0 },
                    { 119, 300000m, "Dịch vụ kỳ 11/2026", 119, new DateTime(2026, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Đưa đón sân bay", 0 },
                    { 120, 150000m, "Dịch vụ kỳ 11/2026", 120, new DateTime(2026, 11, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Ăn sáng buffet", 0 },
                    { 121, 150000m, "Dịch vụ kỳ 01/2025", 121, new DateTime(2025, 1, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Ăn sáng buffet", 0 },
                    { 122, 650000m, "Dịch vụ kỳ 02/2025", 122, new DateTime(2025, 2, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Rượu vang đỏ", 0 },
                    { 123, 50000m, "Dịch vụ kỳ 03/2025", 123, new DateTime(2025, 3, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Giặt là nhanh", 0 },
                    { 124, 350000m, "Dịch vụ kỳ 04/2025", 124, new DateTime(2025, 4, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Massage thư giãn", 0 },
                    { 125, 300000m, "Dịch vụ kỳ 05/2025", 125, new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Đưa đón sân bay", 0 },
                    { 126, 400000m, "Dịch vụ kỳ 06/2025", 126, new DateTime(2025, 6, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Bữa tối Room Service", 0 },
                    { 127, 150000m, "Dịch vụ kỳ 07/2025", 127, new DateTime(2025, 7, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "Ăn sáng buffet", 0 },
                    { 128, 200000m, "Dịch vụ kỳ 08/2025", 128, new DateTime(2025, 8, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Trà chiều set VIP", 0 },
                    { 129, 150000m, "Dịch vụ kỳ 09/2025", 129, new DateTime(2025, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Thuê xe máy", 0 },
                    { 130, 300000m, "Dịch vụ kỳ 10/2025", 130, new DateTime(2025, 10, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Đưa đón sân bay", 0 },
                    { 131, 350000m, "Dịch vụ kỳ 11/2025", 131, new DateTime(2025, 11, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Massage thư giãn", 0 },
                    { 132, 650000m, "Dịch vụ kỳ 12/2025", 132, new DateTime(2025, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Rượu vang đỏ", 0 },
                    { 133, 20000m, "Dịch vụ kỳ 12/2025", 133, new DateTime(2025, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "Nước suối khoáng", 0 },
                    { 134, 150000m, "Dịch vụ kỳ 01/2024", 134, new DateTime(2024, 1, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Ăn sáng buffet", 0 },
                    { 135, 400000m, "Dịch vụ kỳ 02/2024", 135, new DateTime(2024, 2, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Bữa tối Room Service", 0 },
                    { 136, 50000m, "Dịch vụ kỳ 03/2024", 136, new DateTime(2024, 3, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Giặt là nhanh", 0 },
                    { 137, 350000m, "Dịch vụ kỳ 04/2024", 137, new DateTime(2024, 4, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Massage thư giãn", 0 },
                    { 138, 300000m, "Dịch vụ kỳ 05/2024", 138, new DateTime(2024, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Đưa đón sân bay", 0 },
                    { 139, 150000m, "Dịch vụ kỳ 06/2024", 139, new DateTime(2024, 6, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Ăn sáng buffet", 0 },
                    { 140, 650000m, "Dịch vụ kỳ 07/2024", 140, new DateTime(2024, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Rượu vang đỏ", 0 },
                    { 141, 200000m, "Dịch vụ kỳ 08/2024", 141, new DateTime(2024, 8, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Trà chiều set VIP", 0 },
                    { 142, 150000m, "Dịch vụ kỳ 09/2024", 142, new DateTime(2024, 9, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Thuê xe máy", 0 },
                    { 143, 300000m, "Dịch vụ kỳ 10/2024", 143, new DateTime(2024, 10, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Đưa đón sân bay", 0 },
                    { 144, 150000m, "Dịch vụ kỳ 11/2024", 144, new DateTime(2024, 11, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Ăn sáng buffet", 0 },
                    { 145, 400000m, "Dịch vụ kỳ 12/2024", 145, new DateTime(2024, 12, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Bữa tối Room Service", 0 },
                    { 146, 20000m, "Dịch vụ kỳ 12/2024", 146, new DateTime(2024, 12, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Nước suối khoáng", 0 },
                    { 147, 150000m, "Dịch vụ kỳ 01/2023", 147, new DateTime(2023, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Ăn sáng buffet", 0 },
                    { 148, 300000m, "Dịch vụ kỳ 02/2023", 148, new DateTime(2023, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Đưa đón sân bay", 0 },
                    { 149, 50000m, "Dịch vụ kỳ 03/2023", 149, new DateTime(2023, 3, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Giặt là nhanh", 0 },
                    { 150, 350000m, "Dịch vụ kỳ 04/2023", 150, new DateTime(2023, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Massage thư giãn", 0 },
                    { 151, 150000m, "Dịch vụ kỳ 05/2023", 151, new DateTime(2023, 5, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Ăn sáng buffet", 0 },
                    { 152, 400000m, "Dịch vụ kỳ 06/2023", 152, new DateTime(2023, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Bữa tối Room Service", 0 },
                    { 153, 150000m, "Dịch vụ kỳ 07/2023", 153, new DateTime(2023, 7, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Thuê xe máy", 0 },
                    { 154, 200000m, "Dịch vụ kỳ 08/2023", 154, new DateTime(2023, 8, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Trà chiều set VIP", 0 },
                    { 155, 45000m, "Dịch vụ kỳ 09/2023", 155, new DateTime(2023, 9, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "Nước ép cam tươi", 0 },
                    { 156, 300000m, "Dịch vụ kỳ 10/2023", 156, new DateTime(2023, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Đưa đón sân bay", 0 },
                    { 157, 50000m, "Dịch vụ kỳ 11/2023", 157, new DateTime(2023, 11, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Giặt là nhanh", 0 },
                    { 158, 650000m, "Dịch vụ kỳ 12/2023", 158, new DateTime(2023, 12, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Rượu vang đỏ", 0 },
                    { 159, 25000m, "Dịch vụ kỳ 12/2023", 159, new DateTime(2023, 12, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "Coca cola", 0 },
                    { 160, 20000m, "Dịch vụ kỳ 01/2022", 160, new DateTime(2022, 1, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Nước suối khoáng", 0 },
                    { 161, 150000m, "Dịch vụ kỳ 02/2022", 161, new DateTime(2022, 2, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Ăn sáng buffet", 0 },
                    { 162, 50000m, "Dịch vụ kỳ 03/2022", 162, new DateTime(2022, 3, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Giặt là nhanh", 0 },
                    { 163, 150000m, "Dịch vụ kỳ 04/2022", 163, new DateTime(2022, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Thuê xe máy", 0 },
                    { 164, 300000m, "Dịch vụ kỳ 05/2022", 164, new DateTime(2022, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Đưa đón sân bay", 0 },
                    { 165, 150000m, "Dịch vụ kỳ 06/2022", 165, new DateTime(2022, 6, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Ăn sáng buffet", 0 },
                    { 166, 350000m, "Dịch vụ kỳ 07/2022", 166, new DateTime(2022, 7, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Massage thư giãn", 0 },
                    { 167, 400000m, "Dịch vụ kỳ 08/2022", 167, new DateTime(2022, 8, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Bữa tối Room Service", 0 },
                    { 168, 30000m, "Dịch vụ kỳ 09/2022", 168, new DateTime(2022, 9, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Cà phê đen đá", 0 },
                    { 169, 200000m, "Dịch vụ kỳ 10/2022", 169, new DateTime(2022, 10, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Trà chiều set VIP", 0 },
                    { 170, 50000m, "Dịch vụ kỳ 11/2022", 170, new DateTime(2022, 11, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Giặt là nhanh", 0 },
                    { 171, 300000m, "Dịch vụ kỳ 12/2022", 171, new DateTime(2022, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Đưa đón sân bay", 0 },
                    { 172, 20000m, "Dịch vụ kỳ 12/2022", 172, new DateTime(2022, 12, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Nước suối khoáng", 0 }
                });

            migrationBuilder.InsertData(
                table: "HoaDons",
                columns: new[] { "MaHoaDon", "GiamGia", "HinhThucThanhToan", "MaDatPhong", "NgayLap", "SoDem", "TienDichVu", "TienPhong", "TongTienThanhToan", "TrangThai" },
                values: new object[,]
                {
                    { 101, 0m, 1, 101, new DateTime(2026, 1, 8, 11, 20, 0, 0, DateTimeKind.Unspecified), 3, 300000m, 6000000m, 6300000m, 0 },
                    { 102, 100000m, 2, 102, new DateTime(2026, 1, 17, 11, 20, 0, 0, DateTimeKind.Unspecified), 3, 450000m, 3600000m, 3950000m, 0 },
                    { 103, 0m, 1, 103, new DateTime(2026, 2, 8, 11, 20, 0, 0, DateTimeKind.Unspecified), 4, 800000m, 6400000m, 7200000m, 0 },
                    { 104, 200000m, 2, 104, new DateTime(2026, 2, 19, 11, 20, 0, 0, DateTimeKind.Unspecified), 4, 650000m, 8000000m, 8450000m, 0 },
                    { 105, 0m, 0, 105, new DateTime(2026, 3, 9, 11, 20, 0, 0, DateTimeKind.Unspecified), 3, 100000m, 2400000m, 2500000m, 0 },
                    { 106, 0m, 1, 106, new DateTime(2026, 3, 21, 11, 20, 0, 0, DateTimeKind.Unspecified), 4, 350000m, 4800000m, 5150000m, 0 },
                    { 107, 0m, 2, 107, new DateTime(2026, 4, 9, 11, 20, 0, 0, DateTimeKind.Unspecified), 4, 300000m, 4800000m, 5100000m, 0 },
                    { 108, 100000m, 1, 108, new DateTime(2026, 4, 21, 11, 20, 0, 0, DateTimeKind.Unspecified), 3, 300000m, 6000000m, 6200000m, 0 },
                    { 109, 0m, 1, 109, new DateTime(2026, 5, 7, 11, 20, 0, 0, DateTimeKind.Unspecified), 4, 400000m, 6400000m, 6800000m, 0 },
                    { 110, 100000m, 0, 110, new DateTime(2026, 5, 20, 11, 20, 0, 0, DateTimeKind.Unspecified), 5, 300000m, 6000000m, 6200000m, 0 },
                    { 111, 0m, 1, 111, new DateTime(2026, 6, 9, 11, 20, 0, 0, DateTimeKind.Unspecified), 5, 650000m, 10000000m, 10650000m, 0 },
                    { 112, 200000m, 2, 112, new DateTime(2026, 6, 19, 11, 20, 0, 0, DateTimeKind.Unspecified), 5, 600000m, 8000000m, 8400000m, 0 },
                    { 113, 0m, 1, 113, new DateTime(2026, 7, 9, 11, 20, 0, 0, DateTimeKind.Unspecified), 6, 800000m, 12000000m, 12800000m, 0 },
                    { 114, 150000m, 2, 114, new DateTime(2026, 7, 20, 11, 20, 0, 0, DateTimeKind.Unspecified), 5, 700000m, 6000000m, 6550000m, 0 },
                    { 115, 0m, 1, 115, new DateTime(2026, 8, 10, 11, 20, 0, 0, DateTimeKind.Unspecified), 5, 300000m, 8000000m, 8300000m, 0 },
                    { 116, 100000m, 0, 116, new DateTime(2026, 8, 20, 11, 20, 0, 0, DateTimeKind.Unspecified), 4, 400000m, 8000000m, 8300000m, 0 },
                    { 117, 0m, 1, 117, new DateTime(2026, 9, 8, 11, 20, 0, 0, DateTimeKind.Unspecified), 4, 450000m, 4800000m, 5250000m, 0 },
                    { 118, 0m, 2, 118, new DateTime(2026, 9, 18, 11, 20, 0, 0, DateTimeKind.Unspecified), 3, 100000m, 2400000m, 2500000m, 1 },
                    { 119, 0m, 1, 119, new DateTime(2026, 11, 5, 11, 20, 0, 0, DateTimeKind.Unspecified), 3, 300000m, 6000000m, 6300000m, 0 },
                    { 120, 0m, 2, 120, new DateTime(2026, 11, 8, 11, 20, 0, 0, DateTimeKind.Unspecified), 3, 300000m, 4800000m, 5100000m, 0 },
                    { 121, 0m, 1, 121, new DateTime(2025, 1, 10, 11, 20, 0, 0, DateTimeKind.Unspecified), 4, 300000m, 4800000m, 5100000m, 0 },
                    { 122, 150000m, 2, 122, new DateTime(2025, 2, 13, 11, 20, 0, 0, DateTimeKind.Unspecified), 5, 650000m, 10000000m, 10500000m, 0 },
                    { 123, 0m, 0, 123, new DateTime(2025, 3, 14, 11, 20, 0, 0, DateTimeKind.Unspecified), 4, 150000m, 6400000m, 6550000m, 0 },
                    { 124, 0m, 1, 124, new DateTime(2025, 4, 12, 11, 20, 0, 0, DateTimeKind.Unspecified), 5, 350000m, 6000000m, 6350000m, 0 },
                    { 125, 100000m, 2, 125, new DateTime(2025, 5, 13, 11, 20, 0, 0, DateTimeKind.Unspecified), 4, 300000m, 8000000m, 8200000m, 0 },
                    { 126, 0m, 1, 126, new DateTime(2025, 6, 12, 11, 20, 0, 0, DateTimeKind.Unspecified), 6, 800000m, 12000000m, 12800000m, 0 },
                    { 127, 0m, 1, 127, new DateTime(2025, 7, 16, 11, 20, 0, 0, DateTimeKind.Unspecified), 6, 600000m, 9600000m, 10200000m, 0 },
                    { 128, 100000m, 2, 128, new DateTime(2025, 8, 10, 11, 20, 0, 0, DateTimeKind.Unspecified), 5, 400000m, 8000000m, 8300000m, 0 },
                    { 129, 0m, 0, 129, new DateTime(2025, 9, 12, 11, 20, 0, 0, DateTimeKind.Unspecified), 4, 300000m, 4800000m, 5100000m, 0 },
                    { 130, 0m, 1, 130, new DateTime(2025, 10, 16, 11, 20, 0, 0, DateTimeKind.Unspecified), 5, 300000m, 10000000m, 10300000m, 0 },
                    { 131, 0m, 2, 131, new DateTime(2025, 11, 13, 11, 20, 0, 0, DateTimeKind.Unspecified), 4, 350000m, 4800000m, 5150000m, 0 },
                    { 132, 200000m, 1, 132, new DateTime(2025, 12, 10, 11, 20, 0, 0, DateTimeKind.Unspecified), 6, 650000m, 12000000m, 12450000m, 0 },
                    { 133, 0m, 0, 133, new DateTime(2025, 12, 19, 11, 20, 0, 0, DateTimeKind.Unspecified), 3, 80000m, 2400000m, 2480000m, 1 },
                    { 134, 0m, 1, 134, new DateTime(2024, 1, 11, 11, 20, 0, 0, DateTimeKind.Unspecified), 3, 300000m, 3600000m, 3900000m, 0 },
                    { 135, 0m, 2, 135, new DateTime(2024, 2, 14, 11, 20, 0, 0, DateTimeKind.Unspecified), 4, 400000m, 6400000m, 6800000m, 0 },
                    { 136, 0m, 0, 136, new DateTime(2024, 3, 15, 11, 20, 0, 0, DateTimeKind.Unspecified), 3, 100000m, 2400000m, 2500000m, 0 },
                    { 137, 50000m, 1, 137, new DateTime(2024, 4, 10, 11, 20, 0, 0, DateTimeKind.Unspecified), 4, 350000m, 4800000m, 5100000m, 0 },
                    { 138, 0m, 2, 138, new DateTime(2024, 5, 13, 11, 20, 0, 0, DateTimeKind.Unspecified), 4, 300000m, 8000000m, 8300000m, 0 },
                    { 139, 0m, 1, 139, new DateTime(2024, 6, 13, 11, 20, 0, 0, DateTimeKind.Unspecified), 5, 450000m, 8000000m, 8450000m, 0 },
                    { 140, 150000m, 1, 140, new DateTime(2024, 7, 16, 11, 20, 0, 0, DateTimeKind.Unspecified), 5, 650000m, 10000000m, 10500000m, 0 },
                    { 141, 0m, 0, 141, new DateTime(2024, 8, 11, 11, 20, 0, 0, DateTimeKind.Unspecified), 4, 200000m, 6400000m, 6600000m, 0 },
                    { 142, 0m, 2, 142, new DateTime(2024, 9, 13, 11, 20, 0, 0, DateTimeKind.Unspecified), 3, 300000m, 3600000m, 3900000m, 0 },
                    { 143, 100000m, 1, 143, new DateTime(2024, 10, 16, 11, 20, 0, 0, DateTimeKind.Unspecified), 4, 300000m, 8000000m, 8200000m, 0 },
                    { 144, 0m, 0, 144, new DateTime(2024, 11, 9, 11, 20, 0, 0, DateTimeKind.Unspecified), 3, 300000m, 3600000m, 3900000m, 0 },
                    { 145, 100000m, 1, 145, new DateTime(2024, 12, 10, 11, 20, 0, 0, DateTimeKind.Unspecified), 5, 800000m, 10000000m, 10700000m, 0 },
                    { 146, 0m, 0, 146, new DateTime(2024, 12, 21, 11, 20, 0, 0, DateTimeKind.Unspecified), 3, 60000m, 1500000m, 1560000m, 1 },
                    { 147, 0m, 0, 147, new DateTime(2023, 1, 12, 11, 20, 0, 0, DateTimeKind.Unspecified), 3, 300000m, 2400000m, 2700000m, 0 },
                    { 148, 0m, 1, 148, new DateTime(2023, 2, 14, 11, 20, 0, 0, DateTimeKind.Unspecified), 3, 300000m, 3600000m, 3900000m, 0 },
                    { 149, 0m, 0, 149, new DateTime(2023, 3, 11, 11, 20, 0, 0, DateTimeKind.Unspecified), 4, 100000m, 2000000m, 2100000m, 0 },
                    { 150, 0m, 2, 150, new DateTime(2023, 4, 17, 11, 20, 0, 0, DateTimeKind.Unspecified), 3, 350000m, 4800000m, 5150000m, 0 },
                    { 151, 0m, 1, 151, new DateTime(2023, 5, 10, 11, 20, 0, 0, DateTimeKind.Unspecified), 4, 300000m, 4800000m, 5100000m, 0 },
                    { 152, 100000m, 2, 152, new DateTime(2023, 6, 14, 11, 20, 0, 0, DateTimeKind.Unspecified), 4, 400000m, 8000000m, 8300000m, 0 },
                    { 153, 0m, 1, 153, new DateTime(2023, 7, 13, 11, 20, 0, 0, DateTimeKind.Unspecified), 5, 300000m, 8000000m, 8300000m, 0 },
                    { 154, 0m, 0, 154, new DateTime(2023, 8, 16, 11, 20, 0, 0, DateTimeKind.Unspecified), 4, 200000m, 4800000m, 5000000m, 0 },
                    { 155, 0m, 0, 155, new DateTime(2023, 9, 8, 11, 20, 0, 0, DateTimeKind.Unspecified), 3, 180000m, 2400000m, 2580000m, 0 },
                    { 156, 0m, 1, 156, new DateTime(2023, 10, 13, 11, 20, 0, 0, DateTimeKind.Unspecified), 4, 300000m, 6400000m, 6700000m, 0 },
                    { 157, 0m, 2, 157, new DateTime(2023, 11, 14, 11, 20, 0, 0, DateTimeKind.Unspecified), 3, 100000m, 3600000m, 3700000m, 0 },
                    { 158, 150000m, 1, 158, new DateTime(2023, 12, 11, 11, 20, 0, 0, DateTimeKind.Unspecified), 4, 650000m, 8000000m, 8500000m, 0 },
                    { 159, 0m, 0, 159, new DateTime(2023, 12, 20, 11, 20, 0, 0, DateTimeKind.Unspecified), 3, 100000m, 1500000m, 1600000m, 1 },
                    { 160, 0m, 0, 160, new DateTime(2022, 1, 13, 11, 20, 0, 0, DateTimeKind.Unspecified), 3, 60000m, 1500000m, 1560000m, 0 },
                    { 161, 0m, 1, 161, new DateTime(2022, 2, 15, 11, 20, 0, 0, DateTimeKind.Unspecified), 3, 300000m, 2400000m, 2700000m, 0 },
                    { 162, 0m, 0, 162, new DateTime(2022, 3, 11, 11, 20, 0, 0, DateTimeKind.Unspecified), 3, 100000m, 3600000m, 3700000m, 0 },
                    { 163, 0m, 2, 163, new DateTime(2022, 4, 15, 11, 20, 0, 0, DateTimeKind.Unspecified), 4, 150000m, 3200000m, 3350000m, 0 },
                    { 164, 0m, 1, 164, new DateTime(2022, 5, 12, 11, 20, 0, 0, DateTimeKind.Unspecified), 3, 300000m, 3600000m, 3900000m, 0 },
                    { 165, 0m, 1, 165, new DateTime(2022, 6, 11, 11, 20, 0, 0, DateTimeKind.Unspecified), 4, 450000m, 6400000m, 6850000m, 0 },
                    { 166, 100000m, 2, 166, new DateTime(2022, 7, 16, 11, 20, 0, 0, DateTimeKind.Unspecified), 4, 350000m, 8000000m, 8250000m, 0 },
                    { 167, 0m, 0, 167, new DateTime(2022, 8, 10, 11, 20, 0, 0, DateTimeKind.Unspecified), 4, 400000m, 4800000m, 5200000m, 0 },
                    { 168, 0m, 0, 168, new DateTime(2022, 9, 13, 11, 20, 0, 0, DateTimeKind.Unspecified), 3, 90000m, 2400000m, 2490000m, 0 },
                    { 169, 0m, 1, 169, new DateTime(2022, 10, 11, 11, 20, 0, 0, DateTimeKind.Unspecified), 3, 200000m, 4800000m, 5000000m, 0 },
                    { 170, 0m, 2, 170, new DateTime(2022, 11, 17, 11, 20, 0, 0, DateTimeKind.Unspecified), 3, 100000m, 3600000m, 3700000m, 0 },
                    { 171, 100000m, 1, 171, new DateTime(2022, 12, 13, 11, 20, 0, 0, DateTimeKind.Unspecified), 4, 300000m, 8000000m, 8200000m, 0 },
                    { 172, 0m, 0, 172, new DateTime(2022, 12, 22, 11, 20, 0, 0, DateTimeKind.Unspecified), 3, 40000m, 1500000m, 1540000m, 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DichVuPhatSinhs",
                keyColumn: "MaDichVuPhatSinh",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "DichVuPhatSinhs",
                keyColumn: "MaDichVuPhatSinh",
                keyValue: 102);

            migrationBuilder.DeleteData(
                table: "DichVuPhatSinhs",
                keyColumn: "MaDichVuPhatSinh",
                keyValue: 103);

            migrationBuilder.DeleteData(
                table: "DichVuPhatSinhs",
                keyColumn: "MaDichVuPhatSinh",
                keyValue: 104);

            migrationBuilder.DeleteData(
                table: "DichVuPhatSinhs",
                keyColumn: "MaDichVuPhatSinh",
                keyValue: 105);

            migrationBuilder.DeleteData(
                table: "DichVuPhatSinhs",
                keyColumn: "MaDichVuPhatSinh",
                keyValue: 106);

            migrationBuilder.DeleteData(
                table: "DichVuPhatSinhs",
                keyColumn: "MaDichVuPhatSinh",
                keyValue: 107);

            migrationBuilder.DeleteData(
                table: "DichVuPhatSinhs",
                keyColumn: "MaDichVuPhatSinh",
                keyValue: 108);

            migrationBuilder.DeleteData(
                table: "DichVuPhatSinhs",
                keyColumn: "MaDichVuPhatSinh",
                keyValue: 109);

            migrationBuilder.DeleteData(
                table: "DichVuPhatSinhs",
                keyColumn: "MaDichVuPhatSinh",
                keyValue: 110);

            migrationBuilder.DeleteData(
                table: "DichVuPhatSinhs",
                keyColumn: "MaDichVuPhatSinh",
                keyValue: 111);

            migrationBuilder.DeleteData(
                table: "DichVuPhatSinhs",
                keyColumn: "MaDichVuPhatSinh",
                keyValue: 112);

            migrationBuilder.DeleteData(
                table: "DichVuPhatSinhs",
                keyColumn: "MaDichVuPhatSinh",
                keyValue: 113);

            migrationBuilder.DeleteData(
                table: "DichVuPhatSinhs",
                keyColumn: "MaDichVuPhatSinh",
                keyValue: 114);

            migrationBuilder.DeleteData(
                table: "DichVuPhatSinhs",
                keyColumn: "MaDichVuPhatSinh",
                keyValue: 115);

            migrationBuilder.DeleteData(
                table: "DichVuPhatSinhs",
                keyColumn: "MaDichVuPhatSinh",
                keyValue: 116);

            migrationBuilder.DeleteData(
                table: "DichVuPhatSinhs",
                keyColumn: "MaDichVuPhatSinh",
                keyValue: 117);

            migrationBuilder.DeleteData(
                table: "DichVuPhatSinhs",
                keyColumn: "MaDichVuPhatSinh",
                keyValue: 118);

            migrationBuilder.DeleteData(
                table: "DichVuPhatSinhs",
                keyColumn: "MaDichVuPhatSinh",
                keyValue: 119);

            migrationBuilder.DeleteData(
                table: "DichVuPhatSinhs",
                keyColumn: "MaDichVuPhatSinh",
                keyValue: 120);

            migrationBuilder.DeleteData(
                table: "DichVuPhatSinhs",
                keyColumn: "MaDichVuPhatSinh",
                keyValue: 121);

            migrationBuilder.DeleteData(
                table: "DichVuPhatSinhs",
                keyColumn: "MaDichVuPhatSinh",
                keyValue: 122);

            migrationBuilder.DeleteData(
                table: "DichVuPhatSinhs",
                keyColumn: "MaDichVuPhatSinh",
                keyValue: 123);

            migrationBuilder.DeleteData(
                table: "DichVuPhatSinhs",
                keyColumn: "MaDichVuPhatSinh",
                keyValue: 124);

            migrationBuilder.DeleteData(
                table: "DichVuPhatSinhs",
                keyColumn: "MaDichVuPhatSinh",
                keyValue: 125);

            migrationBuilder.DeleteData(
                table: "DichVuPhatSinhs",
                keyColumn: "MaDichVuPhatSinh",
                keyValue: 126);

            migrationBuilder.DeleteData(
                table: "DichVuPhatSinhs",
                keyColumn: "MaDichVuPhatSinh",
                keyValue: 127);

            migrationBuilder.DeleteData(
                table: "DichVuPhatSinhs",
                keyColumn: "MaDichVuPhatSinh",
                keyValue: 128);

            migrationBuilder.DeleteData(
                table: "DichVuPhatSinhs",
                keyColumn: "MaDichVuPhatSinh",
                keyValue: 129);

            migrationBuilder.DeleteData(
                table: "DichVuPhatSinhs",
                keyColumn: "MaDichVuPhatSinh",
                keyValue: 130);

            migrationBuilder.DeleteData(
                table: "DichVuPhatSinhs",
                keyColumn: "MaDichVuPhatSinh",
                keyValue: 131);

            migrationBuilder.DeleteData(
                table: "DichVuPhatSinhs",
                keyColumn: "MaDichVuPhatSinh",
                keyValue: 132);

            migrationBuilder.DeleteData(
                table: "DichVuPhatSinhs",
                keyColumn: "MaDichVuPhatSinh",
                keyValue: 133);

            migrationBuilder.DeleteData(
                table: "DichVuPhatSinhs",
                keyColumn: "MaDichVuPhatSinh",
                keyValue: 134);

            migrationBuilder.DeleteData(
                table: "DichVuPhatSinhs",
                keyColumn: "MaDichVuPhatSinh",
                keyValue: 135);

            migrationBuilder.DeleteData(
                table: "DichVuPhatSinhs",
                keyColumn: "MaDichVuPhatSinh",
                keyValue: 136);

            migrationBuilder.DeleteData(
                table: "DichVuPhatSinhs",
                keyColumn: "MaDichVuPhatSinh",
                keyValue: 137);

            migrationBuilder.DeleteData(
                table: "DichVuPhatSinhs",
                keyColumn: "MaDichVuPhatSinh",
                keyValue: 138);

            migrationBuilder.DeleteData(
                table: "DichVuPhatSinhs",
                keyColumn: "MaDichVuPhatSinh",
                keyValue: 139);

            migrationBuilder.DeleteData(
                table: "DichVuPhatSinhs",
                keyColumn: "MaDichVuPhatSinh",
                keyValue: 140);

            migrationBuilder.DeleteData(
                table: "DichVuPhatSinhs",
                keyColumn: "MaDichVuPhatSinh",
                keyValue: 141);

            migrationBuilder.DeleteData(
                table: "DichVuPhatSinhs",
                keyColumn: "MaDichVuPhatSinh",
                keyValue: 142);

            migrationBuilder.DeleteData(
                table: "DichVuPhatSinhs",
                keyColumn: "MaDichVuPhatSinh",
                keyValue: 143);

            migrationBuilder.DeleteData(
                table: "DichVuPhatSinhs",
                keyColumn: "MaDichVuPhatSinh",
                keyValue: 144);

            migrationBuilder.DeleteData(
                table: "DichVuPhatSinhs",
                keyColumn: "MaDichVuPhatSinh",
                keyValue: 145);

            migrationBuilder.DeleteData(
                table: "DichVuPhatSinhs",
                keyColumn: "MaDichVuPhatSinh",
                keyValue: 146);

            migrationBuilder.DeleteData(
                table: "DichVuPhatSinhs",
                keyColumn: "MaDichVuPhatSinh",
                keyValue: 147);

            migrationBuilder.DeleteData(
                table: "DichVuPhatSinhs",
                keyColumn: "MaDichVuPhatSinh",
                keyValue: 148);

            migrationBuilder.DeleteData(
                table: "DichVuPhatSinhs",
                keyColumn: "MaDichVuPhatSinh",
                keyValue: 149);

            migrationBuilder.DeleteData(
                table: "DichVuPhatSinhs",
                keyColumn: "MaDichVuPhatSinh",
                keyValue: 150);

            migrationBuilder.DeleteData(
                table: "DichVuPhatSinhs",
                keyColumn: "MaDichVuPhatSinh",
                keyValue: 151);

            migrationBuilder.DeleteData(
                table: "DichVuPhatSinhs",
                keyColumn: "MaDichVuPhatSinh",
                keyValue: 152);

            migrationBuilder.DeleteData(
                table: "DichVuPhatSinhs",
                keyColumn: "MaDichVuPhatSinh",
                keyValue: 153);

            migrationBuilder.DeleteData(
                table: "DichVuPhatSinhs",
                keyColumn: "MaDichVuPhatSinh",
                keyValue: 154);

            migrationBuilder.DeleteData(
                table: "DichVuPhatSinhs",
                keyColumn: "MaDichVuPhatSinh",
                keyValue: 155);

            migrationBuilder.DeleteData(
                table: "DichVuPhatSinhs",
                keyColumn: "MaDichVuPhatSinh",
                keyValue: 156);

            migrationBuilder.DeleteData(
                table: "DichVuPhatSinhs",
                keyColumn: "MaDichVuPhatSinh",
                keyValue: 157);

            migrationBuilder.DeleteData(
                table: "DichVuPhatSinhs",
                keyColumn: "MaDichVuPhatSinh",
                keyValue: 158);

            migrationBuilder.DeleteData(
                table: "DichVuPhatSinhs",
                keyColumn: "MaDichVuPhatSinh",
                keyValue: 159);

            migrationBuilder.DeleteData(
                table: "DichVuPhatSinhs",
                keyColumn: "MaDichVuPhatSinh",
                keyValue: 160);

            migrationBuilder.DeleteData(
                table: "DichVuPhatSinhs",
                keyColumn: "MaDichVuPhatSinh",
                keyValue: 161);

            migrationBuilder.DeleteData(
                table: "DichVuPhatSinhs",
                keyColumn: "MaDichVuPhatSinh",
                keyValue: 162);

            migrationBuilder.DeleteData(
                table: "DichVuPhatSinhs",
                keyColumn: "MaDichVuPhatSinh",
                keyValue: 163);

            migrationBuilder.DeleteData(
                table: "DichVuPhatSinhs",
                keyColumn: "MaDichVuPhatSinh",
                keyValue: 164);

            migrationBuilder.DeleteData(
                table: "DichVuPhatSinhs",
                keyColumn: "MaDichVuPhatSinh",
                keyValue: 165);

            migrationBuilder.DeleteData(
                table: "DichVuPhatSinhs",
                keyColumn: "MaDichVuPhatSinh",
                keyValue: 166);

            migrationBuilder.DeleteData(
                table: "DichVuPhatSinhs",
                keyColumn: "MaDichVuPhatSinh",
                keyValue: 167);

            migrationBuilder.DeleteData(
                table: "DichVuPhatSinhs",
                keyColumn: "MaDichVuPhatSinh",
                keyValue: 168);

            migrationBuilder.DeleteData(
                table: "DichVuPhatSinhs",
                keyColumn: "MaDichVuPhatSinh",
                keyValue: 169);

            migrationBuilder.DeleteData(
                table: "DichVuPhatSinhs",
                keyColumn: "MaDichVuPhatSinh",
                keyValue: 170);

            migrationBuilder.DeleteData(
                table: "DichVuPhatSinhs",
                keyColumn: "MaDichVuPhatSinh",
                keyValue: 171);

            migrationBuilder.DeleteData(
                table: "DichVuPhatSinhs",
                keyColumn: "MaDichVuPhatSinh",
                keyValue: 172);

            migrationBuilder.DeleteData(
                table: "HoaDons",
                keyColumn: "MaHoaDon",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "HoaDons",
                keyColumn: "MaHoaDon",
                keyValue: 102);

            migrationBuilder.DeleteData(
                table: "HoaDons",
                keyColumn: "MaHoaDon",
                keyValue: 103);

            migrationBuilder.DeleteData(
                table: "HoaDons",
                keyColumn: "MaHoaDon",
                keyValue: 104);

            migrationBuilder.DeleteData(
                table: "HoaDons",
                keyColumn: "MaHoaDon",
                keyValue: 105);

            migrationBuilder.DeleteData(
                table: "HoaDons",
                keyColumn: "MaHoaDon",
                keyValue: 106);

            migrationBuilder.DeleteData(
                table: "HoaDons",
                keyColumn: "MaHoaDon",
                keyValue: 107);

            migrationBuilder.DeleteData(
                table: "HoaDons",
                keyColumn: "MaHoaDon",
                keyValue: 108);

            migrationBuilder.DeleteData(
                table: "HoaDons",
                keyColumn: "MaHoaDon",
                keyValue: 109);

            migrationBuilder.DeleteData(
                table: "HoaDons",
                keyColumn: "MaHoaDon",
                keyValue: 110);

            migrationBuilder.DeleteData(
                table: "HoaDons",
                keyColumn: "MaHoaDon",
                keyValue: 111);

            migrationBuilder.DeleteData(
                table: "HoaDons",
                keyColumn: "MaHoaDon",
                keyValue: 112);

            migrationBuilder.DeleteData(
                table: "HoaDons",
                keyColumn: "MaHoaDon",
                keyValue: 113);

            migrationBuilder.DeleteData(
                table: "HoaDons",
                keyColumn: "MaHoaDon",
                keyValue: 114);

            migrationBuilder.DeleteData(
                table: "HoaDons",
                keyColumn: "MaHoaDon",
                keyValue: 115);

            migrationBuilder.DeleteData(
                table: "HoaDons",
                keyColumn: "MaHoaDon",
                keyValue: 116);

            migrationBuilder.DeleteData(
                table: "HoaDons",
                keyColumn: "MaHoaDon",
                keyValue: 117);

            migrationBuilder.DeleteData(
                table: "HoaDons",
                keyColumn: "MaHoaDon",
                keyValue: 118);

            migrationBuilder.DeleteData(
                table: "HoaDons",
                keyColumn: "MaHoaDon",
                keyValue: 119);

            migrationBuilder.DeleteData(
                table: "HoaDons",
                keyColumn: "MaHoaDon",
                keyValue: 120);

            migrationBuilder.DeleteData(
                table: "HoaDons",
                keyColumn: "MaHoaDon",
                keyValue: 121);

            migrationBuilder.DeleteData(
                table: "HoaDons",
                keyColumn: "MaHoaDon",
                keyValue: 122);

            migrationBuilder.DeleteData(
                table: "HoaDons",
                keyColumn: "MaHoaDon",
                keyValue: 123);

            migrationBuilder.DeleteData(
                table: "HoaDons",
                keyColumn: "MaHoaDon",
                keyValue: 124);

            migrationBuilder.DeleteData(
                table: "HoaDons",
                keyColumn: "MaHoaDon",
                keyValue: 125);

            migrationBuilder.DeleteData(
                table: "HoaDons",
                keyColumn: "MaHoaDon",
                keyValue: 126);

            migrationBuilder.DeleteData(
                table: "HoaDons",
                keyColumn: "MaHoaDon",
                keyValue: 127);

            migrationBuilder.DeleteData(
                table: "HoaDons",
                keyColumn: "MaHoaDon",
                keyValue: 128);

            migrationBuilder.DeleteData(
                table: "HoaDons",
                keyColumn: "MaHoaDon",
                keyValue: 129);

            migrationBuilder.DeleteData(
                table: "HoaDons",
                keyColumn: "MaHoaDon",
                keyValue: 130);

            migrationBuilder.DeleteData(
                table: "HoaDons",
                keyColumn: "MaHoaDon",
                keyValue: 131);

            migrationBuilder.DeleteData(
                table: "HoaDons",
                keyColumn: "MaHoaDon",
                keyValue: 132);

            migrationBuilder.DeleteData(
                table: "HoaDons",
                keyColumn: "MaHoaDon",
                keyValue: 133);

            migrationBuilder.DeleteData(
                table: "HoaDons",
                keyColumn: "MaHoaDon",
                keyValue: 134);

            migrationBuilder.DeleteData(
                table: "HoaDons",
                keyColumn: "MaHoaDon",
                keyValue: 135);

            migrationBuilder.DeleteData(
                table: "HoaDons",
                keyColumn: "MaHoaDon",
                keyValue: 136);

            migrationBuilder.DeleteData(
                table: "HoaDons",
                keyColumn: "MaHoaDon",
                keyValue: 137);

            migrationBuilder.DeleteData(
                table: "HoaDons",
                keyColumn: "MaHoaDon",
                keyValue: 138);

            migrationBuilder.DeleteData(
                table: "HoaDons",
                keyColumn: "MaHoaDon",
                keyValue: 139);

            migrationBuilder.DeleteData(
                table: "HoaDons",
                keyColumn: "MaHoaDon",
                keyValue: 140);

            migrationBuilder.DeleteData(
                table: "HoaDons",
                keyColumn: "MaHoaDon",
                keyValue: 141);

            migrationBuilder.DeleteData(
                table: "HoaDons",
                keyColumn: "MaHoaDon",
                keyValue: 142);

            migrationBuilder.DeleteData(
                table: "HoaDons",
                keyColumn: "MaHoaDon",
                keyValue: 143);

            migrationBuilder.DeleteData(
                table: "HoaDons",
                keyColumn: "MaHoaDon",
                keyValue: 144);

            migrationBuilder.DeleteData(
                table: "HoaDons",
                keyColumn: "MaHoaDon",
                keyValue: 145);

            migrationBuilder.DeleteData(
                table: "HoaDons",
                keyColumn: "MaHoaDon",
                keyValue: 146);

            migrationBuilder.DeleteData(
                table: "HoaDons",
                keyColumn: "MaHoaDon",
                keyValue: 147);

            migrationBuilder.DeleteData(
                table: "HoaDons",
                keyColumn: "MaHoaDon",
                keyValue: 148);

            migrationBuilder.DeleteData(
                table: "HoaDons",
                keyColumn: "MaHoaDon",
                keyValue: 149);

            migrationBuilder.DeleteData(
                table: "HoaDons",
                keyColumn: "MaHoaDon",
                keyValue: 150);

            migrationBuilder.DeleteData(
                table: "HoaDons",
                keyColumn: "MaHoaDon",
                keyValue: 151);

            migrationBuilder.DeleteData(
                table: "HoaDons",
                keyColumn: "MaHoaDon",
                keyValue: 152);

            migrationBuilder.DeleteData(
                table: "HoaDons",
                keyColumn: "MaHoaDon",
                keyValue: 153);

            migrationBuilder.DeleteData(
                table: "HoaDons",
                keyColumn: "MaHoaDon",
                keyValue: 154);

            migrationBuilder.DeleteData(
                table: "HoaDons",
                keyColumn: "MaHoaDon",
                keyValue: 155);

            migrationBuilder.DeleteData(
                table: "HoaDons",
                keyColumn: "MaHoaDon",
                keyValue: 156);

            migrationBuilder.DeleteData(
                table: "HoaDons",
                keyColumn: "MaHoaDon",
                keyValue: 157);

            migrationBuilder.DeleteData(
                table: "HoaDons",
                keyColumn: "MaHoaDon",
                keyValue: 158);

            migrationBuilder.DeleteData(
                table: "HoaDons",
                keyColumn: "MaHoaDon",
                keyValue: 159);

            migrationBuilder.DeleteData(
                table: "HoaDons",
                keyColumn: "MaHoaDon",
                keyValue: 160);

            migrationBuilder.DeleteData(
                table: "HoaDons",
                keyColumn: "MaHoaDon",
                keyValue: 161);

            migrationBuilder.DeleteData(
                table: "HoaDons",
                keyColumn: "MaHoaDon",
                keyValue: 162);

            migrationBuilder.DeleteData(
                table: "HoaDons",
                keyColumn: "MaHoaDon",
                keyValue: 163);

            migrationBuilder.DeleteData(
                table: "HoaDons",
                keyColumn: "MaHoaDon",
                keyValue: 164);

            migrationBuilder.DeleteData(
                table: "HoaDons",
                keyColumn: "MaHoaDon",
                keyValue: 165);

            migrationBuilder.DeleteData(
                table: "HoaDons",
                keyColumn: "MaHoaDon",
                keyValue: 166);

            migrationBuilder.DeleteData(
                table: "HoaDons",
                keyColumn: "MaHoaDon",
                keyValue: 167);

            migrationBuilder.DeleteData(
                table: "HoaDons",
                keyColumn: "MaHoaDon",
                keyValue: 168);

            migrationBuilder.DeleteData(
                table: "HoaDons",
                keyColumn: "MaHoaDon",
                keyValue: 169);

            migrationBuilder.DeleteData(
                table: "HoaDons",
                keyColumn: "MaHoaDon",
                keyValue: 170);

            migrationBuilder.DeleteData(
                table: "HoaDons",
                keyColumn: "MaHoaDon",
                keyValue: 171);

            migrationBuilder.DeleteData(
                table: "HoaDons",
                keyColumn: "MaHoaDon",
                keyValue: 172);

            migrationBuilder.DeleteData(
                table: "DatPhongs",
                keyColumn: "MaDatPhong",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "DatPhongs",
                keyColumn: "MaDatPhong",
                keyValue: 102);

            migrationBuilder.DeleteData(
                table: "DatPhongs",
                keyColumn: "MaDatPhong",
                keyValue: 103);

            migrationBuilder.DeleteData(
                table: "DatPhongs",
                keyColumn: "MaDatPhong",
                keyValue: 104);

            migrationBuilder.DeleteData(
                table: "DatPhongs",
                keyColumn: "MaDatPhong",
                keyValue: 105);

            migrationBuilder.DeleteData(
                table: "DatPhongs",
                keyColumn: "MaDatPhong",
                keyValue: 106);

            migrationBuilder.DeleteData(
                table: "DatPhongs",
                keyColumn: "MaDatPhong",
                keyValue: 107);

            migrationBuilder.DeleteData(
                table: "DatPhongs",
                keyColumn: "MaDatPhong",
                keyValue: 108);

            migrationBuilder.DeleteData(
                table: "DatPhongs",
                keyColumn: "MaDatPhong",
                keyValue: 109);

            migrationBuilder.DeleteData(
                table: "DatPhongs",
                keyColumn: "MaDatPhong",
                keyValue: 110);

            migrationBuilder.DeleteData(
                table: "DatPhongs",
                keyColumn: "MaDatPhong",
                keyValue: 111);

            migrationBuilder.DeleteData(
                table: "DatPhongs",
                keyColumn: "MaDatPhong",
                keyValue: 112);

            migrationBuilder.DeleteData(
                table: "DatPhongs",
                keyColumn: "MaDatPhong",
                keyValue: 113);

            migrationBuilder.DeleteData(
                table: "DatPhongs",
                keyColumn: "MaDatPhong",
                keyValue: 114);

            migrationBuilder.DeleteData(
                table: "DatPhongs",
                keyColumn: "MaDatPhong",
                keyValue: 115);

            migrationBuilder.DeleteData(
                table: "DatPhongs",
                keyColumn: "MaDatPhong",
                keyValue: 116);

            migrationBuilder.DeleteData(
                table: "DatPhongs",
                keyColumn: "MaDatPhong",
                keyValue: 117);

            migrationBuilder.DeleteData(
                table: "DatPhongs",
                keyColumn: "MaDatPhong",
                keyValue: 118);

            migrationBuilder.DeleteData(
                table: "DatPhongs",
                keyColumn: "MaDatPhong",
                keyValue: 119);

            migrationBuilder.DeleteData(
                table: "DatPhongs",
                keyColumn: "MaDatPhong",
                keyValue: 120);

            migrationBuilder.DeleteData(
                table: "DatPhongs",
                keyColumn: "MaDatPhong",
                keyValue: 121);

            migrationBuilder.DeleteData(
                table: "DatPhongs",
                keyColumn: "MaDatPhong",
                keyValue: 122);

            migrationBuilder.DeleteData(
                table: "DatPhongs",
                keyColumn: "MaDatPhong",
                keyValue: 123);

            migrationBuilder.DeleteData(
                table: "DatPhongs",
                keyColumn: "MaDatPhong",
                keyValue: 124);

            migrationBuilder.DeleteData(
                table: "DatPhongs",
                keyColumn: "MaDatPhong",
                keyValue: 125);

            migrationBuilder.DeleteData(
                table: "DatPhongs",
                keyColumn: "MaDatPhong",
                keyValue: 126);

            migrationBuilder.DeleteData(
                table: "DatPhongs",
                keyColumn: "MaDatPhong",
                keyValue: 127);

            migrationBuilder.DeleteData(
                table: "DatPhongs",
                keyColumn: "MaDatPhong",
                keyValue: 128);

            migrationBuilder.DeleteData(
                table: "DatPhongs",
                keyColumn: "MaDatPhong",
                keyValue: 129);

            migrationBuilder.DeleteData(
                table: "DatPhongs",
                keyColumn: "MaDatPhong",
                keyValue: 130);

            migrationBuilder.DeleteData(
                table: "DatPhongs",
                keyColumn: "MaDatPhong",
                keyValue: 131);

            migrationBuilder.DeleteData(
                table: "DatPhongs",
                keyColumn: "MaDatPhong",
                keyValue: 132);

            migrationBuilder.DeleteData(
                table: "DatPhongs",
                keyColumn: "MaDatPhong",
                keyValue: 133);

            migrationBuilder.DeleteData(
                table: "DatPhongs",
                keyColumn: "MaDatPhong",
                keyValue: 134);

            migrationBuilder.DeleteData(
                table: "DatPhongs",
                keyColumn: "MaDatPhong",
                keyValue: 135);

            migrationBuilder.DeleteData(
                table: "DatPhongs",
                keyColumn: "MaDatPhong",
                keyValue: 136);

            migrationBuilder.DeleteData(
                table: "DatPhongs",
                keyColumn: "MaDatPhong",
                keyValue: 137);

            migrationBuilder.DeleteData(
                table: "DatPhongs",
                keyColumn: "MaDatPhong",
                keyValue: 138);

            migrationBuilder.DeleteData(
                table: "DatPhongs",
                keyColumn: "MaDatPhong",
                keyValue: 139);

            migrationBuilder.DeleteData(
                table: "DatPhongs",
                keyColumn: "MaDatPhong",
                keyValue: 140);

            migrationBuilder.DeleteData(
                table: "DatPhongs",
                keyColumn: "MaDatPhong",
                keyValue: 141);

            migrationBuilder.DeleteData(
                table: "DatPhongs",
                keyColumn: "MaDatPhong",
                keyValue: 142);

            migrationBuilder.DeleteData(
                table: "DatPhongs",
                keyColumn: "MaDatPhong",
                keyValue: 143);

            migrationBuilder.DeleteData(
                table: "DatPhongs",
                keyColumn: "MaDatPhong",
                keyValue: 144);

            migrationBuilder.DeleteData(
                table: "DatPhongs",
                keyColumn: "MaDatPhong",
                keyValue: 145);

            migrationBuilder.DeleteData(
                table: "DatPhongs",
                keyColumn: "MaDatPhong",
                keyValue: 146);

            migrationBuilder.DeleteData(
                table: "DatPhongs",
                keyColumn: "MaDatPhong",
                keyValue: 147);

            migrationBuilder.DeleteData(
                table: "DatPhongs",
                keyColumn: "MaDatPhong",
                keyValue: 148);

            migrationBuilder.DeleteData(
                table: "DatPhongs",
                keyColumn: "MaDatPhong",
                keyValue: 149);

            migrationBuilder.DeleteData(
                table: "DatPhongs",
                keyColumn: "MaDatPhong",
                keyValue: 150);

            migrationBuilder.DeleteData(
                table: "DatPhongs",
                keyColumn: "MaDatPhong",
                keyValue: 151);

            migrationBuilder.DeleteData(
                table: "DatPhongs",
                keyColumn: "MaDatPhong",
                keyValue: 152);

            migrationBuilder.DeleteData(
                table: "DatPhongs",
                keyColumn: "MaDatPhong",
                keyValue: 153);

            migrationBuilder.DeleteData(
                table: "DatPhongs",
                keyColumn: "MaDatPhong",
                keyValue: 154);

            migrationBuilder.DeleteData(
                table: "DatPhongs",
                keyColumn: "MaDatPhong",
                keyValue: 155);

            migrationBuilder.DeleteData(
                table: "DatPhongs",
                keyColumn: "MaDatPhong",
                keyValue: 156);

            migrationBuilder.DeleteData(
                table: "DatPhongs",
                keyColumn: "MaDatPhong",
                keyValue: 157);

            migrationBuilder.DeleteData(
                table: "DatPhongs",
                keyColumn: "MaDatPhong",
                keyValue: 158);

            migrationBuilder.DeleteData(
                table: "DatPhongs",
                keyColumn: "MaDatPhong",
                keyValue: 159);

            migrationBuilder.DeleteData(
                table: "DatPhongs",
                keyColumn: "MaDatPhong",
                keyValue: 160);

            migrationBuilder.DeleteData(
                table: "DatPhongs",
                keyColumn: "MaDatPhong",
                keyValue: 161);

            migrationBuilder.DeleteData(
                table: "DatPhongs",
                keyColumn: "MaDatPhong",
                keyValue: 162);

            migrationBuilder.DeleteData(
                table: "DatPhongs",
                keyColumn: "MaDatPhong",
                keyValue: 163);

            migrationBuilder.DeleteData(
                table: "DatPhongs",
                keyColumn: "MaDatPhong",
                keyValue: 164);

            migrationBuilder.DeleteData(
                table: "DatPhongs",
                keyColumn: "MaDatPhong",
                keyValue: 165);

            migrationBuilder.DeleteData(
                table: "DatPhongs",
                keyColumn: "MaDatPhong",
                keyValue: 166);

            migrationBuilder.DeleteData(
                table: "DatPhongs",
                keyColumn: "MaDatPhong",
                keyValue: 167);

            migrationBuilder.DeleteData(
                table: "DatPhongs",
                keyColumn: "MaDatPhong",
                keyValue: 168);

            migrationBuilder.DeleteData(
                table: "DatPhongs",
                keyColumn: "MaDatPhong",
                keyValue: 169);

            migrationBuilder.DeleteData(
                table: "DatPhongs",
                keyColumn: "MaDatPhong",
                keyValue: 170);

            migrationBuilder.DeleteData(
                table: "DatPhongs",
                keyColumn: "MaDatPhong",
                keyValue: 171);

            migrationBuilder.DeleteData(
                table: "DatPhongs",
                keyColumn: "MaDatPhong",
                keyValue: 172);
        }
    }
}
