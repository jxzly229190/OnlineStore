var Choose = {
    selected: Array,
    container: Object,
    siteUrl: String,

    pageIndex: Number,
    pageSize: Number,
    pageCount: Number,
    rowsCount: Number,

    //功能描述：初始化
    Init: function (option) {
        if (!Choose.SetOption(option)) return;
        Choose.LoadFrame(option);
        Choose.LoadEvent();
        Choose.LoadProduct();
        Choose.LoadBrand();
        Choose.LoadProductSelected();
    },

    //功能描述：设置选项
    SetOption: function (option) {
        if (!option.container || option.container == "") return false;

        Choose.container = $(option.container);
        if (!Choose.container || Choose.container == null) return false;

        Choose.pageIndex = option.pageIndex || 1;
        Choose.pageSize = option.pageSize || 10;
        Choose.selected = option.selected || [];

        //获取站点地址
        Choose.GetSiteUrl();

        return true;
    },

    //功能描述：加载事件
    LoadEvent: function () {
        //Tab页切换
        $(".choose_tab span:first").addClass("choose_tab_active");
        $(".choose_content ul:not(:first)").hide();
        $(".choose_tab span").click(function () {
            $(".choose_tab span").removeClass("choose_tab_active");
            $(this).addClass("choose_tab_active");
            $(".choose_content ul").hide();
            $("#" + $(this).attr("target")).show();
        });

        //产品搜索
        $("#product_name").keyup(function (event) {
            var keyCode = event.keyCode || "";
            if (keyCode == 13 || keyCode == 108) {
                Choose.LoadProduct();
            }
        })

        //产品查找
        $("#product_name_selected").keyup(function (event) {
            var keyCode = event.keyCode || "";
            if (keyCode == 13 || keyCode == 108) {
                Choose.FindProduct();
            }
        })
    },

    //功能描述：
    Flod: function () {
        $("#choose_product_count").html(Choose.selected.length);
        if ($(".choose_body").is(":visible")) {
            $(".choose_body").hide();
        } else {
            $(".choose_body").show();
        }
    },

    //功能描述：加载框架
    LoadFrame: function (option) {
        var show_head_button = option.show_head_button == false ? false : true;
        var show_save_button = option.show_save_button == false ? false : true;

        var html = "<dl class=\"choose\">";

        //显示头部按钮
        if (show_head_button) {
            html += "    <dt class=\"choose_head\">";
            html += "       <span class=\"choose_head_button\" onclick=\"javascript:Choose.Flod();\">选择商品</span>(已选择<em id=\"choose_product_count\">0</em>个商品)";
            html += "    </dt>";
            html += "    <dd class=\"choose_body\">";
        } else {
            html += "    <dd class=\"choose_body\" style=\"display:block;\">";
        }
        html += "        <div class=\"choose_tab\">";
        html += "            <span target=\"choose_item01\">选择商品</span>";
        html += "            <span target=\"choose_item02\" >已选商品</span>";
        html += "        </div>";
        html += "        <div class=\"choose_content\">";
        html += "            <ul class=\"choose_item\" id=\"choose_item01\">";
        html += "                <li class=\"choose_item_head\">";
        html += "                     <select id=\"product_brand\"></select>";
        html += "                     <span>商品名称:</span>";
        html += "                     <input type=\"text\" id=\"product_name\"/>";
        html += "                     <span class=\"b_button\"onclick=\"javascript:Choose.LoadProduct();\">搜索</span>";
        html += "                     <span class=\"b_button\"onclick=\"javascript:Choose.ImportProduct();\">导入</span>";
        html += "                </li>";
        html += "                <li class=\"choose_item_body\">";
        html += "                    <table>";
        html += "                        <thead><tr><td class=\"p_name\">商品信息</td><td>价格</td><td>库存</td><td>操作</td></tr></thead>";
        html += "                    </table>";
        html += "                    <div class=\"choose_item_body_inner\">";
        html += "                        <table id=\"item_list\"><tr><td></td><td></td><td></td><td></td></tr></table>";
        html += "                    </div>";
        html += "                </li>";
        html += "                <li class=\"choose_item_foot\">";
        html += "                    <div class=\"choose_item_action\">";
        html += "                        <input type=\"checkbox\" onclick=\"javascript:Choose.SelectAll(this);\" id=\"item_list_checkall\"/>全选<span class=\"b_button\" onclick=\"javascript:Choose.SelectItemBatch();\" >批量选择</span>";
        html += "                        <div class=\"choose_item_page\">                            ";
        html += "                            <span class=\"page_info\" id=\"page_info\">1/1</span>";
        html += "                            <a href=\"#\" class=\"page_prev\" onclick=\"javascript:Choose.PagePrev();\"><span>上一页</span></a>";
        html += "                            <a href=\"#\" class=\"page_next\" onclick=\"javascript:Choose.PageNext();\"><span>下一页</span></a>";
        html += "                        </div>";
        html += "                    </div>";

        //显示保存按钮
        if (show_save_button) {
            html += "                    <div><span class=\"b_save_button\" onclick=\"javascript:Choose.Flod();\" >保存设置</span></div>";
        }

        html += "                </li>";
        html += "            </ul>";
        html += "            <ul class=\"choose_item\" id=\"choose_item02\">";
        html += "                <li class=\"choose_item_head\">";
        html += "                     <span>商品名称:</span>";
        html += "                     <input type=\"text\" id=\"product_name_selected\"/>";
        html += "                     <span class=\"b_button\"onclick=\"javascript:Choose.FindProduct();\">查找</span>";
        html += "                </li>";
        html += "                <li class=\"choose_item_body\">";
        html += "                    <table>";
        html += "                        <thead><tr><td class=\"p_name\">商品信息</td><td>价格</td><td>库存</td><td>操作</td></tr></thead>";
        html += "                    </table>";
        html += "                    <div class=\"choose_item_body_inner\">";
        html += "                        <table id=\"item_list_selected\"></table>";
        html += "                    </div>";
        html += "                </li>";
        html += "                <li class=\"choose_item_foot\">";
        html += "                    <div class=\"choose_item_action\">";
        html += "                        <input type=\"checkbox\" onclick=\"javascript:Choose.SelectAll(this);\" />全选<span class=\"b_button\" onclick=\"javascript:Choose.UnSelectItemBatch();\" >批量取消</button>";
        html += "                    </div>";

        //显示保存按钮
        if (show_save_button) {
            html += "                    <div><span class=\"b_save_button\" onclick=\"javascript:Choose.Flod();\" >保存设置</span></div>";
        }

        html += "                </li>";
        html += "            </ul>";
        html += "        </div>";
        html += "    </dd>";
        html += "</dl>";

        Choose.container.html(html);
    },

    //功能描述：上一页
    PagePrev: function () {
        if (Choose.pageIndex > 1) {
            Choose.pageIndex--;
            Choose.LoadProduct();
        }
    },

    //功能描述：下一页
    PageNext: function () {
        if (Choose.pageIndex < Choose.pageCount) {
            Choose.pageIndex++;
            Choose.LoadProduct();
        }
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

    //功能描述：加载产品数据
    LoadProduct: function () {
        var product_name = $("#product_name").val() || "";
        var product_brand = ($("#product_brand").val() || "").replace(/^bp/, "");
        var condition = "";
        if (product_brand != "") {
            condition = " ParentBrandID = " + product_brand;
        }

        var opt = {};
        opt.url = "/Product/QueryProductOnSaleSimple";
        opt.data = { pageIndex: Choose.pageIndex, pageSize: Choose.pageSize, Name: product_name, condition: condition };
        opt.dataType = "json";
        opt.fn = function (result) {
            if (!result) {
                LoadProductEmpty();
                return;
            }
            var data = result.data;
            if (!data || data == null) return;

            Choose.rowsCount = result.total;
            Choose.pageCount = Math.ceil(result.total / Choose.pageSize);
            $("#page_info").html(Choose.pageIndex + "/" + Choose.pageCount);

            var html = "";
            for (var i = 0; i < data.length; i++) {
                var src = data[i].Path || "";
                var name = data[i].Name || "";
                var price = data[i].GoujiuPrice || "0";
                var num = data[i].InventoryNumber || "0";
                var id = data[i].ID;

                html += "<tr name=\"" + name + "\">";
                html += "<td class=\"p_name\" ><input type=\"checkbox\" /><img src=\"" + src + "\" alt=\"\" />" + Choose.GetProductUrl(data[i].ID, name) + "</td>";
                html += "<td><em>&#65509;" + Choose.FormatCurrency(price) + "</em></td>";
                html += "<td><em>" + num + "</em></td>";

                if ($.inArray(id, Choose.selected) < 0) {
                    html += "<td><span class=\"b_button\" onclick=\"javascript:Choose.SelectItem(this);\" productId=\"" + id + "\" >选择</span><span productId=\"" + id + "\" class=\"b_selected hide\">已选择</span></td>";
                } else {
                    html += "<td><span class=\"b_button hide\" onclick=\"javascript:Choose.SelectItem(this);\" productId=\"" + id + "\" >选择</span><span productId=\"" + id + "\" class=\"b_selected\">已选择</span></td>";
                }
                html += "</tr>";
            }

            $("#item_list").html(html);

            //清除全选
            $("#item_list_checkall").attr("checked", false);
        };

        //加载数据
        Choose.LoadData(opt);
    },

    //功能描述：
    LoadProductEmpty: function () {
        Choose.rowsCount = 0;
        Choose.pageCount = 0;

        //分页
        $("#page_info").html("1/1");

        //清除内容
        $("#item_list").html(html);

        //清除全选
        $("#item_list_checkall").attr("checked", false);
    },

    //功能描述：加载筛选类别
    LoadBrand: function () {
        var opt = {};
        opt.url = "/Product/QueryBrandTree";
        opt.dataType = "json";
        opt.fn = function (data) {
            if (!data || data == null) return;

            var cache = [];
            var html = "";
            html += "<option value=\"\">全部商品</option>";
            for (var i = 0; i < data.length; i++) {
                if ($.inArray(data[i].ID, cache) > 0) continue;

                if (/c\d+/.test(data[i].ID)) {
                    cache.push(data[i].ID);
                    html += "<optgroup label=\"" + (data[i].Name || "") + "\"></optgroup>";

                    for (var j = 0; j < data.length; j++) {
                        if ($.inArray(data[j].ID, cache) > 0) continue;

                        if (data[i].ID == data[j].PID) {
                            html += "<option value=\"" + (data[j].ID || "") + "\">└" + (data[j].Name || "") + "</option>";
                        }
                    }
                }
            }

            $("#product_brand").html(html);
        };

        //加载数据
        Choose.LoadData(opt);
    },

    //功能描述：加载已选商品
    LoadProductSelected: function () {
        //已选商品数量
        $("#choose_product_count").html(Choose.selected.length);

        if (Choose.selected.length == 0) return;

        var condition = Choose.selected.join(",");
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
                var src = data[i].ThumbnailPath || "";
                var name = data[i].Name || "";
                var price = data[i].GoujiuPrice || "0";
                var num = data[i].InventoryNumber || "0";
                var id = data[i].ID;

                html += "<tr name=\"" + name + "\">";
                html += "<td class=\"p_name\" ><input type=\"checkbox\" /><img src=\"" + src + "\" alt=\"\" />" + Choose.GetProductUrl(data[i].ID, name) + "</td>";
                html += "<td><em>&#65509;" + Choose.FormatCurrency(price) + "</em></td>";
                html += "<td><em>" + num + "</em></td>";
                html += "<td><span class=\"b_button\" onclick=\"javascript:Choose.UnSelectItem(this);\" productId=\"" + id + "\">取消选择</span></td>";
                html += "</tr>";
            }

            $("#item_list_selected").html(html);
        };

        //加载数据
        Choose.LoadData(opt);
    },

    //功能描述：查找
    FindProduct: function () {
        //移除样式
        $("#item_list_selected tr").removeClass("selected");

        //名称
        var name = $("#product_name_selected").val();
        if (!name || name == "") return;

        //查找
        $("#item_list_selected").find("tr[name*=" + name + "]").addClass("selected");
    },

    //功能描述：全选
    SelectAll: function (obj) {
        var checked = obj.checked;
        var target = $(".choose_tab .choose_tab_active").attr("target") || "";
        $("#" + target + " .choose_item_body_inner input[type='checkbox']").each(function () {
            this.checked = obj.checked;
        });
    },

    //功能描述：选择单项
    SelectItem: function (obj) {
        var productId = $(obj).attr("productId");
        if (isNaN(productId)) return;

        if ($.inArray(productId, Choose.selected) < 0) {
            var html = $(obj).parent().parent().html();
            var name = $(obj).parent().parent().attr("name") || "";
            html = html.replace(/\<td\>\<span.*/, "<td><span class=\"b_button\" onclick=\"javascript:Choose.UnSelectItem(this);\" productId=\"" + productId + "\">取消选择</span></td>");

            $("#item_list_selected").append("<tr name=\"" + name + "\">" + html + "</tr>");

            $(obj).hide();
            $(obj).siblings().removeClass("hide");

            Choose.selected.push(Number(productId));
        }
    },

    //功能描述：批量选择
    SelectItemBatch: function () {
        $("#choose_item01 .choose_item_body_inner input[type='checkbox']").each(function () {
            if (this.checked) {
                var obj = $(this).parent().parent().find(".b_button")[0];
                Choose.SelectItem(obj);
            }
        });
    },

    //功能描述：取消选择
    UnSelectItem: function (obj) {
        var productId = $(obj).attr("productId");
        if (isNaN(productId)) return;

        var target = $("#item_list").find("span[productId=" + productId + "]");
        if (target.length > 0) {
            target.show();
            target.siblings().addClass("hide");
        }

        //移除选中数组
        Choose.selected.splice($.inArray(Number(productId), Choose.selected), 1);

        //移除此项
        $(obj).parent().parent().remove();
    },

    //功能描述：批量取消
    UnSelectItemBatch: function () {
        $("#choose_item02 .choose_item_body_inner input[type='checkbox']").each(function () {
            if (this.checked) {
                var obj = $(this).parent().parent().find(".b_button")[0];
                Choose.UnSelectItem(obj);
            }
        });
    },

    //功能描述：获取选中产品
    GetSelect: function () {
        return Choose.selected;
    },

    //功能描述：金额格式化(保留两位小数)
    FormatCurrency: function (num) {
        num = num.toString().replace(/\$|\,/g, '');
        if (isNaN(num)) {
            num = "0";
        }

        sign = (num == (num = Math.abs(num)));
        num = Math.floor(num * 100 + 0.50000000001);
        cents = num % 100;
        num = Math.floor(num / 100).toString();
        if (cents < 10) {
            cents = "0" + cents;
        }
        for (var i = 0; i < Math.floor((num.length - (1 + i)) / 3); i++) {
            num = num.substring(0, num.length - (4 * i + 3)) + ',' + num.substring(num.length - (4 * i + 3));
        }
        return (((sign) ? '' : '-') + num + '.' + cents);
    },

    //功能描述：将位置转换为产品数组
    Convert: function (product) {
        product = product || "";
        if (product == "") return [];

        var products = product.split('');
        if (products.length == 0) return [];

        var result = [];
        for (var i = 0; i < products.length; i++) {
            if (products[i] == 1) {
                result.push(i);
            }
        }

        return result;
    },

    //功能描述：获取站点地址
    GetSiteUrl: function () {
        var opt = {};
        opt.url = "/Home/GetSiteUrl";
        opt.dataType = "text";
        opt.async = false;
        opt.fn = function (data) {
            Choose.siteUrl = data || "";
        };

        //加载数据
        Choose.LoadData(opt);
    },

    //功能描述：获取产品URL
    GetProductUrl: function (id, name) {
        if (isNaN(id)) return "";
        if (!name || name == "") return "";

        return "<a href=\"" + Choose.siteUrl + "/Product/Item-id-" + id + ".htm\" title=\"" + name + "\" target=\"_blank\" >" + name + "</a>";
    }
}