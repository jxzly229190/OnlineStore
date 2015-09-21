(function ($) {
    var Answer = function (ele, opt) {
        this.$element = ele,
        this.defaults = {
            submit: "#submit",
            container: "#container",
            //random: Math.floor(Math.random() * 10),
            //random2: Math.floor(Math.random() * 10)
        },

        this.data = [{ id: 1, title: "景芝酒业有四大系列产品" }, { id: 2, title: "一品景芝酿造过程中使用过芝麻" }],
        this.length = this.data.length;
        this.options = $.extend({}, this.defaults, opt);
    };
    Answer.prototype = {
        lastAnswer: false,
        nextAnswer: false,
        start: 0,
        end: 2,
        initialize: function () {
            this.toggleAnswer();
            this.submitAnswer();
        },
        toggleAnswer: function () {

            var html = "";
            html += "<li>问题 1：" + this.data[0].title + "</li>";
            html += "<li><input type=\"radio\" name=\"no1button\" value=\"no1button\" checked> &nbsp;A.&nbsp;正确&nbsp;&nbsp;&nbsp;    <input type=\"radio\" name=\"no1button\" value=\"no1button\" checked>&nbsp;B.&nbsp;错误</li>";
            if (this.end <= this.length) {
                html += "<li>问题 2：" + this.data[1].title + "</li>";
                html += "<li><input type=\"radio\" name=\"no2button\" value=\"no1button\" checked> &nbsp;A.&nbsp;正确&nbsp;&nbsp;&nbsp;    <input type=\"radio\" name=\"no2button\" value=\"no2button\" checked>&nbsp;B.&nbsp;错误</li>";
            }
            $(container).html(html);
        },
        submitAnswer: function () {
            var oThis = this;
            $(this.options.submit).click(function () {
                if (oThis.checkUserOnLine()) {
                    $.ajax({
                        type: "POST",
                        async: false,
                        url: "../UserPromote/Participate",
                        dataType: "json",
                        success: function(res) {
                            if (res.State == -1) {
                                alert("您已经参加并领取过赠品了,");
                            } else if (res.State == 1) {
                                $.ajax({                                    
                                   type:"POST",
                                   dataType:"json",
                                   url:"../Cart/Add",
                                   data: {productId:res.Data,quantity:1},
                                   success: function(data) {
                                       if (data.State == 0) {
                                           alert(data.Message);
                                       } else if(data.State==1) {
                                           alert("已为您添加到购物车，请到购物车中领取");
                                           return;
                                       }
                                   }
                                });
                                //alert("恭喜你已领到增品，系统已经为你加入购物车");
                            }
                        }
                    });
                }
            });
        },
        checkUserOnLine: function () {
            var result = false;
            $.ajax({
                type: "POST",
                async: false,
                url:"../UserPromote/CheckLogin",
                dataType: "json",
                success: function (res) {
                    if (res.State == 1) {
                        result = true;
                    }else if (res.State == 0) {
                        alert("您没有登录,请登录再领取");
                        window.location.href = "/Login";
                    }
                }
            });
            return result;
        }
    };
    $.fn.Answer = function (options) {
        var answer = new Answer(this, options);
        return answer.initialize();
    };
})(jQuery);