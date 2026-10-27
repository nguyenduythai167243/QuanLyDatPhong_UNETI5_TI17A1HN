/**
 * UNETI LUXURY HOTEL - HOMEPAGE INTERACTIONS & ANIMATIONS
 */

document.addEventListener('DOMContentLoaded', function () {
    // 1. Interactive Category Filter Chips (Lọc nhanh theo hạng phòng)
    const filterChips = document.querySelectorAll('.room-filter-chip');
    const roomCards = document.querySelectorAll('.room-card-item');

    filterChips.forEach(chip => {
        chip.addEventListener('click', function () {
            filterChips.forEach(c => c.classList.remove('active'));
            this.classList.add('active');

            const category = this.getAttribute('data-filter');

            roomCards.forEach(card => {
                const cardCat = card.getAttribute('data-category');
                if (category === 'all' || cardCat === category) {
                    card.style.display = 'block';
                    card.style.opacity = '0';
                    setTimeout(() => {
                        card.style.transition = 'opacity 0.35s ease, transform 0.35s ease';
                        card.style.opacity = '1';
                        card.style.transform = 'translateY(0)';
                    }, 50);
                } else {
                    card.style.display = 'none';
                }
            });
        });
    });

    // 2. Interactive Reveal: Quick Amenities Tray (Bấm hiện/ẩn tiện nghi phòng)
    document.querySelectorAll('.room-amenities-toggle').forEach(btn => {
        btn.addEventListener('click', function (e) {
            e.preventDefault();
            const trayId = this.getAttribute('data-target');
            const tray = document.getElementById(trayId);
            if (tray) {
                tray.classList.toggle('show');
                const isShown = tray.classList.contains('show');
                this.innerHTML = isShown 
                    ? '<i class="bi bi-chevron-up"></i> Thu gọn tiện ích' 
                    : '<i class="bi bi-stars"></i> Xem nhanh tiện ích';
            }
        });
    });

    // 3. Interactive Experience Cards Click (Thẻ trải nghiệm thượng lưu)
    const expCards = document.querySelectorAll('.experience-card');
    expCards.forEach(card => {
        card.addEventListener('click', function () {
            expCards.forEach(c => c.classList.remove('active'));
            this.classList.add('active');
        });
    });

    // 4. Interactive FAQ Accordion (Hiện / Ẩn câu hỏi thường gặp)
    document.querySelectorAll('.hotel-faq-question').forEach(header => {
        header.addEventListener('click', function () {
            const item = this.closest('.hotel-faq-item');
            if (item) {
                item.classList.toggle('open');
            }
        });
    });

    // 5. Booking Bar Preset Tabs (Chuyển tab dịch vụ trên thanh tìm kiếm)
    document.querySelectorAll('.booking-nav-btn').forEach(tab => {
        tab.addEventListener('click', function (e) {
            e.preventDefault();
            document.querySelectorAll('.booking-nav-btn').forEach(t => t.classList.remove('active'));
            this.classList.add('active');

            const type = this.getAttribute('data-type');
            const guestSelect = document.querySelector('select[name="soNguoi"]');
            if (guestSelect) {
                if (type === 'family') {
                    guestSelect.value = '6';
                } else if (type === 'event') {
                    guestSelect.value = '4';
                } else {
                    guestSelect.value = '2';
                }
            }
        });
    });
});
