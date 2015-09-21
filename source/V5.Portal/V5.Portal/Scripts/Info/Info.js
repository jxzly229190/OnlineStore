var Info = {
    Init: function () {
        Info.CheckSuccess();
        Info.InitControl();
        Info.SaveModeifyMessage();
        validate.type = "0";
        $(".validate").parent().find(".validate_message").remove();
        validate.init({ obj: ".validate", type: "1" });
        Info.ClearMessage();

    },
    ///资料完整度
    CheckSuccess: function () {
        var width = 0;
        var field = ["head", "email", "nickname", "address"];
        for (var i = 0; i < field.length; i++) {
            var fvalue = $("#" + field[i]).val() || "";
            if (fvalue != "") {
                width += 130;
            }
        }
        $(".progress_inner").css("width", width);
    },
    ///初始化页面控件
    InitControl: function () {
        if ($("#hdGender").val() == "True") {
            $("#male").attr("checked", true);
        } else if ($("#hdGender").val() == "False") {
            $("#female").attr("checked", true);
            $("#male").attr("checked", false);

        }
        //初始化用户头象
        $("#imgpre").click(function () {
            var td = this.parentNode;
            td.removeChild(this); //移除用户头像
            $("#imgMaster").css("display", "block");
            $("input[name=pic]").css("margin-bootom", "30px");
        });
        if ($("#Gender").val() == "False") {
            $("#female").attr("checked", "checked");
        } else {
            $("#male").attr("checked", "checked");
        }

    },
    ///保存修改信息
    SaveModeifyMessage: function () {
        $("#btnSave").click(function () {
            var headerimg = $("#imgpre").attr("src");
            var email = $("#email").val();
            if (email == "") {
                $("#email").parent().append("<span class='validate_message'>" + "邮件不能为空" + "</span>");
                return;
            }
            var nickname = $("#nickname").val();
            if (nickname == "") {
                $("#nickname").parent().append("<span class='validate_message'>" + "昵称不能为空" + "</span>");
                return;
            }
            var gender = $("#male").is(":checked") == true ? "true" : "false";
            var country = $("#country").val() == "" ? -1 : $("#country").val();
            var address = $("#address").val();
            if (address == "") {
                $("#address").parent().append("<span class='validate_message'>" + "地址不能为空" + "</span>");
                return;
            }
            Info.SendRequest({ action: "../User/ModifiyUserMessage", data: { headerimg: headerimg, email: email, gender: gender, nickname: nickname, country: country, address: address} }, function (res) {
                if (res.State == 1) {
                    alert(res.Message);
                }
            });
        });
    },
    ClearMessage: function () {
        var message = ["email", "nickname", "address"];
        setTimeout(function () {
            for (var i = 0; i < message.length; i++) {
                $("#" + message[i] + "").parent().remove();
            }

        }, 1000);

    },
    //发送请求
    SendRequest: function (opt, fn) {
        $.ajax({
            type: "POST",
            url: opt.action,
            data: opt.data,
            dataType: "json",
            success: function (data) {
                if ($.isFunction(fn)) {
                    fn(data);
                }
            },
            error: function () {
                try {
                    console.log("请求失败");
                } catch (e) {
                }
            }
        });
    }
}