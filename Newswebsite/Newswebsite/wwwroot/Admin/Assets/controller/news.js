var news = {
    init: function () {
        news.tblNews();
        $('#btnSeach').click(function () {
            news.tblNews();
        });
        $('#sltCategory').change(function () {
            news.getCateDetailById($(this).val(), 0);
        });
        var url = window.location.href;
        if (url.indexOf("CreateOrEdit") != -1) {
            if ($('#sltCategory').val() > 0 && $('#ipHiddenId').val() > 0) {
                news.getCateDetailById($('#sltCategory').val(), $('#ipHiddenId').val() > 0);
            }
        }

        $('#btnSubmit').click(function () {
            var formData = new FormData();
            formData.append("Id", $('#ipHiddenId').val());
            formData.append("CateId", $('#sltCategory').val());
            formData.append("CateDetaiId", $('#sltCategoryDetail').val());
            formData.append("Title", $('#ipTitle').val());  
            formData.append("ShortDescription", $('#ipShortDescription').val());
            var fileUpload = $("#ipFileUpload").get(0);
            var files = fileUpload.files;
            formData.append("fileUpload", files[0]);  
            var desc = CKEDITOR.instances['TxtDescription'].getData();
            formData.append("Description", desc);
            $.ajax({
                url: '/Admin/News/CreateOrEdit',
                type: 'post',
                processData: false,
                contentType: false,
                data: formData,
                success: function (res) {
                    if (res.status) {
                        base.success(res.message);
                        setTimeout(function () {
                            window.location.href = '/Admin/News';
                        }, 1500);
                    } else {
                        base.error(res.message);
                    }
                }
            });
        });
    },
    getCateDetailById: function (id, detaiid) {
        $.ajax({
            url: '/Admin/News/GetCateDetailByCateId',
            type: 'post',
            dataType:'json',
            data: {
                Id: id
            },
            success: function (res) {
                var html = '';
                if (res != null && res.length > 0) {
                    html += '<option value="0">--Chọn chi tiết danh mục--</option>';
                    for (var i = 0; i < res.length; i++) {
                        if (res[i].id == detaiid) {
                            html += '<option value="' + res[i].id + '" selected>' + res[i].name + '</option>';
                        } else {
                            html += '<option value="' + res[i].id + '">' + res[i].name + '</option>';
                        }
                    }
                }
                $('#sltCategoryDetail').empty;
                $('#sltCategoryDetail').html(html);
            }
        });
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
                                                        $("#tblNews").bootstrapTable('refresh', { silent: true });
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
                                                        $("#tblNews").bootstrapTable('refresh', { silent: true });
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
                                                        $("#tblNews").bootstrapTable('refresh', { silent: true });
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
                    field: "isOutstanding",
                    title: "Tin nổi bật",
                    align: 'left',
                    valign: 'left',
                    formatter: function (value, row, index) {
                        var html = '';
                        if (value == true) {
                            html += '<button class="btn btn-success btn-sm btnIsOutstanding">Hiển thị</button>';
                        } else {
                            html += '<button class="btn btn-danger btn-sm btnIsOutstanding">Không</button>';
                        }
                        return html;
                    },
                    events: {
                        'click .btnIsOutstanding': function (e, value, row, index) {
                            $.confirm({
                                title: 'Cảnh báo!',
                                content: 'Bạn chắc chắn muốn thay đổi trạng thái hiển thị tin tức nổi bật này?',
                                buttons: {
                                    formSubmit: {
                                        text: 'Xác nhận',
                                        btnClass: 'btn btn-primary',
                                        action: function () {
                                            $.ajax({
                                                url: '/Admin/News/ChangeIsOutstanding',
                                                type: 'post',
                                                data: {
                                                    Id: row.id,
                                                },
                                                success: function (res) {
                                                    if (res.status) {
                                                        base.success(res.message);
                                                        $("#tblNews").bootstrapTable('refresh', { silent: true });
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
                        action += '<a href="/Admin/News/CreateOrEdit?Id=' + row.id +'" class=" btn btn-primary btn-sm btnEdit" title="Sửa"><i class="bx bx-pencil"></i></a>\
                        <a class=" btn btn-danger btn-sm btnDelete" title="Xóa"><i class="ri-delete-bin-fill"></i></a>';
                        return action;
                    },
                    events: {
                        'click .btnDelete': function (e, value, row, index) {
                            $.confirm({
                                title: 'Cảnh báo!',
                                content: 'Bạn chắc chắn muốn xóa thông tin bài viết này không?',
                                buttons: {
                                    formSubmit: {
                                        text: 'Xác nhận',
                                        btnClass: 'btn btn-primary',
                                        action: function () {
                                            $.ajax({
                                                url: '/Admin/News/Delete',
                                                type: 'post',
                                                data: {
                                                    Id: row.id
                                                },
                                                success: function (res) {
                                                    if (res.status) {
                                                        base.success(res.message);
                                                        $("#tblNews").bootstrapTable('refresh', { silent: true });
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
    news.init();
});