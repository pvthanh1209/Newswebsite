var news = {
    init: function () {
        $('#btnSendComment').click(function () {
            $.ajax({
                url: '/News/SendComment',
                method: 'post',
                dataType: 'json',
                data: {
                    newsId: $('#ipNewsDetailIdHidden').val(),
                    comment: $('#txtCommentDetail').val(),
                },
                success: function (res) {
                    if (res.status) {
                        base.success(res.message);
                        $('#txtCommentDetail').val('');
                    } else {
                        base.error(res.message);
                    }
                }
            });
        });
    }
}
$(document).ready(function () {
    news.init();
});