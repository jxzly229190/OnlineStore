//商品属性
var Attribute = {
    categoryId: String, //类目ID
    attributes: Array,  //属性
    attributesValue: Array,   //属性值
    container: Object,  //容器

    //功能描述：初始化
    Init: function (option) {
        if (!Attribute.SetOption(option)) return;
        Attribute.LoadAttribute();
    },

    //功能描述：设置参数
    SetOption: function (option) {
        if (!option || option == null) return false;
        if (isNaN(option.categoryId)) return false;
        if (!option.container || option.container == "") return false;

        Attribute.container = $("#" + option.container.replace(/^#/, ""));
        if (!Attribute.container || Attribute.container == null) return false;

        if (!option.attributesValue || option.attributesValue == null) return false;
        if (!$.isArray(option.attributesValue)) return false;

        Attribute.categoryId = option.categoryId;
        Attribute.attributesValue = option.attributesValue;

        return true;
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
                try {
                    console.log("请求失败");
                } catch (e) {
                    console.error("" + e.name + e.message + "");
                }
            }
        });
    },

    //功能描述：获取产品属性
    LoadAttribute: function () {
        var option = {};
        option.url = "/Product/QueryAttributeAndAttributeValue";
        option.data = { categoryId: Attribute.categoryId };
        option.fn = function (data) {
            if (!data || data == null) return;

            Attribute.attributes = data;
            Attribute.LoadForm();
            Attribute.SetAttributeValue();
            Attribute.LoadFormEvent();
        }
        Attribute.LoadData(option);
    },

    //功能描述：根据产品属性创建控件
    LoadForm: function () {
        var data = Attribute.attributes;
        if (!data || data == null) return;

        var html = "";
        for (var i = 0; i < data.length - 1; i += 2) {
            html += "<tr>";
            html += "<td class=\"pname\">" + (data[i].AttributeName || "") + "：</td><td>" + Attribute.LoadEelment(data[i]) + "</td>";
            html += "<td class=\"pname\">" + (data[i + 1].AttributeName || "") + "：</td><td>" + Attribute.LoadEelment(data[i + 1]) + "</td>";
            html += "</tr>";
        }
        html = "" ? "" : "<tbody class=\"from_custom_body\">" + html + "</tbody>";

        Attribute.container.append(html);
    },

    //功能描述：加载元素
    LoadEelment: function (data) {
        if (!data || data == null) return "未指明控件类型";

        var html = "";
        var ID = data.ID || "";
        var AttributeName = data.AttributeName || "";
        var InputType = (data.InputType || "").toLowerCase();
        var DataType = data.DataType || "string";
        var Length = data.Length || 0;
        var AttributeValues = data.AttributeValues || null;

        switch (InputType) {
            case "select":
                if (AttributeValues && AttributeValues.length > 0) {
                    html += "<select class=\"form_select\" id=\"form_custom_column_" + ID + "\">";
                    html += "<option value=\"-1\">请选择...</option>";
                    for (var i = 0; i < AttributeValues.length; i++) {
                        if (AttributeValues[i].IsDefault == 1) {
                            html += "<option value=\"" + AttributeValues[i].ID + "\" selected=\"selected\">" + (AttributeValues[i].Value || "") + "</option>";
                        } else {
                            html += "<option value=\"" + AttributeValues[i].ID + "\">" + (AttributeValues[i].Value || "") + "</option>";
                        }
                    }
                    html += "</select>";
                } else {
                    html += "未设置属性值，无法创建下拉控件";
                }
                break;
            case "text":
                if (AttributeValues && AttributeValues.length > 0) {
                    html += "<input type=\"text\" class=\"form_text k-textbox\" id=\"form_custom_column_" + ID + "\" value=\"" + AttributeValues[0].Value + "\" />";
                } else {
                    html += "<input type=\"text\" class=\"form_text k-textbox\" id=\"form_custom_column_" + ID + "\" />";
                }
                break;
            case "radio":
                if (AttributeValues && AttributeValues.length > 0) {
                    for (var i = 0; i < AttributeValues.length; i++) {
                        if (AttributeValues[i].IsDefault == 1) {
                            html += "<input type=\"radio\" class=\"form_radio\" name=\"form_radio_" + ID + "\" value=\"" + AttributeValues[i].ID + "\" fvalue=\"" + (AttributeValues[i].Value || "") + "\" checked=\"checked\" \">" + (AttributeValues[i].Value || "");
                        } else {
                            html += "<input type=\"radio\" class=\"form_radio\" name=\"form_radio_" + ID + "\" value=\"" + AttributeValues[i].ID + "\" fvalue=\"" + (AttributeValues[i].Value || "") + "\" >" + (AttributeValues[i].Value || "");
                        }
                    }
                } else {
                    html += "未设置属性值，无法创建单选控件";
                }
                break;
            case "checkbox":
                if (AttributeValues && AttributeValues.length > 0) {
                    for (var i = 0; i < AttributeValues.length; i++) {
                        if (AttributeValues[i].IsDefault == 1) {
                            html += "<input type=\"checkbox\" class=\"form_checkbox\" name=\"form_checkbox_" + ID + "\" value=\"" + AttributeValues[i].ID + "\" fvalue=\"" + (AttributeValues[i].Value || "") + "\" checked=\"checked\" >" + (AttributeValues[i].Value || "");
                        } else {
                            html += "<input type=\"checkbox\" class=\"form_checkbox\" name=\"form_checkbox_" + ID + "\" value=\"" + AttributeValues[i].ID + "\" fvalue=\"" + (AttributeValues[i].Value || "") + "\" >" + (AttributeValues[i].Value || "");
                        }
                    }
                } else {
                    html += "未设置属性值，无法创建复选控件";
                }
                break;
            case "number":
                if (AttributeValues && AttributeValues.length > 0) {
                    html += "<input type=\"number\" class=\"form_number\" id=\"form_custom_column_" + ID + "\" value=\"" + AttributeValues[0].Value + "\" />";
                } else {
                    html += "<input type=\"number\" class=\"form_number\" id=\"form_custom_column_" + ID + "\" />";
                }
                break;
            default:
                html += "未指明控件类型";
                break;
        }

        return html;
    },

    //功能描述：绑定表单事件
    LoadFormEvent: function (data) {
        $(".form_select").kendoDropDownList();
        $(".form_number").kendoNumericTextBox();
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
        var InputType = (data.InputType || "").toLowerCase();
        var DataType = data.DataType || "string";
        var Length = data.Length || 0;
        var AttributeValues = data.AttributeValues || null;
        var CurAttributes = null;

        for (var i = 0; i < Attribute.attributesValue.length; i++) {
            if (Attribute.attributesValue[i].ID == ID) {
                var valueId = Attribute.attributesValue[i].ValueID || "";
                var value = Attribute.attributesValue[i].Value || "";
                switch (InputType) {
                    case "select":
                        $("#form_custom_column_" + ID).val(valueId);
                        break;
                    case "text":
                        $("#form_custom_column_" + ID).val(value);
                        break;
                    case "radio":
                        $("input[type='radio'][name='form_radio_" + ID + "'][value='" + valueId + "']").attr("checked", "checked");
                        break;
                    case "checkbox":
                        $("input[type='checkbox'][name='form_checkbox_" + ID + "'][value='" + valueId + "']").attr("checked", "checked");
                        break;
                    case "number":
                        $("#form_custom_column_" + ID).val(value);
                        break;
                }
            }
        }
    },

    //功能描述：保存数据
    GetAttributeValue: function () {
        var result = [];
        var data = Attribute.attributes;
        if (!data || data == null) return "";

        for (var i = 0; i < data.length; i++) {
            var fvalue = Attribute.GetElementAttributeValue(data[i]);
            if (fvalue && fvalue != "") {
                result.push(fvalue);
            }
        }
        return result.length == 0 ? "" : "[" + result.join(',') + "]";
    },

    //功能描述：获取元素属性
    GetElementAttributeValue: function (data) {
        if (!data || data == null) return "";

        var html = "";
        var ID = data.ID || "";
        var InputType = (data.InputType || "").toLowerCase();
        var DataType = data.DataType || "string";
        var Length = data.Length || 0;
        var ValueID = "0";
        var Value = "";

        switch (InputType) {
            case "select":
                ValueID = $("#form_custom_column_" + ID).val();
                Value = $("#form_custom_column_" + ID).find("option:selected").text();
                break;
            case "text":
                Value = $("#form_custom_column_" + ID).val();
                break;
            case "radio":
                ValueID = $("input[type='radio'][name='form_radio_" + ID + "']:checked").val();
                Value = $("input[type='radio'][name='form_radio_" + ID + "']:checked").attr("fvalue") || "";
                break;
            case "checkbox":
                var result = "";
                $("input[type=checkbox][name='form_checkbox_" + ID + "']:checked").each(function () {
                    ValueID = $(this).val();
                    Value = $(this).attr("fvalue") || "";
                    result += "{ \"ID\": " + ID + ", \"ValueID\": " + ValueID + ", \"Value\": \"" + Value + "\" },";
                });
                return result.replace(/,$/, "");
            case "number":
                Value = $("#form_custom_column_" + ID).val();
                break;
        }
        if (ID && ValueID && Value) {
            return "{ \"ID\": " + ID + ", \"ValueID\": " + ValueID + ", \"Value\": \"" + Value + "\" }";
        }
        return "";
    }
}

//选择产品图片
function selectProductPictureBtnClick() {
    $.get(
            "/Picture/Selector",
            null,
            function call(data) {
                $("#pictureSelectorWindowDiv").html(data);
            },
            "text"
        );

    $("#pictureSelectorWindow").data("kendoWindow").open();
    $("#pictureSelectorWindow").data("kendoWindow").center();
    $(".k-overlay").css('display', 'block');
}

//选择产品图片,ckeditor
function selectPictureBtnClick() {
    $.get(
            "/Picture/SelectorForCKEditor",
            null,
            function call(data) {
                $("#pictureSelectorWindowDiv").html(data);
            },
            "text"
        );

    $("#pictureSelectorWindow").data("kendoWindow").open();
    $("#pictureSelectorWindow").data("kendoWindow").center();
    $(".k-overlay").css('display', 'block');
}

//选择产品分类
var ProductCategory = {
    //功能描述：初始化
    Init: function () {
        var opt = {
            url: "/Product/GetProductParentCategory",
            fn: function (data) {
                ProductCategory.AppendHtml(data, "#productParentCategory", "ProductCategory.ParentCategoryClick");
            }
        }
        ProductCategory.LoadData(opt);
    },

    //功能描述：附加HTML
    AppendHtml: function (data, container, fn) {
        if (!data || data == null || data.length == 0) {
            $(container).html("");
            return;
        }

        fn = fn ? "onclick='" + fn + "(this)'" : "";
        var html = "";
        for (var i = 0; i < data.length; i++) {
            html += "<li class='parent' " + fn + " name='" + data[i].id + "'>" + data[i].name + "</li>";
        }
        html = html == "" ? "" : "<ul>" + html + "</ul>";

        $(container).html(html);
    },

    //功能描述：加载数据
    LoadData: function (opt) {
        if (!opt.url || opt.url == "") return;

        opt.type = opt.type || "GET";
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
                try {
                    console.log("请求失败");
                } catch (e) {
                    console.error("" + e.name + e.message + "");
                }
            }
        });
    },

    //功能描述：点击第一级别商品类别时，获取第二级别商品类别信息
    ParentCategoryClick: function (parentCategory) {
        var parentCategoryID = $(parentCategory).attr("name");
        $("#parentCategoryID").val(parentCategoryID);
        $("#productCategoryID").val("");
        $("#productBrandID").val("");
        $("#parentCategoryName").val(parentCategory.innerHTML);
        $(parentCategory).addClass("curr").siblings().removeClass("curr");

        var opt = {
            url: "/Product/GetProductSubCategory",
            data: { parentCategoryID: parentCategoryID },
            fn: function (data) {
                if (!data && data == null) {
                    $("#productSubCategory").css("display", "none");
                    $("#productSubCategory").html("");
                } else {
                    $("#productParentBrand").css("display", "none");
                    $("#productParentBrand").html("");

                    $("#productSubBrand").css("display", "none");
                    $("#productSubBrand").html("");

                    $("#productSubCategory").css("display", "block");
                    ProductCategory.AppendHtml(data, "#productSubCategory", "ProductCategory.SubCategoryClick");
                }
            }
        }
        ProductCategory.LoadData(opt);
    },

    //功能描述：点击第二级别商品类别时，获取第一级别商品品牌信息
    SubCategoryClick: function (subCategory) {
        var subCategoryID = $(subCategory).attr("name");
        $("#productCategoryID").val(subCategoryID);
        $("#productBrandID").val("");
        $("#subCategoryName").val(subCategory.innerHTML);
        $(subCategory).addClass("curr").siblings().removeClass("curr");

        var opt = {
            url: "/Product/GetProductParentBrand",
            data: { subCategoryID: subCategoryID },
            fn: function (data) {
                if (!data || data == null) {
                    $("#productParentBrand").css("display", "none");
                    $("#productParentBrand").html("");
                } else {
                    $("#productSubBrand").css("display", "none");
                    $("#ProductSubBrand").html("");

                    $("#productParentBrand").css("display", "block");
                    ProductCategory.AppendHtml(data, "#productParentBrand", "ProductCategory.ParentBrandClick");
                }
            }
        }
        ProductCategory.LoadData(opt);
    },

    //功能描述：点击第一级别商品品牌时，获取第二级别商品品牌信息
    ParentBrandClick: function (parentBrand) {
        var parentBrandID = $(parentBrand).attr("name");
        $("#parentBrandID").val(parentBrandID);
        $("#parentBrandName").val(parentBrand.innerHTML);
        $(parentBrand).addClass("curr").siblings().removeClass("curr");

        var opt = {
            url: "/Product/GetProductSubBrand",
            data: { parentBrandID: parentBrandID },
            fn: function (data) {
                if (!data || data == null) {
                    $("#productSubBrand").css("display", "none");
                    $("#productSubBrand").html("");
                    $("#productBrandID").val(parentBrandID);
                } else {
                    $("#productSubBrand").css("display", "block");
                    ProductCategory.AppendHtml(data, "#productSubBrand", "ProductCategory.SubBrandClick");
                }
            }
        }
        ProductCategory.LoadData(opt);
    },

    //功能描述：点击第二级别商品品牌事件
    SubBrandClick: function (subBrand) {
        $(subBrand).addClass("curr").siblings().removeClass("curr");
        $("#productBrandID").val($(subBrand).attr("name"));
        $("#subBrandName").val(subBrand.innerHTML);
    },

    //功能描述：验证是否选择 商品类别和品牌
    ValidateIsSelectCategoryAndBrand: function () {
        if ($("#productCategoryID").val() == "" || $("#productBrandID").val() == "") {
            alert("请选择完整的商品类别和品牌后，再发布商品！");
            return false;
        } else {
            $("#selectCategoryAndBrandDiv").hide();

            if ($("#subBrandName").val() != "") {
                var text = $("#parentCategoryName").val() + " > " + $("#subCategoryName").val() + " > " + $("#parentBrandName").val() + " > " + $("#subBrandName").val();
                $("#productCategoryLabel").text(text);
            } else {
                text = $("#parentCategoryName").val() + " > " + $("#subCategoryName").val() + " > " + $("#parentBrandName").val();
                $("#productCategoryLabel").text(text);
            }
            $(".product_main").show();

            //抓取关联属性
            var option = {};
            option.categoryId = $("#productCategoryID").val();
            option.container = "product_attr";
            option.attributesValue = [];
            Attribute.Init(option);
        }
        return false;
    }
}

//产品图片
var ProductPicture = {
    productId: Number,

    //功能描述：初始化
    Init: function (productId) {
        if (isNaN(productId)) return;

        ProductPicture.productId = productId;
        ProductPicture.Load();
    },

    //功能描述：加载数据
    Load: function () {
        $.ajax({
            type: "GET",
            url: "/Product/GetPictureHtml",
            data: { productID: ProductPicture.productId },
            dataType: "json",
            success: function (data) {
                ProductPicture.Append(data);
            },
            error: function () {
                errorMessage();
            }
        });
    },

    //功能描述：绑定到指定位置
    Append: function (data) {
        if (!data || data == null || data.length == 0) return;

        var html = "";
        var pictureId = "", path = "", isMaster = "";
        for (var i = 0; i < data.length; i++) {
            pictureId = data[i].PictureID || "";
            path = data[i].Path || "";
            isMaster = data[i].IsMaster || -1;

            html += "<div class='productItem' id='productItem" + pictureId + "'>";
            if (isMaster == "1") {
                html += "<img class='productItemImg master' ondblclick='ProductPicture.SetMaster(this)' src='" + path + "' alt='' title='双击设为主图' data='" + pictureId + "' />";
            } else {
                html += "<img class='productItemImg' ondblclick='ProductPicture.SetMaster(this)' src='" + path + "' alt='' title='双击设为主图' data='" + pictureId + "' />";
            }
            html += "<div class='productItemBar'><a onclick='ProductPicture.GoLeft(this)'>左移</a><a onclick='ProductPicture.Delete(this)'>删除</a><a onclick='ProductPicture.GoRight(this)'>右移</a></div>";
            html += "</div>";
        }

        $("#productImgsDiv").append(html);
    },

    //功能描述：设为主图
    SetMaster: function (obj) {
        $(".productItem .master").removeClass("master");
        $(obj).addClass("master");
    },

    //功能描述：删除
    Delete: function (obj) {
        $(obj).parent().parent().remove();
    },

    //功能描述：左移
    GoLeft: function (obj) {
        var imgDiv = $(obj).parent().parent();
        imgDiv.prev().before(imgDiv);
    },

    //功能描述：右移
    GoRight: function (obj) {
        var imgDiv = $(obj).parent().parent();
        imgDiv.next().after(imgDiv);
    }
}

//产品保存
var product = {
    Add: function (status) {
        // 必填项
        if ($.trim($("#ProductName").val()) == "") {
            alert("商品名称不能为空！");
            return;
        }
        if ($.trim($("#Barcode").val()) == "") {
            alert("商品条形码不能为空！");
            return;
        }

        if ($.trim($("#GoujiuPrice").val()) == "") {
            alert("商品购酒网价格不能为空！");
            return;
        }
        if ($.trim($("#MarketPrice").val()) == "") {
            alert("商品市场价不能为空！");
            return;
        }
        var jsonData = {};
        //类目信息
        jsonData["product.ID"] = $("#ProductID").val() || "";
        jsonData["product.ParentCategoryID"] = $("#parentCategoryID").val();
        jsonData["product.ProductCategoryID"] = $("#productCategoryID").val();
        jsonData["product.ParentBrandID"] = $("#parentBrandID").val();
        jsonData["product.ProductBrandID"] = $("#productBrandID").val();

        //基本属性
        jsonData["product.Name"] = $("#ProductName").val();
        jsonData["product.Advertisement"] = $("#Advertisement").val();
        jsonData["product.Barcode"] = $("#Barcode").val();
        jsonData["product.GoujiuPrice"] = $("#GoujiuPrice").val();
        jsonData["product.InventoryNumber"] = $("#InventoryNumber").val();
        jsonData["product.MarketPrice"] = $("#MarketPrice").val();
        jsonData["product.Integral"] = $("#Integral").val();
        jsonData["product.SoldOfVirtual"] = $("#SoldOfVirtual").val();

        //SEO数据
        jsonData["product.SEOTitle"] = $("#SEOTitle").val();
        jsonData["product.SEODescription"] = $("#SEODescription").val();
        jsonData["product.SEOKeywords"] = $("#SEOKeywords").val();

        var index = -1;
        $("select[name='ProductAttribute']").each(function () {
            if ($(this).val() != -1) {
                index++;
                var temp = $(this).val();
                jsonData["productAttributeValueSets[" + index + "].AttributeID"] = temp.split("_")[0];
                jsonData["productAttributeValueSets[" + index + "].AttributeValueID"] = temp.split("_")[1];
            } else {
                jsonData["productAttributeValueSets[" + index + "].AttributeID"] = "-1";
                jsonData["productAttributeValueSets[" + index + "].AttributeValueID"] = "-1";
            }
        });

        //图片信息
        index = -1;
        $("#productImgsDiv .productItemImg").each(function () {
            if ($(this).attr("src") != "") {
                index++;
                jsonData["pictures[" + index + "]"] = $(this).attr("data");
            }
        });
        if (index < 0) {
            alert("请选择商品图片");
            return;
        }
        jsonData["masterPictureID"] = $("#productImgsDiv .master").length > 0 ? $("#productImgsDiv .master").attr("data") : "";

        var editor = CKEDITOR.instances.Introduce;
        var content = editor.getData();

        jsonData["product.Introduce"] = content || " ";
        jsonData["product.Status"] = status;
        jsonData["product.Attributes"] = Attribute.GetAttributeValue();
        var url = "/Product/Add";
        $.ajax({
            type: "POST",
            url: url,
            data: jsonData,
            datatype: "json",
            success: function (data) {
                if (data.State == "1") {
                    alert("保存成功！");
                    return true;
                } else {
                    alert(data.Message || "保存失败!");
                }
                return false;
            },
            error: function () {
                errorMessage();
            }
        });
    },

    // 修改
    Modify: function () {
        // 必填项
        if ($.trim($("#ProductName").val()) == "") {
            alert("商品名称不能为空！");
            return;
        }
        if ($.trim($("#Barcode").val()) == "") {
            alert("商品条形码不能为空！");
            return;
        }
        if ($.trim($("#Product_GoujiuPrice").val()) == "") {
            alert("商品购酒网价格不能为空！");
            return;
        }
        if ($.trim($("#Product_MarketPrice").val()) == "") {
            alert("商品市场价不能为空！");
            return;
        }
        var jsonData = {};
        //类目信息
        jsonData["product.ID"] = $("#ProductID").val();
        jsonData["product.ParentCategoryID"] = $("#parentCategoryID").val();
        jsonData["product.ProductCategoryID"] = $("#productCategoryID").val();
        jsonData["product.ParentBrandID"] = $("#parentBrandID").val();
        jsonData["product.ProductBrandID"] = $("#productBrandID").val();

        //基本属性
        jsonData["product.Name"] = $("#ProductName").val();
        jsonData["product.Advertisement"] = $("#Advertisement").val();
        jsonData["product.Barcode"] = $("#Barcode").val();
        jsonData["product.GoujiuPrice"] = $("#Product_GoujiuPrice").val();
        jsonData["product.InventoryNumber"] = $("#Product_InventoryNumber").val();
        jsonData["product.MarketPrice"] = $("#Product_MarketPrice").val();
        jsonData["product.Integral"] = $("#Product_Integral").val();
        jsonData["product.SoldOfVirtual"] = $("#Product_SoldOfVirtual").val();

        //SEO数据
        jsonData["product.SEOTitle"] = $("#SEOTitle").val();
        jsonData["product.SEODescription"] = $("#SEODescription").val();
        jsonData["product.SEOKeywords"] = $("#SEOKeywords").val();

        //商品属性(此处不再使用)
        var index = -1;
        $("select[name='ProductAttribute']").each(function () {
            if ($(this).val() != -1) {
                index++;
                var temp = $(this).val();
                jsonData["productAttributeValueSets[" + index + "].AttributeID"] = temp.split("_")[0];
                jsonData["productAttributeValueSets[" + index + "].AttributeValueID"] = temp.split("_")[1];
            } else {
                jsonData["productAttributeValueSets[" + index + "].AttributeID"] = "-1";
                jsonData["productAttributeValueSets[" + index + "].AttributeValueID"] = "-1";
            }
        });

        //图片信息
        index = -1;
        $("#productImgsDiv .productItemImg").each(function () {
            if ($(this).attr("src") != "") {
                index++;
                jsonData["pictures[" + index + "]"] = $(this).attr("data");
            }
        });
        jsonData["masterPictureID"] = $("#productImgsDiv .master").length > 0 ? $("#productImgsDiv .master").attr("data") : "";

        var editor = CKEDITOR.instances.Introduce;
        var content = editor.getData();

        jsonData["product.Introduce"] = content;
        jsonData["product.Status"] = $("#ProductStatus").val();
        jsonData["product.Attributes"] = Attribute.GetAttributeValue();
        var url = "/Product/Modify";
        $.ajax({
            type: "POST",
            url: url,
            data: jsonData,
            datatype: "json",
            success: function (data) {
                if (data.State == "1") {
                    alert("保存成功！");
                    return true;
                } else {
                    alert(data.Message || "保存失败!");
                }
                return false;
            },
            error: function () {
                errorMessage();
            }
        });
    }
};