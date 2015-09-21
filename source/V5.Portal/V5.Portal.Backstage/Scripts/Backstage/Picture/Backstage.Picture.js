$(function () {
    $("#SelectAll").click(
            function () {
                if (this.checked) {
                    $("#SelectAll").attr("checked", "checked");
                    $("input[name='checkboxName']").each(function () {
                        var imgNameLabel = document.getElementsByName(this.id);
                        $(imgNameLabel).css("font-weight", "bold");
                        this.checked = true;
                    });
                } else {
                    $("#SelectAll").attr("checked", null);
                    $("input[name='checkboxName']").each(function () {
                        var imgNameLabel = document.getElementsByName(this.id);
                        $(imgNameLabel).css("font-weight", "normal");
                        this.checked = false;
                    });
                }
            });
});

// 图片搜索时刷新 ListView，并调用 SearchListViewData() 函数将查询条件传入到后台方法
function searchImg() {
    $("#picturelistView").data("kendoListView").dataSource.read();
    $("#picturelistView").data("kendoListView").dataSource.refresh();
}
function SearchListViewData() {
    return {
        type: $("#type").val(),
        brandID: $("#brandID").val(),
        pictureName: $("#pictureName").val(),
        startTime: $("#StartTime").val(),
        endTime: $("#EndTime").val()
    };
}

// 时间范围搜索条件触发事件
function onStartTimeChanged() {
    var endTimePicker = $("#TimeEnd").data("kendoDateTimePicker");
    var startDate = this.value();

    if (startDate) {
        startDate = new Date(startDate);
        startDate.setDate(startDate.getDate() + 1);
        endTimePicker.min(startDate);
    }
}
function onEndTimeChanged() {
    var startTimePicker = $("#TimeStart").data("kendoDateTimePicker");
    var endDate = this.value();

    if (endDate) {
        endDate = new Date(endDate);
        endDate.setDate(endDate.getDate() - 1);
        startTimePicker.max(endDate);
    }
}

// 图片 ListView 数据源变化时移除全选 CheckBox 选中状态
function dataSourceChanged() {
    $("#SelectAll").removeAttr("checked");
}

// 点击上传图片按钮，打开上传图片 Kendo Window
function uploadPictureBtnClick() {
    $("#uploadImgWindow").data("kendoWindow").open();
    $("#uploadImgWindow").data("kendoWindow").center();
    $(".k-overlay").css('display', 'block');
}

// 上传图片时，将商品类别以及商品品牌编号作为参数传递给后台方法
function onUpload(e) {
    var parentCategoryID = $("#ParentCategory").val();
    var productCategoryID = $("#ProductCategory").val();
    var parentBrandID = $("#ParentBrand").val();
    var productBrandID = $("#ProductBrand").val();

    e.data = { parentCategoryID: parentCategoryID, productCategoryID: productCategoryID, parentBrandID: parentBrandID, productBrandID: productBrandID };
}

// 上传图片时，商品类别与品牌下拉框联动，获取父级编号方法
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

// 修改图片所在类别时，商品类别与品牌下拉框联动，获取父级编号方法
function GetParentCategoryID1() {
    return {
        ParentID: $("#ParentCategory1").val()
    };
}
function GetProductCategoryID1() {
    return {
        categoryID: $("#ProductCategory1").val()
    };
}
function GetParentBrandID1() {
    return {
        parentID: $("#ParentBrand1").val()
    };
}

// 复制图片链接
function copyImgLink() {
    var imgUrl = "";
    var checkboxes = $("input[name='checkboxName']:checked");
    if (checkboxes.length < 1) {
        alert("请选择图片！");
        return;
    }

    for (var i = 0; i < checkboxes.length; i++) {
        var temp = checkboxes[i].value;
        imgUrl += temp + "\r\n";
    }

    imgUrl = imgUrl.substring(0, imgUrl.length);
    if (imgUrl != "") {
        $("#copyImgLinkTextArea").val(imgUrl);
        $("#copyImgLinkWindow").data("kendoWindow").open();
        $("#copyImgLinkWindow").data("kendoWindow").center();
        $(".k-overlay").css('display', 'block');

        var copyLink = document.getElementById("copyImgLinkTextArea");
        copyLink.focus();
        copyLink.select();
    }
}

// 复制图片代码
function copyImgCode() {
    var imgHtmlCode = "";
    var checkboxes = $("input[name='checkboxName']:checked");
    if (checkboxes.length < 1) {
        alert("请选择图片！");
        return;
    }

    for (var i = 0; i < checkboxes.length; i++) {
        var temp = checkboxes[i].value;
        imgHtmlCode += "<img src=\"" + temp + "\" alt=''>" + "\r\n";
    }

    if (imgHtmlCode != "") {
        $("#copyImgLinkTextArea").val(imgHtmlCode);
        $("#copyImgLinkWindow").data("kendoWindow").open();
        $("#copyImgLinkWindow").data("kendoWindow").center();
        $(".k-overlay").css('display', 'block');

        var copyLink = document.getElementById("copyImgLinkTextArea");
        copyLink.focus();
        copyLink.select();
    }
}

// 点击设置图片类别按钮时，打开设置图片类别的 Window
function setImgCategory() {
    var checkboxes = $("input[name='checkboxName']:checked");
    if (checkboxes.length < 1) {
        alert("请选择图片！");
        return;
    }

    $("#modifyImgCategoryWindow").data("kendoWindow").open();
    $("#modifyImgCategoryWindow").data("kendoWindow").center();
    $(".k-overlay").css('display', 'block');
}
// 点击修改按钮，触发事件
function modifyImgCategoryClick() {
    var jsonData = {};
    var checkboxes = $("input[name='checkboxName']:checked");
    var isDeleteImg = confirm("确认修改图片所在类别？");
    if (isDeleteImg) {
        for (var i = 0; i < checkboxes.length; i++) {
            jsonData["pictures[" + i + "]"] = checkboxes[i].title;
        }

        var parentCategoryID = $("#ParentCategory1").val();
        var productCategoryID = $("#ProductCategory1").val();
        var parentBrandID = $("#ParentBrand1").val();
        var productBrandID = $("#ProductBrand1").val();

        jsonData["parentCategoryID"] = parentCategoryID;
        jsonData["productCategoryID"] = productCategoryID;
        jsonData["parentBrandID"] = parentBrandID;
        jsonData["productBrandID"] = productBrandID;

        $.ajax({
            type: "POST",
            url: "picture/modifyPictureCategory",
            data: jsonData,
            datatype: "json",
            success: function (data) {
                if (data.State == -401) {
                    onSessionLost();
                } else if (data.State == -403) {
                    alert("对不起，您无此操作权限！");
                } else {
                    if (data.State == 1) {
                        $("#modifyImgCategoryWindow").data("kendoWindow").close();
                        alert("修改成功！");
                        $("#SelectAll").removeAttr("checked");
                        $("#picturelistView").data("kendoListView").dataSource.read();
                        $("#picturelistView").data("kendoListView").dataSource.refresh();
                    }
                    if (data.State == 2) {
                        alert("修改失败！");
                    }
                    return true;
                }
            },
            error: function () {
                try {
                    console.log("请求失败");
                } catch (e) {
                    console.error("" + e.name + e.message + "");
                }
            }
        });
    }
}

// 删除图片
function deleteImg() {
    var jsonData = {};
    var checkboxes = $("input[name='checkboxName']:checked");
    if (checkboxes.length < 1) {
        alert("请选择图片！");
        return;
    }

    var isDeleteImg = confirm("确认删除图片？");
    if (isDeleteImg) {
        for (var i = 0; i < checkboxes.length; i++) {
            jsonData["pictures[" + i + "]"] = checkboxes[i].title;
        }

        $.ajax({
            type: "POST",
            url: "picture/remove",
            data: jsonData,
            datatype: "json",
            success: function (data) {
                if (data.State == -401) {
                    onSessionLost();
                } else if (data.State == -403) {
                    alert("对不起，您无此操作权限！");
                } else {
                    if (data.State == 1) {
                        alert("删除成功！");
                        $("#SelectAll").removeAttr("checked");
                        $("#picturelistView").data("kendoListView").dataSource.read();
                        $("#picturelistView").data("kendoListView").dataSource.refresh();
                    }
                    if (data.State == 2) {
                        alert("删除失败！");
                    }
                    return true;
                }
            },
            error: function () {
                try {
                    console.log("请求失败");
                } catch (e) {
                    console.error("" + e.name + e.message + "");
                }
            }
        });
    }
}

// 当鼠标移入图片区域时，显示图片操作栏
function displayOperateBar(bar) {
    var operateBar = document.getElementById($(bar).attr("name"));
    operateBar.style.display = "block";
}
// 当鼠标移出图片区域时，隐藏图片操作栏
function hiddleOperateBar(bar) {
    var operateBar = document.getElementById($(bar).attr("name"));
    operateBar.style.display = "none";
}
// 单个图片左上角的 CheckBox 变化时触发事件
function itemCheckBoxChanged(checkBox) {
    var imgNameLabel = document.getElementsByName(checkBox.id);
    if (checkBox.checked == true) {
        $(imgNameLabel).css("font-weight", "bold");
    } else {
        $(imgNameLabel).css("font-weight", "normal");
    }

    var checkboxes = $("input[name='checkboxName']:checked");
    var selectAll = document.getElementById("SelectAll");
    if (checkboxes.length == $("input[name='checkboxName']").length) {
        selectAll.checked = true;
    } else {
        selectAll.checked = false;
    }
}
// 点击图片区域时触发事件
function itemImgClick(img) {
    var itemCheckBox = document.getElementById(img.name);
    var imgNameLabel = document.getElementsByName(itemCheckBox.id);
    if (itemCheckBox.checked == true) {
        itemCheckBox.checked = false;
        $(imgNameLabel).css("font-weight", "normal");
    } else {
        itemCheckBox.checked = true;
        $(imgNameLabel).css("font-weight", "bold");
    }

    var checkboxes = $("input[name='checkboxName']:checked");
    var selectAll = document.getElementById("SelectAll");
    if (checkboxes.length == $("input[name='checkboxName']").length) {
        selectAll.checked = true;
    } else {
        selectAll.checked = false;
    }
}
// 查看原图
function viewPicture(e) {
    window.open($(e).attr("name"));
}

// 商品类别品牌树选中项触发事件
function onTreeviewSelect(e) {
    var treeview = $("#treeview").data("kendoTreeView");
    $("#brandID").val(treeview.dataItem(e.node).id);
    $("#type").val(treeview.dataItem(e.node).type);
}