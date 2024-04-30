$(document).ready(function () {
    reloadTable();
});
function reloadTable() {
    var Order_id = $("#Value-id").val();
    var URL = '/order/get?id=' + Order_id;

    $.ajax({
        url: URL,
        type: 'Get',
        dataType: 'json',
        contentType: 'application/jon; charset=utf-8',
        success: function (result, status, xhr) {
            var Obj = "";
            $.each(result, function (index, value) {
                if (value.serial_number != null)
                {
                    return;
                }
                Obj += '<tr>';
                Obj += '<td>' + value.order_id + '</td>';
                Obj += '<td>' + value.product_id + '</td>';
                Obj += '<td>' + value.id + '</td>';
                Obj += '<td><button type="button" class="btn btn-primary" data-toggle="modal" data-target="#exampleModal" onclick=OneAdd(' + value.id + ')>Add</button > ';
            });
            $('#t-body').html(Obj);
        },
        Error: function (result) {
            alert(result);
        }
    })
}

function OneAdd(id) {
    $("#Stock_NO").val(id); 
}



$("#myForm").on("submit", function (e) {
    e.preventDefault();
    var Id = $("#Stock_NO").val(); 
    console.log(Id);
    var Serial_no = $("#serial_no").val();
    alert(Id);
    var obj = {
        id: Id,
        serial_no: Serial_no
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