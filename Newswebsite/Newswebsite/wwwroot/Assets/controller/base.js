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
};
$(document).ready(function () {

});