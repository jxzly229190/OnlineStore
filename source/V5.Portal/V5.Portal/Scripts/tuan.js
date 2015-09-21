var team = {
    container: Object,
    id: Number,
    name: String,
    advertisement: String,
    marketprice: String,
    goujiuprice: String,
    soldofvirtual: Number,
    pageview: Number,
    iamgepath: String,
    thumbnailpath: String,
    time: String,

    //功能描述：初始化
    Init: function (option) {
        if (!team.SetOption(option)) {
            alert("参数错误!");
            return;
        }

        team.LoadHtml(option);
        team.LoadEvent();
    },

    //功能描述：设置参数
    SetOption: function (option) {
        if (!option) return false;
        team.container = document.getElementById(option.container);
        team.id = option.id;
        team.name = option.name;
        team.advertisement = option.advertisement;
        team.marketprice = option.marketprice;
        team.goujiuprice = option.goujiuprice;
        team.soldofvirtual = option.soldofvirtual;
        team.pageview = option.pageview;
        team.thumbnailpath = option.thumbnailpath || "";
        //team.GetIamgeUrl(option);
        return true;
    },

    //功能描述：加载事件
    LoadEvent: function () {
        $("#buy").click(function () {
            var productId = $("#productId").val();
            if (isNaN(productId)) return;

            //添加到购物车
            Cat.Add(productId);
        });

        //推荐切换
        $('.tabbtn b').length == 3 && $('.tabbtn b:last,#zuhe').hide(); //组合临时隐藏

        $('.tabbtn b').mouseenter(function (e) {
            $(this).addClass('down').siblings('b').removeClass('down');
            $('.mptabc').hide(0); //IE8 幽灵BUG 处理
            $('.mptabc').eq($(this).index()).show();
            btnSlide('.mptabc', '.b-l', '.b-r', 'div', 'dl');
        });


        //推荐滚动
        btnSlide('.mptabc', '.b-l', '.b-r', 'div', 'dl');
        function btnSlide(mod, lbtn, rbtn, cent, label) {
            $(mod).each(function () {
                var _this = $(this);
                var o = _this.find(cent);
                if ($(_this).is(':hidden') || $(_this).find(lbtn).data("events") || o.find(label).length < 4) {
                    return true;
                }
                var lock = false;
                var n = o.find(label).length;
                var w = o.find(label).outerWidth(true);
                var i = _this.outerWidth(true) / w;
                o.append(o.html());
                _this.find(lbtn).hover(function () {
                    $(this).css('background-position', '-278px -421px');
                }, function () {
                    $(this).removeAttr('style');
                });
                _this.find(rbtn).hover(function () {
                    $(this).css('background-position', '-298px -421px');
                }, function () {
                    $(this).removeAttr('style');
                });
                _this.find(lbtn + ',' + rbtn).one('click', function () {
                    _this.find('img').each(function () {
                        $(this).attr('load') && $(this).attr('src', $(this).attr('load')).removeAttr('load');
                    });
                });
                _this.find(lbtn).click(function () {
                    if (lock) {
                        return false;
                    }
                    lock = true;
                    o.find(label + ':gt(' + (n * 2 - 5) + ')').prependTo(o);
                    o.css({ left: -w * 4 }).animate({ left: 0 }, 'slow', function () {
                        lock = false;
                    });
                    return false;
                });
                _this.find(rbtn).click(function () {
                    if (lock) {
                        return false;
                    }
                    lock = true;
                    o.css({ left: 0 }).animate({ left: -w * 4 }, 'slow', function () {
                        o.append(o.find(label + ':lt(4)')).css({ left: 0 });
                        lock = false;
                    });
                    return false;
                });
            });
        }

        $("#hotproduct").children("p").each(function (i, obj) {
            if (i < 2) {
                $(obj).remove();
            } else {
                var html = $(obj).html();
                html = html.replace("#Index#", i + 1);
                $(obj).html(html);
            }
        });

        $("#hotproduct").children("dl").each(function (i, obj) {
            var html = $(obj).html();
            html = html.replace("#Index#", i + 1);
            $(obj).html(html);
            if (i < 2) {
                $(obj).removeClass("hovershow");
            } else {
                $(obj).hide();
            }
        });


        //热销排行效果
        $('#hotproduct p').live("mouseenter", function () {
            $('#hotproduct .hovershow').hide().next().show();
            $(this).hide().prev().show();
        });



        //计时器
        Timer.Init(".silder_column_time .time");
    },

    //功能描述：加载数据
    LoadData: function (opt) {
        if (!opt.url || opt.url == "") return;

        opt.type = opt.type || "POST";
        opt.dataType = opt.dataType || "json";
        opt.async = opt.async == false ? false : true;
        $.ajax({
            type: opt.type,
            url: opt.url,
            data: opt.data,
            dataType: opt.dataType,
            async: opt.async,
            success: function (result) {
                if ($.isFunction(opt.fn)) {
                    opt.fn(result);
                }
            },
            error: function () {
                alert("处理失败!");
            }
        });
    },

    // 功能描述：加载Html
    LoadHtml: function (data) {
        var html = " ";
        html += "<div class=\"silder_column_item\" style=\"display:block;\">";
        html += "<div class=\"silder_column_image\" style=\"background-image:url(" + team.thumbnailpath + ");\"></div>";
        html += "<ul class=\"silder_column_intro\">";
        html += "<li class=\"silder_column_name\"><i class=\"image_group\"></i><span>" + data.name + "</span></li>";
        html += "<li class=\"silder_column_desc\">" + data.advertisement + "</li>";
        html += "<li class=\"silder_column_price\">";
        html += "<div class=\"goujiu_price\">￥<span>" + data.goujiuprice + "</span></div>";
        html += "<div class=\"goujiu_discount\">";
        html += "<div class=\"discount\"><span>" + (data.goujiuprice * 10.00 / data.marketprice).toFixed(1) + "</span>折</div>";
        html += "<div class=\"market_price\">￥<span>" + data.marketprice + "</span></div>";
        html += "</div>";
        html += "<div class=\"split\"></div>";
        html += "<a href=\"###\" class=\"image_group buy\" id=\"buy\"></a>";
        html += "<div class=\"clear\"></div>";
        html += "</li>";
        html += "<li class=\"silder_column_time\">";
        html += "<i class=\"image_group\"></i>";
        html += "<div class=\"time\" time=\"@home.GetTime().ToString()\"></div>";
        html += "<div class=\"clear\"></div>";
        html += "</li>";
        html += "<li class=\"silder_column_other\"><i class=\"image_group\"></i>";
        html += "<div class=\"attention\">共有<span class=\"attention_count\">" + data.pageview + "</span>人关注</div>";
        html += "<div class=\"buyer\"><span class=\"buyer_count\">" + data.soldofvirtual + "</span>人购买</div>";
        html += "<div class=\"clear\"></div>";
        html += "</li>";
        html += "</ul>";
        html += "<div class=\"clear\"></div>";
        html += "</div>";

        team.container.innerHTML = html;
    },

    GetIamgeUrl: function (data) {
        var opt = {};
        opt.url = "/Home/GetTuanImagePath";
        opt.dataType = "text";
        opt.data = { productId: data.id, thumbnailpath: data.thumbnailpath };
        opt.async = false;
        opt.fn = function (result) {

            team.iamgepath = result || "";
        };

        //加载数据
        team.LoadData(opt);
    },

    GetTime: function (data) {

    }
};