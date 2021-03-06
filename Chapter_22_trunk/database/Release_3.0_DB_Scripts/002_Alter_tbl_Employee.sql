USE [EmployeeTraining]
GO

BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO

ALTER TABLE dbo.tbl_Employee ADD
    Username varchar(50) NOT NULL DEFAULT (' '),
	LoginHash varchar(120) NULL DEFAULT (' '),
	FK_UserRole int NULL
	
GO

ALTER TABLE [dbo].[tbl_Employee]  WITH CHECK ADD  CONSTRAINT [FK_tbl_Employee_tbl_UserRoles_LU] FOREIGN KEY([FK_UserRole])
REFERENCES [dbo].[tbl_UserRoles_LU] ([UserRoleID])
GO

ALTER TABLE [dbo].[tbl_Employee] CHECK CONSTRAINT [FK_tbl_Employee_tbl_UserRoles_LU]



GO


COMMIT

