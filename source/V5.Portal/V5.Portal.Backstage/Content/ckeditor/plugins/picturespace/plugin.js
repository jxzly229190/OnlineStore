(function () {
    //Section 1 : 按下自定义按钮时执行的代码
    var a = {
        exec: function (editor) {
            window.selectPictureBtnClick();

            //创建回调函数
            window.pushPictureSpaceImage = function (imgSrc) {
                if (!imgSrc || imgSrc == "") return;
				var arrSrc = imgSrc.split(',');
                for (var i = 0; i < arrSrc.length; i++) {
                    if (arrSrc[i]) {
                        editor.insertHtml("<img src=\"" + arrSrc[i] + "\" alt=\"\" />");
                    }
                }
            }

            window.pushPictureSpaceImage(str_url.split(','));
        }
    },
    //Section 2 : 创建自定义按钮、绑定方法
    b = 'picturespace';
    CKEDITOR.plugins.add(b, {
        init: function (editor) {
            editor.addCommand(b, a);
            editor.ui.addButton('picturespace', {
                label: '图片空间',
                icon: this.path + 'picturespace.png',
                command: b
            });
        }
    });
})();