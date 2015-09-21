var table = {
    container: Object,
    columns: Array, //格式：[{name:"path", desc:"", type:columnType.Image},{name:"price", desc:"价格", type:columnType.Amt},{name:"productName", desc:"名称", type:columnType.Text}]
    columnType: { Image: "Image", Amt: "Amt", Text: "Text", Custom: "Custom", Date: "Date", DateTime: "DateTime" },
    columnAlign: { center: "center", left: "left", right: "right" },
    search: Object,
    select: { enabled: false, url: "", key: "" },
    pageUrl: String,
    pageIndex: Number,
    pageCount: Number,
    pageSize: Number,
    rowsCount: Number,
    showPage: Boolean,

    //功能描述：初始化
    Init: function (option) {
        if (!table.SetOption(option)) {
            alert("参数错误!");
            return;
        }
        table.LoadTable();
        table.LoadData();
        table.LoadEvent();
    },

    //功能描述：设置参数
    SetOption: function (option) {
        if (!option) return false;
        if (!option.container || option.container == null) return false;
        if (!option.columns || option.columns == null) return false;
        if (!option.columns.length || option.columns.length <= 0) return false;
        if (!option.pageUrl || option.pageUrl == "") return false;

        table.container = document.getElementById(option.container);
        if (!table.container || table.container == null) return false;

        if (option.select && option.select.enabled == true) {
            if (!option.select.url || option.select.url == "") return false;
            if (!option.select.key || option.select.key == "") return false;
            table.select = option.select;
        }

        table.search = document.getElementById(option.search);
        if (table.search && table.search != null) {
            table.search.value = "";
        }

        table.columns = option.columns;
        table.pageSize = option.pageSize || 5;
        table.pageIndex = option.pageIndex || 1;
        table.pageUrl = option.pageUrl || "";
        table.showPage = option.showPage == false ? false : true;

        if ($.isFunction(option.getCustomCell)) {
            table.GetCustomCell = option.getCustomCell;
        }
        return true;
    },

    //功能描述：加载事件
    LoadEvent: function () {
        if (table.search) {
            table.search.onkeydown = function (event) {
                var keyCode = event.keyCode || "";
                if (keyCode == 13 || keyCode == 108) {
                    table.pageIndex = 1;
                    table.LoadData();
                }
            }
        }
    },

    //功能描述：选择全部
    SelectAll: function (obj) {
        if (obj.checked) {
            $(table.container).find(".checkbox input").attr("checked", true);
        } else {
            $(table.container).find(".checkbox input").attr("checked", false);
        }
    },

    //功能描述：删除全部
    DeleteAll: function () {
        var list = "";
        $(table.container).find("tbody .checkbox input:checked").each(function () {
            var data = $(this).attr("data") || "";
            list += data == "" ? "" : data + ",";
        });
        list = list.replace(/\,$/, "");

        //删除
        table.Delete(list);
    },

    //功能描述：删除
    Delete: function (id) {
        if (!id || id == "") return;

        if (confirm("是否删除?")) {
            $.ajax({
                type: "POST",
                url: table.select.url + "?t=" + Math.random(),
                data: { ID: id },
                success: function (result) {
                    if (!result || result.State != 1) {
                        alert("删除失败: " + result.Message);
                        return;
                    }
                    alert("删除完毕");

                    //重新加载数据
                    table.pageIndex = 1;
                    table.LoadData();
                },
                error: function () {
                    alert("请求失败，请联系系统管理员！");
                }
            });
        }
    },

    //功能描述：加入购物车
    Cart: function (id) {
        if (!id || id == "") return;
       
        $.ajax({
            type: "POST",
            url: "/Cart/Add?t=" + Math.random(),
            data: { quantity: 1, productId: id },
            success: function (result) {
                if (result.State == 1) {
                    alert("添加成功");
                    //$("#head_cart_no").load("/Cart/GetCartInfo?t=" + Math.random());
                    Cat.LoadServer();
                } else {
                    if (result.Message) {
                        alert(result.Message);
                    } else {
                        alert('操作失败');
                    }
                }
            },
            error: function () {
                alert("请求失败，请联系系统管理员！");
            }
        });
    },

    //功能描述：跳转到上一页
    GotoPagePrev: function () {
        if (table.pageIndex == 1) {
            alert("已经是第一页了");
            return;
        }
        table.pageIndex--;
        table.LoadData();
    },

    //功能描述：跳转到下一页
    GotoPageNext: function () {
        if (table.pageIndex == table.pageCount) {
            alert("已经是最后一页了");
            return;
        }
        table.pageIndex++;
        table.LoadData();
    },

    //功能描述：加载表格
    LoadTable: function () {
        var html = "";
        var len = table.columns.length;

        //头部
        html += "<thead>";
        html += "<tr>";
        if (table.select.enabled == true) {
            html += "<td class=\"checkbox\"><input type=\"checkbox\" class=\"uc_order_select\" onclick=\"javascript:table.SelectAll(this); return true;\" />全选</td>";
            len++;
        }
        for (var i = 0; i < table.columns.length; i++) {
            var width = table.columns[i].width || "auto";
            width = width == "auto" ? "auto" : width + "px";
            html += "<td style=\"width:" + width + "\">" + (table.columns[i].desc || "") + "</td>";
        }
        if (table.action && table.action.desc) {
            html += ("<td class=\"oper\">" + (table.action.desc || "操作") + "</td>");
            len++;
        }
        html += "</tr>";
        html += "</thead>";

        //中部
        html += "<tbody>";
        html += "<tr><td colspan=\"" + len + "\"></td></tr>";
        html += "</tbody>";

        //底部
        html += "<tfoot>";
        html += "<tr>";
        if (table.select.enabled == true) {
            len--;
            html += "<td class=\"checkbox\"><input type=\"checkbox\" class=\"uc_order_select\" onclick=\"javascript:table.SelectAll(this); return true;\" /><a href=\"###\" class=\"button\" onclick=\"javascript:table.DeleteAll();\" >批量删除</a></td>";
        }
        html += "<td colspan=\"" + len + "\">";
        html += "<div class=\"uc_page_container\"><div class=\"uc_page\">";
        html += "</div></div>";
        html += "</td>";
        html += "</tr>";
        html += "</tfoot>";

        table.container.innerHTML = "<table>" + html + "</table>";
    },

    //功能描述：加载数据
    LoadData: function (data) {
        table.pageIndex = data || table.pageIndex;
        var search = table.search ? table.search.value : "";
        $.ajax({
            type: "POST",
            url: table.pageUrl + "?t=" + Math.random(),
            data: { pageIndex: table.pageIndex, pageSize: table.pageSize, search: search },
            dataType: "json",
            success: function (result) {
                if (!result) return;

                //清除全选
                $(".checkbox .uc_order_select").attr("checked", false);

                table.rowsCount = result.rowsCount;
                table.pageCount = Math.ceil(result.rowsCount / table.pageSize);

                table.LoadBody(result.data);
                table.LoadFoot();
            },
            error: function () {
                alert("请求失败，请联系系统管理员！");
            }
        });
    },

    //功能描述：加载中部列表
    LoadBody: function (data) {
        var html = "";
        if (data && data.length > 0) {
            for (var i = 0; i < data.length; i++) {
                if (data[i]) {
                    html += "<tr>" + table.GetRow(data[i]) + "</tr>";
                }
            }
        }
        var tbody = table.container.getElementsByTagName("tbody")[0];
        //tbody.innerHTML = html;
        $(tbody).html(html);
    },

    //功能描述：加载底部分页
    LoadFoot: function () {
        if (!table.showPage) return;

        var html = "";
        if (table.pageCount > 0) {
            html += "<a href=\"###\" class=\"prev\" onclick=\"javascript:table.GotoPagePrev()\">&lt;</a>";
            for (var i = 1; i < table.pageCount + 1; i++) {
                if (i == table.pageIndex) {
                    html += "<a href=\"###\" class=\"curr\">" + i + "</a>";
                } else {
                    html += "<a href=\"###\" onclick=\"javascript:table.LoadData(" + i + ")\">" + i + "</a>";
                }
            }
            html += "<a href=\"###\" class=\"next\" onclick=\"javascript:table.GotoPageNext()\">&gt;</a>";
        }
        var tfoot = table.container.getElementsByTagName("tfoot")[0];
        $(tfoot).find(".uc_page").html(html);
    },

    //功能描述：获取行
    GetRow: function (data) {
        var html = "";
        if (table.select.enabled == true) {
            html += "<td class=\"checkbox\"><input type=\"checkbox\" data=\"" + (data[table.select.key] || "") + "\" /></td>";
        }
        for (var i = 0; i < table.columns.length; i++) {
            html += table.GetCell(table.columns[i], data);
        }
        return html;
    },

    //功能描述：获取单元格
    GetCell: function (fcolumn, fdata) {
        var fvalue = fdata[fcolumn.name] || "";
        var html = "";
        switch (fcolumn.type) {
            case "Image":
                html += "<img src=\"" + fvalue + "\" />";
                break;
            case "Amt":
                html += ("&#65509;" + table.FormatCurrency(fvalue));
                break;
            case "Text":
                html += fvalue;
                break;
            case "Date":
                html += fvalue;
                break;
            case "DateTime":
                html += fvalue;
                break;
            case "Custom": //自定义                
                html += table.GetCustomCell(fcolumn, fdata, fvalue);
                break;
            default:
                html += fvalue;
                break;
        }
        html = "<td class=\"" + (fcolumn.align || "") + "\">" + html + "</td>";
        return html;
    },

    //功能描述：实现自定义单元格
    GetCustomCell: function (fcolumn, fdata, fvalue) {
        //
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
    }
}