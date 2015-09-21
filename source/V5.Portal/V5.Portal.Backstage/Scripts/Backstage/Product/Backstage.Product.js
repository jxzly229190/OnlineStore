$(function () {
    // 顶部菜单选中样式
    $("#top_product").addClass("select");
    // 默认选中左边菜单的第一个
    $('.left-f ul li:first a').trigger("click");
});

// 请求并显示商品管理下的部分视图
function ShowPartialView(a) {
    // 获取部分视图 Url
    var leftMenuName = $(a).attr("name");
    var topMenuName = $(a).attr("parent");
    var href = "/" + topMenuName + "/" + leftMenuName + "/";

    // 设置当前位置
    $("#current").text($(a).text());

    $.ajax({
        type: "GET",
        url: href,
        data: null,
        datatype: "html",
        success: function (data) {
            if (data.State == -401) {
                onSessionLost();
            } else if (data.State == -403) {
                alert("对不起，您无此操作权限！");
            } else {
                if (data.indexOf("loginBtn") > 0) {
                    location.href = "/login";
                    return false;
                }
                $('.right-box').html(data);
                return true;
            }
        },
        error: function () {
            errorMessage();
        }
    });

    // 更改选中左边菜单的样式
    $(a).parent().addClass("left-s").siblings().removeClass("left-s");
}

// 搜索商品时，商品类别与品牌下拉框联动，获取父级编号方法
function GetParentCategoryID() {
    return {
        ParentID: $("#ParentCategory").val()
    };
}
function GetProductCategoryID() {
    return {
        categoryID: $("#ProductCategory").val()
    };
}
function GetParentBrandID() {
    return {
        parentID: $("#ParentBrand").val()
    };
}

// 搜索商品时，获取搜索条件值
function SearchProductData() {
    return {
        parentCategoryID: $("#ParentCategory").val(),
        productCategoryID: $("#ProductCategory").val(),
        parentBrandID: $("#ParentBrand").val(),
        productBrandID: $("#ProductBrand").val(),
        productName: $("#ProductName").val(),
        barcode: $("#Barcode").val(),
        minPrice: $("#MinPrice").val(),
        maxPrice: $("#MaxPrice").val()
    };
}