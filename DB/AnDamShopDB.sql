create database ANDAM on(
  SIZE = 200 --tạo tệp tin data
  ,MAXSIZE = unlimited
  ,FILEGROWTH=20
  ,NAME=ANDAM
  ,FILENAME='D:\BT\DATN\ShopAnDam\DB\AnDam.MDF'

)
log on
(
  SIZE = 100 --tạo tệp tin log
  ,MAXSIZE = unlimited
  ,FILEGROWTH=20
  ,NAME=ANDAM_log
  ,FILENAME='D:\BT\DATN\ShopAnDam\DB\AnDam.ldf'

)


--tạo bảng nhóm người dừng
create table UserGroup(
    ID varchar(50) primary key not null
   ,Name nvarchar(50)
   ,CreateDate datetime DEFAULT GETDATE()
   ,CreateBy varchar(50)
)
--Tạo bảng người dùng
create table Users(
    ID int not null primary key identity
   ,UserName varchar(50) 
   ,PassWord varchar(50) 
   ,GroupID varchar(50) 
   ,Name nvarchar(50) 
   ,Email varchar(50)
   ,Phone varchar(11)
   ,CreateDate datetime default getdate()
   ,CreateBy varchar(50)
   ,Status bit default(0) 
   constraint FK_User_UserGroups foreign key (GroupID) references UserGroup(ID)
)


-- Tạo bảng quyền
create table Roles (
    ID varchar(50) primary key not null
	,Name nvarchar(50)
    ,CreateDate datetime DEFAULT GETDATE()
   ,CreateBy varchar(50)
)


--Tạo bảng quyền người dùng
create table Role_User(
    GroupID varchar(50) not null
   ,RoleID varchar(50) not null
  -- ,constraint FK_User_UserGroup foreign key (GroupID) references UserGroup(ID)
   --,constraint FK_Roles_UserGroups foreign key (RoleID) references Roles(ID)
)
--tạo bảng khách hàng
create table Customers(
  ID int primary key not null identity
  ,Name nvarchar(100)
  ,Address nvarchar(200) 
  ,Email varchar(50)
  ,Phone varchar(12)
  ,Createdate datetime default getdate()
  ,CreateBy varchar(50)
)
--tạo bảng loại sản phẩm
create table Category(
  ID int primary key not null identity
  ,Name nvarchar(100)
  ,MetaTilte varchar(100)
  ,DisplayOrder int default(0)
  ,SeoTitle nvarchar(100)
  ,Status bit default(1)
  ,CreateDate datetime default getdate()
  ,CreateBy varchar(50)
)
--Tạo bảng thương hiệu
create table Brands(
  ID int primary key not null identity
  ,Name nvarchar(50)
  ,Logo nvarchar(255)
  ,CreateDate datetime default getdate()
  ,CreateBy varchar(50)
)

--tạo bảng ảnh sản phẩm
create table Product_Image(
ID int primary key not null identity
,Image nvarchar(Max)
,CreateDate datetime default getdate()
,CreateBy varchar(50)
)
--tạo bảng sản phẩm
create table Products(
  ID int primary key not null identity
  ,ImageID int not null
  ,BrandID int not null
  ,CategoryID int not null
  ,Name nvarchar(150)
  ,Code varchar(50)
  ,Title nvarchar(50) 
  ,MetaTitle varchar(150)
  ,Description ntext
  ,Price decimal(18,0) default(0)
  ,MotionPrice decimal(18,0)
  ,Quantity int default(0)
  ,IncludeVAT bit  -- bao gồm VAT chưa mặc định là rồi
  ,Status bit default(0)
  ,TopHot datetime
  ,ViewCount int default(0)
  ,Tags nvarchar(500)
  ,CreateDate datetime default getdate()
  ,CreateBy varchar(50)
  ,constraint FK_Product_Image foreign key (ImageID) references Product_Image(ID)
  ,constraint FK_Product_Brand foreign key(BrandID) references Brands(ID)
  ,constraint FK_Product_Cate foreign key (CategoryID) references Category(ID)
)



--Tọa bảng nhà cung cấp (NCC)
create table Supplies(
  ID int primary key not null identity
  ,Name nvarchar(100)
  ,Address nvarchar(200)
  ,Email varchar(50)
  ,Phone varchar(12)
  ,Status bit default(0)
  ,CreateDate datetime default getdate()
  ,CreateBy varchar(50)
)
 create table Goods_Detail(
  ID int primary key not null identity
  ,ProductID int not null
  ,Name nvarchar(100)
  ,Quantity int
  ,Price decimal
  ,CreateDate datetime default getdate()
  ,CreateBy varchar(50)
  ,constraint FK_Cate_Product foreign key (ProductID) references Products(ID)
)
 --tạo bảng Product_Catagory
  create table Goods (
   ID int not null primary key identity
   ,GoodID int not null
   , SupplyID int not null
   ,CreateDate datetime default getdate() --ngày lập
   ,CreateBy varchar(50) 
	,constraint FK_Cate_Goods foreign key (SupplyID) references Supplies(ID)
	 ,constraint FK_GoodDetails foreign key (GoodID) references Goods_Detail(ID)
)
 --Taọ bảng Catagories



--tạo bảng Hóa đơn
 create table Orders(
   ID bigint primary key not null identity
   ,CustomersID int not null
   ,NameShip nvarchar(50)
   ,PhoneShip varchar(12)
   ,AdressShip nvarchar(250)
   ,MailShip varchar(50)
   ,Node nvarchar(250)
   ,Status int default(0)
   ,Payment_Method int default(0)
   ,CreateDate datetime default getdate()
   ,CreateBy varchar(50)
   ,constraint FK_Order_customer foreign key (CustomersID) references Customers(ID)
 )

 -- Taọ bảng chi tiết hóa đơn
 create table Order_Detail(
   ID int primary key not null identity
   ,OrderID bigint not null
   ,ProductID int not null
   ,Quantity bigint default(1)
   ,Price decimal(18,0)
   ,constraint FK_product_order foreign key (ProductID) references products(ID)
   ,constraint FK_order foreign key (OrderID) references Orders(ID)
 )

 
 --tạo bảng chủ đề
 create table Topic(
  ID int primary key not null identity
  ,Name nvarchar(150)
  ,MetaTilte varchar(150)
  ,DisplayOrder int default(0)
  ,SeoTitle nvarchar(100)
  ,Status bit default(1)
  ,CreateDate datetime default getdate()
  ,CreateBy varchar(50)
)
 --tạo bảng bài viết
 create table Article(
   ID int primary key not null identity
   ,TopicID int not null
   ,ImageID int not null
   ,Name nvarchar(250)
   ,MetaTitle varchar(250)
   ,Title nvarchar(250)
   ,Description nvarchar(250)
   ,Content nText
   ,ViewCount datetime
   ,Status bit default(0)
   ,CreateDate date default getdate()
   ,CreateBy varchar(50)
    ,constraint FK_topic_Ar foreign key (TopicID) references Topic(ID)
	 ,constraint FK_img_Ar foreign key (ImageID) references Product_Image(ID)
 )
 -- tạo bảng đánh giá
 create table Review(
    ID int primary key not null identity
	,CustomersID int not null
	,ProductID int not null
	,ArticleID int not null
	,comment nvarchar(255)
	,Status bit default(0)
	,CreateDate datetime default getdate()
	,CreateBy varchar(50)
	,constraint FK_Review_customer foreign key (CustomersID) references Customers(ID)
	,constraint FK_Product_review foreign key (ProductID) references Products(ID)
	,constraint FK_review_Ar foreign key (ArticleID) references Article(ID)
 
 )
 --tạo bảng menu
 create table Menu(
    ID int primary key not null identity
	,Name nvarchar(50)
	,Link nvarchar(200)
	,DisplayOrder int
	,Target varchar(50)
	,Status bit default(0)
	,CreateDate datetime  default getdate()
	,CreateBy varchar(50)
 )


 create table Tags(
   ID varchar(50) not null primary key
   ,Name nvarchar(50)
 )

 create table ArticleTag (
    ArticleID int 
	,TagID varchar(50)
 )
 create table About(
 ID int primary key not null identity
   ,MetaTitle varchar(250)
   ,Title nvarchar(250)
   ,Content nText
   ,ViewCount datetime
   ,Status bit default(0)
   ,CreateDate datetime default getdate()
   ,CreateBy varchar(50)
    
 )
 create table Slide(
     ID int primary key not null identity
	 ,Image nvarchar(250)
	 ,DisplayOrder int
	 ,Link varchar(250)
	 ,Description nvarchar(250)
	 ,Status bit default(0)
	 ,CreateDate datetime default getdate()
	 ,CreateBy varchar(50)
	 
 )
 create table Contact(
    ID int not null primary key identity
	,Address nvarchar(150)
	,Email varchar(50)
	,SDT varchar(12)
	,Status bit default(0)
 ) 
 
 --Tạo bảng liên hệ
 create table Feedback(
   ID int primary key not null
   ,Name nvarchar(50)
   ,Email varchar(50)
   ,Content nvarchar(255)
   ,Status bit default(0)
   ,CreateDate date default getdate()
   
 )
 create table SystemConfig(
    ID varchar(50) not null primary key
	,Name nvarchar(50)
	,Type varchar(50)
	,Value nvarchar(250)
	,Status bit 
 )
--Tạo procduce
--Bảng User
create proc pr_user_login
	@UserName varchar(50) 
	,@PassWord varchar(50)
as
begin
	declare @count int
	declare @res bit
	select @count = count (*) from Users where UserName = @UserName and PassWord = @PassWord
	if(@count>0)
	   set @res = 1
	else
	   set @res = 0
	select @res
end

--Bảng Brand

create proc [dbo].[DeleteBrand]
	@Id int 
	as
	 delete from Brands where ID = @Id

create proc [dbo].[InsertBrand]
  
	  @Name nvarchar(50),
	  @Logo nvarchar(255)
	  as
	  insert into Brands(Name, Logo) values(@Name,@Logo)
	
create proc SelectBrand
  as
select * from Brands

create proc [dbo].[UpdateBrand]
       @ID int,
	  @Name nvarchar(50),
	  @Logo nvarchar(255) 
	  as
    update Brands set Name = @Name, Logo= @Logo where ID = @ID
  