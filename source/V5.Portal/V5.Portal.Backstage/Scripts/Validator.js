var validate = {
    objs: Object,
    type: String,
    init: function (option) {
        if (!option) return "参数错误";
        if (!option.obj) return "参数错误";
        if ($(option.obj).length == 0) return "无法找到对象";

        validate.objs = $(option.obj);
        validate.type = option.type || "show";
        for (var i = 0; i < validate.objs.length; i++) {
            $(validate.objs[i]).blur(function () {
                validate.check($(this), true);
            });
        }
    },

    info: function ($this, msg, defaultmsg) {
        msg = msg || "";
        var message = msg == "" ? defaultmsg : msg;
        switch (validate.type) {
            case "0":
                alert(message);
                break;
            case "1":
                var msg = $this.attr("message");
                if (msg == message) return;

                $this.attr("message", message);
                $this.parent().append("<span class='validate_message'>" + message + "</span>");
                break;
        }
    },

    check: function ($this, checkEmpty) {
        if (!$this) return;
        var type = $this.attr("validate_type") || "";
        var len = $this.attr("validate_length") || "";
        var msg = $this.attr("validate_message") || "";
        var val = ($this.val() || "").replace(/^\s+|\s+$/, "");

        if (checkEmpty == true && val == "") {
            return false;
        }
        if (checkEmpty == false && val == "") {
            return true;
        }

        var success = true;
        switch (type) {
            case "telphone":
                if (!/^1[3|4|5|8][0-9]\d{4,8}$/.test(val)) {
                    validate.info($this, msg, "手机号码有误，请重新输入!");
                    success = false;
                }
                break;
            case "phone":
                if (!/^(([0\+]\d{2,3}-?)?(0\d{2,3})-?)(\d{7,8})(-?(\d{3,}))?$/.test(val)) {
                    validate.info($this, msg, "电话号码有误，请重新输入!");
                    success = false;
                }
                break;
            case "email":
                if (!/^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+$/.test(val)) {
                    validate.info($this, msg, "邮件地址有误，请重新输入!");
                    success = false;
                }
                break;
            case "qq": //QQ号码限制为5到12位
                if (!/^[1-9][0-9]{4,11}$/.test(val)) {
                    validate.info($this, msg, "QQ号码有误，请重新输入!");
                    success = false;
                }
                break;
            case "chineseName":
                if (!/^[\u4E00-\u9FA5\uf900-\ufa2d]{2,6}$/.test(val)) {
                    validate.info($this, msg, "名称输入有误，请重新输入!");
                    success = false;
                }
                break;
            case "integer": //正整数限制为0-2147483648
                if (!/^[1-9]\d*$/.test(val)) {
                    validate.info($this, msg, "正整数输入有误，请重新输入!");
                    success = false;
                } else if (Number(val) > Math.pow(2, 31)) {
                    validate.info($this, msg, "正整数输入有误，请重新输入!");
                    success = false;
                }
                break;
            case "float":
                if (!/^[1-9]\d*$|^[1-9]\d*\.\d{1,4}$/.test(val)) {
                    validate.info($this, msg, "浮点数输入有误，请重新输入!");
                    success = false;
                } else if (Number(val) > Math.pow(2, 127)) {
                    validate.info($this, msg, "正整数输入有误，请重新输入!");
                    success = false;
                }
                break;
            case "zipcode":
                if (!/^[1-9]\d{5}$/.test(val)) {
                    validate.info($this, msg, "邮政编码有误，请重新输入!");
                    success = false;
                }
                break;
            case "ip":
                if (!/^([1-9]|[1-9]\d|1\d{2}|2[0-1]\d|22[0-3])(\.(\d|[1-9]\d|1\d{2}|2[0-4]\d|25[0-5])){3}$/.test(val)) {
                    validate.info($this, msg, "IP地址有误，请重新输入!");
                    success = false;
                }
                break;
            case "url":
                if (!/^(http[s]?:\/\/(www\.)?|(www\.)?){1}([0-9A-Za-z-\.@:%_\‌​+~#=]+)+((\.[a-zA-Z]{2,3})+)(\/(\w)*)?(\?(\w)*)?/.test(val)) {
                    validate.info($this, msg, "网址有误，请重新输入!");
                    success = false;
                }
                break;
            case "id":
                if (!/^\d{6}(18|19|20)?\d{2}(0[1-9]|1[12])(0[1-9]|[12]\d|3[01])\d{3}(\d|X)$/i.test(val)) {
                    validate.info($this, msg, "身份证号码有误，请重新输入!");
                    success = false;
                }
                break;
            case "word":
                if (!/^[A-Za-z\s]+$/.test(val)) {
                    validate.info($this, msg, "字母输入有误，请重新输入!");
                    success = false;
                }
                break;
        }

        if (!success) {
            $this.val("");
            setTimeout(function () {
                $this.focus();
            }, 0);
        } else {
            //长度校验
            if (len != "" && !isNaN(len)) {
                if (val.length > Number(len)) {
                    validate.info($this, "超出长度限制：{ 0, " + len + " }");
                    $this.val("");
                    setTimeout(function () {
                        $this.focus();
                    }, 0);
                    success = false;
                }
            } else {
                $this.parent().find(".validate_message").remove();
            }
        }
        return success;
    },

    checkall: function (checkEmpty) {
        if (!validate.objs) return true;
        if (!validate.objs.length) return true;

        for (var i = 0; i < validate.objs.length; i++) {
            var success = validate.check($(validate.objs[i]), checkEmpty);
            if (!success) { return false; }
        }
        return true;
    }
}
