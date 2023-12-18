var comment = {
    init: function () {
        comment.tblComment();
        $('#btnSearch').click(function () {
            comment.tblComment();
        });
    },
    tblComment: function () {
        $("#tblComment").bootstrapTable('destroy');
        $("#tblComment").bootstrapTable({
            method: 'get',
            url: "/Admin/Comment/GetCommentData",
            queryParams: function (p) {
                var param = $.extend(true, {
                    search: $('#txtSearch').val(),
                    startDate: $('#ipStartDate').val(),
                    endDate: $('#ipEndDate').val(),
                    offset: p.offset,
                    limit: p.limit
                }, p);
                return param;
            },
            formatLoadingMessage: function () {
                return '<span>Đang tải dữ liệu...</span>';
            },
            formatNoMatches: function () {
                return '<span>Không có dữ liệu</span>';
            },
            striped: true,
            sidePagination: 'server',
            pagination: true,
            paginationVAlign: 'bottom',
            search: false,
            pageSize: 10,
            pageList: [10, 50, 500],
            columns: [

                {
                    field: "cateName",
                    title: "Tên danh mục",
                    align: 'left',
                    valign: 'left',
                },    
                {
                    field: "username",
                    title: "Tài khoản",
                    align: 'left',
                    valign: 'left',
                }, 
                {
                    field: "title",
                    title: "Tiêu đề bài viết",
                    align: 'left',
                    valign: 'left',
                }, 
                {
                    field: "contentComment",
                    title: "Nội dung bình luận",
                    align: 'left',
                    valign: 'left',
                }, 
                {
                    field: "isActive",
                    title: "Trạng thái",
                    align: 'left',
                    valign: 'left',
                    formatter: function (value, row, index) {
                        var html = '';
                        if (value == true) {
                            html += '<button class="btn btn-success btn-sm btnActive">Kích hoạt</button>';
                        } else {
                            html += '<button class="btn btn-danger btn-sm btnActive">Hủy</button>';
                        }
                        return html;
                    },
                    events: {
                        'click .btnActive': function (e, value, row, index) {
                            $.confirm({
                                title: 'Cảnh báo!',
                                content: 'Bạn chắc chắn muốn thay đổi trạng thái bình luận này?',
                                buttons: {
                                    formSubmit: {
                                        text: 'Xác nhận',
                                        btnClass: 'btn btn-primary',
                                        action: function () {
                                            $.ajax({
                                                url: '/Admin/Comment/ChangeIsActive',
                                                type: 'post',
                                                data: {
                                                    Id: row.id,
                                                },
                                                success: function (res) {
                                                    if (res.status) {
                                                        base.success(res.message);
                                                        $("#tblComment").bootstrapTable('refresh', { silent: true });
                                                    }
                                                    else {
                                                        base.error(res.message);
                                                    }
                                                }
                                            });
                                        }
                                    },
                                    cancel: {
                                        text: 'Đóng',
                                        btnClass: 'btn btn-danger'
                                    },
                                }
                            });
                        }
                    }
                },
                {
                    field: "createdDate",
                    title: "Ngày tạo",
                    align: 'left',
                    valign: 'left',
                    formatter: function (value, row, index) {
                        if (value != null && value != undefined && value != '') {
                            return moment(value).format('DD/MM/YYYY');
                        }
                    }
                },
                {
                    title: "Chức năng",
                    valign: 'middle',
                    align: 'center',
                    class: 'CssAction',
                    formatter: function (value, row, index) {
                        var action = "";
                        action += '<a class=" btn btn-danger btn-sm btnDelete" title="Xóa"><i class="bx bx-trash"></i></a>';
                        return action;
                    },
                    events: {
                        'click .btnDelete': function (e, value, row, index) {
                            $.confirm({
                                title: 'Cảnh báo!',
                                content: 'Bạn chắc chắn muốn xóa bình luận này?',
                                buttons: {
                                    formSubmit: {
                                        text: 'Xác nhận',
                                        btnClass: 'btn btn-primary',
                                        action: function () {
                                            $.ajax({
                                                url: '/Admin/Comment/Delete',
                                                type: 'post',
                                                data: {
                                                    Id: row.id,
                                                },
                                                success: function (res) {
                                                    if (res.status) {
                                                        base.success(res.message);
                                                        $("#tblComment").bootstrapTable('refresh', { silent: true });
                                                    }
                                                    else {
                                                        base.error(res.message);
                                                    }
                                                }
                                            });
                                        }
                                    },
                                    cancel: {
                                        text: 'Đóng',
                                        btnClass: 'btn btn-danger'
                                    },
                                }
                            });
                        },
                    }
                },

            ],
            onLoadSuccess: function (data) {

            },
        })
    },
}
$(document).ready(function () {
    comment.init();
});