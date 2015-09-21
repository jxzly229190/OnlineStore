(function ($) {
    //备份jquery的ajax方法  
    var _ajax = $.ajax;

    //重写jquery的ajax方法  
    $.ajax = function (opt) {
        //备份opt中error和success方法  
        var fn = {
            error: function(XMLHttpRequest, textStatus, errorThrown) {
            },
            success: function(data, textStatus) {
            }
        };
            
        if (opt.error) {
            fn.error = opt.error;
        }
        
        if (opt.success) {
            fn.success = opt.success;
        }

        //扩展增强处理  
        var _opt = $.extend(opt, {
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                //错误方法增强处理
                //此处统一处理程序通用错误，用于向用户提示
                if (XMLHttpRequest.status == 600) {
                    alert("会话失效，请重新登录。");
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
    };
})(jQuery); 