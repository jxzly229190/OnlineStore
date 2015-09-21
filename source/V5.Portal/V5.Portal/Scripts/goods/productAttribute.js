//商品属性
var Attribute = {
    attributes: Array,  //属性
    attributesValue: Array,   //属性值
    container: Object,  //容器

    //功能描述：初始化
    Init: function (option) {
        if (!Attribute.SetOption(option)) return;

        Attribute.LoadForm();
        Attribute.SetAttributeValue();
    },

    //功能描述：设置参数
    SetOption: function (option) {
        if (!option || option == null) return false;
        if (!option.container || option.container == "") return false;

        Attribute.container = $("#" + option.container.replace(/^#/, ""));
        if (!Attribute.container || Attribute.container == null) return false;

        if (!option.attributesValue || option.attributesValue == null) return false;
        if (!$.isArray(option.attributesValue)) return false;
        Attribute.attributesValue = option.attributesValue;
        Attribute.attributes = option.attributes;
        return true;
    },

    //功能描述：根据产品属性创建控件
    LoadForm: function () {
        var data = Attribute.attributes;
        if (!data || data == null) return;

        var html = "";
        html += "<ul>";
        for (var i = 0; i < data.length - 1; i++) {
            html += "<li>";
            html += " <img src=\"/Images/b1.gif\" alt=\"\">" + data[i].AttributeName + "： <span id=\"form_custom_column_" + data[i].ID + "\"></span>";
            html += "</li>";
        }
        html += "</ul>";

        Attribute.container.html(html);
    },

    //功能描述：绑定数据
    SetAttributeValue: function () {
        var data = Attribute.attributes;
        if (!data || data == null) return;

        for (var i = 0; i < data.length; i++) {
            Attribute.SetElementAttributeValue(data[i]);
        }
    },

    //功能描述：绑定属性值
    SetElementAttributeValue: function (data) {
        if (!data || data == null) return;

        var html = "";
        var ID = data.ID || "";
        var AttributeValues = data.AttributeValue || null;

        for (var i = 0; i < Attribute.attributesValue.length; i++) {
            if (Attribute.attributesValue[i].ID == ID) {
                var value = Attribute.attributesValue[i].Value || "";
                $("#form_custom_column_" + ID).html(value);
                break;
            }
        }
    }
}
