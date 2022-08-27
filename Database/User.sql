CREATE TABLE [dbo].[User]
(
	[Id] INT NOT NULL , 
    [Account] NVARCHAR(256) NOT NULL, 
    [Pwd] NVARCHAR(256) NOT NULL, 
    [Name] NVARCHAR(256) NOT NULL, 
    [Email] NVARCHAR(256) NOT NULL, 
    [Role] INT NOT NULL, 
    [Status] INT NOT NULL, 
    [CreateTime] DATETIME NOT NULL, 
    [UpdateTime] DATETIME NOT NULL
)

GO