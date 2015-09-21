var primission = {
    zSettingRole: Object,
    zSettingRes: Object,
    zSettingRights: Object,

    //功能描述：初始化
    Init: function (opt) {
        primission.SetOption(opt);
        primission.LoadData();
    },

    //功能描述：配置
    SetOption: function (opt) {
        primission.zSettingRole = {
            data: {
                key: {
                    name: "Name",
                    title: "Name"
                },
                simpleData: {
                    enable: true,
                    idKey: "ID",
                    pIdKey: "PID",
                    rootPId: "root"
                }
            },
            callback: {
                onClick: primission.onZtreeRoleClick
            }
        };
        primission.zSettingRes = {
            check: {
                enable: true
            },
            data: {
                key: {
                    name: "Description",
                    title: "Description"
                },
                simpleData: {
                    enable: true,
                    idKey: "Code",
                    pIdKey: "ParentCode",
                    rootPId: "root"
                }
            }
        };
        primission.zSettingRights = {
            data: {
                key: {
                    name: "Description",
                    title: "Description"
                },
                simpleData: {
                    enable: true,
                    idKey: "Code",
                    pIdKey: "ParentCode",
                    rootPId: 0
                }
            },
            callback: {
                onClick: primission.onZtreeRightsClick
            }
        };
    },

    //功能描述：加载数据
    LoadData: function () {
        $(".save_right").click(function () {
            primission.SaveRights();
        });

        primission.SendRequest({ action: "system/QueryRoleWithUser" }, function (data) {
            for (var i = 0; i < data.length; i++) {
                if (data[i].Type == 0) {
                    data[i].ID = "User_" + data[i].ID;
                }
            }
            data.push({ ID: 0, PID: "root", Name: "上海购酒网电子商务有限公司" });
            primission.LoadTree({ treeId: "#treeRole", setting: primission.zSettingRole }, data);
        });
        setTimeout(function () {
            primission.SendRequest({ action: "system/QueryAll" }, function (data) {
                data.push({ Code: 0, ParentCode: "root", Description: "功能权限" });
                primission.LoadTree({ treeId: "#treeRes", setting: primission.zSettingRes }, data);
                primission.LoadTree({ treeId: "#treeRights", setting: primission.zSettingRights }, data);
            });
        }, 300);
    },

    //功能描述：加载树
    LoadTree: function (opt, data) {
        if (data == null) { return; }

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

    //功能描述：加载权限
    LoadRights: function (mydata) {
        primission.SendRequest(mydata, function (data) {
            if (!data) return;

            var user_rights = data.toString().split('');
            var treeObj = $.fn.zTree.getZTreeObj("treeRes");
            for (var i = 0; i < user_rights.length; i++) {
                if (user_rights[i] == "1") {
                    var nodes = treeObj.getNodesByParam("Position", i, null);
                    if (nodes && nodes.length > 0) {
                        treeObj.checkNode(nodes[0], true, true);
                    }
                }
            }
        });
    },

    //功能描述：发起Ajax请求
    SendRequest: function (opt, fn) {
        $.ajax({
            type: "POST",
            url: opt.action,
            data: opt.data,
            datatype: "json",
            success: function (data) {
                if ($.isFunction(fn)) {
                    fn(data);
                }
            },
            error: function () {
                alert("请求失败，请联系系统管理员！");
            }
        });
    },

    //功能描述：角色树点击事件
    onZtreeRoleClick: function (e, treeId, treeNode) {
        var treeObj = $.fn.zTree.getZTreeObj("treeRes");
        treeObj.checkAllNodes(false);
        if (treeNode.Type == 1) {
            primission.LoadRights({ action: "/system/QueryRoleRight?roleId=" + treeNode.ID });
        } else {
            treeNode = treeNode.getParentNode();
            if (treeNode.Type == 1) {
                primission.LoadRights({ action: "/system/QueryRoleRight?roleId=" + treeNode.ID });
            }
        }
    },

    //功能描述：验证树点击事件
    onZtreeRightsClick: function (e, treeId, treeNode) {
        if (treeNode.isParent) return;
        if (isNaN(treeNode.Position)) return;
        if (Number(treeNode.Position) == Infinity) return;

        var position = Number(treeNode.Position);
        var message = primission.GetParentNode(treeNode);
        var condition = "";
        var user_name = "";
        var role_name = "";
        var role_id = "";
        var treeObj = null;

        treeObj = $.fn.zTree.getZTreeObj("treeRole");
        nodes = treeObj.getSelectedNodes();
        if (nodes && nodes.length > 0) {
            if (nodes[0].Type == 1) {
                role_id = nodes[0].ID || "";
                role_name = "角色：\"" + (nodes[0].Name || "") + "\"";
            } else {
                var treeNode = nodes[0].getParentNode();
                if (treeNode.Type == 1) {
                    role_id = treeNode.ID || "";
                    user_name = "用户：\"" + (treeNode.Name || "") + "\"";
                } else {
                    alert("请选择角色");
                    return;
                }
            }
        } else {
            alert("请选择角色");
            return;
        }
        var name = role_name + user_name;
        primission.SendRequest({ action: "/system/QueryRoleRight?roleId=" + role_id }, function (data) {
            if (!data) {
                alert(name + ", 没有\"" + message + "\"");
                return;
            }
            var user_rights = data.toString().split('');
            if (user_rights.length < position) {
                alert(name + ",没有\"" + message + "\"");
                return;
            } else {
                message = name + "," + (user_rights[position] == "1" ? "拥有\"" : "没有\"") + message + "\"";
                alert(message);
            }
        });
    },

    //功能描述：获取父节点信息
    GetParentNode: function (node) {
        if (!node) { return ""; }
        var path = node.Description || "";
        while (node.getParentNode()) {
            node = node.getParentNode();
            path = (node.Description || "") + "->" + path;
        }
        return path;
    },

    //功能描述：保存权限
    SaveRights: function () {
        var role_code = "";
        var user_code = "";
        var user_rights = "";
        var treeObj = null;
        var nodes = null;
        var mynode = null;

        treeObj = $.fn.zTree.getZTreeObj("treeRole");
        nodes = treeObj.getSelectedNodes();
        if (nodes && nodes.length > 0) {
            if (nodes[0].Type == 1) {
                if (!confirm("是否授权?")) return;
                role_code = (nodes[0].ID || "").toString();
            } else {
                alert("请选择角色");
                return;
            }
        } else {
            alert("请选择角色");
            return;
        }

        treeObj = $.fn.zTree.getZTreeObj("treeRes");
        var len = treeObj.transformToArray(treeObj.getNodes()).length;
        var rights = primission.CreateArray(len, "0");
        if (treeObj && treeObj.getSelectedNodes) {
            nodes = treeObj.getCheckedNodes(true);
            for (var i = 0; i < nodes.length; i++) {
                if (nodes[i].isParent) continue;
                if (isNaN(nodes[i].Position)) continue;
                if (Number(nodes[i].Position) == Infinity) continue;
                rights[Number(nodes[i].Position)] = "1";
            }
        }
        var user_rights = rights.join('');
        if (role_code == "") return;
        user_code = user_code || "0";
        primission.SendRequest({ action: "/system/ModifyRights", data: { roleId: role_code, userId: user_code, permissions: user_rights} }, function (data) {
            if (!data) return;
            if (data.State && data.State == -403) {
                alert(data.Message);
            } else {
                alert("授权完毕");
            }
        });
    },

    //功能描述：创建数组
    CreateArray: function (len, def) {
        def = def || "";
        var str = "";
        for (var i = 0; i < len; i++) {
            str += def;
        }
        return str.split('');
    }
}