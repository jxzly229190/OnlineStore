$(function () {
    FindPassWord.Init();
});
var FindPassWord = {
    Id: Object,
    userId: Object,
    loadcount: Number,

    Init: function () {
        this.ChangeSecurity();
        this.InitControl();
        this.CheckEmailOrPhone();
    },
    //功能描述:变换验证码
    ChangeSecurity: function () {
        if (isNaN(FindPassWord.loadcount)) {
            FindPassWord.loadcount = 0;
        } else {
            FindPassWord.loadcount += 1;
        }
        if (FindPassWord.loadcount > 15) {
            alert("验证码请求次数过频繁，请刷新页面后重试");
            return;
        }

        var img = new Image();
        img.src = "/Login/GetSecurityCode?t=" + Math.random();

        if (img.complete) {
            //如果使用了缓存，则重新加载
            FindPassWord.ChangeSecurity();
        } else {
            // 加载错误后的事件
            img.onerror = function () {
                //如果使用了缓存，则重新加载
                FindPassWord.ChangeSecurity();
                img = img.onload = img.onerror = null;
            };

            // 加载完毕事件
            img.onload = function () {
                $("#virificationImg").attr('src', img.src);
                img = img.onload = img.onerror = null;
            };
        };
    },
    CheckEmailOrPhone: function () {
        $("#userName").blur(function () {
            var emailOrPhone = $("#userName").val();
            var result = emailOrPhone.indexOf("@");
            if (result == -1) {
                if (!/^1[3|4|5|8][0-9]\d{4,8}$/.test(emailOrPhone)) {
                    $("#validateMessage").text("手机号码有误，请重新输入!").css("color", "red");
                    FindPassWord.ChangeSecurity();
                    return;
                }
            } else {
                if (!/^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+$/.test(emailOrPhone)) {
                    $("#validateMessage").text("邮件地址有误，请重新输入!").css("color", "red");
                    FindPassWord.ChangeSecurity();
                    return;
                }
            }
        });

    },
    InitControl: function () {
        $("#userName").focus(function () {
            $("#userName").val("");
            $("#validateMessage").text("请输入邮箱/已验证手机号").css("color", "");
        });

        $("#virificationImg").click(function () {
            FindPassWord.ChangeSecurity();
        });

        $("#ChangeSecCode").click(function () {
            FindPassWord.ChangeSecurity();
        });

        $("#loginsubmit").click(function () {
            var emailOrSms = $("#userName").val();
            var validate = $("#validateCode").val();
            if (emailOrSms == "邮箱/已验证手机号" || emailOrSms == "") {
                alert("请输入您的 邮箱/已验证手机号");
                FindPassWord.ChangeSecurity();
                return;
            }
            if (validate == "") {
                alert("请输入验码");
                FindPassWord.ChangeSecurity();
                return;
            }
            FindPassWord.SendRequest({ action: "/Login/SendNewEncyCode", data: { emailOrSms: emailOrSms, validateCode: validate} }, function (data) {
                if (data.State == 0) {
                    alert(data.Message);
                    FindPassWord.ChangeSecurity();
                } else if (data.State == 1) {
                    alert("密码已发送到您的" + data.Message + ",请注意查收，系统将为您跳转到登录页面");
                    window.location.href = "/Login";
                }
            });
        });

    },
    SendRequest: function (opt, fn) {
        opt.dataType = opt.dataType || "json",
        $.ajax({
            url: opt.action,
            type: "POST",
            dataType: opt.dataType,
            data: opt.data,
            success: function (data) {
                if ($.isFunction(fn)) {
                    fn(data);
                }
            }
        });
    }
}