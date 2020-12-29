//$(document).ready(function () {
    
//})
$('#name').keypress(function () {
    $('#meta').val(getMeta($(this).val()));
});

function getMeta(title) {
    return title.toLowerCase().trim()  //đưa hết về kiểu chữ thường
        .replace(/à|á|ạ|ả|ã|ằ|ắ|ẳ|ặ|ẵ|ấ|ầ|ẩ|ậ|ẫ/g, "a")
        .replace(/\ /g, '-')
        .replace(/đ/g, "d")
        .replace(/ỳ|ý|ỵ|ỷ|ỹ/g, "y")
        .replace(/ù|ú|ụ|ủ|ũ|ừ|ứ|ử|ự|ữ/g, "u")
        .replace(/ó|ò|ỏ|ọ|õ|ồ|ố|ổ|ộ|ỗ|ớ|ờ|ở|ợ|ỡ.+/g, "o")
        .replace(/è|é|ẻ|ẹ|ẽ|ề|ế|ệ|ể|ễ/g, "e")
        .replace(/ì|í|ỉ|ị|ĩ/g, "i")
        .replace(/\s+/g, '-')
        .replace(/&/g, '-va-')
        .replace(/[^\w\-]+/g, '')
        .replace(/\-\-+/g, '-');
}

var user = {
    init: function () {
        user.registerEvents();
    },
    registerEvents: function () {
        $('.btn-active').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');
            $.ajax({
                url: "/Admin/Category/ChangeCategory",
                data: { id: id },
                dataType: "json",
                type: "POST",
                success: function (response) {
                    if (response.status == true) {
                        btn.text('Kích hoạt');
                    } else {
                        btn.text('Khóa');
                    }
                }
            });
        });
    }
}
user.init();