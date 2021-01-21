﻿var cart = {
    init: function () {
        cart.regEvents();
    },
    regEvents: function () {
        $('#btnTiepTuc').off('click').on('click', function (e) {
            e.preventDefault();
            window.location.href = "/"; //đưa về trang chủ tiếp tục mua hàng
        });
        $('#btnPayment').off('click').on('click', function (e) {
            e.preventDefault();
            window.location.href = "/thanh-toan"; //đưa về trang chủ tiếp tục mua hàng
        });
        $('#btnUpdate').off('click').on('click', function () {
            var listProduct = $('.txtQuantity');
            var cartList = [];
            $.each(listProduct, function (i, item) {
                cartList.push({
                    Quantity: $(item).val(),
                    Product: {
                        ID: $(item).data('id')
                    }
                });
            });

            $.ajax({
                url: '/Details/Update',
                data: { cartModel: JSON.stringify(cartList) },
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = "/gio-hang";
                    }
                }
            })
        });
       
        
    }
}
cart.init();