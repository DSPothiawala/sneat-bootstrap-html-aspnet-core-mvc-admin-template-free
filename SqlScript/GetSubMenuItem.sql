
/****** Object:  StoredProcedure [dbo].[GetSubMenuItem]    Script Date: 2/17/2018 6:05:10 PM ******/
DROP PROCEDURE [dbo].[GetSubMenuItem]
GO

/****** Object:  StoredProcedure [dbo].[GetSubMenuItem]    Script Date: 2/17/2018 6:05:10 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO







CREATE PROCEDURE [dbo].[GetSubMenuItem]
@userid int = 0
	
	
AS


Select A.*
From Menu A









GO


