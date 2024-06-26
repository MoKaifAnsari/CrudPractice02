USE [practice]
GO
/****** Object:  Table [dbo].[tbl_LoginUser]    Script Date: 14-May-24 10:43:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_LoginUser](
	[username] [varchar](100) NULL,
	[password] [varchar](100) NULL,
	[lid] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_tbl_LoginUser] PRIMARY KEY CLUSTERED 
(
	[lid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_Registation]    Script Date: 14-May-24 10:43:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_Registation](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[fname] [varchar](500) NULL,
	[lname] [varchar](500) NULL,
	[number] [varchar](100) NULL,
	[email] [varchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[sp_DeleteRagistation]    Script Date: 14-May-24 10:43:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[sp_DeleteRagistation]
 @id int=0
 as
 begin
 Declare @Msg varchar(Max)=''
  delete from tbl_Registation where id=@id
  set @Msg='Date hase been Delated !';
 select @Msg as Msg
 end
GO
/****** Object:  StoredProcedure [dbo].[sp_insertupdateregistation]    Script Date: 14-May-24 10:43:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 CREATE proc [dbo].[sp_insertupdateregistation]
 @id int=0,
 @fname varchar(100),
 @lname varchar(100),
 @email varchar(100),
 @number varchar(100)
 as
 begin
  Declare @Msg varchar(max)='' , @Focus varchar(100)='',@Status int=0
  if @id=0
  begin
    insert into tbl_Registation(fname,lname,email,number) values(@fname,@lname,@email,@number)
	set @Msg='Data Inserted !'
	set @Status=1
  end
  else
  begin
    update tbl_Registation set fname=@fname,lname=@lname,email=@email,number=@number where id=@id
	set @Msg='Data Updated Succesfully !'
	set @Status=2
  end
  select @Msg as Msg,@Focus as Focus,@Status as [Status]
 end
GO
/****** Object:  StoredProcedure [dbo].[sp_LoginUser]    Script Date: 14-May-24 10:43:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_LoginUser]
@username varchar(100),
@password varchar(100)
as
begin
 Declare @Msg varchar(max)='' , @Focus varchar(100)='',@Status int=''
 if exists (select lid from tbl_LoginUser where username=@username and password=@password)
  begin
   set @Status=1
   set @Msg='You Log-in successfully.!'
  end
 else
  begin
   Set @Status=0
	Set @Msg='Your email and pass was not currect.!'
  end
  Select @Msg As Msg,@Status As [Status],@Focus As [Focus]
end

 select * from tbl_LoginUser
GO
/****** Object:  StoredProcedure [dbo].[sp_ShowData]    Script Date: 14-May-24 10:43:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 CREATE proc [dbo].[sp_ShowData]
 @id int=0,
 @Type varchar(10)
 as
 begin
  if(@Type='S')
  begin
   select id,fname + lname as Fullname,email,number from tbl_Registation order by id desc
  end
  else
  begin
    select id,fname,lname,email,number from tbl_Registation where id=@id
  end
 end
GO
