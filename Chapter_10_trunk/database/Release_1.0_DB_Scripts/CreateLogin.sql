/****** Object:  Login [EmployeeTraining]    Script Date: 10/13/2012 08:52:28 ******/
CREATE LOGIN [EmployeeTraining] WITH PASSWORD=N'a1e5i9a1e5i9', DEFAULT_DATABASE=[EmployeeTraining], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
GO

EXEC sys.sp_addsrvrolemember @loginame = N'EmployeeTraining', @rolename = N'dbcreator'
GO





