$(document).ready(function () {
    reloadTable();
});
function reloadTable() {
    $.ajax({
        url: '/Product/GetAll',
        type: 'Get',
        dataType: 'json',
        ContentType: 'application/json;charset=utf-8;',
        success: function (result, status, xhr) {
            var Obj = "";
            var Instock = "";
            $.each(result, function (index, value) {
                Obj += '<tr>';
                Obj += '<td>' + value.id + '</td>';
                Obj += '<td>' + value.name + '</td>';
                Obj += '<td>' + value.description + '</td>';
                Obj += '<td>' + value.category + '</td>';
                Obj += '<td>' + value.company + '</td>';
                if (typeof value.in_stock != "number") {
                    Instock = 0;
                }
                else {
                    Instock = value.in_stock;
                }
                Obj += '<td>' + Instock + '</td>';
                Obj += '<td><a onclick=Delete("/Product/Delete?id=' + value.id + '") class="btn btn-danger">Delete</a></td>';

            });
            $("#t-body").html(Obj);
        },
        error: function () {
            alert("there was an error");
        }
    })
}

function Delete(Url)
{

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

$("#my-form").on("submit", function (e) {
    e.preventDefault();
    var Id = $("#id").val();
    var Name = $("#Name").val();
    var Description = $("#Description").val();
    var Catagory = $("#C-at").val();
    var Company = $("#Company").val();
    var obj = {
        id: Id,
        name: Name,
        description: Description,
        category: Catagory,
        company: Company
    };
    $.ajax({
        url: '/product/Index',
        type: 'Post',
        dataType: 'json',
        data: obj,
        success: function (response) {
            if (response.success) {
                toastr["success"](response.message, "Value Added", { timeOut: 5000 });
                document.getElementById("my-form").reset();
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