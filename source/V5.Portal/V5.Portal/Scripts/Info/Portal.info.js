var Info = {
    Init: function () {
        Info.CheckSuccess();
        Info.loadPrivince();
        Info.InitControl();
        Info.SaveModeifyMessage();
        Info.selectCityId();

    },

    //功能描述：资料完整度
    CheckSuccess: function () {
        var width = 0;
        var field = ["head", "email", "nickname", "address"];
        for (var i = 0; i < field; i++) {
            var fvalue = $("#" + field[i]).val() || "";
            if (fvalue != "") {
                width += 130;
            }
        }
        $(".progress_inner").css("width", width);
    },

    //功能描述：初始化页面控件
    InitControl: function () {
        //监视省的改变
        $("#province").unbind("change").change(function () {
            Info.loadCities($(this).val());
        });

        //监视市的改变
        $("#city").unbind("change").change(function () {
            Info.loadCountry($(this).val());
        });

        //初始化用户头象
        $("#imgpre").click(function () {
            var td = this.parentNode;
            td.removeChild(this); //移除用户头像
            $("#imgMaster").css("display", "block");
            $("input[name=pic]").css("margin-bootom", "30px");
        });

        //alert($("#Gender").val());
        if ($("#Gender").val() == "False") {
            $("#female").attr("checked", "checked");
        } else {
            $("#male").attr("checked", "checked");
        }
    },

    //加载省
    loadPrivince: function (sltIndex) {
        Info.SendRequest({ action: "../Utility/GetProvinces" }, function (data) {
            $(data.Data).each(function (index, element) {
                if (sltIndex && parseInt(sltIndex) == element.ID) {
                    $("#province").append("<option selected='selected' value='" + element.ID + "'>" + element.Name + "</option>");
                } else {
                    $("#province").append("<option value='" + element.ID + "'>" + element.Name + "</option>");
                }
            });
        });
    },

    //加载市
    loadCities: function (provinceID, sltIndex) {
        $("#city").html("<option value='0' selected='selected'>请选择</option>");
        if (provinceID < 1) {
            return false;
        }
        Info.SendRequest({ action: "../Utility/GetCities", data: { provinceID: provinceID} }, function (res) {

            $(res.Data).each(function (index, element) {
                if (sltIndex && parseInt(sltIndex) == element.ID) {
                    $("#city").append("<option selected='selected' value='" + element.ID + "'>" + element.Name + "</option>");
                    Info.loadPrivince(provinceID);
                } else {
                    $("#city").append("<option value='" + element.ID + "'>" + element.Name + "</option>");
                }
            });
        });
    },

    //加载区
    loadCountry: function (cityID, sltIndex) {
        $("#country").html("<option value='0' selected='selected'>请选择</option>");
        if (cityID < 1) {
            return false;
        }
        Info.SendRequest({ action: "../Utility/GetCounties", data: { cityID: cityID} }, function (res) {
            $(res.Data).each(function (index, element) {
                if (sltIndex && parseInt(sltIndex) == element.ID) {
                    $("#country").append("<option selected ='selected' value='" + element.ID + "'>" + element.Name + "</option>");
                    Info.loadCities(element.CityID, cityID);
                } else {
                    $("#country").append("<option value='" + element.ID + "'>" + element.Name + "</option>");
                }
            });
        });
    },

    //查询城市ID
    selectCityId: function () {
        Info.SendRequest({ action: "../Utility/GetCityID", data: { countryId: $("#hdzone").val()} }, function (res) {
            Info.loadCountry(res.Data[0].CityID, $("#hdzone").val());
        });
    },

    //保存修改信息
    SaveModeifyMessage: function () {
        $("#btnSave").click(function () {
            var headerimg = $("#imgpre").attr("src");
            var email = $("#email").val();
            var nickname = $("#nickname").val();
            var gender = $("#male").is(":checked") == true ? "true" : "false";
            var country = $("#country").val();
            var address = $("#address").val();
            Info.SendRequest({ action: "../User/ModifiyUserMessage", data: { headerimg: headerimg, email: email, gender: gender, nickname: nickname, country: country, address: address} }, function (res) {
                if (res.State == 1) {
                    alert(res.Message);
                }
            });
        });
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
                    console.error("" + e.name + e.message + "");
                }

            }
        });
    }
}