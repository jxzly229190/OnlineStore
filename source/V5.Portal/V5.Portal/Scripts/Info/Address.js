var Address = {
    setDefaultAddressId: -1,
    setDefaultId: -1,
    Init: function () {
        Address.InitControl();
        this.LoadAddress();
        validate.type = "0";
        // $(".validate").parent().find(".validate_message").remove();

        validate.init({ obj: ".validate", type: "1" });
        $("#addressContainer").myAreas({
            province: "province",
            city: "city",
            country: "country",
            countryId: this.setDefaultAddressId
        });
    },
    LoadAddress: function () {
        this.SendRequest({ action: "../User/GetAddressByUserId", async: false }, function (message) {
            var html = "";
            var titleNumber = 1;
            if (message.State == 1) {
                for (var i = 0; i < message.Data.length; i++) {

                    html += "<dl class=\"uc_address_item\" name=\"" + message.Data[i].ID + "\">";
                    html += "<dt class=\"tag\">" + titleNumber + "</dt><dd class=\"address\">";
                    if (message.Data[i].IsDefault == true) {
                        html += "<label style=\"color: red\" name=\"" + message.Data[i].ID + "\">" + message.Data[i].Consignee + "," + message.Data[i].Address + "," + message.Data[i].Tel + "</label>";
                    } else {
                        html += "<label name=\"" + message.Data[i].ID + "\">" + message.Data[i].Consignee + "," + message.Data[i].Address + "," + message.Data[i].Tel + "</label>";
                    }
                    html += "<label><a href=\"##\"  onclick=\"Address.ModifyInfo(" + message.Data[i].ID + ")\" class=\"button\">修改</a>";
                    html += "<a href=\"###\" class=\"button\" onclick=\"Address.DeleteInfo(" + message.Data[i].ID + ")\">删除</a><a href=\"###\" class=\"button\" onclick=\"Address.SetDefault(" + message.Data[i].ID + ")\">设为默认</a></label></dd></dl>";
                    titleNumber++;
                }
            } else if (message.State == 0) {
                html += "<div>地址薄为空，请添加</div>";
            }
            $("#userInfo").html(html);
        });
    },
    //根据ID加载地址信息
    LoadAddressMessage: function () {
        var oThis = this;
        Address.SendRequest({ action: "../User/GetReceivedAddressById", data: { id: oThis.setDefaultId} }, function (res) {
            $("#Consignee").val(res.Consignee);
            $("#Address").val(res.Address);
            $("#Mobile").val(res.Mobile);
            $("#email").val(res.Email);
            $("#zipcode").val(res.ZipCode);
            $("#tel").val(res.Tel);
            oThis.setDefaultAddressId = res.CountyID;
            $("#addressContainer").myAreas({
                province: "province",
                city: "city",
                country: "country",
                countryId: oThis.setDefaultAddressId
            });
            if (res.IsDefault == true) {
                $("#defaultcheck").attr("checked", "checked");
            } else {
                $("#defaultcheck").attr("checked", false);
            }
        });
    },
    ModifyInfo: function (id) {
        this.setDefaultId = id;
        Address.LoadAddressMessage();
    },
    DeleteInfo: function (id) {
        Address.SendRequest({ action: "../User/DeleteAddress", data: { addressId: id} }, function (res) {
            alert(res.Message);
            Address.LoadAddress();
        });
    },
    SetDefault: function (id) {
        Address.SendRequest({ action: "../User/SetAddressDefault", data: { addressId: id} }, function (res) {
            //alert(res.Message);
            Address.LoadAddress();
        });
    },
    ClearMessage: function () {
        var messageValue = ["Consignee", "Address", "Mobile", "email", "zipcode", "tel"];
        for (var i = 0; i < messageValue.length; i++) {
            $("#" + messageValue[i] + "").val("");
        }
        $("#addressContainer").myAreas({
            province: "province",
            city: "city",
            country: "country",
            countryId: -1
        });
        $("#defaultcheck").attr("checked", false);
    },

    InitControl: function () {
        var controls = ["Consignee", "Address", "Mobile", "email", "zipcode"];
        for (var i = 0; i < controls.length; i++) {
            $("#" + controls[i] + "").mousedown(function () {
                $(this).siblings().remove();
            });
        }
        var oThis = this;
        $(".txt").focus(function () {
            $(this).val("");
        });

        //功能描述：加载默认选中
        if ($("#isDefault").val() == "True") {
            $("#defaultcheck").attr("checked", true);
        }
        ///保存修改
        $("#btnSaveAddress").click(function () {
            var Consignee = $("#Consignee").val();
            if (Consignee == "") {
                $("#Consignee").parent().append("<span class='validate_message'>" + "*用户名不可为空" + "</span>");
                return;
            }
            var address = $("#Address").val();
            if (address == "") {
                $("#Address").parent().append("<span class='validate_message'>" + "*请输入地址" + "</span>");
                return;
            }
            var Mobile = $("#Mobile").val();
            if (Mobile == "") {
                $("#Mobile").parent().append("<span class='validate_message'>" + "*请输入手机号码" + "</span>");
                return;
            }
            var Email = $("#email").val();
            if (Email == "") {
                $("#email").parent().append("<span class='validate_message'>" + "*请输入邮箱地址" + "</span>");
                return;
            }
            var ZipCode = $("#zipcode").val();
            var Tel = $("#tel").val();
            var countryId = $("#country").val();
            var Isdefault = $("#defaultcheck").is(":checked") == true ? true : false;
            Address.SendRequest({ action: "../User/SaveModifyAddress", data: { id: oThis.setDefaultId, consignee: Consignee, address: address, mobile: Mobile, Isdefault: Isdefault, email: Email, zipCode: ZipCode, tel: Tel, countryId: countryId} }, function (res) {
                oThis.LoadAddress();
                Address.ClearMessage();
                oThis.setDefaultId = -1;
            });
        });
    },
    //发送请求
    SendRequest: function (opt, fn) {
        opt.type = opt.type || "json";
        opt.async = opt.async || true;
        $.ajax({
            async: opt.async,
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

                }
            }
        });
    }
}