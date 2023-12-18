var login = {
    init: function () {
        $('#btnLogin').click(function () {
            login.login();
        });
        $('#btnRegister').click(function () {
            login.register();
        });
        $('#btnConfirmOTP').click(function () {
            login.confirmOTP();
        });
        $('#btnForgotPass').click(function () {
            login.checkEmail();
        });
        $('#btnConfirmOTPForgot').click(function () {
            login.confirmForgotOTP();
        });
        $('#btnResetPass').click(function () {
            login.resetPass();
        });
        $('#btnChangePassword').click(function () {
            login.changePassword();
        });
        $('#btnEditProfile').click(function () {
            login.editProfile();
        });
    },
    login: function () {
        $.ajax({
            url: '/Login/Login',
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
                    window.location.href = '/Home/Index';
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
    register: function () {
        $.ajax({
            url: '/Login/Register',
            type: 'post',
            data: $('#frmRegister').serialize(),
            beforSend: function () {
                $('#btnRegister').prop('disabled', true);
            },
            success: function (res) {
                $('#btnRegister').prop('disabled', false);
                if (res.status) {
                    base.success(res.message);
                    $('#myModalRegister').modal('hide');
                    $('#myModalLogin').modal('show');
                }
                else {
                    base.error(res.message);
                }
            },
            error: function () {
                base.error('Có lỗi xảy ra vui lòng thử lại');
            }
        });
    },
    confirmOTP: function () {
        $.ajax({
            url: '/Login/ConfirmRegisterOTP',
            type: 'post',
            data: {
                email: $('#ipEmail').val(),
                otpCode: $('#ipCodeOTP').val(),
            },
            beforSend: function () {
                $('#btnConfirmOTP').prop('disabled', true);
            },
            success: function (res) {
                $('#btnConfirmOTP').prop('disabled', false);
                if (res.status) {
                    base.success(res.message);
                    $('#myModalConfirmOTP').modal('hide');
                    $('#myModalResetPassword').modal('show');
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
    checkEmail: function () {
        $.ajax({
            url: '/Login/ForgotPassword',
            type: 'post',
            data: {
                email: $('#ipEmailConfirm').val(),
            },
            beforSend: function () {
                $('#btnForgotPass').prop('disabled', true);
            },
            success: function (res) {
                $('#btnForgotPass').prop('disabled', false);
                if (res.status) {
                    base.success(res.message);
                    $('#ipUserIdConfirmHidden').val(res.id);
                    $('#myModalConfirmMail').modal('hide');
                    $('#myModalConfirmOTP').modal('show');
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
    confirmForgotOTP: function () {
        $.ajax({
            url: '/Login/ConfirmForgotOTP',
            type: 'post',
            data: {
                Id: $('#ipUserIdConfirmHidden').val(),
                otpCode: $('#ipCodeOTP').val(),
            },
            beforSend: function () {
                $('#btnConfirmOTPForgot').prop('disabled', true);
            },
            success: function (res) {
                $('#btnConfirmOTPForgot').prop('disabled', false);
                if (res.status) {
                    base.success(res.message);
                    $('#myModalConfirmOTP').modal('hide');
                    $('#myModalResetPassword').modal('show');
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
    resetPass: function () {
        $.ajax({
            url: '/Login/ResetPassword',
            type: 'post',
            data: {
                Id: $('#ipUserIdConfirmHidden').val(),
                newPassword: $('#ipPasswordNewReset').val(),
                confirmPassword: $('#ipConfirmPasswordNewReset').val()
            },
            beforSend: function () {
                $('#btnResetPass').prop('disabled', true);
            },
            success: function (res) {
                $('#btnResetPass').prop('disabled', false);
                if (res.status) {
                    base.success(res.message);
                    $('#myModalResetPassword').modal('hide');
                    $('#myModalLogin').modal('show');
                }
                else {
                    base.error(res.message);
                }
            },
            error: function () {
                base.error('Có lỗi xảy ra vui lòng thử lại');
            }
        });
    },
    changePassword: function () {
        $.ajax({
            url: '/Login/ChangePassword',
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
                        window.location.href = '/Login/Logout';
                    }, 1500);
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
    editProfile: function () {
        var data = $('#frmEditProject').serialize();
        $.ajax({
            url: '/Login/Profile',
            type: 'post',
            data: data,
            dataType: 'json',
            beforSend: function () {
                $('#btnEditProfile').prop('disabled', true);
            },
            success: function (res) {
                $('#btnEditProfile').prop('disabled', false);
                if (res.status) {               
                    base.success(res.message);
                    $('#myModalProfile').modal('hide');
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
$(document).ready(function () {
    login.init();
});