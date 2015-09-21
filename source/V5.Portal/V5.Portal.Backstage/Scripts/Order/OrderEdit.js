/**********订单管理>>确认订单相关 JS 方法****************/
var orderID = 0;
//异步加载订单编辑页面（订单确认）

function showOrderEdit(e) {
    e.preventDefault();
    var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
    orderID = dataItem.ID;

    $.ajax({
        type: "GET",
        url: "Transact/OrderEdit",
        data: { orderID: dataItem.ID },
        datatype: "html",
        success: function (data) {
            if (!data.State) {
                $('#divEditOrder').html(data);
                $('#divEditOrder').css("display", "block");
                $('#divOrderInfo').css("display", "none");
            } else if (data.State == -401) {
                onSessionLost();
            } else if (data.State == -403) {
                alert('对不起，您无此操作权限！若需要操作，请与管理员联系。');
            }
        },
        error: function () {
            alert("处理失败!");
        }
    });
}

var OrderInfo, UserInfo, AddressInfo, InvoiceInfo;

//请求订单信息
function getOrderInfoCtrl($scope, $http) {
    $http({ url: "Transact/GetOrderInfoByID", method: "post", data: { orderId: orderID} }).success(function (data) {
        OrderInfo = $scope.orderInfo = data;
        getUserInfoCtrl($scope, $http);
        getAddressInfoCtrl($scope, $http);
        GetInvoice(OrderInfo, $scope, $http);
    });
}

//请求订单信息
function getUserInfoCtrl($scope, $http) {
    if (!OrderInfo)
        return;
    $http({ url: "Transact/GetUserInfoByID", method: "post", data: { userId: OrderInfo.UserID} }).success(function (data) {
        UserInfo = $scope.userInfo = data;
    });
}

//请求订单信息
function getAddressInfoCtrl($scope, $http) {
    if (!OrderInfo)
        return;
    $http({ url: "Transact/GetReceiveAddressByID", method: "post", data: { addressId: OrderInfo.RecieveAddressID} }).success(function (data) {
        AddressInfo = $scope.addressInfo = data;
        SetProvices(); //加载省市区县信息
    });
}

function GetInvoice(orderInfo, $scope, $http) {
    $("#chkIsRequireInvoice").checked = orderInfo.IsRequireInvoice;
    if (orderInfo.IsRequireInvoice) {
        $http({ url: "Transact/GetOrderInvoicInfo", method: "post", data: { orderId: OrderInfo.ID} }).success(function (data) {
            if (data.State == 1) {
                InvoiceInfo = $scope.invoiceInfo = data.Data;
                switchInvoice($("#chkIsRequireInvoice")[0], $scope.invoiceInfo);
            }else if (data.state == -1) {
                alert("服务器出错了，获取订单发票失败！！");
            }
        });
    }
}

function getCpsListCtrl($scope, $http) {
    $http({ url: "Transact/QueryCpsList", method: "post" }).success(function (response) {
        if (response.State == 1) {
            for (var i = 0; i < response.Data.length; i++) {
                if (response.Data[i].Value == $scope.orderInfo.CpsID) {
                    $("#selectOrderCpsID").append("<option selected='selected' value='" + response.Data[i].Value + "'>" + response.Data[i].Text + "</option>");
                } else {
                    $("#selectOrderCpsID").append("<option value='" + response.Data[i].Value + "'>" + response.Data[i].Text + "</option>");
                }
            }
        }
        else if (response.State == -1) {
            alert("获取CPS数据失败！\r\r服务器错误：" + response.Message);
        }
    });
}

function getInvoiceContentCtrl($scope, $http) {
    $http({ url: "Config/QueryInvoiceContent", method: "get" }).success(function (data) {
        $scope.invoiceContents = data;
    });
}

function switchInvoice(sender,invoiceInfo) {
    if (sender.checked == true) {   // 若开票金额为空，则设置订单总金额为开票金额默认值
        if (isEmpty($("#txtInvoiceMoney").val())) {
            $("#txtInvoiceMoney").val(OrderInfo.TotalMoney);
        }
        $("#txtInvoiceTitle").removeAttr("disabled");
        $("#selectInvoiceContent").removeAttr("disabled");
        $("#txtInvoiceMoney").removeAttr("disabled");
        if (invoiceInfo) {
            $("#txtInvoiceTitle").val(InvoiceInfo.InvoiceTitle);
            $("#selectInvoiceContent").val(InvoiceInfo.InvoiceContentID);
            $("#txtInvoiceMoney").val(InvoiceInfo.InvoiceCost);
        }
    } else {
        $("#txtInvoiceTitle").attr("disabled", "disabled");
        $("#selectInvoiceContent").attr("disabled", "disabled");
        $("#txtInvoiceMoney").attr("disabled", "disabled");
    }
}

// 获取和设置收货地址信息
function SetProvices() {
    $.ajax({
    type: "GET",
    url: "Home/QueryProvinces",
    data: null,
    datatype: "text",
    success: function (list) {
        if (!list) {
            alert("获取用户收货地址信息失败!");
            return false;
        }
        $(list).each(function () {
            if (this.ID == AddressInfo.ProvinceID) {
                $("#addressProvince").append("<option selected='selected' value='" + this.ID + "'>" + this.Name + "</option>");
                SetCities(this.ID);
            }
            else {
                $("#addressProvince").append("<option value='" + this.ID + "'>" + this.Name + "</option>");
            }
        });
    },
    error: function() {
        alert("获取用户收货地址信息失败!");
        }
    });
}

function SetCities(pId) {
    $("#addressCity").html("");
    $("#addressCounty").html("");
    $.ajax({
        type: "GET",
        url: "Home/QueryCities",
        data: {provinceID:pId},
        datatype: "text",
        success: function (list) {
            if (!list) {
                alert("获取用户收货地址信息失败!");
                return false;
            }
            $("#addressCity").append("<option value='0'>请选择</option>");
            $(list).each(function () {
                if (this.ID == AddressInfo.CityID) {
                    $("#addressCity").append("<option selected='selected' value='" + this.ID + "'>" + this.Name + "</option>");
                    SetCounties(this.ID);
                }
                else {
                    $("#addressCity").append("<option value='" + this.ID + "'>" + this.Name + "</option>");
                }
            });
        },
        error: function () {
            alert("获取用户收货地址信息失败!");
        }
    });
}

function SetCounties(cId) {
    $("#addressCounty").html("");
    $.ajax({
        type: "GET",
        url: "Home/QueryCounties",
        data: {cityID:cId},
        datatype: "text",
        success: function (list) {
            if (!list) {
                alert("获取用户收货地址信息失败!");
                return false;
            }
            $("#addressCounty").append("<option value='0'>请选择</option>");
            $(list).each(function () {
                if (this.ID == AddressInfo.CountyID)
                    $("#addressCounty").append("<option selected='selected' value='" + this.ID + "'>" + this.Name + "</option>");
                else {
                    $("#addressCounty").append("<option value='" + this.ID + "'>" + this.Name + "</option>");
                }
            });
        },
        error: function () {
            alert("获取用户收货地址信息失败!");
        }
    });
}

// 获取邮政编码并设置
function SetPostCode(value) {
    $.ajax({
        type: "post",
        url: "Transact/QueryPostCodeByCountyID",
        data: { countyId: value },
        datatype: "json",
        success: function (data) {
            if (!data) {
                alert("获取邮编信息失败!");
                return false;
            }
            $("#txtReceivePostCode").val(data);
        }
    });
}

////////////////////////////////////////////

function orderEdit_DeleteCartProduct(e) {
    if (!confirm("您确定要删除吗？")) {
        return;
    }
    setOrderEditCartProductQuantity();
    var cartProductDataSource = $("#OrderEditCartProductGrid").data("kendoGrid").dataSource;
    $(cartProductDataSource.data()).each(function() {
        if (parseInt(e.name) == this.ProductID) {
            cartProductDataSource.remove(this);
        }
    });
    calcOrderEditAllMoney(); // 计算订单总金额
}

//计算所有商品的合计金额
function calcOrderEditAllMoney() {
    setOrderEditCartProductQuantity();
    OrderInfo.TotalMoney = 0;
    var cartProductDataSource = $("#OrderEditCartProductGrid").data("kendoGrid").dataSource;
    $(cartProductDataSource.data()).each(function () {
        this.TotalPrice = this.TransactPrice * this.Quantity;
        OrderInfo.TotalMoney += this.TotalPrice;
        $("#orderEdit_labelMoney_" + this.ProductID).text(this.TransactPrice * this.Quantity);
    });
    $("span[name='orderTotalMoney']").text(OrderInfo.TotalMoney);
    $("#txtInvoiceMoney").val(OrderInfo.TotalMoney);
}

function getOrderID(e) {
    return { orderId: orderID };
}

//设置购物车中商品的数量
function setOrderEditCartProductQuantity() {
    var cartProductDataSource = $("#OrderEditCartProductGrid").data("kendoGrid").dataSource;
    $(cartProductDataSource.data()).each(function() {
        this.Quantity = $("#orderEdit_Quantity_" + this.ProductID).val();
        this.TransactPrice = $("#orderEdit_Price_" + this.ProductID).val();
    });
}

function orderEdit_EditPrice(sender) {
    var cartProductDataSource = $("#OrderEditCartProductGrid").data("kendoGrid").dataSource;
    $(cartProductDataSource.data()).each(function() {
        if (this.ID == parseInt(sender.name)) {
            this.TransactPrice = $(sender).val();
        }
    });
    calcOrderEditAllMoney();
}

function showAddProductDiv() {
    if ($("#divAddProduct").css("display") == "block") {
        $("#divAddProduct").css("display", "none");
        $("#OrderEditProductGrid").data("kendoGrid").dataSource.read().refresh();
    } else {
        $("#divAddProduct").css("display", "block");
    }
}

function orderEdit_AddProduct() {
    setOrderEditCartProductQuantity();
    var arrChk = $("input[name='selectWare']:checked");
    if (arrChk.length < 1) {
        alert("请选择商品进行操作！");
        return;
    }

    var productDataItems = $("#OrderEditProductGrid").data("kendoGrid").dataSource.data();
    $(productDataItems).each(function() {
        var productDataItem = this;
        $(arrChk).each(function() { //比对商品是否为选中商品
            if (productDataItem.ProductID == this.value) {
                var cartProductDataSource = $("#OrderEditCartProductGrid").data("kendoGrid").dataSource;
                var cartDataItems = cartProductDataSource.data();
                for (var i = 0; i < cartDataItems.length; i++) {
                    if (cartDataItems[i].ProductID == productDataItem.ProductID) { //比对商品是否已经添加，若添加，则不再添加
                        return;
                    }
                }
                if (productDataItem.InventoryNumber > 0) {
                    // 若有库存，则添加并将购买数量设置为1.
                    productDataItem.Quantity = 1;
                    cartProductDataSource.add(productDataItem);
                } else {
                    alert("注意：您选择的商品:" + productDataItem.ProductName + "（商品编码：" + productDataItem.Barcode + "）无库存，不能添加。");
                }
            }
            this.checked = false;
        });
    });
    calcOrderEditAllMoney();
}

//////////////////////////////////////////////////

function CheckData() {
    if (OrderInfo.ID < 1) {
        alert("订单编码错误！");
        return false;
    }
    if (parseInt($("#selectOrderCpsID").val()) < 0) {
        alert("请选择订单来源！");
        return false;
    }
    if (OrderInfo.DeliveryCost < 0) {
        alert("运费错误！");
        return false;
    }
    if (AddressInfo.ID < 1) {
        alert("收货地址编码错误！");
        return false;
    }
    if (trim(AddressInfo.Consignee).length < 1){
        alert("收货人姓名错误！");
        return false;
    }
    if (isEmpty(AddressInfo.Mobile) && isEmpty(AddressInfo.Tel)) {
        alert("收货人手机和电话不能同时为空！");
        return false;
    }
    if (!isEmpty(AddressInfo.Mobile)&&!isMobile(AddressInfo.Mobile)){
        alert("收货人联系手机错误！");
        return false;
    }
    if (AddressInfo.CountyID<1){
        alert("地区地址错误！");
        return false;
    }
    if (isEmpty(AddressInfo.Address) || trim(AddressInfo.Address).length < 2) {
        alert("请填写正确的地址");
        return false;
    }

    // 发票数据
    if (OrderInfo.IsRequireInvoice) {
        if ($("#selectInvoiceType").val() < 1) {
            alert('请选择发票类别！');
            return false;
        }

        if (isEmpty(trim($("#txtInvoiceTitle").val()))) {
            alert("请填写正确的发票抬头！");
            return false;
        }

        if (!isFloat($("#txtInvoiceMoney").val()) || parseFloat($("#txtInvoiceMoney").val()) < 0) {
            alert("开票金额填写不正确!");
            return false;
        }
    }

    //  订单商品列表
    var productDataItems = $("#OrderEditCartProductGrid").data("kendoGrid").dataSource.data();
    if (!productDataItems || productDataItems.length <1) {
        alert("订单商品不能为空！！");
        return false;
    }
    return true;
}

function getAjaxData() {
    var json= {
        "OrderInfo.ID": OrderInfo.ID,
        "OrderInfo.CpsID": $("#selectOrderCpsID").val(),
        "OrderInfo.DeliveryCost": OrderInfo.DeliveryCost,
        "OrderInfo.IsRequireInvoice": OrderInfo.IsRequireInvoice,
        "OrderInfo.Description": OrderInfo.Description,
        //收货地址相关
        "ReceiverInfo.ID": AddressInfo.ID,
        "ReceiverInfo.Consignee": AddressInfo.Consignee,
        "ReceiverInfo.Mobile": AddressInfo.Mobile,
        "ReceiverInfo.Tel": AddressInfo.Tel,
        "ReceiverInfo.PostCode": AddressInfo.PostCode,
        "ReceiverInfo.CountyID": AddressInfo.CountyID,
        "ReceiverInfo.Address": AddressInfo.Address
    };
       
    // 发票数据
    if (OrderInfo.IsRequireInvoice) {
        json["InvoiceInfo.ID"] = InvoiceInfo? InvoiceInfo.ID:0;
        json["InvoiceInfo.InvoiceTypeID"] = 1;
        json["InvoiceInfo.InvoiceContentID"] = $("#selectInvoiceContent").val();
        json["InvoiceInfo.InvoiceTitle"]= $("#txtInvoiceTitle").val();
        json["InvoiceInfo.InvoiceCost"] = $("#txtInvoiceMoney").val();
    }
      
    //  订单商品列表
    var productDataItems = $("#OrderEditCartProductGrid").data("kendoGrid").dataSource.data();
    if (productDataItems && productDataItems.length > 0) {
        for (var i = 0; i < productDataItems.length; i++) {
            json["products[" + i + "].ID"] = productDataItems[i].ID;
            json["products[" + i + "].OrderID"] = OrderInfo.ID;
            json["products[" + i + "].ProductID"] = productDataItems[i].ProductID;
            json["products[" + i + "].Quantity"] = productDataItems[i].Quantity;
            json["products[" + i + "].TransactPrice"] = productDataItems[i].TransactPrice;
        }
    }
    return json;
}
    
function orderConfirmAndBack() {
    orderConfirm(function(e) {
        if (e.State == 1) {
            alert("订单确认成功，返回管理列表。");
            goBackOrderList();
        } else {
            alert("订单确认失败，请与技术人员联系。\n\r错误信息：" + e.Message);
        }
    } );
}

function editOrderDetail() {
    orderEditDetail(function (e) {
        if (e.State == 1) {
            alert("订单修改成功。");
        } else {
            alert("操作失败。\n\r错误信息：" + e.Message);
        }
    });
}

function applyConfirmation() {
    orderConfirm(function (e) {
        if (e.State == 1) {
            alert("订单确认成功.");
        } else {
            alert("订单确认失败，请与技术人员联系。\n\r错误信息：" + e.Message);
        }
    });
}

function orderEditDetail(callback) {
    if (!CheckData())
        return false;

    if (!confirm("您确定要修改此订单吗？")) {
        return false;
    }

    $.ajax({
        type: "POST",
        url: "Transact/EditOrderInfo",
        data: getAjaxData(),
        datatype: "text",
        success: function (data) {
            if (callback)
                callback(data);
        }
    });
}

function orderConfirm(callback) {
    if (!CheckData())
        return false;

    if (!confirm("您确定要确认此订单吗？\r\n注意：确认后的订单将推送到ERP系统进行打包发货！")) {
        return false;
    }
    
    $.ajax({
        type: "POST",
        url: "Transact/ConfirmAndEditOrderDetail",
        data: getAjaxData(),
        datatype: "text",
        success: function (data) {
            if (callback)
                callback(data);
        }
    });
}

//手动完成订单
function completeOrder(e) {
    if (!confirm("您确定手动签收此订单吗？")) {
        return false;
    }

    var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
    if (!dataItem || dataItem.ID < 1) {
        alert("订单编码错误！");
    }
    
    $.post("/Transact/CompleteOrder", { id: dataItem.ID }, function (res) {
        if (res) {
            if (res.State == 1) {
                alert(res.Message);
                SearchOrder(); //刷新表格数据
            } else {
                alert(res.Message);
            }
        } else {
            alert("服务器太懒了，什么也没有告诉我们 %>_<%");
        }
    });
}

//设置废单
function setInvalidOrder() {
    var content = $("#divInvalidOrderDescription").val();
    if (isEmpty(content)) {
        alert("作废原因不能为空！");
        return false;
    }

    if (!confirm("您确定要将此订单作废吗？\n\r注意：作废的订单将无法操作！")) {
        return false;
    }
    
    $.ajax({
        type: "POST",
        url: "Transact/SetOrderToInvalid",
        data: { orderId: OrderInfo.ID, reason:content },
        datatype: "Json",
        success: function (data) {
            if (data.State == 1) {
                alert("设置成功");
                closeSetInvalidOrderWindow();
                goBackOrderList();
            } else {
                alert("设置失败，请与管理员联系。\r\n错误信息：" + data.Message);
            }
        }
    });
}

function showSetInvalidOrderWindow() {
    $("#divSetInvalidOrderWindow").css('display', 'block');
    $("#divInvalidOrderDescription").val("");
    $("#orderInvalidDesecription").data("kendoWindow").center().open();
}

function closeSetInvalidOrderWindow() {
    $("#divSetInvalidOrderWindow").css('display', 'none');
    $("#orderInvalidDesecription").data("kendoWindow").close();
}
    
function goBackOrderList() {
    $('#divEditOrder').html("");
    $('#divEditOrder').css("display", "none");
    $('#divOrderInfo').css("display", "block");
    SearchOrder(); //刷新表格数据
}
