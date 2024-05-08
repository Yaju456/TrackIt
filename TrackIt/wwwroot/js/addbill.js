$(document).ready(function () {
    LoadTable();
});

function LoadTable() {
    $.ajax({
        url: '/bill/AllBill',
        type: 'Get',
        dataType: 'json',
        contentType: 'application/ json; charset = utf - 8;',
        success: function (result) {
            var Obj = "";
            $.each(result, function (index, value) {
                Obj += '<tr>';
                Obj += '<td>' + value.description + '</td>';
                Obj += '<td>' + value.total + '</td>';
                Obj += '<td>' + value.customer.name + '</td>';
                Obj += '<td> <a href="/bill/check?id=' + value.id + '" class="btn btn-success">View</a>';
                Obj += '<td> <button class="btn btn-danger" onclick=Delete(' + value.id + ')>Delete</button><td>'; 
                Obj += '</tr>';
            });
            $("#t-body").html(Obj);
        }
    });
}

function Delete(id)
{
    var Url = '/bill/Deletebill?id=' + id;
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
                        LoadTable();
                    },

                })
            },
            cancel: function () {
                $.alert('Canceled!');
            }
        }
    });
}