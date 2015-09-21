
var loginIn = {
    loginsubmit: Object,
    exponent: String,
    modulus: String,
    loadcount: Number,

    init: function (option) {
        loginIn.setOptoin(option);
        loginIn.LoadEvent();
    },

    setOptoin: function (option) {
        //处理参数
        loginIn.loginsubmit = $("#loginsubmit");
        $.post("/login/GetPrivateKey", null, function (data) {
            if (data) {
                loginIn.exponent = data.exponent;
                loginIn.modulus = data.modulus;
            }
        });
    },

    //
    LoadEvent: function () {
        loginIn.loginsubmit.click(loginIn.LoginSubmit);
    },

    LoginSubmit: function () {
        if ($("#uname").val() == "") {
            alert("请输入您的登录名！");
            return false;
        }

        if ($("#upwd").val() == "") {
            alert("请输入您的密码！");
            return false;
        }
        loginIn.cmdEncrypt();
        $("#loginsubmit").attr("disabled", "disabled");

        $.ajax({
            type: "POST",
            url: "/login/login/?r=" + Math.random(),
            contentType: "application/x-www-form-urlencoded; charset=utf-8",
            data: $("#loginForm").serialize(),
            datatype: "text",
            success: function (data) {
                if (data.State == 0) {
                    alert(data.Message);
                    loginIn.resetform();
                } else if (data.State == 1) {
                    //登录后，删除登录状态Cookie
                    $.cookie("L", null, { expires: -1, path: '/' });
                    $.cookie("C", null, { expires: -1, path: '/' });
                    var s = window.location.search;
                    s = unescape(s).replace(/\?backurl="?/gi, "");
                    if (!s || s == "") {
                        location.href = "/";
                    } else {
                        location.href = s;
                    }

                    return true;
                }
                return false;
            },
            error: function () {
                alert("网络超时，请稍后再试。");
            }
        });
        return false;
    },

    // 处理敏感信息
    cmdEncrypt: function () {
        setMaxDigits(129);
        var key = new RSAKeyPair(loginIn.exponent, "", loginIn.modulus);
        var pwdMD5Twice = $.md5($("#upwd").attr("value")); // MD5加密
        var pwdRtn = encryptedString(key, pwdMD5Twice);
        $("#upwd").attr("value", pwdRtn);
    },

    // 重置表单
    resetform: function () {
        $("#loginsubmit").removeAttr("disabled");
        $("#upwd").removeAttr("value");
        $("#authcode").removeAttr("value");
        //$("#SecurityCodeImg").attr('src', '/Login/GetSecurityCode' + "?t=" + Math.random());
        loginIn.setOptoin();
        loginIn.loadSecurityCode();
    },

    // 加载验证码
    loadSecurityCode: function () {
        if (isNaN(loginIn.loadcount)) {
            loginIn.loadcount = 0;
        } else {
            loginIn.loadcount += 1;
        }
        if (loginIn.loadcount > 15) {
            alert("验证码请求次数过频繁，请刷新页面后重试");
            return;
        }

        var img = new Image();
        img.src = "/Login/GetSecurityCode?t=" + Math.random();

        if (img.complete) {
            //如果使用了缓存，则重新加载
            loginIn.loadSecurityCode();
        } else {
            // 加载错误后的事件
            img.onerror = function () {
                //如果使用了缓存，则重新加载
                loginIn.loadSecurityCode();
                img = img.onload = img.onerror = null;
            };

            // 加载完毕事件
            img.onload = function () {
                $("#SecurityCodeImg").attr('src', img.src);
                img = img.onload = img.onerror = null;
            };
        };
    }
};


$(document).ready(function () {
    //首次默认加载
    loginIn.loadSecurityCode();

    // 注册验证码点击事件
    $("#SecurityCodeImg").click(function () {
        loginIn.loadSecurityCode();
    });
    $("#replace_img").click(function () {
        loginIn.loadSecurityCode();
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