var comment = {
    init: function () {
        comment.regEvents();
    },
    regEvents: function () {
        $('#btn_ProductCMT').off('click').on('click', function () {
            var comment = $('#sNoiDungCMT').val();
            var ProductID = $('#idSanPham').val();
            $.ajax({
                url: "/Product/AddComment",
                type: 'POST',
                dataType: 'json',
                data: {
                    comment: comment,
                    ProductID: ProductID,
                },
                success: function (res) {
                    if (res.status == true) {
                        window.alert('Thêm thành công!');
                        resetForm.resetForm();
                    }
                }
            });
        });
        $('#btn_ContentCMT').off('click').on('click', function () {
            var comment = $('#sNoiDungCMT').val();
            var ArticleID = $('#ArticleID').val();
            $.ajax({
                url: "/Article/AddComment",
                type: 'POST',
                dataType: 'json',
                data: {
                    comment: comment,
                    ArticleID: ArticleID,
                },
                success: function (res) {
                    if (res.status == true) {
                        window.alert('Thêm thành công!');
                        resetForm.resetForm2();
                    }
                }
            });
        });

    },
    resetForm: function () {
        $('#sNoiDungCMT').val('');
    },
    resetForm2: function () {
        $('#sNoiDungCMTBaiViet').val('');
    }
}
comment.init();