var advertiseLp = {
    zBatchSetting: Object,
    zLpSetting: Object,
    selectLp: String,
    websiteUrl: String,

    //功能描述：初始化
    Init: function () {
        advertiseLp.SetOption();
        advertiseLp.LoadEvent();
        advertiseLp.LoadData();
    },

    //功能描述：加载数据
    LoadData: function () {
        advertiseLp.SendRequest({ action: "Advertise/InitLpTree" }, function (data) {
            for (var i = 0; i < data.length; i++) {
                if (data[i].Type == 0) {
                    data[i].ID = "User_" + data[i].ID;
                }
            }
            advertiseLp.LoadTree({ treeId: "#lpTree", setting: advertiseLp.zLpSetting }, data);
        });

        advertiseLp.SendRequest({ action: "Advertise/InitLpTree" }, function (data) {
            for (var i = 0; i < data.length; i++) {
                if (data[i].Type == 0) {
                    data[i].ID = "User_" + data[i].ID;
                }
            }
            advertiseLp.LoadTree({ treeId: "#batchlpTree", setting: advertiseLp.zBatchSetting }, data);
        });
    },

    //功能描述：初始化
    SetOption: function () {
        advertiseLp.zLpSetting = {
            data: {
                key: {
                    name: "Name",
                    title: "Name"
                },
                simpleData: {
                    enable: true,
                    idKey: "ID",
                    pIdKey: "PID",
                    rootPId: 0
                }
            },
            callback: {
                onClick: advertiseLp.onZtreeClick
            }
        };

        advertiseLp.zBatchSetting = {
            check: {
                enable: true,
                autoCheckTrigger: false,
                chkboxType: { "Y": "ps", "N": "ps" },
                chkStyle: "checkbox",
                nocheckInherit: false,
                chkDisabledInherit: false,
                radioType: "level"
            },
            data: {
                key: {
                    name: "Name",
                    title: "Name"
                },
                simpleData: {
                    enable: true,
                    idKey: "ID",
                    pIdKey: "PID",
                    rootPId: 0
                }
            },
            callback: {
                onCheck: advertiseLp.OnZtreeChecked
            }
        };
        advertiseLp.websiteUrl = ($("#advertiseWebSiteUrl").val() || "").replace(/\/$/, '');
    },

    //功能描述：加载树
    LoadTree: function (opt, data) {
        if (data == null) {
            return;
        }

        var treeObj = $.fn.zTree.init($(opt.treeId), opt.setting, data);
        var node = treeObj.getNodeByTId(opt.treeId + "_1");
        if (node != null) {
            treeObj.expandNode(node, true, false, false);
        }
        node = treeObj.getNodeByTId(opt.treeId + "_2");
        if (node != null) {
            treeObj.expandNode(node, true, false, false);
        }
        node = treeObj.getNodeByTId(opt.treeId + "_3");
        if (node != null) {
            treeObj.expandNode(node, true, false, false);
        }
    },

    //功能描述：lp树点击事件
    onZtreeClick: function (e, treeId, treeNode) {
        var treeObj = $.fn.zTree.getZTreeObj("lpTree");
        var sNodes = treeObj.getSelectedNodes();
        if (sNodes.length > 0) {
            var isParent = sNodes[0].isParent;
            if (!isParent) {
                advertiseLp.selectLp = treeNode.ID;
            }
        }
    },

    //功能描述：批量lp树点击事件
    OnZtreeChecked: function (e, treeId, treeNode) {
        var treeObj = $.fn.zTree.getZTreeObj("batchlpTree");
        var arr = new Array();
        var sNodes = treeObj.getCheckedNodes(true);
        for (var i = 0; i < sNodes.length; i++) {
            var isParent = sNodes[i].isParent;
            if (!isParent) {
                arr.push(sNodes[i]);
            }
        }
        return arr;
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
    },

    //功能描述：获取图片地址
    GetImage: function () {
        var img = {};
        $("#idPicList img").each(function () {
            img.src = $(this).attr("src") || "";
            img.id = $(this).attr("imageId") || "0";
        });
        return img;
    },

    //功能描述：保存
    Save: function (url, edit) {
        var data = {};
        data.name = $("#advertiseName").val() || "";
        data.source = $("#dropdownlistSource").val() || "-1";
        data.url = $("#advertiseUrl").val() || "";
        data.indexId = $("#advertiseIndexID").val() || "-1";
        data.description = escape($("#advertiseDescription").val() || "");
        data.width = $("#advertiseWidth").val() || "0";
        data.height = $("#advertiseHeight").val() || "0";
        data.backgroundColor = $('#advertiseBackgroundColor').val() || "#ffffff";
        data.isParent = $("#MenuSetting").is(":checked");
        data.filter = $("#filter").is(":checked") == true ? 1 : 0;

        var img = advertiseLp.GetImage();
        data.thumbnailImagePath = img.src || "";
        data.imagePath = data.thumbnailImagePath.replace("_1.", ".");
        data.imageId = img.id || "0";
        data.imageId = data.imageId == "undefined" ? "" : data.imageId;

        if (edit == true) {
            data.id = $("#advertiseID").val();
        } else {
            data.pid = $("#hdTreeParent").val();
        }
        advertiseConfig.SendRequest({ action: url, data: data }, function (result) {
            alert("保存成功");
        });
    },

    //功能描述：绑定事件
    LoadEvent: function () {
        //功能描述：
        $("#btnSureLead").unbind("click").click(function () {
            var checkedlist = advertiseLp.OnZtreeChecked();
            var json = "";
            for (var i = 0; i < checkedlist.length; i++) {
                json += "," + $("#hdTreeParent").val() + ":" + checkedlist[i].Name;
            }
            advertiseLp.SendRequest({ action: "Advertise/BatchAddConfig", data: { batchJson: json.toString()} }, function (data) {
                alert("导入成功");
            });
        });

        //功能描述：
        $("#selectlpage").unbind("click").click(function () {
            $("#selectLpWindow").data("kendoWindow").open();
            $("#selectLpWindow").data("kendoWindow").center();
        });

        //功能描述：
        $("#selectProduct").unbind("click").click(function () {
            var window = $("#SelectProduct");
            window.data("kendoWindow").open();
            window.data("kendoWindow").center();
            advertiseLp.SendRequestHtml({ action: "Advertise/SelectProduct" }, function (html) {
                window.kendoWindow({
                    content: html
                });
            });
        });

        //功能描述：
        $("#btnSureLp").unbind("click").click(function () {
            var lp = advertiseLp.selectLp || "";
            if (lp == null || lp == "") {
                alert("请选择来源");
                return;
            } else {
                $("#advertiseUrl").val(GetWebSiteUrl(lp, "2"));
                $("#advertiseIndexID").val(lp);
            }
            $("#selectLpWindow").data("kendoWindow").close();
        });

        //功能描述：
        $("#btnCancelLp").unbind("click").click(function () {
            $("#selectLpWindow").data("kendoWindow").close();
        });

        //功能描述：
        $("#dropdownlistSource").kendoDropDownList({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [{ text: "选择来源", value: "0" }, { text: "选择商品", value: "1" }, { text: "选择LP", value: "2" }, { text: "其他来源", value: "3"}],
            close: function (e) {
                switch (this.value()) {
                    case "1": //选择商品
                        $("#pro_ID").css("display", "block");
                        $("#SelectProduct").data("kendoWindow").open();
                        $("#SelectProduct").data("kendoWindow").center();
                        break;
                    case "2": //选择LP
                        $("#pro_ID").css("display", "none");
                        $("#selectLpWindow").data("kendoWindow").open();
                        $("#selectLpWindow").data("kendoWindow").center();
                        break;
                    case "3":
                        $("#pro_ID").css("display", "none");
                        break;
                }
            }
        });

        //功能描述：添加一条配置信息
        $("#btnSureAdvertiseConfig").click(function () {
            advertiseLp.Save("Advertise/InsertAdvertiseConfig");
        });

        //功能描述：修改信息
        $("#btnUpdateAdvertiseConfig").unbind("click").click(function () {
            advertiseLp.Save("Advertise/UpdateConfig", true);
        });

        //功能描述：商品多项导入
        $("#MultiAddProduct").unbind("click").click(function () {
            var arr = new Array();
            var arrChk = $("input[name='multiSelect']:checked");
            var json = "";

            for (var i = 0; i < arrChk.length; i++) {
                arr.push({ "key": arrChk[i].value, "value": $(arrChk[i]).attr('attname') });
            }

            $.each(arr, function (index, element) {
                json += "," + $("#hdTreeParent").val() + ":" + element.value;
            });

            advertiseLp.SendRequest({ action: "Advertise/BatchAddConfig", data: { batchJson: json.toString()} }, function (data) {
                alert(data);
            });
        });
    }
};


//功能描述：获取商品\LP的地址
function GetWebSiteUrl(id, type) {
    if (!id || id == "") return "";

    var result = "";
    switch (type) {
        case "1": //商品
            result += advertiseLp.websiteUrl + "/product/item-id-" + id + ".htm";
            break;
        case "2": //LP
            result += advertiseLp.websiteUrl + "/Home/LandingPage/" + id + ".html";
            break;
        case "3":
            result += id;
            break;
    }
    return result;
}