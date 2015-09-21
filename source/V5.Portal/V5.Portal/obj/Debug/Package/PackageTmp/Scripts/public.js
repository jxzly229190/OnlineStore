// JavaScript Document
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

//产品搜索
var productSearch = {
    timeout: Number,
    suggest: {
        id: String,
        value: String
    },

    Init: function (opt) {
        productSearch.timeout = opt.timeout || 300;
        productSearch.LoadEvent();
    },

    LoadEvent: function () {
        $(document).click(function () {
            $(".search_suggest").hide();
        });
        $(document).keydown(productSearch.KeyDownEvent);
        $(document).keyup(productSearch.KeyupEvent);
        $("#sch").keyup(productSearch.KeyupEvent);
        $("#btnsch").click(function () {
            var keyword = $("#sch").val();
            if (keyword != "") {
                window.location.href = "/Home/Search?search=" + escape(keyword) + "@";
            }
        });
    },

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
                if (keyword != "") {
                    window.location.href = "/Home/Search?search=" + escape(keyword) + "@";
                }
            }
        }
    },

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

    LoadSuggestData: function (fvalue) {
        console.log(fvalue);
        productSearch.suggest.value = fvalue;
        $.ajax({
            type: "POST",
            url: "/Home/SearchSuggest",
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
                    html += "<a href='/home/item?id=" + data[i].ID + "' >" + item + "</a>";
                }
                if (html == "") return;
                $(".search_suggest").html(html);
                $(".search_suggest").show();
            },
            error: function () {
                alert("请求失败，请联系系统管理员！");
            }
        });
    },

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
    }
}

$(function () {
    //图片软加载
    $("img.lazy").lazyload({ effect: "fadeIn", skip_invisible: false });

    //产品搜索
    productSearch.Init({ timeout: 100 });

    //读取购物车数量
    //var dd = new Date();
    //$("#head_cart_no").load("/ajax/head/shoppingcart.htm?act=getcount&d=" + escape(dd));

    //head 弹出菜单部分
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
        /*
        $(".navCon_on").hover(function () {
        $(".cateMenu").show();
        },
        function () {
        $(".cateMenu").hide();
        })
        */
    } ();

//    var miniMenu = function () {
//        /*购物列表*/
//        $(".miniMenu").find(".m1").hover(
//			function () {
//			    $(this).addClass("on");
//			    $(this).find(".mini-cart").show();
//			    var dd = new Date();
//			    $("#head_cart").load("/Cart/ShoppingCart?d=" + escape(dd));
//			},
//			function () {
//			    $(this).removeClass("on");
//			    $(this).find(".mini-cart").hide();
//			}
//		)
//        /*用户中心*/
//        $(".miniMenu").find(".m3").hover(
//			function () {
//			    $(this).addClass("cur");
//			    $(this).find(".miniMenu-child").show();
//			},
//			function () {
//			    $(this).removeClass("cur");
//			    $(this).find(".miniMenu-child").hide();
//			}
//		)
//    } ();


    /*topBar置顶*/
    var positionMenu = function (id) {
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
    } ("topBar")
})


/*搜索提示*/
var searchTips = {
    keyBoo: false,
    currentIndex: -1,
    currentArr: [],
    box: null,
    input: null,
    init: function (obj) {
        var that = this;
        this.input = document.getElementById(obj.inputID);
        this.createContent(obj.listID);
        this.onChange = obj.onChange || null;
        this.onSubmit = obj.onSubmit || null;

        this.addKeyBoardEvent();
        this.change();
        this.addEvent(that.input, "blur", function () {
            that.focusout();
        });

    },
    change: function () {
        var that = this;
        if (that.input.addEventListener) {
            that.input.addEventListener("input", function () {
                that.onChange();
                that.keyBoo = true;
            }, false);
        } else if (that.input.attachEvent) {
            that.input.attachEvent("onpropertychange", function () {
                that.onChange();
                that.keyBoo = true;
            });
        }
    },
    find: function (data) {
        this.box.parentNode.style.display = "block";
        //this.box.style.display = "block";
        var ul = this.box.getElementsByTagName("ul")[0];
        ul.innerHTML = "";
        this.currentArr = [];
        this.currentIndex = -1;
        for (var i = 0; i < data.length; i++) {
            var li = this.getLi();
            li.innerHTML = data[i].keyword;
            ul.appendChild(li);
            this.currentArr.push(li);
        }
    },
    createContent: function (sBox) {
        this.box = document.createElement("div");
        this.box.className = "searchTips";
        document.getElementById(sBox).appendChild(this.box);
        var ul = document.createElement("ul");
        this.box.appendChild(ul);
        //this.box.style.display = "none";
    },
    selectCurrent: function () {
        var len = this.currentArr.length;
        if (this.currentIndex < 0) this.currentIndex = len - 1;
        if (this.currentIndex > len) this.currentIndex = 0;
        for (var i = 0; i < len; i++) {
            this.currentArr[i].className = this.currentIndex == i ? "on" : "";
        }
    },
    addKeyBoardEvent: function () {
        var that = this;
        this.addEvent(document, "keydown", function (e) {
            if (that.keyBoo) {
                var e = e || window.event;
                switch (e.keyCode) {
                    case 40:
                        that.currentIndex++;
                        that.selectCurrent();
                        break;
                    case 38:
                        that.currentIndex--;
                        that.selectCurrent();
                        break;
                    case 13:
                        if (that.currentIndex != -1) {
                            that.input.value = that.currentArr[that.currentIndex].innerHTML;
                        }
                        that.onSubmit();
                        that.focusout();
                        break
                    default: break;
                }

            }
        })
    },
    focusout: function () {
        var that = this;
        setTimeout(function () {
            //that.box.style.display = "none";
            that.box.parentNode.style.display = "none";
            that.keyBoo = false;
        }, 100)
    },
    getLi: function () {
        var that = this;
        var li = document.createElement("li");
        this.addEvent(li, "mouseover", function () { li.className = "on"; });
        this.addEvent(li, "mouseout", function () { li.className = ""; });
        this.addEvent(li, "click", function () { that.input.value = li.innerHTML; that.onSubmit(); });
        return li;
    },
    addEvent: function (obj, type, fn) {
        if (obj.addEventListener) {
            obj.addEventListener(type, fn, false);
        } else if (obj.attachEvent) {
            obj.attachEvent("on" + type, fn);
        }
    }
}










