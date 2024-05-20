$(document).ready(function () {
    getProductList();
});

function getProductList() {
    _Ajax("get", "/home/getproductlist", { categoryid: CateGoryID }, "json", "application/json", getProductListSuccess, null, true, false);
}

function getProductListSuccess(result) {
    var prodListDiv = $("#ProductList");

    if (result.length > 0) {

        $(prodListDiv).empty();
        result.forEach((x, y) => {
            $(prodListDiv).append(`<div class="col-sm-4" style="padding:10px;">
        <div class="box-shadow"  style="display: grid; ">
            <div style="text-align:center;">
                <h4>${x.ProdName}</h4>
            </div>
            <div style="justify-content: center; display: flex;">
                <img src="${x.ProdImage}" alt=${x.ProdName}>
            </div>
            <div style="padding: 10px;">
                <p>${x.ProdDesc}</p>
            </div>
             <div style="text-align:center;">
                <h4>$${x.Price.toFixed(2)}</h4>
            </div>
            <div style="padding: 10px;justify-content: center; display: flex;">
                <button product-id=${x.ProdID} class="btn btn-warning">Add To Cart</button>
            </div>
        </div>
    </div>`);
        });
    }
    else {
        $("#ProductList").html(`<div style="text-align:center;padding:10px;" > <h3>No Data Found</h3> </div>`);
    }
}


$(document).on("click", "[product-id]", function () {
    var productID = $(this).attr("product-id");
    getCartProdQty(productID);
});

function getCartProdQty(productID) {
    _Ajax("get", "/home/GetCartProductQty", { productID: productID }, "json", "application/json", function (res) { getCartProdQtySuccess(res, productID) }, null, true, false);
}

function getCartProdQtySuccess(qty, productID) {
    $("#CartQty").val(qty);
    $("[addcart-product-id]").attr("addcart-product-id", productID);
    $("#quantityModal").modal("show");
}

$(document).on("click", "[addcart-product-id]", function () {
    var productID = $(this).attr("addcart-product-id");
    var qty = $("#CartQty").val();

    if (qty > 0) {
        _Ajax("post", "/home/InsertUpdateCart", { productID: productID, qty: qty }, "", "application/json", function () { $("#quantityModal").modal("hide"); }, null, true, false);
    }
    else {
        $("#AddQtyErrorModal").modal("show");
    }
});
