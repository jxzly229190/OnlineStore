// Kendo Grid 编辑事件公用方法
function onEdit(e) {
    if (e.model.isNew()) {
        $(".k-window-title").html("新建");
        e.container.find(".k-update").parent().text("添加");
    } else {
        $(".k-window-title").html("编辑");
        e.container.find(".k-update").parent().text("修改");
    }

    e.container.find(".k-cancel").parent().text("取消");
}

// kendo 错误处理方法
function error_handler(e) {
    if (e) {
        alert(e.xhr.statusText + ":" + e.xhr.responseText);
    }
}

// 省、市、区县 Kendo DropDownList 联动筛选方法
function filterCity() {
    return {
        provinceID: $("#ProvinceID").val()
    };
}
function filterCounty() {
    return {
        cityID: $("#CityID").val()
    };
}

function closeCustomWindow() {
    $(".k-overlay").css('display', 'none');
}