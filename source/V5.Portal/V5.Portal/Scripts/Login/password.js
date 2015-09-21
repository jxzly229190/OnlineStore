$(function () {
    passwordIn.init();
});

var passwordIn = {
    passwordsubmit: Object,

    init: function (option) {
        passwordIn.setOptoin(option);
        passwordIn.LoadEvent();
    },

    setOptoin: function (option) {
        //处理参数
        passwordIn.passwordsubmit = $("#pwdsubmit");
    },

    //
    LoadEvent: function () {
        passwordIn.passwordsubmit.click(passwordIn.PwdSubmit);
    },

    PwdSubmit: function () {
        if ($("#oldpwd").val() == "") {
            alert("请输入您的原密码！");
            return false;
        }

        if ($("#newpwd").val() == "") {
            alert("请输入您的新密码！");
            return false;
        }

        if ($("#rnewpwd").val() == "") {
            alert("请确认您的新密码！");
            return false;
        }

        if ($("#newpwd").val() != $("#rnewpwd").val()) {
            alert("两次密码输入不一致！");
            return false;
        }

        passwordIn.cmdEncrypt();
        $("#pwdsubmit").attr("disabled", "disabled");

        $.ajax({
            type: "POST",
            url: "/User/ModifyPassword/?r=" + Math.random(),
            contentType: "application/x-www-form-urlencoded; charset=utf-8",
            data: $("#pwdForm").serialize(),
            datatype: "text",
            success: function (data) {
                if (data.State == 0) {
                    alert(data.Message);
                    passwordIn.resetform();
                } else if (data.State == 1) {
                    passwordIn.resetform();
                    alert(data.Message);
                    location.href = "/User/";
                    return true;
                }
                return false;
            },
            error: function () {
                passwordIn.resetform();
                try {
                    console.log("请求失败，请联系系统管理员！");
                } catch (e) {
                   
                }

            }
        });
        return false;
    },

    // 处理敏感信息
    cmdEncrypt: function () {
        setMaxDigits(129);
        var exponent = $("#pke").attr("value");
        var modulus = $("#pkm").attr("value");
        var key = new RSAKeyPair(exponent, "", modulus);
        var oldpwdMD5Twice = $.md5($("#oldpwd").attr("value")); // MD5加密
        var newpwdMD5Twice = $.md5($("#newpwd").attr("value")); // MD5加密
        var oldpwdRtn = encryptedString(key, oldpwdMD5Twice);
        var newpwdRtn = encryptedString(key, newpwdMD5Twice);
        $("#oldpwd").attr("value", oldpwdRtn);
        $("#newpwd").attr("value", newpwdRtn);
    },
    // 重置表单
    resetform: function () {
        $("#pwdsubmit").removeAttr("disabled");
        $("#oldpwd").removeAttr("value");
        $("#newpwd").removeAttr("value");
        $("#rnewpwd").removeAttr("value");
    }
};