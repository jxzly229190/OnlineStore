var advertiseConfig = {
    zSetting: Object,
    treeId: String,

    //功能描述：初始化
    Init: function (opt) {
        advertiseConfig.SetOption(opt);
        advertiseConfig.LoadTree();
    },

    //功能描述：参数设置
    SetOption: function (opt) {
        advertiseConfig.zSetting = {
            data: {
                key: {
                    name: "Name",
                    title: "Name"
                },
                simpleData: {
                    enable: true,
                    idKey: "ID",
                    pIdKey: "PID",
                    rootPId: null
                }
            },
            callback: {
                onClick: advertiseConfig.onZtreeClick
            }
        };

        advertiseConfig.treeId = "treeDemo";
    },

    //功能描述：加载树
    LoadTree: function () {
        advertiseConfig.SendRequest({ action: "Advertise/TreeNodes" }, function (data) {
            if (!data || data == null) {
                return;
            }
            var treeObj = $.fn.zTree.init($("#" + advertiseConfig.treeId), advertiseConfig.zSetting, data);
            var node = treeObj.getNodeByTId(advertiseConfig.treeId + "_1");
            if (node != null) {
                treeObj.expandNode(node, true, false, false);
            }
            node = treeObj.getNodeByTId(advertiseConfig.treeId + "_2");
            if (node != null) {
                treeObj.expandNode(node, true, false, false);
            }
            node = treeObj.getNodeByTId(advertiseConfig.treeId + "_3");
            if (node != null) {
                treeObj.expandNode(node, true, false, false);
            }
        });
    },

    //功能描述：加载列表
    LoadTable: function () {
        var filter = new Array();
        var grid = $("#AdvertiseGradView").data("kendoGrid");
        grid.dataSource.filter(filter);
        $("#advertiseList").css("display", "block");
        $("#advertiseEdit").css("display", "none");
    },

    //功能描述：加载页面
    LoadPage: function (url, id) {
        if (!url || url == "") return;

        //判断是否为编辑(以是否传入ID作为标识，ID不为空，则为编辑，ID为空，则为新增)
        if (!id || id == "") {
            if ($("#hdTreeParent").val() == "") {
                alert("请选择父节点");
                return;
            }
        }

        advertiseConfig.SendRequest({ action: url, data: { id: id }, type: "html" }, function (html) {
            $("#advertiseList").css("display", "none");
            $("#advertiseEdit").html(html);
            $("#advertiseEdit").css("display", "block");
        });
    },

    //功能描述：树节点单击事件
    onZtreeClick: function (event, treeId, treeNode) {
        console.log(treeNode.ID);
        //如果是父级节点，则显示列表，如果是子节点，则显示内容
        if (treeNode.isParent) {
            $("#hdTreeParent").val(treeNode.ID);
            advertiseConfig.LoadTable(treeNode);
        } else {
            $("#hdTreeParent").val("");
            advertiseConfig.LoadPage("Advertise/ModifiyConfig", treeNode.ID);
        }
    },

    //功能描述：删除记录
    RemoveRecord: function (id, filter, isParent) {
        if (isParent == true) {            
            return;
        }
        if (filter == 1) {
            alert("该节点不可以删除，请选 择其他节点");
            return;
        }

        var strChk = "";
        if (!id || id == "") {
            var arrChk = $("input[name='chkproduct']:checked");
            if (arrChk.length <= 0) {
                alert("请选择要删除的数据");
                return;
            }

            for (var i = 0; i < arrChk.length; i++) {
                strChk += "," + arrChk[i].value;
            }
            strChk = strChk.replace(/\,$/, "");
        } else {
            strChk = id;
        }
        if (!confirm("是否删除当前记录?")) return;

        advertiseConfig.SendRequest({ action: "Advertise/DeleteRow", data: { array: strChk} }, function () {
            alert("删除成功");
            advertiseConfig.LoadTable();
            advertiseConfig.LoadTree();
        });
    },

    //功能描述：刷新首页
    RefreshHome: function () {
        advertiseConfig.SendRequest({ action: "Advertise/RefreshHome" }, function () {
            alert("刷新完毕");
        });
    },

    //功能描述：全选
    SelectAll: function (obj) {
        if ($("#selectAll").is(":checked")) {
            $("input[name='chkproduct']").each(function () {
                this.checked = true;
            });
        } else {
            $("input[name='chkproduct']").each(function () {
                this.checked = false;
            });
        }
    },

    //功能描述：移除全选
    UnSelectAll: function () {
        if ($("#selectAll").is(":checked")) {
            $("#selectAll").attr("checked", false);
        }
    },

    //功能描述：排序
    IsUpOrder: function (id, pid) {
        advertiseConfig.SendRequest({ action: "Advertise/IsUpOrder", data: { id: id, pid: pid} }, function (res) {
            if (res.Data == 1) {
                advertiseConfig.LoadTable();
            }
        });
    },

    //功能描述：不可删除
    SetFilter: function () {
        var parentId = $("#hdTreeParent").val();
        this.SendRequest({ action: "/Advertise/FilterSetting", data: { id: parentId, filter: 1} }, function (res) {
            alert(res.Message);
            advertiseConfig.LoadTree();
        });
    },

    //功能描述：发送请求
    SendRequest: function (opt, fn) {
        opt.type = opt.type || "json";
        $.ajax({
            type: "POST",
            url: opt.action,
            data: opt.data,
            dataType: opt.type,
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