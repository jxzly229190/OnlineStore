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

//现金券的搜索数据
function SearchCashData() {
    return {
        cashName: $("#SearchCashName").val(),
        cashStatus: $("#SearchCashStatus").val()
    };
}

//搜索现金券
function SearchCash() {
    var filter = new Array();
    var grid = $("#CouponCashGrid").data("kendoGrid");
    grid.dataSource.filter(filter);
}
//满减券的搜索数据
function SearchDecreaseData() {
    return {
        decreaseName: $("#SearchDecreaseName").val(),
        decreaseStatus: $("#SearchDecreaseStatus").val()
    };
}
//搜索满减券
function SearchDecrease() {
    var filter = new Array();
    var grid = $("#CouponDecreaseGrid").data("kendoGrid");
    grid.dataSource.filter(filter);
}
//查看现金券详情
function ShowCounponCashDetails(e) {
    e.preventDefault();
    var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
    var couponCashID = dataItem.ID;
    if (couponCashID > 0) {
        var href = "/promote/CouponCashDetail?couponCashID=" + couponCashID;

        $.ajax({
            type: "GET",
            url: href,
            data: null,
            datatype: "html",
            success: function (data) {
                $('#detailDiv').html(data);
                $('#detailDiv').css("display", "block");
                $('#defaultDiv').css("display", "none");
            },
            error: function () {
                alert("处理失败!");
            }
        });
    }
}
//查看现金券详情
function ShowCounponDecreaseDetails(e) {
    e.preventDefault();
    var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
    var couponDecreaseID = dataItem.ID;
    if (couponDecreaseID > 0) {
        var href = "/promote/CouponDecreaseDetail?couponDecreaseID=" + couponDecreaseID;

        $.ajax({
            type: "GET",
            url: href,
            data: null,
            datatype: "html",
            success: function (data) {
                $('#detailDiv').html(data);
                $('#detailDiv').css("display", "block");
                $('#defaultDiv').css("display", "none");
            },
            error: function () {
                alert("处理失败!");
            }
        });
    }
}

//返回电子券列表
function backList() {
    $('#detailDiv').css("display", "none");
    $('#defaultDiv').css("display", "block");
}

//添加现金券
function AddCouponCash() {
    var tabStrip = $("#tabStrip").data("kendoTabStrip");
    var getItem = function (target) {
        return tabStrip.tabGroup.children("li").eq(target);
    };
    var tab = tabStrip.select();
    tabStrip.enable(tab, tab.hasClass("k-state-disabled"));
    var otherTab = tab.next();
    otherTab = otherTab.length ? otherTab : tab.prev();
    tabStrip.enable(otherTab, otherTab.hasClass("k-state-disabled"));
    otherTab = otherTab.next();
    otherTab = otherTab.length ? otherTab : tab.prev();
    tabStrip.enable(otherTab, otherTab.hasClass("k-state-disabled"));
    tabStrip.append({
        text: "添加现金券",
        content: "<div id='actionPanel'></div>"
    });

    tabStrip.select(getItem(3));

    $("#actionPanel").html($("#CounponScopeDiv").html());
    $("#couponContent").html($("#CounponCashCountent").html());
    $('#checkboxDiv').html($("#check_all").html());
    CouponCashbind();

    //$(".k-datetimepicker").css("width", "160px");
}
// 添加满减券
function AddCouponDecreaseDiv() {
    var tabStrip = $("#tabStrip").data("kendoTabStrip");
    var getItem = function (target) {
        return tabStrip.tabGroup.children("li").eq(target);
    };
    var tab = tabStrip.select();
    tabStrip.enable(tab, tab.hasClass("k-state-disabled"));
    var otherTab = tab.next();
    otherTab = otherTab.length ? otherTab : tab.prev();
    tabStrip.enable(otherTab, otherTab.hasClass("k-state-disabled"));
    otherTab = otherTab.next();
    otherTab = otherTab.length ? otherTab : tab.prev();
    tabStrip.enable(otherTab, otherTab.hasClass("k-state-disabled"));
    tabStrip.append({
        text: "添加满减券",
        content: "<div id='actionPanel'></div>"
    });

    tabStrip.select(getItem(3));

    $("#actionPanel").html($("#CounponScopeDiv").html());
    $("#couponContent").html($("#CouponDecreaseContent").html());
    $('#checkboxDiv').html($("#check_all").html());
    CouponCashbind();
}

//添加电子券时选择使用范围所绑定事件
function CouponCashbind() {
    $("#rb_all").bind("click", function () {
        $("#chooseScope").html("");
        $('#checkboxDiv').html($("#check_all").html());
    });
    $("#rb_type").bind("click", function () {
        $("#chooseScope").html("");
        $('#checkboxDiv').html($("#check_type").html());
    });
    $("#rb_category").bind("click", function () {
        $("#chooseScope").html($("#drop_category").html());
        var href = "/promote/QuerySelectCategoryListItems";
        $.ajax({
            type: "POST",
            url: href,
            data: { parentID: 17 },
            datatype: "html",
            success: function (data) {
                $('#checkboxDiv').html(data);
            },
            error: function () {
                alert("处理失败!");
            }
        });
    });
    $("#rb_brand").bind("click", function () {
        $('#checkboxDiv').html("");
        $("#chooseScope").html($("#drop_category").html() + $("#drop_brand").html());
    });
    $("#rb_product").bind("click", function () {
        $("#chooseScope").html($("#grid_product").html());

        //全选、全不选
        $(function () {
            $("#SelectAll").click(
                    function () {
                        if (this.checked) {
                            $("#SelectAll").attr("checked", "checked");
                            $("input[name='selectproduct']").each(function () {
                                this.checked = true;
                            });
                        } else {
                            $("#SelectAll").attr("checked", null);
                            $("input[name='selectproduct']").each(function () {
                                this.checked = false;
                            });
                        }
                    });
        });

    });
}

//添加优惠券时所选商品父类别改变事件
function ParentCategoryChange() {
    var href = "/promote/QuerySelectCategoryListItems";
    $.ajax({
        type: "POST",
        url: href,
        data: { parentID: $("#ParentCategory").val() },
        datatype: "html",
        success: function (data) {
            $('#checkboxDiv').html(data);
        },
        error: function () {
            alert("处理失败!");
        }
    });
}
//添加优惠券时所选商品子类别改变事件
function ProductCategoryChange() {
    var href = "/promote/QuerySelectCategoryListItemsByParentID";
    $.ajax({
        type: "POST",
        url: href,
        data: { categoryID: $("#ProductCategory").val() },
        datatype: "html",
        success: function (data) {
            $('#checkboxDiv').html(data);
        },
        error: function () {
            alert("处理失败!");
        }
    });
}

//商品搜索查询
function SearchProduct() {
    var filter = new Array();
    var grid = $("#ProductGrid").data("kendoGrid");
    grid.dataSource.filter(filter);
}
//商品查询所需要的数据
function SearchProductData() {
    return {
        ProductName: $("#ProductName").val(),
        Barcode: $("#ProductBarcode").val(),
        CategoryID: null,
        MinPrice: null,
        MaxPrice: null,
        StartTime: null,
        EndTime: null
    };
}

//赠券时指定会员范围
function alluser() {
    $("#chooseuser").html($("#user_all").html());
}
function userlevel() {
    $("#chooseuser").html($("#user_level").html());
}
function usersingle() {
    $("#chooseuser").html($("#user_single").html());
}
//会员搜索查询
function SearchUser() {
    var filter = new Array();
    var grid = $("#UserGrid").data("kendoGrid");
    grid.dataSource.filter(filter);
}
//会员查询所需要的数据
function SearchUserData() {
    return {
        userLevelID: null,
        userName: $("#UserName").val(),
        isHasOrder: null,
        status: null,
        email: $("#Email").val(),
        mobile: $("#Mobile").val(),
        startTime: null,
        endTime: null
    };
}
//会员查询结果全选、全不选
function SelectAllUser(e) {
    if (e.checked) {
        $("#SelectAllUser").attr("checked", "checked");
        $("input[name='selectuser']").each(function () {
            this.checked = true;
        });
    } else {
        $("#SelectAllUser").attr("checked", null);
        $("input[name='selectuser']").each(function () {
            this.checked = false;
        });
    }
}
//选择赠送现金券
function ChooseCouponCash() {
    $("#counponDecreaseList").css("display", "none");
    $("#counponCashList").css("display", "block");
}
//选择赠送满减券
function ChooseCouponDecrease() {
    $("#counponDecreaseList").css("display", "block");
    $("#counponCashList").css("display", "none");
}

//设置添加现金券
function SetCouponCash() {
    var objtype = "";
    var objdata = "";

    var couponName = $("#counponCashName").val();
    var quantity = $("#Quantity").val();
    var faceValue = $("#faceValue").val();
    var startTime = $("#startTime").val();
    var endTime = $("#endTime").val();
    var remarks = $("#cashRemarks").val();
    if (couponName == "") {
        alert("优惠券名称不能为空！");
        $("#counponCashName").focus();
        return;
    } if (couponName.length > 20) {
          alert("优惠券名称过长！");
          $("#counponCashName").focus();
        return;
    } if (quantity == "") {
          alert("初始数量不能为空！");
          $("#Quantity").focus();
        return;
    } if (faceValue == "") {
          alert("优惠券面额不能为空");
          $("#faceValue").focus();
        return;
    } if (startTime == "") {
          alert("生效时间不能为空！");
          $("#startTime").focus();
        return;
    } if (endTime == "") {
          alert("作废时间不能为空！");
          $("#endTime").focus();
        return;
    }
    switch (true) {
        case $("#rb_all")[0].checked:
            objtype = $("#rb_all").val();
            $("input[name='ck_all']").each(function () {
                var $item = $(this);
                if ($item[0].checked) {
                    objdata += $item.val() + ",";
                }
            });
            if (objdata.replace(/,/g, "").replace(/0/g, "") == "") {
                alert("请选择全场！");
                return;
            }
            break; //全场
        case $("#rb_type")[0].checked:
            objtype = $("#rb_type").val();
            $("input[name=ck_type]").each(function () {
                var $item = $(this);
                if ($item[0].checked) {
                    objdata += $item.val() + ",";
                }
            });
            if (objdata.replace(/,/g, "").replace(/0/g, "") == "") {
                alert("请选择商品种类！");
                return;
            }
            break; //商品种类 
        case $("#rb_category")[0].checked:
            objtype = $("#rb_category").val();
            $("input[name=ck_category]").each(function () {
                var $item = $(this);
                if ($item[0].checked) {
                    objdata += $item.val() + ",";
                }
            });
            if (objdata.replace(/,/g, "").replace(/0/g, "") == "") {
                alert("请选择商品类别！");
                return;
            }
            break; //商品类别
        case $("#rb_brand")[0].checked:
            objtype = $("#rb_brand").val();
            $("input[name=ck_Brand]").each(function () {
                var $item = $(this);
                if ($item[0].checked) {
                    objdata += $item.val() + ",";
                }
            });
            if (objdata.replace(/,/g, "").replace(/0/g, "") == "") {
                alert("请选择商品品牌！");
                return;
            }
            break; //商品品牌  
        case $("#rb_product")[0].checked:
            objtype = $("#rb_product").val();
            $("input[name=selectproduct]").each(function () {
                var $item = $(this);
                if ($item[0].checked) {
                    objdata += $item.val() + ",";
                }
            });
            if (objdata.replace(/,/g, "").replace(/0/g, "") == "") {
                alert("请选择商品！");
                return;
            }
            break; //商品 
    }

    $.post("/promote/AddCouponCash", {
        objtype: objtype,
        objdata: objdata,
        couponName: couponName,
        quantity: quantity,
        faceValue: faceValue,
        startTime: startTime,
        endTime: endTime,
        remarks: remarks
    }, function (data) {
        alert(data);
    });
}
//取消或结束 电子券编辑
function CloseAdd() {
    var tabStrip = $("#tabStrip").data("kendoTabStrip");
    var tab = tabStrip.select();
    var otherTab = tab.prev();
    tabStrip.enable(otherTab, otherTab.hasClass("k-state-disabled"));
    otherTab = otherTab.prev();
    tabStrip.enable(otherTab, otherTab.hasClass("k-state-disabled"));
    otherTab = otherTab.prev();
    tabStrip.enable(otherTab, otherTab.hasClass("k-state-disabled"));
    tabStrip.remove(tab);
    tabStrip.select(otherTab);
    $("#CouponCashGrid").data("kendoGrid").dataSource.read();
    $("#CouponCashGrid").data("kendoGrid").refresh();
    $("#CouponDecreaseGrid").data("kendoGrid").dataSource.read();
    $("#CouponDecreaseGrid").data("kendoGrid").refresh();
}

//设置添加满减券
function SetCouponDecrease() {
    var objtype = "";
    var objdata = "";

    var couponName = $("#decreaseName").val();
    var quantity = $("#Quantity").val();
    var faceValue = $("#faceValue").val();
    var meetMoney = $("#meetMoney").val();
    var startTime = $("#startTime").val();
    var endTime = $("#endTime").val();
    var remarks = $("#dacreaseRemarks").val();
    if (couponName == "") {
        alert("优惠券名称不能为空！");
        $("#decreaseName").focus();
        return;
    } if (couponName.length > 32) {
          alert("优惠券名称过长！");
          $("#decreaseName").focus();
        return;
    } if (quantity == "") {
          alert("初始数量不能为空！");
          $("#Quantity").focus();
        return;
    } if (faceValue == "") {
          alert("优惠券面额不能为空");
          $("#faceValue").focus();
        return;
    } if (meetMoney == "") {
          alert("满足的消费金额不能为空");
          $("#meetMoney").focus();
        return;
    }
    if (startTime == "") {
        alert("生效时间不能为空！");
        $("#startTime").focus();
        return;
    } if (endTime == "") {
          alert("作废时间不能为空！");
          $("#endTime").focus();
        return;
    }
    switch (true) {
        case $("#rb_all")[0].checked:
            objtype = $("#rb_all").val();
            $("input[name='ck_all']").each(function () {
                var $item = $(this);
                if ($item[0].checked) {
                    objdata += $item.val() + ",";
                }
            });
            if (objdata.replace(/,/g, "").replace(/0/g, "") == "") {
                alert("请选择全场！");
                return;
            }
            break; //全场
        case $("#rb_type")[0].checked:
            objtype = $("#rb_type").val();
            $("input[name=ck_type]").each(function () {
                var $item = $(this);
                if ($item[0].checked) {
                    objdata += $item.val() + ",";
                }
            });
            if (objdata.replace(/,/g, "").replace(/0/g, "") == "") {
                alert("请选择酒种！");
                return;
            }
            break; //商品种类 
        case $("#rb_category")[0].checked:
            objtype = $("#rb_category").val();
            $("input[name=ck_category]").each(function () {
                var $item = $(this);
                if ($item[0].checked) {
                    objdata += $item.val() + ",";
                }
            });
            if (objdata.replace(/,/g, "").replace(/0/g, "") == "") {
                alert("请选择类别！");
                return;
            }
            break; //商品类别
        case $("#rb_brand")[0].checked:
            objtype = $("#rb_brand").val();
            $("input[name=ck_Brand]").each(function () {
                var $item = $(this);
                if ($item[0].checked) {
                    objdata += $item.val() + ",";
                }
            });
            if (objdata.replace(/,/g, "").replace(/0/g, "") == "") {
                alert("请选择品牌！");
                return;
            }
            break; //商品品牌  
        case $("#rb_product")[0].checked:
            objtype = $("#rb_product").val();
            $("input[name=selectproduct]").each(function () {
                var $item = $(this);
                if ($item[0].checked) {
                    objdata += $item.val() + ",";
                }
            });
            if (objdata.replace(/,/g, "").replace(/0/g, "") == "") {
                alert("请选择商品！");
                return;
            }
            break; //商品 
    }

    $.post("/promote/AddCouponDecrease", {
        objtype: objtype,
        objdata: objdata,
        couponName: couponName,
        quantity: quantity,
        faceValue: faceValue,
        meetMoney: meetMoney,
        startTime: startTime,
        endTime: endTime,
        remarks: remarks
    }, function (data) {
        alert(data);
    });
}
//赠券
function GiveCoupon() {
    var objtype = "";
    var objdata = "";
    var couponType = "";
    var couponID = "";

    var couponCount = $("#sendCount").val();
    var cause = $("#txtcause").val();

    switch (true) {
        case $("#rb_alluser")[0].checked:
            objtype = $("#rb_alluser").val();
            $("input[name='ck_alluser']").each(function () {
                var $item = $(this);
                if ($item[0].checked) {
                    objdata += $item.val() + ",";
                }
            });
            if (objdata.replace(/,/g, "").replace(/0/g, "") == "") {
                alert("请选择所有会员！");
                return;
            }
            break; //所有会员
        case $("#rb_userlevel")[0].checked:
            objtype = $("#rb_userlevel").val();
            $("input[name='ck_level']").each(function () {
                var $item = $(this);
                if ($item[0].checked) {
                    objdata += $item.val() + ",";
                }
            });
            if (objdata.replace(/,/g, "").replace(/0/g, "") == "") {
                alert("请选择会员等级！");
                return;
            }
            break; //会员等级
        case $("#rb_usersingle")[0].checked:
            objtype = $("#rb_usersingle").val();
            $("input[name='selectuser']").each(function () {
                var $item = $(this);
                if ($item[0].checked) {
                    objdata += $item.val() + ",";
                }
            });
            if (objdata.replace(/,/g, "").replace(/0/g, "") == "") {
                alert("请选择会员！");
                return;
            }
            break; //所有会员
    }
    $("input[name='rb_CouponType']").each(function () {
        var $item = $(this);
        if ($item[0].checked) {
            couponType = $item.val();
        }
    });
    if (couponType == "1") {
        couponID = $("#CounponDecrease").val();
    } else {
        couponID = $("#CounponCash").val();
    }

    if (Number(couponID) <= 0) {
        alert("请选择优惠券！");
        return;
    } if (Number(couponCount) <= 0) {
        alert("请输入赠券数量", function () { $("#sendCount").focus(); });
        return;
    } if (cause == "") {
        alert("赠券原因不能为空！", function () { $("#txtcause").focus(); });
        return;
    }

    $.post("/promote/GiveCoupon", {
        objType: objtype,
        objData: objdata,
        couponTpye: couponType,
        couponID: couponID,
        couponCount: couponCount,
        cause: cause
    }, function (data) {
        alert(data);
        if (data == "添加成功！") {
            $("#CouponCashGrid").data("kendoGrid").dataSource.read();
            $("#CouponCashGrid").data("kendoGrid").refresh();
            $("#CouponDecreaseGrid").data("kendoGrid").dataSource.read();
            $("#CouponDecreaseGrid").data("kendoGrid").refresh();
        }
    });

}
//追加现金券的数量
function IncreaseCouponCash() {
    var couponCashID = $("#ID").val();
    var addNum = Number($("#txtAddNum").val());
    var initialNum = Number($("#InitialNum").html());
    if (addNum > 0) {
        var initialNumber = addNum + initialNum;
        $.post("/promote/addCouponCashInitialNumber", { "couponCashID": couponCashID, "initialNumber": initialNumber },
                function (data) {
                    alert(data);
                    $("#InitialNum").html(initialNumber);
                    var txtNum = document.getElementById("txtAddNum");
                    txtNum.value = "";
                    txtNum.focus();
                    $("#CouponCashGrid").data("kendoGrid").dataSource.read();
                    $("#CouponCashGrid").data("kendoGrid").refresh();
                });
    } else {
        alert("追加的数量必须大于0");
    }
}
//追加满减券的数量
function IncreaseCouponDecrease() {
    var couponDecreaseID = $("#ID").val();
    var addNum = Number($("#txtAddNum").val());
    var initialNum = Number($("#InitialNum").html());
    if (addNum > 0) {
        var initialNumber = addNum + initialNum;
        $.post("/promote/addCouponDecreaseInitialNumber", { "couponDecreaseID": couponDecreaseID, "initialNumber": initialNumber },
                function (data) {
                    alert(data);
                    $("#InitialNum").html(initialNumber);
                    var txtNum = document.getElementById("txtAddNum");
                    txtNum.value = "";
                    txtNum.focus();
                    $("#CouponDecreaseGrid").data("kendoGrid").dataSource.read();
                    $("#CouponDecreaseGrid").data("kendoGrid").refresh();
                });
    } else {
        alert("追加的数量必须大于0");
    }
}

