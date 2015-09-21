
var nodes, treeObj;
function onZtreeClick() {

}

function getFontCss(treeId, treeNode) {
	return (!!treeNode.highlight) ? { color: "#A60000", "font-weight": "bold"} : { color: "#333", "font-weight": "normal" };
}

function updateNodes(highlight) {
	if (!nodes) return;
	if (nodes.length == 0) return;

	for (var i = 0; i < nodes.length; i++) {
		var node = nodes[i];
		while (node != null) {
			node.highlight = highlight;
			treeObj.updateNode(node);
			node = node.getParentNode();
		}
	}
}

function fnGetData() {
	var begdt = new Date('2010/1/1');
	var enddt = new Date();
	var data = [];
	var count = enddt.getFullYear() - begdt.getFullYear() + 1;
	var begyear = begdt.getFullYear();
	var endyear = enddt.getFullYear();
	var month = enddt.getMonth();
	for (var i = 0; i < count; i++) {
		data.push({ "ID": i + 1, "PID": 0, "Name": begyear });
		for (var j = 1; j <= 12; j++) {
			if (begyear == endyear && j > month + 1) break;
			data.push({ "ID": (i + 1) * 12 + j, "PID": i + 1, "Name": begyear + "-" + j });
		}
		begyear++;
	}
	return data;
}

$(function () {
	var setting = {
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
			onClick: onZtreeClick
		},
		view: {
			fontCss: getFontCss
		}
	};
	var data = fnGetData();
	$.fn.zTree.init($("#lp"), setting, data);

	treeObj = $.fn.zTree.getZTreeObj("lp");
	if (!treeObj) return;

	var id = 0;
	$("#sch").keyup(function () {
		var fvalue = $(this).val() || "";

		clearInterval(id);
		id = setTimeout(function () {
			if (fvalue == null || fvalue == "") {
				updateNodes(false);
				return;
			}
			updateNodes(false);
			nodes = treeObj.getNodesByParamFuzzy("Name", fvalue, null);
			updateNodes(true);
		}, 300);
	});
});

// Grid列表过滤
function filterData() {
	var treeObj = $.fn.zTree.getZTreeObj("lp");
	var nodes = treeObj.getSelectedNodes();
	var filterYear = "";
	var filterMonth = "";
	if (nodes[0].PID == 0) {
		filterYear = nodes[0].Name;
	} else if (nodes[0].PID > 0) {
		filterMonth = nodes[0].Name;
	}
	debugger;
	return { filterYear: filterYear, filterMonth: filterMonth };
}
// 显示编辑界面
function ShowLandingPageEdit() {
	$.get("promote/LandingPageEdit", null, function (data) {
		$("#lpGrid").css("display", "none");
		$("#LpEdit").css("display", "block");
		$("#LpEdit").html(data);
		$("#modifyBtn").css("display", "none");
	});
}

// 取消、关闭编辑界面
function CancelEdit() {
	$("#lpGrid").css("display", "block");
	$("#LpEdit").css("display", "none");
	$("#LpEdit").html().empty();
}

// 添加Lp促销
function AddLandingPage() {
	var promoteName = $("#lpName").val();
	var LpStartTime = $("#LpStartTime").val();
	var LpEndTime = $("#LpEndTime").val();
	var lpLink = $("#lpLink").val();
	var LPContent = CKEDITOR.instances.LPContent.getData();

	if (promoteName == "") {
		alert("活动名称不能为空！");
		grade.focus("#lpName");
		return;
	}
	if (LpStartTime == "") {
		alert("活动开始时间不能为空！");
		grade.focus("#LpStartTime");
		return;
	}
	if (LpEndTime == "") {
		alert("活动结束时间不能为空！");
		grade.focus("#LpEndTime");
		return;
	}
	if (lpLink == "") {
		alert("活动链接地址不能为空！");
		grade.focus("#lpLink");
		return;
	}
	if (LPContent == "") {
		alert("促销活动内容不能为空！");
		grade.focus("#LPContent");
		return;
	}
	var json = {};
	json["Name"] = promoteName;
	json["StartTime"] = LpStartTime;
	json["EndTime"] = LpEndTime;
	json["Link"] = lpLink;
	json["Content"] = LPContent;

	$.post("promote/AddLandingPage", json, function (data) {
		alert(data.Message);
		if (data.State == 1) {
			$("#LandingPageGrid").data("kendoGrid").dataSource.read();
			$("#LandingPageGrid").data("kendoGrid").refresh();
			CancelEdit(); // 成功添加关闭编辑界面
		}
	});
}

// 显示修改页面
function UpdateLandingPage(e) {
	$.post("promote/QueryLandingPageByID", {landingPageId:e.name}, function(data) {
		if (data.State == 0) {
			alert(data.Message);
		} else {
			$.get("promote/LandingPageEdit", null, function (datahtml) {
				$("#lpGrid").css("display", "none");
				$("#LpEdit").css("display", "block");
				$("#LpEdit").html(datahtml);
				$("#addBtn").css("display", "none");
				$("#lpID").val(data.ID);
				$("#lpName").val(data.Name);
				$("#LpStartTime").data("kendoDateTimePicker").value(data.StartTime);
				$("#LpEndTime").data("kendoDateTimePicker").value(data.EndTime);
				$("#lpLink").val(data.Link);
				var editor = CKEDITOR.instances.LPContent;
				editor.setData(data.Content);
				
			});
		}
	});
}

// 修改Lp促销
function ModifyLandingPage() {
	var promoteID = $("#lpID").val();
	var promoteName = $("#lpName").val();
	var LpStartTime = $("#LpStartTime").val();
	var LpEndTime = $("#LpEndTime").val();
	var lpLink = $("#lpLink").val();
	var LPContent = CKEDITOR.instances.LPContent.getData();

	if (promoteName == "") {
		alert("活动名称不能为空！");
		grade.focus("#lpName");
		return;
	}
	if (LpStartTime == "") {
		alert("活动开始时间不能为空！");
		grade.focus("#LpStartTime");
		return;
	}
	if (LpEndTime == "") {
		alert("活动结束时间不能为空！");
		grade.focus("#LpEndTime");
		return;
	}
	if (lpLink == "") {
		alert("活动链接地址不能为空！");
		grade.focus("#lpLink");
		return;
	}
	if (LPContent == "") {
		alert("促销活动内容不能为空！");
		grade.focus("#LPContent");
		return;
	}
	var json = {};
	json["ID"] = promoteID;
	json["Name"] = promoteName;
	json["StartTime"] = LpStartTime;
	json["EndTime"] = LpEndTime;
	json["Link"] = lpLink;
	json["Content"] = LPContent;

	$.post("promote/ModifyLandingPage", json, function (data) {
		alert(data.Message);
		if (data.State == 1) {
			$("#LandingPageGrid").data("kendoGrid").dataSource.read();
			$("#LandingPageGrid").data("kendoGrid").refresh();
			CancelEdit(); // 成功添加关闭编辑界面
		}
	});
}
