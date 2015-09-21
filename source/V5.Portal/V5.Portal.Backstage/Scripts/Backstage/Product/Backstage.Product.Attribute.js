// 显示添加属性的 Span 标签
function showAddAttributeSpan() {
    $("#addAttributeBtn").css("display", "none");
    $("#addAttributeSpan").css("display", "inline");
}
// 当商品类别下拉框选择项改变时，刷新属性 Grid
function subCategoryChange() {
    var filter = new Array();
    var grid = $("#attributeGrid").data("kendoGrid");
    grid.dataSource.filter(filter);
}
// 商品类别下拉框联动参数获取函数
function getProductCategoryID() {
    return { categoryID: $("#SubCategory").val() };
}
function getSubCategory() {
    return {
        ParentID: $("#CategoryParent").val()
    };
}
// 添加属性方法
function addAttribute() {
    if ($("#SubCategory").val() == "-1") {
        alert("请选择商品二级类别！");
        return false;
    }

    $.post(
            "/Product/AddAttribute",
            {
                "productCateID": $("#SubCategory").val(),
                "productAttrName": $("#AttributeName").val()
            },
            function myfunction(data) {
                if (data == '1') {
                    $("#addAttributeSpan").css("display", "none");
                    $("#addAttributeBtn").css("display", "inline");

                    $("#attributeGrid").data("kendoGrid").dataSource.read();
                    $("#attributeGrid").data("kendoGrid").dataSource.refresh();

                    alert("添加属性成功！");

                    $("#AttributeName").val("");
                } else {
                    alert("添加属性出现异常，请联系系统管理员！");
                }
            }
        );
    return false;
}

function ShowWindow() {
    var window = $("#attributeWindow");
    window.kendoWindow({
        width: "420px",
        height: "265px",
        title: "添加属性",
        actions: ["Close"],
        position: { top: 250, left: 500 },
        modal: true
    });
    if ($("#SubCategory").val() == -1) {
        alert("请选择分类");
        return;
    }
    var attr = $("#SubCategory").siblings().text();
    var result = attr.substring(0, attr.length - 6);

    $("#attrSelected").val(result);
    window.data("kendoWindow").open();
}

function onClose() {

    var window = $("#attributeWindow");
    window.kendoWindow({
        width: "420px",
        height: "265px",
        title: "添加属性",
        actions: ["Close"],
        position: { top: 250, left: 500 },
        modal: true
    });
    PostData();
    window.data("kendoWindow").close();
}

function onCancel() {
    var window = $("#attributeWindow");
    window.kendoWindow({
        width: "420px",
        height: "265px",
        title: "添加属性",
        actions: ["Close"],
        position: { top: 250, left: 500 },
        modal: true
    });
    window.data("kendoWindow").close();
}
function PostData() {
    var productCateID = $("#CategoryParent").val();
    var productAttrName = $("#attrSelected").val();
    var inputType = $("#InputType").val();
    var dataType = $("#DataType").val();
    var length = $("#DataLength").val();
    $.ajax({
        url: "/Product/AddAttribute",
        dataType: "json",
        data: { productCateID: productCateID, productAttrName: productAttrName, inputType: inputType, dataType: dataType, length: length },
        type: "POST",
        success: function (data) {
            if (data == "1") {
                alert("添加成功");
            }
        }
    });
}

function GetsDefault() {
    var IsDefault = 0;
    if ($("#DefaultSetting").is(":checked") == true) {
        IsDefault = 1;
    }
    var attributeValue = $("#AttributeValue").val();
    var Sorting = $("#Sorting").val();
    var AttributeID = $("#AttributeID").val();
    var ID = $("#ID").val();
    return {
        isDefault: IsDefault,
        attributeValue: attributeValue,
        sorting: Sorting,
        attributeID: AttributeID,
        id: ID
    };
}
function GetsDefaultsecond() {
    var IsDefault = 0;
    if ($("#DefaultSetting").is(":checked") == true) {
        IsDefault = 1;
    }
    var attributeValue = $("#AttributeValue").val();
    var Sorting = $("#Sorting").val();
    var AttributeID = $("#AttributeID").val();
    var ID = $("#ID").val();
    return {
        isDefault: IsDefault,
        attributeValue: attributeValue,
        sorting: Sorting,
        attributeID: AttributeID,
        id: ID
    };
}

function saveChnages() {
    var IsDefault = 0;
    if ($("#DefaultSetting").is(":checked") == true) {
        IsDefault = 1;
    }
    var attributeValue = $("#AttributeValue").val();
    var Sorting = $("#Sorting").val();
    var AttributeID = $("#AttributeID").val();
    var ID = $("#ID").val();
    $.ajax({
        type: "POST",
        url: "/Product/ModifyAttributeValue",
        data: { isDefault: IsDefault, attributeValue: attributeValue, sorting: Sorting, attributeID: AttributeID, id: ID },
        dataType: "json",
        success: function (data) {
            alert("修改成功");
        }
    });
}