USE [HMS]
GO
/****** Object:  Table [dbo].[Accounttype]    Script Date: 23-10-2019 16:25:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Accounttype](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[accounttype] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[passwordresets]    Script Date: 23-10-2019 16:25:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[passwordresets](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[userid] [bigint] NOT NULL,
	[otp] [nvarchar](50) NULL,
	[status] [bit] NULL,
	[enteredby] [nvarchar](255) NOT NULL,
	[entereddate] [datetime] NOT NULL,
	[enteredbyip] [nvarchar](20) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[users]    Script Date: 23-10-2019 16:25:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[users](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[firstname] [nvarchar](100) NULL,
	[lastname] [nvarchar](100) NULL,
	[accounttype] [int] NULL,
	[username] [nvarchar](100) NULL,
	[password] [nvarchar](100) NULL,
	[email] [nvarchar](100) NULL,
	[hide] [nvarchar](2) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Accounttype] ON 

INSERT [dbo].[Accounttype] ([id], [accounttype]) VALUES (1, N'Admin')
INSERT [dbo].[Accounttype] ([id], [accounttype]) VALUES (2, N'Doctor')
INSERT [dbo].[Accounttype] ([id], [accounttype]) VALUES (3, N'Patient')
INSERT [dbo].[Accounttype] ([id], [accounttype]) VALUES (4, N'Nurse')
INSERT [dbo].[Accounttype] ([id], [accounttype]) VALUES (5, N'Pharmacist')
INSERT [dbo].[Accounttype] ([id], [accounttype]) VALUES (6, N'Laboratorist')
INSERT [dbo].[Accounttype] ([id], [accounttype]) VALUES (7, N'Accountant')
SET IDENTITY_INSERT [dbo].[Accounttype] OFF
SET IDENTITY_INSERT [dbo].[passwordresets] ON 

INSERT [dbo].[passwordresets] ([id], [userid], [otp], [status], [enteredby], [entereddate], [enteredbyip]) VALUES (1, 1, N'52230661', 1, N'1', CAST(N'2019-07-09T16:56:41.420' AS DateTime), N'::1')
SET IDENTITY_INSERT [dbo].[passwordresets] OFF
SET IDENTITY_INSERT [dbo].[users] ON 

INSERT [dbo].[users] ([id], [firstname], [lastname], [accounttype], [username], [password], [email], [hide]) VALUES (3, N'Ravi', N'Rana', 1, N'admin', N'123', N'ravirana.equitec@gmail.com', N'N')
INSERT [dbo].[users] ([id], [firstname], [lastname], [accounttype], [username], [password], [email], [hide]) VALUES (4, N'Mayur', N'wagh', 2, N'mayur', N'123', N'mayur.equitec@gmail.com', N'N')
INSERT [dbo].[users] ([id], [firstname], [lastname], [accounttype], [username], [password], [email], [hide]) VALUES (5, N'Bhim', N'singh', 3, N'bhim', N'123', N'bhim@gmail.com', N'N')
SET IDENTITY_INSERT [dbo].[users] OFF
/****** Object:  StoredProcedure [dbo].[sp_ForgotPassword_VerifyUser]    Script Date: 23-10-2019 16:25:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_ForgotPassword_VerifyUser] 
(
	@usernameOrEmail nvarchar(300)
)
as
begin
	select * from users 
		where (username = @usernameOrEmail or email = @usernameOrEmail)
end


GO
/****** Object:  StoredProcedure [dbo].[sp_getAllDoctors]    Script Date: 23-10-2019 16:25:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[sp_getAllDoctors]
as
begin
  select * from users where hide='N' and accounttype=2
end 
GO
/****** Object:  StoredProcedure [dbo].[sp_getAllNurse]    Script Date: 23-10-2019 16:25:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[sp_getAllNurse]
as
begin
  select * from users where hide='N' and accounttype=4
end 
GO
/****** Object:  StoredProcedure [dbo].[sp_getAllPatient]    Script Date: 23-10-2019 16:25:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[sp_getAllPatient]
as
begin
  select * from users where hide='N' and accounttype=3
end 
GO
/****** Object:  StoredProcedure [dbo].[sp_getAllUsers]    Script Date: 23-10-2019 16:25:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[sp_getAllUsers]
(
@accountType int
) 
as
begin
  select * from users where hide='N' and accounttype=@accountType
end 
GO
/****** Object:  StoredProcedure [dbo].[sp_GetOTPDetails]    Script Date: 23-10-2019 16:25:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[sp_GetOTPDetails](
@UserId BIGINT,
@OTPNumber VARCHAR(20)
)
AS BEGIN
       SET @OTPNumber = COALESCE(@OTPNumber, 0)
    Select * from passwordresets
	WHERE 
	  (@OTPNumber = 0 OR otp = @OTPNumber)
	   AND userid = @UserId
	   AND entereddate <= GETDATE() and entereddate >= DATEADD(minute,-15,GETDATE())
END 
GO
/****** Object:  StoredProcedure [dbo].[sp_getUserRoles]    Script Date: 23-10-2019 16:25:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[sp_getUserRoles]
(
 @username nvarchar(100)
)
as
begin
  select at.accounttype from users u 
  inner join Accounttype at on u.accounttype =at.id
  where u.username=@username
end
GO
/****** Object:  StoredProcedure [dbo].[sp_SaveOTPDetails]    Script Date: 23-10-2019 16:25:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create PROCEDURE [dbo].[sp_SaveOTPDetails](
@UserId BIGINT,
@OTP NVARCHAR(50),
@Status BIT,
@EnteredBy VARCHAR(20),
@EnteredIp	 VARCHAR(20)
)
AS BEGIN
   INSERT INTO passwordresets( 
                                 userid, 
								 otp,
								 status,
								 enteredby,
								 entereddate,
							     enteredbyip 
							  )
						VALUES(
						         @UserId,
								 @OTP,
						         @Status,
								 @EnteredBy,
								 GETDATE(),
								 @EnteredIp
							  )
END 
GO
/****** Object:  StoredProcedure [dbo].[sp_saveUsers]    Script Date: 23-10-2019 16:25:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE proc [dbo].[sp_saveUsers]
(
@firstname nvarchar(100),
@lastname nvarchar(100),
@accountType int,
@username nvarchar(100),
@password nvarchar(100),
@email nvarchar(50),
@status nvarchar(2)
)
as
begin
  insert into users(firstname,lastname,accounttype,username,password,email,hide) values(@firstname,@lastname,@accountType,@username,@password,@email,@status)
end

GO
