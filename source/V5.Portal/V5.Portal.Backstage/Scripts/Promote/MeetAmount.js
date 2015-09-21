
//满件优惠的列表搜索数据
function SearchData() {
    return {
        AmountName: $("#SearchMeetAmountName").val(),
        AmountStatus: $("#SearchMeetAmountStatus").val(),
        startStartTime: $("#promoteStartStartTime").val(),
        startEndTime: $("#promoteStartEndTime").val(),
        endStartTime: $("#promoteEndStartTime").val(),
        endEndTime: $("#promoteEndEndTime").val()
    };
}

//搜索
function SearchMeetAmount() {
    var filter = new Array();
    var grid = $("#MeetAmountGrid").data("kendoGrid");
    grid.dataSource.filter(filter);
}

var meetAmount = {
    //功能描述：初始化
    Init: function(option) {
        if (!meetAmount.SetOption(option)) {
            alert("参数错误!");
            return;
        }
    },

    //功能描述：设置参数
    SetOption: function(option) {
        return true;
    },

    //功能描述：加载事件
    LoadEvent: function() {

    },
    
    //功能描述：获取站点地址
    GetSiteUrl: function () {
        var opt = {};
        opt.url = "/Home/GetSiteUrl";
        opt.dataType = "text";
        opt.async = false;
        opt.fn = function (data) {
            meetMoney.siteUrl = data || "";
        };
        //加载数据
        meetMoney.LoadData(opt);
    },

    // 功能描述:删除
    MeetAmountDelete: function (e) {
        var mess = confirm("确实要删除吗?");
        if (mess != "0") {
            $.post("/promote/RemoveMeetAmount", { meetAmountID: e.name }, function(data) {
                alert(data.Message);
                if (data.State == 1) {
                    var meetMoeyDataSource = $("#MeetAmountGrid").data("kendoGrid").dataSource;
                    $(meetMoeyDataSource.data()).each(function() {
                        if (parseInt(e.name) == this.ID) {
                            meetMoeyDataSource.remove(this);
                        }
                    });
                }
            });
        }
    },

    // 功能描述:详情
    MeetAmountDetail: function (e) {
        $.post("promote/MeetAmountDetail", { id: e.name }, function (data) {
            if (data) {
                meetAmount.DetailStruct(data);
            }
        });
    },

    // 功能描述:停止
    MeetAmountStop: function(e) {
        if ($(e).val() == "停止") {
            var mess = confirm("确实要停止吗?");
            if (mess != "0") {
                $.post("/promote/ChangesMeetAmountStatus", { meetAmountID: e.name, status: 3 }, function (data) {
                    alert(data.Message);
                    if (data.State == 1) {
                        meetAmount.Close();
                    }
                });
            }
        }
    },

    // 功能描述: 暂停、恢复
    MeetAmountSuspend: function(e) {
        if ($(e).val() == "暂停") {
            var mess = confirm("确实要暂停吗?");
            if (mess != "0") {
                $.post("/promote/ChangesMeetAmountStatus", { meetAmountID: e.name, status: 2 }, function(data) {
                    alert(data.Message);
                    if (data.State == 1) {
                        meetAmount.Close();
                    }
                });
            } else {
                return;
            }
        } else {
            var mess = confirm("确实要恢复吗?");
            if (mess != "0") {
                $.post("/promote/ChangesMeetAmountStatus", { meetAmountID: e.name, status: 1 }, function(data) {
                    alert(data.Message);
                    if (data.State == 1) {
                        meetAmount.Close();
                    }
                });
            }
        }
    },

    // 功能描述：显示满额优惠编辑页
    MeetAmountEdit: function (e,type) {
        var href = "/promote/MeetAmountEdit";
        $.ajax({
            type: "GET",
            url: href,
            data: null,
            datatype: "html",
            async: false,
            success: function(data) {
                if (data.State == -401) {
                    onSessionLost();
                } else if (data.State == -403) {
                    alert("对不起，您无此操作权限！");
                } else {
                    $('#defaultDiv').html(data);
                    if (!e) {
                        amountgrade.init();
                        var option = {};
                        option.container = "#container";
                        Choose.Init(option);
                    } else {
                        if (type == null) {
                            $("#AddBtn").css("display", "none");
                            $("#UpdateBtn").css("display", "block");
                        } else {
                            $("#AddBtn").css("display", "block");
                            $("#UpdateBtn").css("display", "none");
                        }
                        meetAmount.GetModifyData(e.name,type);
                    }
                }
            },
            error: function() {
                alert("处理失败!");
            }
        });
    },

    // 功能描述：关闭满额优惠编辑页
    Close: function() {
        $.get("/promote/MeetAmount", null, function (data) {
            if (data.State == -401) {
                onSessionLost();
            } else if (data.State == -403) {
                alert("对不起，您无此操作权限！");
            } else {
                $('#defaultDiv').html(data);
            }
        });
    },

    // 创建数组
    CreateArray: function(len, def) {
        def = def || "";
        var str = "";
        for (var i = 0; i < len; i++) {
            str += def;
        }
        return str.split('');
    },

    //获取满额优惠活动的商品
    GetScope: function(pro) {
        var len = 3908; // 获取商品最大ID
        $.ajax({
            type: "post",
            url: "/Product/GetProductCount",
            async: false,
            success: function(data) {
                len = data;
            }
        });

        var rights = meetAmount.CreateArray(len, "0");
        for (var i = 0; i < pro.length; i++) {
            var position = pro[i];
            rights[position] = "1";
        }
        var user_rights = rights.join('');
        return user_rights;
    },


    // 功能描述：添加满件优惠促销活动
    AddMeetAmount: function() {
        var promoteName = $("#promoteMeetAmountName").val();
        var startTime = $("#promoteMeetAmountStartTime").val();
        var endTime = $("#promoteMeetAmountEndTime").val();
        var description = $("#promoteAmountDescription").val(); //活动备注
        var isMoblieVerify = $("#mobileVerify").is(":checked");
        var isNewUser = $("#newUser").is(":checked");
        var isUseCoupon = $("#useCoupon").is(":checked");

        if (promoteName == "") {
            alert("促销活动名称不能为空！");
            amountgrade.focus("#promoteMeetAmountName");
            return;
        }
        if (startTime == "") {
            alert("促销活动开始时间不能为空！");
            amountgrade.focus("#promoteMeetAmountStartTime");
            return;
        }
        if (endTime == "") {
            alert("促销活动结束时间不能为空！");
            amountgrade.focus("#promoteMeetAmountEndTime");
            return;
        }

        var json = {};
        var grades = amountgrade.get();
        if (grades == null) {
            return;
        } else {
            for (var i in grades.item) {
                json["contents[" + i + "]" + ".MeetAmount"] = (grades.item[i].MeetAmount || "");
                json["contents[" + i + "]" + ".IsDiscount"] = (grades.item[i].IsDiscount || "");
                json["contents[" + i + "]" + ".Discount"] = (grades.item[i].Discount || "");
                json["contents[" + i + "]" + ".IsDiscount"] = (grades.item[i].IsDiscount ? "true" : "false");
                json["contents[" + i + "]" + ".IsGiveGift"] = (grades.item[i].IsGiveGift ? "true" : "false");
                json["contents[" + i + "]" + ".IsNoPostage"] = (grades.item[i].IsNoPostage ? "true" : "false");
                json["contents[" + i + "]" + ".ProductID"] = (grades.item[i].GiftID || "");
            }
        }

        json["Name"] = promoteName;
        json["StartTime"] = startTime;
        json["EndTime"] = endTime;
        json["Description"] = description;
        json["IsMobileValidate"] = isMoblieVerify;
        json["IsNewUser"] = isNewUser;
        json["IsUseCoupon"] = isUseCoupon;
        var scope = Choose.GetSelect();
        if (!scope || scope.length <= 0) {
            alert("请选择参与活动商品！");
            return;
        }
        json["meetAmountScopeModel.Scope"] = meetAmount.GetScope(scope);
        
        $.ajax({
            type: "post",
            url: "/promote/AddMeetAmount",
            data: json,
            async: false,
            success: function(data) {
                alert(data.Message);
                if (data.State == 1) {
                    meetAmount.Close();
                }
            }
        });
    },

    // 功能描述：获取需要修改的数据
    GetModifyData: function (id, reform) {
        $.ajax({
            type: "GET",
            url: "Promote/ModifyMeetAmountShow",
            data: { meetAmountID: id },
            datatype: "html",
            async: false,
            success: function(data) {
                if (data.State == -401) {
                    onSessionLost();
                } else if (data.State == -403) {
                    alert('对不起，您无此操作权限！若需要操作，请与管理员联系。');
                } else {
                    $("#promoteMeetAmountName").val(data.Name);
                    $("#promoteAmountDescription").val(data.Description);
                    if (reform == null) {
                        $("#promoteMeetAmountID").val(data.ID);
                        $("#promoteMeetAmountStartTime").data("kendoDateTimePicker").value(data.StartTime);
                        $("#promoteMeetAmountEndTime").data("kendoDateTimePicker").value(data.EndTime);
                    }

                    var option = {};
                    option.container = "#container";
                    option.selected = Choose.Convert(data.MeetAmountScopeModel.Scope);
                    Choose.Init(option);
                    
                    $("#mobileVerify").attr("checked", data.IsMobileValidate);
                    $("#newUser").attr("checked", data.IsNewUser);
                    $("#useCoupon").attr("checked", data.IsUseCoupon);
                    amountgrade.init(data.MeetAmountRuleModels);
                }
            },
            error: function() {
                alert("处理失败!");
            }
        });
    },
    
    // 功能描述：修改满件优惠促销活动
    ModifyMeetAmount: function() {
        var promoteID = $("#promoteMeetAmountID").val();
        var promoteName = $("#promoteMeetAmountName").val();
        var startTime = $("#promoteMeetAmountStartTime").val();
        var endTime = $("#promoteMeetAmountEndTime").val();
        var description = $("#promoteAmountDescription").val(); //活动备注
        var isMoblieVerify = $("#mobileVerify").is(":checked");
        var isNewUser = $("#newUser").is(":checked");
        var isUseCoupon = $("#useCoupon").is(":checked");

        if (promoteName == "") {
            alert("促销活动名称不能为空！");
            amountgrade.focus("#promoteMeetAmountName");
            return;
        }
        if (startTime == "") {
            alert("促销活动开始时间不能为空！");
            amountgrade.focus("#promoteMeetAmountStartTime");
            return;
        }
        if (endTime == "") {
            alert("促销活动结束时间不能为空！");
            amountgrade.focus("#promoteMeetAmountEndTime");
            return;
        }

        var json = {};
        var grades = amountgrade.get();
        if (grades == null) {
            return;
        } else {
            for (var i in grades.item) {
                json["contents[" + i + "]" + ".ID"] = (grades.item[i].ID || "");
                json["contents[" + i + "]" + ".PromoteMeetAmountID"] = (grades.item[i].PromoteMeetAmountID || "");
                json["contents[" + i + "]" + ".MeetAmount"] = (grades.item[i].MeetAmount || "");
                json["contents[" + i + "]" + ".IsDiscount"] = (grades.item[i].IsDiscount || "");
                json["contents[" + i + "]" + ".Discount"] = (grades.item[i].Discount || "");
                json["contents[" + i + "]" + ".IsDiscount"] = (grades.item[i].IsDiscount ? "true" : "false");
                json["contents[" + i + "]" + ".IsGiveGift"] = (grades.item[i].IsGiveGift ? "true" : "false");
                json["contents[" + i + "]" + ".IsNoPostage"] = (grades.item[i].IsNoPostage ? "true" : "false");
                json["contents[" + i + "]" + ".ProductID"] = (grades.item[i].GiftID || "");
            }
        }

        json["ID"] = promoteID;
        json["Name"] = promoteName;
        json["StartTime"] = startTime;
        json["EndTime"] = endTime;
        json["Description"] = description;
        json["IsMobileValidate"] = isMoblieVerify;
        json["IsNewUser"] = isNewUser;
        json["IsUseCoupon"] = isUseCoupon;
        var scope = Choose.GetSelect();
        if (!scope || scope.length <= 0) {
            alert("请选择参与活动商品！");
            return;
        }
        json["meetAmountScopeModel.Scope"] = meetAmount.GetScope(scope);
        for (var j in grades.removeObj) {
            json["removeRuleId[" + j + "]"] = (grades.removeObj[j] || "");
        }
        
        $.ajax({
            type: "post",
            url: "/promote/ModifyMeetAmount",
            data: json,
            async: false,
            success: function(data) {
                alert(data.Message);
                if (data.State == 1) {
                    meetAmount.Close();
                }
            }
        });
    },
    
    // 详细页结构
    DetailStruct: function (data) {
        var html = "";
        html += "<div class=\"member-box\">";
        html += "<h3>促销基本信息 </h3>";
        html += "<div class=\"member-detail\">";
        html += "<table width=\"1020\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">";
        html += "<tr>";
        html += "<td width=\"89\" height=\"5\" align=\"right\">活动名称：</td>";
        html += "<td width=\"174\" align=\"left\">" + data.Name + "</td>";
        html += "<td width=\"108\" align=\"right\">使用优惠券:" + data.IsUseCoupon + "</td>";
        html += "</tr>";
        html += "<tr>";
        html += "<td width=\"89\" height=\"5\" align=\"right\">活动时间：</td>";
        html += "<td width=\"174\" align=\"left\">" + data.StartTime + "</td>";
        html += "<td width=\"174\" align=\"right\">" + data.EndTime + "</td>";
        html += "</tr>";
        html += "<tr>";
        html += "<td width=\"89\" height=\"5\" align=\"right\">活动备注：</td>";
        html += "<td width=\"174\" align=\"left\">" + (data.Description||"") + "</td>";
        html += "<td width=\"108\" align=\"right\"></td>";
        html += "</tr>";
        html += "</table>";
        html += "</div>";
        html += "</div>";
        html += "<table>";
        html += "    <thead><tr><td class=\"p_name\">商品信息</td></tr></thead>";
        html += "</table>";
        html += "<div class=\"choose_item_body_inner\">";
        html += "    <table border=\"1\"><tbody id=\"item_list_selected\" border=\"1\" width=\"200\" cellpadding=\"0\" cellspacing=\"0\"></tbody></table>";
        html += "    <table id=\"item_list_promote\"></table>";
        html += "</div>";

        $('#defaultDiv').html(html);
        meetAmount.DetailProData(data.MeetAmountRuleModels, data.MeetAmountScopeModel.Scope);

    },

    // 详细页活动商品数据
    DetailProData: function (rules, scope) {
        var promote = rules;
        var condition = Choose.Convert(scope).join(",");
        var opt = {};
        opt.url = "/Product/ChooseProductOnSale";
        opt.data = { condition: condition };
        opt.dataType = "json";
        opt.fn = function (result) {
            if (!result) return;
            var data = result.data;
            if (!data || data == null) return;
            var html = "";
            for (var i = 0; i < data.length; i++) {
                var src = data[i].Path || "";
                var name = data[i].Name || "";
                var id = data[i].ID;
                html += "<tr name=\"" + name + "\">";
                html += "<td class=\"p_name\" ><img src=\"" + src + "\" alt=\"\" /><a href=\" " + meetAmount.siteUrl + "/Product/Item-id-" + id + ".htm\" title=\"" + name + "\" target=\"_blank\" >" + name + "</a></td>";
                html += "</tr>";
            }
            $("#item_list_selected").html(html);
            meetAmount.DetailPromote(promote);
        };
        //加载数据
        Choose.LoadData(opt);
    },

    // 详情页促销信息
    DetailPromote: function (data) {
        var html = "";
        for (var i = 0; i < data.length; i++) {
            html += "<tr><td>满足" + data[i].MeetAmount + "件。</td><td><td>";
            if (data[i].IsDecreaseCash) {
                html += "打" + data[i].Discount + "折；";
            }
            if (data[i].IsGiveGift) {
                html += "送1瓶 <a href=\" " + meetAmount.siteUrl + "/Product/Item-id-" + data[i].ProductID + ".htm\" title=\"" + data[i].ProductName + "\" target=\"_blank\" >" + data[i].ProductName + "</a>";
            }
        }
        $("#item_list_promote").html(html);
    }
};

/************* 满件优惠添加层级 *******************/
//grade控件
var amountgrade = {
    limit: 5,
    removeObj: Array,
    seed: 0,
    focus: function(obj) {
        setTimeout(function() {
            $(obj).focus();
        }, 0);
    },

    init: function(data) {
        amountgrade.removeObj = [];
        if (data && data.length > 0) {
            for (var i = 0; i < data.length; i++) {
                var id = amountgrade.seed++;
                $("#J_MultiGrade").append(amountgrade.create(id));
                amountgrade.edit(data[i], id);
                amountgrade.event(id);
            }
        } else {
            var count = $("#J_MultiGrade").find(".J_SectionGrade").length;
            if (count >= amountgrade.limit) {
                alert("超出个数限制,不允许添加");
                return;
            }
            var id = amountgrade.seed++;
            $("#J_MultiGrade").append(amountgrade.create(id));
            amountgrade.event(id);
        }
    },

    create: function(id) {
        var html = "";
        html += "<div class=\"J_SectionGrade section-grade " + (id == 1 ? "first" : "") + (id % 2 == 0 ? "even" : "") + "\" id=\"J_SectionGrade_" + id + "\">";
        html += "<ul class=\"form-elem J_PromoSection\">";
        html += "<li class=\"J_Required\">";
        html += "<span class=\"label-like\">优惠条件：</span>买家消费满";
        html += "<input type=\"text\" id=\"startFee_" + id + "\" class=\"input-text input-cash J_GradeCond\"> 件 ";
        html += "</li>";
        html += "<li><span class=\"label-like\">优惠内容：</span>";
        html += "    <p class=\"J_Discount\">";
        html += "        <input type=\"checkbox\" id=\"hasDiscountCash_" + id + "\" >";
        html += "        <label>打折</label>";
        html += "        <span class=\"hidden\"><input type=\"text\" id=\"discountCash_" + id + "\" class=\"input-text input-cash\">折</span>";
        html += "    </p>";
        html += "    <p class=\"J_Postfee\">";
        html += "        <input type=\"checkbox\" id=\"hasFreePostage_" + id + "\">";
        html += "        <label>免邮</label>";
        html += "    </p>";
        html += "    <div class=\"J_Present\">";
        html += "        <input type=\"checkbox\" id=\"hasGift_" + id + "\">";
        html += "        <label>送<em>礼品</em></label>";
        html += "        <span class=\"hidden\"><span id=\"giftName_" + id + "\" style=\"display:inline-block; padding:0px 5px;\" ></span><a class=\"J_AddGift\" href=\"###\" id=\"J_AddGift_" + id + "\">选择礼品</a></span>";
        html += "    </div>";
        html += "</li>";
        html += "</ul>";
        html += "<a href=\"###\" class=\"J_Del ui-btn-del\" id=\"delGrade_" + id + "\">删除</a>";
        html += "</div>";

        return html;
    },

    edit: function(data, id) {
        if (!data) return;

        //属性
        $("#J_SectionGrade_" + id).attr("J_SectionGrade_ID", data.ID || "");
        $("#J_SectionGrade_" + id).attr("J_SectionGrade_PromoteMeetAmountID", data.PromoteMeetAmountID || "");

        //满件
        $("#startFee_" + id).val(data.MeetAmount || "");

        //打折
        if (data.IsDiscount == true) {
            $("#hasDiscountCash_" + id).attr("checked", true);
            $("#discountCash_" + id).val(data.Discount);
            $("#discountCash_" + id).parent().removeClass("hidden");
        }

        //送礼物
        if (data.IsGiveGift == true) {
            $("#hasGift_" + id).attr("checked", true);
            $("#giftName_" + id).attr("GiftID", data.ProductID);
            $("#giftName_" + id).html(data.ProductName);
            $("#giftName_" + id).parent().removeClass("hidden");
        }

        //包邮
        if (data.IsNoPostage == true) {
            $("#hasFreePostage_" + id).attr("checked", true);
        }
    },

    event: function(id) {
        //打折
        $("#hasDiscountCash_" + id).click(function() {
            if ($(this).is(":checked")) {
                $("#discountCash_" + id).parent().removeClass("hidden");
            } else {
                $("#discountCash_" + id).parent().addClass("hidden");
            }
        });

        $("#discountCash_" + id).blur(function() {
            var discountCash = $(this).val();
            if (discountCash != "" && !isNaN(discountCash)) {
                var myDiscountCash = Number(discountCash);
                if (myDiscountCash > 10 || myDiscountCash < 0) {
                    alert("折扣范围须在0-10范围内（不包括0和10）");
                    $(this).val("");
                    amountgrade.focus("#discountCash_" + id);
                }
            }
        });

        //送礼物
        $("#hasGift_" + id).click(function() {
            if ($(this).is(":checked")) {
                $("#giftName_" + id).parent().removeClass("hidden");
            } else {
                $("#giftName_" + id).parent().addClass("hidden");
            }
        });

        //删除
        $("#delGrade_" + id).click(function() {
            if ($("#J_MultiGrade").find(".J_SectionGrade").length <= 1) return;
            if (confirm("是否删除当前层级")) {
                //获取对象ID
                var J_SectionGrade_ID = $("#J_SectionGrade_" + id).attr("J_SectionGrade_ID") || "";
                if (J_SectionGrade_ID != "") {
                    amountgrade.removeObj.push(J_SectionGrade_ID);
                }
                $(this).parent().remove();
            }
        });

        //选择礼物
        $("#J_AddGift_" + id).click(function () {
            amountgrade.giftName = $("#giftName_" + id);
            OpenAmountProductWindow();
        });
    },

    dealGift: function (option) {
        //{ "productID": productID, "productName": productName, "goujiuPrice": goujiuPrice, "imageUrl": imageUrl }
        if (!option) return;

        amountgrade.giftName.html(option.productName || "");
        amountgrade.giftName.attr("GiftID", option.productID || "");
        amountgrade.giftName.attr("GoujiuPrice", option.goujiuPrice || "");
        amountgrade.giftName.attr("imageUrl", option.imageUrl || "");
    },

    get: function() {
        var section_grade = $("#J_MultiGrade").find(".J_SectionGrade");
        if (section_grade.length == 0) {
            return;
        }

        var result = [];
        for (var i = 0; i < section_grade.length; i++) {
            var id = (section_grade[i].id || "").replace("J_SectionGrade_", "");
            var temp = {
                ID: null,
                PromoteMeetAmountID: null,  //活动编号
                MeetAmount: null, //满足件数
                IsDiscount: false, //是否打折
                Discount: null, //折扣                        
                IsGiveGift: false, //是否送礼物
                GiftID: null, //商品编号
                GiftName: null, //商品名称
                GoujiuPrice: null,       //礼物价格
                imageUrl: null,  //图片地址
                IsNoPostage: false //是否包邮
            };

            temp.ID = $("#J_SectionGrade_" + id).attr("J_SectionGrade_ID") || "";
            temp.PromoteMeetAmountID  = $("#J_SectionGrade_" + id).attr("J_SectionGrade_PromoteMeetAmountID") || "";

            //满件
            var startFee = $("#startFee_" + id).val();
            startFee = startFee || "";
            if (startFee == "" || isNaN(startFee)) {
                alert("请输入满多少件!");
                $("#startFee_" + id).val("");
                amountgrade.focus("#startFee_" + id);
                return;
            } else {
                temp.MeetAmount = startFee;
            }

            //打折
            if ($("#hasDiscountCash_" + id).is(":checked")) {
                var val = $("#discountCash_" + id).val() || "";
                if (val == "" || isNaN(val)) {
                    alert("请输入折扣!");
                    $("#discountCash_" + id).val("");
                    amountgrade.focus("#discountCash_" + id);
                    return;
                }
                temp.IsDiscount = true;
                temp.Discount = Number(val);
            }

            //送礼物
            if ($("#hasGift_" + id).is(":checked")) {
                var val = $("#giftName_" + id).html() || "";
                if (val == "") {
                    alert("请选择礼物!");
                    $("#giftName_" + id).html("");
                    amountgrade.focus("#giftName_" + id);
                    return;
                }
                temp.IsGiveGift = true;
                temp.GiftID = $("#giftName_" + id).attr("GiftID");
                temp.goujiuPrice = $("#giftName_" + id).attr("goujiuPrice");
                temp.imageUrl = $("#giftName_" + id).attr("imageUrl");
            }

            //包邮
            if ($("#hasFreePostage_" + id).is(":checked")) {
                temp.IsNoPostage = true;
            }

            result.push(temp);
        }

        return { item: result, removeObj: amountgrade.removeObj || [] };
    }
};
/*************** 满件优惠添加层级 ******************/
//添加满件优惠促销层级
function AddMeetAmountTier() {
    amountgrade.init();
}

//打开商品选择界面
function OpenAmountProductWindow() {
    $("#choiceProductWindow").data("kendoWindow").open().center();
    $("#ConfirmProduct").click(function () {
        var dataItems = $("#ProductGrid").data("kendoGrid").dataSource.data();
        var productID;
        var productName;
        var goujiuPrice;
        var imageUrl;
        $("input[name=selectproduct]").each(function () {
            if ($(this)[0].checked) {
                productID = $(this).val();
            }
        });
        $(dataItems).each(function () {
            if (this.ID == productID) {
                productID = this.ID;
                productName = this.Name;
                goujiuPrice = this.GoujiuPrice;
                imageUrl = this.Name;
            }
        });

        $("#choiceProductWindow").data("kendoWindow").close();
        amountgrade.dealGift({ "productID": productID, "productName": productName, "goujiuPrice": goujiuPrice, "imageUrl": imageUrl });
    });
}
