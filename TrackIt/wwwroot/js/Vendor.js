$(document).ready(function () {
    reloadTable();
});

function reloadTable() {
    $.ajax({
        url: '/Main/GetVendor',
        type: 'Get',
        dataType: 'json',
        contentType: 'application/jon; charset=utf-8',
        success: function (result) {
            var Obj = "";
            $.each(result, function (index, value) {
                Obj += '<tr class="text-center">';
                Obj += '<td>' + value.id + '</td>';
                Obj += '<td>' + value.name + '</td>';
                Obj += '<td>' + value.description + '</td>';
                Obj += '<td>' + value.phoneNumber + '</td>';
                Obj += '<td><button type="button" class="btn  btn-success" data-toggle="modal" data-target="#exampleModal" onclick="OneAdd(' + value.id + ', \'' + value.name + '\',\'' + value.description + '\',' + value.phoneNumber +')"><i class="bi bi-pencil-square"></i> Edit</button></td>';
                Obj += '<td><a class="btn btn-danger" onclick=Delete("/Main/DeleteVendor?id=' + value.id + '")><i class="bi bi-trash"></i> Delete</a></td>';
                Obj += '</tr>'
            });
            $("#t-body").html(Obj);

        },
        error: function () {
            alert("There was error fetching Data");
        }
    })
}

function OneAdd(id, name, Description, PhoneNumber) {
    $("#ID").val(id);
    $("#Name").val(name);
    $("#Description").val(Description);
    $("#Phonenumber").val(PhoneNumber);
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

$("#myForm").on("submit", function (e) {
    e.preventDefault();
    var Id = $("#ID").val();
    var Name = $("#Name").val();
    var PhoneNumber = $("#Phonenumber").val();
    var Description = $("#Description").val();
    var obj = {
        id: Id,
        name: Name,
        phonenumber: PhoneNumber,
        description: Description,
    };

    $.ajax({
        url: '/Main/Addvendor',
        type: 'Post',
        dataType: 'json',
        data: obj,
        success: function (response) {
            if (response.success) {
                toastr["success"](response.message, response.type, { timeOut: 5000 });
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