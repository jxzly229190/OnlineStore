
var areaPrice = {
    init: function () {
        var node = $("#area-price");
        var item = node.find("div[data-widget=area]");
        this.bind(item, this.mouseenter, "mouseenter");
        this.bind(item, this.mouseleave, "mouseleave");
    },
    bind: function (element, fn, type) {
        var self = this,
            argus = Array.prototype.slice.call(arguments, 3);
        element.each(function () {
            $(this).bind(type ? type : "click", function (e) {
                fn.call(self, this, element, e, argus)
            })
        });
        return this;
    },
    mouseenter: function (target) {
        var tar = $(target);

        if (!tar.data("flag")) {
            var input = tar.find("div[data-widget=area-check]");
            var list = tar.find("input[data-widget=area-list]");
            var num = tar.find("span[data-widget=area-num]");
            this.bind(input, this.selectAll, "click", num, list);
            this.bind(list, this.selectList, "click", num, list, input);
            tar.data("flag", true);
        }
        if (tar.hasClass("bottom")) {
            var c = tar.find(".city");
            c.css({ top: -c.height() - 11 });
        }
        tar.addClass("curr");
    },
    mouseleave: function (target) {
        $(target).removeClass("curr");
    },
    selectAll: function (target, element, e, argus) {
        var tar = $(target), num = argus[0], list = argus[1];
        if (tar.attr("all") == "1") {
            console.log("tar.removeClass(\"checked\").attr(\"all\", \"0\")");
            tar.removeClass("checked").attr("all", "0");
            for (var i = 0; i < list.length; i++) {
                list.eq(i).prop("checked", false);
            }
            this.checkNum(num, 0);
        } else {
            console.log("tar.removeClass(\"selected\").addClass(\"checked\").attr(\"all\", \"1\")");
            tar.removeClass("selected").addClass("checked").attr("all", "1");
            for (var i = 0; i < list.length; i++) {
                list.eq(i).prop("checked", true);
            }
            this.checkNum(num, list.length);
        }
    },
    selectList: function (target, element, e, argus) {
        var num = argus[0], list = argus[1], input = argus[2], length = list.length;
        switch (this.count(list)) {
            case 0:
                console.log("input.get(0).className = \"input\"");
                input.get(0).className = "input";
                this.checkNum(argus[0], 0);
                break;
            case length:
                console.log("input.removeClass(\"selected\").addClass(\"checked\").attr(\"all\", \"1\")");
                input.removeClass("selected").addClass("checked").attr("all", "1");
                this.checkNum(argus[0], length);
                break;
            default:
                console.log("input.hasClass(\"checked\") && input.removeClass(\"checked\").attr(\"all\", \"0\")");
                input.hasClass("checked") && input.removeClass("checked").attr("all", "0");
                input.addClass("selected");
                this.checkNum(argus[0], list);
        }
    },
    count: function (list) {
        var i = list.length - 1,
                j = 0;
        while (i !== -1) {
            list.eq(i).prop("checked") == true && j++;
            i--;
        }
        return j
    },
    checkNum: function (target, list) {
        var total = typeof list === "number" ? list : this.count(list);
        target.text(total);

        if (target.parents(".fore2").length == 1) {
            var count = target.parents(".sequ").find(".checked,.selected").length;
            $(".pbox li.current .num").text(count);
        }
    },
    getLimitedArea: function () {
        var limitArea = "";
        $(".item").find(".i-item").each(function () {
            var input = $(this).find(".input");
            var city = $(this).find(".city");
            if (input.hasClass("checked") || input.hasClass("selected")) {
                var province = input.attr("data-id");

                limitArea += "{ Id:" + province + ", Cities:[";
                var area = city.find("input");
                area.each(function () {
                    if ($(this).prop("checked") == true) {
                        limitArea += $(this).attr("data-id") + ",";
                    }
                });
                limitArea = limitArea.replace(/,$/, "") + "]},";
            }
        });
        limitArea = limitArea == "" ? "" : "Provinces: [" + limitArea.replace(/,$/, "") + "]";
        return limitArea;
    },
    setLimitedArea: function (data) {
        if (!data || data == null) return;

        var limitArea = "";
        for (var i = 0; i < data.length; i++) {
            var Id = data[i].Id || "";
            var Cities = data[i].Cities || null;
            if (Id == "") return;

            var Province = $("#area-check-" + Id);
            var ItemCount = Province.parent().parent()[0].getElementsByTagName("li").length;
            var ClassName = ItemCount == Cities.length ? "checked" : "selected";
            Province.addClass(ClassName);
            $("#area-num-" + Id).html(Cities.length);

            for (var j = 0; j < Cities.length; j++) {
                var city = $("#area-list-" + Cities[j]);
                if (city != null) {
                    city.prop("checked", true);
                }
            }
        }
    }
};

$(function () {
    $("#area-price").find(".i-item").each(function () {
        var node = $(this),
            prove = node.find("div[data-widget=area-check]"),
            area = node.find("input[data-widget=area-list]"),
            total = node.find("span[data-widget=area-num]");
        if (prove.hasClass("checked")) {
            area.each(function () {
                $(this).prop("checked", true);
            });
            total.text(area.length);
        }
    });
    if ($(".fore2.current").length == 1) {
        $(".sequ").each(function () {
            var _this = $(this);
            var index = $(".sequ").index(_this);
            var count = _this.find(".checked,.selected").length;
            $(".pbox li:eq(" + index + ") .num").text(count);
        });
    }
    $("#getLimitedAreaBtn").click(function () {
        var limitedArea = areaPrice.getLimitedArea();
        $("#info").html(limitedArea);
    });

    $("#setLimitedAreaBtn").click(function () {
        var strLimitedArea = prompt("请输入省市信息(JSON)", "");
        strLimitedArea = strLimitedArea || "";
        if (strLimitedArea != "") {
            var data = $.parseJSON(strLimitedArea);
            areaPrice.setLimitedArea(data);
        }
    });

    //设置限购区域
    $("#savelimitedArea")[0].onclick = function () {
        var ProductId = $("#hdproductlimitId").val();
        if (typeof ProductId == "undefined" || ProductId == "") {
            BatchLimitedSet();
        } else {
            var data = areaPrice.getLimitedArea();
            $.ajax({
                url: "/Product/SaveLimitedBuyArea",
                type: "POST",
                dataType: "json",
                data: { productId: ProductId, AreaID: data, AreaType: 1 },
                success: function (res) {
                    alert(res.Message);
                }
            });
        }
        $("#limitedBuyAreaWindow").kendoWindow();
        var dialog = $("#limitedBuyAreaWindow").data("kendoWindow");
        dialog.close();
    };
    areaPrice.init();
});

//批量设置限购区域
function BatchLimitedSet() {
    var arr = new Array();
    var batch = $("input[name=productCheckBox]");
    for (var i = 0; i < batch.length; i++) {
        if ($(batch[i]).is(":checked")) {
            arr.push($(batch[i]).val());
        }
    }
    var data = areaPrice.getLimitedArea();
    if (arr.toString() == "" || data.toString() == "") {
        alert("请选择限购商品或地区");
        return;
    }
    $.post("/Product/BatchAddLimitedArea", { batchId: arr.toString(), content: data }, function (res) {
        if (res.State == 0) {
            alert(res.Messsage);
        } else {
            alert(res.Message);
        }
    });
    areaPrice.init();
}