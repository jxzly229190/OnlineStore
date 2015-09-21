/////////////////////////////////////////////////
/**************取消订单******************/

function cancelOrder(para) {
    para.preventDefault();
    var dataItem = this.dataItem($(para.currentTarget).closest("tr"));
    $.ajax({
        type: "POST",
        async: false,
        url: "Transact/CheckOrderStatus",
        data: { orderId: dataItem.ID },
        datatype: "Json",
        success: function (data) {
            if (data.State == 1) {
                showOrderCancelWindow(0, dataItem.ID, dataItem.OrderCode);

            } else if (data.State == 2) {
                alert("已支付订单退货功能正在开发，暂不可用！");
                return false;
                showOrderCancelWindow(1, dataItem.ID, dataItem.OrderCode);
            } else {
                alert("打开订单取消窗口失败，请与管理员联系。\r\n错误信息：" + data.Message);
            }
        }
    });
}

var wnd;
function showOrderCancelWindow(type,orderId,orderCode) {
    var url;
    var html;
    if (type == 0) {
        url = "Transact/OrderCancelView";
    } else {
        url = "Transact/OrderCancelWithRefundView";
    }

    $.ajax({
        type: "GET",
        async: false,
        url: url,
        data: { orderId: orderId, orderCode: orderCode},
        datatype: "HTML",
        success: function (data) {
            if (!data.State) {
                html = data;
            }else if (data.State == -401) {
                alert('对不起，会话已过期，请重新登录!');
                redirectToLogin();
            }else if (data.State == -403) {
                alert('对不起，您无此操作权限！若需要操作，请与管理员联系。');
            }
        }
    });
    wnd = $("#OrderCancelWindow").data("kendoWindow");
    wnd.content(html).open().center();
}

function closeOrderCancelWindow() {
    wnd.close();
    SearchOrder();
}


///////////////////////////////

function buildRequestData(e) {
    var requestData;
    if (e == 0) {
        requestData = {
            "OrderID": $("#OrderID").val(),
            "OrderCancelCauseID": $("#OrderCancelCauseID").val(),
            "Description": $("#Description").val(),
            "type": "0"
        };
        
        if (!isInteger(requestData.OrderID)) {
            alert("订单编码获取错误！");
            return false;
        }

        if (!isInteger(requestData.OrderCancelCauseID) || parseInt(requestData.OrderCancelCauseID)<1) {
            alert("请选择订单取消原因！");
            return false;
        }

        if (isEmpty(requestData.Description)) {
            alert("取消备注不能为空！");
            return false;
        }
    } else {
        requestData = {
            "OrderID": $("#OrderID").val(),
            "OrderCancelCauseID": $("#OrderCancelCauseID").val(),
            "Description": $("#cancelDescription").val(),
            "RefundMethodID": $("#RefundMethodID").val(),
            "ActualRefundMoney": $("#ActualRefundMoney").val(),
            "RefundDescription": $("#Description").val(),
            "type": "1"
        };

        if (!isInteger(requestData.OrderID)) {
            alert("订单编码获取错误！");
            return false;
        }

        if (!isInteger(requestData.OrderCancelCauseID) || parseInt(requestData.OrderCancelCauseID) < 1) {
            alert("请选择订单取消原因！");
            return false;
        }

        if (isEmpty(requestData.orderCancelDescription)) {
            alert("取消备注不能为空！");
            return false;
        }

        if (!isInteger(requestData.RefundMethodID) || parseInt(requestData.RefundMethodID)<1) {
            alert("请选择退款方式！");
            return false;
        }

        if (!isFloat(requestData.ActualRefundMoney)||parseFloat(requestData.ActualRefundMoney)<0) {
            alert("退款金额输入错误！");
            return false;
        }

        if (parseFloat(requestData.ActualRefundMoney) > parseFloat($("#PaymentMoney").val())) {
            alert("退款金额不能大于已支付金额！");
            return false;
        }
    }

    return requestData;
}

function submitOrderCancel(e) {
    var url = "Transact/CancelOrderRefund" , requestData = buildRequestData(e);
    if (!requestData) {
        return false;
    }

    if (!confirm("请确定要取消此订单吗？")) {
        return false;
    }
    
    $.ajax({
        type: "POST",
        async: false,
        url: url,
        data: requestData,
        datatype: "Json",
        success: function (response) {
            if (response.State == 1) {
                alert("订单取消成功！");
                closeOrderCancelWindow();
            } else if (response.State == 2) {
                alert("订单已发货，请在售后模块处理！");
                closeOrderCancelWindow();
            } else {
                alert(response.Message);
            }
        }
    });
}

