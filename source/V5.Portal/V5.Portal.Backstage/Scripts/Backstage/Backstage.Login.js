// Cookie 操作相关函数
var cookieManager = {
    setCookie: function(name, value, days) {
        var exp = new Date();
        exp.setTime(exp.getTime() + (days || 1) * 24 * 60 * 60 * 1000);
        document.cookie = name + "=" + escape(value) + ";expires=" + exp.toGMTString();
    },
    getCookie: function(name) {
        var arr = document.cookie.match(new RegExp("(^| )" + name + "=([^;]*)(;|$)"));
        if (arr != null) return unescape(arr[2]);
        return null;
    },
    delCookie: function(name) {
        var exp = new Date();
        exp.setTime(exp.getTime() - 1);
        var cval = cookieManager.getCookie(name);
        if (cval != null) document.cookie = name + "=" + cval + ";expires=" + exp.toGMTString();
    }
};

$(document).ready(function () {
    // 注册验证码点击事件
    $("#SecurityCodeImg").click(function () {
        $("#SecurityCodeImg").attr('src', '/Login/GetSecurityCode' + "?" + Math.random());
    });

    // 回车提交登录请求
    $(document).keypress(function (e) {
        var enterKey = 13;
        if (e.which == enterKey) {
            $("#loginBtn").trigger("click");
        }
    });
    
    // 判断 Cookie 中是否存在登录名，如果存在就加载到输入框中
    if (cookieManager.getCookie("LoginName") == null) {
        return false;
    } else {
        $("#loginName").val(cookieManager.getCookie("LoginName"));
        if (cookieManager.getCookie("LoginName") != "") {
            $("#remember").attr("checked", true);
        } else {
            $("#remember").attr("checked", false);
        }
    }

    return true;
});

// 登录输入验证
function login() {
    if ($("#loginName").val() == "") {
        alert("请输入您的登录名！");
        return false;
    }

    if ($("#loginPassword").val() == "") {
        alert("请输入您的密码！");
        return false;
    }

    if ($("#securityCode").val() == "") {
        alert("请输入验证码！");
        return false;
    }

    $.ajax({
        type: "POST",
        url: "/login/index",
        data: {
            loginName: $("#loginName").val(),
            loginPassword: $("#loginPassword").val(),
            securityCode: $("#securityCode").val(),
            remember: $("#remember").attr("checked")
        },
        datatype: "text",
        success: function (data) {
            if (data == "1") {
                alert("验证码输入有误！");
            } else if (data == "2") {
                alert("登录名或密码不正确！");
            } else if (data == "success") {
                location.href = "/Home";
                return true;
            }

            $("#SecurityCodeImg").trigger("click");
            return false;
        },
        error: function () {
            alert("请求失败，请联系系统管理员！");
        }
    });

    return false;
};