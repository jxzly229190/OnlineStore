
//添加订单模块相关 JS 方法
function showAddUserWindow() {
    $(".k-overlay").css('display', 'block');
    $("#addUserWindow").data("kendoWindow").open().center();
}

// 关闭添加用户的 Window

function closeAddUserWindow() {
    $(".k-overlay").css('display', 'none');
    $("#addUserWindow").data("kendoWindow").close();
}

function close() {
    $("#addUserWindow").data("kendoWindow").close();
    $(".k-overlay").css('display', 'none');
}

function buildUserRequstData() {
    var existFlag;
    var json = {};
    if (isMobile($("#Mobile").val())) {
        $.ajax({
            type: "POST",
            async: false,
            url: "/User/IsMobileExists",
            data: { mobile: $("#Mobile").val() },
            datatype: "text",
            success: function(response) {
                if (response != 0) {
                    alert("手机号码已被注册过！");
                    existFlag = true;
                } else {
                    json["Mobile"] = $("#Mobile").val();
                }
            },
            error: function() {
                alert("验证手机号码发生错误！");
            }
        });
    } else {
        alert("手机号码错误！");
        return false;
    }

    //邮箱
    if (!isEmpty($("#Email").val())) {
        if (isEmail($("#Email").val())) {
            $.ajax({
                type: "POST",
                async: false,
                url: "/User/IsEmailExists",
                data: { email: $("#Email").val() },
                datatype: "text",
                success: function (response) {
                    if (response != 0) {
                        alert("邮箱已被注册过！");
                        existFlag = true;
                    } else {
                        json["Email"] = $("#Email").val();
                    }
                },
                error: function () {
                    alert("验证邮箱地址发生错误！");
                }
            });
        } else {
            alert("邮箱错误！"); return false;
        }
    }
    
    
    if (existFlag) {
        return false;
    }
    
    if (!isEmpty($("#Name").val())) {
        json["Name"] = $("#Name").val();
    } else {
        alert("姓名不能为空");
        return false;
    }
    
    if (!isEmpty($("#Tel").val())&& !isPhone($("#Tel").val())) {
        alert("联系电话不正确！");
        return false;
    } else {
        json["Tel"] = $("#Tel").val();
    }
    
    if (!isEmpty($("#CountyID").val())) {
        json["CountyID"] = $("#CountyID").val();
    } else {
        alert("请选择地区！");
        return false;
    }

    if (!isEmpty($("#Address").val())) {
        json["Address"] = $("#Address").val();
    } else {
        alert("地址不能为空！");
        return false;
    }
    return json;
}

// 订单后台添加会员
function addUser() {
    var json = buildUserRequstData();
    if (!json) {
        return false;
    }

    $.ajax({
        type: "POST",
        url: "/Transact/AddUser",
        data:json,
        success: function(res) {
            if (res.State == 1) {
                $("#hiddenUserId").val(res.Data.UserID);
                showUserInfo($("#Name").val(), $("#Mobile").val(), $("#Tel").val(), res.Data.CountyInfo, $("#Address").val());
                closeAddUserWindow();
            } else {
                alert("添加会员失败！");
            }
        }
    });
}

//异步根据会员邮箱号或者手机搜索会员信息
function searchUserInfo() {
    $("#hiddenUserId").val();
    $("#hiddenUserReceiveAddressId").val();
    $.ajax({
        type: "POST",
        url: "/Transact/SearchUserInfo",
        data: { searchStr: $("#txtSearch").val() },
        datatype: "text",
        success: function(response) {
            if (response.State == 2) {
                alert("用户不存在！请确认您搜索的手机/邮箱是否正确。");
            } else if (response.State == 3) {
                $("#divUserReceiveAddress").css("display", "block");
                $("#btnSelectAddress").val("添加收货信息");
                $("#hiddenUserId").val(response.Data.ID);
                alert("会员收货信息不全或没有默认收货地址，请修改会员收货信息！");
                showAddressWindow();
            } else if (response.State == 1) {
                $("#btnSelectAddress").val("修改收货信息");
                    $("#hiddenUserId").val(response.Data.UserID);
                    $("#hiddenUserReceiveAddressId").val(response.Data.ID);
                    showUserInfo(response.Data.Consignee, response.Data.Mobile, response.Data.Tel, response.Data.CountyName, response.Data.Address);
            } else {
                alert("服务器出错，请联系技术人员！\r\n错误信息：" + response.Message);
            }
        }
    });
}

//展示会员的收货地址信息
function showUserInfo(userName, mobile, tel, countyInfo, address) {
    $("#divUserReceiveAddress").css("display", "block");
    $("#spanUserReceiveAddress").text("姓名：" + userName + " 手机：" + mobile + " 电话：" + tel + " 省市区：" + countyInfo + " 地址：" + address);
}

function filterCityAddress() {
    return { provinceID: $("#AddressProvinceID").val() };
}

function filterCountyAddress() {
    return { cityID: $("#AddressCityID").val() };
}

//设置配送地址

function onSelectAddress(e) {
    var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
    showUserInfo(dataItem.Consignee, dataItem.Mobile, dataItem.Tel, dataItem.CountyName, dataItem.Address);
    $("#hiddenUserReceiveAddressId").val(dataItem.ID);
    closeAddressWindow();
}

//获取会员编码信息

function getUserID(e) {
    return { currentUserID: $("#hiddenUserId").val() };
}

//获取会员编码信息

function returnReceiveAddress(e) {
    return {
        currentUserID: $("#hiddenUserId").val(),
        currentCountyID: $("#AddressCountyID").val()
    };
}

//打开会员地址窗口

function showAddressWindow() {
    $("#divSelectAddress").css("display", "block");
    $("#receiveAddressWindow").data("kendoWindow").open().center();
    $("#receiveAddressGrid").data("kendoGrid").dataSource.read();
    $("#receiveAddressGrid").data("kendoGrid").refresh();
}

//关闭会员地址窗口

function closeAddressWindow() {
    $("#divSelectAddress").css("display", "none");
    $("#receiveAddressWindow").data("kendoWindow").close();
}

function showDivAddProduct() {
    if (isEmpty($("#hiddenUserReceiveAddressId").val())) {
        alert("请选择收货地址信息！");
        return false;
    }
    
    $("#divAddProduct").css("display", "block");
    $("#OrderProductGrid").data("kendoGrid").dataSource.read();
    $("#OrderProductGrid").data("kendoGrid").refresh();
}

/*********************订单>>添加商品*********************/
function getParentBrandID() {
    return { parentBrandId: $("#BrandId").val() };
}

function onSelectAll(e) {
    alert("selectAll");
}
    
function searchProductData() {
    var filter = new Array();
    var grid = $("#OrderProductGrid").data("kendoGrid");
    grid.dataSource.filter(filter);
}

function searchOrderEditProductData() {
    var filter = new Array();
    var grid = $("#OrderEditProductGrid").data("kendoGrid");
    grid.dataSource.filter(filter);
}

function getProductSearchParams() {
    return {
        ProductCategoryID: $("#ParentCategoryID").val(),
        SubProductCategoryID: $("#CategoryID").val(),
        ProductBrandID: $("#BrandId").val(),
        SubProductBrandID: $("#SubProductBrandID").val(),
        ProductName: $("#ProductName").val(),
        Barcode: $("#Barcode").val()
    };
}

//设置购物车中商品的数量
function setCartProductQuantity() {
    var cartProductDataSource = $("#OrderCartProductGrid").data("kendoGrid").dataSource;
    $(cartProductDataSource.data()).each(function () {
        this.Quantity = parseInt($("#Quantity_" + this.ID).val());
        this.GoujiuPrice = parseInt($("#txtPrice_" + this.ID).val());
    });
}

function setSpanValues() {
    var productCount = 0, totalMoney = 0, deliveryCost = 0, discountMoney = 0, paymentMoney = 0;
    var cartProductDataSource = $("#OrderCartProductGrid").data("kendoGrid").dataSource;
    $(cartProductDataSource.data()).each(function () {
        productCount += this.Quantity;
        totalMoney += (this.Quantity * this.GoujiuPrice);
    });
    deliveryCost = totalMoney >= 100 ? 0 : 10;
    paymentMoney = totalMoney + deliveryCost - discountMoney;
    $("#spanProductCount").text(productCount);
    $("#spanTotalMoney").text(totalMoney);
    $("#spanDeliveryCost").text(deliveryCost);
    $("#spanDiscountMoney").text(discountMoney);
    $("#spanPaymentMoney").text(paymentMoney);
    $("#txtInvoiceMoney").val(totalMoney);
}

function showDivOrderInfo() {
    $("#divOrderInfo").css("display", "block");
}

//添加商品到购物车中
function addProductToCart() {
    showDivOrderInfo();
    setCartProductQuantity();
    var arrChk = $("input[name='selectWare']:checked");
    if (arrChk.length < 1) {
        alert("请选择商品进行操作！");
        return;
    }

    var productDataItems = $("#OrderProductGrid").data("kendoGrid").dataSource.data();
    $(productDataItems).each(function () {
        var productDataItem = this;
        $(arrChk).each(function() {  //比对商品是否为选中商品
            if (productDataItem.ID == this.value) {
                var cartProductDataSource = $("#OrderCartProductGrid").data("kendoGrid").dataSource;
                var cartDataItems = cartProductDataSource.data();
                for (var i = 0; i < cartDataItems.length; i++) {
                    if (cartDataItems[i].ID == productDataItem.ID) { //比对商品是否已经添加，若添加，则不再添加
                        return;
                    }
                }
                productDataItem.Quantity = 1;
                $("#OrderCartProductGrid").data("kendoGrid").dataSource.add(productDataItem);
            }
        });
    });
    calcAllMoney();
}

function getCheckedProductId(e) {
    return { checkedProductIds: $("#hiddenCheckedProductIds").val() };
}
   
//计算订单金额
function calcMoney(sender) {
    calcAllMoney();
}

function calcAllMoney() {
    var cartProductDataSource = $("#OrderCartProductGrid").data("kendoGrid").dataSource;
    $(cartProductDataSource.data()).each(function () {
        this.Quantity = parseInt($("#Quantity_" + this.ID).val());
        this.GoujiuPrice = parseFloat($("#txtPrice_" + this.ID).val());
        $("#labelMoney_" + this.ID).text(parseInt($("#Quantity_" + this.ID).val()) * parseFloat($("#txtPrice_" + this.ID).val()));
    });
    setSpanValues();
}

//删除购物车中的商品
function deleteCartProduct(e) {
    if (!confirm("您确定要删除吗？")) {
        return;
    }
    setCartProductQuantity();
    var cartProductDataSource = $("#OrderCartProductGrid").data("kendoGrid").dataSource;
    $(cartProductDataSource.data()).each(function () {
        if (parseInt(e.name) == this.ID) {
            cartProductDataSource.remove(this);
        }
    });
    calcAllMoney();
}

function onSelectProduct(e) {
    var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
}

//当索取发票按钮改变时
function switchRequireInvoice(sender) {
    if (sender.checked == true) {
        $("#txtInvoiceTitle").removeAttr("disabled");
        $("#InvoceContentType").data("kendoDropDownList").enable(true);
        $("#txtInvoiceMoney").removeAttr("disabled");
    } else {
        $("#txtInvoiceTitle").attr("disabled", "disabled");
        $("#InvoceContentType").data("kendoDropDownList").enable(false);
        $("#txtInvoiceMoney").attr("disabled", "disabled");
    }
}

function checkInvoiceCost(sender) {
    if (parseFloat(sender.value) > parseFloat($("#spanTotalMoney").text()) - parseFloat($("#spanDiscountMoney").text())) {
        alert("开票金额不能大于商品总金额!");
        $(sender).val(parseFloat($("#spanTotalMoney").text()) - parseFloat($("#spanDiscountMoney").text()));
    } else {
        return parseFloat(sender.value);
    }
}

////////////////////////

function buildOrderRequstData() {
    var json = {
        "userID": $("#hiddenUserId").val(),
        "isRequireInvoice": $("#chkSwitchRequireInvoice")[0].checked,
        "description": $("textarea[name='orderDescription']").val()
    };
    
    if (isInteger($("#hiddenUserReceiveAddressId").val())) {
        json["receiveAddressID"] = $("#hiddenUserReceiveAddressId").val();
    } else {
        alert("请选择收货信息！！");
        return false;
    }

    if (!isEmpty($("#PaymentMethod").data("kendoDropDownList").value())) {
        json["paymentMethodID"] = $("#PaymentMethod").data("kendoDropDownList").value();
    } else {
        alert("请选择支付方式！！");
        return false;
    }
    
    var dataItems = $("#OrderCartProductGrid").data("kendoGrid").dataSource.data();
    if (dataItems) {
        for (var i = 0; i < dataItems.length; i++) {
            json["products[" + i + "].ID"] = dataItems[i].ID;
            json["products[" + i + "].GoujiuPrice"] = dataItems[i].GoujiuPrice;
            json["products[" + i + "].Quantity"] = dataItems[i].Quantity;
        }
    } else {
        alert("请先添加商品！");
        return false;
    }

    //订单发票信息
    if ($("#chkSwitchRequireInvoice")[0].checked) {
        json["invoiceInfo.InvoiceTypeID"] = 1;
        if ($("#InvoceContentType").data("kendoDropDownList").value()) {
            json["invoiceInfo.InvoiceContentID"] = $("#InvoceContentType").data("kendoDropDownList").value();
        } else {
            alert("请选择消费类型！");
            return false;
        }

        if ($("#txtInvoiceTitle").val()) {
            json["invoiceInfo.InvoiceTitle"] = $("#txtInvoiceTitle").val();
        } else {
            alert("发票抬头不能为空！");
            return false;
        }

        if (isFloat($("#txtInvoiceMoney").val())) {
            json["invoiceInfo.InvoiceCost"] = $("#txtInvoiceMoney").val();
        } else {
            alert("发票金额不正确！");
            return false;
        }
    }

    return json;
}

//后台添加订单
function addOrder() {
    var data = buildOrderRequstData();
    if (!data) {
        return false;
    }

    if (!confirm("您确定要添加此订单吗？")) {
        return false;
    }

    $.ajax({
        type: "POST",
        url: "Transact/AddOrder",
        data: data,
        datatype: "text",
        success: function (response) {
            if (response.State == 1) {
                alert("订单添加成功！");
                ShowPartialView($("a[name='OrderAdd']"));
            } else {
                alert("执行失败，服务器错误!\r\n错误信息:" + data.Message);
            }
        }
    });
}


///////////////////////////////////////
function clickTest(e) {
    alert(e);
}