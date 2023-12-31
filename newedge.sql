USE [master]
GO
/****** Object:  Database [NewsEdge]    Script Date: 12/18/2023 8:31:59 PM ******/
CREATE DATABASE [NewsEdge]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'NewsEdge', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\NewsEdge.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'NewsEdge_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\NewsEdge_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [NewsEdge] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [NewsEdge].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [NewsEdge] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [NewsEdge] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [NewsEdge] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [NewsEdge] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [NewsEdge] SET ARITHABORT OFF 
GO
ALTER DATABASE [NewsEdge] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [NewsEdge] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [NewsEdge] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [NewsEdge] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [NewsEdge] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [NewsEdge] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [NewsEdge] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [NewsEdge] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [NewsEdge] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [NewsEdge] SET  DISABLE_BROKER 
GO
ALTER DATABASE [NewsEdge] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [NewsEdge] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [NewsEdge] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [NewsEdge] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [NewsEdge] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [NewsEdge] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [NewsEdge] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [NewsEdge] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [NewsEdge] SET  MULTI_USER 
GO
ALTER DATABASE [NewsEdge] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [NewsEdge] SET DB_CHAINING OFF 
GO
ALTER DATABASE [NewsEdge] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [NewsEdge] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [NewsEdge] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [NewsEdge] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [NewsEdge] SET QUERY_STORE = OFF
GO
USE [NewsEdge]
GO
/****** Object:  Table [dbo].[Accounts]    Script Date: 12/18/2023 8:31:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Accounts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](500) NULL,
	[Password] [nvarchar](500) NULL,
	[Email] [nvarchar](500) NULL,
	[PhoneNumber] [nchar](10) NULL,
	[Address] [nvarchar](max) NULL,
	[CreateDate] [datetime] NULL,
	[IsActive] [bit] NULL,
	[FullName] [nvarchar](500) NULL,
 CONSTRAINT [PK_Accounts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 12/18/2023 8:31:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CateName] [nvarchar](500) NULL,
	[Title] [nvarchar](500) NULL,
	[Thumb] [nvarchar](500) NULL,
	[IsActive] [bit] NULL,
	[ShortDescription] [nvarchar](max) NULL,
	[IsMenu] [bit] NULL,
	[CreatedDate] [datetime] NULL,
	[IsHome] [bit] NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CategoriesDetail]    Script Date: 12/18/2023 8:31:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CategoriesDetail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CateId] [int] NULL,
	[Name] [nvarchar](500) NULL,
	[Thumb] [nvarchar](500) NULL,
	[IsHome] [bit] NULL,
	[CreatedDate] [datetime] NULL,
	[Title] [nvarchar](500) NULL,
 CONSTRAINT [PK_CategoriesDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Comments]    Script Date: 12/18/2023 8:31:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NewId] [int] NULL,
	[UserId] [int] NULL,
	[ContentComment] [nvarchar](max) NULL,
	[CreatedDate] [datetime] NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_Comments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HistoryViewNews]    Script Date: 12/18/2023 8:31:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HistoryViewNews](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[NewsId] [int] NULL,
	[UserId] [int] NULL,
 CONSTRAINT [PK_HistoryViewNews] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[News]    Script Date: 12/18/2023 8:31:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[News](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CateId] [int] NULL,
	[CateDetaiId] [int] NULL,
	[Title] [nvarchar](500) NULL,
	[ShortDescription] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[Thumb] [nvarchar](500) NULL,
	[IsActive] [bit] NULL,
	[CreatedDate] [datetime] NULL,
	[AccountId] [int] NULL,
	[IsHome] [bit] NULL,
	[IsHot] [bit] NULL,
	[IsOutstanding] [bit] NULL,
 CONSTRAINT [PK_News] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 12/18/2023 8:31:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](500) NULL,
	[Password] [nvarchar](500) NULL,
	[Email] [nvarchar](500) NULL,
	[PhoneNumber] [nchar](10) NULL,
	[Address] [nvarchar](max) NULL,
	[IsActive] [bit] NULL,
	[FullName] [nvarchar](500) NULL,
	[Gender] [bit] NULL,
	[BirthDay] [datetime] NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Accounts] ON 

INSERT [dbo].[Accounts] ([Id], [Username], [Password], [Email], [PhoneNumber], [Address], [CreateDate], [IsActive], [FullName]) VALUES (1, N'admin', N'8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', N'admin@gmail.com', N'0997752127', N'Hà Nội', CAST(N'2023-12-12T00:00:00.000' AS DateTime), 1, N'admin')
INSERT [dbo].[Accounts] ([Id], [Username], [Password], [Email], [PhoneNumber], [Address], [CreateDate], [IsActive], [FullName]) VALUES (2, N'abc', N'8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', N'abc@demo.esoft', N'0997752127', N'30 Đặng Thùy Trâm', NULL, 1, N'thanhpv')
SET IDENTITY_INSERT [dbo].[Accounts] OFF
GO
SET IDENTITY_INSERT [dbo].[Categories] ON 

INSERT [dbo].[Categories] ([Id], [CateName], [Title], [Thumb], [IsActive], [ShortDescription], [IsMenu], [CreatedDate], [IsHome]) VALUES (1, N'Thể thao', N'Thể thao', N'231215\20231215_cdbb038a2e8fb80179913c6cebdbde26.jpg', 1, N'abcv', 1, CAST(N'2023-12-15T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Categories] ([Id], [CateName], [Title], [Thumb], [IsActive], [ShortDescription], [IsMenu], [CreatedDate], [IsHome]) VALUES (2, N'Bóng đá ', N'Thể thao - Bóng đá', NULL, 1, N'avc', 0, CAST(N'2023-12-15T00:00:00.000' AS DateTime), 0)
INSERT [dbo].[Categories] ([Id], [CateName], [Title], [Thumb], [IsActive], [ShortDescription], [IsMenu], [CreatedDate], [IsHome]) VALUES (3, N'Chính trị', N'Chính trị', N'231218\20231218_iconphone.png', 1, N'Chính trị', 1, CAST(N'2023-12-18T15:29:37.327' AS DateTime), 0)
SET IDENTITY_INSERT [dbo].[Categories] OFF
GO
SET IDENTITY_INSERT [dbo].[CategoriesDetail] ON 

INSERT [dbo].[CategoriesDetail] ([Id], [CateId], [Name], [Thumb], [IsHome], [CreatedDate], [Title]) VALUES (1, 1, N'Bóng đá', N'231217\20231217_11.jpg', 1, CAST(N'2023-12-17T00:00:00.000' AS DateTime), N'abx')
INSERT [dbo].[CategoriesDetail] ([Id], [CateId], [Name], [Thumb], [IsHome], [CreatedDate], [Title]) VALUES (2, 1, N'Điền kinh', N'231217\20231217_340-tay-da-chet-body-dove.jpg', 1, CAST(N'2023-12-17T00:00:00.000' AS DateTime), N'abc')
INSERT [dbo].[CategoriesDetail] ([Id], [CateId], [Name], [Thumb], [IsHome], [CreatedDate], [Title]) VALUES (4, 1, N'Cầu vợt', N'231217\20231217_chi_dan.jpg', 1, CAST(N'2023-12-17T14:52:04.600' AS DateTime), N'Cầu vợt ')
INSERT [dbo].[CategoriesDetail] ([Id], [CateId], [Name], [Thumb], [IsHome], [CreatedDate], [Title]) VALUES (5, 1, N'Cử tạ', N'231217\20231217_iconphone.png', 1, CAST(N'2023-12-17T14:52:55.587' AS DateTime), N'Cử tạ')
SET IDENTITY_INSERT [dbo].[CategoriesDetail] OFF
GO
SET IDENTITY_INSERT [dbo].[Comments] ON 

INSERT [dbo].[Comments] ([Id], [NewId], [UserId], [ContentComment], [CreatedDate], [IsActive]) VALUES (1, 2, 1, N'Đá này hơi faild rồi', CAST(N'2023-12-18T13:54:10.810' AS DateTime), 1)
INSERT [dbo].[Comments] ([Id], [NewId], [UserId], [ContentComment], [CreatedDate], [IsActive]) VALUES (3, 4, 1, N'Quá hay luôn', CAST(N'2023-12-18T15:17:03.417' AS DateTime), 1)
INSERT [dbo].[Comments] ([Id], [NewId], [UserId], [ContentComment], [CreatedDate], [IsActive]) VALUES (4, 6, 1, N'Lại căng thẳng rồi', CAST(N'2023-12-18T15:34:56.557' AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[Comments] OFF
GO
SET IDENTITY_INSERT [dbo].[HistoryViewNews] ON 

INSERT [dbo].[HistoryViewNews] ([Id], [NewsId], [UserId]) VALUES (1, 1, 1)
INSERT [dbo].[HistoryViewNews] ([Id], [NewsId], [UserId]) VALUES (2, 4, 1)
INSERT [dbo].[HistoryViewNews] ([Id], [NewsId], [UserId]) VALUES (3, 6, 1)
SET IDENTITY_INSERT [dbo].[HistoryViewNews] OFF
GO
SET IDENTITY_INSERT [dbo].[News] ON 

INSERT [dbo].[News] ([Id], [CateId], [CateDetaiId], [Title], [ShortDescription], [Description], [Thumb], [IsActive], [CreatedDate], [AccountId], [IsHome], [IsHot], [IsOutstanding]) VALUES (1, 1, 1, N'Điểm tựa Anfield, Liverpool chờ “hủy diệt” MU nhờ Salah & hàng công thăng hoa', N'Về mọi mặt, Liverpool là đội bóng được đánh giá cao hơn trước “màn thư hùng” gặp MU (23h30, 17/12). Thầy trò HLV Jurgen Klopp đang nhận được sự ủng hộ tuyệt đối từ giới mộ điệu với phong độ cao cùng chất lượng cầu thủ đang sở hữu trong tay.', N'<p>Nhưng n&oacute;i g&igrave; th&igrave; n&oacute;i, trước trận đấu v&agrave;o đ&ecirc;m nay, Liverpool l&agrave; những người nằm ở thế cửa tr&ecirc;n so với MU về mọi kh&iacute;a cạnh. &ldquo;The Kop&rdquo; c&oacute; phong độ tốt hơn từ đầu m&ugrave;a, đang dẫn đầu bảng xếp hạng của Ngoại Hạng Anh. Điều đ&oacute; ho&agrave;n to&agrave;n tr&aacute;i ngược với bộ mặt bạc nhược, thiếu sức sống được MU thể hiện xuy&ecirc;n suốt những trận đấu đ&atilde; qua.</p>

<p>Một điều nữa để Liverpool c&oacute; thể tự tin v&agrave;o 3 điểm trọn vẹn đ&oacute; l&agrave; việc họ lu&ocirc;n sở hữu một th&agrave;nh t&iacute;ch cực tốt trong những lần đối đầu trực tiếp với &ldquo;nửa đỏ th&agrave;nh Manchester&rdquo;. Đo&agrave;n qu&acirc;n Klopp tỏ ra &aacute;p đảo cả về mặt kết quả cũng như thế trận tr&ecirc;n s&acirc;n trước đội b&oacute;ng đ&atilde; v&ocirc; địch nước Anh&nbsp;20 lần trong lịch sử.</p>
', N'231217\20231217_live-2-1702779646-810-width740height495.jpg', 1, CAST(N'2023-12-17T13:46:55.807' AS DateTime), 1, 1, 1, 1)
INSERT [dbo].[News] ([Id], [CateId], [CateDetaiId], [Title], [ShortDescription], [Description], [Thumb], [IsActive], [CreatedDate], [AccountId], [IsHome], [IsHot], [IsOutstanding]) VALUES (2, 1, 1, N'Nhận định trận HOT hôm nay: Arsenal dốc sức vì ngôi đầu, Real quyết nối dài mạch bất bại', N'Hai ông lớn Arsenal và Real Madrid thi đấu ở 2 giải đấu khác nhau, nhưng có điểm chung là đang đạt phong độ cao cũng như chiếm lợi thế lớn.', N'<p>Bởi thế, Arsenal c&oacute; đội h&igrave;nh tốt nhất cho cuộc đấu với Brighton. Ngược lại, đội kh&aacute;ch vừa phải dốc to&agrave;n lực đ&aacute;nh bại Marseille 1-0 để gi&agrave;nh ng&ocirc;i đầu bảng tại đấu trường Europa League hồi giữa tuần. V&igrave; thế, nh&acirc;n sự v&agrave; thể lực của &ldquo;Chim m&ograve;ng biển&rdquo; đều thua thiệt so với đội chủ nh&agrave;.</p>

<p>X&eacute;t về th&agrave;nh t&iacute;ch đối đầu, Brighton lại duy tr&igrave; được sự lấn lướt đ&aacute;ng khen trong c&aacute;c cuộc đấu với Arsenal. Rất ngạc nhi&ecirc;n l&agrave; trong 5 lần đụng độ gần nhất giữa hai đội, &ldquo;Ph&aacute;o thủ&rdquo; chỉ thắng được đ&uacute;ng 1 trận. Ngược lại, Brighton đ&atilde; thắng đến 3 trong 5 trận ấy.</p>

<p>V&agrave; điều đ&aacute;ng n&oacute;i l&agrave; cả 3 lần gần nhất l&agrave;m kh&aacute;ch ở Emirates bao gồm 2 trận ở Ngoại hạng Anh v&agrave; 1 trận tại League Cup, đội b&oacute;ng đang được dẫn dắt bởi HLV Roberto De Zerbi đều thắng trận. Thế n&ecirc;n, sự thận trọng của HLV Arteta c&oacute; cơ sở.</p>
', N'231217\20231217_495-1702730004-196-width740height495.jpg', 1, CAST(N'2023-12-17T15:55:30.120' AS DateTime), 1, 1, 1, 1)
INSERT [dbo].[News] ([Id], [CateId], [CateDetaiId], [Title], [ShortDescription], [Description], [Thumb], [IsActive], [CreatedDate], [AccountId], [IsHome], [IsHot], [IsOutstanding]) VALUES (3, 1, 1, N' Nóng bỏng bảng xếp hạng Ngoại hạng Anh: Man City bất ngờ hụt hơi, MU lại tụt hạng ', N'Man City bất ngờ hụt hơi trong cuộc đua vô địch trong khi MU lại tụt thứ hạng dù chưa thi đấu.', N'<p>Đầu ti&ecirc;n cần phải nhắc tới trận h&ograve;a 2-2 của Man City trước Crystal Palace. HLV Pep Guardiola tiếp tục cất Erling Haaland nhưng &quot;The Citizen&quot; vẫn kiểm so&aacute;t ho&agrave;n to&agrave;n thế trận. Họ vượt l&ecirc;n dẫn trước 2-0 từ kh&aacute; sớm. Tuy nhi&ecirc;n, Man City bất ngờ đ&aacute;nh rơi dần lợi thế. Đầu ti&ecirc;n l&agrave; b&agrave;n thắng của Mateta sau đ&oacute; l&agrave; quả phạt đền ở ph&uacute;t 90+5. Những ph&uacute;t c&ograve;n lại l&agrave; qu&aacute; &iacute;t ỏi để Man City t&igrave;m được chiến thắng.</p>

<p>Trận h&ograve;a n&agrave;y kh&aacute; tai hại với thầy tr&ograve; Pep Guardiola. Sau v&ograve;ng 17, họ mới chỉ c&oacute; được 34 điểm v&agrave; tạm thời xếp thứ 4. Man City chỉ c&ograve;n hơn đội xếp liền sau l&agrave; Tottenham đ&uacute;ng 1 điểm. Ba đội xếp tr&ecirc;n l&agrave; Liverpool, Arsenal v&agrave; Aston Villa đều chưa thi đấu n&ecirc;n khả năng &quot;The Citizens&quot; bị bỏ lại ph&iacute;a sau l&agrave; rất cao. Thầy tr&ograve; Pep Guardiola sẽ ho&atilde;n đ&aacute; v&ograve;ng 18 do tham dự FIFA Club World Cup n&ecirc;n khoảng c&aacute;ch c&oacute; thể c&ograve;n gia tăng hơn nữa.</p>

<p>Một đại gia kh&aacute;c trong Big 6 của Ngoại hạng Anh l&agrave;&nbsp;<a href="https://www.24h.com.vn/chelsea-c48e1505.html" title="Chelsea">Chelsea</a>&nbsp;đ&atilde; t&igrave;m lại được t&igrave;m niềm vui. Sau khi thất bại trong việc t&igrave;m b&agrave;n thắng ở hiệp một, &quot;The Blues&quot; đ&atilde; c&oacute; 2 b&agrave;n thắng trong hiệp hai để vượt qua Sheffield United. Ba điểm qu&yacute; gi&aacute; n&agrave;y gi&uacute;p thầy tr&ograve; HLV Pochettino tạm thời vươn l&ecirc;n vị tr&iacute; thứ 10 tr&ecirc;n bảng xếp hạng.&nbsp;</p>
', N'231217\20231217_3-495-1702756596-977-width740height495.jpg', 1, CAST(N'2023-12-17T15:57:35.667' AS DateTime), 1, 1, 1, 0)
INSERT [dbo].[News] ([Id], [CateId], [CateDetaiId], [Title], [ShortDescription], [Description], [Thumb], [IsActive], [CreatedDate], [AccountId], [IsHome], [IsHot], [IsOutstanding]) VALUES (4, 1, 1, N'Việt Nam ở bảng “siêu tử thần”', N'thần” gồm Nhật Bản, Triều Tiên và Trung Quốc.', N'<p>Kết quả bốc thăm v&ograve;ng chung kết U-20 nữ ch&acirc;u &Aacute;, c&aacute;c c&ocirc; g&aacute;i trẻ Việt Nam (VN) rơi v&agrave;o bảng B &ldquo;si&ecirc;u tử thần&rdquo; gồm Nhật Bản, Triều Ti&ecirc;n v&agrave; Trung Quốc. Trong khi đ&oacute;, bảng A c&oacute; chủ nh&agrave; Uzbekistan, H&agrave;n Quốc, &Uacute;c, Đ&agrave;i Loan. Giải đấu diễn ra từ ng&agrave;y 3 đến 16-3-2024 v&agrave; bốn đội v&agrave;o b&aacute;n kết c&oacute; suất đi World Cup U-20 nữ c&ugrave;ng năm tại Colombia.</p>

<p>Thầy tr&ograve; HLV Akira Ijiri sẽ gặp nhiều gian nan v&igrave; phải đụng độ ba đối thủ mạnh nhất l&agrave;ng&nbsp;<a href="https://www.24h.com.vn/doi-tuyen-nu-viet-nam-c1063.html" title="bóng đá nữ">b&oacute;ng đ&aacute; nữ</a>&nbsp;ch&acirc;u &Aacute; nổi tiếng với c&aacute;c tuyến nữ trẻ ấn tượng. Muốn lấy v&eacute; chơi World Cup nữ U-20, c&aacute;c c&ocirc; g&aacute;i trẻ VN phải &iacute;t nhất đứng nh&igrave; bảng v&agrave; điều đ&oacute; kh&oacute; như h&aacute;i sao tr&ecirc;n trời.</p>
', N'231218\20231218_anh_bong_da.jpg', 1, CAST(N'2023-12-18T15:15:39.997' AS DateTime), 1, 1, 1, 1)
INSERT [dbo].[News] ([Id], [CateId], [CateDetaiId], [Title], [ShortDescription], [Description], [Thumb], [IsActive], [CreatedDate], [AccountId], [IsHome], [IsHot], [IsOutstanding]) VALUES (5, 1, 1, N'Tin mới nhất bóng đá trưa 18/12: Chấn thương nghiêm trọng, David Alaba nghỉ hết mùa', N'Theo kết quả kiểm tra sau trận thắng của Real, David Alaba bị rách dây chằng chéo và phải nghỉ hết mùa.', N'<p>Cuối hiệp 1, trong nỗ lực tranh cướp b&oacute;ng với cầu thủ đối phương, đầu gối tr&aacute;i của David Alaba đ&atilde; khuỵu xuống. Ngay lập tức, tuyển thủ &Aacute;o nằm s&acirc;n tỏ ra cực k&igrave; đau đớn v&agrave; ra hiệu cầu cứu đội ngũ y tế của Real Madrid.</p>

<p>Theo kết quả kiểm tra sau trận, David Alaba bị r&aacute;ch d&acirc;y chằng ch&eacute;o. Tr&ecirc;n trang chủ, Real x&aacute;c nhận tuyển thủ &Aacute;o sẽ phải phẫu thuật trong những ng&agrave;y tới. Chấn thương n&agrave;y khiến Alaba coi như nghỉ hết m&ugrave;a 2023/24.</p>

<p><strong>Đội b&oacute;ng Ph&aacute;p t&iacute;nh giải cứu Fabio Vieira</strong></p>

<p>Theo SunSport, Marseille đ&atilde; li&ecirc;n hệ&nbsp;<a href="https://www.24h.com.vn/arsenal-fc-c48e3424.html" title="Arsenal">Arsenal</a>&nbsp;với mong muốn mượn tiền vệ Fabio Vieira trong th&aacute;ng 1 tới. Mối quan hệ giữa Marseille v&agrave; Arsenal trong nhiều năm qua đ&atilde; được củng cố với nhiều thương vụ như Guendouzi, Saliba hay Tavares. Tuy vậy, bản th&acirc;n cầu thủ người Bồ Đ&agrave;o Nha lại đang bị m&ocirc;ng lung giữa ra đi v&agrave; ở lại. Vieira cần được ra s&acirc;n thi đấu đẻ c&oacute; suất tham dự v&ograve;ng chung kết EURO 2024 c&ugrave;ng ĐT Bồ Đ&agrave;o Nha. Tuy nhi&ecirc;n, cầu thủ n&agrave;y cũng muốn ở lại Arsenal khi m&agrave; đội b&oacute;ng đang c&oacute; cơ hội đua v&ocirc; địch Ngoại hạng Anh.</p>

<p><strong>Barca sắm trung vệ đua v&ocirc; địch La Liga</strong></p>

<p>Theo tờ Sport, Barca đ&atilde; x&aacute;c định được mục ti&ecirc;u cho vị tr&iacute; trung vệ, đ&oacute; l&agrave; Radu Dragusin, người được mệnh danh l&agrave; &ldquo;Ronald Araujo mới&rdquo;. Dragusin cũng mang quốc tịch Uruguay, năm nay 21 tuổi v&agrave; hiện đang thuộc bi&ecirc;n chế CLB Genoa của Italia. Theo một số nguồn tin uy t&iacute;n, mức gi&aacute; giải ph&oacute;ng hợp đồng của trung vệ Dragusin l&agrave; 25 triệu bảng. &ldquo;Blaugrana&rdquo; sẽ phải cạnh tranh với nhiều CLB kh&aacute;c trong thương vụ n&agrave;y, bao gồm Bayern Munich.</p>

<p><strong>Arsenal s&aacute;ng cửa chi&ecirc;u mộ Ivan Toney</strong></p>

<p>Theo Fabrizio Romano, tiền đạo Ivan Toney của Brentford hứng th&uacute; với viễn cảnh đến Arsenal. Ch&acirc;n s&uacute;t người Anh sẽ kết th&uacute;c &aacute;n phạt cấm thi đấu 8 th&aacute;ng v&agrave;o giữa th&aacute;ng 1 năm sau. &ldquo;Bầy ong&rdquo; hiện định gi&aacute; Toney từ 60 đến 80 bảng, t&ugrave;y thuộc v&agrave;o thời điểm 1 CLB bất k&igrave; hỏi mua. &ldquo;Ph&aacute;o thủ&rdquo; từ l&acirc;u đ&atilde; rất muốn mua Ivan Toney v&agrave; đang cạnh tranh quyết liệt với Chelsea trong thương vụ n&agrave;y.</p>

<p><strong>Nhiều đại gia Anh nhắm em trai Jude Bellingham</strong></p>
', N'231218\20231218_495-1702871211-589-width740height495.jpg', 1, CAST(N'2023-12-18T15:31:05.433' AS DateTime), 1, 1, 0, 1)
INSERT [dbo].[News] ([Id], [CateId], [CateDetaiId], [Title], [ShortDescription], [Description], [Thumb], [IsActive], [CreatedDate], [AccountId], [IsHome], [IsHot], [IsOutstanding]) VALUES (6, 3, 0, N'Căng thẳng Nga - Ukraine ngày 17/12: Ukraine tiết lộ cách Nga đánh lừa phòng không của Kiev', N'Ông Yurii Ihnat - phát ngôn viên Không quân Ukraine cho rằng, Nga đang quan sát phòng không của Ukraine và sử dụng mọi năng lực kỹ thuật để đánh lừa hệ thống của Kiev.', N'<p>Theo chia sẻ của &ocirc;ng Yurii Ihnat - ph&aacute;t ng&ocirc;n vi&ecirc;n Kh&ocirc;ng qu&acirc;n Ukraine, trong vụ tấn c&ocirc;ng h&ocirc;m 14/12, Nga đ&atilde; &aacute;p dụng chiến thuật mới nhằm đ&aacute;nh lừa ph&ograve;ng kh&ocirc;ng Ukraine, với c&ocirc;ng nghệ t&aacute;c chiến điện tử vượt trội.</p>

<p>Cụ thể, t&aacute;c chiến điện tử của Nga đ&atilde; ph&aacute;t ra c&aacute;c xung điện từ nhằm khiến c&aacute;c radar cảnh giới của Ukraine tưởng nhầm c&aacute;c vũ kh&iacute; &quot;mồi&quot; l&agrave; t&ecirc;n lửa si&ecirc;u vượt &acirc;m Kinzhal.</p>

<p>&Ocirc;ng Yurii Ihnat tiết lộ, khi một vũ kh&iacute; của Nga xuất hiện tr&ecirc;n kh&ocirc;ng phận v&agrave; bị radar ph&aacute;t hiện, n&oacute; c&oacute; thể l&agrave; mồi nhử v&agrave; Kiev kh&ocirc;ng phải l&uacute;c n&agrave;o cũng ph&acirc;n biệt được giữa mục ti&ecirc;u thật v&agrave; giả.</p>

<p>&quot;Tốc độ của vũ kh&iacute; Nga trung b&igrave;nh l&agrave; hơn 7.000km/h v&igrave; vậy ch&uacute;ng t&ocirc;i phải th&ocirc;ng b&aacute;o ngay lập tức cho người d&acirc;n về mối đe dọa trước khi x&aacute;c định l&agrave; quả t&ecirc;n lửa l&agrave; thật hay giả.</p>

<p>R&otilde; r&agrave;ng, đối phương đang quan s&aacute;t ph&ograve;ng kh&ocirc;ng của Ukraine v&agrave; sử dụng mọi năng lực kỹ thuật để đ&aacute;nh lừa hệ thống của ch&uacute;ng t&ocirc;i&rdquo;, ph&aacute;t ng&ocirc;n vi&ecirc;n Kh&ocirc;ng qu&acirc;n Ukraine nhận định.</p>

<p>&Ocirc;ng Yurii Ihnat cũng cho rằng, Nga gần đ&acirc;y sử dụng lượng lớn m&aacute;y bay kh&ocirc;ng người l&aacute;i (UAV) tự s&aacute;t kiểu Shahed-136/131 để tập k&iacute;ch Ukraine v&agrave; năng lực của thiết bị n&agrave;y ng&agrave;y c&agrave;ng được cải thiện. Tuy nhi&ecirc;n, số t&ecirc;n lửa h&agrave;nh tr&igrave;nh được triển khai lại giảm xuống v&igrave; Moscow chưa sẵn s&agrave;ng ti&ecirc;u hao lượng lớn vũ kh&iacute; đắt tiền.</p>

<p>Ng&agrave;y 14/12, ph&aacute;t ng&ocirc;n vi&ecirc;n Kh&ocirc;ng qu&acirc;n Ukraine cho hay, một t&ecirc;n lửa Kinzhal được ph&oacute;ng về ph&iacute;a th&agrave;nh phố Starokostiantyniv thuộc tỉnh Khmelnitsky, nơi c&oacute; s&acirc;n bay qu&acirc;n sự trọng yếu của Ukraine.&nbsp;</p>

<p>&quot;T&ecirc;n lửa lao xuống khoảng 10 ph&uacute;t sau khi b&aacute;o động ph&ograve;ng kh&ocirc;ng được ph&aacute;t ra. Ch&uacute;ng t&ocirc;i sẽ kh&ocirc;ng tiết lộ hậu quả của đ&ograve;n đ&aacute;nh. H&atilde;y để đối phương tự r&uacute;t ra kết luận&quot;, &ocirc;ng&nbsp;Yurii Ihnat n&oacute;i.</p>

<p>Cơ quan qu&acirc;n sự thủ đ&ocirc; Kiev sau đ&oacute; cảnh b&aacute;o ti&ecirc;m k&iacute;ch MiG-31K Nga đ&atilde; ph&oacute;ng t&ecirc;n lửa Kinzhal về ph&iacute;a th&agrave;nh phố. Truyền th&ocirc;ng Ukraine n&oacute;i rằng &iacute;t nhất ba tiếng nổ lớn vang l&ecirc;n gần s&acirc;n bay quốc tế Zhulyany ở Kiev, nơi triển khai trận địa t&ecirc;n lửa ph&ograve;ng kh&ocirc;ng Patriot.</p>

<p>Theo th&ocirc;ng tin từ &ocirc;ng Yurii Ihnat, những tiếng nổ l&agrave; do hệ thống ph&ograve;ng kh&ocirc;ng tham chiến, khẳng định qu&acirc;n đội Ukraine đ&atilde; bắn rơi một quả đạn của Nga. Được biết, cuộc tấn c&ocirc;ng h&ocirc;m 14/12 l&agrave;&nbsp;lần đầu ti&ecirc;n Nga sử dụng t&ecirc;n lửa Kinzhal để tập k&iacute;ch Ukraine sau nhiều th&aacute;ng chỉ khai hỏa t&ecirc;n lửa h&agrave;nh tr&igrave;nh v&agrave; UAV tự s&aacute;t.</p>

<p>Nga hiện chưa b&igrave;nh luận về c&aacute;c th&ocirc;ng tin do Ukraine cung cấp nhưng trước đ&oacute; Moscow nhiều lần tuy&ecirc;n bố t&ecirc;n lửa Kinzhal, với tầm bắn hơn 1.000km v&agrave; tốc độ Mach 9 (11.113km/h), l&agrave; vũ kh&iacute; &quot;bất khả chiến bại&quot; với bất cứ hệ thống ph&ograve;ng kh&ocirc;ng n&agrave;o của đối phương v&agrave;o l&uacute;c n&agrave;y.</p>

<p>Sự nguy hiểm của t&ecirc;n lửa Kinzhal nằm ở sự ch&iacute;nh x&aacute;c, tầm bắn v&agrave; tốc độ si&ecirc;u vượt &acirc;m của n&oacute;. Kinzhal sở hữu hệ thống dẫn đường đặc biệt, gi&uacute;p vũ kh&iacute; n&agrave;y c&oacute; thể thay đổi quỹ đạo để n&eacute; ph&ograve;ng kh&ocirc;ng của đối phương ở mọi giai đoạn khi bay v&agrave; khiến việc bắn hạ rất kh&oacute; khăn.</p>

<p>Hồi th&aacute;ng 5/2023, Ukraine từng tuy&ecirc;n bố đ&aacute;nh chặn th&agrave;nh c&ocirc;ng Kinzhal của Nga bằng hệ thống Patriot do Mỹ viện trợ. Tuy nhi&ecirc;n, ph&iacute;a Nga b&aacute;c bỏ th&ocirc;ng tin n&agrave;y, cho rằng về mặt kỹ thuật Patriot bắn rơi được Kinzhal l&agrave; kh&ocirc;ng thể xảy ra.</p>

<p>Cụ thể, một quan chức Nga n&oacute;i rằng vận tốc của Kinzhal vượt qu&aacute; chế độ đ&aacute;nh chặn tối đa của Patriot. Vũ kh&iacute; n&agrave;y c&oacute; khả năng tr&aacute;nh t&ecirc;n lửa giai đoạn bay cuối c&ugrave;ng v&agrave; tấn c&ocirc;ng mục ti&ecirc;u theo phương thẳng đứng khiến c&aacute;c hệ thống đất đối kh&ocirc;ng hiện tại kh&ocirc;ng thể bắn rơi.</p>

<p>Cũng v&agrave;o thời điểm n&oacute;i tr&ecirc;n, Nga tuy&ecirc;n bố một t&ecirc;n lửa si&ecirc;u vượt &acirc;m Kinzhal đ&atilde; ph&aacute; hủy tổ hợp ph&ograve;ng kh&ocirc;ng Patriot của qu&acirc;n đội Ukraine.&nbsp;Sau đ&oacute;, Mỹ x&aacute;c nhận Patriot đ&atilde; bị hư hại sau cuộc tấn c&ocirc;ng của Nga nhưng hệ thống đ&atilde; được sửa chữa để đưa trở lại t&aacute;c chiến.</p>

<p>Trong diễn biến li&ecirc;n quan, Bộ trưởng Quốc ph&ograve;ng Nga Sergei Shoigu hồi th&aacute;ng 5/2023 n&oacute;i rằng Ukraine thường xuy&ecirc;n ph&oacute;ng đại qu&aacute; mức số lượng t&ecirc;n lửa Kinzhal được sử dụng.</p>

<p>&quot;Đối phương thường xuy&ecirc;n nhầm lẫn về chủng loại t&ecirc;n lửa Nga triển khai. Đ&oacute; l&agrave; l&yacute; do họ kh&ocirc;ng chặn được mục ti&ecirc;u&quot;, Bộ trưởng Quốc ph&ograve;ng Nga cho biết.</p>
', N'231218\20231218_1702808963-737-thumbnail-width740height495_anh_cat_3_2.jpg', 1, CAST(N'2023-12-18T15:33:11.227' AS DateTime), 1, 0, 0, 0)
SET IDENTITY_INSERT [dbo].[News] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([Id], [Username], [Password], [Email], [PhoneNumber], [Address], [IsActive], [FullName], [Gender], [BirthDay]) VALUES (1, N'thanhpham', N'8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', N'thanh123@gmail.com', N'0997752127', NULL, 1, N'ThanhPham', 0, CAST(N'2000-12-18T00:00:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
ALTER TABLE [dbo].[Categories] ADD  CONSTRAINT [DF_Categories_IsHome]  DEFAULT ((0)) FOR [IsHome]
GO
ALTER TABLE [dbo].[CategoriesDetail] ADD  CONSTRAINT [DF_CategoriesDetail_IsHome]  DEFAULT ((0)) FOR [IsHome]
GO
ALTER TABLE [dbo].[News] ADD  CONSTRAINT [DF_News_CateDetaiId]  DEFAULT ((0)) FOR [CateDetaiId]
GO
ALTER TABLE [dbo].[News] ADD  CONSTRAINT [DF_News_IsOutstanding]  DEFAULT ((0)) FOR [IsOutstanding]
GO
/****** Object:  StoredProcedure [dbo].[Sp_GetAccountListAll]    Script Date: 12/18/2023 8:32:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create PROCEDURE [dbo].[Sp_GetAccountListAll]
	@search nvarchar(500),
	@offset int,
	@limit int
AS
BEGIN
	SELECT a.*
	, COUNT(1) OVER() as TotalRow
	FROM Accounts a
	Where (@search is null 
	OR a.FullName like N'%'+@search+'%'
	OR a.Username like N'%'+@search+'%')
	Order By a.Id DESC
	offset @offset rows
	fetch next @limit rows only;

END
GO
/****** Object:  StoredProcedure [dbo].[Sp_GetCategoryByTree_LisAllPaging]    Script Date: 12/18/2023 8:32:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Sp_GetCategoryByTree_LisAllPaging]
	@search nvarchar(500),
	@offset int,
	@limit int
AS
BEGIN
	Select *,
	COUNT(1) OVER() as TotalRow
	FROM Categories
	Where (@search is null 
	OR Title like N'%'+@search+'%'
	OR CateName like N'%'+@search+'%')
	Order By Id DESC
	offset @offset rows
	fetch next @limit rows only
END
GO
/****** Object:  StoredProcedure [dbo].[Sp_GetComment_ByNewsId]    Script Date: 12/18/2023 8:32:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Sp_GetComment_ByNewsId]
	@newsId int
AS
BEGIN
	Select c.*, u.Username
	From Comments c
	INNER JOIN Users u ON u.Id = c.UserId
	Where c.[NewId] = @newsId
	AND c.IsActive = 1
END
GO
/****** Object:  StoredProcedure [dbo].[Sp_GetCommentListAllPaging]    Script Date: 12/18/2023 8:32:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Sp_GetCommentListAllPaging]
	@search nvarchar(500),
	@startDate nvarchar(50),
	@endDate nvarchar(50),
	@offset int,
	@limit int
AS
BEGIN
	Select c.*, ca.CateName, u.Username
	, n.Title
	, COUNT(1) OVER() as TotalRow
	FROM Comments c
	INNER JOIN News n ON n.Id = c.[NewId]
	INNER JOIN Categories ca ON ca.Id = n.CateId
	INNER JOIN Users u ON u.Id = c.UserId
	Where (@search is null
	OR u.Username like N'%'+@search+'%'
	OR n.Title like N'%'+@search+'%'
	OR ca.CateName like N'%'+@search+'%')
	AND (
		(@startDate is null OR (CONVERT(date, c.CreatedDate) >= CONVERT(date, @startDate)))
		AND (@endDate is null OR (CONVERT(date, c.CreatedDate) <= CONVERT(date, @endDate)))
	)
	Order BY c.Id DESC
	offset @offset rows
	fetch next @limit rows only
END
GO
/****** Object:  StoredProcedure [dbo].[Sp_GetHistoryViewNewsByUserId]    Script Date: 12/18/2023 8:32:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Sp_GetHistoryViewNewsByUserId]
	@userId int
AS
BEGIN
	SELECT n.*, c.CateName
	, cd.Name as CateNameDetail
	, a.FullName
	FROM HistoryViewNews h
	INNER JOIN News n ON n.Id = h.NewsId
	INNER JOIN Categories c ON c.Id = n.CateId
	INNER JOIN Accounts a ON a.Id = n.AccountId
	LEFT JOIN CategoriesDetail cd ON cd.Id = n.CateDetaiId
	Where n.IsActive = 1 AND h.UserId = @userId
END
GO
/****** Object:  StoredProcedure [dbo].[Sp_GetNewById]    Script Date: 12/18/2023 8:32:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Sp_GetNewById]
	@Id int
AS
BEGIN
	Select n.*
	, c.CateName
	, a.FullName
	, cd.Name as CateNameDetail
	FROM News n
	LEFT JOIN Categories c ON c.Id = n.CateId
	LEFT JOIN Accounts a ON a.Id = n.AccountId
	LEFT JOIN CategoriesDetail cd ON cd.Id = n.CateDetaiId 
	Where n.IsActive = 1 AND (n.Id = @Id)
END
GO
/****** Object:  StoredProcedure [dbo].[Sp_GetNewIsActiveAll]    Script Date: 12/18/2023 8:32:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Sp_GetNewIsActiveAll]

AS
BEGIN
	SELECT n.*
	, c.CateName
	, a.FullName
	, cd.Name as CateNameDetail
	FROM News n
	LEFT JOIN Categories c ON c.Id = n.CateId
	LEFT JOIN Accounts a ON a.Id = n.AccountId
	LEFT JOIN CategoriesDetail cd ON cd.Id = n.CateDetaiId 
	Where n.IsActive = 1
	ORDER BY n.CreatedDate DESC
END
GO
/****** Object:  StoredProcedure [dbo].[Sp_GetNewIsHomeAndIsHot]    Script Date: 12/18/2023 8:32:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Sp_GetNewIsHomeAndIsHot] 
	
AS
BEGIN
	SELECT Top 10 n.*
	, c.CateName
	, a.FullName
	FROM News n
	LEFT JOIN Categories c ON c.Id = n.CateId
	LEFT JOIN Accounts a ON a.Id = n.AccountId
	Where n.IsActive = 1
	AND n.IsHome = 1 AND n.IsOutstanding = 1
	ORDER BY n.CreatedDate DESC
END
GO
/****** Object:  StoredProcedure [dbo].[Sp_GetNewIsHotIsHome]    Script Date: 12/18/2023 8:32:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Sp_GetNewIsHotIsHome]

AS
BEGIN
	SELECT Top 10 n.*
	, c.CateName
	, a.FullName
	FROM News n
	LEFT JOIN Categories c ON c.Id = n.CateId
	LEFT JOIN Accounts a ON a.Id = n.AccountId
	Where n.IsActive = 1
	AND n.IsHome = 1 AND n.IsHot = 1
	ORDER BY n.CreatedDate DESC
END
GO
/****** Object:  StoredProcedure [dbo].[Sp_GetNewListAllPaging]    Script Date: 12/18/2023 8:32:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Sp_GetNewListAllPaging] 
	@cateId int,
	@search nvarchar(500),
	@offset int,
	@limit int
AS
BEGIN
	SELECT ns.*, a.FullName, c.CateName, COUNT(1) OVER() as TotalRow
	FROM News ns
	LEFT JOIN Categories c ON c.Id = ns.CateId
	LEFT JOIN Accounts a ON a.Id = ns.AccountId
	Where (@cateId = 0 OR ns.CateId = @cateId)
	AND (@search is null 
	OR ns.Title like N'%'+@search+'%'
	OR ns.ShortDescription like N'%'+@search+'%')
	Order by ns.Id DESC
	offset @offset rows 
	fetch next @limit rows only;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetNewsSearch]    Script Date: 12/18/2023 8:32:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_GetNewsSearch] 
	@search nvarchar(500),
	@offset int,
	@limit int
AS
BEGIN
	SELECT n.*
	, c.CateName
	, a.FullName
	, cd.Name as CateNameDetail
	, COUNT(1) OVER() as TotalRow
	FROM News n
	LEFT JOIN Categories c ON c.Id = n.CateId
	LEFT JOIN Accounts a ON a.Id = n.AccountId
	LEFT JOIN CategoriesDetail cd ON cd.Id = n.CateDetaiId 
	Where n.IsActive = 1 
	AND (n.Title like N'%'+@search+'%'
	OR c.CateName like N'%'+@search+'%'
	OR cd.[Name] like N'%'+@search+'%'
	OR a.FullName like N'%'+@search+'%')
	ORDER BY n.CreatedDate DESC
	offset @offset rows
	fetch next @limit rows only
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetUser_ListAllPaging]    Script Date: 12/18/2023 8:32:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetUser_ListAllPaging]
	@search nvarchar(500),
	@offset int,
	@limit int
AS
BEGIN
	SELECT *
	, COUNT(1) OVER() AS TotalRow
	FROM Users 
	Where (@search is null
	OR [Username] like N'%'+@search+'%'
	OR Email like N'%'+@search+'%'
	OR PhoneNumber like N'%'+@search+'%')
	OR FullName like N'%'+@search+'%'
	Order By Id DESC
	offset @offset rows
	fetch next @limit rows only;
END
GO
USE [master]
GO
ALTER DATABASE [NewsEdge] SET  READ_WRITE 
GO
