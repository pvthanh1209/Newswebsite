var login = {
    init: function () {
        $('#btnLogin').click(function () {
            login.login();
        });
        $('#btnChangePassword').click(function () {
            login.changePassword();
        });
    },
    login: function () {
        $.ajax({
            url: '/Admin/Login/Login',
            type: 'post',
            data: {
                username: $('#username').val(),
                password: $('#password').val(),
            },
            beforSend: function () {
                $('#btnLogin').prop('disabled', true);
            },
            success: function (res) {
                $('#btnLogin').prop('disabled', false);
                if (res.status) {
                    base.success(res.message);
                    window.location.href = '/Admin/Home/Index';
                }
                else {
                    base.error(res.message);
                }
            },
            error: function () {
                base.error('Có lỗi xảy ra vui lòng thử lại');
            }
        })
    },
    changePassword: function () {
        $.ajax({
            url: '/Admin/Login/ChangePassword',
            type: 'post',
            dataType: "json",
            data: $('#frmChangePassword').serialize(),
            beforSend: function () {
                $('#btnChangePassword').prop('disabled', true);
            },
            success: function (res) {
                $('#btnChangePassword').prop('disabled', false);
                if (res.status) {
                    base.success(res.message);
                    setTimeout(function () {
                        window.location.href = '/Admin/Login/Logout';
                    }, 2000);
                }
                else {
                    base.error(res.message);
                }
            },
            error: function () {
                base.error('Có lỗi xảy ra vui lòng thử lại');
            }
        })
    },
};
$(document).on('keypress', function (e) {
    if (e.which == 13) {
        $('#btnLogin').trigger('click');
    }
});
$(document).on('keypress', function (e) {
    if (e.which == 13) {
        $('#btnChangePassword').trigger('click');
    }
});
$(document).ready(function () {
    login.init();
});