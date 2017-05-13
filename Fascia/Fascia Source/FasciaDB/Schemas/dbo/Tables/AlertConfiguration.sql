CREATE TABLE [dbo].[AlertConfiguration]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Key] VARCHAR(50) NOT NULL, 
    [Value] VARCHAR(100) NULL, 
    [MinValue] INT NULL, 
    [MaxValue] INT NULL
	
)
