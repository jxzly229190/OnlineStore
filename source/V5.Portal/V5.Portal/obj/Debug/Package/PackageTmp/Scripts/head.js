/*品牌列表*/
var brandArea = function () {
    $(".brand_class li").each(function (i) {
        $(this).mouseenter(function () {
            $(this).find("div").stop().animate({ "top": -50 }, 300);
        })
        $(this).mouseleave(function () {
            $(this).find("div").stop().animate({ "top": 0 }, 300);
        })
    });
}

$("#head_cart_no").load("/Cart/GetCartInfo");
var cateMenu = function () {
    var cateLiNum = $(".cateMenu li").length;
    $(".cateMenu li").each(function (index, element) {
        if (index < cateLiNum - 1) {
            $(this).mouseenter(function () {
                var ty = $(this).offset().top - 158;
                var obj = $(this).find(".list-item");
                var sh = document.documentElement.scrollTop || document.body.scrollTop;
                var oy = ty + (obj.height() + 30) + 158 - sh;
                var dest = oy - $(window).height()
                if (oy > $(window).height()) {
                    ty = ty - dest - 10;
                }
                if (ty < 0) ty = 0;
                $(this).addClass("on");
                obj.show();
                $(".cateMenu li").find(".list-item").stop().animate({ "top": ty });
                obj.stop().animate({ "top": ty });
            })
            $(this).mouseleave(function () {
                $(this).removeClass("on");
                $(this).find(".list-item").hide();
            })
        }
    });

    //判断是否为首页
    if (window.website_default == true) {
        $(".cateMenu").show();
    } else {
        $(".navCon_on").hover(function () {
            $(".cateMenu").show();
            $(".navCon-cate-title").addClass("hover");
        },
	    function () {
	        $(".cateMenu").hide();
	        $(".navCon-cate-title").removeClass("hover");
	    });
    }
}

$(function () {
    //弹出菜单
    cateMenu();

    //Mini购物内容
    try {
        $("#head_cart_no").load("/Cart/GetCartInfo/d=" + new Date().toDateString());
    } catch (ex) {
    }
    
    var miniMenu = function () {
        /*购物列表*/
        $(".miniMenu").find(".m1").hover(
			function () {
			    $(this).addClass("on");
			    $(this).find(".mini-cart").show();
			    $("#head_cart").load("/Cart/ShoppingCart");
			},
			function () {
			    $(this).removeClass("on");
			    $(this).find(".mini-cart").hide();
			}
		)
        /*用户中心*/
        $(".miniMenu").find(".m3").hover(
			function () {
			    $(this).addClass("cur");
			    $(this).find(".miniMenu-child").show();
			},
			function () {
			    $(this).removeClass("cur");
			    $(this).find(".miniMenu-child").hide();
			}
		)
    } ();

    /*topBar置顶*/
    var positionMenu = function(id) {
        var mc = document.getElementById(id);
        var minNumber = mc.offsetTop;
        var isIE6 = navigator.appVersion.indexOf("MSIE 6") > -1;

        $(window).scroll(function() {
            var sh = document.documentElement.scrollTop || document.body.scrollTop;
            var th = document.documentElement.clientHeight;
            if (sh > minNumber) {
                mc.style.position = !isIE6 ? "fixed" : "absolute";
                mc.style.top = !isIE6 ? "0px" : sh + "px";
            } else {
                mc.style.position = "static";
                mc.style.top = minNumber + "px";
            }
        });
    }("topBar");

    /*tabs置顶*/
    /*
    var positionMenu = function (id) {
    var mc = document.getElementById(id);
    var minNumber = mc.offsetTop;
    var isIE6 = navigator.appVersion.indexOf("MSIE 6") > -1;

    $(window).scroll(function () {
    var sh = document.documentElement.scrollTop || document.body.scrollTop;
    var th = document.documentElement.clientHeight;
    if (sh > minNumber) {
    mc.style.position = !isIE6 ? "fixed" : "absolute";
    mc.style.top = !isIE6 ? "30px" : sh + "px";
    } else {
    mc.style.position = "static";
    mc.style.top = minNumber + "px"
    }
    })
    } ("scrip_lin")
    */
})