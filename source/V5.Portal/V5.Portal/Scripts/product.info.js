
var productinfo = {
    id: Number,

    //功能描述：初始化
    Init: function (option) {
        if (!productinfo.SetOption(option)) {
            alert("参数错误!");
            return;
        }
        productinfo.GetProductData(option.id);
    },

    //功能描述：设置参数
    SetOption: function (option) {
        if (!option) return false;
        productinfo.id = option.id;
        return true;
    },

    //功能描述：加载事件
    LoadEvent: function () {
        productinfo.countCat();
        productinfo.storeinfo();
    },

    //功能描述：获取商品数据
    GetProductData: function (id) {
        $.get("/Product/GetProductPromote", { productID: id }, function (data) {
            if (data && data.Status == 2) {
                $(function() {
                    productinfo.LoadHtml(data);
                    productinfo.LoadEvent();
                });
            } else {
                alert("商品已下架");
                window.location.href = "/";
            }
        }, "json");
    },

    //功能描述：加载Html
    LoadHtml: function (data) {
        $("#mark_price").html(data.MarketPrice);
        $("#comment_sum").html(data.CommentNumber);
        $("#commentCount").html(data.CommentNumber);
        if (!data.CommentScore) {
            $("#commet_score").html("5");
            $("#commet_start").attr("class", "s_10 star db fl");
        } else {
            $("#commet_score").html(data.CommentScore.toString());
            $("#commet_start").attr("class", "s_" + (data.CommentScore * 2) + " star db fl");
        }
        if (data.InventoryNumber > 0) {
            $("#inventory_no").html("有");
        } else {
            $("#inventory_no").html("无");
        }
        if (data.InventoryNumber > 0) {
            $("#cart_btn").html("<a class=\"buy_btn\" onclick=\"javascript:productinfo.addCat();\"></a>");
        } else {
            $("#cart_btn").html("<a id=\"addCatNo\" class=\"buy_btn_no\" title=\"此商品已售完\"</a>");
        }

        if (data.PromoteNames != "") {
            var html = "";
            html = "<p class=\"c01 lineHeight20 fwb fs14\">" + (data.PromoteNames || "").replace(",", " ") + "</p>";
            $("#pro_promote").html(html); // 活动名称
            $("#spPrice").html(data.PromotePrice);
            html = "<span class=\"c09 fl\">促销信息：</span>";
            html += "<div class=\"fl\">";
            var promotes = data.PromoteTypes.split(',');
            var promoteid = data.PromoteIDs.split(',');
            if ($.inArray("1", promotes) >= 0) {
                html += "<p class=\"cuxiao\"><em>限时打折</em>";
                html += "已优惠￥" + (data.MarketPrice - data.PromotePrice).toFixed(2) + "元</p>";
            }
            if ($.inArray("11", promotes) >= 0) {
                html += "<p class=\"cuxiao\"><em class=\"hl_red_bg\">满额优惠</em>";
                var rules = productinfo.GetPromoteInfo(promoteid[$.inArray("11", promotes)], 1);
                for (var i = 0; i < rules.length; i++) {
                    html += rules[i] + "; ";
                }
                html += "</p>";
            }
            if ($.inArray("12", promotes) >= 0) {
                html += "<p class=\"cuxiao\"><em class=\"hl_red_bg\">满件优惠</em>";
                rules = productinfo.GetPromoteInfo(promoteid[$.inArray("12", promotes)], 2);
                for (i = 0; i < rules.length; i++) {
                    html += rules[i] + "; ";
                }
                html += "</p>";
            }
            html += "</div>";
            $("#promote_info").html(html);
        } else {
            $("#spPrice").html(data.GoujiuPrice);
        } // 活动信息
    },

    //加入购物车
    addCat: function () {
        var quantity = $.trim($("#txtQuantity").val());
        var productId = $.trim($("#proId").val());
        Cat.Add(productId, quantity);
    },

    // 加入收藏
    addCollect: function () {
        var option = {};
        option.productId = $.trim($("#proId").val());
        option.fn = function (result) {
            if (result.State == 1) {
                $("#divSuccess").removeClass("hide");
                setTimeout(function () { $("#divSuccess").addClass("hide"); }, 1000);
            } else if (result.State == 2) {
                $("#divCollect").removeClass("hide");
                setTimeout(function () { $("#divCollect").addClass("hide"); }, 1000);
            } else if (result.State == 3) {
                window.location = '/login?backurl=' + document.URL;
            } else {
                alert(result.Message);
            }
        };

        Collect.Add(option.productId, option.fn);
    },

    //加、减数量
    countCat: function () {
        var add = $("#buynum_down"), cut = $("#buynum_up"), val = $("#txtQuantity");
        add.click(function () {
            var init = val.val(), sum;
            sum = parseInt(init) + 1;
            val.val(sum);
            getPromotionPrice(val); //get price
        });
        cut.click(function () {
            var init = val.val(), sum;
            sum = parseInt(init) - 1;
            if (sum < 1) {
                alert("商品数量不能小于1件。");
                val.val(1);
            } else {
                val.val(sum);
            }
            getPromotionPrice(val); //get price
        });
        val.keyup(function () {
            if (parseInt($(this).val()) == 0 || parseInt($(this).val()) < 0) {
                alert("商品数量不能小于1件。");
                $(this).val(1);
            }
            getPromotionPrice(val); //get price
        });
    },

    //    // 功能描述：获取商品促销信息
    //    GetProductPromote: function (id) {
    //        $.post("/Product/GetProductPromote", { productID: id }, function (data) {
    //            if (data && data.PromoteNames != "") {
    //                productinfo.GetProductData(id, data);
    //            } else {
    //                productinfo.GetProductData(id, null);
    //            }
    //        });
    //    },

    // 功能描述：获取促销详情
    GetPromoteInfo: function (id, type) {
        var result = null;
        $.ajax({
            type: "Get",
            url: "/Product/GetPromoteinfo",
            data: { promoteID: id, type: type },
            datatype: "json",
            async: false,
            success: function (data) {
                result = data;
            }
        });
        return result;
    },

    // 功能描述：商品区域事件
    storeinfo: function () {
        $("#storeinfo .at").bind({
            "mouseenter": function () {
                $(this).children(".addrArea").show();
                $(".at_btn").addClass("on");
            },
            "mouseleave": function () {
                $(this).children(".addrArea").hide();
                $(".at_btn").removeClass("on");
            }
        });
    }
};