$(document).ready(function () {
    $("#feedsearch").click(function () {
        var filter = new Array();
        var grid = $("#FeedBackGrid").data("kendoGrid");
        grid.dataSource.filter(filter);
    });

});

function Criteria() {
    var feedType = $("#feedType").val();
    var gender = $("#gender").val();
    var keywords = $("#keywords").val();
    return {
        type: feedType,
        gender: gender,
        keyword: keywords
    };
}
//查看用户反馈详细信息
function LookView(id) {
    $.ajax({
        async: "false",
        type: "POST",
        url: "/Config/LookFeedBack",
        dataType: "html",
        data: { id: id },
        success: function (data) {
            $("#information").css("display", "block");
            $("#information").append(data);
            $("#feedlist").css("display", "none");
        }
    });
}