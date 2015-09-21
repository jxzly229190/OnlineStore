function ChangBandDropDown() {
    var ctrl = arguments[0];
    var url = arguments[1];
    var sel = arguments[2];
    ctrl.empty();
    $.get(url, function (json) {
        var data = eval(json);
        var num = null;
        try {
            num = data.length;
        } catch (err) {
            //alert(err.description);
        }
        for (i = 0; i < num; i++) {
            if (data[i].Id == sel) {
                ctrl.append("<option selected=\"selected\" value=" + data[i].Id + ">" + data[i].Name + "</option>");
            }
            else {
                ctrl.append("<option value=" + data[i].Id + ">" + data[i].Name + "</option>");
            }
        }
    });
    //ctrl.change();
};
$(function () {
    //0：要绑定的控件；1：发送地址；2：附加参数{Selected:0,Name:"请选择",Value:""}
    $.fn.fillDropdown = function () {
        var sender = $(this);
        var ctrl = arguments[0];
        var url = arguments[1];
        var ext = null;
        if (arguments.length > 2) {
            ext = arguments[2];
        }
        sender.change(function () {
            ctrl.empty();
            if (ext.Name) {
                var fval = "";
                if (ext.Value)
                    fval = ext.Value;
                ctrl.append("<option value=" + fval + ">" + ext.Name + "</option>");
            }
            var val = sender.val();
            var sendurl = url;
            if (sendurl.indexOf("?") > -1) {
                sendurl += "&id=" + val;
            }
            else {
                sendurl += "?id=" + val;
            }
            $.get(sendurl, function (json) {
                var data = eval(json);
                var num = null;
                try {
                    num = data.length;
                } catch (err) {
                    //alert(err.description);
                }
                for (i = 0; i < num; i++) {
                    var sel = "";
                    if (ext != null) {
                        if (ext.Selected) {
                            if (data[i].Id == ext.Selected) {
                                sel = " selected=\"selected\"";
                            }
                        }
                    }
                    ctrl.append("<option" + sel + " value=" + data[i].Id + ">" + data[i].Name + "</option>");
                }
            });
            //ctrl.change();
        });
    };
});