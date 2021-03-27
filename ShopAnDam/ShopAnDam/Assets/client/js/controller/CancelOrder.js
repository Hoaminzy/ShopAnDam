var user = {
    init: function () {
        user.registerEvents();
    },
    registerEvents: function () {
        $('.btnActive').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');
            if (confirm('Bạn muốn hủy đơn hàng này ?')) {
                $.ajax({
                    url: "/Account/CancelOrder",
                    data: { id: id },
                    dataType: "json",
                    type: "POST",
                    success: function (reponse) {
                        if (reponse.Status == 1) {
                            btn.text('Hủy đơn');
                        } else {
                            btn.text('Đơn đã được hủy bỏ!');
                        }
                    }
                });
            }
        });
        $('.btnexcept').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');
            if (confirm('Bạn đã nhận được hàng!')) {
                $.ajax({
                    url: "/Account/ExceptOrder",
                    data: { id: id },
                    dataType: "json",
                    type: "POST",
                    success: function (reponse) {
                        if (reponse.Status == 2) {
                            btn.text('Đã Nhận Được Hàng');
                        } else {
                            btn.text('Cảm ơn bạn đã xác nhận!');
                        }
                    }
                });
            }
        });
    }
}
user.init();