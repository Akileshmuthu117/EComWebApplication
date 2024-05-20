$(document).ready(function () {
    getMyCartList();
});

function getMyCartList() {
    _Ajax("get", "/home/getmycartproduct", "", "json", "application/json", getMyCartListSuccess, null, true, false);
}

function getMyCartListSuccess(result) {

    if (result.length > 0) {

        var mycartTable = $("#MyCartTblBody");
        $(mycartTable).empty();
        result.forEach((x, y) => {
            $(mycartTable).append(`<tr >
            <td>${y + 1}</td>
            <td>${x.ProdName}</td>
            <td> <input inp-prod-id="${x.ProdID}" class="form-control" type="text" value=${x.Qty} readonly/>  </td>
            <td>${x.Price.toFixed(2)}</td>
            <td>${x.TotalPrice.toFixed(2)}</td>
            <td><a edit-prod-id=${x.ProdID}>Edit</a> / <a del-product-id=${x.ProdID}>Delete</a></td>
        </tr>`);
        });
    }
    else {
        $("#MyCartTable").html(` <div style="text-align:center;padding:10px;" > <h3>No Data Found</h3> </div>`);
    }
}


$(document).on("click", "[del-product-id]", function () {
    var productID = $(this).attr("del-product-id");
    _Ajax("post", "/home/deletecartproduct", { productID: productID }, "", "application/json", deleteProdSuccess, null, true, false);
});

function deleteProdSuccess() {
    getMyCartList();
}

$(document).on("keypress", "[inp-prod-id]", function (e) {
    var charCode = (e.which) ? e.which : event.keyCode
    if (String.fromCharCode(charCode).match(/[^0-9]/g))
        return false;
});

$(document).on("click", "[edit-prod-id]", function () {
    var productID = $(this).attr("edit-prod-id");
    var targetInputVal = $(`[inp-prod-id=${productID}]`);
    $(targetInputVal).removeAttr("readonly");
});


$(document).on("blur", "[inp-prod-id]", function (e) {
    e.preventDefault();
    var productID = $(this).attr("inp-prod-id");
    var qty = $(this).val();
    if (qty > 0) {
        _Ajax("post", "/home/InsertUpdateCart", { productID: productID, qty: qty }, "", "application/json", editProdSuccess, null, true, false);
    }
    else {
        $("#AddQtyErrorModal").modal("show");
        $(this).val(1);
    }
});

function editProdSuccess() {
    getMyCartList();
}
