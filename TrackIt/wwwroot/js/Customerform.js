var nameSet = new Set();
function fillOptions() {
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
}

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


function OneAdd(id, name, phoneNumber) {
    $("#ID1").val(id);
    $("#Name1").val(name);
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
/////////////////////////////////////////////////////////

$("#CustomerForm").on("submit", function (e) {
    e.preventDefault();
    var Id = $("#ID1").val();
    var Name = $("#Name1").val();
    var PhoneNumber = $("#phoneNumber").val();
    var Province = $("#ProvinceOption").val();
    var District = $("#DistrictOption").val();
    var LocalBody = $("#LocalBodyOption").val();
    var obj = {
        id: Id,
        name: Name,
        phoneNumber: PhoneNumber,
        provinceId: Province,
        districtId: District,
        localBodyId: LocalBody
    };

    if (nameSet.has(Name.toUpperCase()) && Id == 0) {
        $.confirm({
            title: 'Name Alread Exists!',
            content: 'Given Customer Name Already Exists are you sure this is new Customer?',
            buttons: {
                confirm: function () {
                    f_submit(obj);
                },
                cancel: function () {
                    $.alert('Canceled!');
                }
            }
        });
    }
    else {
        f_submit(obj);
    }

});


function f_submit(obj) {
    $.ajax({
        url: '/Main/CustomerInfo',
        type: 'Post',
        dataType: 'json',
        data: obj,
        success: function (response) {
            if (response.success) {
                toastr["success"](response.message, response.type, { timeOut: 5000 });
                document.getElementById("CustomerForm").reset();
                reloadTable("");
                fillCustomer();
            }
            else {
                toastr["error"](response.message, "Not entered", { timeOut: 5000 });
            }
        },
        error: function (how) {
            alert("Missing Form data");
        }
    });
}