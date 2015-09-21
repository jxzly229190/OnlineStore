//功能描述：获取URL参数
function fnGetUrlParam(url_param) {
    var url_param = (url_param || "").replace(/^\?/, '');
    if (url_param == "") { return; }

    url_param = "{\"" + url_param.replace(/&/g, "\",\"").replace(/=/g, "\":\"") + "\"}";
    try {
        var arr_param = JSON.parse(url_param);
    } catch (e) {
        alert("参数异常~!" + url_param);
        return null;
    }

    for (var param in arr_param) {
        if (arr_param[param]) {
            arr_param[param] = unescape(arr_param[param]);
        }
    }
    return arr_param;
}

$(function () {
    $(".j_More").toggle(function () {
        $(this).parent().parent().find(".av-collapse").removeClass("av-collapse").addClass("av-expand av-scroll");
        $(this).removeClass("ui-more-drop-l").addClass("ui-more-expand-l");
        $(this).html("收起<i class=\"ui-more-expand-l-arrow\"></i>");
    },
    function () {
        $(this).parent().parent().find(".av-expand").removeClass("av-expand av-scroll").addClass("av-collapse");
        $(this).removeClass("ui-more-expand-l").addClass("ui-more-drop-l");
        $(this).html("更多<i class=\"ui-more-drop-l-arrow\"></i>");
    }).html("更多<i class=\"ui-more-drop-l-arrow\"></i>");
    
    var url_param = fnGetUrlParam(window.location.search);
    var w = url_param ? url_param.w : "";
    if (w) {
        $("#sch").val(w);
        $("#keyword").html(w);
    }
});