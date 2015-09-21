var Address = {
    Init: function () {
        Address.loadPrivince();
        Address.InitControl();
        Address.InitZone();
    },

    //功能描述：根据ID加载地址信息
    LoadAddressMessage: function (addressid) {
        Address.SendRequest({ action: "../User/ModifyReceivedAddress", data: { addressId: addressid} }, function (res) {
            $("#Consignee").val(res.Consignee);
            $("#Address").val(res.Address);
            $("#Mobile").val(res.Mobile);
            $("#email").val(res.Email);
            $("#zipcode").val(res.ZipCode);
            $("#tel").val(res.Tel);
            $("#defaultAddress").val(res.CountyID); //给隐藏域赋值
            $("#addressId").val(res.ID);
            Address.selectCityId(res.CountyID);
            if (res.IsDefault == true) {
                $("#defaultcheck").attr("checked", "checked");
            } else {
                $("#defaultcheck").attr("checked", false);
            }
        });
    },

    //功能描述：
    InitControl: function () {
        //监视省的改变
        $("#province").unbind("change").change(function () {
            Address.loadCities($(this).val(), 0);
        });

        //监视市的改变
        $("#city").unbind("change").change(function () {
            Address.loadCountry($(this).val(), 0);
        });

        //修改
        $("a[modifyid]").click(function () {
            Address.LoadAddressMessage($(this).attr("modifyid"));
        });

        //删除Address
        $("a[delete]").click(function () {
            var defautId = $(this).attr("delete");
            Address.SendRequest({ action: "../User/DeleteAddress", data: { addressId: $(this).attr("delete")} }, function (res) {
                $("dl[name]").each(function () {
                    if ($(this).attr("name") == defautId) {
                        $(this).remove();
                    }
                });
                alert(res.Message);
            });
        });

        //设为默认收货地址
        $("a[isdefault]").click(function () {
            var defautId = $(this).attr("isdefault");
            Address.SendRequest({ action: "../User/SetAddressDefault", data: { addressId: $(this).attr("isdefault")} }, function (res) {
                $("label[name]").each(function () {
                    if ($(this).attr("name") == defautId) {
                        $(this).css("color", "red");
                    } else {
                        $(this).css("color", "");
                    }
                });
                Address.LoadAddressMessage(defautId); //加载地址信息
                alert(res.Message);
            });
        });

        //保存修改
        $("#btnSaveAddress").click(function () {
            var Id = $("#addressId").val();
            var Consignee = $("#Consignee").val();
            if (Consignee == "") {
                $("#Consignee").parent().append("<span class='validate_message'>" + "用户名不可为空" + "</span>");
                return;
            }
            var address = $("#Address").val();
            if (address == "") {
                $("#Address").parent().append("<span class='validate_message'>" + "请输入地址" + "</span>");
                return;
            }
            var Mobile = $("#Mobile").val();
            if (Mobile == "") {
                $("#Mobile").parent().append("<span class='validate_message'>" + "请输入邮箱地址" + "</span>");
                return;
            }
            var Email = $("#email").val();
            if (Email == "") {
                $("#email").parent().append("<span class='validate_message'>" + "请输入邮箱地址" + "</span>");
                return;
            }
            var ZipCode = $("#zipcode").val();
            var Tel = $("#tel").val();
            var countryId = $("#defaultAddress").val();
            var Isdefault = $("#defaultcheck").attr("checked") == "checked" ? true : false;
            Address.SendRequest({ action: "../User/SaveModifyAddress", data: { id: Id, consignee: Consignee, address: address, mobile: Mobile, Isdefault: Isdefault, email: Email, zipCode: ZipCode, tel: Tel, countryId: countryId} }, function (res) {
                alert(res.Message);
            });
        });
    },

    //初始化默认地址所有信息
    InitZone: function () {
        var cityId = $("#defaultAddress").val();
        Address.selectCityId(cityId);
    },

    //加载省
    loadPrivince: function (sltIndex) {
        $("#province").html("<option value='0' selected='selected'>请选择省/直辖市</option>");
        Address.SendRequest({ action: "../Utility/GetProvinces" }, function (data) {
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
        $("#city").html("<option value='0' selected='selected'>请选择市/地区</option>");
        if (provinceID < 1) {
            return false;
        }
        Address.SendRequest({ action: "../Utility/GetCities", data: { provinceID: provinceID} }, function (res) {
            $(res.Data).each(function (index, element) {
                if (sltIndex && parseInt(sltIndex) == element.ID) {
                    $("#city").append("<option selected='selected' value='" + element.ID + "'>" + element.Name + "</option>");

                    Address.loadPrivince(provinceID);
                } else {
                    $("#city").append("<option value='" + element.ID + "'>" + element.Name + "</option>");
                }
            });
        });
    },

    //加载区
    loadCountry: function (cityID, sltIndex) {
        $("#country").html("<option value='0' selected='selected'>请选择市区/县</option>");
        $("#country").html("");
        if (cityID < 1) {
            return false;
        }
        Address.SendRequest({ action: "../Utility/GetCounties", data: { cityID: cityID} }, function (res) {
            $(res.Data).each(function (index, element) {
                if (sltIndex && parseInt(sltIndex) == element.ID) {
                    $("#country").append("<option selected ='selected' value='" + element.ID + "'>" + element.Name + "</option>");
                    Address.selectProviceID(element.CityID, cityID);
                } else {
                    $("#country").append("<option value='" + element.ID + "'>" + element.Name + "</option>");
                }
            });
        });
    },

    //根据城市ID查询省ID
    selectProviceID: function (cityID) {
        Address.SendRequest({ action: "../Utility/GetProvinceID", data: { cityID: cityID} }, function (res) {
            Address.loadCities(res.Data[0].ProvinceID, cityID);
        });
    },

    //根据区ID查区
    selectCityId: function (countryId) {
        Address.SendRequest({ action: "../Utility/GetCityID", data: { countryId: countryId} }, function (res) {
            Address.loadCountry(res.Data[0].CityID, countryId);
        });
    },

    //发送请求
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