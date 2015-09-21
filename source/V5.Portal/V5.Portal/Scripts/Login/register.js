
var register = {
    registersubmit: Object,
    exponent: String,
    modulus: String,
    
    init: function (option) {
        register.setOptoin(option);
        register.LoadEvent();
    },

    setOptoin: function (option) {
        //处理参数
        register.registersubmit = $("#register_submit");
        $.post("/login/GetPrivateKey", null, function (data) {
            if (data) {
                register.exponent = data.exponent;
                register.modulus = data.modulus;
            }
        });
    },

    //
    LoadEvent: function () {
        register.registersubmit.click(register.RegisterSubmit);
    },

    RegisterSubmit: function () {
        if (!register.inputverify()) {
            return false;
        }

        register.cmdEncrypt();
        $("#register_submit").attr("disabled", "disabled");
        $.ajax({
            type: "POST",
            url: "/login/RegisterResult/?r=" + Math.random(),
            contentType: "application/x-www-form-urlencoded; charset=utf-8",
            data: $("#register_form").serialize(),
            datatype: "text",
            success: function (data) {
                $.cookie("L", null, { expires: -1, path: '/' });
                $.cookie("C", null, { expires: -1, path: '/' });
                if (data.State == 0) {
                    alert(data.Message);
                    register.resetform();
                } else if (data.State == 1) {
                    register.resetform();
                    location.href = "/Login/RegisterSuccess";
                    return true;
                }
                return false;
            },
            error: function () {
                register.resetform();
                try {
                    console.log("请求失败,请联系系统管理员");
                } catch (e) {

                }
            }
        });
        return false;
    },

    //输入验证
    inputverify: function () {
        if ($("#uname").val() == "") {
            alert("请输入您的登录名！");
            document.getElementById("uname").focus();
            return false;
        }

        // todo:优化验证 检查邮箱格式or手机格式
        if ($("#uname").val().length < 6 || $("#uname").val().length > 50) {
            alert("您的登录名出范围！");
            document.getElementById("uname").focus();
            return false;
        }

        if ($("#upwd").val() == "") {
            alert("请输入您的密码！");
            document.getElementById("upwd").focus();
            return false;
        }
        if ($("#upwd").val().length < 6 || $("#upwd").val().length > 20) {
            alert("您的密码长度超出范围！");
            document.getElementById("upwd").focus();
            return false;
        }

        if ($("#upwd").val() != $("#upwd2").val()) {
            alert("两次输入的密码不一致！");
            document.getElementById("upwd2").focus();
            return false;
        }

        if (!$("#agree").attr("checked")) {
            alert("请阅读《购酒网用户注册协议》！");
            return false;
        }
        return true;
    },

    // 处理敏感信息
    cmdEncrypt: function () {
        setMaxDigits(129);
        var key = new RSAKeyPair(register.exponent, "",register.modulus);
        var pwdMD5Twice = $.md5($("#upwd").attr("value")); // MD5加密
        var pwdRtn = encryptedString(key, pwdMD5Twice);
        $("#upwd").attr("value", pwdRtn);
    },
    // 重置表单
    resetform: function () {
        $("#register_submit").removeAttr("disabled");
        $("#upwd").removeAttr("value");
        $("#upwd2").removeAttr("value");
        $("#authcode").removeAttr("value");
        $("#SecurityCodeImg").attr('src', '/Login/GetSecurityCode' + "?" + Math.random());
    }
};

$(document).ready(function () {
    $("#SecurityCodeImg").attr('src', '/Login/GetSecurityCode?t=' + Math.random());
    // 注册验证码点击事件
    $("#SecurityCodeImg").click(function () {
        $("#SecurityCodeImg").attr('src', '/Login/GetSecurityCode?t=' + Math.random());
    });
    $("#replace_img").click(function () {
        $("#SecurityCodeImg").attr('src', '/Login/GetSecurityCode?t=' + Math.random());
    });
    // 回车提交登录请求
    $(document).keypress(function (e) {
        var enterKey = 13;
        if (e.which == enterKey) {
            $("#loginsubmit").trigger("click");
        }
    });

    return true;
});