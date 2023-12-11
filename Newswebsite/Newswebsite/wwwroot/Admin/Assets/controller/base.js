var base = {
    init: function () {

    },
    alert: function (type, message) {
        var result = '';
        switch (type) {
            case 'success':
                result = '<div class="example-alert"><div class="alert alert-fill alert-dismissible alert-success alert-icon"><em class="icon ni ni-check-circle"></em>' + message + '<button class="close" data-bs-dismiss="alert"></button></div></div>';
                break;
            case 'error':
                result = '<div class="example-alert"><div class="alert alert-fill alert-dismissible alert-danger alert-icon"><em class="icon ni ni-cross-circle"></em>' + message + '<button class="close" data-bs-dismiss="alert"></button></div></div>';
                break;
            case 'warning':
                result = '<div class="example-alert"><div class="alert alert-fill alert-dismissible alert-warning alert-icon"><em class="icon ni ni-alert-circle"></em>' + message + '<button class="close" data-bs-dismiss="alert"></button></div></div>';
                break;
            case 'infor':
                result = '<div class="example-alert"><div class="alert alert-fill alert-dismissible alert-info alert-icon"><em class="icon ni ni-alert-circle"></em> ' + message + '<button class="close" data-bs-dismiss="alert"></button></div></div>';
                break;
        }
        return result;
    },
    success: function (message) {
        toastr.success(message);
    },
    error: function (message) {
        toastr.error(message);
    },
    date: function (date) {
        if (date == '' || date == undefined || date == null) {
            return '';
        } else {
            var newdate = new Date(date);
            var month = newdate.getMonth() + 1;
            var day = newdate.getDate();
            var year = newdate.getFullYear();
            if (month < 10)
                month = "0" + month;
            if (day < 10)
                day = "0" + day;
            return day + "/" + month + "/" + year;
        }
    },
    datetime: function (date) {
        if (date == '' || date == undefined || date == null) {
            return '';
        } else {
            var newdate = new Date(date);
            var month = newdate.getMonth() + 1;
            var day = newdate.getDate();
            var year = newdate.getFullYear();
            var hour = newdate.getHours();
            var min = newdate.getMinutes();
            var sec = newdate.getSeconds();
            if (month < 10)
                month = "0" + month;
            if (day < 10)
                day = "0" + day;
            if (hour < 10)
                hour = "0" + hour;
            if (min < 10)
                min = "0" + min;
            if (sec < 10)
                sec = "0" + sec;
            return day + "/" + month + "/" + year + " " + hour + ":" + min + ":" + sec;
        }
    },
    money: function (number, unit) {
        return accounting.formatMoney(number, unit, 0, ".", ",", "%v%s");
    },
    number: function (number, unit) {
        return accounting.formatMoney(number, unit, 0, ".", ",", "%v%s");
    },
    copy: function (content) {
        var $temp = $("<input>");
        $("body").append($temp);
        $temp.val(content).select();
        document.execCommand("copy");
        $temp.remove();
    },
    CreateCKEditor: function (elName) {
        var editor = CKEDITOR.replace(elName, {
            height: 200,
            allowedContent: true,
            removePlugins: 'showblocks',
            filebrowserBrowseUrl: '/ckfinder/ckfinder.html',
            filebrowserImageBrowseUrl: '/ckfinder/ckfinder.html?type=Images',
            filebrowserFlashBrowseUrl: '/ckfinder/ckfinder.html?type=Flash',
            filebrowserUploadUrl: '/ckfinder/connector?command=QuickUpload&type=Files',
            filebrowserImageUploadUrl: '/ckfinder/connector?command=QuickUpload&type=Images',
            filebrowserFlashUploadUrl: '/ckfinder/connector?command=QuickUpload&type=Flash',
            filebrowserWindowWidth: '1000',
            filebrowserWindowHeight: '700',
            language: 'vi'

        }).on('instanceReady', function (ev) {
            var editor = ev.editor,
                dataProcessor = editor.dataProcessor,
                htmlFilter = dataProcessor && dataProcessor.htmlFilter;
            htmlFilter.addRules({
                elements: {
                    img: function (element) {
                        console.log(element.html);
                        var src = element.attributes.src;
                        if (src.indexOf(AppConfig.FEUrlRoot + "Uploads/") == -1) {
                            $.ajax({
                                type: "POST",
                                url: "/Handler/DownloadImageFromUrl",
                                async: false,
                                data: { imageUrl: src },
                                beforeSend: function () {
                                    backend.startLoading();
                                },
                                success: function (r) {
                                    backend.stopLoading();
                                    element.attributes.src = r.fileName;
                                    element.attributes['data-cke-saved-src'] = r.fileName;
                                    element.attributes.width = r.width;
                                    element.attributes.height = r.height;
                                    element.attributes.alt = r.fileName;
                                },
                                error: function () {
                                    backend.stopLoading();
                                }
                            });
                        }
                    }
                }
            });
        });
        return editor;
    }
};
$(document).ready(function () {

});