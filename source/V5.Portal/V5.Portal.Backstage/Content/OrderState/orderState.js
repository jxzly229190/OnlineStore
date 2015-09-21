//进度条控件
var progress = {
    init: function (option) {
        if (!option) return "参数错误";
        if (!option.objContainer) return "参数错误";
        if (!(option.objContainer.tagName && option.objContainer.tagName.toLowerCase() == "div")) return "控件容器必须是div";
        if (!option.objParamter) return "参数错误";
        if (!(option.objParamter.length && option.objParamter.length > 0)) return "参数错误";

        var html = "";
        for (var i in option.objParamter) {
            var item = option.objParamter[i];
            if (item) {
                switch (item.type || "") {
                    case "node":
                        html += progress.create_node(item);
                        break;
                    case "proce":
                        html += progress.create_proce(item);
                        break;
                }
            }
        }
        option.objContainer.innerHTML = html;
        option.objContainer.style.width = option.objParamter.length * 80 + "px";
        return "success";
    },

    create_node: function (item) {
        var html = "<div class=\"node " + (item.status || "") + "\">";
        html += "<ul>";
        html += "<li class=\"tx1\">&nbsp;</li>";
        html += "<li class=\"tx2\">" + (item.desc || "") + "</li>";
        html += "<li id=\"track_time_0\" class=\"tx3\">" + progress.format_time(item.time, "yyyy-mm-dd") + " <br> " + progress.format_time(item.time, "hh:mm:ss") + "</li>";
        html += "</ul>";
        html += "</div>";
        return html;
    },

    create_proce: function (item) {
        var html = "";
        html += "<div class=\"proce " + (item.status || "") + "\">";
        html += "<ul>";
        html += "<li class=\"tx1\">&nbsp;</li>";
        html += "</ul>";
        html += "</div>";
        return html;
    },

    format_time: function (time, format) {
        var result = "";

        if (!time) return "";
        if (time.split(/\s+/).length < 2) return "";

        format = format || "";
        switch (format) {
            case "yyyy-mm-dd":
                result = time.split(/\s+/)[0];
                break;
            case "hh:mm:ss":
                result = time.split(/\s+/)[1];
                break;
        }
        return result;
    },

    create_paramter: function (option) {
        if (!option) return;
        if (!option.time && option.time.length == 0) return;

        var status_desc = null;
        switch (option.type) {
            case "0":
                status_desc = [{ status: 0, desc: "提交订单" }, { status: 1, desc: "等待付款" }, { status: 2, desc: "商品出库" }, { status: 3, desc: "等待收货" }, { status: 4, desc: "完成"}];
                break;
            case "1":
                status_desc = [{ status: 0, desc: "提交订单" }, { status: 2, desc: "商品出库" }, { status: 3, desc: "等待收货" }, { status: 4, desc: "完成"}];
                break;
        }
        if (status_desc == null) return;

        var paramter = [];
        for (var i = 0; i < status_desc.length; i++) {
            var node_status = "";
            var proce_status = "";
            if (option.status > status_desc[i].status) {
                node_status = "ready";
                proce_status = "ready";
            } else if (option.status == status_desc[i].status) {
                node_status = "ready";
                proce_status = "doing";
            } else {
                node_status = "wait";
                proce_status = "wait";
            }
            paramter.push({ type: "node", status: node_status, desc: status_desc[i].desc, time: option.time[i] });
            if (i < status_desc.length - 1) {
                paramter.push({ type: "proce", status: proce_status, desc: status_desc[i].desc, time: option.time[i] });
            }
        }
        return paramter;
    }
}
