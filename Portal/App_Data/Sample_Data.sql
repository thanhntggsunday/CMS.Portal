USE [Learning]
Go
Delete From  Role_Permission;
Delete From [Function_Action];
Delete From [Functions];
Delete From  [AppRoles];
Delete From  [Action];
Delete From [AppUsers];

Go

INSERT [dbo].[Functions] ([ID], [Name], [URL], [DisplayOrder], [ParentId], [Status], [IconCss]) VALUES (N'ACCESS', N'Báo cáo truy cập', N'/main/report/visitor', 2, N'REPORT', 1, N'fa-bar-chart-o')
INSERT [dbo].[Functions] ([ID], [Name], [URL], [DisplayOrder], [ParentId], [Status], [IconCss]) VALUES (N'ACTIVITY', N'Nhật ký', N'/main/activity/index', 4, N'SYSTEM', 1, N'fa-home')
INSERT [dbo].[Functions] ([ID], [Name], [URL], [DisplayOrder], [ParentId], [Status], [IconCss]) VALUES (N'ANNOUNCEMENT', N'Thông báo', N'/main/announcement/index', 3, N'UTILITY', 1, N'fa-clone')
INSERT [dbo].[Functions] ([ID], [Name], [URL], [DisplayOrder], [ParentId], [Status], [IconCss]) VALUES (N'CONTACT', N'Lien hệ', N'/main/contact/index', 4, N'UTILITY', 1, N'fa-clone')
INSERT [dbo].[Functions] ([ID], [Name], [URL], [DisplayOrder], [ParentId], [Status], [IconCss]) VALUES (N'CONTENT', N'Nội dung', N'/', 3, NULL, 1, N'fa-table')
INSERT [dbo].[Functions] ([ID], [Name], [URL], [DisplayOrder], [ParentId], [Status], [IconCss]) VALUES (N'ERROR', N'Lỗi', N'/main/error/index', 5, N'SYSTEM', 1, N'fa-home')
INSERT [dbo].[Functions] ([ID], [Name], [URL], [DisplayOrder], [ParentId], [Status], [IconCss]) VALUES (N'FEEDBACK', N'Phản hồi', N'/main/feedback/index', 2, N'UTILITY', 1, N'fa-clone')
INSERT [dbo].[Functions] ([ID], [Name], [URL], [DisplayOrder], [ParentId], [Status], [IconCss]) VALUES (N'FOOTER', N'Footer', N'/main/footer/index', 1, N'UTILITY', 1, N'fa-clone')
INSERT [dbo].[Functions] ([ID], [Name], [URL], [DisplayOrder], [ParentId], [Status], [IconCss]) VALUES (N'FUNCTION', N'Chức năng', N'/main/function/index', 2, N'SYSTEM', 1, N'fa-home')
INSERT [dbo].[Functions] ([ID], [Name], [URL], [DisplayOrder], [ParentId], [Status], [IconCss]) VALUES (N'ORDER', N'Hóa đơn', N'/main/order/index', 3, N'PRODUCT', 1, N'fa-chevron-down')
INSERT [dbo].[Functions] ([ID], [Name], [URL], [DisplayOrder], [ParentId], [Status], [IconCss]) VALUES (N'POST', N'Bài viết', N'/main/post/index', 2, N'CONTENT', 1, N'fa-table')
INSERT [dbo].[Functions] ([ID], [Name], [URL], [DisplayOrder], [ParentId], [Status], [IconCss]) VALUES (N'POST_CATEGORY', N'Danh mục', N'/main/post-category/index', 1, N'CONTENT', 1, N'fa-table')
INSERT [dbo].[Functions] ([ID], [Name], [URL], [DisplayOrder], [ParentId], [Status], [IconCss]) VALUES (N'PRODUCT', N'Sản phẩm', N'/', 2, NULL, 1, N'fa-chevron-down')
INSERT [dbo].[Functions] ([ID], [Name], [URL], [DisplayOrder], [ParentId], [Status], [IconCss]) VALUES (N'PRODUCT_CATEGORY', N'Danh mục', N'/main/product-category/index', 1, N'PRODUCT', 1, N'fa-chevron-down')
INSERT [dbo].[Functions] ([ID], [Name], [URL], [DisplayOrder], [ParentId], [Status], [IconCss]) VALUES (N'PRODUCT_LIST', N'Sản phẩm', N'/main/product/index', 2, N'PRODUCT', 1, N'fa-chevron-down')
INSERT [dbo].[Functions] ([ID], [Name], [URL], [DisplayOrder], [ParentId], [Status], [IconCss]) VALUES (N'READER', N'Báo cáo độc giả', N'/main/report/reader', 3, N'REPORT', 1, N'fa-bar-chart-o')
INSERT [dbo].[Functions] ([ID], [Name], [URL], [DisplayOrder], [ParentId], [Status], [IconCss]) VALUES (N'REPORT', N'Báo cáo', N'/', 5, NULL, 1, N'fa-bar-chart-o')
INSERT [dbo].[Functions] ([ID], [Name], [URL], [DisplayOrder], [ParentId], [Status], [IconCss]) VALUES (N'REVENUES', N'Báo cáo doanh thu', N'/main/report/revenues', 1, N'REPORT', 1, N'fa-bar-chart-o')
INSERT [dbo].[Functions] ([ID], [Name], [URL], [DisplayOrder], [ParentId], [Status], [IconCss]) VALUES (N'ROLE', N'Nhóm', N'/main/role/index', 1, N'SYSTEM', 1, N'fa-home')
INSERT [dbo].[Functions] ([ID], [Name], [URL], [DisplayOrder], [ParentId], [Status], [IconCss]) VALUES (N'SETTING', N'Cấu hình', N'/main/setting/index', 6, N'SYSTEM', 1, N'fa-home')
INSERT [dbo].[Functions] ([ID], [Name], [URL], [DisplayOrder], [ParentId], [Status], [IconCss]) VALUES (N'SYSTEM', N'Hệ thống', N'/', 1, NULL, 1, N'fa-desktop')
INSERT [dbo].[Functions] ([ID], [Name], [URL], [DisplayOrder], [ParentId], [Status], [IconCss]) VALUES (N'USER', N'Người dùng', N'/main/user/index', 3, N'SYSTEM', 1, N'fa-home')
INSERT [dbo].[Functions] ([ID], [Name], [URL], [DisplayOrder], [ParentId], [Status], [IconCss]) VALUES (N'UTILITY', N'Tiện ích', N'/', 4, NULL, 1, N'fa-clone')
INSERT [dbo].[Functions] ([ID], [Name], [URL], [DisplayOrder], [ParentId], [Status], [IconCss]) VALUES (N'USER_ROLE', N'USER_ROLE', N'/', 4, NULL, 1, N'fa-clone')
INSERT [dbo].[Functions] ([ID], [Name], [URL], [DisplayOrder], [ParentId], [Status], [IconCss]) VALUES (N'DASHBOARD', N'DASHBOARD', N'/', 4, NULL, 1, N'fa-clone')

INSERT [dbo].[Functions] ([ID], [Name], [URL], [DisplayOrder], [ParentId], [Status], [IconCss]) VALUES (N'COURSE_CATEGORY', N'COURSES_CATEGORY', N'/', 4, NULL, 1, N'fa-clone')
INSERT [dbo].[Functions] ([ID], [Name], [URL], [DisplayOrder], [ParentId], [Status], [IconCss]) VALUES (N'COURSE', N'COURSE', N'/', 4, NULL, 1, N'fa-clone')
INSERT [dbo].[Functions] ([ID], [Name], [URL], [DisplayOrder], [ParentId], [Status], [IconCss]) VALUES (N'LESSON', N'LESSON', N'/', 4, NULL, 1, N'fa-clone')
INSERT [dbo].[Functions] ([ID], [Name], [URL], [DisplayOrder], [ParentId], [Status], [IconCss]) VALUES (N'STUDENT', N'STUDENT', N'/', 4, NULL, 1, N'fa-clone')
INSERT [dbo].[Functions] ([ID], [Name], [URL], [DisplayOrder], [ParentId], [Status], [IconCss]) VALUES (N'TRAINNER', N'TRAINNER', N'/', 4, NULL, 1, N'fa-clone')
INSERT [dbo].[Functions] ([ID], [Name], [URL], [DisplayOrder], [ParentId], [Status], [IconCss]) VALUES (N'COURSE_LESSON', N'COURSE_LESSON', N'/', 4, NULL, 1, N'fa-clone')
INSERT [dbo].[Functions] ([ID], [Name], [URL], [DisplayOrder], [ParentId], [Status], [IconCss]) VALUES (N'COURSE_STUDENT', N'COURSE_STUDENT', N'/', 4, NULL, 1, N'fa-clone')
INSERT [dbo].[Functions] ([ID], [Name], [URL], [DisplayOrder], [ParentId], [Status], [IconCss]) VALUES (N'LESSON_COMMENT', N'LESSON_COMMENT', N'/', 4, NULL, 1, N'fa-clone')

--INSERT [dbo].[AppRoles] ([Id], [Name], [Description], [Discriminator]) VALUES (N'4a370bd7-7a56-4c83-a93c-3f07ba9a5270', N'CUSTOMER', N'Role for customer of company', N'AppRole')
--INSERT [dbo].[AppRoles] ([Id], [Name], [Description], [Discriminator]) VALUES (N'69b11556-7afb-42b9-858b-993cb4b7e5aa', N'EMPLOYEE', N'Role for employees of company', N'AppRole')
--INSERT [dbo].[AppRoles] ([Id], [Name], [Description], [Discriminator]) VALUES (N'81092a0f-c47f-4b33-8697-e5da63a633e9', N'STAFF', N'Role for employees of company', N'AppRole')
--INSERT [dbo].[AppRoles] ([Id], [Name], [Description], [Discriminator]) VALUES (N'8e9a6a1b-9ef6-4a04-9322-0e4f829680bc', N'MANAGER', N'Role for manager of company', N'AppRole')
--INSERT [dbo].[AppRoles] ([Id], [Name], [Description], [Discriminator]) VALUES (N'938272c1-4109-4b53-8a07-a2af6b217b1b', N'HR', N'Role for HR of company', N'AppRole')
--INSERT [dbo].[AppRoles] ([Id], [Name], [Description], [Discriminator]) VALUES (N'a9c387a2-c8cc-4e94-a30a-9e2fc6e90771', N'ADMIN', N'Admin of system', N'AppRole')

INSERT [dbo].[Functions] ([ID], [Name], [URL], [DisplayOrder], [ParentId], [Status], [IconCss]) VALUES (N'PERMISSION', N'PERMISSION', N'/', 4, NULL, 1, N'fa-clone')

GO
INSERT [dbo].[Action] ([ID], [Name], [Description]) VALUES (N'ASSIGN_PERMISSION_TO_ROLE', N'ASSIGN_PERMISSION_TO_ROLE', N'ASSIGN_PERMISSION_TO_ROLE')
INSERT [dbo].[Action] ([ID], [Name], [Description]) VALUES (N'ASSIGN_USER_TO_ROLE', N'ASSIGN_USER_TO_ROLE', N'ASSIGN_USER_TO_ROLE')
INSERT [dbo].[Action] ([ID], [Name], [Description]) VALUES (N'CREATE', N'CREATE', N'CREATE')
INSERT [dbo].[Action] ([ID], [Name], [Description]) VALUES (N'DELETE', N'DELETE', N'DELETE')
INSERT [dbo].[Action] ([ID], [Name], [Description]) VALUES (N'EDIT', N'EDIT', N'EDIT')
INSERT [dbo].[Action] ([ID], [Name], [Description]) VALUES (N'EDIT_DEPARTMENT', N'EDIT_DEPARTMENT', N'EDIT_DEPARTMENT')
INSERT [dbo].[Action] ([ID], [Name], [Description]) VALUES (N'EDIT_POSITION', N'EDIT_POSITION', N'EDIT_POSITION')
INSERT [dbo].[Action] ([ID], [Name], [Description]) VALUES (N'VIEW', N'VIEW', N'VIEW')

GO
INSERT [dbo].[Function_Action] ( [ActionId], [FunctionId]) VALUES (N'CREATE', N'ACCESS')
INSERT [dbo].[Function_Action] ( [ActionId], [FunctionId]) VALUES (N'EDIT', N'ACCESS')
INSERT [dbo].[Function_Action] ( [ActionId], [FunctionId]) VALUES (N'VIEW', N'ACCESS')
INSERT [dbo].[Function_Action] ( [ActionId], [FunctionId]) VALUES (N'DELETE', N'ACCESS')
INSERT [dbo].[Function_Action] ( [ActionId], [FunctionId]) VALUES (N'CREATE', N'ACTIVITY')
INSERT [dbo].[Function_Action] ( [ActionId], [FunctionId]) VALUES (N'EDIT', N'ACTIVITY')
INSERT [dbo].[Function_Action] ( [ActionId], [FunctionId]) VALUES (N'VIEW', N'ACTIVITY')
INSERT [dbo].[Function_Action] ( [ActionId], [FunctionId]) VALUES (N'DELETE', N'ACTIVITY')
INSERT [dbo].[Function_Action] ( [ActionId], [FunctionId]) VALUES (N'CREATE', N'ANNOUNCEMENT')
INSERT [dbo].[Function_Action] ( [ActionId], [FunctionId]) VALUES (N'EDIT', N'ANNOUNCEMENT')
INSERT [dbo].[Function_Action] ( [ActionId], [FunctionId]) VALUES (N'VIEW', N'ANNOUNCEMENT')
INSERT [dbo].[Function_Action] ( [ActionId], [FunctionId]) VALUES (N'DELETE', N'ANNOUNCEMENT')
INSERT [dbo].[Function_Action] ( [ActionId], [FunctionId]) VALUES (N'CREATE', N'CONTACT')
INSERT [dbo].[Function_Action] ( [ActionId], [FunctionId]) VALUES (N'EDIT', N'CONTACT')
INSERT [dbo].[Function_Action] ( [ActionId], [FunctionId]) VALUES (N'VIEW', N'CONTACT')
INSERT [dbo].[Function_Action] ( [ActionId], [FunctionId]) VALUES (N'DELETE', N'CONTACT')
INSERT [dbo].[Function_Action] ( [ActionId], [FunctionId]) VALUES (N'CREATE', N'CONTENT')
INSERT [dbo].[Function_Action] ( [ActionId], [FunctionId]) VALUES (N'EDIT', N'CONTENT')
INSERT [dbo].[Function_Action] ( [ActionId], [FunctionId]) VALUES (N'VIEW', N'CONTENT')
INSERT [dbo].[Function_Action] ( [ActionId], [FunctionId]) VALUES ( N'DELETE', N'CONTENT')
INSERT [dbo].[Function_Action] ( [ActionId], [FunctionId]) VALUES ( N'CREATE', N'FEEDBACK')
INSERT [dbo].[Function_Action] ( [ActionId], [FunctionId]) VALUES ( N'EDIT', N'FEEDBACK')
INSERT [dbo].[Function_Action] ( [ActionId], [FunctionId]) VALUES ( N'VIEW', N'FEEDBACK')
INSERT [dbo].[Function_Action] ( [ActionId], [FunctionId]) VALUES ( N'DELETE', N'FEEDBACK')
INSERT [dbo].[Function_Action] ( [ActionId], [FunctionId]) VALUES ( N'CREATE', N'FOOTER')
INSERT [dbo].[Function_Action] ( [ActionId], [FunctionId]) VALUES ( N'EDIT', N'FOOTER')
INSERT [dbo].[Function_Action] ( [ActionId], [FunctionId]) VALUES ( N'VIEW', N'FOOTER')
INSERT [dbo].[Function_Action] ( [ActionId], [FunctionId]) VALUES ( N'DELETE', N'FOOTER')
INSERT [dbo].[Function_Action] ( [ActionId], [FunctionId]) VALUES ( N'CREATE', N'ORDER')
INSERT [dbo].[Function_Action] ( [ActionId], [FunctionId]) VALUES ( N'EDIT', N'ORDER')
INSERT [dbo].[Function_Action] ( [ActionId], [FunctionId]) VALUES ( N'VIEW', N'ORDER')
INSERT [dbo].[Function_Action] ( [ActionId], [FunctionId]) VALUES ( N'DELETE', N'ORDER')
INSERT [dbo].[Function_Action] ( [ActionId], [FunctionId]) VALUES ( N'CREATE', N'POST')
INSERT [dbo].[Function_Action] ( [ActionId], [FunctionId]) VALUES ( N'EDIT', N'POST')
INSERT [dbo].[Function_Action] ( [ActionId], [FunctionId]) VALUES ( N'VIEW', N'POST')
INSERT [dbo].[Function_Action] ( [ActionId], [FunctionId]) VALUES ( N'DELETE', N'POST')
INSERT [dbo].[Function_Action] ( [ActionId], [FunctionId]) VALUES ( N'CREATE', N'POST_CATEGORY')
INSERT [dbo].[Function_Action] ( [ActionId], [FunctionId]) VALUES ( N'EDIT', N'POST_CATEGORY')
INSERT [dbo].[Function_Action] ( [ActionId], [FunctionId]) VALUES ( N'VIEW', N'POST_CATEGORY')
INSERT [dbo].[Function_Action] ( [ActionId], [FunctionId]) VALUES ( N'DELETE', N'POST_CATEGORY')
INSERT [dbo].[Function_Action] ( [ActionId], [FunctionId]) VALUES ( N'CREATE', N'PRODUCT')
INSERT [dbo].[Function_Action] ( [ActionId], [FunctionId]) VALUES ( N'EDIT', N'PRODUCT')
INSERT [dbo].[Function_Action] ( [ActionId], [FunctionId]) VALUES ( N'VIEW', N'PRODUCT')
INSERT [dbo].[Function_Action] ( [ActionId], [FunctionId]) VALUES ( N'DELETE', N'PRODUCT')
INSERT [dbo].[Function_Action] ( [ActionId], [FunctionId]) VALUES ( N'CREATE', N'PRODUCT_CATEGORY')
INSERT [dbo].[Function_Action] ( [ActionId], [FunctionId]) VALUES ( N'EDIT', N'PRODUCT_CATEGORY')
INSERT [dbo].[Function_Action] ( [ActionId], [FunctionId]) VALUES ( N'VIEW', N'PRODUCT_CATEGORY')
INSERT [dbo].[Function_Action] ( [ActionId], [FunctionId]) VALUES ( N'DELETE', N'PRODUCT_CATEGORY')
INSERT [dbo].[Function_Action] ( [ActionId], [FunctionId]) VALUES ( N'CREATE', N'READER')
INSERT [dbo].[Function_Action] ( [ActionId], [FunctionId]) VALUES ( N'EDIT', N'READER')
INSERT [dbo].[Function_Action] ( [ActionId], [FunctionId]) VALUES ( N'VIEW', N'READER')
INSERT [dbo].[Function_Action] ( [ActionId], [FunctionId]) VALUES ( N'DELETE', N'READER')
INSERT [dbo].[Function_Action] ( [ActionId], [FunctionId]) VALUES ( N'CREATE', N'REPORT')
INSERT [dbo].[Function_Action] ( [ActionId], [FunctionId]) VALUES ( N'EDIT', N'REPORT')
INSERT [dbo].[Function_Action] ( [ActionId], [FunctionId]) VALUES ( N'VIEW', N'REPORT')
INSERT [dbo].[Function_Action] ( [ActionId], [FunctionId]) VALUES ( N'DELETE', N'REPORT')
INSERT [dbo].[Function_Action] ( [ActionId], [FunctionId]) VALUES ( N'CREATE', N'REVENUES')
INSERT [dbo].[Function_Action] ( [ActionId], [FunctionId]) VALUES ( N'EDIT', N'REVENUES')
INSERT [dbo].[Function_Action] ( [ActionId], [FunctionId]) VALUES ( N'VIEW', N'REVENUES')
INSERT [dbo].[Function_Action] ( [ActionId], [FunctionId]) VALUES ( N'DELETE', N'REVENUES')
INSERT [dbo].[Function_Action] ( [ActionId], [FunctionId]) VALUES ( N'CREATE', N'SETTING')
INSERT [dbo].[Function_Action] ( [ActionId], [FunctionId]) VALUES ( N'EDIT', N'SETTING')
INSERT [dbo].[Function_Action] ( [ActionId], [FunctionId]) VALUES ( N'VIEW', N'SETTING')
INSERT [dbo].[Function_Action] ( [ActionId], [FunctionId]) VALUES ( N'DELETE', N'SETTING')
INSERT [dbo].[Function_Action] ( [ActionId], [FunctionId]) VALUES ( N'CREATE', N'SETTING')
INSERT [dbo].[Function_Action] ( [ActionId], [FunctionId]) VALUES ( N'EDIT', N'SETTING')
INSERT [dbo].[Function_Action] ( [ActionId], [FunctionId]) VALUES ( N'VIEW', N'SETTING')
INSERT [dbo].[Function_Action] ( [ActionId], [FunctionId]) VALUES ( N'DELETE', N'SETTING')
INSERT [dbo].[Function_Action] ( [ActionId], [FunctionId]) VALUES ( N'CREATE', N'USER')
INSERT [dbo].[Function_Action] ( [ActionId], [FunctionId]) VALUES ( N'EDIT', N'USER')
INSERT [dbo].[Function_Action] ( [ActionId], [FunctionId]) VALUES ( N'VIEW', N'USER')
INSERT [dbo].[Function_Action] ( [ActionId], [FunctionId]) VALUES ( N'DELETE', N'USER')
INSERT [dbo].[Function_Action] ( [ActionId], [FunctionId]) VALUES ( N'EDIT_DEPARTMENT', N'USER')
INSERT [dbo].[Function_Action] ( [ActionId], [FunctionId]) VALUES ( N'EDIT_POSITION', N'USER')
INSERT [dbo].[Function_Action] ( [ActionId], [FunctionId]) VALUES ( N'ASSIGN_USER_TO_ROLE', N'USER_ROLE')
INSERT [dbo].[Function_Action] ( [ActionId], [FunctionId]) VALUES ( N'VIEW', N'USER_ROLE')
INSERT [dbo].[Function_Action] ( [ActionId], [FunctionId]) VALUES ( N'DELETE', N'USER_ROLE')
INSERT [dbo].[Function_Action] ( [ActionId], [FunctionId]) VALUES ( N'ASSIGN_PERMISSION_TO_ROLE', N'PERMISSION')
INSERT [dbo].[Function_Action] ( [ActionId], [FunctionId]) VALUES ( N'VIEW', N'PERMISSION')
INSERT [dbo].[Function_Action] ( [ActionId], [FunctionId]) VALUES ( N'DELETE', N'PERMISSION')

INSERT [dbo].[Function_Action] ([ActionId], [FunctionId]) VALUES ( N'CREATE', N'ROLE')
INSERT [dbo].[Function_Action] ([ActionId], [FunctionId]) VALUES ( N'EDIT', N'ROLE')
INSERT [dbo].[Function_Action] ([ActionId], [FunctionId]) VALUES ( N'DELETE', N'ROLE')
INSERT [dbo].[Function_Action] ([ActionId], [FunctionId]) VALUES ( N'VIEW', N'ROLE')

----------------

INSERT [dbo].[Function_Action] ([ActionId], [FunctionId]) VALUES ( N'CREATE', N'COURSE_CATEGORY')
INSERT [dbo].[Function_Action] ([ActionId], [FunctionId]) VALUES ( N'EDIT', N'COURSE_CATEGORY')
INSERT [dbo].[Function_Action] ([ActionId], [FunctionId]) VALUES ( N'DELETE', N'COURSE_CATEGORY')
INSERT [dbo].[Function_Action] ([ActionId], [FunctionId]) VALUES ( N'VIEW', N'COURSE_CATEGORY')

INSERT [dbo].[Function_Action] ([ActionId], [FunctionId]) VALUES ( N'CREATE', N'COURSE')
INSERT [dbo].[Function_Action] ([ActionId], [FunctionId]) VALUES ( N'EDIT', N'COURSE')
INSERT [dbo].[Function_Action] ([ActionId], [FunctionId]) VALUES ( N'DELETE', N'COURSE')
INSERT [dbo].[Function_Action] ([ActionId], [FunctionId]) VALUES ( N'VIEW', N'COURSE')

INSERT [dbo].[Function_Action] ([ActionId], [FunctionId]) VALUES ( N'CREATE', N'LESSON')
INSERT [dbo].[Function_Action] ([ActionId], [FunctionId]) VALUES ( N'EDIT', N'LESSON')
INSERT [dbo].[Function_Action] ([ActionId], [FunctionId]) VALUES ( N'DELETE', N'LESSON')
INSERT [dbo].[Function_Action] ([ActionId], [FunctionId]) VALUES ( N'VIEW', N'LESSON')

INSERT [dbo].[Function_Action] ([ActionId], [FunctionId]) VALUES ( N'CREATE', N'STUDENT')
INSERT [dbo].[Function_Action] ([ActionId], [FunctionId]) VALUES ( N'EDIT', N'STUDENT')
INSERT [dbo].[Function_Action] ([ActionId], [FunctionId]) VALUES ( N'DELETE', N'STUDENT')
INSERT [dbo].[Function_Action] ([ActionId], [FunctionId]) VALUES ( N'VIEW', N'STUDENT')

INSERT [dbo].[Function_Action] ([ActionId], [FunctionId]) VALUES ( N'CREATE', N'TRAINNER')
INSERT [dbo].[Function_Action] ([ActionId], [FunctionId]) VALUES ( N'EDIT', N'TRAINNER')
INSERT [dbo].[Function_Action] ([ActionId], [FunctionId]) VALUES ( N'DELETE', N'TRAINNER')
INSERT [dbo].[Function_Action] ([ActionId], [FunctionId]) VALUES ( N'VIEW', N'TRAINNER')

INSERT [dbo].[Function_Action] ([ActionId], [FunctionId]) VALUES ( N'CREATE', N'COURSE_LESSON')
INSERT [dbo].[Function_Action] ([ActionId], [FunctionId]) VALUES ( N'EDIT', N'COURSE_LESSON')
INSERT [dbo].[Function_Action] ([ActionId], [FunctionId]) VALUES ( N'DELETE', N'COURSE_LESSON')
INSERT [dbo].[Function_Action] ([ActionId], [FunctionId]) VALUES ( N'VIEW', N'COURSE_LESSON')


INSERT [dbo].[Function_Action] ([ActionId], [FunctionId]) VALUES ( N'CREATE', N'COURSE_STUDENT')
INSERT [dbo].[Function_Action] ([ActionId], [FunctionId]) VALUES ( N'EDIT', N'COURSE_STUDENT')
INSERT [dbo].[Function_Action] ([ActionId], [FunctionId]) VALUES ( N'DELETE', N'COURSE_STUDENT')
INSERT [dbo].[Function_Action] ([ActionId], [FunctionId]) VALUES ( N'VIEW', N'COURSE_STUDENT')

INSERT [dbo].[Function_Action] ([ActionId], [FunctionId]) VALUES ( N'CREATE', N'LESSON_COMMENT')
INSERT [dbo].[Function_Action] ([ActionId], [FunctionId]) VALUES ( N'EDIT', N'LESSON_COMMENT')
INSERT [dbo].[Function_Action] ([ActionId], [FunctionId]) VALUES ( N'DELETE', N'LESSON_COMMENT')
INSERT [dbo].[Function_Action] ([ActionId], [FunctionId]) VALUES ( N'VIEW', N'LESSON_COMMENT')


GO
SET IDENTITY_INSERT [dbo].[About] ON 

INSERT [dbo].[About] ([ID], [Description], [PhoneNumber], [Email], [OpenTime], [Address]) VALUES (1, N'Education', N'0000', N'email@gmail.com', N'10-20', N'Vietnam')
SET IDENTITY_INSERT [dbo].[About] OFF
GO

INSERT [dbo].[Footer] ([ID], [Content]) VALUES ('Footer', N'')

GO

GO
SET IDENTITY_INSERT [dbo].[ContentCategories] ON 

INSERT [dbo].[ContentCategories] ([ID], [Name], [MetaTitle], [ParentID], [DisplayOrder], [SeoTitle], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [MetaKeywords], [MetaDescriptions], [Status], [ShowOnHome], [Language], [IsActive],[MetaCode]) VALUES (1, N'Tin tức', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'News', NULL, 1, NULL, NULL, 1, N'NEWS')
INSERT [dbo].[ContentCategories] ([ID], [Name], [MetaTitle], [ParentID], [DisplayOrder], [SeoTitle], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [MetaKeywords], [MetaDescriptions], [Status], [ShowOnHome], [Language], [IsActive],[MetaCode]) VALUES (2, N'Sự kiện', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Event', NULL, 1, NULL, NULL, 1, N'EVENT')
SET IDENTITY_INSERT [dbo].[ContentCategories] OFF
GO
GO
--INSERT [dbo].[AppUsers] ([Id], [FullName], [Address], [Avatar], [BirthDay], [Status], [Gender], [Department], [Position], [Country], [CountryRegionCode], [City], [Postcode], [FileContentType], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'432096a2-a89c-4683-b411-738ea2293c7d', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'admin@gmail.com', 0, N'AEDsWpmSOvBoKnYzkPB33XvAglzl7WldhPBdMg6WTRwxyHIMLLf1yuc6LPByckHzTg==', N'249c5f58-c475-4f5a-93ea-08314d6235a0', NULL, 0, 0, NULL, 0, 0, N'admin@gmail.com')
--INSERT [dbo].[AppUsers] ([Id], [FullName], [Address], [Avatar], [BirthDay], [Status], [Gender], [Department], [Position], [Country], [CountryRegionCode], [City], [Postcode], [FileContentType], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'633b962b-ff1b-49f1-bbe5-f4ee66aeff9e', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'customer@gmail.com', 0, N'AF1x7oNXl2bN1LNuDxMo178nUM0JcRUHorFhQ5HGMKheGO+WTeBZCOLpmFLQv6Pm8w==', N'ab9bc56c-b96e-451f-8a6e-0e6d448e2a55', NULL, 0, 0, NULL, 0, 0, N'customer@gmail.com')
--GO
--INSERT [dbo].[AppUserRoles] ([UserId], [RoleId], [IdentityRole_Id], [AppUser_Id]) VALUES (N'432096a2-a89c-4683-b411-738ea2293c7d', N'69b11556-7afb-42b9-858b-993cb4b7e5aa', NULL, NULL)
--INSERT [dbo].[AppUserRoles] ([UserId], [RoleId], [IdentityRole_Id], [AppUser_Id]) VALUES (N'432096a2-a89c-4683-b411-738ea2293c7d', N'8e9a6a1b-9ef6-4a04-9322-0e4f829680bc', NULL, NULL)
--INSERT [dbo].[AppUserRoles] ([UserId], [RoleId], [IdentityRole_Id], [AppUser_Id]) VALUES (N'432096a2-a89c-4683-b411-738ea2293c7d', N'a9c387a2-c8cc-4e94-a30a-9e2fc6e90771', NULL, NULL)
--INSERT [dbo].[AppUserRoles] ([UserId], [RoleId], [IdentityRole_Id], [AppUser_Id]) VALUES (N'633b962b-ff1b-49f1-bbe5-f4ee66aeff9e', N'4a370bd7-7a56-4c83-a93c-3f07ba9a5270', NULL, NULL)
--GO
