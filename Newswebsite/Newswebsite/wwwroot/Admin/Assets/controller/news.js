var news = {
    init: function () {
        news.tblNews();
    },
    tblNews: function () {
        $("#tblNews").bootstrapTable('destroy');
        $("#tblNews").bootstrapTable({
            method: 'get',
            url: "/Admin/News/GetNewsData",
            queryParams: function (p) {
                var param = $.extend(true, {
                    search: $('#txtSearch').val(),
                    cateId: $('#sltCateSearch').val(),
                    offset: p.offset,
                    limit: p.limit,
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
                    title: "Thể loại",
                    align: 'left',
                    valign: 'left',
                },
                {
                    field: "title",
                    title: "Tiêu đề",
                    align: 'left',
                    valign: 'left',
                },
                {
                    field: "thumb",
                    title: "Ảnh đại diện",
                    align: 'left',
                    valign: 'left',
                    formatter: function (value, row, index) {
                        if (value != null && value != '' && value != undefined) {
                            return '<image src ="/Uploads/Images/' + value + '" width="80" height="80"/>'
                        }
                    }
                },
                {
                    field: "fullName",
                    title: "Người tạo",
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
                            html += '<button class="btn btn-danger btn-sm btnActive">Ngừng hoạt động</button>';
                        }
                        return html;
                    },
                    events: {
                        'click .btnActive': function (e, value, row, index) {
                            $.confirm({
                                title: 'Cảnh báo!',
                                content: 'Bạn chắc chắn muốn thay đổi trạng thái bài viết này?',
                                buttons: {
                                    formSubmit: {
                                        text: 'Xác nhận',
                                        btnClass: 'btn btn-primary',
                                        action: function () {
                                            $.ajax({
                                                url: '/Admin/News/ChangeStatusAll',
                                                type: 'post',
                                                data: {
                                                    Id: row.id,
                                                    type: 1
                                                },
                                                success: function (res) {
                                                    if (res.status) {
                                                        base.success(res.message);
                                                        $("#tblAccount").bootstrapTable('refresh', { silent: true });
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
                    field: "isHome",
                    title: "Hiển thị trang chủ",
                    align: 'left',
                    valign: 'left',
                    formatter: function (value, row, index) {
                        var html = '';
                        if (value == true) {
                            html += '<button class="btn btn-success btn-sm btnIsHome">Hiển thị</button>';
                        } else {
                            html += '<button class="btn btn-danger btn-sm btnIsHome">Không</button>';
                        }
                        return html;
                    },
                    events: {
                        'click .btnIsHome': function (e, value, row, index) {
                            $.confirm({
                                title: 'Cảnh báo!',
                                content: 'Bạn chắc chắn muốn thay đổi trạng thái hiển thị trên trang chủ này?',
                                buttons: {
                                    formSubmit: {
                                        text: 'Xác nhận',
                                        btnClass: 'btn btn-primary',
                                        action: function () {
                                            $.ajax({
                                                url: '/Admin/News/ChangeStatusAll',
                                                type: 'post',
                                                data: {
                                                    Id: row.id,
                                                    type: 2
                                                },
                                                success: function (res) {
                                                    if (res.status) {
                                                        base.success(res.message);
                                                        $("#tblAccount").bootstrapTable('refresh', { silent: true });
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
                    field: "isHot",
                    title: "Tin nóng",
                    align: 'left',
                    valign: 'left',
                    formatter: function (value, row, index) {
                        var html = '';
                        if (value == true) {
                            html += '<button class="btn btn-success btn-sm btnIsHost">Hiển thị</button>';
                        } else {
                            html += '<button class="btn btn-danger btn-sm btnIsHost">Không</button>';
                        }
                        return html;
                    },
                    events: {
                        'click .btnIsHost': function (e, value, row, index) {
                            $.confirm({
                                title: 'Cảnh báo!',
                                content: 'Bạn chắc chắn muốn thay đổi trạng thái hiển thị tin tức nóng này?',
                                buttons: {
                                    formSubmit: {
                                        text: 'Xác nhận',
                                        btnClass: 'btn btn-primary',
                                        action: function () {
                                            $.ajax({
                                                url: '/Admin/News/ChangeStatusAll',
                                                type: 'post',
                                                data: {
                                                    Id: row.id,
                                                    type:3
                                                },
                                                success: function (res) {
                                                    if (res.status) {
                                                        base.success(res.message);
                                                        $("#tblAccount").bootstrapTable('refresh', { silent: true });
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
                    title: "Chức năng",
                    valign: 'middle',
                    align: 'center',
                    class: 'CssAction',
                    formatter: function (value, row, index) {
                        var action = "";
                        action += '<a class=" btn btn-primary btn-sm btnEdit" title="Sửa"><i class="bx bx-pencil"></i></a>';
                        return action;
                    },
                    events: {
                        'click .btnEdit': function (e, value, row, index) {
                            $('#ipHiddenId').val(row.id);
                            $('#ipFullName').val(row.fullName);
                            $('#ipEmail').val(row.email);
                            $('#ipPhoneNumber').val(row.phoneNumber);
                            $('#ipUsername').val(row.username).prop("readonly", true);
                            $('#ipPassword').val('');
                            $('#ipAddress').val(row.address);
                            $('#titleModal').text('Chỉnh sửa nhân viên');
                            $('#modalsAddEdit').modal('show');
                        },
                    }
                },

            ],
            onLoadSuccess: function (data) {

            },
        })
    },
}
}
$(document).ready(function () {
    news.init();
});