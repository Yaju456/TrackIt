$(document).ready(function () {
    $.ajax({
        url:'/Order/getall',
        type: 'Get',
        dataType: 'json',
        ContentType: 'application/jon; charset=utf-8',
        success: function (result, status, xhr) {
            var Obj = "";
            $.each(result, function (index, value) {
                Obj += '<tr>';
                Obj += '<td>' + value.id + '</td>';
                Obj += '<td>' + value.arival.substr(0, 10) + '</td>';
                Obj += '<td>' + value.quantity + '</td>';
                Obj += '<td>' + value.rate + '</td>';
                Obj += '<td>' + value.vendor.name + '</td>';
                Obj += '<td>' + value.product.name + '</td>';
                Obj += '<td> <a Onclick=Delete("/Order/Delete?id='+value.id+'") class="btn btn-danger"">Delete</a></td>';

            });
            $('#t-body').html(Obj);
        },
        Error: function (result) {
            alert(result);
        }
    })
});

function Delete(Url) {
    $.confirm({
        title: 'Confirm!',
        content: 'Simple confirm!',
        buttons: {
            confirm: function () {
                $.ajax({
                    url: Url,
                    type: 'Delete',
                    success: function (data) {
                        toastr["success"](data.message, "Value Deleted" ,{ timeOut: 5000 });

                        setTimeout(function () {
                            window.location.reload();
                        }, 2000);
                    }
                })

            },
            cancel: function () {
                $.alert('Canceled!');
            }
        }
    });
}

$("#myForm").on("submit", function (e) {
    e.preventDefault();
    var id = $("#ID").val();
    var Arival = $("#arival").val();
    var Quantity = $("#qua").val();
    var Rate = $("#rate").val();
    var Vendor_id = $("#V-id").val();
    var Product_id = $("#P-id").val();

    var obj = {
        id: id,
        arival: Arival,
        quantity: Quantity,
        rate: Rate,
        vendor_id: Vendor_id,
        product_id: Product_id,
    }

    $.ajax({
        url: '/Order/Index',
        type: 'Post',
        dataType: 'json',
        data: obj,
        success: function (response) {
            if (response.success) {
                toastr["success"](response.message, "Value Added", { timeOut: 5000 });
                document.getElementById("myForm").reset(); 
                setTimeout(function () {
                    window.location.reload();
                }, 2000);
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