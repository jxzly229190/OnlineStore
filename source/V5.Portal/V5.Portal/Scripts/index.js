$(function () {
    var ids = [];
    $(".product").each(function () {
        ids.push(Number($(this).attr("data") || "0"));
    });

    $.ajax({
        type: "POST",
        dataType: "json",
        url: "/Home/GetProductListInfo",
        traditional: true,
        data: { "id": ids }, //"ids=" + data + "&random=" + Math.random(),
        success: function (result) {
            if (!result) return;
            if (result.length == 0) return;

            for (var i = 0; i < result.length; i++) {
                $("#G_" + result[i].ID).html(result[i].P);
                $("#S_" + result[i].ID).html(result[i].S);
                $("#C_" + result[i].ID).html(result[i].C);
                $("#A_" + result[i].ID).html(result[i].A);
                $("#M_" + result[i].ID).html(result[i].M);
            }
        }
    });
    
    //本月热销
    $(".limit_content li").click(function () {
        var url = $(this).attr("url") || "";
        window.open(url, "_blank");
    });

    //品牌顺序
    $(".rank_cont").each(function () {
        var i = 0;
        $(this).find(".rank_cont_rank_order").each(function () {
            $(this).addClass("rank_cont_rank_order_" + (++i));
        })
    });

    $('.baijiu_content ul').last().css('border', 'none');
    $('.group_content ul').last().css('display', 'block');

    i = 0;
    $(".limit_content li").each(function () {
        $(this).addClass("li" + (i % 2 + 1));
        i++;
    })
    $(".brand_content ul").each(function () {
        $(this).find("li:last").addClass("last");
    });

    //顶部广告
    var len = $("#banner-box li").length;
    var html = "";
    for (var i = 0; i < len; i++) {
        html += "<span class=\"\" index=\"" + i + "\">" + i + "</span>";
    }
    $("#banner-btn").html(html);

    new SwitchTab({
        "imgId": "banner-box",
        "btnId": "banner-btn",
        "time": 5000,
        "speed": 600,
        "autoPlay": true,
        "effect": "opacity",
        "activeClass": "on"
    });
    /*banner链接*/
    $(".banner-btn-box").click(function () {
        var id = parseInt($(this).find(".on").attr("index").toString());
        var url = $($("#banner-box li")[id]).attr("url");
        window.open(url, "_blank");
    });

    //图片Banner
    $(".hd_scroll:eq(0)").Xslider({
        unitdisplayed: 4,
        numtoMove: 3,
        viewedSize: 720,
        loop: "cycle",
        speed: 600 //动画速度
    });
    $("a").focus(function () { this.blur(); });

    //品牌
    $('.baijiu_brand a').mouseover(function () {
        $(this).stop().animate({ "top": "-50px" }, 200);
    })
    $('.baijiu_brand a').mouseout(function () {
        $(this).stop().animate({ "top": "0" }, 200);
    })

    //最新公告和促销信息
    $('#gonggao .ant_title li').hover(function () {
        $('#gonggao .ant_title li').removeClass('cur');
        $(this).addClass('cur');
        $('#gonggao .ant_content').hide().eq($(this).index()).show();
    });

    //新品上市
    $('#newpro .limit_tab li').hover(function () {
        $('#newpro .limit_tab li').removeClass('limit_tab_cur');
        $(this).addClass('limit_tab_cur');
        $('#newpro .content_box').hide().eq($(this).index()).show();
    });

    //白酒频道
    $('#baijiu .baijiu_tab li').hover(function () {
        $('#baijiu .baijiu_tab li').removeClass('cur');
        $(this).addClass('cur');
        $('#baijiu .baijiu_content').hide().eq($(this).index()).show();
    });

    /*品牌活动
    $('#pinpai .change').hover(function () {
    $('#pinpai .change').removeClass('cur');
    $(this).addClass('cur');
    $('#pinpai .brand_content').hide().eq($(this).index()).show();
    });
    */

    //排行选项卡
    $('#paihang .rank_hd_tab li').hover(function () {
        $('#paihang .rank_hd_tab li').removeClass('cur');
        $(this).addClass('cur');
        $('#paihang .rank_cont').hide().eq($(this).index() / 2).show();
    });

    //啤酒选项卡
    $('#pijiu .pijiu_right_tab li').hover(function () {
        $('#pijiu .pijiu_right_tab li').removeClass('cur');
        $(this).addClass('cur');
        $('#pijiu .pijiu_right_cont').hide().eq($(this).index()).show();
    });

    //保健酒选项卡
    $('#baojianjiu .pijiu_right_tab li').hover(function () {
        $('#baojianjiu .pijiu_right_tab li').removeClass('cur');
        $(this).addClass('cur');
        $('#baojianjiu .pijiu_right_cont').hide().eq($(this).index()).show();
    });

    //团购轮播;
    var index_img = 0;

    function sliimg() {
        if (index_img < 0) {
            index_img = ($('.groupbuy .slidbox_cent li').length - 1);
        }
        if (index_img > ($('.groupbuy .slidbox_cent li').length - 1)) {
            index_img = 0;
        }
        $('.groupbuy .group_content ul').hide().eq(index_img).fadeIn("slow");
        ;
        $('.groupbuy .slidbox_cent li').removeClass('cur').eq(index_img).addClass('cur');
    }

    $('.groupbuy .slidbox_cent li').click(function () {
        index_img = $(this).index();
        sliimg()
    });
    setInterval(function () { sliimg(index_img++) }, 7000);

    var imglist = $("#imglist");
    $("#pre").click(function () {
        var top = imglist.position().top - 120;
        if (!imglist.is(":animated")) {
            imglist.animate({ top: top + "px" }, 600, function () {
                imglist.css({ top: 0 }).find("a:first").appendTo(imglist);
            })
        }
    });

    $("#next").click(function () {
        var top = imglist.position().top - 120;
        if (!imglist.is(":animated")) {
            imglist.animate({ top: top + "px" }, 600, function () {
                imglist.css({ top: 0 }).find("a:first").appendTo(imglist);
            })
        }
    });

    //换一批
    $("#brand_change").click(function () {
        $.ajax({
            type: "POST",
            url: "/Home/GetAdvertiseHtml_Brand?t=" + Math.random(),
            dataType: "html",
            success: function (data) {
                if (!data || data == "") return;
                $("#brand_content_inner").html(data);
            },
            error: function () {
                try {
                    conslog.log("请求失败，请联系系统管理员！");
                } catch (e) {
                }
            }
        });
    });
});