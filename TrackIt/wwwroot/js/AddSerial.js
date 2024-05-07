$(document).ready(function () {
    reloadTable();
});
function reloadTable() {
    var Order_id = $("#Value-id").val();
    console.log(Order_id);
    var URL = "";
    if (Order_id == 0) {
        URL = '/order/getMost';
    }
    else
    {
        URL = '/order/get?id=' + Order_id;
    }
     

    $.ajax({
        url: URL,
        type: 'Get',
        dataType: 'json',
        contentType: 'application/jon; charset=utf-8',
        success: function (result, status, xhr) {
            var Obj = "";
            $.each(result, function (index, value) {
                //if (value.serial_number != null)
                //{
                //    return;
                //}
                Obj += '<tr>';
                Obj += '<td>' + value.order_id + '</td>';
                Obj += '<td>' + value.product_id + '</td>';
                Obj += '<td>' + value.serial_number + '</td>';
                Obj += '<td>' + value.product.name+ '</td>';
                Obj += '<td>' + value.inStock + '</td>';
                Obj += '<td>' + String(value.customer_id) + '</td>';
                if (value.customer_id != null) {
                    Obj += '<td>' + value.customer.name + '</td>';
                }
                else {
                    Obj +='<td>null</td>'
                }
                Obj += '<td><button type="button" class="btn btn-success" data-toggle="modal" data-target="#exampleModal" onclick="OneAdd(' + value.id + ', \'' + value.serial_number + '\')"><i class="bi bi-pencil-square"></i> Edit</button></td>';
                Obj += '<td><a class="btn btn-danger" onclick=Delete("/Order/Deletestock?id=' + value.id + '")><i class="bi bi-trash"></i> Delete</a></td>';

            });
            $('#t-body').html(Obj);
        },
        Error: function (result) {
            alert(result);
        }
    })
}

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

function OneAdd(id, serial_no)
{
    $("#Stock_NO").val(id);
    $("#serial_no").val(serial_no); 
}

$("#myForm").on("submit", function (e) {
    e.preventDefault();
    var Id = $("#Stock_NO").val(); 
    console.log(Id);
    var Serial_no = $("#serial_no").val();
    var Customer_id = $("#Customer").val();
    var obj = {
        id: Id,
        serial_no: Serial_no,
        customer_id: Customer_id
    };

    $.ajax({
        url: '/Order/AddSerial',
        type: 'Post',
        dataType: 'json',
        data: obj,
        success: function (response) {
            if (response.success) {
                toastr["success"](response.message, "Value Added", { timeOut: 5000 });
                document.getElementById("myForm").reset();
                reloadTable();

            }
            else {
                toastr["error"](response.message, "Not entered", { timeOut: 5000 });
            }
        },
        error: function (how) {
            alert("Missing Form data");
        }
    })
});




$("#myForm1").on("submit", function (e) {
    e.preventDefault();
    var Id = $("#Stock_NO1").val();
    console.log(Id);
    var Order_id = $("#Order_NO1").val();
    var Serial_no = $("#serial_no1").val();
    var Customer_id = $("#Customer1").val();
    var obj = {
        id: Id,
        order_id: Order_id,
        serial_no: Serial_no,
        customer_id: Customer_id
    };

    $.ajax({
        url: '/Order/AddStock',
        type: 'Post',
        dataType: 'json',
        data: obj,
        success: function (response) {
            if (response.success) {
                toastr["success"](response.message, "Value Added", { timeOut: 5000 });
                document.getElementById("myForm").reset();
                reloadTable();

            }
            else {
                toastr["error"](response.message, "Not entered", { timeOut: 5000 });
            }
        },
        error: function (how) {
            alert("Missing Form data");
        }
    })
});