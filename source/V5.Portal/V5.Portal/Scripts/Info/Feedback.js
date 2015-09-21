var FeedBack = {
    //功能描述:页面加载时变换验证码
    Init: function () {
        //刷新验证码
        FeedBack.ChangeSecurity();

        //初始化控件
        FeedBack.InitControl();

        //图片上传控件初始化
        //ImagePreviewEx.Init({ img: ["imgMaster"], type: "1" });

        //开启校验
        validate.clear();
        validate.init({ obj: ".validate", type: "1" });
    },

    //功能描述:变换验证码
    ChangeSecurity: function () {
        $("#virificationImg").attr('src', '/Login/GetSecurityCode' + "?" + Math.random());
    },

    //功能描述：监听页面事件
    InitControl: function () {
        //功能描述：保存用户反馈信息
        $("#btnSubmit").click(function () {
            FeedBack.Save();
        });

        //功能描述：点击图片变换验证码
        $("#securityCode").click(function () {
            FeedBack.ChangeSecurity();
        });

        //功能描述：点击链接变换验证码
        $("#chang_Code").click(function () {
            FeedBack.ChangeSecurity();
        });
    },

    //功能描述：保存
    Save: function () {
        var data = {};
        data.name = $("#name").val();
        if (data.name == "") {
            alert("请输入姓名");
            return;
        }
        data.telPhone = $("#mob").val();
        if (data.telPhone == "") {
            alert("联系方式不能为空");
            return;
        }
        data.email = $("#email").val();
        if (data.email == "") {
            alert("输入邮箱");
            return;
        }
        data.type = $("#back_type").val();
        data.content = $("#content").val();
        data.imgUrl = "///";
        data.gjwNumber = $("#gjw-number").val();
        data.gender = true;
        if ($("#male").attr("checked") == "checked") {
            gender = true;
        } else if ($("#female").attr("checked") == "checked") {
            gender = false;
        }
        data.checkCode = $("#validate").val();

        FeedBack.SendRequest({ action: "../Home/SaveFeedBack", data: data }, function (result) {
            if (result.State == 0) {
                alert(result.Message);

                FeedBack.ChangeSecurity();
            } else {
                alert("感谢您给我们提的宝贵建议!");
                window.location.href = "/";
            }
        });
    },

    //功能描述：发送请求
    SendRequest: function (opt, fn) {
        opt.type = opt.type || "json";
        $.ajax({
            type: "POST",
            url: opt.action,
            data: opt.data,
            dataType: opt.type,
            success: function (data) {
                if ($.isFunction(fn)) {
                    fn(data);
                }
            },
            error: function () {
                try {
                    console.log("请求失败");
                } catch (e) {
                    console.error("" + e.name + e.message + "");
                }
            }
        });
    }
}