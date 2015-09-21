function Back() {
    var url = document.referrer;
    if (url) {
        history.go(-1);
    }
    else {
        window.location = '/default.htm';
    }
}
function Del(ID) {
    if (confirm("确定要删除该商品吗？")) {
        $.post("/Cart/Delete", { "proId": ID }, function (res) {
            if (res.State == 1) {
                //var dd = new Date();
                //$("#divOrderInfo").load("/Ajax/Order/OrderProduct.htm?time=" + escape(dd));
                $('#product_' + ID).remove();
                cart.calcMoney();
            }
            else {
                showAlert("删除失败", res.Message, "error");
            }
        });
    }
}
//收藏
function Collect(ID) {
    $.post(
        "/ajax/User/UserHandle-act-Collect-ID-" + ID + ".htm",
        function (data) {
            var json = eval(data)[0];
            if (json.Success == 1) {
                alert("收藏成功");
            }
            else if (json.Success == 2) {
                alert("您已收藏过此商品");
            }
            else if (json.Success == 3) {
                window.location = '/login/login.htm?backurl=/purchase/ShoppingCart.htm';
            }
            else {
                alert("收藏失败");
            }
        }
    );
    }

var cart = {
    init:function() {
        cart.eventBinding();
        cart.cartEmptyInit();
    },

    cartEmptyInit: function () {
        $("#cartEmpty").removeClass();
        if ($(".item_form").length < 1) {
            $("#cartEmpty").addClass("cart-empty-show");
        } else {
            $("#cartEmpty").addClass("cart-empty");
        }
    },

    editQuantity:function(ID, Quantity) {
        $.post("/Cart/Edit", { "productId": ID, "quantity": Quantity }, function(res) {
            if (res.State == 1) {
                $("#quantity_" + ID).attr("value", Quantity);
            } else if (res.State == 0) {
                alert(res.Message);
                $("#quantity_" + ID).attr("value", res.Data);
                Quantity = res.Data;
            } else {
                alert(res.Message);
            }
            $("#money_" + ID).text(cart.round(parseInt(Quantity) * parseFloat($("#price_" + ID).text()),2));
            cart.calcMoney();
        });
    },
    
    calcMoney: function() {
        var total = 0.00;
        var count = 0;
        $(".item_form").each(function () {
            //$("span[name='money']")
            if ($(this).find(":checkbox").attr("checked")) {
                total += parseFloat($(this).find("span[name='money']").html());
                count += parseInt($(this).find(".quantity-text").val());
            }
        });
        $("#totalMoney").text(cart.round(total, 3));
        $("#proCount").text(count);
    },
    
    round:function(v, e) {
         var t=1; for(;e>0;t*=10,e--); for(;e<0;t/=10,e++); return Math.round(v*t)/t;
    },

     eventBinding: function () {
         //全选
         $(".toggle-checkboxes").unbind("change").change(function () {
             if ($(this).attr("checked")) {
                 $(".checkbox").attr("checked", "");
             } else {
                 $(".checkbox").removeAttr("checked");
             }
                          cart.calcMoney();
             //alert("123");
         });
         
         //商品选择
           $(".p-checkbox .checkbox").unbind("change").change(function () {
            if (!$(this).attr("checked")) {
                $(".toggle-checkboxes").removeAttr("checked");
            } else {
                var isAllCheck = true;
                $(".p-checkbox > .checkbox").each(function () {
                    if (!$(this).attr("checked")) {
                        isAllCheck = false;
                    }
                });
                if (isAllCheck) {
                    $(".toggle-checkboxes").attr("checked","");
                }
            }
            cart.calcMoney();
         });

        $(".decrement").unbind("click").click(function (e) {
            var ID = $(this).attr("index");
            var Quantity = $(this).parent().parent().find(".quantity-text").val();
            if (Quantity) {
                if (Quantity > 1) {
                    cart.editQuantity(ID, (parseInt(Quantity) - 1));
                }
                else {
                    alert("购买的商品数量不能小于1件!");
                }
            }
            else {
                alert("数据不能为空");
            }
        });
        $(".increment").unbind("click").click(function (e) {
            var ID = $(this).attr("index");
            var Quantity = $(this).parent().parent().find(".quantity-text").val();
            if (Quantity) {
                if (Quantity > 0) {
                    cart.editQuantity(ID, (parseInt(Quantity) + 1));
                }
                else {
                    alert("购买的商品数量不能小于1件!");
                }
            }
            else {
                alert("数据不能为空");
            }
        });
        /*数量格式*/
        $(".quantity-text").unbind("keydown").keydown(function (e) {
            var code = e.keyCode;
            var b1 = code >= 96 && code <= 105;
            var b2 = code >= 48 && code <= 57;
            var b3 = code == 8 || code == 46 || code == 37 || code == 38 || code == 39 || code == 40;
            return b1 || b2 || b3;
        });
        $(".quantity-text").unbind("blur").blur(function (e) {
            var ID = $(this).attr("index");
            var Quantity = $(this).val();
            if (Quantity) {
                if (Quantity > 0) {
                    cart.editQuantity(ID, Quantity);
                }
                else {
                    alert("购买的商品数量不能小于1件!");
                    cart.editQuantity(ID, 1);
                }
            }
            else {
                alert("数据不能为空");
            }
        });
        //批量删除
        $(".batchRemove").unbind("click").click(function () {
            var proIds = "";
             $("input[name=proIds]").each(function () {
                 if ($(this).attr("checked")) {
                     proIds += "proIds=" + $(this).val() + "&";
                 }
             });
            
            if (cmValidator.isEmpty(proIds)) {
                alert("请先选择商品再进行操作。");
            }
            
            if (!confirm("您确定要删除购物车中的商品吗？")) {
                return false;
            }

            $.post("Cart/BatchDelete", proIds, function (res) {
                if (res.State == 1) {
                    $("input[name=proIds]").each(function () {
                        if ($(this).attr("checked")) {
                            $("#product_" + $(this).val()).remove();
                        }
                    });
                } else {
                    alert("删除失败！");
                }
                cart.init();
                cart.calcMoney();
            });
        });

        //加入购物车
        $(".AddCart").unbind("click").click(function () {
            var productId = $(this).attr("index");
            $.post(
            "/Cart/TrinketAddProCartPro",
            { productId: productId },
            function (data) {
                if (data && !cmValidator.isEmpty(data)) {
                    if ($("#product_" + productId)) {
                        $("#product_" + productId).remove();
                    }
                    $(".cart-tbody > .item").append(data);
                    cart.init();
                } else {
                    alert("对不起，此商品库存不足或者已下架。");
                }
                cart.calcMoney();
            },"html");
        });

        $("#toSettlement").unbind("click").click(function () {
            var params = "";
             $("input[name=proIds]").each(function() {
                 if ($(this).attr("checked")) {
                     params += "proIds=" + $(this).val() + "&";
                     params += "quantity=" + $("#quantity_" + $(this).val()).val() + "&";
                 }
             });
             if (cmValidator.isEmpty(params)) {
                 alert("请选择商品");
                 return false;
             }
            window.location = "Order/OrderInfo?" + params;
        });
     }
};

$(function() {
    cart.init();
});

