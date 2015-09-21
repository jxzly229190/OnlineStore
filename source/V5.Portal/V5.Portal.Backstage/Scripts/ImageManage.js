
$('[name=chk_list]:checkbox').click(function () {
    //定义一个临时变量，避免重复使用同一个选择器选择页面中的元素，提升程序效率。
    var $tmp = $('[name=chk_list]:checkbox');
    //用filter方法筛选出选中的复选框。并直接给CheckedAll赋值      
    $('#CheckedAll').attr('checked', $tmp.length == $tmp.filter(':checked').length);
});

//删除图片
function deleteImage() {
    var currPage = $("#currPage").val();
    var checkedImgId = "";
    var arrChk = $("input[name='chk_list']:checked");
    if (arrChk.length < 1) {
        jdalert("请选择图片进行操作！");
        return;
    }

    for (var i = 0; i < arrChk.length; i++) {
        checkedImgId += arrChk[i].value + ",";
    }
    checkedImgId = checkedImgId.substring(0, checkedImgId.length - 1);

    jdconfirm("确定要删除选择图片？", ok);
    function ok() {
        jQuery.ajax({
            url: '/imginfo/batchDeleteImage.action?imgIds=' + checkedImgId,
            encoding: 'UTF-8',
            dataType: 'json',
            async: false,
            data: {
                checkedImgId: checkedImgId
            },
            success: function (data) {
                if (data.retFlag == "Y") {
                    var showType = $("#showType").val();
                    showmsg(data.retMsg, 'succeed', 10, function () {
                        doQuery(showType, currPage);
                    });
                } else {
                    jdalert(data.retMsg);
                }
            },
            error: function (data) {
                jdalert("操作失败！");
            }
        });
    }
}

//更新图片名称
function updateImgNameById(imgId, imgName) {
    $("#span_" + imgId).html(imgName);
    $("#imgName_" + imgId).val(imgName);
}

//根据图片编号获取单条图片信息
function getImgInfoById(imgId) {
    var obj = new Object();
    obj.imgName = $("#imgName_" + imgId).val();
    obj.imgType = $("#imgType_" + imgId).val();
    obj.imgSize = $("#imgSize_" + imgId).val();
    obj.imgWidth = $("#imgWidth_" + imgId).val();
    obj.imgHeight = $("#imgHeight_" + imgId).val();
    obj.created = $("#created_" + imgId).val();
    obj.imgUrl = $("#imgUrl_" + imgId).val();
    return obj;
}

//根据图片编号获取图片URL
function getImgUrlById(imgId) {
    var temp = "";
    var imgUrl = "";
    $('input[name^="item_img_info"]').each(function () {
        temp = $(this).val();
        obj = temp.split(",");
        if (imgId == obj[0]) {
            imgUrl = obj[1];
        }
    });
    return imgUrl;
}

//获取查询条件信息
function getQueryInfo() {
    var qryKey = $("#qryKey").val();
    if (qryKey != null && qryKey != "") {
        qryKey = trim(qryKey);
    }
    var obj = new Object();
    obj.startDate = $("#startDate").val();
    obj.endDate = $("#endDate").val();
    obj.imgCategory = $("#categoryId").val();
    obj.qryKeyType = $("#qryKeyType").val();
    obj.qryKey = qryKey;
    obj.showType = $("#showType").val();
    obj.isUsed = $("#isUsed").val();
    obj.orderByDate = $("#orderByDate").val();
    return obj;
}

//创建预览用URL代码块
function createPreviewImgUrl() {
    var imgFullUrl = $("#imgFullUrl").val();
    var imgType = "";
    var imgId = "";
    var imgUrl = "";
    var temp = "";
    var obj = "";
    var html = "";
    html += '<ul>';
    $('input[name^="item_img_info"]').each(function () {
        temp = $(this).val();
        obj = temp.split(",");
        imgId = obj[0];
        imgType = $("#imgType_" + imgId).val();
        if (imgType == "gif" || imgType == "GIF") {
            imgUrl = imgFullUrl + obj[1]; //gif没有缩略图，特殊处理
        } else
            imgUrl = imgFullUrl + "s350x350_" + obj[1];

        html += '<li id=\"li_' + imgId + '\" ><img id=\"' + imgId + '\" src=\"' + imgUrl + '\" width=\"46\" height=\"46\" /></li>';
    });

    html += '</ul>';
    return html;
}

//预览图片
function previewImage(imgId) {
    var imgUrl = $("#imgUrl_" + imgId).val();
    var imgType = $("#imgType_" + imgId).val();
    var currPage = $("#currPage").val();
    jQuery.jdThickBox({
        type: "iframe",
        title: "预览",
        source: "/imginfo/toPreviewImagePage.action?imgId=" + imgId + "&imgUrl=" + imgUrl + "&imgType=" + imgType + "&page=" + currPage,
        width: 805,
        height: 465,
        //_title: "thicktitler",
        //_close: "thickcloser",
        close: false,
        _con: "thickconr"
    });
};

//换图
function replaceImage(imgId) {
    var imgUrl = $("#imgUrl_" + imgId).val();
    var imgSize = $("#imgSize_" + imgId).val();
    var currPage = $("#currPage").val();

    if (typeof (Worker) !== "undefined") {
        //alert("支持HTML5");
        jQuery.jdThickBox({
            type: "iframe",
            title: "换图",
            source: "/imguploadnew/toReplaceImagePage.action?imgId=" + imgId + "&imgSize=" + imgSize + "&page=" + currPage,
            width: 350,
            height: 100,
            _title: "thicktitler",
            _close: "thickcloser",
            close: false,
            _con: "thickconr"
        });

    } else {
        //alert("不支持HTML5");
        jQuery.jdThickBox({
            type: "iframe",
            title: "换图",
            source: "/imgupload/toReplaceImagePage.action?imgId=" + imgId + "&imgSize=" + imgSize + "&page=" + currPage,
            width: 350,
            height: 100,
            _title: "thicktitler",
            _close: "thickcloser",
            close: false,
            _con: "thickconr"
        });

    }


};

//设置分类
function setCategory() {
    var currPage = $("#currPage").val();
    var checkedImgId = "";
    var arrChk = $("input[name='chk_list']:checked");
    if (arrChk.length < 1) {
        jdalert("请选择图片进行操作！");
        return;
    }

    for (var i = 0; i < arrChk.length; i++) {
        checkedImgId += arrChk[i].value + ",";
    }
    checkedImgId = checkedImgId.substring(0, checkedImgId.length - 1);
    if (checkedImgId != "") {
        jQuery.jdThickBox({
            type: "iframe",
            title: "设置分类",
            source: "/imginfo/toSetCategoryPage.action?imgIds=" + checkedImgId + "&page=" + currPage,
            width: 350,
            height: 100,
            _title: "thicktitler",
            _close: "thickcloser",
            close: false,
            _con: "thickconr"
        });
    }

};

//复制链接
function copyLink() {
    var checkedImgUrl = "";
    var arrChk = $("input[name='checkname']:checked");
    if (arrChk.length < 1) {
        alert("请选择图片进行操作！");
        return;
    }

    for (var i = 0; i < arrChk.length; i++) {
        var temp = arrChk[i].id;
        checkedImgUrl += temp + "\r\n";
    }
    alert(checkedImgUrl);
    checkedImgUrl = checkedImgUrl.substring(0, checkedImgUrl.length);
    alert(checkedImgUrl);
    if (checkedImgUrl != "") {

        var url = checkedImgUrl;

        window.clipboardData.clearData();
        window.clipboardData.setData('Text', url);

        var data = window.clipboardData.getData('Text');
        if (data != null && data != "") {
            //显示或者隐藏效果
            //    			$(".copyl-tip").show();
            //    			$(".copyl-tip").fadeOut(2000);
        }

    }
}



//复制代码

function copyCode() {
    var checkedImgCode = "";
    var arrChk = $("input[name='checkname']:checked");
    if (arrChk.length < 1) {
        alert("请选择图片进行操作！");
        return;
    }
    var altVal = "";
    for (var i = 0; i < arrChk.length; i++) {
        var temp = arrChk[i].id;
        checkedImgCode += "<img  src=\"" + temp + "\">" + "\r\n";
    }
    if (checkedImgCode != "") {
        var code = checkedImgCode;
        window.clipboardData.clearData();
        window.clipboardData.setData("Text", code);
        var data = window.clipboardData.getData('Text');
        if (data != null && data != "") {
            //显示或者隐藏效果
            //            $(".copyc-tip").show();
            //            $(".copyc-tip").fadeOut(2000);

        }
    }
}

//去掉空格
function trim(str) {
    if (str == null) {
        return "";
    }
    return str.replace(/^\s*(.*?)[\s\n]*$/g, '$1');
}

//关闭弹层


/**
* URL添加时间戳
* @param url
* @returns
*/
function convertURL(url) {
    var timstamp = new Date().getTime();
    if (url.indexOf("?") > -1) {
        url = url + "&t=" + timstamp;
    } else {
        url = url + "?t=" + timstamp;
    }
    return url;
}

/**
* 删除图片-用于图片预览
* @param imgId
* @returns
*/
function deleteImageById(imgId) {
    $(".right ul li img").each(function () {
        var checkNode = $(this).next();
        var value = checkNode.attr("value");
        var imageUrl = "/common/images/deleted_big.png";
        if (imgId == value) {
            $(this).attr({ "src": imageUrl });
            checkNode.attr({ "name": "chk_list1" });
            checkNode.attr({ "disabled": "disabled" });
            $("#opts_" + imgId).remove();
            $("#item_" + imgId).attr({ "name": "N" });
        }
    });

    $(".right table tr td div img").each(function () {
        var checkNode = $(this).parent().parent().prev().children();
        var trNode = $(this).parent().parent().parent();
        var tdNode = trNode.children().eq(7);
        var aNodes = tdNode.children();
        var value = checkNode.attr("value");
        var imageUrl = "/common/images/deleted_small.png";
        if (imgId == value) {
            aNodes.eq(0).removeAttr("target");
            aNodes.eq(0).attr({ "href": "javascript:void(0)" });
            aNodes.eq(1).removeAttr("onClick");
            $(this).attr({ "src": imageUrl });
            checkNode.attr({ "name": "chk_list1" });
            checkNode.attr({ "disabled": "disabled" });
            $("#item_" + imgId).attr({ "name": "N" });
        }
    });
}


/**
* 替换图片-用于图片预览
* @param imgId
* @returns
*/
function replaceImageById(imgId, imgName, imgType, imgWidth, imgHeight, imgSize, formatSize) {
    $(".right ul li img").each(function () {
        var checkNode = $(this).next();
        var value = checkNode.attr("value");
        if (imgId == value) {
            var src = $(this).attr("src");
            src = generateTimestampURL(src);
            $(this).removeAttr('data-original');
            $(this).attr("data-original", src);
            $(this).removeAttr('src');
            $(this).attr("src", src);

            $("#span_" + imgId).text(imgName);
            $("#imgName_" + imgId).val(imgName);
            $("#imgType_" + imgId).val(imgType);
            $("#imgWidth_" + imgId).val(imgWidth);
            $("#imgHeight_" + imgId).val(imgHeight);
            $("#imgSize_" + imgId).val(imgSize);
            var imgFullUrl = $("#imgFullUrl").val();
            var imgUrl = $("#imgUrl_" + imgId).val();
            imgUrl = generateTimestampURL(imgUrl);
            $("#imgUrl_" + imgId).val(imgUrl);
            $("#item_" + imgId).val(imgId + "," + imgUrl);
            $("#view_" + imgId).attr({ "href": imgFullUrl + imgUrl });
        }
    });


    $(".right table tr td div img").each(function () {
        var checkNode = $(this).parent().parent().prev().children();
        var value = checkNode.attr("value");
        if (imgId == value) {
            var src = $(this).attr("src");
            src = generateTimestampURL(src);
            $(this).removeAttr('data-original');
            $(this).attr("data-original", src);
            $(this).removeAttr('src');
            $(this).attr("src", src);

            $("#span_" + imgId).text(imgName);
            $("#imgName_" + imgId).val(imgName);
            $("#imgType_" + imgId).val(imgType);
            $("#imgWidth_" + imgId).val(imgWidth);
            $("#imgHeight_" + imgId).val(imgHeight);
            $("#imgSize_" + imgId).val(imgSize);
            var imgFullUrl = $("#imgFullUrl").val();
            var imgUrl = $("#imgUrl_" + imgId).val();
            imgUrl = generateTimestampURL(imgUrl);
            $("#imgUrl_" + imgId).val(imgUrl);
            $("#item_" + imgId).val(imgId + "," + imgUrl);
            $("#view_" + imgId).attr({ "href": imgFullUrl + imgUrl });

            var trNode = $(this).parent().parent().parent();
            var imgTypeNode = trNode.children().eq(2);
            var dimensionNode = trNode.children().eq(3);
            var imgSizeNode = trNode.children().eq(4);
            imgTypeNode.text(imgType);
            dimensionNode.text(imgWidth + "×" + imgHeight);
            imgSizeNode.text(formatSize);
        }

    });
}


//生成URL的时间戳
function generateTimestampURL(url) {
    var timestamp = new Date().getTime();
    var index = url.lastIndexOf("?t=");
    if (index > -1) {
        url = url.substring(0, index);
    }
    url += "?t=" + timestamp;
    return url;
}

//去掉URL的时间戳
function removeURLTimestamp(url) {
    var index = url.lastIndexOf("?t=");
    if (index > -1) {
        url = url.substring(0, index);
    }
    return url;
}

