var Common = {
    //功能描述：初始化
    Init: function () {
        Common.Event();
    },

    //功能描述：事件
    Event: function () {
        window.onerror = function () {
            Common.Error();
            return true;
        }
        Common.AjaxError();
    },

    //功能描述：系统错误
    Error: function () {
        arglen = arguments.length;
        var errorMsg = "参数个数：" + arglen + "个";
        for (var i = 0; i < arglen; i++) {
            errorMsg += "/n参数" + (i + 1) + "：" + arguments[i];
        }
        alert(errorMsg);
    },

    //jQuery Ajax请求失败
    AjaxError: function () {
        (function ($) {
            var ajax = $.ajax;
            $.ajax = function (s) {
                debugger;
                if (s.error) {
                    var error = s.error;
                    s.error = function (xhr, status, err) {
                        console.log("error:" + err);
                        error(xhr, status, errMsg || err);
                    }
                }

                if (s.success) {
                    var success = s.success;
                    s.success = function (data, status) {
                        console.log("success:" + status);
                        success(data, status);
                    }
                }
                ajax(s);
            }
        })(jQuery);
    }
}

//功能描述：表单对象，用于获取表单结构和数据
var form = {
    err401: "对不起，当前会话已失效,请重新登录!",
    err403: "对不起，您无此操作权限!",
    errUrl: "参数URL未传入",
    errType: "参数Type未传入",
    errDataType: "参数DataType未传入",
    errFail: "处理失败!",

    //功能描述：判断是否为空
    fnIsEmpty: function (value) {
        if (value == null) { // 等同于 value === undefined || value === null
            return true;
        }

        var type = Object.prototype.toString.call(value).slice(8, -1);
        switch (type) {
            case 'String':
                return !$.trim(value);
            case 'Array':
                return !value.length;
            case 'Object':
                return $.isEmptyObject(value); // 普通对象使用 for...in 判断，有 key 即为 false
            default:
                return false; // 其他对象均视作非空
        }
    },

    //功能描述：获取参数
    fnGetOption: function (option) {
        if (form.fnIsEmpty(option)) return form.fnError();
        if (form.fnIsEmpty(option.type)) return form.fnError(form.errType);
        if (form.fnIsEmpty(option.dataType)) return form.fnError(form.errDataType);
        if (form.fnIsEmpty(option.url)) return form.fnError(form.errUrl);

        return option;
    },

    //功能描述：加载Ajax
    fnLoadAjax: function (option, fn) {
        option = form.fnGetOption(option);
        if (!option) return false;

        $.ajax({
            type: option.type,
            url: option.url,
            data: option.data,
            dataType: option.dataType,
            success: function (result) {
                switch (result.State) {
                    case -401: //会话失效
                        form.fnSessionInvalid();
                        break;
                    case -403: //没有操作权限
                        form.fnError(form.err403);
                        break;
                    default:
                        if ($.isFunction(fn)) {
                            fn(result);
                        }
                        break;
                }
            },
            error: function () {
                form.fnError(form.errFail);
            }
        });
    },

    fnLoadList: function () {
        option.type = option.type || "GET";
        option.dataType = option.dataType || "html";
        form.fnLoadAjax(option, function (data) {
            $("#defaultDiv").show();
            $("#detailDiv").hide();
            $('#defaultDiv').html(data);
            if ($.isFunction(option.fn)) {
                option.fn();
            }
        });
        return true;
    },

    //功能描述：加载表单页面
    fnLoadForm: function (option) {
        option.type = option.type || "GET";
        option.dataType = option.dataType || "html";
        form.fnLoadAjax(option, function (data) {
            $("#defaultDiv").hide();
            $("#detailDiv").show();
            $('#detailDiv').html(data);
            if ($.isFunction(option.fn)) {
                option.fn();
            }
        });
        return true;
    },

    //功能描述：加载表单数据
    fnLoadData: function (option) {
        option.type = option.type || "POST";
        option.dataType = option.dataType || "json";
        form.fnLoadAjax(option, function (data) {
            if ($.isFunction(option.fn)) {
                option.fn(data);
            }
        });
        return true;
    },

    //功能描述：会话失效,重新登录
    fnSessionInvalid: function () {
        //跳转到登录页面(注意，此处尚未注销用户，需要注销用户)
        window.location = "Login";
    },

    //功能描述：输出错误信息
    fnError: function (msg) {
        msg = msg || "参数错误";
        alert(msg);
        return false;
    }
}

// Cookie 操作相关函数
var cookieManager = {
    setCookie: function (name, value, days) {
        var exp = new Date();
        exp.setTime(exp.getTime() + (days || 1) * 24 * 60 * 60 * 1000);
        document.cookie = name + "=" + escape(value) + ";expires=" + exp.toGMTString();
    },
    getCookie: function (name) {
        var arr = document.cookie.match(new RegExp("(^| )" + name + "=([^;]*)(;|$)"));
        if (arr != null) return unescape(arr[2]);
        return null;

    },
    delCookie: function (name) {
        var exp = new Date();
        exp.setTime(exp.getTime() - 1);
        var cval = cookieManager.getCookie(name);
        if (cval != null) document.cookie = name + "=" + cval + ";expires=" + exp.toGMTString();
    }
};

// 当前用户退出后台系统方法
function exit() {
    if (confirm("确认要退出吗？")) {

    } else {
        return false;
    }

    $.ajax({
        type: "POST",
        url: "/Home/Exit",
        data: null,
        datatype: "content",
        success: function (data) {
            if (data == "exit") {
                location.href = "/login";
                return false;
            }
            return false;
        },
        error: function () {
            alert("请求失败，请联系系统管理员！");
        }
    });

    return false;
}

// ajax 请求异常时，调用此方法
function errorMessage() {
    alert("请求失败，请联系系统管理员！");
}

// 睡眠
function sleep(numberMillis) {
    var now = new Date();
    var exitTime = now.getTime() + numberMillis;
    while (true) {
        now = new Date();
        if (now.getTime() > exitTime) return;
    }
}

function onRequestEnd(e) {
    if (e.response.State == -401) {
        alert("会话已失效，请从新登陆！");
        redirectToLogin();
    } else if (e.response.State == -403) {
        alert("对不起，您无此操作权限！");
    }
}

function redirectToLogin() {
    window.location = "Login";
}

function validatePermission(action,controler,method,fun) {
    $.ajax({
        type: "POST",
        url: "/System/GetRightInfo",
        async:false,
        data: { action: action, controller: controler, requestMethod: method },
        datatype: "json",
        success: function (response) {
            if (response.State == -403) {
                fun(false);
            } else if (response.State == -401) {
                onSessionLost();
            } else {
                fun(true);
            }
        },
        error: function () {
            alert("检查失败，请联系系统管理员！");
            return false;
        }
    });
}

function hiddenBtnByPermssion(action,controler,method,object) {
    validatePermission(action,controler,method, function (result) {
        if (result == false) {
            $(object).css('display', 'none');
        }
    });
}

function onSessionLost() {
    alert("会话失效，请重新登录!");
    redirectToLogin();
}
