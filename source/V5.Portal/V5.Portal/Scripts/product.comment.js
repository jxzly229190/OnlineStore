var comment = {
    container: Object,
    pageUrl: String,
    productID: Number,
    pageIndex: Number,
    pageCount: Number,
    pageSize: Number,
    rowsCount: Number,
    showPage: Boolean,
    showCount: Object,

    //功能描述：初始化
    Init: function (option) {
        if (!comment.SetOption(option)) {
            alert("参数错误!");
            return;
        }
        comment.Loadcomment();
        comment.LoadData();
        comment.LoadEvent();
    },

    //功能描述：设置参数
    SetOption: function (option) {
        if (!option) return false;
        if (!option.pageUrl || option.pageUrl == "") return false;
        comment.container = document.getElementById(option.container);
        if (!comment.container || comment.container == null) return false;

        comment.showCount = document.getElementById(option.showCount);
        //if (!comment.showCount || comment.showCount == null) return false;

        comment.productID = option.productID;
        if (comment.productID <= 0) return false;
        comment.pageSize = option.pageSize || 5;
        comment.pageIndex = option.pageIndex || 1;
        comment.pageUrl = option.pageUrl || "";
        comment.showPage = option.showPage == false ? false : true;

        if ($.isFunction(option.getCustomCell)) {
            comment.GetCustomCell = option.getCustomCell;
        }
        return true;
    },

    //功能描述：加载事件
    LoadEvent: function () {

    },

    //功能描述：评论
    CommentShow: function () {
        var obj = $("#add_pro_com");
        if (obj.css("display") == "none") {
            obj.css("display", "block");
        } else {
            obj.css("display", "none");
        }
    },

    //功能描述：回复
    ReplyShow: function (e) {
        var obj = $(e).parent().parent().find(".inputelem");
        if (obj.css("display") == "none") {
            obj.css("display", "block");
        } else {
            obj.css("display", "none");
        }
    },

    //功能描述：提交评论
    SubmitComment: function () {
        var score = 5;
        var productID = $("#txtContent").attr("proID");
        var content = $("#txtContent").val();
        $("input[name=Score]").each(function () {
            if ($(this)[0].checked) {
                score = $(this).val();
            }
        });
        if (score == 0) {
            alert("请选择评分");
            return;
        }
        if ($.trim(content) == "") {
            alert("请输入评论内容!");
            return;
        }

        $.post("/User/AddComment", { "score": score, "productID": productID, "content": content }, function (data) {
            if (data.State == 1) {
                alert(data.Message); // 回复成功！
                $("#txtContent").attr("value", "");
            } else if (data.State == 2) {
                window.location = "../../Login/index?backurl=" + document.URL;
            } else {
                alert(data.Message); // 没有购买过
            }
        });
    },

    //功能描述：提交回复
    SubmitReply: function (e) {
        var $this = $(e);
        var commentID = $this.attr("index");
        var strContent = $("#txtContent" + commentID).val();
        if ($.trim(strContent) == "") {
            alert("请输入回复内容!");
            return;
        }
        $.post("/User/AddCommentReply", { "commentID": commentID, "content": strContent }, function (data) {
            if (data.State == 1) {
                alert(data.Message); // 回复成功！
                $("#txtContent" + commentID).attr("value", "");
            } else if (data.State == 2) {
                window.location = "../../Login/index?backurl=" + document.URL; // 未登录！
            } else {
                alert(data.Message); // 没有购买过
            }
        });
    },

    //功能描述：跳转到第一页
    GotoPageFirst: function () {
        comment.pageIndex = 1;
        comment.LoadData();
    },

    //功能描述：跳转到上一页
    GotoPagePrev: function () {
        if (comment.pageIndex == 1) {
            alert("已经是第一页了");
            return;
        }
        comment.pageIndex--;
        comment.LoadData();
    },

    //功能描述：跳转到下一页
    GotoPageNext: function () {
        if (comment.pageIndex == comment.pageCount) {
            alert("已经是最后一页了");
            return;
        }
        comment.pageIndex++;
        comment.LoadData();
    },

    //功能描述：跳转到最后一页
    GotoPageLast: function () {
        comment.pageIndex = comment.pageCount;
        comment.LoadData();
    },

    //功能描述：加载结构
    Loadcomment: function () {
        var html = "";
        // 平均评分
        html += "<div class=\" bb1s pb10 mt20 \"><ul class=\"pl_star\"><li><dd class=\"db fl\">评分：</dd>";
        html += "<dd class=\"s_" + 6 + " star db fl\"></dd><dd class=\" db fl\"><em class=\"fwb\">" + 3 + "</em>分</dd>";
        html += "<div class=\"clear\"></div></li></ul>";
        html += "<div class=\" pl_say \">购买过该商品的用户才能发表评论</div>";
        html += "<div class=\"clear\"></div>";
        html += "</div>";
        // 详细评论
        html += "<div id=\"commentlist\"></div>";
        // 底部分页
        html += "<div id=\"page_box\" class=\"page_box PageComment\"></div>";

        comment.container.innerHTML = html;
    },

    //功能描述：加载数据
    LoadData: function (data) {
        comment.pageIndex = data || comment.pageIndex;

        $.ajax({
            type: "POST",
            url: comment.pageUrl + "?t=" + Math.random(),
            data: { productID: comment.productID, pageIndex: comment.pageIndex, pageSize: comment.pageSize },
            dataType: "json",
            success: function (result) {
                if (!result) return;
                comment.rowsCount = result.rowsCount;
                comment.pageCount = Math.ceil(result.rowsCount / comment.pageSize);
                if (comment.showCount && comment.showCount != null) {
                    comment.showCount.innerHTML = comment.rowsCount;
                }
                comment.LoadBody(result.data);
                comment.LoadFoot();
            }
        });
    },

    //功能描述：加载中部列表
    LoadBody: function (data) {
        var html = "";
        if (!data) return "";
        if (data && data.length > 0) {
            for (var i = 0; i < data.length; i++) {
                if (data[i]) {
                    html += "<div class=\"bb1d pb10 pt10\">" + comment.GetRow(data[i]) + "</div>";
                }
            }
        } else {
            html += "<dl><div class=\"pl_user_say \">";
            html += "<div style=\"padding-top: 15px; text-align: center;\">";
            html += "暂无评价.</div></div></dl>";
        }
        var tbody = document.getElementById("commentlist");
        tbody.innerHTML = html;
    },

    //功能描述：加载底部分页
    LoadFoot: function () {
        if (!comment.showPage) return;

        var html = "";
        if (comment.pageCount > 0) {
            html += "<span><font><a href=\"###\" onclick=\"javascript:comment.GotoPageFirst()\">&lt;&lt;首页</a></font></span>";
            html += "<span><font><a href=\"###\" onclick=\"javascript:comment.GotoPagePrev()\">上一页</a></font></span>";

            var i = comment.pageIndex - 3 > 0 ? comment.pageIndex - 3 : 1;
            var c = comment.pageCount - 3 > comment.pageIndex ? comment.pageIndex + 3 : comment.pageCount;
            for (; i <= c; i++) {
                if (i == comment.pageIndex) {
                    html += "<span class=\"page_on\">" + i + "</span>";
                } else {
                    html += "<span><a href=\"###\" onclick=\"javascript:comment.LoadData(" + i + ")\">" + i + "</a></span>";
                }
            }
            html += "<span><a href=\"###\" onclick=\"javascript:comment.GotoPageNext()\">下一页</a></span>";
            html += "<span><a href=\"###\" onclick=\"javascript:comment.GotoPageLast()\">尾页&gt;&gt;</a></span>";
            html += "<span>" + comment.pageIndex + "/共" + comment.pageCount + "页</span>";
        }

        var tfoot = document.getElementById("page_box");
        $(tfoot).html(html);
    },

    //功能描述：获取行
    GetRow: function (data) {
        var html = "";

        html += "<ul class=\"pl_face fl\"><img width=\"45\" height=\"45\" src=\"/images/1.jpg\"></img></ul>";
        html += "<ul class=\"pl_user_say_r fl\">";
        html += "<li><dd><strong>" + (data.UserName || "匿名") + "</strong></dd>";
        html += "<dd><span>发表于 " + (data.CreateTime || "") + "</span></dd></li>";
        html += "<li>" + (data.Content || "") + "</li>";
        html += "<li><div class=\"thumb fl\"></div></li></ul>";
        html += "<ul class=\"pl_star_" + data.Score + "\"><li><dd class=\"s_" + (data.Score * 2) + " star db fl\"></dd>";
        html += "<div class=\"clear\"></div></li></ul>";
        html += "<div class=\"clear\"></div><div class=\"huifu\"><div>";
        html += "<a class=\"btn_reply\" onclick=\"javascript:comment.ReplyShow(this);\" href=\"javascript:void(0);\">回复</a></div>";
        html += "<div class=\"inputelem\" style=\"display: none;\">";
        html += "<input id=\"txtContent" + data.ID + "\" class=\"ipt\" type=\"text\" maxlength=\"500\"></input>";
        html += "<input class=\"btn ContentReply\" onclick=\"javascript:comment.SubmitReply(this);\" type=\"button\" value=\"提交\" index=\"" + data.ID + "\"></input>";
        html += "</div></div>";
        html += "<div class=\"clear\"></div><div class=\"replyContent\" style=\"_margin-top:10px;\">";
        html += "<div class=\"replyBox\"><ul>";
        html += comment.GetReply(data.CommentReplys);
        html += "<div class=\"clear\"></div>";
        html += "</ul>";
        html += "</div>";
        html += "<div class=\"replyMore\" style=\"display: none;\">";
        html += "<a href=\"javascript:void(0);\">" + "查看更多回复 >>" + "</a></div></div>";

        return html;
    },
    GetReply: function (data) {
        if (!data) return "";
        var html = "";
        for (var i = 0; i < data.length; i++) {
            html += "<li class=\"fl\">";
            html += "<div class=\"replyList\">";

            html += "<ul>";
            html += "<li class=\"fl num\">" + (i + 1) + "</li>";
            html += "<li class=\"fl txt\">";
            html += "<div>" + (data[i].UserName || "匿名") + "回复：" + (data[i].Content || "") + "</div>";
            html += "<div style=\" color:#999;\">" + (data[i].CreateTime || "") + "</div>";
            html += "</li>";
            html += "</ul>";

            html += "</div>";
            html += "</li>";
        }

        return html;
    }
}