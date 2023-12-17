var categories = {
    init: function () {
        categories.tblCategories();
        categories.tblCategoriesDetail();
        $('#btnSeach').click(function () {
            categories.tblCategories();s
        });
        $('#btnCreate').click(function () {
            $('#titleModal').text('Thêm mới danh mục bài viết');
            $('#ipHiddenId').val(0);
            $('#ipTitle').val('');
            $('#ipCateName').val('');
            $('#ipShortDescription').val('');
            $('#modalsAddEdit').modal('show');
        });
        $('#btnCreateDetail').click(function () {
            $('#titleModaldetail').text('Thêm mới danh mục chi tiết');
            $('#ipHiddenDetailId').val(0);
            $('#ipCateNameDetail').val('');
            $('#ipTitleDetail').val('');
            $('#ipFileUpload').val('');
            $('#modalsAddEditDetail').modal('show');
        });
        $('#btnSubmitDetail').click(function () {
            var formData = new FormData();
            formData.append("Id", $('#ipHiddenDetailId').val());
            formData.append("CateId", $('#ipCateIdHiddenModel').val());
            formData.append("Name", $('#ipCateNameDetail').val());
            formData.append("Title", $('#ipTitleDetail').val());
            var fileUpload = $("#ipFileUpload").get(0);
            var files = fileUpload.files;
            formData.append("fileUpload", files[0]);     
            $.ajax({
                url: '/Admin/Categories/CreateOrEditDetail',
                type: 'post',
                processData: false,
                contentType: false,
                data: formData,
                success: function (res) {
                    if (res.status) {
                        $('#modalsAddEditDetail').modal('hide');
                        $("#tblCategoryDetail").bootstrapTable('refresh', { silent: true });
                        base.success(res.message);
                    } else {
                        base.error(res.message);
                    }
                }
            });
        });
        $('#btnSubmit').on('click', function () {
            var formData = new FormData();
            formData.append("Id", $('#ipHiddenId').val());
            formData.append("CateName", $('#ipCateName').val());
            formData.append("Title", $('#ipTitle').val());
            formData.append("ShortDescription", $('#ipShortDescription').val());
            formData.append("Parents", $('#sltCategories').val());        
            var fileUpload = $("#ipFileUpload").get(0);
            var files = fileUpload.files;
            formData.append("fileUpload", files[0]);         
            $.ajax({
                url: '/Admin/Categories/CreateOrEdit',
                type: 'post',
                processData: false,
                contentType: false,
                data: formData,
                success: function (res) {
                    if (res.status) {
                        $('#modalsAddEdit').modal('hide');
                        $("#tblCategory").bootstrapTable('refresh', { silent: true });
                        base.success(res.message);
                    } else {
                        base.error(res.message);
                    }
                }
            });
        });
    },
    tblCategories: function () {
        $("#tblCategory").bootstrapTable('destroy');
        $("#tblCategory").bootstrapTable({
            method: 'get',
            url: "/Admin/Categories/GetCategoryListAll",
            queryParams: function (p) {
                var param = $.extend(true, {
                    search: $('#txtSearch').val(),
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
                    formatter: function (value, row, index) {
                        return '<a href="/Admin/Categories/CategroiesDetail?cateId=' + row.id + '">' + value + '</a>'
                    }
                },
                {
                    field: "thumb",
                    title: "Hình ảnh",
                    align: 'left',
                    valign: 'left',
                    formatter: function (value, row, index) {
                        if (value != null && value != '' && value != undefined) {
                            return '<image src ="/Uploads/Images/' + value +'" width="80" height="80"/>'
                        }
                    }
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
                                content: 'Bạn chắc chắn muốn thay đổi trạng thái danh mục này?',
                                buttons: {
                                    formSubmit: {
                                        text: 'Xác nhận',
                                        btnClass: 'btn btn-primary',
                                        action: function () {
                                            $.ajax({
                                                url: '/Admin/Categories/ChangeIsActive',
                                                type: 'post',
                                                data: {
                                                    Id: row.id,
                                                },
                                                success: function (res) {
                                                    if (res.status) {
                                                        base.success(res.message);
                                                        $("#tblCategory").bootstrapTable('refresh', { silent: true });
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
                    field: "isMenu",
                    title: "Menu",
                    align: 'left',
                    valign: 'left',
                    formatter: function (value, row, index) {
                        var html = '';
                        if (value == true) {
                            html += '<button class="btn btn-success btn-sm btnIsMenu">Hiển thị</button>';
                        } else {
                            html += '<button class="btn btn-danger btn-sm btnIsMenu">Không</button>';
                        }
                        return html;
                    },
                    events: {
                        'click .btnIsMenu': function (e, value, row, index) {
                            $.confirm({
                                title: 'Cảnh báo!',
                                content: 'Bạn chắc chắn muốn thay đổi trạng thái danh mục này?',
                                buttons: {
                                    formSubmit: {
                                        text: 'Xác nhận',
                                        btnClass: 'btn btn-primary',
                                        action: function () {
                                            $.ajax({
                                                url: '/Admin/Categories/ChangeIsMenu',
                                                type: 'post',
                                                data: {
                                                    Id: row.id,
                                                },
                                                success: function (res) {
                                                    if (res.status) {
                                                        base.success(res.message);
                                                        $("#tblCategory").bootstrapTable('refresh', { silent: true });
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
                                content: 'Bạn chắc chắn muốn thay đổi trạng thái hiển thị trang chủ này?',
                                buttons: {
                                    formSubmit: {
                                        text: 'Xác nhận',
                                        btnClass: 'btn btn-primary',
                                        action: function () {
                                            $.ajax({
                                                url: '/Admin/Categories/ChangeIsHome',
                                                type: 'post',
                                                data: {
                                                    Id: row.id,
                                                },
                                                success: function (res) {
                                                    if (res.status) {
                                                        base.success(res.message);
                                                        $("#tblCategory").bootstrapTable('refresh', { silent: true });
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
                        action += '<a class=" btn btn-primary btn-sm btnEdit" title="Sửa"><i class="bx bx-pencil"></i></a>';
                        return action;
                    },
                    events: {
                        'click .btnEdit': function (e, value, row, index) {
                            $('#titleModal').text('Chỉnh sửa danh mục bài viết');
                            $('#ipHiddenId').val(row.id);
                            $('#ipTitle').val(row.title);
                            $('#ipCateName').val(row.cateName);
                            $('#ipShortDescription').val(row.shortDescription);
                            $('#modalsAddEdit').modal('show');
                        },
                    }
                },

            ],
            onLoadSuccess: function (data) {

            },
        })
    },
    tblCategoriesDetail: function () {
        $("#tblCategoryDetail").bootstrapTable('destroy');
        $("#tblCategoryDetail").bootstrapTable({
            method: 'get',
            url: "/Admin/Categories/GetDataCateDetail",
            queryParams: function (p) {
                var param = $.extend(true, {
                    cateId: $('#ipCateIdHidden').val(),
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
            sidePagination: 'client',
            pagination: true,
            paginationVAlign: 'bottom',
            search: false,
            pageSize: 10,
            pageList: [10, 50, 500],
            columns: [

                {
                    field: "name",
                    title: "Tên thể loại",
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
                    title: "Hình ảnh",
                    align: 'left',
                    valign: 'left',
                    formatter: function (value, row, index) {
                        if (value != null && value != '' && value != undefined) {
                            return '<image src ="/Uploads/Images/' + value + '" width="80" height="80"/>'
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
                            html += '<button class="btn btn-success btn-sm btnActive">Hiển thị</button>';
                        } else {
                            html += '<button class="btn btn-danger btn-sm btnActive">Không</button>';
                        }
                        return html;
                    },
                    events: {
                        'click .btnActive': function (e, value, row, index) {
                            $.confirm({
                                title: 'Cảnh báo!',
                                content: 'Bạn chắc chắn muốn thay đổi trạng thái hiển thị này?',
                                buttons: {
                                    formSubmit: {
                                        text: 'Xác nhận',
                                        btnClass: 'btn btn-primary',
                                        action: function () {
                                            $.ajax({
                                                url: '/Admin/Categories/ChangeIsActiveDetail',
                                                type: 'post',
                                                data: {
                                                    Id: row.id,
                                                },
                                                success: function (res) {
                                                    if (res.status) {
                                                        base.success(res.message);
                                                        $("#tblCategoryDetail").bootstrapTable('refresh', { silent: true });
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
                        action += '<a class=" btn btn-primary btn-sm btnEdit" title="Sửa"><i class="bx bx-pencil"></i></a>\
                        <a class=" btn btn-danger btn-sm btnDelete" title="Xóa"><i class="bx bx-trash"></i></a>';
                        return action;
                    },
                    events: {
                        'click .btnEdit': function (e, value, row, index) {
                            $('#titleModaldetail').text('Chỉnh sửa danh mục chi tiết');
                            $('#ipHiddenDetailId').val(row.id);
                            $('#ipCateNameDetail').val(row.name);
                            $('#ipTitleDetail').val(row.title);
                            $('#modalsAddEditDetail').modal('show');
                        },
                        'click .btnDelete': function (e, value, row, index) {
                            $.confirm({
                                title: 'Cảnh báo!',
                                content: 'Bạn chắc chắn muốn xóa danh mục chi tiết này?',
                                buttons: {
                                    formSubmit: {
                                        text: 'Xác nhận',
                                        btnClass: 'btn btn-primary',
                                        action: function () {
                                            $.ajax({
                                                url: '/Admin/Categories/DeleteCateDetail',
                                                type: 'post',
                                                data: {
                                                    Id: row.id,
                                                },
                                                success: function (res) {
                                                    if (res.status) {
                                                        base.success(res.message);
                                                        $("#tblCategoryDetail").bootstrapTable('refresh', { silent: true });
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

            ],
            onLoadSuccess: function (data) {

            },
        })
    },
}
$(document).ready(function () {
    categories.init();
});