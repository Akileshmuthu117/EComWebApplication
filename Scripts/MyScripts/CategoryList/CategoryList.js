$(document).ready(function () {
    getCategoryList();
});

function getCategoryList() {
    _Ajax("get", "/home/getproductcatory", "", "json", "application/json", getCategoryListSuccess, null, true, false);
}

function getCategoryListSuccess(result) {
    var categoryListDiv = $("#CategoryList");
    if (result.length > 0) {
        $(categoryListDiv).empty();
        result.forEach((x, y) => {
            $(categoryListDiv).append(`<div class="col-sm-4" category-id=${x.CatID} style="padding:10px;">
        <div class="box-shadow" style="display: grid;">
            <div style="text-align:center;">
                <h4>${x.CatName}</h4>
            </div>
            <div style="justify-content: center; display: flex;">
                <img src="${x.CatImage}" alt=${x.CatName}>
            </div>
            <div style="padding: 10px;">
                <p>${x.CatDesc}</p>
            </div>
        </div>
    </div>`);
        });
    }
    else {
        $(categoryListDiv).html(`<div style="text-align:center;padding:10px;" > <h3>No Data Found</h3> </div>`);
    }
}

$(document).on("click", "[category-id]", function () {
    var categoryID = $(this).attr("category-id");
    window.location.href = "/home/productlist?categoryid=" + categoryID;
});
