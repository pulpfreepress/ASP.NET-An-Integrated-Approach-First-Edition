USE [EmployeeTraining]
GO


/******************************************************
   Stored procedure to populate the 
   Dropdown values for Course dropdowns
 *****************************************************/

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure appSP_GetCourseLU

as

begin

SELECT
	CourseID,
	(Code + '-' + Title) AS CodeTitle
FROM
	tbl_Course_LU
ORDER BY 
	Code
end

GO
