var formID;
var fileType;

jQuery(document).ready(function () {
    jQuery("#file_upload").uploadify({
        'buttonImage': '../../JScript/uploadify/browse-btn.png',
        'auto': false,
        'multi':false,
        'successTimeout': 99999,
        'swf': '../../JScript/uploadify/uploadify.swf',
        'queueID': 'uploadfileQueue',
        'uploader': '../Upload/UploadHandler.ashx?FormID=' + formID,
        'fileSizeLimit': '0',
        'queueSizeLimit': 5,
        'progressData': 'speed',
        'overrideEvents': ['onDialogClose'],
        //'fileTypeExts': '*.rar;*.zip;*.7z;*.jpg;*.jpge;*.gif;*.png',
        'fileTypeExts': fileType,
        'onSelectError': function (file, errorCode, errorMsg) {
            switch (errorCode) {
                case -100:
                    alert("上傳的文檔數量已超過" + jQuery('#file_upload').uploadify('settings', 'queueSizeLimit') + "個文檔！");
                    break;
                case -110:
                    alert("文檔 [" + file.name + "] 大小超出系統限制的" + jQuery('#file_upload').uploadify('settings', 'fileSizeLimit') + "大小！");
                    break;
                case -120:
                    alert("文檔 [" + file.name + "] 大小異常！");
                    break;
                case -130:
                    alert("文檔 [" + file.name + "] 類型不正確！");
                    break;
            }
        },
        'onClearQueue': function (queueItemCount) {
            alert("取消上傳");
            return;
        },
        'onQueueComplete': function (queueData) {
            alert("文檔上傳成功！");
            return;
        }
    });
});