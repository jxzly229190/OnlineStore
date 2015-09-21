//商品搜索查询
function SearchProduct() {
    var filter = new Array();
    var grid = $("#ProductGrid").data("kendoGrid");
    grid.dataSource.filter(filter);
}
//商品查询所需要的数据
function SearchProductData() {
    return {
        ProductName: trim($("#ProductName").val() || ""),
        Barcode: trim($("#ProductBarcode").val() || ""),
        CategoryID: null,
        MinPrice: null,
        MaxPrice: null,
        StartTime: null,
        EndTime: null
    };
}
// 限时打折查询所需要的数据
function LimitedDiscountSearchData() {
    return {
        promoteName: $("#promoteName").val(),
        productName: $("#pro_Name").val(),
        startStartTime: $("#promoteStartStartTime").val(),
        startEndTime: $("#promoteStartEndTime").val(),
        endStartTime: $("#promoteEndStartTime").val(),
        endEndTime: $("#promoteEndEndTime").val(),
        searchStatus: $("#SearchStatus").val()
    };
}

//删除左右两端的空格
function trim(str) {
    str = str || "";
    return str.replace(/(^\s*)|(\s*$)/g, "");
}


// 限时抢购 搜索
function searchLimitedDiscount() {
    var filter = new Array();
    var grid = $("#promoteLimitedDiscountGrid").data("kendoGrid");
    grid.dataSource.filter(filter);
}

var limitedDiscount = {    
    //功能描述：初始化
    Init: function () {
        
    },

    //功能描述：设置参数
    SetOption: function (option) {
    },

    //功能描述：加载事件
    LoadEvent: function () {

    },
    
    // 功能描述：关闭满额优惠编辑页
    Close: function () {
        $.get("/promote/LimitedDiscount", null, function (data) {
            if (data.State == -401) {
                onSessionLost();
            } else if (data.State == -403) {
                alert("对不起，您无此操作权限！");
            } else {
                $('#defaultDiv').html(data);
            }
        });
    },
    
    //功能描述：删除
    Remove: function (e) {
        var mess = confirm("确实要删除吗?");
        if (mess != "0") {
            $.post("/promote/RemoveLimited", { limitedID: e.name }, function (data) {
                alert(data.Message);
                if (data.State == 1) {
                    limitedDiscount.Close();
                }
            });
        }
    },

     // 功能描述：停止
    Stop: function (e) {
        if ($(e).val() == "停止") {
            var mess = confirm("确实要停止吗?");
            if (mess != "0") {
                $.post("/promote/ChangesLimitedStatus", { limitedID: e.name, status: 3 }, function (data) {
                    alert(data.Message);
                    if (data.State == 1) {
                        limitedDiscount.Close();
                    }
                });
            }
        }
    },

    // 功能描述：暂停、恢复
    Suspend: function (e) {
        if ($(e).val() == "暂停") {
            var mess = confirm("确实要暂停吗?");
            if (mess != "0") {
                $.post("/promote/ChangesLimitedStatus", { limitedID: e.name, status: 2 }, function (data) {
                    alert(data.Message);
                    if (data.State == 1) {
                        limitedDiscount.Close();
                    }
                });
            } else {
                return;
            }
        } else {
            var mess = confirm("确实要恢复吗?");
            if (mess != "0") {
                $.post("/promote/ChangesLimitedStatus", { limitedID: e.name, status: 1 }, function (data) {
                    alert(data.Message);
                    if (data.State == 1) {
                        limitedDiscount.Close();
                    }
                });
            }
        }
    }
};

    //显示添加限时打折编辑内容
    function AddLimiteDiscountTab() {
        var tabStrip = $("#tabStrip").data("kendoTabStrip");
        var getItem = function (target) {
            return tabStrip.tabGroup.children("li").eq(target);
        };
        var tab = tabStrip.select();
        tabStrip.enable(tab, tab.hasClass("k-state-disabled"));
        tabStrip.append({
            text: "添加限时打折",
            content: "<div id='actionPanel'></div>"
        });
        tabStrip.select(getItem(1));
        $("#actionPanel").html($("#editorLimitedDiscount").html());
    }

    //关闭或取消编辑限时打折
    function CancelAddLimited() {
        var tabStrip = $("#tabStrip").data("kendoTabStrip");
        var tab = tabStrip.select();
        var otherTab = tab.prev();
        tabStrip.enable(otherTab, otherTab.hasClass("k-state-disabled"));
        tabStrip.remove(tab);
        tabStrip.select(otherTab);
        $("#promoteLimitedDiscountGrid").data("kendoGrid").dataSource.read();
        $("#promoteLimitedDiscountGrid").data("kendoGrid").refresh();
    }

    //设置活动时全选商品
    function SelectAllProduct() {
        if ($("#SelectAllProduct")[0].checked) {
            $("#SelectAllProduct").attr("checked", "checked");
            $("input[name='selectproduct']").each(function () {
                this.checked = true;
            });
        } else {
            $("#SelectAllProduct").attr("checked", null);
            $("input[name='selectproduct']").each(function () {
                this.checked = false;
            });
        }
    }

    //设置限时打折时选择商品
    function finishChoice() {
        var tableStr = ""; // 输出的table html字符串
        var choiceArry = new Array(); //已存在的商品编号数组
        var addchoiceArry = new Array();//新添加的商品编号数组

        //检查是否存在已选择的商品
        if ($("#showChoiceResult").find("#example").length > 0) {
            $("tr[name=choiceTr]").each(function () {
                var $itemTrID = $(this)[0].id.substr(6);
                choiceArry.push($itemTrID); //把已存在的TR组成一个数组
            });
        }
        var dataItems = $("#ProductGrid").data("kendoGrid").dataSource.data();
        $("input[name=selectproduct]").each(function () {
            var $item = $(this);
            if ($item[0].checked) {
                $item[0].checked = false;
                $("#SelectAllProduct")[0].checked = false;
                if ($.inArray($item.val(), choiceArry) < 0 && $.inArray($item.val(), addchoiceArry) < 0) {
                    //choiceArry.push($item.val());
                    addchoiceArry.push($item.val());
                    $(dataItems).each(function () {
                        if (this.ID == $item.val()) {
                            tableStr += "<tr id=trPro_" + $item.val() + " name='choiceTr'>";
                            tableStr += "<td>" + this.Name + "</td>";
                            tableStr += "<td> <span id='gjwPrice" + $item.val() + "'>" + this.GoujiuPrice + "</span></td>";
                            tableStr += "<td><input id='discount" + $item.val() + "' type='number" + $item.val() + "' name='discount' min='1' max='10' style='width:80px' onblur='UpdateLimitedPrice(" + $item.val() + ")' class='k-textbox' /></td>";
                            tableStr += "<td><input id='discounted" + $item.val() + "' type='number" + $item.val() + "' name='discounted' min='0' style='width:80px' onblur='UpdateLimited(" + $item.val() + ")' class='k-textbox'/></td>";
                            tableStr += "<td><input id='buyCount" + $item.val() + "' type='number" + $item.val() + "' name='buyCount'  min='0' style='width:80px' class='k-textbox' /></td>";
                            tableStr += "<td><input id='totalNumber" + $item.val() + "' type='number" + $item.val() + "' name='totalNumber' min='1' style='width:80px' class='k-textbox' /></td>";
                            tableStr += "<td><input type='checkbox' name='isOnlinePay' /></td>";
                            tableStr += "<td><input type='checkbox' checked='checked' name='useCoupon' /></td>";
                            tableStr += "<td><a href='javascript:removeTR(" + $item.val() + ")'>删除</a></td>";
                            tableStr += "</tr>";
                        }
                    });
                } //小于0表示不存在相同的商品
            }
        });
        if (choiceArry.length <= 0 && addchoiceArry.length<=0) {
            alert("请选择商品！");
            return;
        }
        if ($("#showChoiceResult").find("#example").length > 0) {
            tableStr += $("#tableResult").html();
        } else {
            $("#showChoiceResult").html($("#choiceResult").html());
            $("#choiceResultGrid").kendoGrid();
        }
        $("#tableResult").html(tableStr);
        $('#submitButton').css("display", "block");
    }
    //删除已选择的商品
    function removeTR(n) {
        $("#trPro_" + n).remove();
        if ($("tr[name=choiceTr]").length<=0) {
            $("#example").remove();
            $('#submitButton').css("display", "none");
        }
    }
   //计算折后价
    function UpdateLimitedPrice(n) {
        $("#discounted" + n).val("0");
        
        var discount = parseInt($("#discount" + n).val());
        var gjwPrice = parseFloat($("#gjwPrice" + n).html());
        if (discount > 0 && gjwPrice > 0 && discount < 10) {
            $("#discounted" + n).val(discount * gjwPrice * 0.1);
        }
    }

    function UpdateLimited(n) {
        $("#discount" + n).val("0");
        var discounted = parseFloat($("#discounted" + n).val());
        var gjwPrice = parseFloat($("#gjwPrice" + n).html());
        if (discounted > 0 && gjwPrice > 0 ) {
            $("#discount" + n).val(discounted / gjwPrice * 10);
        }
    }
    //计算折后价（修改模版）
    function CountLimitedPrice(sender) {
        $("#DiscountPrice").val("0.00");
        var discount = parseInt(sender.value);
        var gjwPrice = parseFloat($("#GoujiuPrice").val());
        if (discount > 0 && gjwPrice > 0 && discount < 10) {
            var temp = discount * gjwPrice * 0.1;
            $("#DiscountPrice").val(temp);
            $("#DiscountPrice").data("kendoNumericTextBox").focus();
        }
        else{
            $("#DiscountPrice").val(gjwPrice);
            $("#DiscountPrice").data("kendoNumericTextBox").focus();
        }
    }
    function CountLimitedDiscount(sender) {
        $("#Discount").val("0");
        var discounted = parseFloat(sender.value);
        var gjwPrice = parseFloat($("#GoujiuPrice").val());
        if (discounted > 0 && gjwPrice > 0) {
            var temp = discounted / gjwPrice * 10;
            $("#Discount").val(temp);
            $("#Discount").data("kendoNumericTextBox").focus();
        }
        else {
            $("#Discount").val(10);
            $("#Discount").data("kendoNumericTextBox").focus();
        }
    }
    //修改限时打折
    function onEditLimited(e) {
        if (e.model.isNew()) {
            $(".k-window-title").html("新建");
            e.container.find(".k-update").parent().text("添加");
        } else {
            $(".k-window-title").html("编辑");
            e.container.find(".k-update").parent().text("修改");
        }

        e.container.find(".k-cancel").parent().text("取消");
    }
    //添加限时打折促销
    function AddLimitedDiscount() {
        var promoteName = $("#limitedDiscountName").val();
        var promoteStartTime = $("#LimitedStartTime").val();
        var promoteEndTime = $("#LimitedEndTime").val();
        var productArry = "";
        var discountArry = "";
        var discountPriceArry = "";
        var limitedBuyNumArry = "";
        var totalNumArry = "";
        var isOnlinePayArry = "";
        var newUserArry = "";
        var mobileverifyArry = "";
        var useCouponArry = "";

        if (promoteName == "") {
            alert("促销活动名称不能为空！", function () { $("#limitedDiscountName").focus(); });
            return;
        } if (promoteName.length > 20) {
            alert("促销活动名称长度超过范围！", function () { $("#limitedDiscountName").focus(); });
            return;
        } if (promoteStartTime == "") {
            alert("促销活动时间不能为空！", function () { $("#LimitedStartTime").focus(); });
            return;
        } if (promoteEndTime == "") {
            alert("促销活动时间不能为空！", function () { $("#LimitedEndTime").focus(); });
            return;
        }

        $("tr[name='choiceTr']").each(function () {
            var $item = $(this);
            var txtproductID = $item[0].id.substr(6);
            if (txtproductID != "") {
                productArry += txtproductID + ",";
            }
        }); //商品编号集合
        $("input[name='discount']").each(function () {
            var $item = $(this);
            var txtdiscount = $item.val();
            if (txtdiscount != "") {
                discountArry += txtdiscount + ",";
            } else {
                alert("折扣不能为空！");
                return false;
            }
        }); //商品打折数集合
        $("input[name='discounted']").each(function () {
            var $item = $(this);
            var txtdiscounted = $item.val();
            if (txtdiscounted != "") {
                discountPriceArry += txtdiscounted + ",";
            } else {
                alert("折扣不能为空！");
                return false;
            }
        }); //商品折后价集合
        $("input[name='buyCount']").each(function () {
            var $item = $(this);
            var txtbuyCount = $item.val();
            if (txtbuyCount != "") {
                limitedBuyNumArry += txtbuyCount + ",";
            } else {
                alert("每人限购数不能为空！");
                return false;
            }
        }); //商品每人做多购买数集合
        $("input[name='totalNumber']").each(function () {
            var $item = $(this);
            var txttotalNumber = $item.val();
            if (txttotalNumber != "") {
                totalNumArry += txttotalNumber + ",";
            } else {
                alert("商品活动数量不能为空！");
                return false;
            }
        }); //参与促销的商品总数量集合
        $("input[name='isOnlinePay']").each(function () {
            var $item = $(this);
            if ($item[0].checked) {
                isOnlinePayArry += "1,";
            } else {
                isOnlinePayArry += "0,";
            }
        }); // 是否仅支持在线支付
        $("input[name='newUser']").each(function () {
            var $item = $(this);
            if ($item[0].checked) {
                newUserArry += "1,";
            } else {
                newUserArry += "0,";
            }
        }); // 是否新会员
        $("input[name='mobileverify']").each(function () {
            var $item = $(this);
            if ($item[0].checked) {
                mobileverifyArry += "1,";
            } else {
                mobileverifyArry += "0,";
            }
        }); // 是否手机验证
        $("input[name='useCoupon']").each(function () {
            var $item = $(this);
            if ($item[0].checked) {
                useCouponArry += "1,";
            } else {
                useCouponArry += "0,";
            }
        }); // 能否使用优惠券

        $.post("promote/AddLimitedDiscount", {
            promoteName: promoteName,
            promoteStartTime: promoteStartTime,
            promoteEndTime: promoteEndTime,
            productArry: productArry,
            discountArry: discountArry,
            discountPriceArry: discountPriceArry,
            limitedBuyNumArry: limitedBuyNumArry,
            totalNumArry: totalNumArry,
            isOnlinePayArry: isOnlinePayArry,
            newUserArry: newUserArry,
            mobileverifyArry: mobileverifyArry,
            useCouponArry: useCouponArry
        }, function (data) {
            alert(data.Message);
            if (data.State == 1) {
                CancelAddLimited();
            }
        });
    }