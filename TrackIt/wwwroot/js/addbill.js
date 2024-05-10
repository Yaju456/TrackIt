﻿var mySet = new Set();
$(document).ready(function () {
    LoadTable("");
});


function dateSearch() {
    console.log(Date.parse($("#From").val()));
    var from_date = Date.parse($("#From").val());
    var to_date = Date.parse($("#TOo").val());
    
    $.ajax({
        url: '/bill/AllBill',
        type: 'Get',
        dataType: 'json',
        contentType: 'application/ json; charset = utf - 8;',
        success: function (result) {
            var Obj = "";

            $.each(result, function (index, value) {
                console.log(Date.parse(value.date));
                if (Date.parse(value.date) >= from_date && Date.parse(value.date) <= to_date) {
                    Obj += '<tr>';
                    Obj += '<td>' + String(value.date).slice(0, 10) + '</td>';
                    Obj += '<td>' + value.description + '</td>';
                    Obj += '<td>' + value.total + '</td>';
                    Obj += '<td>' + value.customer.name + '</td>';
                    if (value.payment == null || value.payment == 0) {
                        Obj += '<td style="text-align: center;"><i class="text-danger bi bi-x-circle h1"></i></td>';
                    }
                    else if (value.payment < (value.total - value.total * 0.015)) {
                        Obj += '<td style="text-align: center;"><i class="text-warning bi bi-arrow-clockwise h1"></i></td>';
                    }
                    else {
                        Obj += '<td style="text-align: center;"><i class="text-success bi bi-check2-circle h1"></i></td>';
                    }
                    Obj += '<td> <a href="/bill/check?id=' + value.id + '" class="btn btn-success">View</a>';
                    Obj += '<td> <button class="btn btn-danger" onclick=Delete(' + value.id + ')>Delete</button><td>';
                    Obj += '</tr>';
                }
            });
            $("#t-body").html(Obj);
        }
    });
}

$("#form-search").on("submit", function (e) {
    e.preventDefault();
    var mval = String($("#myInput").val()).toUpperCase();
    LoadTable(mval);
});
function LoadTable(mval) {
    $.ajax({
        url: '/bill/AllBill',
        type: 'Get',
        dataType: 'json',
        contentType: 'application/ json; charset = utf - 8;',
        success: function (result) {
            var Obj = "";
            
            $.each(result, function (index, value) {
                if (mval == "" || mval == String(value.description).toUpperCase()
                    || mval == String(value.total).toUpperCase() ||
                    mval == String(value.customer.name).toUpperCase() ||
                    mval == (String(value.date).slice(0, 10)))
                {
                    Obj += '<tr>';
                    Obj += '<td>' + String(value.date).slice(0, 10) + '</td>';
                    mySet.add(String(value.date).slice(0, 10));
                    Obj += '<td>' + value.description + '</td>';
                    mySet.add(String(value.description).toUpperCase());

                    Obj += '<td>' + value.total + '</td>';
                    mySet.add(String(value.total).toUpperCase());

                    Obj += '<td>' + value.customer.name + '</td>';
                    mySet.add(String(value.customer.name).toUpperCase());
                    if (value.payment == null || value.payment == 0) {
                        Obj += '<td style="text-align: center;"><i class="text-danger bi bi-x-circle h1"></i></td>';
                    }
                    else if (value.payment < (value.total - value.total * 0.015)) {
                        Obj += '<td style="text-align: center;"><i class="text-warning bi bi-arrow-clockwise h1"></i></td>';
                    }
                    else {
                        Obj += '<td style="text-align: center;"><i class="text-success bi bi-check2-circle h1"></i></td>';
                    }
                    Obj += '<td> <a href="/bill/check?id=' + value.id + '" class="btn btn-success">View</a>';
                    Obj += '<td> <button class="btn btn-danger" onclick=Delete(' + value.id + ')>Delete</button><td>';
                    Obj += '</tr>';
                }
            });
            autocomplete(document.getElementById("myInput"), Array.from(mySet));
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
                        LoadTable("");
                    },

                })
            },
            cancel: function () {
                $.alert('Canceled!');
            }
        }
    });
}