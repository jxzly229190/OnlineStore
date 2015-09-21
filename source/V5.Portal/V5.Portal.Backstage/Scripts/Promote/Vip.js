//满额优惠列表搜索条件
function SearchData() {
    return {
        promoteName: $("#promoteName").val(),
        promoteStatus: $("#promoteStatus").val(),
        startStartTime: $("#promoteStartStartTime").val(),
        startEndTime: $("#promoteStartEndTime").val(),
        endStartTime: $("#promoteEndStartTime").val(),
        endEndTime: $("#promoteEndEndTime").val()
    };
}

function searchVip() {
    var filter = new Array();
    var grid = $("#VipGrid").data("kendoGrid");
    grid.dataSource.filter(filter);
}

var promotevip = {
    //功能描述：初始化
    Init: function() {
        //获取站点地址
        promotevip.GetSiteUrl();
    },

    //功能描述：设置参数
    SetOption: function(option) {
    },

    //功能描述：加载事件
    LoadEvent: function() {

    },

    //功能描述：加载数据
    LoadData: function(opt) {
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
            success: function(result) {
                if ($.isFunction(opt.fn)) {
                    opt.fn(result);
                }
            },
            error: function() {
                alert("处理失败!");
            }
        });
    },

    //功能描述：获取站点地址
    GetSiteUrl: function() {
        var opt = {};
        opt.url = "/Home/GetSiteUrl";
        opt.dataType = "text";
        opt.async = false;
        opt.fn = function(data) {
            promotevip.siteUrl = data || "";
        };
        //加载数据
        promotevip.LoadData(opt);
    },
    //功能描述：详情页
    Detail: function(e) {
        $.get("promote/QueryVipByID", { id: e.name }, function(data) {
            if (data) {
                promotevip.DetailStruct(data);
            }
        });
    },
    //功能描述：
    DetailStruct: function(data) {
        var html = "";
        html += "<div class=\"member-box\">";
        html += "<h3>促销基本信息 </h3>";
        html += "<div class=\"member-detail\">";
        html += "<table width=\"1020\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">";
        html += "<tr>";
        html += "<td width=\"89\" height=\"5\" align=\"right\">活动名称：</td>";
        html += "<td width=\"174\" align=\"left\">" + data.Name + "</td>";
        html += "<td width=\"108\" align=\"right\">活动备注:" + (data.Description || "") + "</td>";
        html += "</tr>";
        html += "<tr>";
        html += "<td width=\"89\" height=\"5\" align=\"right\">活动时间：</td>";
        html += "<td width=\"174\" align=\"left\">" + data.StartTime + "</td>";
        html += "<td width=\"174\" align=\"right\">" + data.EndTime + "</td>";
        html += "</tr>";
        html += "<tr>";
        html += "<td width=\"89\" height=\"5\" align=\"right\"></td>";
        html += "<td width=\"174\" align=\"left\">使用优惠券：" + data.IsUseCoupon + "仅限新会员：" + data.IsNewUser + "需要手机验证：" + data.IsMobileValidate + "</td>";
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
        promotevip.DetailProData(data.Scopes);
    },

    DetailProData: function(scope) {
        var condition = "";
        for (var j = 0; j < scope.length; j++) {
            condition += scope[j].ProductID + ",";
        }
        var opt = {};
        opt.url = "/Product/ChooseProductOnSale";
        opt.data = { condition: condition };
        opt.dataType = "json";
        opt.fn = function(result) {
            if (!result) return;
            var data = result.data;
            if (!data || data == null) return;
            var html = "";
            for (var i = 0; i < data.length; i++) {
                var src = data[i].Path || "";
                var name = data[i].Name || "";
                var id = data[i].ID;
                html += "<tr name=\"" + name + "\">";
                html += "<td class=\"p_name\" ><img src=\"" + src + "\" alt=\"\" /><a href=\" " + promotevip.siteUrl + "/Product/Item-id-" + id + ".htm\" title=\"" + name + "\" target=\"_blank\" >" + name + "</a></td>";
                html += "</tr>";
            }
            $("#item_list_selected").html(html);
        };

        //加载数据
        Choose.LoadData(opt);
    },
    
    //功能描述：删除
    Delete: function(e) {
        var mess = confirm("确实要删除吗?");
        if (mess != "0") {
            $.post("/promote/RemoveVip", { id: e.name }, function(data) {
                alert(data.Message);
                if (data.State == 1) {
                    var meetMoeyDataSource = $("#VipGrid").data("kendoGrid").dataSource;
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
    Stop: function(e) {
        if ($(e).val() == "停止") {
            var mess = confirm("确实要停止吗?");
            if (mess != "0") {
                $.post("/promote/ChangesVipStatus", { id: e.name, status: 3 }, function(data) {
                    alert(data.Message);
                    if (data.State == 1) {
                        promotevip.Close();
                    }
                });
            }
        }
    },

    // 功能描述：暂停、恢复
    Suspend: function(e) {
        if ($(e).val() == "暂停") {
            var mess = confirm("确实要暂停吗?");
            if (mess != "0") {
                $.post("/promote/ChangesVipStatus", { id: e.name, status: 2 }, function(data) {
                    alert(data.Message);
                    if (data.State == 1) {
                        promotevip.Close();
                    }
                });
            } else {
                return;
            }
        } else {
            var mess = confirm("确实要恢复吗?");
            if (mess != "0") {
                $.post("/promote/ChangesVipStatus", { id: e.name, status: 1 }, function(data) {
                    alert(data.Message);
                    if (data.State == 1) {
                        promotevip.Close();
                    }
                });
            }
        }
    },

    // 功能描述：关闭满额优惠编辑页
    Close: function() {
        $.get("/promote/Vip", null, function(data) {
            if (data.State == -401) {
                onSessionLost();
            } else if (data.State == -403) {
                alert("对不起，您无此操作权限！");
            } else {
                $('#defaultDiv').html(data);
            }
        });
    },

    // 功能描述：显示满额优惠编辑页
    Edit: function(e, type) {
        var href = "/promote/EditVip";
        $.ajax({
            type: "GET",
            url: href,
            data: null,
            datatype: "html",
            success: function(data) {
                if (data.State == -401) {
                    onSessionLost();
                } else if (data.State == -403) {
                    alert("对不起，您无此操作权限！");
                } else {
                    $('#defaultDiv').html(data);
                    if (!e) {
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
                        promotevip.GetModifyData(e.name, type);
                    }
                    setTimeout(function() {
                        $("#product_brand").html("<option value=\"bp417\">└其它</option>");
                        Choose.LoadProduct();
                    },1000);
                }
            },
            error: function() {
                alert("处理失败!");
            }
        });
    },

    // 功能描述：添加会员促销活动
    Add: function() {
        var promoteName = $("#promoteName").val();
        var startTime = $("#promoteStartTime").val();
        var endTime = $("#promoteEndTime").val();
        var description = $("#promoteDescription").val(); //活动备注
        var isMoblieVerify = $("#mobileVerify").is(":checked");
        var isNewUser = $("#newUser").is(":checked");
        var isUseCoupon = $("#useCoupon").is(":checked");

        if (promoteName == "") {
            alert("促销活动名称不能为空！");
            grade.focus("#promoteName");
            return;
        }
        if (startTime == "") {
            alert("促销活动开始时间不能为空！");
            grade.focus("#promoteStartTime");
            return;
        }
        if (endTime == "") {
            alert("促销活动结束时间不能为空！");
            grade.focus("#promoteEndTime");
            return;
        }

        var startDate = new Date(startTime);
        var endDate = new Date(endTime);
        if (startDate >= endDate) {
            alert("促销活动时间无效！");
            grade.focus("#promoteEndTime");
            return;
        }

        var json = {};
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
        } else {
            for (var i in scope) {
                json["scopes[" + i + "]"] = scope[i];
            }
        }

        $.ajax({
            type: "post",
            url: "/promote/AddVip",
            data: json,
            async: false,
            success: function(msg) {
                alert(msg.Message);
                if (msg.State == 1) {
                    promotevip.Close();
                }
            }
        });
    },

    // 功能描述：获取
    GetModifyData: function(id, reform) {
        $.ajax({
            type: "GET",
            url: "Promote/QueryVipByID",
            data: { id: id },
            dataType: "json",
            success: function(data) {
                if (data.State == -401) {
                    onSessionLost();
                } else if (data.State == -403) {
                    alert('对不起，您无此操作权限！若需要操作，请与管理员联系。');
                } else {
                    $("#promoteName").val(data.Name);
                    $("#promoteDescription").val(data.Description);
                    if (reform == null) {
                        $("#promoteID").val(data.ID);
                        $("#promoteStartTime").data("kendoDateTimePicker").value(data.StartTime);
                        $("#promoteEndTime").data("kendoDateTimePicker").value(data.EndTime);
                    }

                    var option = {};
                    option.container = "#container";
                    var productItems = [];
                    for (var i in data.Scopes) {
                        productItems.push(data.Scopes[i].ProductID);
                    }
                    option.selected = productItems;
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
                }
            },
            error: function() {
                alert("处理失败??!");
            }
        });
    },

    // 功能描述：修改会员促销优惠活动
    Modify: function() {
        var promoteID = $("#promoteID").val();
        var promoteName = $("#promoteName").val();
        var startTime = $("#promoteStartTime").val();
        var endTime = $("#promoteEndTime").val();
        var description = $("#promoteDescription").val(); //活动备注
        var isMoblieVerify = $("#mobileVerify").is(":checked");
        var isNewUser = $("#newUser").is(":checked");
        var isUseCoupon = $("#useCoupon").is(":checked");

        if (promoteName == "") {
            alert("促销活动名称不能为空！");
            grade.focus("#promoteName");
            return;
        }
        if (startTime == "") {
            alert("促销活动开始时间不能为空！");
            grade.focus("#promoteStartTime");
            return;
        }
        if (endTime == "") {
            alert("促销活动结束时间不能为空！");
            grade.focus("#promoteEndTime");
            return;
        }

        var startDate = new Date(startTime);
        var endDate = new Date(endTime);
        if (startDate >= endDate) {
            alert("促销活动时间无效！");
            grade.focus("#promoteEndTime");
            return;
        }

        var json = {};
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
        } else {
            for (var i in scope) {
                json["scopes[" + i + "]"] = scope[i];
            }
        }

        $.ajax({
            type: "post",
            url: "/promote/ModifyVip",
            data: json,
            async: false,
            success: function(msg) {
                alert(msg.Message);
                if (msg.State == 1) {
                    promotevip.Close();
                }
            }
        });
    }
};
$(function () {
    promotevip.Init();
});