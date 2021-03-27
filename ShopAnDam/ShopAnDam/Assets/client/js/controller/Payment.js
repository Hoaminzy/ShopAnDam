var user = {
    init: function () {

        user.loadProvince();
        user.registerEvent();
    },
    registerEvent: function () {
        $('#ddlProvint').off('change').on('change', function () {
            var id = $(this).val();
            $("#hdnTenTinhThanh").val($(this).find("option:selected").text());
            if (id != '') {
                user.loadDistrict(parseInt(id));
            }
            else {
                $('#ddlDistrict').html('');
            }
        });
        $('#ddlDistrict').off('change').on('change', function () {
            $("#hdnTenQuanHuyen").val($(this).find("option:selected").text());

        });
        $('input[name="option_payment"]').off('click').on('click', function () {
            if ($(this).val() == 'COD') {
                $('.boxContent').hide();
                $('#hdnFormOFPayment').val($(this).val());
            }
            else if ($(this).val() == 'NL') {
                $('.boxContent').hide();
                $('#nganluongpayment').show();
                $('#hdnFormOFPayment').val($(this).val());

            }
            else if ($(this).val() == 'ATM_ONLINE') {
                $('.boxContent').hide();
                $('#bankpayment').show();
                $('#hdnFormOFPayment').val($(this).val());
                $('input[name="bankcode"]').off('click').on('click', function () {
                    $('#hdnBankCode').val($(this).prop('id'));
                });
            }
            else {
                $('.boxContent').hide();
                $('#hdnFormOFPayment').val($(this).val());


            }
        });

    },

    loadProvince: function () {

        $.ajax({
            url: '/Cart/LoadProvince',
            type: "POST",
            dataType: "json",
            success: function (response) {
                if (response.status == true) {
                    var html = '<option value="">--Chọn tỉnh thành--</option>';
                    var data = response.data;
                    $.each(data, function (i, item) {
                        html += '<option value="' + item.ID + '" text="' + item.Name + '">' + item.Name + '</option>'
                    });
                    $('#ddlProvint').html(html);
                }
            }
        })
    },
    loadDistrict: function (id) {
        $.ajax({
            url: '/Cart/LoadDistrict',
            type: "POST",
            data: { tinhThanhID: id },
            dataType: "json",
            success: function (response) {
                if (response.status == true) {
                    var html = '<option value="">--Chọn quận huyện--</option>';
                    var data = response.data;
                    $.each(data, function (i, item) {
                        html += '<option value="' + item.ID + '"  text="' + item.Name + '">' + item.Name + '</option>'
                    });
                    $('#ddlDistrict').html(html);
                }
            }
        })
    },
    



}
user.init();
