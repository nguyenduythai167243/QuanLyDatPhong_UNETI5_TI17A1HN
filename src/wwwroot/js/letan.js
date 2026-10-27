/**
 * HOTEL RECEPTION LUXURY DASHBOARD JS (LỄ TÂN ĐẶT PHÒNG)
 * Module 4 - Nguyễn Duy Thái (MSV: 23103100052)
 */

document.addEventListener('DOMContentLoaded', function () {
    // 1. Digital Clock for Receptionist Command Bar
    const clockEl = document.getElementById('letan-live-clock');
    if (clockEl) {
        function updateClock() {
            const now = new Date();
            const days = ['Chủ nhật', 'Thứ hai', 'Thứ ba', 'Thứ tư', 'Thứ năm', 'Thứ sáu', 'Thứ bảy'];
            const dayName = days[now.getDay()];
            const dateStr = now.toLocaleDateString('vi-VN', { day: '2-digit', month: '2-digit', year: 'numeric' });
            const timeStr = now.toLocaleTimeString('vi-VN', { hour: '2-digit', minute: '2-digit', second: '2-digit' });
            clockEl.textContent = `${dayName}, ${dateStr} - ${timeStr}`;
        }
        updateClock();
        setInterval(updateClock, 1000);
    }

    // 2. Dynamic Price Calculator for Walk-in Booking (Create / Edit)
    const ngayNhanInput = document.getElementById('NgayNhanPhong');
    const ngayTraInput = document.getElementById('NgayTraPhong');
    const phongSelect = document.getElementById('MaPhong');
    const previewSoDem = document.getElementById('preview-sodem');
    const previewGiaPhong = document.getElementById('preview-giaphong');
    const previewTongTien = document.getElementById('preview-tongtien');

    if (ngayNhanInput && ngayTraInput && phongSelect && previewTongTien) {
        function calculateWalkInPrice() {
            const checkIn = new Date(ngayNhanInput.value);
            const checkOut = new Date(ngayTraInput.value);

            let diffTime = checkOut.getTime() - checkIn.getTime();
            let nights = Math.ceil(diffTime / (1000 * 3600 * 24));
            if (isNaN(nights) || nights <= 0) nights = 1;

            if (previewSoDem) {
                previewSoDem.textContent = `${nights} đêm`;
            }

            // Extract price from selected option text (format: "101 - Standard (500,000 đ/đêm)")
            const selectedOpt = phongSelect.options[phongSelect.selectedIndex];
            if (selectedOpt) {
                const text = selectedOpt.text;
                const match = text.match(/\(([\d.,]+)\s*đ/);
                let pricePerNight = 0;
                if (match && match[1]) {
                    pricePerNight = parseInt(match[1].replace(/[,.]/g, ''), 10) || 0;
                }

                if (previewGiaPhong) {
                    previewGiaPhong.textContent = pricePerNight.toLocaleString('vi-VN') + ' đ/đêm';
                }

                const total = pricePerNight * nights;
                previewTongTien.textContent = total.toLocaleString('vi-VN') + ' VNĐ';

                const tongTienHidden = document.getElementById('TongTienDuKien');
                if (tongTienHidden) {
                    tongTienHidden.value = total;
                }
            }
        }

        ngayNhanInput.addEventListener('change', calculateWalkInPrice);
        ngayTraInput.addEventListener('change', calculateWalkInPrice);
        phongSelect.addEventListener('change', calculateWalkInPrice);

        // Run once on initial load
        calculateWalkInPrice();
    }

    // 3. Quick Copy for Booking ID or Phone Number
    document.querySelectorAll('.btn-copy-info').forEach(btn => {
        btn.addEventListener('click', function (e) {
            e.preventDefault();
            const textToCopy = this.getAttribute('data-copy');
            if (textToCopy) {
                navigator.clipboard.writeText(textToCopy).then(() => {
                    const originalTitle = this.getAttribute('title') || '';
                    this.classList.add('text-success');
                    setTimeout(() => {
                        this.classList.remove('text-success');
                    }, 1500);
                });
            }
        });
    });
});
