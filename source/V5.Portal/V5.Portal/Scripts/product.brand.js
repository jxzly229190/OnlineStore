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

    var ids = [];
    $("#J_ItemList .product").each(function () {
        ids.push(Number($(this).attr("data") || "0"));
    });

    jQuery.ajax({
        type: "POST",
        dataType: "json",
        url: "/Product/GetProductListInfo",
        traditional: true,
        data: { "id": ids }, //"ids=" + data + "&random=" + Math.random(),
        success: function (result) {
            if (!result) return;
            if (result.length == 0) return;

            for (var i = 0; i < result.length; i++) {
                $("#G_" + result[i].ID).html("<b>￥</b>" + result[i].P);
                $("#S_" + result[i].ID).html(result[i].S + "笔");
                $("#C_" + result[i].ID).html(result[i].C);
                $("#A_" + result[i].ID).html(result[i].A);
            }
        }
    });
});