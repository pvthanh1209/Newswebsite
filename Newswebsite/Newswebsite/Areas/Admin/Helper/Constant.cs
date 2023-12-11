using System.ComponentModel;

namespace Newswebsite.Areas.Admin.Helper
{
    public class Constant
    {
        public const string MESSAGE_UNAUTHORISED = "Yêu cầu trái phép <br> Bạn không có quyền truy cập tài nguyên được yêu cầu do hạn chế về bảo mật.<br>Để được truy cập, vui lòng liên hệ với quản trị viên hệ thống của bạn.";
        public const string OLD_PASS_FAILED = "Mật khẩu cũ không hợp lệ";
        public const string VERIFY_PASS_FAILED = "Mật khẩu nhắc lại không hợp lệ";
        public const string ALERT_SUCCESS = "{0} thành công.";
        public const string ALERT_FAIL = "{0} không thành công.";
        public const string ALERT_EXIST_DATA = "{0} đã tồn tại.";
        public const string NULLPASS = "Bạn chưa nhập mật khẩu cũ";
        public const string OLD_PASS_EQUAL_NEW_PASS = "Mật khẩu mới không được trùng với mật khẩu cũ";
    }

    public enum ActionModule
    {
        All = -1,
        Admin = 1,
        ServicesDetail = 2,
        Product = 3,
        Categories = 4,
        Brand = 5,
        Blog = 6,
        User = 7,
        Order = 8,
        Booking = 9,
        Doctor = 10,
        Banner = 11,
        RoleGroup = 12,

    }
    public enum ActionTypeCustom
    {
        [Description("All")]
        All = -1,
        [Description("Thêm mới")]
        Add = 1,
        [Description("Sửa")]
        Edit = 2,
        [Description("Xem")]
        View = 3,
        [Description("Xoá")]
        Delete = 4,
        [Description("Xác nhận")]
        Confirm = 5,
    }
}
