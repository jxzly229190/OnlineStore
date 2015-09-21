var postOrder = {
    init: function() {
        postOrder.initReceiveInfo();
        postOrder.eventBind();
    },
    
    //初始化收货人信息
    initReceiveInfo: function() {
        if (!$("#defaultShipInfo")) {
            // 若没有收货人信息，显示新增收货人
            $("#user_info").removeClass("show").addClass("hidden");
            $("#modifyShip").removeClass("hidden").addClass("show");
            $("#add_ship_show").attr("checked", "checked");
        }
    },

    eventBind: function() {
        //单击更改按钮
        $("#alterUserAddress").unbind("click").click(function() {
            $("#user_info").removeClass("show").addClass("hidden");
            $("#modifyShip").removeClass("hidden").addClass("show");
        });

        $(".shipModify").unbind("click").click(function() {
            //若不是新增或者编辑，则修改收货地址信息，若是，则先保存数据库，再修改收货信息
            if (!$("#alert-user-input").hasClass("hidden")) {
                alert("对不起，此功能还在开发中！");
                return false;
//                $.post("AddAddress", {}, function() {
//                    
//                });
            }
            postOrder.applyChange();
        });

        //选择一个已有的地址
        $(".defaultPath .radio").unbind("click").click(function() {
            $(".defaultPath .radio").removeAttr("checked");
            $(this).attr("checked", "checked");
            $(".alert-user-input").removeClass("show").addClass("hidden");
            $(".defaultPath").removeClass("selected");
            $(this).parents(".defaultPath").addClass("selected");
            $("#add_ship_show").removeClass("selected");
        });

        //选择添加新地址
        $("#add_ship_show").unbind("click").click(function() {
            $(".alert-user-input").removeClass("hidden").addClass("show");
            $(this).children(".radio").attr("checked", "checked");
            $(".defaultPath").removeClass("selected");
            $(this).addClass("selected");
            postOrder.loadProvince();
        });

        //选择省市区地址
        $("#province").unbind("change").change(function () {
            postOrder.loadCity($(this).val());
        });

        $("#city").unbind("change").change(function () {
            postOrder.loadCounty($(this).val());
        });

        //删除地址信息
        $(".address-remove").unbind("click").click(function () {
            
            if (!confirm("您确定要删除此地址吗？")) {
                return false;
            }
            
            var id = $(this).attr("index");
            $.post("RemoveAddress", { addressID: id }, function (res) {
                if (res.State == 1) {
                    $("#addreess_" + id).remove();
                } else {
                    alert("操作失败");
                }
            });
        });

        //修改地址
        $(".address-edit").unbind("click").click(function () {
            alert("对不起，此功能尚在开发中，暂不能操作~");
            return false;
            
            var id = $(this).attr("index");
            var shipInfoObj = $("#shipinfo_" + id);
            $("#txtConsignee").val(shipInfoObj.attr("consignee"));
            $("#province").html("");
            postOrder.loadProvince(shipInfoObj.attr("consignee"));
            $("#city").html("");
            postOrder.loadCity(shipInfoObj.attr("province"), shipInfoObj.attr("city"));
            $("#county").html("");
            postOrder.loadCounty(shipInfoObj.attr("city"), shipInfoObj.attr("county"));
            $("#postcode").val(shipInfoObj.attr("zipcode"));
            $("#txtAddress").val(shipInfoObj.attr("address"));
            $("#mobileinfo").val(shipInfoObj.attr("mobile"));
            $("#telinfo").val(shipInfoObj.attr("tel"));
            $("#eMailinfo").val(shipInfoObj.attr("email"));
        });

        $(".address-default").unbind("click").click(function () {
            var id = $(this).attr("index");
            $.post("SetAddressDefault", { addressID: id }, function (res) {
                if (res.State == 1) {
                    alert("设置成功");
                } else {
                    alert("操作失败");
                }
            });
        });
        
        //提交订单
        $("#postOrder").unbind("click").click(function () {
            //int addressID, int payMethod, int[] productIds, 
            //string intro, int isRequireInvoice, string invoiceTitle, int invoiceContent, int ctype,int cId
            var addressID = $("#receivedID").val();
            if (!addressID) {
                alert("获取收货地址错误！");
                return false;
            }
            var payMethod = $(":radio[name=pay][checked]").val();
            if (!payMethod) {
                alert("支付方式错误！");
                return false;
            }
            var productIds = "";
            $(".shopping-list tbody").each(function() {
                productIds += $(this).attr("index") + ",";
            });
            
            if (!productIds) {
                alert("商品列表错误！");
                return false;
            }

            var intro = $("#txtIntro").text();
            var isRequireInvoice = $(":radio[name=invoice][checked]").val();
            if (!isRequireInvoice) {
                isRequireInvoice = false;
            }
            var invoiceTitle="";
            var invoiceContent="0";
            if (isRequireInvoice == "1"&&$(":radio[name=invoiceType][checked]").val()=="1") {
                invoiceTitle = $("#txtInvoiceTitle").text();
            }
            invoiceContent = $(":radio[name=invoiceContent]").val();
            $.post("Add", { addressID: addressID, payMethod: payMethod, productIds: productIds, intro: intro, isRequireInvoice: isRequireInvoice, invoiceTitle: invoiceTitle, invoiceContent: invoiceContent?invoiceContent:0, ctype: 0, cId: 100, account: $("#txtAccount").val()?$("#txtAccount").val():0 }, function (res) {
                if (res.State == 1) {
                    window.location = '/Order/Success?oid=' + res.Data + "&payType=" + payMethod;
                } else {
                    alert(res.Message);
                }
            });
        });
        
        $("#btnUseAccount").unbind("click").click(function() {
            //确定使用账户余额抵扣，检查账户余额。
        });

        $("#payModify").unbind("click").click(function() {
            $(".alert-pay").css("display", "block");
            $("#pay_info").css("display", "none");
        });

        $(".savePayInfo").unbind("click").click(function () {
            var payMethod = $(":radio[name=pay][checked]").val();
            if (payMethod == "0") {
                $("#txtPayMethod").text("在线支付");
            }else if (payMethod == "1") {
                $("#txtPayMethod").text("货到付款（现金支付）");
            }
            else if (payMethod == "2") {
                $("#txtPayMethod").text("货到付款（pos机刷卡）");
            }
            $(".alert-pay").css("display", "none");
            $("#pay_info").css("display", "block");
        });
        
        // 修改发票信息
        $(".alter-invoice").unbind("click").click(function() {
            $(".invoice-fill").css("display", "block");
            $(".invoice-info").css("display", "none");
        });

        $(".invoiceRadio").unbind("change").change(function() {
            if ($(this).attr("checked")) {
                $(this).parents("p").addClass("selected").siblings("p").removeClass("selected");
            }
            
            if ($(this).val() == "1") {
                $(".invoice-cont").css("display", "inline-block");
            } else {
                $(".invoice-cont").css("display", "none");
            }
        });

        $(":radio[name=invoiceType]").unbind("change").change(function () {
            $("#invoiceError").css("display", "none");
            if ($(this).val() == "1") {
                $("#txtInvoiceTitle").css("visibility", "visible");
            } else {
                $("#txtInvoiceTitle").css("visibility", "hidden");
            }
        });

        $(".save-invoice").unbind("click").click(function() {
            var title;
            if ($(":radio[name=invoice][checked]").val() == "0") {
                title = "不开发票";
            } else if ($(":radio[name=invoice][checked]").val() == "1") {
                if ($(":radio[name=invoiceType][checked]").val() == "0") {
                    title = "发票抬头：个人";
                } else {
                    if (!$("#txtInvoiceTitle").val()) {
                        $("#invoiceError").css("display", "inline-block");
                        return false;
                    }
                    title = "发票抬头：" + $("#txtInvoiceTitle").val();
                }
            } else {
                title = "不开发票";
            }

            $("#txtInvoiceInfo").text(title);

            $(".invoice-fill").css("display", "none");
            $(".invoice-info").css("display", "block");
            $("#invoiceError").css("display", "none");
        });
    },

    loadProvince: function (sltIndex) {
        $("#province").html();
        $.post("../Utility/GetProvinces", function(res) {
            if (res.State == 1) {
                $(res.Data).each(function () {
                    //<option value="0" selected="selected">请选择</option>
                    if (sltIndex && parseInt(sltIndex) == this.ID) {
                        $("#province").append("<option selected ='selected' value='" + this.ID + "'>" + this.Name + "</option>");
                    } else {
                        $("#province").append("<option value='" + this.ID + "'>" + this.Name + "</option>");
                    }
                });
            }
        });
    },

    loadCity: function (provinceID, sltIndex) {
        $("#city").html("<option value='0' selected='selected'>请选择</option>");
        if (provinceID < 1) {
            return false;
        }

        $.post("../Utility/GetCities", { provinceID: provinceID }, function (res) {
            if (res.State == 1) {
                $(res.Data).each(function () {
                    if (sltIndex && parseInt(sltIndex) == this.ID) {
                        $("#city").append("<option selected ='selected' value='" + this.ID + "'>" + this.Name + "</option>");
                    } else {
                        $("#city").append("<option value='" + this.ID + "'>" + this.Name + "</option>");
                    }
                    //<option value="0" selected="selected">请选择</option>
                });
            }
        });
    },

    loadCounty: function (cityID, sltIndex) {
        $("#county").html("<option value='0' selected='selected'>请选择</option>");
        if (cityID < 1) {
            return false;
        }

        $.post("../Utility/GetCounties", { cityID: cityID }, function (res) {
            if (res.State == 1) {
                $(res.Data).each(function () {
                    //<option value="0" selected="selected">请选择</option>
                    if (sltIndex && parseInt(sltIndex) == this.ID) {
                        $("#county").append("<option selected ='selected' value='" + this.ID + "'>" + this.Name + "</option>");
                    } else {
                        $("#county").append("<option value='" + this.ID + "'>" + this.Name + "</option>");
                    }
                });
            }
        });
    },
    
    applyChange:function() {
        if ($(".defaultPath .radio[checked]")) {
            var receiveId = $(".radio[checked]").val();
            $("#receivedID").val(receiveId);
            var infoHidden = $(".radio[checked]").next("input");
            $("#received_name").text(infoHidden.attr("receiver"));
            $("#received_tel").text(infoHidden.attr("mobile"));
            $("#received_county").text(infoHidden.attr("county"));
            $("#received_addr").text(infoHidden.attr("address"));
            $("#user_info").removeClass("hidden").addClass("show");
            $("#modifyShip").removeClass("show").addClass("hidden");
        } else {
            alert("请选择地址再保存");
            return false;
        }
    },
    
    saveReceiveAddr:function() {
        
    }
};

//初始化订单提交页面
$(function() {
    postOrder.init();
    validate.init({ obj: ".validate", type: 1 });
})