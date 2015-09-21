var ConfigPage = {
    zSettiing: Object,
    ///初始化帮助信息
    Init: function (opt) {
        ConfigPage.SetOption(opt);
        ConfigPage.LoadTree();
        ConfigPage.InitControlers();
        //初始化ckedit
    },
    SetOption: function (opt) {
        ConfigPage.zSettiing = {
            data: {
                keep: {
                    parent: true
                },
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
                onClick: ConfigPage.onZtreeClick
            }
        };
    },

    ///加载帮助树数据
    LoadTree: function () {
        if ($("#helperTree").length == 0) return;
        ConfigPage.SendRequest({ action: "Config/HelperTree" }, function (data) {
            $.fn.zTree.init($("#helperTree"), ConfigPage.zSettiing, data);
        });
    },
    //获取点击树节点动作
    onZtreeClick: function (event, treeId, treeNode) {
        var treeObj = $.fn.zTree.getZTreeObj(treeId);
        var sNodes = treeObj.getSelectedNodes();
        if (sNodes.length > 0) {
            var isParent = sNodes[0].isParent;
            if (isParent) {
                $("#hdParentId").val(treeNode.ID);
                $("#hdChildId").val(-1);
                $("#changtext").val("");
                CKEDITOR.instances.CkeditContent.setData("");
            }
            if (!isParent) {
                var childId = $("#hdChildId").val();
                //var parentId = $("#hdParentId").val();
                //获取子节点的内容赋给edit
                ConfigPage.SendRequest({ action: "Config/helperContent", data: { id: treeNode.ID} }, function (text) {
                    switch (text.Type) {
                        case 1:
                            CKEDITOR.instances.CkeditContent.setData(text.Content);
                            $("#changtext").val("");
                            $("#changtext").val(text.Name);
                            $("#hdChildId").val("");
                            $("#hdChildId").val(text.ID);
                            break;
                        case 2:
                            CKEDITOR.instances.Ckeditannouncement.setData(text.Content);
                            break;
                        case 3:
                            CKEDITOR.instances.CkeditpromoteMessage.setData(text.Content);
                            break;
                    }
                });

            }
        }
    },

    //发送请求
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
                }
            }
        });
    },
    ///初始化页面控件
    InitControlers: function () {
        //添加数据
        $("#btnAddHelper").click(function () {
            var contentName = $("#changtext").val();
            if (contentName == "") {
                alert("请输入目录名称");
                return;
            }
            ConfigPage.SendRequest({ action: "/Config/AddContent", data: { name: contentName} }, function (data) {
                alert(data.Message);
            });
        });
        ///修改数据
        $("#btnSureEdit").click(function () {
            var id = $("#hdChildId").val();
            var name = $("#changtext").val();
            var parentId = $("#hdParentId").val() == "" ? -1 : $("#hdParentId").val();
            var content = CKEDITOR.instances.CkeditContent.getData();
            ConfigPage.SendRequest({ action: "Config/ModifyContent", data: { ID: id, Type: 1, pid: parentId, Content: content, Name: name} }, function (recevie) {

                if (recevie.Data == 1) {
                    alert(recevie.Message);
                } else {
                    alert("操作失败");
                }
            }
            );
        });
    }
};

function loadDetail(id) {
    $.ajax({
        type: "POST",
        url: "Config/LoadDetail",
        data: { id: id },
        dataType: "html",
        success: function (html) {
            $("#modifyDetail").append(html);
            $("#newAnnounce").css("display", "none");
        },
        error: function () {
            try {
                console.log("请求失败");
            } catch (e) {

            }
        }
    });
}

function EditData(id) {
    var Content = CKEDITOR.instances.Ckeditannouncement.getData();
    var annName = $("#annName").val();
    if (id == -1) {
        $.ajax({
            url: "/Config/InserNewAnnouncetContent",
            dataType: "json",
            type: "POST",
            data: { Name: annName, Content: Content },
            success: function (res) {
                if (res.Data == 1) {
                    var filter = new Array();
                    var grid = $("#newannouncementGrid").data("kendoGrid");
                    grid.dataSource.filter(filter);
                    alert(res.Message);
                }
            }
        });
    } else if (id == -2) {
        $.ajax({
            url: "/Config/InsertPromoteMessage",
            dataType: "json",
            type: "POST",
            data: { Name: annName, Content: Content },
            success: function (res) {
                if (res.Data == 1) {
                    var filter = new Array();
                    var grid = $("#newannouncementGrid").data("kendoGrid");
                    grid.dataSource.filter(filter);
                    alert(res.Message);
                }
            }
        });
    }
    else {
        var parentId = $("#hdPid").val();
        $.ajax({
            url: "/Config/ModifyContent",
            dataType: "json",
            type: "POST",
            data: { ID: id, Name: annName, pid: parentId, Content: Content },
            success: function (res) {
                if (res.Data == 1) {
                    alert(res.Message);
                }
            }
        });
    }
}

//初始化窗口
function ShowWindow(id) {
    var window = $("#AnnounceWindow");
    window.kendoWindow({
        width: "800px",
        height: "430px",
        title: "添加属性",
        actions: ["Close"],
        position: { top: 230, left: 450 },
        modal: true
    });
    $("#hdIAnnId").val(id);
    loadConfigById(id);
    window.data("kendoWindow").open();
    //如果ckeditor实例不存在就创建
    if (!CKEDITOR.instances.Ckeditannouncement) {
        CKEDITOR.replace("Ckeditannouncement");
    }

}
//关闭窗口
function onClose() {
    var window = $("#AnnounceWindow");
    window.kendoWindow({
        width: "800px",
        height: "480px",
        title: "添加属性",
        actions: ["Close"],
        position: { top: 250, left: 500 },
        modal: true
    });
    var id = $("#hdIAnnId").val();
    EditData(id);
    window.data("kendoWindow").close();
}
//取消操作
function onCancel() {
    var window = $("#AnnounceWindow");
    window.kendoWindow({
        width: "800px",
        height: "480px",
        title: "添加属性",
        actions: ["Close"],
        position: { top: 250, left: 500 },
        modal: true
    });
    window.data("kendoWindow").close();
}
///删除除操作 
function deleteRow(id) {
    $.post("/Config/DeleteRow", { ID: id }, function (data) {
        alert("删除成功");
        var filter = new Array();
        var grid = $("#newannouncementGrid").data("kendoGrid");
        grid.dataSource.filter(filter);
    });
}

function loadConfigById(id) {
    if (id == -1 || id == -2) return;
    $.post("/Config/QueryContentById", { id: id }, function (data) {
        $("#annName").val(data.Name);
        CKEDITOR.instances.Ckeditannouncement.setData(data.Content);
    });
}
