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