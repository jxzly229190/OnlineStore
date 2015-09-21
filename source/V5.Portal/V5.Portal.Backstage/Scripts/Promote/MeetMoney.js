//满额优惠列表搜索条件
function SearchData() {
    return {
        moneyName: $("#SearchMeetMoneyName").val(),
        moneyStatus: $("#SearchMeetMoneyStatus").val(),
        startStartTime: $("#promoteStartStartTime").val(),
        startEndTime: $("#promoteStartEndTime").val(),
        endStartTime: $("#promoteEndStartTime").val(),
        endEndTime: $("#promoteEndEndTime").val()
    };
}

//搜索
function SearchMeetMoney() {
    var filter = new Array();
    var grid = $("#MeetMoneyGrid").data("kendoGrid");
    grid.dataSource.filter(filter);
}

var meetMoney = {

    siteUrl: String,
    
    //功能描述：初始化
    Init: function () {       
        //获取站点地址
        meetMoney.GetSiteUrl();
    },

    //功能描述：设置参数
    SetOption: function (option) {
    },

    //功能描述：加载事件
    LoadEvent: function () {

    },

    //功能描述：加载数据
    LoadData: function (opt) {
        if (!opt.url || opt.url == "") return;

        opt.type = opt.type || "POST";
        opt.dataType = opt.dataType || "json";
        opt.async = opt.async == false ? false : true;
        $.ajax({
            type: opt.type,
            url: opt.url,
            data: opt.data,
            dataType: opt.dataType,
            async: opt.async,
            success: function (result) {
                if ($.isFunction(opt.fn)) {
                    opt.fn(result);
                }
            },
            error: function () {
                alert("处理失败!");
            }
        });
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

    //功能描述：删除
    MeetMoneyDelete: function (e) {
        var mess = confirm("确实要删除吗?");
        if (mess != "0") {
            $.post("/promote/RemoveMeetMoney", { meetMoneyID: e.name }, function (data) {
                alert(data.Message);
                if (data.State == 1) {
                    var meetMoeyDataSource = $("#MeetMoneyGrid").data("kendoGrid").dataSource;
                    $(meetMoeyDataSource.data()).each(function() {
                        if (parseInt(e.name) == this.ID) {
                            meetMoeyDataSource.remove(this);
                        }
                    });
                }
            });
        }
    },

    // 功能描述：停止
    MeetMoneyStop: function (e) {
        if ($(e).val() == "停止") {
            var mess = confirm("确实要停止吗?");
            if (mess != "0") {
                $.post("/promote/ChangesMeetMoneyStatus", { meetMoneyID: e.name, status: 3 }, function (data) {
                    alert(data.Message);
                    if (data.State == 1) {
                        meetMoney.Close();
                    }
                });
            }
        }
    },

    // 功能描述：暂停、恢复
    MeetMoneySuspend: function (e) {
        if ($(e).val() == "暂停") {
            var mess = confirm("确实要暂停吗?");
            if (mess != "0") {
                $.post("/promote/ChangesMeetMoneyStatus", { meetMoneyID: e.name, status: 2 }, function (data) {
                    alert(data.Message);
                    if (data.State == 1) {
                        meetMoney.Close();
                    }
                });
            } else {
                return;
            }
        } else {
            var mess = confirm("确实要恢复吗?");
            if (mess != "0") {
                $.post("/promote/ChangesMeetMoneyStatus", { meetMoneyID: e.name, status: 1 }, function (data) {
                    alert(data.Message);
                    if (data.State == 1) {
                        meetMoney.Close();
                    }
                });
            }
        }
    },

    // 功能描述：显示满额优惠编辑页
    MeetMoneyEdit: function (e,type) {
        var href = "/promote/MeetMoneyEdit";
        $.ajax({
            type: "GET",
            url: href,
            data: null,
            datatype: "html",
            success: function (data) {
                if (data.State == -401) {
                    onSessionLost();
                } else if (data.State == -403) {
                    alert("对不起，您无此操作权限！");
                } else {
                    $('#defaultDiv').html(data);
                    if (!e) {
                        grade.init("0");
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
                        meetMoney.GetModifyData(e.name,type);
                    }
                }
            },
            error: function () {
                alert("处理失败!");
            }
        });
    },

    // 功能描述：关闭满额优惠编辑页
    Close: function () {
        $.get("/promote/MeetMoney", null, function (data) {
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
    CreateArray: function (len, def) {
        def = def || "";
        var str = "";
        for (var i = 0; i < len; i++) {
            str += def;
        }
        return str.split('');
    },

    //获取满额优惠活动的商品
    GetScope: function (pro) {
        var len = 3908; // 获取商品最大ID
        $.ajax({
            type: "post",
            url: "/Product/GetProductCount",
            async: false,
            success: function (data) {
                len = data;
            }
        });

        var rights = meetMoney.CreateArray(len, "0");
        for (var i = 0; i < pro.length; i++) {
            var position = pro[i];
            rights[position] = "1";
        }
        var user_rights = rights.join('');
        return user_rights;
    },

    // 功能描述：添加满额优惠活动
    AddMeetMoney: function () {
        var promoteName = $("#promoteMeetMoneyName").val();
        var startTime = $("#promoteMeetMoneyStartTime").val();
        var endTime = $("#promoteMeetMoneyEndTime").val();
        var description = $("#promoteDescription").val(); //活动备注
        var isMoblieVerify = $("#mobileVerify").is(":checked");
        var isNewUser = $("#newUser").is(":checked");
        var isUseCoupon = $("#useCoupon").is(":checked");

        if (promoteName == "") {
            alert("促销活动名称不能为空！");
            grade.focus("#promoteMeetMoneyName");
            return;
        }
        if (startTime == "") {
            alert("促销活动开始时间不能为空！");
            grade.focus("#promoteMeetMoneyStartTime");
            return;
        }
        if (endTime == "") {
            alert("促销活动结束时间不能为空！");
            grade.focus("#promoteMeetMoneyEndTime");
            return;
        }

        var json = {};
        var grades = grade.get();
        if (grades == null) {
            return;
        } else {
            for (var i in grades.item) {
                json["contents[" + i + "]" + ".IsNoCeiling"] = (grades.item[i].IsNoCeiling ? "true" : "false");
                json["contents[" + i + "]" + ".Description"] = (grades.item[i].Description || "");
                json["contents[" + i + "]" + ".MeetMoney"] = (grades.item[i].MeetMoney || "");
                json["contents[" + i + "]" + ".IsDecreaseCash"] = (grades.item[i].IsDecreaseCash ? "true" : "false");
                json["contents[" + i + "]" + ".IsGiveGift"] = (grades.item[i].IsGiveGift ? "true" : "false");
                json["contents[" + i + "]" + ".IsGiveIntegral"] = (grades.item[i].IsGiveIntegral ? "true" : "false");
                json["contents[" + i + "]" + ".IsNoPostage"] = (grades.item[i].IsNoPostage ? "true" : "false");
                json["contents[" + i + "]" + ".IsGiveCoupon"] = (grades.item[i].IsGiveCoupon ? "true" : "false");
                json["contents[" + i + "]" + ".DecreaseCash"] = (grades.item[i].DecreaseCash || "");
                json["contents[" + i + "]" + ".ProductID"] = (grades.item[i].ProductID || "");
                json["contents[" + i + "]" + ".Integral"] = (grades.item[i].Integral || "");
                json["contents[" + i + "]" + ".CouponType"] = (grades.item[i].CouponTypeID || "");
                json["contents[" + i + "]" + ".CouponID"] = (grades.item[i].CouponID || "");
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

        json["meetMoneyScopeModel.Scope"] = meetMoney.GetScope(scope);
        $.ajax({
            type: "post",
            url: "/promote/AddMeetMoney",
            data: json,
            async: false,
            success: function (msg) {
                alert(msg.Message);
                if (msg.State == 1) {
                    meetMoney.Close();
                }
            }
        });
    },

    // 功能描述：获取需要修改的数据
    GetModifyData: function (id, reform) {
        $.ajax({
            type: "GET",
            url: "Promote/ModifyMeetMoneyShow",
            data: { meetMoneyID: id },
            dataType: "json",
            success: function (data) {
                if (data.State == -401) {
                    onSessionLost();
                } else if (data.State == -403) {
                    alert('对不起，您无此操作权限！若需要操作，请与管理员联系。');
                } else {
                    $("#promoteMeetMoneyName").val(data.Name);
                    $("#promoteDescription").val(data.Description);
                    if (reform==null) {
                        $("#promoteMeetMoneyID").val(data.ID);
                        $("#promoteMeetMoneyStartTime").data("kendoDateTimePicker").value(data.StartTime);
                        $("#promoteMeetMoneyEndTime").data("kendoDateTimePicker").value(data.EndTime);
                    }
                    
                    var option = {};
                    option.container = "#container";
                    option.selected = Choose.Convert(data.MeetMoneyScope.Scope);
                    Choose.Init(option);

                    $("#mobileVerify").attr("checked", data.IsMobileValidate);
                    $("#newUser").attr("checked", data.IsNewUser);
                    $("#useCoupon").attr("checked", data.IsUseCoupon);
                    if (data.PromoteMeetMoneyRuleModelsList.length > 1) {
                        $("#multilevel")[0].checked = true;
                        $("#addPromoteTier").css("display", "block");

                    } else {
                        $("#general")[0].checked = true;
                    }
                    var type = $("input[type='radio'][name='preferentialWay']:checked").val();
                    grade.init(type, data.PromoteMeetMoneyRuleModelsList);
                }
            },
            error: function () {
                alert("处理失败??!");
            }
        });
    },

    // 功能描述： 修改满就送促销
    ModifyMeetMoney: function () {
        var promoteID = $("#promoteMeetMoneyID").val();
        var promoteName = $("#promoteMeetMoneyName").val();
        var startTime = $("#promoteMeetMoneyStartTime").val();
        var endTime = $("#promoteMeetMoneyEndTime").val();
        var description = $("#promoteDescription").val(); //活动备注
        var isMoblieVerify = $("#mobileVerify").is(":checked");
        var isNewUser = $("#newUser").is(":checked");
        var isUseCoupon = $("#useCoupon").is(":checked");

        if (promoteName == "") {
            alert("促销活动名称不能为空！");
            grade.focus("#promoteMeetMoneyName");
            return;
        }
        if (startTime == "") {
            alert("促销活动开始时间不能为空！");
            grade.focus("#promoteMeetMoneyStartTime");
            return;
        }
        if (endTime == "") {
            alert("促销活动结束时间不能为空！");
            grade.focus("#promoteMeetMoneyEndTime");
            return;
        }

        var json = {};
        var grades = grade.get();
        var i;
        if (grades == null) {
            return;
        } else {
            for (i in grades.item) {
                json["contents[" + i + "]" + ".ID"] = (grades.item[i].ID || "");
                json["contents[" + i + "]" + ".PromoteMeetMoneyID"] = (grades.item[i].PromoteMeetMoneyID || "");
                json["contents[" + i + "]" + ".IsNoCeiling"] = (grades.item[i].IsNoCeiling ? "true" : "false");
                json["contents[" + i + "]" + ".Description"] = (grades.item[i].Description || "");
                json["contents[" + i + "]" + ".MeetMoney"] = (grades.item[i].MeetMoney || "");
                json["contents[" + i + "]" + ".IsDecreaseCash"] = (grades.item[i].IsDecreaseCash ? "true" : "false");
                json["contents[" + i + "]" + ".IsGiveGift"] = (grades.item[i].IsGiveGift ? "true" : "false");
                json["contents[" + i + "]" + ".IsGiveIntegral"] = (grades.item[i].IsGiveIntegral ? "true" : "false");
                json["contents[" + i + "]" + ".IsNoPostage"] = (grades.item[i].IsNoPostage ? "true" : "false");
                json["contents[" + i + "]" + ".IsGiveCoupon"] = (grades.item[i].IsGiveCoupon ? "true" : "false");
                json["contents[" + i + "]" + ".DecreaseCash"] = (grades.item[i].DecreaseCash || "");
                json["contents[" + i + "]" + ".ProductID"] = (grades.item[i].ProductID || "");
                json["contents[" + i + "]" + ".Integral"] = (grades.item[i].Integral || "");
                json["contents[" + i + "]" + ".CouponTypeID"] = (grades.item[i].CouponTypeID || "");
                json["contents[" + i + "]" + ".CouponID"] = (grades.item[i].CouponID || "");
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
        json["meetMoneyScopeModel.Scope"] = meetMoney.GetScope(scope); ;
        for (var j in grades.removeObj) {
            json["removeRuleId[" + j + "]"] = (grades.removeObj[j] || "");
        }
        $.ajax({
            type: "post",
            url: "/promote/ModifyMeetMoney",
            data: json,
            async: false,
            success: function (data) {
                alert(data.Message);
                if (data.State == 1) {
                    meetMoney.Close();
                }
            }
        });
    },

    // 功能描述：详情
    MeetMoneyDetail: function (e) {
        $.post("promote/MeetMoneyDetail", { id: e.name }, function (data) {
            if (data) {
                meetMoney.DetailStruct(data);
            }
        });
        /*
        // 打开单独的页面
        var href = "/promote/MeetMoneyDetail/" + e.name;
        window.open(href); */
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
        html += "<td width=\"174\" align=\"left\">"+data.Name+"</td>";
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
        meetMoney.DetailProData(data.PromoteMeetMoneyRuleModelsList, data.MeetMoneyScope.Scope);
        
    },
    
    // 详细页活动商品数据
    DetailProData: function (rules,scope) {
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
                html += "<td class=\"p_name\" ><img src=\"" + src + "\" alt=\"\" /><a href=\" " + meetMoney.siteUrl + "/Product/Item-id-" + id + ".htm\" title=\"" + name + "\" target=\"_blank\" >" + name + "</a></td>";
                html += "</tr>";
            }
            $("#item_list_selected").html(html);
            meetMoney.DetailPromote(promote);
        };

        //加载数据
        Choose.LoadData(opt);
    },
    
    // 详情页促销信息
    DetailPromote: function (data) {
        var html = "";
        for (var i = 0; i < data.length; i++) {
            html += "<tr><td>满足" + data[i].MeetMoney + "元。</td><td><td>";
            if (data[i].IsDecreaseCash) {
                html += "减" + data[i].DecreaseCash + "元；";
            }
            if (data[i].IsGiveGift) {
                html += "送1瓶 <a href=\" " + meetMoney.siteUrl + "/Product/Item-id-" + data[i].ProductID + ".htm\" title=\"" + data[i].ProductName + "\" target=\"_blank\" >" + data[i].ProductName + "</a>";
            }
            if (data[i].IsGiveIntegral) {
                html += "送 " + data[i].Integral + " 积分;";
            }
            if (data[i].IsGiveCoupon) {
                html += "送电子券" + data[i].CouponID + "</td></tr>";
            }
        }
        $("#item_list_promote").html(html);
    }
};

/**************添加促销层级 Start *************/
//grade控件
var grade = {
    limit: 5,
    removeObj: Array,
    seed: 0,
    focus: function (obj) {
        setTimeout(function () {
            $(obj).focus();
        }, 0);
    },

    init: function (type, data) {
        if (!type) return "参数错误";

        grade.removeObj = [];
        if (data && data.length > 0) {
            for (var i = 0; i < data.length; i++) {
                var id = grade.seed++;
                $("#J_MultiGrade").append(grade.create(type, id));
                grade.edit(data[i], id);
                grade.event(type, id);
            }
        } else {
            var count = $("#J_MultiGrade").find(".J_SectionGrade").length;
            switch (type) {
                case 0:
                    if (count >= 1) {
                        return;
                    }
                    break;
                case 1:
                    if (count >= grade.limit) {
                        alert("超出个数限制,不允许添加");
                        return;
                    }
                    break;
            }
            var id = grade.seed++;
            $("#J_MultiGrade").append(grade.create(type, id));
            grade.event(type, id);
        }
    },

    create: function (type, id) {
        var html = "";
        html += "<div class=\"J_SectionGrade section-grade " + (id == 1 ? "first" : "") + (id % 2 == 0 ? "even" : "") + "\" id=\"J_SectionGrade_" + id + "\">";
        html += "<ul class=\"form-elem J_PromoSection\">";
        html += "<li class=\"J_Required\">";
        html += "<span class=\"label-like\">优惠条件：</span>买家消费满";
        html += "<input type=\"text\" id=\"startFee_" + id + "\" class=\"input-text input-cash J_GradeCond\"> 元 ";
        if (type == "0") {
            html += "<span class=\"field-elem\"><input type=\"checkbox\" id=\"noLimit_" + id + "\"><label>上不封顶</label></span>";
        }
        html += "</li>";
        html += "<li><span class=\"label-like\">优惠内容：</span>";
        html += "    <p class=\"J_Discount\">";
        html += "        <input type=\"checkbox\" id=\"hasDiscountCash_" + id + "\" >";
        html += "        <label>减<em>现金</em></label>";
        html += "        <span class=\"hidden\"><input type=\"text\" id=\"discountCash_" + id + "\" class=\"input-text input-cash\">元</span>";
        html += "    </p>";
        html += "    <div class=\"J_Present\">";
        html += "        <input type=\"checkbox\" id=\"hasGift_" + id + "\">";
        html += "        <label>送<em>礼品</em></label>";
        html += "        <span class=\"hidden\"><span id=\"giftName_" + id + "\" style=\"display:inline-block; padding:0px 5px;\" ></span><a class=\"J_AddGift\" href=\"###\" id=\"J_AddGift_" + id + "\">选择礼品</a></span>";
        html += "    </div>";
        html += "    <p class=\"J_Point\">";
        html += "        <input type=\"checkbox\" id=\"hasPresentPoint_" + id + "\" >";
        html += "        <label>送<em>积分</em></label>";
        html += "        <span class=\"hidden\"><input type=\"text\" id=\"presentPoint_" + id + "\" class=\"input-text input-num\">积分</span>";
        html += "    </p>";
        html += "    <p class=\"J_Postfee\">";
        html += "        <input type=\"checkbox\" id=\"hasFreePostage_" + id + "\">";
        html += "        <label>免邮</label>";
        html += "    </p>";
        html += "    <p class=\"J_Coupon\">";
        html += "        <input type=\"checkbox\" id=\"hasShopBonus_" + id + "\">";
        html += "        <label>送<em></em>优惠券</label>";
        html += "        <span class=\"hidden\"><span id=\"shopBonus_" + id + "\" style=\"display:inline-block; padding:0px 5px;\" ></span><a class=\"J_AddShopBonus\" href=\"###\" id=\"J_AddShopBonus_" + id + "\">选择优惠券</a></span>";
        html += "    </p>";
        html += "</li>";
        html += "</ul>";
        if (type == "1") {
            html += "<a href=\"###\" class=\"J_Del ui-btn-del\" id=\"delGrade_" + id + "\">删除</a>";
        }
        html += "</div>";

        return html;
    },

    edit: function (data, id) {
        if (!data) return;

        //属性
        $("#J_SectionGrade_" + id).attr("J_SectionGrade_ID", data.ID || "");
        $("#J_SectionGrade_" + id).attr("J_SectionGrade_PromoteMeetMoneyID", data.PromoteMeetMoneyID || "");

        //是否上不封顶
        if (data.IsNoCeiling == true) {
            $("#noLimit_" + id).attr("checked", true);
        }

        //满金额
        $("#startFee_" + id).val(data.MeetMoney || "");

        //减现金
        if (data.IsDecreaseCash == true) {
            $("#hasDiscountCash_" + id).attr("checked", true);
            $("#discountCash_" + id).val(data.DecreaseCash);
            $("#discountCash_" + id).parent().removeClass("hidden");
        }

        //送礼物
        if (data.IsGiveGift == true) {
            $("#hasGift_" + id).attr("checked", true);
            $("#giftName_" + id).html(data.ProductName);
            $("#giftName_" + id).attr("ProductID", data.ProductID);
            $("#giftName_" + id).parent().removeClass("hidden");
        }

        //送积分
        if (data.IsGiveIntegral == true) {
            $("#hasPresentPoint_" + id).attr("checked", true);
            $("#presentPoint_" + id).val(data.Integral);
            $("#presentPoint_" + id).parent().removeClass("hidden");
        }

        //包邮
        if (data.IsNoPostage == true) {
            $("#hasFreePostage_" + id).attr("checked", true);
        }

        //送优惠券
        if (data.IsGiveCoupon == true) {
            $("#hasShopBonus_" + id).attr("checked", true);
            if (data.CouponTypeID == "1") {
                $("#shopBonus_" + id).html(data.DecreaseName);
            } else {
                $("#shopBonus_" + id).html(data.CashName);
            }
            $("#shopBonus_" + id).attr("CouponTypeID", data.CouponTypeID);
            $("#shopBonus_" + id).attr("CouponID", data.CouponID);
            $("#shopBonus_" + id).parent().removeClass("hidden");
        }
    },

    event: function (type, id) {
        //减现金
        $("#hasDiscountCash_" + id).click(function () {
            if ($(this).is(":checked")) {
                $("#discountCash_" + id).parent().removeClass("hidden");
            } else {
                $("#discountCash_" + id).parent().addClass("hidden");
            }
        });

        //送礼物
        $("#hasGift_" + id).click(function () {
            if ($(this).is(":checked")) {
                $("#giftName_" + id).parent().removeClass("hidden");
            } else {
                $("#giftName_" + id).parent().addClass("hidden");
            }
        });

        //送积分
        $("#hasPresentPoint_" + id).click(function () {
            if ($(this).is(":checked")) {
                $("#presentPoint_" + id).parent().removeClass("hidden");
            } else {
                $("#presentPoint_" + id).parent().addClass("hidden");
            }
        });

        //送优惠券
        $("#hasShopBonus_" + id).click(function () {
            if ($(this).is(":checked")) {
                $("#shopBonus_" + id).parent().removeClass("hidden");
            } else {
                $("#shopBonus_" + id).parent().addClass("hidden");
            }
        });

        //删除
        $("#delGrade_" + id).click(function () {
            if ($("#J_MultiGrade").find(".J_SectionGrade").length <= 2) return;
            if (confirm("是否删除当前层级")) {
                //属性
                var J_SectionGrade_ID = $("#J_SectionGrade_" + id).attr("J_SectionGrade_ID") || "";
                if (J_SectionGrade_ID != "") {
                    grade.removeObj.push(J_SectionGrade_ID);
                }
                $(this).parent().remove();
            }
        });

        //选择礼物
        $("#J_AddGift_" + id).click(function () {
            grade.giftName = $("#giftName_" + id);
            OpenProductWindow();
        });

        //选择消费券
        $("#J_AddShopBonus_" + id).click(function () {
            grade.shopBonus = $("#shopBonus_" + id);
            OpenCouponWindow();
        });
    },

    dealGift: function (option) {
        //{ "productID": productID, "productName": productName, "goujiuPrice": goujiuPrice, "imageUrl": imageUrl }
        if (!option) return;

        grade.giftName.html(option.productName || "");
        grade.giftName.attr("ProductID", option.productID || "");
        grade.giftName.attr("GoujiuPrice", option.goujiuPrice || "");
        grade.giftName.attr("imageUrl", option.imageUrl || "");
    },

    dealShopBonus: function (option) {
        //{ "couponType": couponType, "couponID": couponID, "couponName": couponName }
        if (!option) return;

        grade.shopBonus.html(option.couponName || "");
        grade.shopBonus.attr("couponType", option.couponType || "");
        grade.shopBonus.attr("couponID", option.couponID || "");
    },

    get: function () {
        var section_grade = $("#J_MultiGrade").find(".J_SectionGrade");
        if (section_grade.length == 0) {
            return;
        }

        var result = [];
        for (var i = 0; i < section_grade.length; i++) {
            var id = (section_grade[i].id || "").replace("J_SectionGrade_", "");
            var temp = {
                ID: null,
                PromoteMeetMoneyID: null,
                IsNoCeiling: false, 	//是否上不封顶
                Description: null, 	//活动备注
                MeetMoney: null,     	//满足金额
                IsDecreaseCash: false, //是否减现金
                IsGiveGift: false, 	//是否送礼物
                IsGiveIntegral: false, //是否送积分
                IsNoPostage: false, 	//是否免邮
                IsGiveCoupon: false, 	//是否送优惠券
                DecreaseCash: null, //减少现金金额
                ProductID: null, 		//礼物的编号
                GoujiuPrice: null,       //礼物价格
                Integral: null, 	//赠送积分
                CouponTypeID: null, 	//优惠券类型编号
                CouponID: null		//优惠券编号
            };

            temp.ID = $("#J_SectionGrade_" + id).attr("J_SectionGrade_ID") || "";
            temp.PromoteMeetMoneyID = $("#J_SectionGrade_" + id).attr("J_SectionGrade_PromoteMeetMoneyID") || "";

            var noLimit = $("#noLimit_" + id).is(":checked");
            temp.IsNoCeiling = noLimit == true ? true : false;

            //满金额
            var startFee = $("#startFee_" + id).val();
            startFee = startFee || "";
            if (startFee == "" || isNaN(startFee)) {
                alert("请输入满多少金额");
                $("#startFee_" + id).val("");
                grade.focus("#startFee_" + id);
                return;
            } else {
                temp.MeetMoney = startFee;
            }

            //减现金
            if ($("#hasDiscountCash_" + id).is(":checked")) {
                var val = $("#discountCash_" + id).val() || "";
                if (val == "" || isNaN(val)) {
                    alert("请输入减现金!");
                    $("#discountCash_" + id).val("");
                    grade.focus("#discountCash_" + id);
                    return;
                }
                temp.DecreaseCash = val;
                temp.IsDecreaseCash = true;
            }

            //送礼物
            if ($("#hasGift_" + id).is(":checked")) {
                var val = $("#giftName_" + id).html() || "";
                if (val == "") {
                    alert("请选择礼物!");
                    $("#giftName_" + id).html("");
                    grade.focus("#giftName_" + id);
                    return;
                }
                temp.IsGiveGift = true;
                temp.ProductID = $("#giftName_" + id).attr("ProductID");
                temp.goujiuPrice = $("#giftName_" + id).attr("goujiuPrice");
                temp.imageUrl = $("#giftName_" + id).attr("imageUrl");
            }

            //送积分
            if ($("#hasPresentPoint_" + id).is(":checked")) {
                var val = $("#presentPoint_" + id).val() || "";
                if (val == "" || isNaN(val)) {
                    alert("请输入积分!");
                    $("#presentPoint_" + id).val("");
                    grade.focus("#presentPoint_" + id);
                    return;
                }
                temp.IsGiveIntegral = true;
                temp.Integral = val;
            }

            //包邮
            if ($("#hasFreePostage_" + id).is(":checked")) {
                temp.IsNoPostage = true;
            }

            //送优惠券
            if ($("#hasShopBonus_" + id).is(":checked")) {
                var val = $("#shopBonus_" + id).html() || "";
                if (val == "") {
                    alert("请选择优惠券!");
                    $("#shopBonus_" + id).html("");
                    grade.focus("#shopBonus_" + id);
                    return;
                }

                temp.CouponTypeID = $("#shopBonus_" + id).attr("couponType") || "";
                temp.CouponID = $("#shopBonus_" + id).attr("couponID") || "";
                temp.IsGiveCoupon = true;
            }

            result.push(temp);
        }

        return { item: result, removeObj: grade.removeObj || [] };
    }
};
/**************添加促销层级 Start *************/

//选择促销方式（普通促销、多级促销）
function choicePreferentialWay(e) {
    $("#J_MultiGrade").empty();
    var type = $(e).val();
    grade.init(type);
    if (type == 0) {
        $("#addPromoteTier").css("display", "none");
    } else {
        $("#addPromoteTier").css("display", "block");
        AddGrade();
    }
}

//添加促销层级
function AddGrade() {
    grade.init(1);
}

//打开商品选择界面
function OpenProductWindow() {
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
        grade.dealGift({ "productID": productID, "productName": productName, "goujiuPrice": goujiuPrice, "imageUrl": imageUrl });
    });
}
//打开优惠券选择界面
function OpenCouponWindow() {
    $("#choiceCouponWindow").data("kendoWindow").open().center();

    $("#ConfirmCoupon").click(function () {
        var couponType = "";
        var couponID = "";
        var couponName = "";
        $("input[name='rb_CouponType']").each(function () {
            if ($(this)[0].checked) {
                couponType = $(this).val();
            }
        });
        var dropdownlist;
        if (couponType == "1") {
            dropdownlist = $("#CounponDecrease").data("kendoDropDownList");
            couponID = dropdownlist.value();
            couponName = dropdownlist.text();
        } else {
            dropdownlist = $("#CounponCash").data("kendoDropDownList");
            couponID = dropdownlist.value();
            couponName = dropdownlist.text();
        }

        $("#choiceCouponWindow").data("kendoWindow").close();
        grade.dealShopBonus({ "couponType": couponType, "couponID": couponID, "couponName": couponName });
    });
}

//送满减券
function choiceDecrease(e) {
    if (e.checked) {
        $(e).next().next().css("display", "block");
        $(e).next().next().next().css("display", "none");
    } else {
        $(e).next().next().next().css("display", "block");
        $(e).next().next().css("display", "none");
    }
}
//送现金券
function choiceCash(e) {
    if (e.checked) {
        $(e).next().next().css("display", "block");
        $(e).next().css("display", "none");
    } else {
        $(e).next().next().css("display", "none");
        $(e).next().css("display", "block");
    }
}
$(function () {
    meetMoney.Init();
});