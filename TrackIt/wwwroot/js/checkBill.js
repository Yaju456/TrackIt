﻿$(document).ready(function(){
    reloadTable();
    
});

function View(id) {
    var URL = '/bill/stockid?id=' + id;
    $.ajax({
        url: URL,
        data: 'json',
        type: 'Get',
        contentType: 'application/json; charset= utf-8;',
        success: function (result) {
            var Obj = ""
            $.each(result, function (index, value) {
                Obj += '<tr>';
                Obj += '<td>' + value.serial_number + '</td>';
                Obj += '<td>' + value.order_id + '</td>';
                Obj += '</td>';
            });
            $("#tm-body").html(Obj);
        },

    });

}

$("#Payment").change(function () {
    $(".ToRemove1").remove();
    calVat();
});



function calVat()
{
    
    var payment = $("#Payment").val();
    var after_TDS = Number($("#t-otal").html()) - Number($("#TDS").html());
    var after_vat = after_TDS + Number($("#VAT").html());
    console.log(Number($("#t-otal").html()));
    console.log(after_vat);
    if (payment >= after_TDS && payment <= after_vat)
    {


        var received_vat = payment - after_TDS;
        var vat_percent = received_vat / Number($("#VAT").html()) * 100;
        
        var Obj = '<tr class="ToRemove1">';
        Obj += '<td><td>';
        Obj += '<th>Received VAT</th>';
        Obj += '<td >' + received_vat + '</td>';
        Obj += '</tr>';

        Obj += '<tr class="ToRemove1">';
        Obj += '<td><td>';
        Obj += '<th>received VAT percent</th>';
        Obj += '<td >' + vat_percent.toPrecision(4) + ' %</td>';
        Obj += '</tr>';

        Obj += '<tr class="ToRemove1">';
        Obj += '<td><td>';
        Obj += '<th>Govenment VAT percent</th>';
        Obj += '<td >' + (100 - vat_percent.toPrecision(4)).toPrecision(4) + ' %</td>';
        Obj += '</tr>';

        $("#t-body").append(Obj);

    }

}
function reloadTable() {
    var URL = '/bill/getCombill?id=' + $("#ID").val();
    $.ajax({
        url: URL,
        data: 'json',
        type: 'Get',
        contentType: 'application/ json; charset = utf - 8;',
        success: function (result) {
            var Obj = "";
            var total = 0;
            $.each(result, function (index, value) {
                Obj += '<tr>';
                Obj += '<td>' + value.product.name + '</td>';
                Obj += '<td>' + value.rate + '</td>';
                Obj += '<td>' + value.quantity + '</td>';
                Obj += '<td>' + value.total + '</td>';
                Obj += '<td> <button onclick=View(' + value.id + ') class="btn btn-success"  data-toggle="modal" data-target="#exampleModal">View</button> '; 
                total += value.total;
                Obj += '</tr>';
            });
            Obj += '<tr>';
            Obj += '<td><td>';
            Obj += '<th>Total</th>';
            Obj += '<td id="t-otal">' + total + '</td>';
            Obj += '</tr>';
            var VAT = total * 0.13;
            Obj += '<tr>';
            //
            Obj += '<td><td>';
            Obj += '<th>13% VAT</th>';
            Obj += '<td id="VAT">' + VAT + '</td>';
            Obj += '</tr>';
            //
            var TDS = (total * 0.015);
            Obj += '<tr>';
            Obj += '<td><td>';
            Obj += '<th>TDS</th>';
            Obj += '<td id="TDS">' + TDS + '</td>';
            Obj += '</tr>';
            //
            Obj += '<tr>';
            Obj += '<td><td>';
            Obj += '<th>G. Total</th>';
            Obj += '<td id="G-total">' + (total + VAT - TDS) + '</td>';
            Obj += '</tr>';
            //
            $("#t-body").html(Obj);
            if ($("#Payment").val != null) {
                calVat();
            }
        }
    });
}