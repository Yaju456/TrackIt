$(document).ready(function () {
    reloadTable();
    $.ajax({
        url: '/Main/getProvince',
        type: 'Get',
        success: function (result) {
            $("#ProvinceOption").empty();
            $('#DistrictOption').append('<option disabled selected>--Select District--</option>');
            $('#LocalBodyOption').append('<option disabled selected>--Select Local Body--</option>');
            $('#ProvinceOption').append('<option disabled selected>--Select Province--</option>');
            $.each(result, function (index, value) {
                $('#ProvinceOption').append('<option value="' + value.id + '">' + value.name + '</option>');
            });

        }
    });

   // populateDistrictSelect($("#ProvinceOption").val());
    $('#ProvinceOption').change(function () {
        var selectedOption = $(this).val();
        populateDistrictSelect(selectedOption);
    });

    $('#DistrictOption').change(function () {
        var selectedOption = $(this).val();
        populateLocalBodySelect(selectedOption);
    });

});


function populateDistrictSelect(P_id) {
    var URL = '/Main/getDistrict/?id=' + P_id;
    $('#DistrictOption').empty(); // Clear existing options
    

    $.ajax({
        url: URL,
        type: 'Get',
        success: function (result) {
            $("#DistrictOption").empty();
            $('#DistrictOption').append('<option disabled selected>--Select District--</option>');
            $('#LocalBodyOption').empty(); 
            $('#LocalBodyOption').append('<option disabled selected>--Select Local Body--</option>');

            $.each(result, function (index, value) {
                $('#DistrictOption').append('<option value="' + value.id + '">' + value.name + '</option>');
            })
        }
    });
}

function populateLocalBodySelect(P_id) {
    var URL = '/Main/getLocalBody/?id=' + P_id;
    $('#LocalBodyOption').empty(); 
    $.ajax({
        url: URL,
        type: 'Get',
        success: function (result) {
            $("#LocalBodyOption").empty();
            $('#LocalBodyOption').append('<option disabled selected>--Select Local Body--</option>');

            $.each(result, function (index, value) {
                $('#LocalBodyOption').append('<option value="' + value.id + '">' + value.name + '</option>');
            })
        }
    });
}





function reloadTable() {
    $.ajax({
        url: '/Main/getAllcutomer',
        type: 'Get',
        dataType: 'json',
        contentType: 'application/jon; charset=utf-8', 
        success: function (result)
        {
            var Obj = "";
            $.each(result, function (index, value) {
                Obj += '<tr class="text-center">';
                Obj += '<td>' + value.id + '</td>';
                Obj += '<td>' + value.name + '</td>';
                if (value.phoneNumber != null) {
                    Obj += '<td>' + value.phoneNumber + '</td>';

                }

                if (value.provinceId != null) {
                    Obj += '<td>' + value.province.name + '</td>';
                    Obj += '<td>' + value.district.name + '</td>';
                    Obj += '<td>' + value.localBody.name + '</td>';
                }
                Obj += '<td><button type="button" class="btn  btn-success" data-toggle="modal" data-target="#exampleModal" \
                onclick="OneAdd(' + value.id + ', \'' + value.name + '\'' + value.phoneNumber+')"><i class="bi bi-pencil-square"></i> Edit</button></td>';                
                Obj += '<td><a class="btn btn-danger" onclick=Delete("/Main/Delete?id=' + value.id + '")><i class="bi bi-trash"></i> Delete</a></td>';
                Obj +='</tr>'
            });
            $("#t-body").html(Obj);

        },
        error: function () {
            alert("There was error fetching Data");
        }
    })
}

function OneAdd(id, name) {
    $("#ID").val(id);
    $("#Name").val(name);
    $("#phoneNumber").val(phoneNumber);
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
    var PhoneNumber = $("#phoneNumber").val();
    var Province = $("#ProvinceOption").val();
    var District= $("#DistrictOption").val();
    var LocalBody = $("#LocalBodyOption").val();
    var obj = {
        id: Id,
        name: Name,
        phoneNumber: PhoneNumber,
        provinceId: Province,
        districtId: District,
        localBodyId: LocalBody
    };

    $.ajax({
        url: '/Main/CustomerInfo',
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