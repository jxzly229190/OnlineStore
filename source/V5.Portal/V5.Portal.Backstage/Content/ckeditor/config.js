/**
 * @license Copyright (c) 2003-2013, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see LICENSE.md or http://ckeditor.com/license
 */

CKEDITOR.editorConfig = function (config) {
    // Define changes to default configuration here. For example:
    // config.language = 'fr';
    // config.uiColor = '#AADC6E';
    // config.font_names = "宋体/宋体;黑体/黑体;仿宋/仿宋_GB2312;楷体/楷体_GB2312;隶书/隶书;幼圆/幼圆;微软雅黑/微软雅黑";
    config.extraPlugins = "picturespace";
    //编辑器样式 三种：kama(默认)、office2003、 v2
    //工具栏（基础'Basic',全能'Full',自定义）plugins/toolbar/plugin.js
    config.toolbar = 'Full';

    // 换行方式
    config.enterMode = CKEDITOR.ENTER_BR;

    // 当输入：shift+Enter是插入的标签
    config.shiftEnterMode = CKEDITOR.ENTER_BR; // 
    //图片处理
    config.pasteFromWordRemoveStyles = true;
    config.filebrowserImageUploadUrl = "/Home/UploadImage?typeName=images";

    // 去掉ckeditor“保存”按钮
    config.removePlugins = 'save';

    // 修改成简体中文
    config.language = "zh-cn";

    // 允许内容
    config.allowedContent = true;
};