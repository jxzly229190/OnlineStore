var consult = {
    container: Object,
    pageUrl: String,
    productID: Number,
    pageIndex: Number,
    pageCount: Number,
    pageSize: Number,
    rowsCount: Number,
    showPage: Boolean,
    showCount:Object,

    //功能描述：初始化
    Init: function (option) {
        if (!consult.SetOption(option)) {
            alert("参数错误!");
            return;
        }
        consult.Loadconsult();
        consult.LoadData();
        consult.LoadEvent();
    },

    //功能描述：设置参数
    SetOption: function (option) {
        if (!option) return false;
        if (!option.pageUrl || option.pageUrl == "") return false;

        consult.container = document.getElementById(option.container);
        if (!consult.container || consult.container == null) return false;

        consult.showCount = document.getElementById(option.showCount);
        if (!consult.showCount || consult.showCount == null) return false;
        
        consult.productID = option.productID;
        if (consult.productID <= 0) return false;
        consult.pageSize = option.pageSize || 15;
        consult.pageIndex = option.pageIndex || 1;
        consult.pageUrl = option.pageUrl || "";
        consult.showPage = option.showPage == false ? false : true;

        if ($.isFunction(option.getCustomCell)) {
            consult.GetCustomCell = option.getCustomCell;
        }
        return true;
    },
    
    //功能描述：加载事件
    LoadEvent: function () {

    },

    //功能描述：咨询
    ConsultShow: function () {
        var obj = $("#add_pro_con");
        if (obj.css("display") == "none") {
            obj.css("display", "block");
        } else {
            obj.css("display", "none");
        }
    },
    
    //功能描述：跳转到第一页
    GotoPageFirst: function () {
        consult.pageIndex = 1;
        consult.LoadData();
    },

    //功能描述：跳转到上一页
    GotoPagePrev: function () {
        if (consult.pageIndex == 1) {
            alert("已经是第一页了");
            return;
        }
        consult.pageIndex--;
        consult.LoadData();
    },

    //功能描述：跳转到下一页
    GotoPageNext: function () {
        if (consult.pageIndex == consult.pageCount) {
            alert("已经是最后一页了");
            return;
        }
        consult.pageIndex++;
        consult.LoadData();
    },

    //功能描述：跳转到最后一页
    GotoPageLast: function () {
        consult.pageIndex = consult.pageCount;
        consult.LoadData();
    },
    
    //功能描述：提交评论
    SubmitConsult: function () {
        var productID = $("#txtConsult").attr("proID");
        var content = $("#txtConsult").val();
        
        if ($.trim(content) == "") {
            alert("请输入评论内容!");
            return;
        }

        $.post("/User/AddConsult", { "productID": productID, "content": content }, function (data) {
            if (data.State == 1) {
                alert(data.Message); // 回复成功！
                $("#txtConsult").attr("value", "");
            }else {
                alert(data.Message); 
            }
        });
    },
    
    //功能描述：加载结构
    Loadconsult: function () {
        var html = "";
        // 内容
        html += "<div id=\"tc\" class=\"\"></div>";

        // 底部分页
        html += "<div id=\"page_box_consult\" class=\"page_box PageConsult\"></div>";
        consult.container.innerHTML = html;
    },

    //功能描述：加载数据
    LoadData: function (data) {
        consult.pageIndex = data || consult.pageIndex;
        $.ajax({
            type: "POST",
            url: consult.pageUrl + "?t=" + Math.random(),
            data: { productID: consult.productID, pageIndex: consult.pageIndex, pageSize: consult.pageSize },
            dataType: "json",
            success: function (result) {
                if (!result) return;
                consult.rowsCount = result.rowsCount;
                consult.pageCount = Math.ceil(result.rowsCount / consult.pageSize);

                consult.showCount.innerHTML = consult.rowsCount;
                consult.LoadBody(result.data);
                consult.LoadFoot();
            }
        });
    },
    
   //功能描述：加载中部列表
    LoadBody: function (data) {
        var html = "";
        if (data && data.length > 0) {
            for (var i = 0; i < data.length; i++) {
                if (data[i]) {
                    if (i % 2 == 0) {
                        html += "<ul class=\"db sp_box_1 bb1d p10\">";
                    } else {
                        html += "<ul class=\"db sp_box_1 bb1d p10 bg_gray\">";
                    }

                    html += "<li class=\"db tar c_gray\" style=\"line-height: 18px;\">";
                    html += "<span class=\"red_weight\">" + (data[i].UserName ||"匿名") + "</span>";
                    html += "<span class=\" pl10 \">发表于" + (data[i].ConsultTime ||"")+ "</span>";
                    html += "</li>";
                    html += "<li class=\"db\">";
                    html += "<div class=\"icon_p fl\"> </div>";
                    html += "<div class=\"w670 fl\"> 问题内容：" + (data[i].ConsultContent||"") + "</div>";
                    html += "<div class=\"clear\"> </div>";
                    html += "</li>";
                    html += "<li class=\"db\">";
                    html += "<div class=\"icon_a fl\"> </div>";
                    html += "<div class=\"c_red w670 fl\"> 客服回复： " + (data[i].Content||"") + "</div>";
                    html += "<div class=\"clear\"> </div>";
                    html += "</li>";
                    html += "<div class=\"clear\"> </div>";
                    html += "</ul>";
                    //html += "<div class=\"bb1d pb10 pt10\">" + consult.GetRow(data[i]) + "</div>";
                }
            }
        }
        var tbody = document.getElementById("tc");
        tbody.innerHTML = html;
    },

    //功能描述：加载底部分页
    LoadFoot: function () {
        if (!consult.showPage) return;

        var html = "";
        if (consult.pageCount > 0) {

            html += "<span><font><a href=\"###\" onclick=\"javascript:consult.GotoPageFirst()\">&lt;&lt;首页</a></font></span>";
            html += "<span><font><a href=\"###\" onclick=\"javascript:consult.GotoPagePrev()\">上一页</a></font></span>";

            var i = consult.pageIndex - 3 > 0 ? consult.pageIndex - 3 : 1;
            var c = consult.pageCount - 3 > consult.pageIndex ? consult.pageIndex + 3 : consult.pageCount;
            for (; i <= c; i++) {
                if (i == consult.pageIndex) {
                    html += "<span class=\"page_on\">" + i + "</span>";
                } else {
                    html += "<span><a href=\"###\" onclick=\"javascript:consult.LoadData(" + i + ")\">" + i + "</a></span>";
                }
            }

            html += "<span><a href=\"###\" onclick=\"javascript:consult.GotoPageNext()\">下一页</a></span>";
            html += "<span><a href=\"###\" onclick=\"javascript:consult.GotoPageLast()\">尾页&gt;&gt;</a></span>";
            html += "<span>" + consult.pageIndex + "/共" + consult.pageCount + "页</span>";
        }

        var tfoot = document.getElementById("page_box_consult");
        $(tfoot).html(html);
    }
}