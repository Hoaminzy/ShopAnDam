//<reference path="jquery-1.9.1.intellisense.js" />
//Load Data in Table when documents is ready
var tuKhoa = '';
$(document).ready(function () {
    $("#txtSearch").on("keyup", function () {
        var value = $('#txtSearch').val().toLowerCase();
        $(".tbody tr").filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
        });
    });
    loadData();
 
});

//Load Data function
function loadData() {
    $.ajax({
        url: "/Admin/Brand/List",
        type: "GET",
       
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        //success: function (data) {
        //    var html = '';
        //    $.each(data, function (key, item) {
        //        html += '<tr>';
        //        html += '<td>' + item.ID + '</td>';
        //        html += '<td>' + item.Name + '</td>';
        //        html += '<td>' + item.Logo + '</td>';
        //        html += '<td><a href="#" class="btn btn-primary btn-xs"  onclick="return getbyID(' + item.ID + ')"><i class="fa fa-pencil"></a> | <a href="#" class="btn btn-danger btn-xs fa fa-trash-o"  onclick="Delele(' + item.ID + ')"></a></td>';
        //        html += '</tr>';
        //    });
        //    $('.tbody').html(html);
        //},
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}


//Add Data Function 
function Add() {
    var res = validate();
    if (res == false) {
        return false;
    }
    var Obj = {
        ID: $('#ID').val(),
        Name: $('#Name').val(),
        Logo: $('#Logo').val(),
       
    };
    $.ajax({
        url: "Brand/Add",
        data: JSON.stringify(Obj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            loadData();
            $('#myModal').modal('hide');
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    loadData();
}

//Function for getting the Data Based upon Employee ID
function getbyID(ID) {
    $('#Name').css('border-color', 'lightgrey');
    $('#Logo').css('border-color', 'lightgrey');
   
    $.ajax({
        url: "Brand/getbyID/" + ID,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            loadData();
            $('#ID').val(result.ID);
            $('#Name').val(result.Name);
            $('#Logo').val(result.Logo);
          ;

            $('#myModal').modal('show');
            $('#btnUpdate').show();
            $('#btnAdd').hide();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
}

//function for updating employee's record
function Update() {
    var res = validate();
    if (res == false) {
        return false;
    }
    var Obj = {
        ID: $('#ID').val(),
        Name: $('#Name').val(),
        Age: $('#Logo').val(),

    };
    $.ajax({
        url: "/Admin/Brand/Update",
        data: JSON.stringify(Obj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            loadData();
            $('#myModal').modal('hide');
            $('#ID').val("");
            $('#Name').val("");
            $('#Logo').val("");
      
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

//function for deleting employee's record
function Delele(ID) {
    var ans = confirm("Are you sure you want to delete this Record?");
    if (ans) {
        $.ajax({
            url: "/Brand/Delete/" + ID,
            type: "POST",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {
                loadData();
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
}

//Function for clearing the textboxes
function clearTextBox() {
    $('#ID').val("");
    $('#Name').val("");
    $('#Logo').val("");
    $('#btnUpdate').hide();
    $('#btnAdd').show();
    $('#Name').css('border-color', 'lightgrey');
    $('#Logo').css('border-color', 'lightgrey');
 
}
//Valdidation using jquery
function validate() {
    var isValid = true;
    if ($('#Name').val().trim() == "") {
        $('#Name').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Name').css('border-color', 'lightgrey');
    }
    if ($('#Logo').val().trim() == "") {
        $('#Logo').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Logo').css('border-color', 'lightgrey');
    }
    
    return isValid;
}