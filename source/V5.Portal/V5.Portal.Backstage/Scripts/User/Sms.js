// 打开发送短息对话框
function showSendSmsWindow() {
    $(".k-overlay").css('display', 'block');
    $("#sendSmsWindow").data("kendoWindow").open();
    $("#sendSmsWindow").data("kendoWindow").center();
}

// 关闭发送短息对话框
function closeSendSmsWindow() {
    $(".k-overlay").css('display', 'none');
    $("#sendSmsWindow").data("kendoWindow").close();
}

// 发送短信
function sendSms() {
    var smsID = $("#MessageSms").val();
    if (smsID == 0) {
        alert("请选择需要发送的短信");
        return;
    }
    $.post("user/SendSms", { smsID: smsID },
        function(data) {
            alert(data.Message);
            if (data.State == 1) {
                closeSendSmsWindow();
            }
        });
}

// 删除短信
function deleteSms(e) {
    e.preventDefault();
    var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
    var smsID = dataItem.ID;
    var mess = confirm("确实要删除吗?");
    if (mess != "0") {
        $.post("user/RemoveSms", { id: smsID }, function (data) {
            alert(data.Message);
            if (data.State == 1) {
                debugger;
                var SmsDataSource = $("#SmsGrid").data("kendoGrid").dataSource;
                $(SmsDataSource.data()).each(function () {
                    if (parseInt(smsID) == this.ID) {
                        SmsDataSource.remove(this);
                    }
                });
            }
        });
    }
}