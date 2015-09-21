var ImagePreviewEx = {
    //检测程序
    exts: String,
    paths: String,
    type: String,
    uploadUrl: String,
    removeUrl: String,

    Init: function (opt) {
        ImagePreviewEx.SetOption(opt);
    },

    //设置选项
    SetOption: function (opt) {
        ImagePreviewEx.exts = opt.exts || "jpg|jpeg|jpe|png|gif|bmp";
        ImagePreviewEx.paths = opt.paths || "|";
        ImagePreviewEx.type = opt.type || "0";
        ImagePreviewEx.uploadUrl = opt.uploadUrl || "/Home/UploadPicture";
        ImagePreviewEx.removeUrl = opt.removeUrl || "/Home/RemovePicture";

        if (opt.img && opt.img.length > 0) {
            for (var i = 0; i < opt.img.length; i++) {
                ImagePreviewEx.AddPreview(opt.img[i], opt.type);
            }
        }
    },

    //添加预览
    AddPreview: function (obj) {
        obj = document.getElementById(obj);
        var file = document.createElement("input");
        var img = document.createElement("img");

        var ip = new ImagePreview(file, img, {
            maxWidth: 150,
            maxHeight: 100,
            action: ImagePreviewEx.uploadUrl + "?type=" + ImagePreviewEx.type,
            onErr: function () { alert("载入预览出错！"); ImagePreviewEx.ResetFile(file); },
            onCheck: ImagePreviewEx.CheckPreview,
            onShow: ImagePreviewEx.ShowPreview
        });
        file.type = "file"; file.name = "pic";
        file.onchange = function () {
            ip.preview();
        };
        obj.appendChild(file);
    },

    //检测预览
    CheckPreview: function () {
        var value = this.file.value, check = true;
        if (!value) {
            check = false; alert("请先选择文件！");
        } else if (!RegExp("\.(?:" + ImagePreviewEx.exts + ")$$", "i").test(value)) {
            check = false; alert("只能上传以下类型：" + exts);
        } else if (ImagePreviewEx.paths.indexOf("|" + value + "|") >= 0) {
            check = false; alert("已经有相同文件！");
        }
        check || ImagePreviewEx.ResetFile(this.file);
        return check;
    },

    //移除预览
    RemovePreview: function (imagePath, imageId) {
        if (!imagePath || imagePath == "") return;
        if (!imageId || imageId == "") return;

        $.ajax({
            type: "POST",
            url: ImagePreviewEx.removeUrl,
            data: { path: imagePath, id: imageId },
            success: function (result) {
                if (result == "1") {
                    //删除成功
                }
            },
            error: function () {
                alert("请求失败");
            }
        });
    },

    //显示预览
    ShowPreview: function () {
        var id = this._id;
        var file = this.file, value = file.value, oThis = this;
        var td = this.file.parentNode.parentNode;
        td.removeChild(this.file.parentNode);
        td.appendChild(this.img);

        this.img.title = "双击移除";
        this.img.setAttribute("imageId", id);
        this.img.ondblclick = function () {
            if (!confirm("是否移除当前图片（此操作不可恢复）?")) return;

            ImagePreviewEx.RemovePreview($(this).attr("src"), $(this).attr("imageId"));

            var td = this.parentNode;
            td.removeChild(this);
            ImagePreviewEx.paths = ImagePreviewEx.paths.replace(value, "");

            var cid = td.getAttribute("cid");
            var div = document.createElement("div");
            div.id = cid;
            td.appendChild(div)
            ImagePreviewEx.AddPreview(cid);
            return false;
        };
        ImagePreviewEx.paths += value + "|";
    },

    //重置
    ResetFile: function (file) {
        file.value = ""; //ff chrome safari
        if (file.value) {
            if ($$B.ie) {//ie
                with (file.parentNode.insertBefore(document.createElement('form'), file)) {
                    appendChild(file); reset(); removeNode(false);
                }
            } else {//opera
                file.type = "text"; file.type = "file";
            }
        }
    }
}