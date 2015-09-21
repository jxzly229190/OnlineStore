// 添加收藏
//var addcollect = function() {
//    $("#ACollect").click(function () {
//        $.post("/Home/AddCollect", { productId: $.trim($("#proId").val()) }, function (data) {
//            if (data.State == 1) {
//                $("#divSuccess").removeClass("hide");
//                setTimeout(function () { $("#divSuccess").addClass("hide"); }, 1000);
//            } else if (data.State == 2) {
//                $("#divCollect").removeClass("hide");
//                setTimeout(function () { $("#divCollect").addClass("hide"); }, 1000);
//            } else if (data.State == 3) {
//                window.location = '/login?backurl=' + document.URL;
//            } else {
//                alert(data.Message);
//            }
//        });
//    });
//};

function GetQuantity(Quantity, Price, Name) {
    $("#txtQuantity").val(Quantity);
    $("#spPrice").html(Price);
    $("#spanChoose").html("<em>已选择</em><strong>“" + Name + "”</strong>");
}


        
//        //加入购物车
//var addCat = function() {
//    $("#addCat").click(function () {
//        debugger;
//        $.post(
//            "/Cart/Add",
//            { quantity: $.trim($("#txtQuantity").val()), productId: $.trim($("#proId").val()) },
//            function (result) {
//                if (result.State == 1) {
//                    Cat.Load();
//                    Cat.Show();
//                    Cat.Close();
//                    return false;
//                } else if (result.State == 0) {
//                    alert(result.Message);
//                    if (result.Data > 0) {
//                        Cat.Load();
//                        Cat.Show();
//                        Cat.Close();
//                    }
//                    return false;
//                } else {
//                    if (result.Message) {
//                        alert(result.Message);
//                    } else {
//                        alert('操作失败');
//                    }
//                }
//            });
//    });
//};

//var autoCloseCat = function() {
//    setTimeout(function() {
//        $("#cat_pop1").fadeOut("slow");
//    }, 5000);
//};

//var closeCat = function() {
//    $("#cat_pop1").find(".close").click(function() {
//        $("#cat_pop1").fadeOut("slow");
//    });
//};

///*定位cat_pop1*/
//function catpopShow(){
//    var tx = ($(window).width() - $("#cat_pop1").width())/2 + $("#cat_pop1").width() /2;
//    var ty = ($(window).height() - $("#cat_pop1").height())/2 + $(document).scrollTop();
//    $("#cat_pop1").css({"left":tx,"top":ty});
//    $("#cat_pop1").show();
//}
        
        function getPromotionPrice(id)
        {
            var v = $("#lstQuantityAndPrice").val()||"";
            if(v!=""){
                var n=0;
                var price = 0.00;
                var str1 = v.split('|')
                for(var x=0;x<str1.length;x++)
                {
                    var str2 = str1[x].split(',');
                    integer = parseInt(str2[0]);
                    if(parseInt(id.val())>=integer)
                    {
                        price = parseFloat(str2[1]);
                        $("#spPrice").html(price);
                        n++;
                        if(parseInt(id.val())==integer)
                        {
                            kitsExpand(n-1);
                            //多瓶初始数量和单价
                            $("#spanChoose").html("<em>已选择</em><strong>“"+str2[2]+"”</strong>");
                           
                        }
                        else{
                            kitsExpand(-1);
                            $("#spanChoose").html("");
                        }
                    }
                }
                n=0;
            }
        }


        //库存
        var openPanel = function() {

            $("#storeinfo .at").bind({
                "mouseenter": function() {
                    $(this).children(".addrArea").show();
                    $(".at_btn").addClass("on");
                },
                "mouseleave": function() {
                    $(this).children(".addrArea").hide();
                    $(".at_btn").removeClass("on");
                }
            });
        };

        //规格样式
        var format = function(){
            var c = -1;
            var last;
            $("#format li").each(function(i,elem){
                $(elem).css({ width:$(elem).width(),height:$(elem).height()});

                $(this).bind({
                    mouseover:function(){
                        if(c!=i){
                            high(elem);
                        }
                    },
                    mouseout:function(){
                        if(c!=i){
                            init(elem);
                        }
                    },
                    click:function(){
                        if(last == elem){
                            c = -1;
                            last = null;
                            init(elem);
                            return false;
                        }
                        if(last != null){
                           init(last);
                        }
                        high(elem);
                        c = i;
                        last = elem;
                    }
                })
            })
            function init(elem){
               $(elem).find("span").css({"padding":"4px 5px 4px 5px","margin-top":0});
               $(elem).css({"background":"#d8d8d8","border-color":"#d8d8d8"});
            }
            function high(elem){
               $(elem).find("span").css({"padding":"3px 4px 3px 4px","margin-top":1});
               $(elem).css({"background":"#cf0404","border-color":"#cf0404"});
            }  
        }

        //新规格样式
        var kitsIndex = parseInt($("#kitsItemStyle").val());
        var kitsCurElem = $(".kits li")[kitsIndex];
        var kits = function(){
            $(".kits li").bind({
                "mouseover":function(){
                    if(this != kitsCurElem){
                        $(this).find("a").addClass("on");
                    }
                },
                "mouseout":function(){
                    if(this != kitsCurElem){
                        $(this).find("a").removeClass("on");
                    }
                },
                "click":function(){
                    if(kitsCurElem != null){
                        $(kitsCurElem).find("a").removeClass("on");
                        $(kitsCurElem).find("i").remove();
                        
                    }
                    if(kitsCurElem != this){
                        $(this).find("a").addClass("on");
                        $(this).append("<i>已选中</i>");
                        kitsCurElem = this;
                    }else{
                        $(this).find("a").removeClass("on");
                        $(this).find("i").remove();
                        kitsCurElem = null;
                    }
                    
                }
            })
            if(typeof($("#imgProudct0").attr("id"))=="undefined"){//文字样式
                $(".kits li a").each(function(index,elem){
                    $(elem).css({"height":"20px","line-height":"20px","padding":"2px"});
                })
            }
            else{
                $(".divSuitClass").attr("style","*height:60px");
            }
        }
        //kitsExpand(0)
        function kitsExpand(index){
            var curElem = $(".kits li")[index];
            $(kitsCurElem).find("a").removeClass("on");
            $(kitsCurElem).find("i").remove();
            if(curElem != null){
                $(curElem).find("a").addClass("on");
                $(curElem).append("<i>已选中</i>");
                kitsCurElem = curElem;
            }
        }
        
        //显示分享
        function viewShare(){
            $(".shareweb").bind({
                "mouseenter":function(){
                    $("#mod_share").css({"display":"block"});
                },
                "mouseleave":function(){
                    $("#mod_share").css({"display":"none"});
                }
            })
        }

        //组合套餐
        allSelect();
        function allSelect(){
            $("#combBox").find("input").each(function(i,elem){
                elem.checked = "checked";
            })
        }
        var checkGift = function(tha){
            var jia = $(tha).parent().parent().parent().find("p");
            var jiaBox = $(tha).parent().parent().parent().parent();
            
            var count = $("#lbSumQuantity");
            var lbMarkPrice = $("#lbMarkPrice");
            var lbCombPrice = $("#lbCombinationPrice");
            var savePrice = $("#lbSumCombinationPrice");
            var marketPrice = $(tha).attr("marketPrice");
            var combPrice = $(tha).attr("combPrice");
            var price = $(tha).attr("price");
            
            
            if(!tha.checked){
                if(getCheck()){
                    $(jia).removeClass("zh_box_add");
                    $(jia).addClass("zh_box_add_nomal");
                    
                    $(count).html(parseInt($(count).html()) - 1);
                    $(lbMarkPrice).html(parseInt($(lbMarkPrice).html()) - parseInt(marketPrice));
                    $(lbCombPrice).html(parseInt($(lbCombPrice).html()) - parseInt(combPrice));
                    $(savePrice).html(parseInt($(savePrice).html()) - (parseInt(price)-parseInt(combPrice)));
                }else{
                    alert("组合商品至少为1个");
                    tha.checked = "checked";
                }

            }else{
                $(jia).removeClass("zh_box_add_nomal");
                $(jia).addClass("zh_box_add");

                $(count).html(parseInt($(count).html()) + 1);
                $(lbMarkPrice).html(parseInt($(lbMarkPrice).html()) + parseInt(marketPrice));
                $(lbCombPrice).html(parseInt($(lbCombPrice).html()) + parseInt(combPrice));
                $(savePrice).html(parseInt($(savePrice).html()) + (parseInt(price)-parseInt(combPrice)));
            }

            function getCheck(){
                var boo = false;
                $(jiaBox).find("input").each(function(i,elem){
                    if(elem.checked){
                        boo = true;
                        return boo;
                    }
                })
                return boo;
            }
        }

        //商品信息切换
        var goodsTab = function () {
            $("#main_l").find(".scrip_lin > .scrip_bar > ul > li").each(function (i) {
                $(this).click(function () {

                    cutoverTabs(i);

                    var maxh = $("#main_l").offset().top;
//                    if (globalSH > maxh) {
//                        var de = document.documentElement.scrollTop == "0" ? document.body : document.documentElement;
//                        de.scrollTop = maxh - 30;
//                    }

                });
            });
        };
        function cutoverTabs(num) {
            var space = 132;
            var spxq = $("#main_l").find(".details1"),
	            mjpl = $("#main_l").find(".details2"),
	            cjjl = $("#main_l").find(".details5"),
	            spzx = $("#main_l").find(".details3"),
	            jysd = $("#main_l").find(".details4"),
	            zlbz = $("#main_l").find(".details6");
            var liArr = $("#main_l").find(".scrip_lin > .scrip_bar > ul > li");
            switch (num) {
                case 0: spxq.show(); mjpl.show(); cjjl.show(); spzx.show(); jysd.show(); zlbz.show(); break;
                case 1: mjpl.show(); spxq.hide(); cjjl.hide(); spzx.hide(); jysd.hide(); zlbz.hide();
                    CommentLoad(); break;
                case 2: spzx.show(); spxq.hide(); mjpl.hide(); cjjl.hide(); jysd.hide(); zlbz.hide();
                    ConsultLoad(); break;
                case 3: spzx.hide(); spxq.hide(); mjpl.hide(); cjjl.hide(); jysd.hide(); zlbz.show(); break;
                case 4: jysd.show(); spxq.hide(); mjpl.hide(); cjjl.hide(); spzx.hide(); zlbz.hide(); break;
            }
            liArr.each(function (index, elem) {
                $(elem).removeAttr("style");
            });
            $(liArr[num]).css("background-position", -4 * space + "px" + " -205px");
        }

        var loadconsult = 0;
        var loadcomment = 0;
        function ConsultLoad() {
            if (loadconsult > 0) {
                return;
            }
            // 咨询
            var option = {};
            option.productID = $("#proId").val();
            option.showCount = "consultCount";
            option.container = "divConsult";
            option.pageUrl = "/Product/GetProductConsult";
            consult.Init(option);
            loadconsult += 1;
        }

        function CommentLoad() {
            if (loadcomment > 0) {
                return;
            }
            var option = {};
            // 评论
            option.productID = $("#proId").val();
            option.showCount = "commentCount";
            option.container = "divComment";
            option.pageUrl = "/Product/GetProductComment";
            comment.Init(option);
            loadcomment += 1;
        }

        $(function () {
            /*$('.jqzoom').jqzoom({
            zoomType: 'standard',
            lens:true,
            preloadImages: false,
            alwaysOn:false,
            title:false
            });*/
            goodsTab();
            openPanel();
            //addCat();
            //addcollect();
            format();
            kits();
            viewShare();

            //多瓶初始数量和单价
            if(parseInt($("#kitsItemQuantity").val())>0)
            {
                $("#txtQuantity").val($("#kitsItemQuantity").val());
                $("#spPrice").html($("#kitsItemPrice").val());
                $("#spanChoose").html("<em>已选择</em><strong>“"+$("#kitsItemName").val()+"”</strong>");
            }

            //放大镜
            var $mNum = $("li.magnifier_simg").length;
            var $width = ($(".magnifier_simg").width() + 10) * $mNum;
            var $imgListWidth = $width;
            $(".magnifier_simglist").css("width", $imgListWidth);
            var $pages = $width / 355;
            $(".magnifier_larrow").click(function () {
                if ($pages > 1) {
                    $(".magnifier_simgbox").scrollLeft(-355);
                }
            });
            $(".magnifier_rarrow").click(function () {
                if ($pages > 1) {
                    $(".magnifier_simgbox").scrollLeft(355);
                }
            });
            for (i = 0; i < $mNum; i++) {
                $("#mSmallImg" + i).hover(function () {
                    $(this).css("border-color", "#D5182A");
                    var $bsrc = $(this).attr("bimg");
                    var $lsrc = $(this).attr("limg");
                    $("#mBigImg").attr("src", $bsrc);
                    $("#mBigImg").attr("limg", $lsrc);
                }, function () {
                    $(this).css("border-color", "#e3e3e3");
                });
            }
            $(".magnifier_bimg").jqueryzoom({
                xzoom: 400,
                yzoom: 400,
                offset: 20,
                position: "right",
                preload: 1
            });
            
            //地区
            //$("#divRegion").load('/Ajax/product/Region-flag-0-ID-238.htm');


            //评论、咨询、晒单
            var hash = window.location.hash;
            if (hash != "") {
                hash = window.location.hash.substring(1);
                if (hash.toLowerCase() != "comment" && hash.toLowerCase() != "consult" && hash.toLowerCase() != "show") {
                    var ha = hash.split('|');
                    var url = ha[0];
                    switch (ha[1]) {
                        case "Comment":
                            //$("#divComment").load('/ajax/product/Comment-ID-238.htm');
                            break;
                        case "Consult":
                           // $("#divConsult").load('/ajax/product/Consult-ID-238.htm');
                            break;
                        case "Record":
                            $("#divRecord").load('/ajax/product/Record-ID-238.htm');
                            break;
                        case "Show":
                            //$("#divShow").load('/show/show_list-ID-238.htm');
                            break;
                    }
                }
                else {
                    //$("#divComment").load('/ajax/product/Comment-ID-238.htm');
                   // $("#divConsult").load('/ajax/product/Consult-ID-238.htm');
                    $("#divRecord").load('/ajax/product/Record-ID-238.htm');
                    //$("#divShow").load('/show/show_list-ID-238.htm');
                }
            }
            else {
               // $("#divComment").load('/ajax/product/Comment-ID-238.htm');
               // $("#divConsult").load('/ajax/product/Consult-ID-238.htm');
                $("#divRecord").load('/ajax/product/Record-ID-238.htm');
                //$("#divShow").load('/show/show_list-ID-238.htm');
            }

            //查看买家评价
            $("#AllComment").click(function(){
               var space = 132;
                $("#main_l").find(".details1").hide();
	            $("#main_l").find(".details2").show();
	            $("#main_l").find(".details5").hide();
	            $("#main_l").find(".details3").hide();
	            $("#main_l").find(".details4").hide();
	            $("#main_l").find(".details6").hide();

                $("#main_l").find(".scrip_lin > .scrip_bar > ul > li:eq(1)").css("background-position", "-132px" + " -205px").siblings("li").removeAttr("style");
            });

            //收藏
            $("#ACollect").click(function () {
                $.post(
                    "/ajax/User/UserHandle-act-Collect-ID-238.htm",
                    function(data) {
                        var json = eval(data)[0];
                        if (json.Success == 1) {
                            $("#divSuccess").removeClass("hide"); 
                            setTimeout(function(){$("#divSuccess").addClass("hide");},1000);
                        }
                        else if (json.Success == 2) {
                            $("#divCollect").removeClass("hide"); 
                            setTimeout(function(){$("#divCollect").addClass("hide");},1000);
                        }
                        else if (json.Success == 3) {
                            window.location='/login/login.htm?backurl=/product/Item-ID-238.htm';
                        }
                        else {
                            showAlert("提交失败", json.Message, "error");
                        }
                    }
                );
            });
            $("#txtQuantity").keyup(function(e){          
               	var sender = $("#txtQuantity"); 
                if (sender.val()!= "") {
                    var $val = sender.val();  
                     var code;  
                     for (var i = 0; i < $val.length; i++) {  
                         var code = $val.charAt(i).charCodeAt(0);  
                         if (code < 48 || code > 57) {  
                             alert("数量不正确！");
                                 sender.val(1);
                                 getPromotionPrice(sender);//get price
                             break;  
                         }
                     }  
                }               
			});
            $("#Continue").click(function(){
               $("#cat_pop1").fadeOut("slow"); 
                false;
            });
            $("#AddPro").click(function(){
				var dd = new Date();
				var Millisecond = dd.getMilliseconds();
				$.post(
			    "/Ajax/Order/OrderAdd-act-AddPro-ID-"+238+"-Quantity-1.htm?time=" + (Millisecond + 0.01),
			    function (data) {
                    var json = eval(data)[0];
			        if (json.Success == "True") {
						dd = new Date();
						$("input[name='cbName']:checkbox").each(function(i){
							if ($(this).attr("checked")) { 
								$.post(
									"/Ajax/Order/OrderAdd-act-AddPro.htm?dd=" + escape(dd),{"ID":$(this).val(),"ProductID":238,"State":"1"},
									function (data) {
										var json = eval(data)[0];
										if (json.Success == "True") {
                                            if(i==$("input[name='cbName']:checkbox").length-1){
                                                dd = new Date();
                                                //$("#head_cart_no").load("/Cart/GetCartInfo?d=" + escape(dd));
                                                Cat.LoadServer();
						                        catpopShow();
						                        closeCat();
						                        autoCloseCat();
                                                return false;
                                            }
										}
										else {
											alert(json.Message);
											return false;
										}
									});
							}
						});
			        }
			        else {
			            alert(json.Message);
                        return false;
			        }
			    });
            });
        });
        
		