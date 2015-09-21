
//打开添加邮件对话框
function OpenAddEmail() {
    $("#emailName").val("");
    $("#emailTitle").val("");
    $("#emailStatus").val("");
    $("#editorEmailWindow").data("kendoWindow").open().center();
    //CKEDITOR.instances.Content.setData("");
    $(".k-overlay").css('display', 'block');
    $("#AddBtnDiv").css("display", "block");
    $("#UpdateBtnDiv").css("display", "none");
}

//打开修改邮件对话框
function OpenModifyEmailWindow(e) {
    OpenAddEmail();
    $("#AddBtnDiv").css("display", "none");
    $("#UpdateBtnDiv").css("display", "block");
    var dataItems = $("#emailGrid").data("kendoGrid").dataSource.data();
    $(dataItems).each(function() {
        if (this.ID == Number($(e)[0].name)) {
            $("#emailID").val(this.ID);
            $("#emailName").val(this.Name);
            $("#emailTitle").val(this.Title);
            var editor = CKEDITOR.instances.Content;
            editor.setData(this.Content);
            $("#emailStatus").data("kendoDropDownList").value(this.Status);
        }
    });
}

//确认添加邮件对话框
function ConfirmAddEmail() {
    var emailName = $("#emailName").val();
    var emailTitle = $("#emailTitle").val();
    var emailStatus = $("#emailStatus").val();
    var emailContent = CKEDITOR.instances.Content.getData();
    if (emailName == "") {
        alert("邮件名称不能为空");
        return;
    }
    if (emailTitle == "") {
        alert("邮件标题不能为空");
        return;
    }
    if (emailContent == "") {
        alert("邮件内容不能为空");
        return;
    }
    var jsonData = {};
    jsonData["Name"] = emailName;
    jsonData["Title"] = emailTitle;
    jsonData["Content"] = emailContent;
    jsonData["Status"] = emailStatus;
    jsonData["CreateTime"] = new Date();
    $.post("user/AddMessageEmail", jsonData, function(data) {
        alert(data.Message);
        if (data.State == 1) {
            CloseEditorEmail();
            $("#emailGrid").data("kendoGrid").dataSource.read();
            $("#emailGrid").data("kendoGrid").refresh();
        }
    }, "json");
}

//关闭编辑对话框事件
function CloseEditorWindow() {
    $(".k-overlay").css('display', 'none');
}

// 取消/结束  邮件编辑对话框事件
function CloseEditorEmail() {
    $("#editorEmailWindow").data("kendoWindow").close();
    $(".k-overlay").css('display', 'none');
}

// 确认更改邮件
function ConfirmUpdateEmail() {
    var emailID = $("#emailID").val();
    var emailName = $("#emailName").val();
    var emailTitle = $("#emailTitle").val();
    var emailStatus = $("#emailStatus").val();
    var emailContent = CKEDITOR.instances.Content.getData();
    if (emailName == "") {
        alert("邮件名称不能为空");
        return;
    }
    if (emailTitle == "") {
        alert("邮件标题不能为空");
        return;
    }
    if (emailContent == "") {
        alert("邮件内容不能为空");
        return;
    }
    var jsonData = {};
    jsonData["ID"] = emailID;
    jsonData["Name"] = emailName;
    jsonData["Title"] = emailTitle;
    jsonData["Content"] = emailContent;
    jsonData["Status"] = emailStatus;

    $.post("user/ModifyMessageEmail", jsonData, function (data) {
        alert(data.Message);
        if (data.State == 1) {
            CloseEditorEmail();
            $("#emailGrid").data("kendoGrid").dataSource.read();
            $("#emailGrid").data("kendoGrid").refresh();
        }
    });
}

// 打开发送邮件对话框
function showSendEmailWindow() {
    $(".k-overlay").css('display', 'block');
    $("#sendEmailWindow").data("kendoWindow").open();
    $("#sendEmailWindow").data("kendoWindow").center();
};

// 关闭发送邮件对话框
function closeSendEmailWindow() {
    $(".k-overlay").css('display', 'none');
    $("#sendEmailWindow").data("kendoWindow").close();
}

// 发送邮件
function sendEmail() {
    var emailID = $("#MessageEmail").val();
    if (emailID == 0) {
        alert("请选择需要发送的邮件");
        return;
    }
    $.post("user/SendEmail", { emailID: emailID },
        function(data) {
            alert(data.Message);
            if (data.State==1) {
                closeSendEmailWindow();
            }
        });
}

// 删除短信
function deleteEmail(e) {
    var emailID = e.name;
    var mess = confirm("确实要删除吗?");
    if (mess != "0") {
        $.post("user/RemoveEmail", { id: emailID }, function (data) {
            alert(data.Message);
            if (data.State == 1) {
                var SmsDataSource = $("#EmailGrid").data("kendoGrid").dataSource;
                $(SmsDataSource.data()).each(function() {
                    if (parseInt(emailID) == this.ID) {
                        SmsDataSource.remove(this);
                    }
                });
            }
        });
    }
}