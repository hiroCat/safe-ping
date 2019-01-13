SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[t_enviroment_config]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[t_enviroment_config](
	[id] [uniqueidentifier] primary key NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[creationDate] [Datetime] NOT NULL DEFAULT(GETDATE()),
	[ip] [nvarchar](30) NOT NULL)

END

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[t_endpoint_config]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[t_endpoint_config](
	[id] [uniqueidentifier] primary key NOT NULL,
	[id_enviroment] [uniqueidentifier] NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[endpoint] [nvarchar](50) NOT NULL,
	[body] [nvarchar](100) NOT NULL,
	constraint fk_endpointC_enviromentC foreign key (id_enviroment) references t_enviroment_config (id))

END


IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[t_enviroment_status]') AND type in (N'U'))
BEGIN

CREATE TABLE [dbo].[t_enviroment_status](
	[id] [uniqueidentifier] primary key NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[pingStatus] [int] NOT NULL,
	[timeStamp] [Datetime] NOT NULL DEFAULT(GETDATE()))		

END


IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[t_endpoint_status]') AND type in (N'U'))
BEGIN

CREATE TABLE [dbo].[t_endpoint_status](
	[id] [uniqueidentifier] primary key NOT NULL,
	[id_enviroment] [uniqueidentifier] NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[status] [int] NOT NULL,
	constraint fk_endpoint_enviroment foreign key (id_enviroment) references t_enviroment_status (id))		

END

