// JavaScript Document

//产品搜索
var productSearch = {
    //参数描述：延迟时间
    timeout: Number,

    //参数描述：搜索建议
    suggest: {
        id: String,
        value: String
    },

    //功能描述：初始化
    Init: function (opt) {
        productSearch.timeout = opt.timeout || 300;
        productSearch.LoadEvent();
    },

    //功能描述：加载事件
    LoadEvent: function () {
        if ($("#sch").length == 0) return;
        if ($("#btnsch").length == 0) return;

        $(document).click(function () {
            $(".search_suggest").hide();
        });
        $(document).keydown(productSearch.KeyDownEvent);
        $(document).keyup(productSearch.KeyupEvent);
        $("#sch").keyup(productSearch.KeyupEvent);
        $("#btnsch").click(function () {
            var keyword = $("#sch").val();
            if (productSearch.Check(keyword)) {
                window.location.href = "/Search?w=" + escape(keyword);
            } else {
                $("#sch").val("");
            }
        });
    },

    //功能描述：键盘事件
    KeyDownEvent: function (event) {
        if ($(".search_suggest").is(":hidden")) {
            return;
        }

        var obj = $(".search_suggest .selected");
        var keyCode = event.keyCode || "";
        if (keyCode >= 37 && keyCode <= 40) {
            if (obj.length == 0) {
                $(".search_suggest a:first").addClass("selected");
                return;
            }
            switch (keyCode) {
                case 37: //键盘中左键(←)
                case 38: //键盘中左键(↑)
                    obj.prev().addClass("selected");
                    obj.removeClass("selected");
                    $("#sch").val(obj.prev().text());
                    break;
                case 39: //键盘中右键(→)
                case 40: //键盘中下键(↓)
                    obj.next().addClass("selected");
                    obj.removeClass("selected");
                    $("#sch").val(obj.next().text());
                    break;
            }
            return false;
        } else if (keyCode == 13 || keyCode == 108) {
            if (obj.length > 0) {
                window.location.href = obj.attr("href");
            } else {
                var keyword = $("#sch").val();
                if (productSearch.Check(keyword)) {
                    window.location.href = "/Search?w=" + escape(keyword);
                } else {
                    $("#sch").val("");
                }
            }
        }
    },

    //功能描述：键盘事件
    KeyupEvent: function (event) {
        var keyCode = event.keyCode || "";
        var flag = false;

        if (keyCode >= 48 && keyCode <= 57) { //数字
            flag = true;
        } else if (keyCode >= 65 && keyCode <= 90) { //字母
            flag = true;
        } else if (keyCode >= 96 && keyCode <= 105) { //字母
            flag = true;
        } else if (keyCode == 8 || keyCode == 32) { //退格键、空格键
            flag = true;
        } else if (keyCode == 13 || keyCode == 108) { //回车键
            flag = true;
        } else {
            return;
        }

        if (flag == true) {
            if (keyCode == 13 || keyCode == 108) {
                productSearch.LoadSuggest(true);
            } else {
                productSearch.LoadSuggest()
            }
        }
    },

    //功能描述：加载搜索建议数据
    LoadSuggestData: function (fvalue) {
        if (!productSearch.Check(fvalue)) {
            $("#sch").val("");
        }
        productSearch.suggest.value = fvalue;
        $.ajax({
            type: "POST",
            url: "/Search/Suggest",
            data: "search=" + fvalue,
            datatype: "json",
            success: function (data) {
                if (!data) return;
                if (data.length == 0) return;

                var html = "", item = "";
                var reg = new RegExp(fvalue, "g");
                for (var i = 0; i < data.length; i++) {
                    item = data[i].Name || "";
                    item = item.replace(reg, "<span class='keyword'>" + fvalue + "</span>");
                    html += "<a href='/product/item-id-" + data[i].ID + ".htm' >" + item + "</a>";
                }
                if (html == "") return;
                $(".search_suggest").html(html);
                $(".search_suggest").show();
            },
            error: function () {
                try {
                    console.log("请求失败，请联系系统管理员！");
                } catch (e) {
                    //
                }
            }
        });
    },

    //功能描述：加载建议
    LoadSuggest: function (flag) {
        clearTimeout(productSearch.suggest.id);

        var fvalue = $("#sch").val() || "";
        if (fvalue == null || fvalue == "") {
            $(".search_suggest").hide();
            return;
        }
        fvalue = fvalue.replace(/^\s+|\s+$/, "");
        if (productSearch.suggest.value == fvalue) return;

        if (flag == true) {
            productSearch.LoadSuggestData(fvalue);
        } else {
            productSearch.suggest.id = setTimeout(function () {
                productSearch.LoadSuggestData(fvalue);
            }, 100);
        }
    },

    //功能描述：
    Check: function (keyword) {
        keyword = keyword || "";
        if (keyword == "") return false;
        var re = /^\?(.*)(select%20|insert%20|delete%20from%20|count\(|drop%20table|update%20truncate%20|asc\(|mid\(|char\(|xp_cmdshell|exec%20master|net%20localgroup%20administrators|\"|:|net%20user|\''''|%20or%20)(.*)$/gi;
        var e = re.test(escape(keyword));
        if (e) {
            alert("含有非法字符～");
            return false;
        }
        return true;
    }
}

//弹出菜单
var CateMenu = {    
    //功能描述：初始化
    Init:function(){
        var length = $(".cateMenu li").length;

        //注册事件
        $(".cateMenu li").each(function (index, element) {
            if (index >= length - 1) return;

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
            }).mouseleave(function () {
                $(this).removeClass("on");
                $(this).find(".list-item").hide();
            });
        });

        //非首页隐藏菜单
        if (window.website_default == true) {
            $(".cateMenu").show();
        } else {
            $(".navCon_on").hover(function () {
                $(".cateMenu").show();
                $(".navCon-cate-title").addClass("hover");
            }, function () {
                $(".cateMenu").hide();
                $(".navCon-cate-title").removeClass("hover");
            });
        }
    }
}

//顶部导航条
var Navigation = {
    //功能描述：初始化
    Init: function () {
        Navigation.LoadEvent();
        Cat.Load();
    },

    //功能描述：加载事件
    LoadEvent: function () {
        if ($.cookie("L")) {
            if ($.cookie("L") == "N") {
                $("#top_login").html("<a class='login-btn' href='/Login/index' target='_self'>登录</a><a class='reg-btn' href='/Login/register' target='_self'>快速注册</a>");
            } else {
                $("#top_login").html($.cookie("L"));
            }
        } else {
            $.post("/Base/GetLoginHtml", null, function (data) {
                if (data.indexOf("login-name") >= 0) {
                    $.cookie("L", data, { path: "/" });
                } else {
                    $.cookie("L", "N", { path: "/" });
                }
                $("#top_login").html(data);
            });
        }

        //购物列表
        $(".miniMenu").find(".m1").hover(
            function () {
                $(this).addClass("on");
                $(this).find(".mini-cart").show();
                if (Cat.isChanged == true) {
                    $("#head_cart").load("/Cart/ShoppingCart");
                    Cat.isChanged = false;
                    Cat.LoadServer();
                }
            },
            function () {
                $(this).removeClass("on");
                $(this).find(".mini-cart").hide();
            }
        );

        //用户中心
        $(".miniMenu").find(".m3").hover(
            function () {
                $(this).addClass("cur");
                $(this).find(".miniMenu-child").show();
            },
            function () {
                $(this).removeClass("cur");
                $(this).find(".miniMenu-child").hide();
            }
        );
    },

    //功能描述：退出登录
    LoginOut: function () {
        $.post("/Login/Exit", null, function (data) {
            if (data.State == 1) {
                //删除cookie
                $.cookie("L", null, { expires: -1, path: '/' });
                $.cookie("C", null, { expires: -1, path: '/' });

                location.reload();
            }
        });
    }
};

//页面访问量
var PageView = {
    //功能描述：初始化
    Init: function() {

    },

    //功能描述：访问统计
    Load: function() {

    }
};

//购物车
var Cat = {
    isChanged: BOOL = true,

    //功能描述：Lp加入购物车
    Add2: function (productId, quantity) {
        if (isNaN(productId)) return;
        if (productId < 1) return;
        Cat.isChanged = true;

        var data = { quantity: quantity || 1, productId: productId };

        $.post("/Cart/Add", data, function (result) {
            if (result.State == 1) {
                if ($.cookie("C")) {
                    $.cookie("C", parseInt($.cookie('C')) + parseInt(data.quantity), { path: "/" });
                } else {
                    $.cookie("C", parseInt(data.quantity), { path: "/" });
                }

                Cat.Load();
                alert("成功加入购物车");
                return false;
            } else if (result.State == 0) {
                if ($.cookie("C")) {
                    $.cookie("C", parseInt($.cookie('C')) + parseInt(result.Data), { path: "/" });
                } else {
                    $.cookie("C", parseInt(result.Data), { path: "/" });
                }
                alert(result.Message);
                return false;
            } else if (result.State == 15) {
                alert(result.Message);
                window.location.href = "/login/index?backurl=" + document.URL;
            } else if (result.State == 13) {
                alert(result.Message);
                Cat.LoadMobileVerify(productId);
                setTimeout(Cat.OpenMobile(), 1000);
            } else {
                if (result.Message) {
                    alert(result.Message);
                } else {
                    alert('操作失败');
                }
            }
        });
    },

    //功能描述：加入购物车
    Add: function (productId, quantity) {
        if (isNaN(productId)) return;
        if (productId < 1) return;
        Cat.isChanged = true;

        var data = { quantity: quantity || 1, productId: productId };

        $.post("/Cart/Add", data, function (result) {
            if (result.State == 1) {
                if ($.cookie("C")) {
                    $.cookie("C", parseInt($.cookie('C')) + parseInt(data.quantity), { path: "/" });
                    // CookieManager.Set({ name: 'C', value: parseInt($.cookie('C')) + parseInt(data.quantity), minutes: 5, path: "/" });
                } else {
                    $.cookie("C", parseInt(data.quantity), { path: "/" });
                    // CookieManager.Set({ name: 'C', value: data.quantity, minutes: 5, path: "/" });
                }

                Cat.Load();
                Cat.Show();
                Cat.Close();
                return false;
            } else if (result.State == 0) {
                if ($.cookie("C")) {
                    $.cookie("C", parseInt($.cookie('C')) + parseInt(result.Data), { path: "/" });
                    // CookieManager.Set({ name: 'C', value: parseInt($.cookie('C')) + result.Data, minutes: 5, path: "/" });
                } else {
                    $.cookie("C", parseInt(result.Data), { path: "/" });
                    // CookieManager.Set({ name: 'C', value: result.Data, minutes: 5, path: "/" });
                }
                alert(result.Message);
                return false;
            } else {
                if (result.Message) {
                    alert(result.Message);
                } else {
                    alert('操作失败');
                }
            }
        });
    },

    //功能描述：刷新购物车
    Load: function () {
        if ($.cookie("C")) {
            $("#head_cart_no").html($.cookie("C"));
        } else {
            Cat.LoadServer();
        }
    },

    //功能描述：刷新购物车
    LoadServer: function () {
        $.get("/Cart/GetCartInfo", null, function (result) {
            $.cookie("C", parseInt(isNaN(result) ? "0" : result), { path: "/" });
            $("#head_cart_no").html($.cookie("C"));
        });
    },

    //功能描述：关闭购物车
    Close: function () {
        $("#cat_pop1").find(".close").click(function () {
            $("#cat_pop1").fadeOut("slow");
        });

        setTimeout(function () {
            $("#cat_pop1").fadeOut("slow");
        }, 5000);
    },

    //功能描述：显示购物车
    Show: function () {
        Cat.LoadCatPop();

        var tx = ($(window).width() - $("#cat_pop1").width()) / 2 + $("#cat_pop1").width() / 2;
        var ty = ($(window).height() - $("#cat_pop1").height()) / 2 + $(document).scrollTop();
        $("#cat_pop1").css({ "left": tx, "top": ty });
        $("#cat_pop1").show();
    },

    //功能描述：加载购物车提醒
    LoadCatPop: function () {
        if ($("#cat_pop1").length > 0) return;

        var html = "";
        html += "<div class=\"pop_out\" id=\"cat_pop1\" style=\"left: 674.5px; top: 572.5px; display: none;\">";
        html += "    <div class=\"pop_in\">";
        html += "        <div class=\"pop_top\" style=\"border-bottom: 1px solid #fff;\">";
        html += "            <div class=\"close\">关闭</div>";
        html += "        </div>";
        html += "        <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">";
        html += "            <tbody>";
        html += "                <tr>";
        html += "                    <td colspan=\"2\" align=\"center\" height=\"50\" valign=\"middle\">";
        html += "                        <span class=\"pop_inf\" style=\"color: #000; font-size: 14px;\">商品已成功添加到购物车</span>";
        html += "                    </td>";
        html += "                </tr>";
        html += "                <tr>";
        html += "                    <td align=\"right\" height=\"30\" valign=\"middle\" width=\"70%\">";
        html += "                        <a class=\"pop_btn\" href=\"/Cart\"></a>";
        html += "                    </td>";
        html += "                    <td align=\"left\" valign=\"middle\" width=\"30%\">";
        html += "                        <a class=\"info_a\" id=\"Continue\" style=\"cursor: pointer;\" href=\"javascript:\">继续逛&gt;&gt;</a>";
        html += "                    </td>";
        html += "                </tr>";
        html += "                <tr>";
        html += "                    <td colspan=\"2\" align=\"center\" height=\"20\" valign=\"middle\">";
        html += "                    </td>";
        html += "                </tr>";
        html += "            </tbody>";
        html += "        </table>";
        html += "    </div>";
        html += "</div>";

        $(document.body).append(html);
    },
    // 加载手机验证html
    LoadMobileVerify: function (productid) {
        if ($("#maskLayer").length > 0) return;
        var html = "";
        html += " <div class=\"maskLayer\" id=\"maskLayer\">";
        html += "<input type=\"hidden\" id=\"freeID\" value=\"" + productid + "\" />";
        html += "     <div class=\"dialogBox\" id=\"dialogBox\">";
        html += "       <table width=\"100%%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">";
        html += "         <tr>";
        html += "           <td height=\"30\">&nbsp;</td>";
        html += "           <td align=\"right\" valign=\"top\" class=\"closeDialog\"><span><a href=\"javascript:Cat.GetInterval();\">关闭</a></span></td>";
        html += "         </tr>";
        html += "         <tr>";
        html += "           <td align=\"right\">手机号码：</td>";
        html += "           <td><input type=\"text\" maxlength=\"11\" id=\"txtMob\" name=\"txtOldPass\" class=\"input_int\" onkeyup=\"value=value.replace(/[^\\d]/g,'')\" /></td>";
        html += "         </tr>";
        html += "         <tr>";
        html += "           <td>&nbsp;</td>";
        html += "           <td height=\"22\"><span class=\"mod_tips\">请输入11位手机号码</span></td>";
        html += "         </tr>";
        html += "         <tr>";
        html += "           <td align=\"right\">验证码：</td>";
        html += "           <td><input type=\"text\" maxlength=\"6\" id=\"txtCode\" name=\"txtCode\" class=\"input_int2\" onkeyup=\"value=value.replace(/[^\\d]/g,'')\" /> <input type=\"submit\" id=\"btn_get\" value=\"点击获取\" onclick=\"Cat.GetCode();\"/></td>";
        html += "         </tr>";
        html += "         <tr>";
        html += "           <td>&nbsp;</td>";
        html += "           <td height=\"22\"><span class=\"vcode_tips\">验证码输入不正确</span></td>";
        html += "         </tr>";
        html += "         <tr>";
        html += "           <td>&nbsp;</td>";
        html += "           <td><span class=\"inputBtn\" id=\"BtnMobOk\" onclick=\"Cat.SaveMobile();\">确定</span></td>";
        html += "         </tr>";
        html += "       </table>";
        html += "     </div>";
        html += "     <div class=\"dialogBox\" style=\"display:none;\" id=\"divSuccess\">";
        html += "       <table width=\"100%%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">";
        html += "         <tr>";
        html += "           <td align=\"right\" valign=\"top\" class=\"closeDialog\"><span><a href=\"javascript:Cat.GetInterval();\">关闭</a></span></td>";
        html += "         </tr>";
        html += "         <tr>";
        html += "           <td align=\"center\" valign=\"middle\" height=\"40\"><img src=\"/images/duigou.gif\" />恭喜您,手机号码验证成功!</td>";
        html += "         </tr>";
        html += "         <tr>";
        html += "           <td align=\"center\" valign=\"middle\" height=\"40\"><label id=\"lbSeconds\">3</label>秒钟后将自动关闭</td>";
        html += "         </tr>";
        html += "         <tr>";
        html += "           <td>&nbsp;</td>";
        html += "         </tr>";
        html += "         <tr>";
        html += "           <td align=\"center\";><span class=\"inputBtn\" id=\"btnClose\" onclick=\"Cat.GetInterval();\">确定</span></td>";
        html += "         </tr>";
        html += "       </table>";
        html += "     </div>";
        html += " </div>";

        $(document.body).append(html);
        $.post("/user/GetUserInfo", null, function (data) {
            if (data && data.Mobile && $.trim(data.Mobile) != "") {
                $("#txtMob").val(data.Mobile);
                $("#txtMob").attr("disabled", "disabled");
            }
        });
    },
    // 打开手机验证界面
    OpenMobile: function () {
        $("#maskLayer").css({ "display": "block" });
        Cat.position();
        $(function () {
            window.onresize = window.onscroll = function () {
                Cat.position();
            };
        });
    },

    position: function () {
        $("#maskLayer").css({
            "left": ($(document).width() - $("#maskLayer").width()) / 2,
            "top": $(document).scrollTop() + 300
        });
    },

    GetInterval: function () {
        $("#maskLayer").css({ "display": "none" });
        $("#btn_get").removeAttr("disabled");
        clearInterval(cdk);
        $("#btn_get").attr("value", "点击获取");
    },

    GetCode: function() {
        var Mob = $("#txtMob").val();
        if (Mob == "") {
            alert("手机号不能为空!");
            $("#txtMob").focus();
            return;
        }
        if (Mob != "") {
            var patrn = /^1[3,4,5,8]\d{9}$/;
            if (!patrn.exec(Mob)) {
                alert("手机号格式不正确!");
                $("#txtMob").focus();
                return;
            }
        }
        var dd = new Date();
        $.post("/user/MobileVerify?time=" + escape(dd), { "mobile": Mob }, function(data) {
            if (data.State == 1) {
                $("#btn_get").attr("disabled", "disabled");
                $("#txtMob").attr("disabled", "disabled");
                var m = 60;
                $("#btn_get").attr("value", m + "秒后重新获取");
                var cdk = setInterval(function() {
                    m--;
                    $("#btn_get").attr("value", m + "秒后重新获取");
                    if (m <= 0) {
                        clearInterval(cdk);
                        $("#btn_get").removeAttr("disabled");
                        $("#txtMob").removeAttr("disabled");
                        $("#btn_get").attr("value", "点击获取");
                    }

                }, 1000);
            } else {
                $("#btn_get").removeAttr("disabled");
                alert(data.Message);
            }
        });
    },

    SaveMobile: function () {
        var Mob = $("#txtMob").val();
        if (Mob == "") {
            alert("手机号不能为空!");
            $("#txtMob").focus();
            return;
        }
        if (Mob != "") {
            var patrn = /^1[3,4,5,8]\d{9}$/;
            if (!patrn.exec(Mob)) {
                alert("手机号格式不正确!");
                $("#txtMob").focus();
                return;
            }
        }
        var Code = $("#txtCode").val();
        if (Code == "") {
            alert("验证码不能为空!");
            $("#txtCode").focus();
            return;
        }
        var dd = new Date();
        $.post("/User/SaveMobile?time=" + escape(dd), { "mobile": Mob, "code": Code },function (data) {
                if (data.State == 1) {
                    $("#dialogBox").css({ "display": "none" });
                    $("#divSuccess").css({ "display": "block" });
                    var proID = $("#freeID").val();
                    Cat.Add2(proID, 1);
                    var s = 3;
                    var cdk = setInterval(function () {
                        $("#lbSeconds").html(s);
                        s--;
                        if (s <= 0) {
                            clearInterval(cdk);
                            Cat.GetInterval();
                        }

                    }, 1000);
                } else {
                    $("#btn_get").removeAttr("disabled");
                    alert(data.Message);
                }
            });
    }
};


//收藏
var Collect = {
    //功能描述：添加收藏    
    Add: function (productId, fn) {
        if (isNaN(productId)) return;
        if (productId < 1) return;

        var data = { productId: productId };
        $.post("/User/AddCollect", data, function (result) {
            if (!result) return;

            if ($.isFunction(fn)) {
                fn(result);
            }
        });
    }
}

//团购定时器
var Timer = {
    target: String,

    //功能描述：初始化
    Init: function (selecter) {
        if (!selecter || selecter == "") return;

        Timer.target = $(selecter);
        Timer.UpdateTime();
        setInterval(Timer.UpdateTime, 1000);
    },

    //功能描述：计算时间差
    GetDiffTime: function (diffTime) {
        if (!diffTime) return;

        //计算出相差天数
        var days = Math.floor(diffTime / (24 * 3600 * 1000));

        //计算出小时数
        var leave1 = diffTime % (24 * 3600 * 1000);     //计算天数后剩余的毫秒数
        var hours = Math.floor(leave1 / (3600 * 1000));

        //计算相差分钟数
        var leave2 = leave1 % (3600 * 1000);   //计算小时数后剩余的毫秒数
        var minutes = Math.floor(leave2 / (60 * 1000));

        //计算相差秒数
        var leave3 = leave2 % (60 * 1000);    //计算分钟数后剩余的毫秒数
        var seconds = Math.round(leave3 / 1000);

        return { day: days, hour: hours, minute: minutes, second: seconds };
    },

    //功能描述：更新时间
    UpdateTime: function () {
        Timer.target.each(function () {
            var time = $(this).attr("time");
            var end = new Date(parseInt(time));
            if (new Date() < end) {
                var diff = Timer.GetDiffTime(end.getTime() - new Date().getTime()); //时间差的毫秒数
                $(this).html("距团购结束<span class=\"hour\">" + diff.hour + "</span>小时<span class=\"minute\">" + diff.minute + "</span>分<span class=\"second\">" + diff.second + "</span>秒");
            }
        });
    }
}

//菜单定位
var MenuPosition = {
    //功能描述：初始化
    Init: function (id) {
        var mc = document.getElementById(id);
        var minNumber = mc.offsetTop;
        var isIE6 = navigator.appVersion.indexOf("MSIE 6") > -1;

        $(window).scroll(function () {
            var sh = document.documentElement.scrollTop || document.body.scrollTop;
            var th = document.documentElement.clientHeight;
            if (sh > minNumber) {
                mc.style.position = !isIE6 ? "fixed" : "absolute";
                mc.style.top = !isIE6 ? "0px" : sh + "px";
            } else {
                mc.style.position = "static";
                mc.style.top = minNumber + "px"
            }
        })
    }
}

//Cookie操作
var CookieManager = {
    //功能描述：设置Cookie, 参数示例：{ name='cookie', value='1', days=1, minutes=1,path='/' }
    Set: function (opt) {
        if (!opt || opt == null) return;

        opt.name = opt.name || "";
        if (opt.name == "") return;

        opt.value = opt.value || "";
        if (opt.value == "") return;
        
        var exp = new Date();
        if (opt.days) {
            exp.setTime(exp.getTime() + opt.days * 24 * 60 * 60 * 1000);
        }
        if (opt.minutes) {
            exp.setTime(exp.getTime() + opt.minutes * 60 * 1000);
        }
        
        if (!opt.path) {
            opt.path = "/";
        }
        //document.cookie = opt.name + "=" + escape(opt.value) + ";expires=" + exp.toGMTString();
        $.cookie(opt.name, opt.value, { expires: exp, path: opt.path }); //{ expires: -1,path: '/' }
    },

    //功能描述：获取Cookie
    Get: function (name) {
        if (!name || name == "") return null;

        var arr = document.cookie.match(new RegExp("(^| )" + name + "=([^;]*)(;|$)"));
        return arr != null ? unescape(arr[2]) : null;
    },

    //功能描述：删除Cookie
    Del: function (name) {
        if (!name || name == "") return null;

        var exp = new Date();
        exp.setTime(exp.getTime() - 1);
        var cval = CookieManager.Get(name);
        if (cval != null) document.cookie = name + "=" + cval + ";expires=" + exp.toGMTString();
    }
};

//日期格式化
var DateFormat = {
    format:function(data){
        try{
            var group = data.match(/\\\/(Date\([0-9-]+\))\\\//gi);
            if (group && group.length > 0) {
                for (var i = 0; i < group.length; i++) {
                    data = data.replace(group[i], DateFormat.toDate(group[i]));
                }
            }
        }catch(ex){
            //   
        }
        return data;
    },

    toDate:function(datetime){
        var dt = datetime || "";
        if (dt == "") return "";

        try {
            dt = (new Date(parseInt(dt.substr(7, dt.length - 9)))).toLocaleDateString();
        } catch (ex) {
            dt = datetime;
        }
        return dt;
    }
}

//CPS
function CPS() {
    if ($.cookie("CPS_IN")) {
        var html;
        html = "<script type=\"text/javascript\"> \n";
        html += "  var _adwq = _adwq || []; \n";
        html += "  _adwq.push(['_setAccount', 'lbi2m']); \n";
        html += "  _adwq.push(['_setDomainName', 'gjw.com']); \n";
        html += "  _adwq.push(['_trackPageview']); \n";
        html += "</script> \n";
        html += "<script type=\"text/javascript\" src=\"http://d.emarbox.com/js/adw.js?adwa=lbi2m\"></script>";
        $("body").append(html);
    }
}

//头部广告
function GetTopAdvertise() {
    $.get("/Home/GetTopAdvertise", null, function (result) {
        $("#top_adv").html(result || "");
    });
}

//主入
$(function () {
    //延迟加载
    $("img.lazy").lazyload({ effect: "fadeIn", skip_invisible: false });

    //鼠标效果
    $("img.lazy").mouseover(function () {
        $(this).stop().animate({ "opacity": 0.86 });
    }).mouseout(function () {
        $(this).animate({ "opacity": 1 });
    });

    //产品搜索
    productSearch.Init({ timeout: 100 });

    //弹出菜单
    CateMenu.Init();

    //顶部导航条
    Navigation.Init();

    //CPS
    CPS();

    //获取广告
    GetTopAdvertise();
});

//AjaxOverride
(function ($) {
    //备份jquery的ajax方法  
    var _ajax = $.ajax;

    //重写jquery的ajax方法  
    $.ajax = function (opt) {
        //备份opt中error和success方法  
        var fn = {
            error: function (XMLHttpRequest, textStatus, errorThrown) {
            },
            success: function (data, textStatus) {
            }
        };

        if (opt.error) {
            fn.error = opt.error;
        }

        if (opt.success) {
            fn.success = opt.success;
        }

        //增加数据过滤，转换日期
        opt.dataFilter = function (data, type) {
            return DateFormat.format(data);
        }

        //扩展增强处理  
        var _opt = $.extend(opt, {
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                //错误方法增强处理
                //此处统一处理程序通用错误，用于向用户提示
                if (XMLHttpRequest.status == 600 || XMLHttpRequest.status == 999) {
                    window.location.href = "/Login/Index?backurl=" + XMLHttpRequest.responseText;
                } else {
                    fn.error(XMLHttpRequest, textStatus, errorThrown);
                }
            },

            success: function (data, textStatus) {
                //成功回调方法增强处理  
                fn.success(data, textStatus);
            }
        });
        _ajax(_opt);
    }
})(jQuery);