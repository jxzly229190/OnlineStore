$(function () {
    /*banner*/
    new SwitchTab({ "imgId": "banner-box",
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
    })
	//公告选项卡效果
		$('#gonggao .ant_title li').hover(function(){
		$('#gonggao .ant_title li').removeClass('cur');
		$(this).addClass('cur');
		$('#gonggao .ant_content').hide().eq($(this).index()).show();
	});
	//新品上市选项卡效果
		$('#newpro .limit_tab li').hover(function(){
		$('#newpro .limit_tab li').removeClass('limit_tab_cur');
		$(this).addClass('limit_tab_cur');
		$('#newpro .content_box').hide().eq($(this).index()).show();
	});
    
	//循环切换;
	$(".hd_scroll:eq(0)").Xslider({
		unitdisplayed:4,
		numtoMove:4,
		viewedSize:720,
		loop:"cycle"
	});
    $("a").focus(function(){this.blur();});
	
	//白酒频道选项卡效果
		$('#baijiu .baijiu_tab li').hover(function(){
		$('#baijiu .baijiu_tab li').removeClass('cur');
		$(this).addClass('cur');
		$('#baijiu .baijiu_content').hide().eq($(this).index()).show();
	});
	//品牌活动
		$('#pinpai .change').hover(function(){
		$('#pinpai .change').removeClass('cur');
		$(this).addClass('cur');
		$('#pinpai .brand_content').hide().eq($(this).index()).show();
	});
		//排行选项卡
		$('#paihang .rank_hd_tab li').hover(function(){
		$('#paihang .rank_hd_tab li').removeClass('cur');
		$(this).addClass('cur');
		$('#paihang .rank_cont').hide().eq($(this).index()).show();

	});
		//啤酒选项卡
		$('#pijiu .pijiu_right_tab li').hover(function(){
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
		var index_img=0;
			function sliimg(){
				if(index_img<0){index_img=($('.groupbuy .slidbox_cent li').length-1);}
				if(index_img>($('.groupbuy .slidbox_cent li').length-1)){index_img=0;}
				$('.groupbuy .group_content ul').hide().eq(index_img).fadeIn("slow");;
				$('.groupbuy .slidbox_cent li').removeClass('cur').eq(index_img).addClass('cur');
			}
			$('.groupbuy .slidbox_cent li').click(function(){
				index_img=$(this).index();
				sliimg()
			});
			setInterval(function(){sliimg(index_img++)},7000);
	
   

    /*倒计时*/
    countDown("cd_h_1", "cd_m_1", "cd_s_1", $("#stime_1").val(), $("#ntime_1").val());
    countDown("cd_h_2", "cd_m_2", "cd_s_2", $("#stime_2").val(), $("#ntime_2").val());
    countDown("cd_h_3", "cd_m_3", "cd_s_3", $("#stime_3").val(), $("#ntime_3").val());
    countDown("cd_h_4", "cd_m_4", "cd_s_4", $("#stime_4").val(), $("#ntime_4").val());

    /*更多优惠商品链接*/
    $("#favorable_more").click(function () {
        var url = $("#tab-tag").find(".on").attr("url");
        window.open(url, "_blank");
    })

    /*热点活动效果*/
    $(".brand_content img").each(function (index, elem) {
        $(elem).mouseover(function () {
            $(".brand_content img").stop().animate({ "opacity": 0.7 });
            $(elem).stop().animate({ "opacity": 1 });
        })
        $(elem).mouseout(function () {
            $(".brand_content img").stop().animate({ "opacity": 1 });
        })

    })


    /*右下角弹出广告*/
    var floatadWindowBox = function () {
        var floatadBoo = false;
        var fl_speed = 500;
        var countTime = 30000;
        var isIE6 = navigator.appVersion.indexOf("MSIE 6") > -1;
        if (isIE6) {
            $(".floatad_windowBox").css({ "position": "absolute" });
        }
        $(".floatad_windowBox").animate({ "bottom": "0" }, fl_speed);
        $("#floatad_resize").click(function () {
            floatadBoo = !floatadBoo;
            if (floatadBoo) {
                if (isIE6) {
                    var ay = parseInt($(".floatad_windowBox").css("top"));
                    if (ay != "auto") {
                        $(".floatad_windowBox").css({ "top": ay + 223, "height": 30 });
                    }
                }
                $(".floatad_windowBox").animate({ "bottom": "-223" }, fl_speed);
                $("#floatad_resize").addClass("floatad_resize_max");
                $("#floatad_resize").removeClass("floatad_resize_min");
            } else {
                if (isIE6) {
                    var ay2 = parseInt($(".floatad_windowBox").css("top"));
                    if (ay2 != "auto") {
                        $(".floatad_windowBox").css({ "top": ay2 - 223, "height": 250 });
                    }
                }
                $(".floatad_windowBox").animate({ "bottom": "0" }, fl_speed);
                $("#floatad_resize").addClass("floatad_resize_min");
                $("#floatad_resize").removeClass("floatad_resize_max");
            }
        })
        $(window).scroll(function () {
            var th = document.documentElement.clientHeight;
            var tw = document.documentElement.clientWidth;
            var sh = document.documentElement.scrollTop || document.body.scrollTop;
            if (isIE6) {
                var fh = floatadBoo ? 30 : $(".floatad_windowBox").height();
                var ty = th - fh + sh - 5;
                $(".floatad_windowBox").css({ "top": ty });
            }
        })

        $("#floatad_close").click(function () {
            closeFloatad();
        })
        function closeFloatad() {
            $(".floatad_windowBox").hide(fl_speed);
            setTimeout(function () {
                $(".floatad_windowBox").remove();
            }, fl_speed)
        }
    } ();
 
})

//品牌效果;
$(document).ready(function(){
	$('.baijiu_brand a').mouseover(function(){
		$(this).stop().animate({"top":"-50px"}, 200); 
	})
	$('.baijiu_brand a').mouseout(function(){
		$(this).stop().animate({"top":"0"}, 200); 
	})
})
	