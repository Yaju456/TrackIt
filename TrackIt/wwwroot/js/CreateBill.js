$(document).ready(function () {
    fillCustomer();
    $("#ClientName").change(function () {
        var selectedOption = $(this).val();
        $("#ClientNumber").val(selectedOption);
    });
    reloadTable();
});


$("#AddProduct").on("submit", function (e) {
    e.preventDefault();
    var ID = $("#ID").val();
    var productName = $("#ProductName").val();
    var Rate = $("#Rate").val();
    var Quantity = $("#Quantity").val();
    var Serialn_no = [];
    for (var i = 0; i < Quantity; i++) {
        var sid = "#S-" + i;
        Serialn_no.push($(sid).val());
    }
    var Class = {
        id: ID,
        product_id: productName,
        rate: Rate,
        quantity: Quantity,
    };
    var DATA = {
        class: Class,
        serial_no: Serialn_no
    }
    $.ajax({
        url: '/bill/addCom',
        type: 'Post',
        dataType: 'json',
        data: DATA,
        success: function (response) {
            if (response.success) {
                toastr["success"](response.message, "Value Added", { timeOut: 5000 });
                document.getElementById("AddProduct").reset();
                reloadTable();
            }
            else {
                toastr["error"](response.message, "Not entered", { timeOut: 5000 });
            }
        },
        error: function (xhr, textStatus, error) {
            console.log("error");
            alert(xhr.statusText);
            console.log(textStatus);
            console.log(error);
        }
    });
});

function fillCustomer() {
    $.ajax({
        url: '/Main/getAllcutomer',
        type: 'Get',
        dataType: 'json',
        contentType: 'application/jon; charset=utf-8',
        success: function (result) {
            $("#ClientName").empty();
            $('#ClientName').append('<option disabled selected>--Select Customer--</option>');
            $("#ClientNumber").empty();
            $('#ClientNumber').append('<option disabled selected>--Customer Number--</option>');

            $.each(result, function (index, value) {
                $('#ClientName').append('<option value="' + value.id + '">' + value.name + '</option>');
                $('#ClientNumber').append('<option value="' + value.id + '">' + value.phoneNumber + '</option>');

            });
        }
    });
}
function reloadTable() {
    $.ajax({
        url: '/bill/getCom',
        type: 'Get',
        data: 'json',
        contentType: 'application/ json; charset = utf - 8;',
        success: function (result) {
            var Obj = "";
            var total = 0;
            $.each(result, function (index, value) {
                Obj += '<tr>';
                Obj += '<td>' + value.product.name + '</td>';
                Obj += '<td>' + value.rate + '</td>';
                Obj += '<td>' + value.quantity + '</td >';
                Obj += '<td>' + value.total + '</td>';
                total += value.total;
                Obj += '<td><a class="btn btn-danger" onclick=Delete("/bill/DeleteCom?id=' + value.id + '")><i class="bi bi-trash"></i> Delete</a></td>';
                Obj += '<td><button class="btn btn-success" onclick=Edit(' + value.id +','+value.product_id + ',' + value.rate + ',' + value.quantity + ') data-toggle="modal" \
        data-target="#exampleModal">Edit</button></td>';
                Obj += '</tr>';
            });
            Obj += '<tr>';
            Obj += '<td><td>';
            Obj += '<th>Total</th>';
            Obj += '<td id="t-otal">' + total + '</td>';
            $("#sub-total").val(total);
            Obj += '</tr>';
            var VAT = total * 0.13;
            Obj += '<tr>';
            Obj += '<td><td>';
            Obj += '<th>13% VAT</th>';
            Obj += '<td>' + VAT + '</td>';
            Obj += '</tr>';
            Obj += '<tr>';
            Obj += '<td><td>';
            Obj += '<th>G. Total</th>';
            Obj += '<td>' + (total + VAT) + '</td>';
            Obj += '</tr>';
            $("#t-body").html(Obj);
        }
    });
}

$("#Quantity").change(function () {
    var total = $("#Quantity").val();
    Obj = "";
    for (var a = 0; a < total; a++) {
        Obj += ' <div class="form-group">\
            <label>Serial no of Product ' + (a+1)+'</label>\
                <input type="text" class="form-control" id="S-'+a+'" placeholder="Serial no">\
                </div>\
        ';
    }
    $("#tada").html(Obj);
});

function Delete(Url) {

    $.confirm({
        title: 'Confirm!',
        content: 'Simple confirm!',
        buttons: {
            confirm: function () {
                $.ajax({
                    url: Url,
                    type: 'delete',
                    success: function (data) {
                        toastr["success"](data.message, "Value Deleted", { timeOut: 5000 });
                        reloadTable();
                    },

                })
            },
            cancel: function () {
                $.alert('Canceled!');
            }
        }
    });
}

function Edit(i, product, rate, quantity) {
    $("#ID").val(i);
    $("#ProductName").val(product);
    $("#Rate").val(rate);
    $("#Quantity").val(quantity);

}

function Add() {
    $("#ID").val(0);
    $("#ProductName").val(0);
    $("#Rate").val(0);
    $("#Quantity").val(0);

}