var BrandCategory = {
    zSetting: Object,
    treeId: String,
    //功能描述：初始化
    Init: function (opt) {
        BrandCategory.SetOption(opt);
        BrandCategory.LoadTree();
        BrandCategory.InitControls();
        BrandCategory.RefershPrduct();
        BrandCategory.LoadLinkUrl();
        Choose.Init({
            show_head_button: false,
            show_save_button: false

        });
    },
    //功能描述:树形控件设置
    SetOption: function (opt) {
        BrandCategory.zSetting = {
            data: {
                key: {
                    name: "Name",
                    title: "Name"
                },
                simpleData: {
                    enable: true,
                    idKey: "ID",
                    pIdKey: "PID",
                    rootPId: "0"
                }
            },
            callback: {
                onClick: BrandCategory.onZtreeClick
            }
        };
        BrandCategory.treeId = "#brandTree";
    },
    //功能描述：获取点击事件,加载对应的数据
    onZtreeClick: function (event, treeId, treeNode) {
        var treeobj = $.fn.zTree.getZTreeObj(treeId);
        var sNodes = treeobj.getSelectedNodes();
        if (sNodes.length > 0) {
            var isParent = sNodes[0].isParent;
            if (!isParent) {
                var stringId = treeNode.ID.substring(2);
                $("#hdbrandId").val(stringId); //给隐藏域赋商品Id的值
                BrandCategory.SendRequest({ action: "/Product/GetBrandInfoByBrandID", data: { brandId: stringId} }, function (data) {
                    if (data.State == 1) {
                        $("#imgpre").attr("src", data.Data.Logo); //初始化商品logo
                        CKEDITOR.instances.TitleCkedit.setData(data.Data.Title); //解说词编辑器
                        CKEDITOR.instances.IntroduceCkedit.setData(data.Data.Introduce); //内容编辑器
                    } else {
                        CKEDITOR.instances.TitleCkedit.setData("");
                        CKEDITOR.instances.IntroduceCkedit.setData("");
                    }
                });
                //刷新品牌链接数据
                BrandCategory.LoadLinkUrl();
                //刷新图片预览数据
                ImagePreviewEx.RemovePreview($("#idPicList img").attr("src"), $("#idPicList img").attr("imageId"));
                //刷新商品选数据
                BrandCategory.RefershPrduct();
            }
        }
    },
    LoadLinkUrl: function () {
        var filter = new Array();
        var grid = $("#ProductLinkTable").data("kendoGrid");
        grid.dataSource.filter(filter);
    },
    //功能描述：加载树
    LoadTree: function () {
        BrandCategory.SendRequest({ action: "/Product/QueryBrandTree" }, function (data) {
            $.fn.zTree.init($(BrandCategory.treeId), BrandCategory.zSetting, data);
        });
    },
    //功能描述：刷新商品选择库
    RefershPrduct: function () {
        var option = {};
        option.container = "#container";
        var selected = Choose.GetSelect();
        var arr = [];
        for (var i = 0; i < selected.length; i++) {
            if (!selected[i] == "underfined") {
                arr.push(selected[i]);
            }
        }
        option.selected = arr;
        Choose.Init(option);
    },
    //功能描述:初始化页面控件
    InitControls: function () {
        $("#postSave").click(function () {
            var brandId = $("#hdbrandId").val();
            if (brandId == "") {
                alert("请选择要添加的品牌");
                return;
            }
            var ImgSrc = $("#idPicList img").attr("src") == "" ? "" : $("#idPicList img").attr("src");
            var title = CKEDITOR.instances.TitleCkedit.getData();
            var selected = Choose.GetSelect();
            var jsonselect = selected.join(",");
            var Introduce = CKEDITOR.instances.IntroduceCkedit.getData();
            BrandCategory.SendRequest({ action: "/Product/ModifyAndInsert", data: { title: title, Introduce: Introduce, brandId: brandId, logo: ImgSrc, product: jsonselect} }, function (res) {
                if (res.Data == 1) {
                    alert(res.Message);
                }
            });
        });
        $("#imgpre").click(function () {
            var td = this.parentNode;
            td.removeChild(this);
            $("#imgMaster").css("display", "block");
        });

    },
    ReadData: function () {
        var brandId = $("#hdbrandId").val() == "" ? -1 : $("#hdbrandId").val();
        return {
            brandId: brandId
        };
    },
    //功能描述：发送请求
    SendRequest: function (opt, fn) {
        $.ajax({
            type: "POST",
            url: opt.action,
            data: opt.data,
            dataType: "json",
            success: function (data) {
                if ($.isFunction(fn)) {
                    fn(data);
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
    }
};
function BrandCateRead() {
    var brandId = $("#hdbrandId").val() == "" ? 2 : $("#hdbrandId").val();
    return {
        brandId: brandId
    };
}
function CreateData() {
    var brandId = $("#hdbrandId").val();
    if (brandId == "") {
        alert("请选择品牌");
    }
    return {
        brandId: brandId
    };
}