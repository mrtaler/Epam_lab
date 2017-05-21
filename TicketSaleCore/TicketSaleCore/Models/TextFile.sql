--
-- Скрипт сгенерирован Devart dbForge Studio for SQL Server, Версия 5.4.275.0
-- Домашняя страница продукта: http://devart.com/ru/dbforge/sql/studio
-- Дата скрипта: 21.05.2017 19:57:58
-- Версия сервера: 13.00.4202
-- Версия клиента: 
--



USE master
GO

IF DB_NAME() <> N'master' SET NOEXEC ON
GO

--
-- Создать таблицу [dbo].[Venue]
--
PRINT (N'Создать таблицу [dbo].[Venue]')
GO
CREATE TABLE dbo.Venue (
  id int NOT NULL,
  Name varchar(50) NULL,
  Address varchar(50) NULL,
  City int NULL,
  CONSTRAINT PK_Venue_id PRIMARY KEY CLUSTERED (id)
)
ON [PRIMARY]
GO

--
-- Создать таблицу [dbo].[tUsers]
--
PRINT (N'Создать таблицу [dbo].[tUsers]')
GO
CREATE TABLE dbo.tUsers (
  Id nvarchar(450) NOT NULL,
  AccessFailedCount int NOT NULL,
  ConcurrencyStamp nvarchar(max) NULL,
  Email nvarchar(256) NULL,
  EmailConfirmed bit NOT NULL,
  LockoutEnabled bit NOT NULL,
  LockoutEnd datetimeoffset NULL,
  NormalizedEmail nvarchar(256) NULL,
  NormalizedUserName nvarchar(256) NULL,
  PasswordHash nvarchar(max) NULL,
  PhoneNumber nvarchar(max) NULL,
  PhoneNumberConfirmed bit NOT NULL,
  SecurityStamp nvarchar(max) NULL,
  TwoFactorEnabled bit NOT NULL,
  UserName nvarchar(256) NULL,
  Year int NOT NULL,
  CONSTRAINT PK_AspNetUsers PRIMARY KEY CLUSTERED (Id)
)
ON [PRIMARY]
TEXTIMAGE_ON [PRIMARY]
GO

--
-- Создать таблицу [dbo].[Ticket]
--
PRINT (N'Создать таблицу [dbo].[Ticket]')
GO
CREATE TABLE dbo.Ticket (
  id int NOT NULL,
  Event int NULL,
  Price money NULL,
  Seller nvarchar(450) NULL,
  CONSTRAINT [PK_Ticket _id] PRIMARY KEY CLUSTERED (id)
)
ON [PRIMARY]
GO

--
-- Создать таблицу [dbo].[Status]
--
PRINT (N'Создать таблицу [dbo].[Status]')
GO
CREATE TABLE dbo.Status (
  id int NOT NULL,
  Name varchar(50) NULL,
  CONSTRAINT PK_Status_id PRIMARY KEY CLUSTERED (id)
)
ON [PRIMARY]
GO

--
-- Создать таблицу [dbo].[Order]
--
PRINT (N'Создать таблицу [dbo].[Order]')
GO
CREATE TABLE dbo.[Order] (
  Id int NOT NULL,
  Ticket int NULL,
  Status int NULL,
  Buyer nvarchar(450) NULL,
  TrackNO nvarchar(50) NULL,
  CONSTRAINT PK_Order_Id PRIMARY KEY CLUSTERED (Id)
)
ON [PRIMARY]
GO

--
-- Создать таблицу [dbo].[EventType]
--
PRINT (N'Создать таблицу [dbo].[EventType]')
GO
CREATE TABLE dbo.EventType (
  id int NOT NULL,
  name varchar(50) NULL,
  CONSTRAINT PK_EventType_id PRIMARY KEY CLUSTERED (id)
)
ON [PRIMARY]
GO

--
-- Создать таблицу [dbo].[Event]
--
PRINT (N'Создать таблицу [dbo].[Event]')
GO
CREATE TABLE dbo.Event (
  id int NOT NULL,
  Name varchar(50) NULL,
  Date datetime NULL,
  Venue int NULL,
  Banner varchar(50) NULL,
  Description varchar(50) NULL,
  Type int NULL,
  CONSTRAINT PK_Event_id PRIMARY KEY CLUSTERED (id)
)
ON [PRIMARY]
GO

--
-- Создать таблицу [dbo].[City]
--
PRINT (N'Создать таблицу [dbo].[City]')
GO
CREATE TABLE dbo.City (
  id int NOT NULL,
  Name varchar(50) NULL,
  CONSTRAINT PK_City_id PRIMARY KEY CLUSTERED (id)
)
ON [PRIMARY]
GO

USE master
GO

IF DB_NAME() <> N'master' SET NOEXEC ON
GO

--
-- Создать внешний ключ [FK_Venue_City_id] для объекта типа таблица [dbo].[Venue]
--
PRINT (N'Создать внешний ключ [FK_Venue_City_id] для объекта типа таблица [dbo].[Venue]')
GO
ALTER TABLE dbo.Venue
  ADD CONSTRAINT FK_Venue_City_id FOREIGN KEY (City) REFERENCES dbo.City (id)
GO

--
-- Создать внешний ключ [FK_Event_EventType_id] для объекта типа таблица [dbo].[Event]
--
PRINT (N'Создать внешний ключ [FK_Event_EventType_id] для объекта типа таблица [dbo].[Event]')
GO
ALTER TABLE dbo.Event
  ADD CONSTRAINT FK_Event_EventType_id FOREIGN KEY (Type) REFERENCES dbo.EventType (id)
GO

--
-- Создать внешний ключ [FK_Event_Venue_id] для объекта типа таблица [dbo].[Event]
--
PRINT (N'Создать внешний ключ [FK_Event_Venue_id] для объекта типа таблица [dbo].[Event]')
GO
ALTER TABLE dbo.Event
  ADD CONSTRAINT FK_Event_Venue_id FOREIGN KEY (Venue) REFERENCES dbo.Venue (id)
GO

--
-- Создать внешний ключ [FK_Ticket _tUsers_Id] для объекта типа таблица [dbo].[Ticket]
--
PRINT (N'Создать внешний ключ [FK_Ticket _tUsers_Id] для объекта типа таблица [dbo].[Ticket]')
GO
ALTER TABLE dbo.Ticket
  ADD CONSTRAINT [FK_Ticket _tUsers_Id] FOREIGN KEY (Seller) REFERENCES dbo.tUsers (Id)
GO

--
-- Создать внешний ключ [FK_Ticket_Event_id] для объекта типа таблица [dbo].[Ticket]
--
PRINT (N'Создать внешний ключ [FK_Ticket_Event_id] для объекта типа таблица [dbo].[Ticket]')
GO
ALTER TABLE dbo.Ticket
  ADD CONSTRAINT FK_Ticket_Event_id FOREIGN KEY (Event) REFERENCES dbo.Event (id)
GO

--
-- Создать внешний ключ [FK_Order_Status_id] для объекта типа таблица [dbo].[Order]
--
PRINT (N'Создать внешний ключ [FK_Order_Status_id] для объекта типа таблица [dbo].[Order]')
GO
ALTER TABLE dbo.[Order]
  ADD CONSTRAINT FK_Order_Status_id FOREIGN KEY (Status) REFERENCES dbo.Status (id)
GO

--
-- Создать внешний ключ [FK_Order_Ticket_id] для объекта типа таблица [dbo].[Order]
--
PRINT (N'Создать внешний ключ [FK_Order_Ticket_id] для объекта типа таблица [dbo].[Order]')
GO
ALTER TABLE dbo.[Order]
  ADD CONSTRAINT FK_Order_Ticket_id FOREIGN KEY (Ticket) REFERENCES dbo.Ticket (id)
GO

--
-- Создать внешний ключ [FK_Order_tUsers_Id] для объекта типа таблица [dbo].[Order]
--
PRINT (N'Создать внешний ключ [FK_Order_tUsers_Id] для объекта типа таблица [dbo].[Order]')
GO
ALTER TABLE dbo.[Order]
  ADD CONSTRAINT FK_Order_tUsers_Id FOREIGN KEY (Buyer) REFERENCES dbo.tUsers (Id)
GO
SET NOEXEC OFF
GO